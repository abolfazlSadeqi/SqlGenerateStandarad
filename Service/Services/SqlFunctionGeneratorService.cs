using Core;
using Service.Interfaces;
using System.Text;

namespace Service.Services;

public class SqlFunctionGeneratorService : ISqlFunctionGeneratorService
{
    public string GenerateCreateFunctionScript(CreateFunctionViewModel model)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"USE [{model.DatabaseName}]");
        sb.AppendLine("GO");

        // بررسی وجود فانکشن و Drop در صورت نیاز
        sb.AppendLine($"IF OBJECT_ID('{model.FunctionName}', 'FN') IS NOT NULL OR OBJECT_ID('{model.FunctionName}', 'IF') IS NOT NULL OR OBJECT_ID('{model.FunctionName}', 'TF') IS NOT NULL");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    DROP FUNCTION [{model.Schema}].[{model.FunctionName}];");
        sb.AppendLine("END");
        sb.AppendLine("GO");

        sb.AppendLine($"CREATE FUNCTION [{model.Schema}].[{model.FunctionName}]");
        sb.AppendLine(model.FunctionDefinition);
        sb.AppendLine("GO");

        return sb.ToString();
    }

    public string GenerateDropFunctionScript(DropFunctionViewModel model)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"USE [{model.DatabaseName}]");
        sb.AppendLine("GO");

        sb.AppendLine($"IF OBJECT_ID('{model.FunctionName}', 'FN') IS NOT NULL OR OBJECT_ID('{model.FunctionName}', 'IF') IS NOT NULL OR OBJECT_ID('{model.FunctionName}', 'TF') IS NOT NULL");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    DROP FUNCTION [{model.Schema}].[{model.FunctionName}];");
        sb.AppendLine("END");
        sb.AppendLine("GO");

        return sb.ToString();
    }
}
