namespace Core;

using System.ComponentModel.DataAnnotations;

public class ColumnAlterViewModel
{
    [Required(ErrorMessage = "Schema is required")]
    [Display(Name = "Schema")]
    public string Schema { get; set; } = "dbo";

    [Required(ErrorMessage = "Table name is required")]
    [Display(Name = "Table")]
    public string TableName { get; set; }

    [Required(ErrorMessage = "Column name is required")]
    [Display(Name = "Column")]
    public string ColumnName { get; set; }

    [Required(ErrorMessage = "Data type is required")]
    [Display(Name = "Data Type")]
    public string DataType { get; set; } // e.g. "int", "nvarchar(100)", "datetime2"

    [Display(Name = "Nullable")]
    public bool IsNullable { get; set; } = true;

    [Display(Name = "Default Value")]
    public string? DefaultValue { get; set; } // optional; user provides SQL literal, e.g. 0, 'N/A', GETDATE()

    // result script (read-only for view)
    public string? GeneratedScript { get; set; }
}
