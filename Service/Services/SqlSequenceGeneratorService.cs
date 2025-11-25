using Core;
using Service.Interfaces;
using System.Text;

namespace Service.Services;

public class SqlSequenceGeneratorService : ISqlSequenceGeneratorService
{

    public string GenerateDropSequenceScript(DropSequenceViewModel model)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"USE [{model.DatabaseName}]");
        sb.AppendLine("GO");

        sb.AppendLine($"IF EXISTS (SELECT * FROM sys.sequences WHERE name = '{model.SequenceName}')");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    DROP SEQUENCE [{model.SequenceName}];");
        sb.AppendLine("END");
        sb.AppendLine("GO");

        return sb.ToString();
    }



    public string GenerateAdvancedCreateSequenceScript(AdvancedSequenceManageViewModel model)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"USE [{model.DatabaseName}]");
        sb.AppendLine("GO");

        sb.AppendLine($"IF NOT EXISTS (SELECT * FROM sys.sequences WHERE name = '{model.SequenceName}')");
        sb.AppendLine("BEGIN");

        sb.Append($"CREATE SEQUENCE [{model.SequenceName}]");
        sb.AppendLine();
        sb.Append($"    START WITH {model.StartValue}");
        sb.AppendLine();
        sb.Append($"    INCREMENT BY {model.Increment}");
        sb.AppendLine();

        if (model.MinValue.HasValue)
            sb.AppendLine($"    MINVALUE {model.MinValue.Value}");
        else
            sb.AppendLine("    NO MINVALUE");

        if (model.MaxValue.HasValue)
            sb.AppendLine($"    MAXVALUE {model.MaxValue.Value}");
        else
            sb.AppendLine("    NO MAXVALUE");


        sb.AppendLine("END");
        sb.AppendLine("GO");

        return sb.ToString();
    }

    public string GenerateDropSequenceScript(AdvancedSequenceManageViewModel model)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"USE [{model.DatabaseName}]");
        sb.AppendLine("GO");

        sb.AppendLine($"IF EXISTS (SELECT * FROM sys.sequences WHERE name = '{model.SequenceName}')");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    DROP SEQUENCE [{model.SequenceName}];");
        sb.AppendLine("END");
        sb.AppendLine("GO");

        return sb.ToString();
    }

}
