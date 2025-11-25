namespace Core;

using System.ComponentModel.DataAnnotations;

public class DropProcedureViewModel
{
    [Required(ErrorMessage = "DatabaseName is required.")]
    [StringLength(128)]
    [Display(Name = "Database Name")]
    public string DatabaseName { get; set; }

    [Required(ErrorMessage = "Schema is required.")]
    [StringLength(128)]
    public string Schema { get; set; } = "dbo";

    [Required(ErrorMessage = "ProcedureName is required.")]
    [StringLength(128)]
    [Display(Name = "Procedure Name")]
    public string ProcedureName { get; set; }



    public string? GeneratedScript { get; set; }
}
