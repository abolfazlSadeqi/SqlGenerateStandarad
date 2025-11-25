namespace Core;

using System.ComponentModel.DataAnnotations;

public class FileGroupViewModel
{
    [Required(ErrorMessage = "Database name is required.")]
    [StringLength(128)]
    public string DatabaseName { get; set; }

    [Required(ErrorMessage = "Filegroup name is required.")]
    [StringLength(128)]
    public string FileGroupName { get; set; }

    // Optional: path for file creation
    [Display(Name = "File Name (Physical)")]
    public string FileName { get; set; }

    [Display(Name = "File Path (Physical)")]
    public string FilePath { get; set; }

    [Display(Name = "Size (MB)")]
    public int? SizeMB { get; set; }

    [Display(Name = "Max Size (MB, 0 = UNLIMITED)")]
    public int? MaxSizeMB { get; set; }

    [Display(Name = "File Growth (%)")]
    public int? FileGrowthPercent { get; set; }

    public string? GeneratedScript { get; set; }
}
