using Core;
using Service.Interfaces;
using System.Text;

namespace Service.Services;

public class SqlTAbleGeneratorService : ISqlTAbleGeneratorService
{
    // Escape identifier: wrap in [ ] and double any closing bracket inside name
    private string BracketIdentifier(string identifier)
    {
        if (string.IsNullOrWhiteSpace(identifier)) return identifier;
        var clean = identifier.Replace("]", "]]");
        return $"[{clean}]";
    }

    // Normalize schema (default dbo)
    private string NormalizeSchema(string schema) => string.IsNullOrWhiteSpace(schema) ? "dbo" : schema;
    public string GenerateDropTableScript(string schema, string tableName)
    {
        schema = NormalizeSchema(schema);
        var sb = new StringBuilder();

        sb.AppendLine("IF ( EXISTS (");
        sb.AppendLine("    SELECT * FROM INFORMATION_SCHEMA.TABLES");
        sb.AppendLine($"    WHERE TABLE_SCHEMA = '{schema.Replace("'", "''")}'");
        sb.AppendLine($"      AND TABLE_NAME = '{tableName.Replace("'", "''")}'");
        sb.AppendLine(") )");
        sb.AppendLine("BEGIN");
        sb.Append("    DROP TABLE ");
        sb.Append(BracketIdentifier(schema));
        sb.Append(".");
        sb.Append(BracketIdentifier(tableName));
        sb.AppendLine(";");
        sb.AppendLine("END");

        return sb.ToString();
    }
    public string GenerateRenameTableScript(string schema, string oldTableName, string newTableName)
    {
        schema = NormalizeSchema(schema);
        var sb = new StringBuilder();

        sb.AppendLine("IF ( EXISTS (");
        sb.AppendLine("    SELECT * FROM INFORMATION_SCHEMA.TABLES");
        sb.AppendLine($"    WHERE TABLE_SCHEMA = '{schema.Replace("'", "''")}'");
        sb.AppendLine($"      AND TABLE_NAME = '{oldTableName.Replace("'", "''")}'");
        sb.AppendLine(") )");
        sb.AppendLine("AND NOT EXISTS (");
        sb.AppendLine("    SELECT * FROM INFORMATION_SCHEMA.TABLES");
        sb.AppendLine($"    WHERE TABLE_SCHEMA = '{schema.Replace("'", "''")}'");
        sb.AppendLine($"      AND TABLE_NAME = '{newTableName.Replace("'", "''")}'");
        sb.AppendLine(") ");
        sb.AppendLine("BEGIN");
        sb.Append("    EXEC sp_rename ");
        sb.Append("'");
        sb.Append(BracketIdentifier(schema) + "." + BracketIdentifier(oldTableName));
        sb.Append("', ");
        sb.Append("'" + newTableName + "', 'OBJECT'");
        sb.AppendLine(";");
        sb.AppendLine("END");

        return sb.ToString();
    }

    public string GenerateCreateTableScript(TableCreateViewModel model)
    {
        var schema = NormalizeSchema(model.Schema);
        var sb = new StringBuilder();

        sb.AppendLine($"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{schema}' AND TABLE_NAME = '{model.TableName}')");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"CREATE TABLE {BracketIdentifier(schema)}.{BracketIdentifier(model.TableName)} (");

        // ستون‌ها
        var columnDefs = new List<string>();
        foreach (var col in model.Columns)
        {
            var colDef = new StringBuilder();
            colDef.Append(BracketIdentifier(col.ColumnName));
            colDef.Append(" ");
            colDef.Append(col.DataType);
            colDef.Append(col.IsNullable ? " NULL" : " NOT NULL");
            if (!string.IsNullOrWhiteSpace(col.DefaultValue))
            {
                var constraint = $"DF_{model.TableName}_{col.ColumnName}".Replace(" ", "_").Replace("]", "");
                colDef.Append($" CONSTRAINT {BracketIdentifier(constraint)} DEFAULT ({col.DefaultValue})");
            }
            columnDefs.Add(colDef.ToString());
        }

        sb.AppendLine(string.Join(",\n", columnDefs));

        // Primary Key
        var pkColumns = model.Columns.Where(c => c.IsPrimaryKey).ToList();
        if (pkColumns.Any())
        {
            var pkCols = string.Join(", ", pkColumns.Select(c => BracketIdentifier(c.ColumnName)));
            sb.AppendLine($", CONSTRAINT {BracketIdentifier($"PK_{model.TableName}")} PRIMARY KEY ({pkCols})");
        }

        sb.AppendLine(")");

        // Filegroup / Partition
        if (!string.IsNullOrWhiteSpace(model.PartitionScheme))
        {
            sb.Append($" ON {model.PartitionScheme}");
        }
        else if (!string.IsNullOrWhiteSpace(model.FileGroup))
        {
            sb.Append($" ON {model.FileGroup}");
        }

        sb.AppendLine(";");
        sb.AppendLine("END");

        return sb.ToString();
    }

    public string GenerateTableStorageUpdateScript(TableStorageUpdateViewModel model)
    {
        var sb = new StringBuilder();
        var schema = string.IsNullOrWhiteSpace(model.Schema) ? "dbo" : model.Schema;

        sb.AppendLine($"USE [{model.DatabaseName}]");
        sb.AppendLine("GO");

        // --- تغییر فایل‌گروپ جدول ---
        if (!string.IsNullOrWhiteSpace(model.NewFileGroup))
        {
            sb.AppendLine($"-- Move table [{schema}].[{model.TableName}] to new filegroup [{model.NewFileGroup}]");
            sb.AppendLine($"ALTER TABLE [{schema}].[{model.TableName}]");
            sb.AppendLine($"REBUILD PARTITION = ALL"); // SQL Server 2016+، می‌تواند تغییر FILEGROUP باشد
            sb.AppendLine($"ON [{model.NewFileGroup}];");
            sb.AppendLine("GO");
        }

        // --- ایجاد Partition Function ---
        if (!string.IsNullOrWhiteSpace(model.PartitionFunctionName) && !string.IsNullOrWhiteSpace(model.RangeValues))
        {
            sb.AppendLine($"IF NOT EXISTS (SELECT * FROM sys.partition_functions WHERE name = '{model.PartitionFunctionName}')");
            sb.AppendLine("BEGIN");
            sb.AppendLine($"    CREATE PARTITION FUNCTION {model.PartitionFunctionName} (int)");
            sb.AppendLine($"    AS RANGE {model.RangeBoundary} FOR VALUES ({model.RangeValues});");
            sb.AppendLine("END");
            sb.AppendLine("GO");
        }

        // --- ایجاد Partition Scheme ---
        if (!string.IsNullOrWhiteSpace(model.PartitionSchemeName) && !string.IsNullOrWhiteSpace(model.PartitionFunctionName))
        {
            sb.AppendLine($"IF NOT EXISTS (SELECT * FROM sys.partition_schemes WHERE name = '{model.PartitionSchemeName}')");
            sb.AppendLine("BEGIN");
            sb.AppendLine($"    CREATE PARTITION SCHEME {model.PartitionSchemeName}");
            sb.AppendLine($"    AS PARTITION {model.PartitionFunctionName} TO ([PRIMARY]); -- باید فایل‌گروپ‌ها جایگزین شوند");
            sb.AppendLine("END");
            sb.AppendLine("GO");
        }

        // --- تغییر جدول به Partition Scheme ---
        if (!string.IsNullOrWhiteSpace(model.PartitionSchemeName) && !string.IsNullOrWhiteSpace(model.PartitionColumn))
        {
            sb.AppendLine($"-- Switch table [{schema}].[{model.TableName}] to partition scheme [{model.PartitionSchemeName}]");
            sb.AppendLine($"CREATE CLUSTERED INDEX IX_{model.TableName}_{model.PartitionColumn}");
            sb.AppendLine($"ON [{model.PartitionSchemeName}] ({model.PartitionColumn});");
            sb.AppendLine("GO");
        }

        return sb.ToString();
    }

    public string GenerateCreateDatabaseScript(string databaseName, string recoveryModel, string fileGroupName, string dataFilePath, string logFilePath, bool isDeleteIfExists, string dataFileSize, string logFileSize, string maxDataFileSize, string maxLogFileSize, string autoGrowthIncrement)
    {
        var sb = new StringBuilder();

        // Handle DELETE IF EXISTS logic
        if (isDeleteIfExists)
        {
            sb.AppendLine($"IF EXISTS (SELECT * FROM sys.databases WHERE name = '{databaseName}')");
            sb.AppendLine($"BEGIN");
            sb.AppendLine($"    DROP DATABASE {databaseName};");
            sb.AppendLine($"END");
        }

        // SQL for creating database with basic file settings
        sb.AppendLine($"CREATE DATABASE {databaseName}");
        sb.AppendLine($"ON PRIMARY");
        sb.AppendLine($"(NAME = '{databaseName}_Data', FILENAME = '{dataFilePath}\\{databaseName}_Data.mdf', SIZE = {dataFileSize}, MAXSIZE = {maxDataFileSize}, FILEGROWTH = {autoGrowthIncrement})");

        // SQL for log file
        sb.AppendLine($"LOG ON");
        sb.AppendLine($"(NAME = '{databaseName}_Log', FILENAME = '{logFilePath}\\{databaseName}_Log.ldf', SIZE = {logFileSize}, MAXSIZE = {maxLogFileSize}, FILEGROWTH = {autoGrowthIncrement})");
            


        sb.AppendLine($"ALTER DATABASE[{databaseName}] SET RECOVERY {recoveryModel};");
        return sb.ToString();
    }

}
