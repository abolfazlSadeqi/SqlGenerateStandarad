namespace Core;

using System.ComponentModel.DataAnnotations;

public class DatabaseCreateViewModel
{
    [Required]
    [Display(Name = "Database Name")]
    public string DatabaseName { get; set; }

    [Required]
    [Display(Name = "Recovery Model")]
    public string RecoveryModel { get; set; } // Full, Simple, Bulk-Logged

    [Required]
    [Display(Name = "Filegroup Name")]
    public string FileGroupName { get; set; }

    [Required]
    [Display(Name = "Data File Path")]
    public string DataFilePath { get; set; }

    [Required]
    [Display(Name = "Log File Path")]
    public string LogFilePath { get; set; }

    [Required]
    [Display(Name = "Data File Size")]
    public string DataFileSize { get; set; } // e.g., 5MB, 100MB

    [Required]
    [Display(Name = "Log File Size")]
    public string LogFileSize { get; set; } // e.g., 5MB, 100MB

    [Required]
    [Display(Name = "Max Data File Size")]
    public string MaxDataFileSize { get; set; } // Max Size (Optional)

    [Required]
    [Display(Name = "Max Log File Size")]
    public string MaxLogFileSize { get; set; } // Max Size (Optional)

    [Required]
    [Display(Name = "Auto Growth Increment")]
    public string AutoGrowthIncrement { get; set; } // Auto growth (e.g., 5MB)


    [Required]
    [Display(Name = "Delete Database If Exists")]
    public bool IsDeleteIfExists { get; set; }

    public string? GeneratedScript { get; set; }
}
