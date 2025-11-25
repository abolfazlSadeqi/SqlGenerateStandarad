using Core;
using Service.Interfaces;
using System.Text;

namespace Service.Services;

public class SqlIndexGeneratorService : ISqlIndexGeneratorService
{

    public string GenerateCreateIndexScript(IndexManageViewModel model)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"USE [{model.DatabaseName}]");
        sb.AppendLine("GO");

        sb.AppendLine($"IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = '{model.IndexName}' AND object_id = OBJECT_ID('{model.TableName}'))");
        sb.AppendLine("BEGIN");

        // Begin index creation
        sb.Append($"CREATE ");

        // If it's a primary key or foreign key
        if (model.IndexType == IndexType.Clustered)
        {
            sb.Append("CLUSTERED ");
        }
        else if (model.IsUnique)
        {
            sb.Append("UNIQUE ");
        }
        else
        {
            sb.Append(model.IndexType == IndexType.Clustered ? "CLUSTERED " : "NONCLUSTERED ");
        }

        sb.AppendLine($"INDEX [{model.IndexName}] ON [{model.TableName}]");

        // Index Columns with Sort Order
        sb.AppendLine("(");
        for (int i = 0; i < model.Columns.Count; i++)
        {
            sb.Append($"    [{model.Columns[i].ColumnName}]");
            if (model.Columns[i].SortOrder == SortOrder.Descending)
                sb.Append(" DESC");
            else
                sb.Append(" ASC");

            if (i < model.Columns.Count - 1)
                sb.AppendLine(",");
        }
        sb.AppendLine(")");

        // If it's not clustered, add covering index
        if (!(model.IndexType == IndexType.Clustered) && !string.IsNullOrWhiteSpace(model.IncludeColumns))
        {
            sb.AppendLine($"INCLUDE ({model.IncludeColumns})");
        }

        // Filter expression for filtered index
        if (!string.IsNullOrWhiteSpace(model.FilterExpression))
        {
            sb.AppendLine($"WHERE {model.FilterExpression}");
        }

        // Filegroup specification
        if (!string.IsNullOrWhiteSpace(model.FileGroup))
        {
            sb.AppendLine($"ON [{model.FileGroup}]");
        }

        sb.AppendLine("END");
        sb.AppendLine("GO");

        return sb.ToString();
    }

    public string GenerateDropIndexScript(IndexManageViewModel model)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"USE [{model.DatabaseName}]");
        sb.AppendLine("GO");

        sb.AppendLine($"IF EXISTS (SELECT * FROM sys.indexes WHERE name = '{model.IndexName}' AND object_id = OBJECT_ID('{model.TableName}'))");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    DROP INDEX [{model.IndexName}] ON [{model.TableName}];");
        sb.AppendLine("END");
        sb.AppendLine("GO");

        return sb.ToString();
    }
}
