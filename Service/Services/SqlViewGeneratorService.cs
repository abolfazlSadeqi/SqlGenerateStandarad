using Core;
using Service.Interfaces;
using System.Text;

namespace Service.Services;

public class SqlViewGeneratorService : ISqlViewGeneratorService
{
    public string GenerateCreateViewScript(ViewManageViewModel model)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"USE [{model.DatabaseName}]");
        sb.AppendLine("GO");

        // بررسی وجود ویو و Drop در صورت نیاز
        sb.AppendLine($"IF OBJECT_ID('{model.ViewName}', 'V') IS NOT NULL");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    DROP VIEW [{model.Schema}].[{model.ViewName}];");
        sb.AppendLine("END");
        sb.AppendLine("GO");

        sb.AppendLine($"CREATE VIEW [{model.Schema}].[{model.ViewName}] AS");
        sb.AppendLine(model.SelectQuery);
        sb.AppendLine("GO");

        return sb.ToString();
    }

    public string GenerateDropViewScript(DropViewViewModel model)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"USE [{model.DatabaseName}]");
        sb.AppendLine("GO");
        sb.AppendLine($"IF OBJECT_ID('{model.ViewName}', 'V') IS NOT NULL");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    DROP VIEW [{model.Schema}].[{model.ViewName}];");
        sb.AppendLine("END");
        sb.AppendLine("GO");
        return sb.ToString();
    }
    public string GenerateCreateProcedureScript(ProcedureManageViewModel model)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"USE [{model.DatabaseName}]");
        sb.AppendLine("GO");

        // بررسی وجود پروسیجر و Drop در صورت نیاز
        sb.AppendLine($"IF OBJECT_ID('{model.ProcedureName}', 'P') IS NOT NULL");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    DROP PROCEDURE [{model.Schema}].[{model.ProcedureName}];");
        sb.AppendLine("END");
        sb.AppendLine("GO");

        sb.AppendLine($"CREATE PROCEDURE [{model.Schema}].[{model.ProcedureName}] as ");
        sb.AppendLine(model.ProcedureDefinition);
        sb.AppendLine("GO");

        return sb.ToString();
    }

    public string GenerateDropProcedureScript(DropProcedureViewModel model)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"USE [{model.DatabaseName}]");
        sb.AppendLine("GO");

        sb.AppendLine($"IF OBJECT_ID('{model.ProcedureName}', 'P') IS NOT NULL");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    DROP PROCEDURE [{model.Schema}].[{model.ProcedureName}];");
        sb.AppendLine("END");
        sb.AppendLine("GO");

        return sb.ToString();
    }

}
