namespace Core;

using System.ComponentModel.DataAnnotations;

public class RenameSchemaViewModel
{
    [Required(ErrorMessage = "DatabaseName is required.")]
    [StringLength(128)]
    [Display(Name = "Database Name")]
    public string DatabaseName { get; set; }

    [Required(ErrorMessage = "SchemaName is required.")]
    [StringLength(128)]
    [Display(Name = "Schema Name")]
    public string SchemaName { get; set; }

    [Required(ErrorMessage = "NewSchemaName is required.")]
    [StringLength(128)]
    [Display(Name = "New Schema Name")]
    public string NewSchemaName { get; set; }

    public string? GeneratedScript { get; set; }
}
