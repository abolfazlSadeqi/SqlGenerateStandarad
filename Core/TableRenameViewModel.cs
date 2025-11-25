namespace Core;

using System.ComponentModel.DataAnnotations;

public class TableRenameViewModel
{
    [Required(ErrorMessage = "Schema is required.")]
    [StringLength(128)]
    public string Schema { get; set; } = "dbo";

    [Required(ErrorMessage = "Old table name is required.")]
    [StringLength(128)]
    public string OldTableName { get; set; }

    [Required(ErrorMessage = "New table name is required.")]
    [StringLength(128)]
    public string NewTableName { get; set; }

    public string? GeneratedScript { get; set; }
}
