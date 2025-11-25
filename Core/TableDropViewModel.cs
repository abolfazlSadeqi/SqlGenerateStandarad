namespace Core;

using System.ComponentModel.DataAnnotations;

public class TableDropViewModel
{
    [Required(ErrorMessage = "Schema is required.")]
    [StringLength(128)]
    public string Schema { get; set; } = "dbo";

    [Required(ErrorMessage = "Table name is required.")]
    [StringLength(128)]
    public string TableName { get; set; }

    public string? GeneratedScript { get; set; }
}
