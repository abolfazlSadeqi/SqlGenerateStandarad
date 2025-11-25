namespace Core;

using System.ComponentModel.DataAnnotations;

public class TableStorageUpdateViewModel
{
    [Required(ErrorMessage = "DatabaseName is required.")]
    [StringLength(128)]
    public string DatabaseName { get; set; }

    [Required(ErrorMessage = "Schema is required.")]
    [StringLength(128)]
    public string Schema { get; set; } = "dbo";

    [Required(ErrorMessage = "TableName is required.")]
    [StringLength(128)]
    public string TableName { get; set; }

    // فایل‌گروپ جدید برای جدول
    [Display(Name = "New Filegroup for Table")]
    public string NewFileGroup { get; set; }

    // Partition Function / Scheme
    [Display(Name = "Partition Function")]
    public string PartitionFunctionName { get; set; }

    [Display(Name = "Partition Scheme")]
    public string PartitionSchemeName { get; set; }

    [Display(Name = "Partition Column")]
    public string PartitionColumn { get; set; } // ستون پارتیشن

    [Display(Name = "Range Values (comma separated)")]
    public string RangeValues { get; set; } // e.g. "100,200,300"

    [Display(Name = "Range Boundary (LEFT/RIGHT)")]
    [RegularExpression("LEFT|RIGHT", ErrorMessage = "Range boundary must be LEFT or RIGHT.")]
    public string RangeBoundary { get; set; } = "LEFT";

    public string? GeneratedScript { get; set; }
}
