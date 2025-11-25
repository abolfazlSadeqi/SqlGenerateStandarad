namespace Core;

using System.ComponentModel.DataAnnotations;

public class ColumnDropViewModel
{
    [Required(ErrorMessage = "Schema is required.")]
    [StringLength(128, ErrorMessage = "Schema length cannot exceed 128 characters.")]
    public string Schema { get; set; } = "dbo";

    [Required(ErrorMessage = "Table name is required.")]
    [StringLength(128, ErrorMessage = "Table name length cannot exceed 128 characters.")]
    public string TableName { get; set; }

    [Required(ErrorMessage = "Column name is required.")]
    [StringLength(128, ErrorMessage = "Column name length cannot exceed 128 characters.")]
    public string ColumnName { get; set; }

    public string? GeneratedScript { get; set; }
}
