namespace Core;

using System.ComponentModel.DataAnnotations;

public class FunctionManageViewModel
{
    [Required(ErrorMessage = "DatabaseName is required.")]
    [StringLength(128)]
    [Display(Name = "Database Name")]
    public string DatabaseName { get; set; }


    [Required(ErrorMessage = "Schema is required.")]
    [StringLength(128)]
    [Display(Name = "Schema")]
    public string Schema { get; set; }


    [Required(ErrorMessage = "FunctionName is required.")]
    [StringLength(128)]
    [Display(Name = "Function Name")]
    public string FunctionName { get; set; }

    [Required(ErrorMessage = "FunctionType is required.")]
    [Display(Name = "Function Type")]
    public string FunctionType { get; set; } // "Scalar", "InlineTable", "MultiStatementTable"

    [Required(ErrorMessage = "FunctionDefinition is required.")]
    [Display(Name = "Function Definition (T-SQL code)")]
    public string FunctionDefinition { get; set; }

    public string? GeneratedScript { get; set; }
}
