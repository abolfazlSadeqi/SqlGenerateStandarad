namespace Core;

using System.ComponentModel.DataAnnotations;

public class DropViewViewModel
{
    [Required(ErrorMessage = "DatabaseName is required.")]
    [StringLength(128)]
    [Display(Name = "Database Name")]
    public string DatabaseName { get; set; }

    [Required(ErrorMessage = "Schema is required.")]
    [StringLength(128)]
    public string Schema { get; set; } = "dbo";


    [Required(ErrorMessage = "ViewName is required.")]
    [StringLength(128)]
    [Display(Name = "View Name")]
    public string ViewName { get; set; }


    public string? GeneratedScript { get; set; }
}
