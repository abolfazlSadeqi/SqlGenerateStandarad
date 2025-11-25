namespace Core;

using System.ComponentModel.DataAnnotations;

public class ColumnRenameViewModel
{
    [Required(ErrorMessage = "Schema is required.")]
    [StringLength(128)]
    public string Schema { get; set; } = "dbo";

    [Required(ErrorMessage = "Table name is required.")]
    [StringLength(128)]
    public string TableName { get; set; }

    [Required(ErrorMessage = "Old column name is required.")]
    [StringLength(128)]
    public string OldColumnName { get; set; }

    [Required(ErrorMessage = "New column name is required.")]
    [StringLength(128)]
    public string NewColumnName { get; set; }

    public string? GeneratedScript { get; set; }
}
