namespace Core;

using System.ComponentModel.DataAnnotations;

public class IndexManageViewModel
{
    [Required(ErrorMessage = "DatabaseName is required.")]
    [StringLength(128)]
    [Display(Name = "Database Name")]
    public string DatabaseName { get; set; }

    [Required(ErrorMessage = "TableName is required.")]
    [StringLength(128)]
    [Display(Name = "Table Name")]
    public string TableName { get; set; }

    [Required(ErrorMessage = "IndexName is required.")]
    [StringLength(128)]
    [Display(Name = "Index Name")]
    public string IndexName { get; set; }

    [Required(ErrorMessage = "IndexType is required.")]
    [Display(Name = "Index Type")]
    public IndexType IndexType { get; set; }


    [Display(Name = "Unique")]
    public bool IsUnique { get; set; } = false;

    [Display(Name = "FileGroup")]
    public string? FileGroup { get; set; }

    [Display(Name = "Include Columns (For Covering Index)")]
    public string? IncludeColumns { get; set; }

    [Display(Name = "Filter Expression")]
    public string? FilterExpression { get; set; }



    // For Index Columns
    public List<IndexColumn> Columns { get; set; } = new List<IndexColumn>();

    public string? GeneratedScript { get; set; }
}
