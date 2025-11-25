using Core;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services;
public class SqlGeneratorService : ISqlGeneratorService
{

    public string GenerateCreateTempDbScript(TempDbSettings settings)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"-- Modifying TempDB with {settings.NumberOfFiles} files of {settings.FileSizeMb}MB each");

        // تغییر فایل اصلی
        sb.AppendLine($"ALTER DATABASE tempdb MODIFY FILE (NAME = tempdev, SIZE = {settings.FileSizeMb}MB, FILEGROWTH = {settings.AutoGrowthMb}MB);");
        sb.AppendLine($"ALTER DATABASE tempdb MODIFY FILE (NAME = templog, SIZE = {settings.FileSizeMb}MB, FILEGROWTH = {settings.AutoGrowthMb}MB);");

        // اگر فایل‌های اضافی نیاز دارید
        for (int i = 0; i < settings.NumberOfFiles; i++)
        {
            sb.AppendLine($"IF NOT EXISTS (SELECT * FROM sys.master_files WHERE name = 'tempdb_file{i + 1}')");
            sb.AppendLine($"    ALTER DATABASE tempdb ADD FILE (NAME = tempdb_file{i + 1}, FILENAME = '{settings.PathFile}\\tempdb_file{i + 1}.ndf', SIZE = {settings.FileSizeMb}MB, MAXSIZE = UNLIMITED, FILEGROWTH = {settings.AutoGrowthMb}MB);");
        }

        return sb.ToString();
    }


}
