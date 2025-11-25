using Core;
using Service.Interfaces;
using System.Text;

namespace Service.Services;

public class SqlSchemaSGeneratorService : ISqlSchemaSGeneratorService
{
    public string GenerateCreateSchemaScript(CreateSchemaViewModel model)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"USE [{model.DatabaseName}]");
        sb.AppendLine("GO");

        sb.AppendLine($"IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = '{model.SchemaName}')");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"   exec('CREATE SCHEMA [{model.SchemaName}];')");
        sb.AppendLine("END");
        sb.AppendLine("GO");

        return sb.ToString();
    }

    public string GenerateDropSchemaScript(DropSchemaViewModel model)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"USE [{model.DatabaseName}]");
        sb.AppendLine("GO");

        sb.AppendLine($"-- Drop all objects in schema [{model.SchemaName}]");
        sb.AppendLine($"DECLARE @sql NVARCHAR(MAX) = N'';");
        sb.AppendLine($"SELECT @sql += 'DROP TABLE [' + s.name + '].[' + o.name + '];' + CHAR(13)");
        sb.AppendLine($"FROM sys.objects o");
        sb.AppendLine($"JOIN sys.schemas s ON o.schema_id = s.schema_id");
        sb.AppendLine($"WHERE s.name = '{model.SchemaName}' AND o.type = 'U';");
        sb.AppendLine("EXEC sp_executesql @sql;");
        sb.AppendLine("GO");

        sb.AppendLine($"IF EXISTS (SELECT * FROM sys.schemas WHERE name = '{model.SchemaName}')");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    DROP SCHEMA [{model.SchemaName}];");
        sb.AppendLine("END");
        sb.AppendLine("GO");

        return sb.ToString();
    }

    public string GenerateRenameSchemaScript(RenameSchemaViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.NewSchemaName))
            throw new ArgumentException("NewSchemaName is required for renaming schema.");

        var sb = new StringBuilder();

        sb.AppendLine($"USE [{model.DatabaseName}]");
        sb.AppendLine("GO");

        sb.AppendLine($"-- Move all objects from [{model.SchemaName}] to [{model.NewSchemaName}]");
        sb.AppendLine($"DECLARE @sql NVARCHAR(MAX) = N'';");
        sb.AppendLine($"SELECT @sql += 'ALTER SCHEMA [{model.NewSchemaName}] TRANSFER [' + s.name + '].[' + o.name + '];' + CHAR(13)");
        sb.AppendLine($"FROM sys.objects o");
        sb.AppendLine($"JOIN sys.schemas s ON o.schema_id = s.schema_id");
        sb.AppendLine($"WHERE s.name = '{model.SchemaName}';");
        sb.AppendLine("EXEC sp_executesql @sql;");
        sb.AppendLine("GO");

        return sb.ToString();
    }
}
