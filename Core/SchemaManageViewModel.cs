namespace Core;

using System.ComponentModel.DataAnnotations;

public class SchemaManageViewModel
{
    [Required(ErrorMessage = "DatabaseName is required.")]
    [StringLength(128)]
    [Display(Name = "Database Name")]
    public string DatabaseName { get; set; }

    [Required(ErrorMessage = "SchemaName is required.")]
    [StringLength(128)]
    [Display(Name = "Schema Name")]
    public string SchemaName { get; set; }

    [StringLength(128)]
    [Display(Name = "New Schema Name (for rename)")]
    public string NewSchemaName { get; set; }

    public string? GeneratedScript { get; set; }
}
