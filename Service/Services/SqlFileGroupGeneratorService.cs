using Core;
using Service.Interfaces;
using System.Text;

namespace Service.Services;

public class SqlFileGroupGeneratorService : ISqlFileGroupGeneratorService
{
    public string GenerateAddFileGroupScript(FileGroupViewModel model)
    {
        var sb = new StringBuilder();

        // بررسی وجود فایل‌گروپ
        sb.AppendLine($"USE [{model.DatabaseName}]");
        sb.AppendLine("GO");
        sb.AppendLine($"IF NOT EXISTS (SELECT * FROM sys.filegroups WHERE name = '{model.FileGroupName}')");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    ALTER DATABASE [{model.DatabaseName}] ADD FILEGROUP [{model.FileGroupName}];");
        sb.AppendLine("END");
        sb.AppendLine("GO");

        // افزودن فایل فیزیکی
        if (!string.IsNullOrWhiteSpace(model.FileName) && !string.IsNullOrWhiteSpace(model.FilePath))
        {
            sb.AppendLine($"ALTER DATABASE [{model.DatabaseName}] ADD FILE (");
            sb.AppendLine($"    NAME = N'{model.FileName}',");
            sb.AppendLine($"    FILENAME = N'{model.FilePath}\\{model.FileName}.ndf',");
            sb.AppendLine($"    SIZE = {model.SizeMB ?? 10}MB,");
            sb.AppendLine($"    MAXSIZE = {(model.MaxSizeMB.HasValue && model.MaxSizeMB.Value > 0 ? model.MaxSizeMB.Value.ToString() + "MB" : "UNLIMITED")},");
            sb.AppendLine($"    FILEGROWTH = {model.FileGrowthPercent ?? 10}%");
            sb.AppendLine($") TO FILEGROUP [{model.FileGroupName}];");
            sb.AppendLine("GO");
        }

        return sb.ToString();
    }

    public string GenerateDropFileGroupScript(FileGroupViewModel model)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"USE [{model.DatabaseName}]");
        sb.AppendLine("GO");
        sb.AppendLine($"IF EXISTS (SELECT * FROM sys.filegroups WHERE name = '{model.FileGroupName}')");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    ALTER DATABASE [{model.DatabaseName}] REMOVE FILEGROUP [{model.FileGroupName}];");
        sb.AppendLine("END");
        sb.AppendLine("GO");

        return sb.ToString();
    }

    public string GeneratePartitionFunctionAndScheme(PartitionCreateViewModel model)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"USE [{model.DatabaseName}]");
        sb.AppendLine("GO");

        // بررسی وجود Partition Function
        sb.AppendLine($"IF NOT EXISTS (SELECT * FROM sys.partition_functions WHERE name = '{model.PartitionFunctionName}')");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    CREATE PARTITION FUNCTION {model.PartitionFunctionName} (int)"); // فرض int، قابل توسعه
        sb.AppendLine($"    AS RANGE {model.RangeBoundary} FOR VALUES ({model.RangeValues});");
        sb.AppendLine("END");
        sb.AppendLine("GO");

        // بررسی وجود Partition Scheme
        sb.AppendLine($"IF NOT EXISTS (SELECT * FROM sys.partition_schemes WHERE name = '{model.PartitionSchemeName}')");
        sb.AppendLine("BEGIN");

        // لیست فایل‌گروپها
        var fgList = string.Join(", ", model.FileGroups.Select(f => $"[{f.FileGroupName}]"));
        sb.AppendLine($"    CREATE PARTITION SCHEME {model.PartitionSchemeName}");
        sb.AppendLine($"    AS PARTITION {model.PartitionFunctionName} TO ({fgList});");
        sb.AppendLine("END");
        sb.AppendLine("GO");

        return sb.ToString();
    }

}
