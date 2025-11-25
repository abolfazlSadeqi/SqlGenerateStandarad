using Service.Interfaces;
using System.Text;

namespace Service.Services;

public class SqlColumnGeneratorService : ISqlColumnGeneratorService
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
    public string GenerateAlterColumnScript(string schema, string tableName, string columnName, string dataType, bool isNullable)
    {
        schema = NormalizeSchema(schema);
        var sb = new StringBuilder();
        // Check if the table exists
        sb.AppendLine("IF ( NOT EXISTS (");
        sb.AppendLine("    SELECT * FROM INFORMATION_SCHEMA.TABLES");
        sb.AppendLine($"    WHERE TABLE_SCHEMA = '{schema.Replace("'", "''")}'");
        sb.AppendLine($"      AND TABLE_NAME = '{tableName.Replace("'", "''")}'");
        sb.AppendLine(") )");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    RAISERROR('Table {tableName} does not exist in schema {schema}.', 16, 1);");
        sb.AppendLine("    RETURN;");
        sb.AppendLine("END");

        sb.AppendLine("IF ( EXISTS (");
        sb.AppendLine("    SELECT * FROM INFORMATION_SCHEMA.COLUMNS");
        sb.AppendLine($"    WHERE TABLE_SCHEMA = '{schema.Replace("'", "''")}'");
        sb.AppendLine($"      AND TABLE_NAME = '{tableName.Replace("'", "''")}'");
        sb.AppendLine($"      AND COLUMN_NAME = '{columnName.Replace("'", "''")}'");
        sb.AppendLine(") )");
        sb.AppendLine("BEGIN");
        sb.Append("    ALTER TABLE ");
        sb.Append(BracketIdentifier(schema));
        sb.Append(".");
        sb.Append(BracketIdentifier(tableName));
        sb.Append(" ALTER COLUMN ");
        sb.Append(BracketIdentifier(columnName));
        sb.Append(" ");
        sb.Append(dataType);
        sb.Append(isNullable ? " NULL" : " NOT NULL");
        sb.AppendLine(";");
        sb.AppendLine("END");

        return sb.ToString();
    }
    public string GenerateAddColumnScript(string schema, string tableName, string columnName, string dataType, bool isNullable, string defaultValue)
    {
        schema = NormalizeSchema(schema);
        var sb = new StringBuilder();

        // Check if the table exists
        sb.AppendLine("IF ( NOT EXISTS (");
        sb.AppendLine("    SELECT * FROM INFORMATION_SCHEMA.TABLES");
        sb.AppendLine($"    WHERE TABLE_SCHEMA = '{schema.Replace("'", "''")}'");
        sb.AppendLine($"      AND TABLE_NAME = '{tableName.Replace("'", "''")}'");
        sb.AppendLine(") )");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    RAISERROR('Table {tableName} does not exist in schema {schema}.', 16, 1);");
        sb.AppendLine("    RETURN;");
        sb.AppendLine("END");

        // Check if the column already exists
        sb.AppendLine("IF ( NOT EXISTS (");
        sb.AppendLine("    SELECT * FROM INFORMATION_SCHEMA.COLUMNS");
        sb.AppendLine($"    WHERE TABLE_SCHEMA = '{schema.Replace("'", "''")}'");
        sb.AppendLine($"      AND TABLE_NAME = '{tableName.Replace("'", "''")}'");
        sb.AppendLine($"      AND COLUMN_NAME = '{columnName.Replace("'", "''")}'");
        sb.AppendLine(") )");
        sb.AppendLine("BEGIN");
        sb.Append("    ALTER TABLE ");
        sb.Append(BracketIdentifier(schema));
        sb.Append(".");
        sb.Append(BracketIdentifier(tableName));
        sb.Append(" ADD ");
        sb.Append(BracketIdentifier(columnName));
        sb.Append(" ");
        sb.Append(dataType);
        if (!isNullable)
        {
            sb.Append(" NOT NULL");
        }
        else
        {
            sb.Append(" NULL");
        }

        // default value (optional) — if provided, add DEFAULT clause (inline) and optionally add a constraint name
        if (!string.IsNullOrWhiteSpace(defaultValue))
        {
            // Use a generated constraint name to be safe: DF_<Table>_<Column>
            var constraint = $"DF_{tableName}_{columnName}".Replace(" ", "_");
            // ensure no invalid chars - keep simple (replace ] if any)
            constraint = constraint.Replace("]", "");
            sb.Append($" CONSTRAINT {BracketIdentifier(constraint)} DEFAULT ({defaultValue})");
        }

        sb.AppendLine(";");
        sb.AppendLine("END");
        return sb.ToString();
    }

    public string GenerateDropColumnScript(string schema, string tableName, string columnName)
    {
        schema = NormalizeSchema(schema);
        var sb = new StringBuilder();

        // Check if the table exists
        sb.AppendLine("IF ( NOT EXISTS (");
        sb.AppendLine("    SELECT * FROM INFORMATION_SCHEMA.TABLES");
        sb.AppendLine($"    WHERE TABLE_SCHEMA = '{schema.Replace("'", "''")}'");
        sb.AppendLine($"      AND TABLE_NAME = '{tableName.Replace("'", "''")}'");
        sb.AppendLine(") )");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    RAISERROR('Table {tableName} does not exist in schema {schema}.', 16, 1);");
        sb.AppendLine("    RETURN;");
        sb.AppendLine("END");

        // Check if the column exists
        sb.AppendLine("IF ( EXISTS (");
        sb.AppendLine("    SELECT * FROM INFORMATION_SCHEMA.COLUMNS");
        sb.AppendLine($"    WHERE TABLE_SCHEMA = '{schema.Replace("'", "''")}'");
        sb.AppendLine($"      AND TABLE_NAME = '{tableName.Replace("'", "''")}'");
        sb.AppendLine($"      AND COLUMN_NAME = '{columnName.Replace("'", "''")}'");
        sb.AppendLine(") )");
        sb.AppendLine("BEGIN");
        sb.Append("    ALTER TABLE ");
        sb.Append(BracketIdentifier(schema));
        sb.Append(".");
        sb.Append(BracketIdentifier(tableName));
        sb.Append(" DROP COLUMN ");
        sb.Append(BracketIdentifier(columnName));
        sb.AppendLine(";");
        sb.AppendLine("END");
        sb.AppendLine("ELSE");
        sb.AppendLine($"    RAISERROR('Column {columnName} does not exist in table {tableName}.', 16, 1);");

        return sb.ToString();
    }

    public string GenerateRenameColumnScript(string schema, string tableName, string oldColumnName, string newColumnName)
    {
        schema = NormalizeSchema(schema);
        var sb = new StringBuilder();

        // Check if the table exists
        sb.AppendLine("IF ( NOT EXISTS (");
        sb.AppendLine("    SELECT * FROM INFORMATION_SCHEMA.TABLES");
        sb.AppendLine($"    WHERE TABLE_SCHEMA = '{schema.Replace("'", "''")}'");
        sb.AppendLine($"      AND TABLE_NAME = '{tableName.Replace("'", "''")}'");
        sb.AppendLine(") )");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    RAISERROR('Table {tableName} does not exist in schema {schema}.', 16, 1);");
        sb.AppendLine("    RETURN;");
        sb.AppendLine("END");

        // Check if the old column exists
        sb.AppendLine("IF ( EXISTS (");
        sb.AppendLine("    SELECT * FROM INFORMATION_SCHEMA.COLUMNS");
        sb.AppendLine($"    WHERE TABLE_SCHEMA = '{schema.Replace("'", "''")}'");
        sb.AppendLine($"      AND TABLE_NAME = '{tableName.Replace("'", "''")}'");
        sb.AppendLine($"      AND COLUMN_NAME = '{oldColumnName.Replace("'", "''")}'");
        sb.AppendLine(") )");
        sb.AppendLine("AND NOT EXISTS (");
        sb.AppendLine("    SELECT * FROM INFORMATION_SCHEMA.COLUMNS");
        sb.AppendLine($"    WHERE TABLE_SCHEMA = '{schema.Replace("'", "''")}'");
        sb.AppendLine($"      AND TABLE_NAME = '{tableName.Replace("'", "''")}'");
        sb.AppendLine($"      AND COLUMN_NAME = '{newColumnName.Replace("'", "''")}'");
        sb.AppendLine("))");
        sb.AppendLine("BEGIN");
        sb.Append("    EXEC sp_rename ");
        sb.Append("'");
        sb.Append(BracketIdentifier(schema) + "." + BracketIdentifier(tableName) + "." + BracketIdentifier(oldColumnName));
        sb.Append("', ");
        sb.Append("'" + newColumnName + "', 'COLUMN'");
        sb.AppendLine(";");
        sb.AppendLine("END");
        sb.AppendLine("ELSE");
        sb.AppendLine($"    RAISERROR('Column {oldColumnName} does not exist in table {tableName}.', 16, 1);");

        return sb.ToString();
    }

}
