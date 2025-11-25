namespace Core;

using System.ComponentModel.DataAnnotations;

public class TableCreateViewModel
{
    [Required(ErrorMessage = "Schema  is required.")]
    [StringLength(128)]
    public string Schema { get; set; } = "dbo";

    [Required(ErrorMessage = "TableName  is required.")]
    [StringLength(128)]
    public string TableName { get; set; }

    public List<TableCreateColumn> Columns { get; set; } = new List<TableCreateColumn>();

    public string? FileGroup { get; set; } // Optional
    public string? PartitionScheme { get; set; } // Optional

    public string? GeneratedScript { get; set; }
}
