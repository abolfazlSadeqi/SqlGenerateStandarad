namespace Core;

public class TempDbSettings
{
    public int CpuCores { get; set; } // تعداد هسته‌ها
    public double RamSizeGb { get; set; } // اندازه RAM (برحسب گیگابایت)

    public int NumberOfFiles
    {
        get
        {
            // حداقل 4 فایل یا تعداد هسته‌ها
            return Math.Max(4, CpuCores);
        }
    }

    public int FileSizeMb
    {
        get
        {
            // فرض می‌کنیم اندازه فایل‌ها برای TempDB بین 4 تا 8 گیگابایت باشد
            // با توجه به RAM می‌توان فایل‌ها را تنظیم کرد. اینجا از 5 گیگابایت استفاده می‌کنیم.
            return 5120; // 5GB
        }
    }

    public int AutoGrowthMb
    {
        get
        {
            // مقدار Auto-growth برای هر فایل TempDB، مثلا 500MB
            return 500;
        }
    }
    public string PathFile { get; set; }

    public string? GeneratedScript { get; set; }
}
