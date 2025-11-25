namespace Core;

using System.ComponentModel.DataAnnotations;

public class IndexColumn
{
    [Required(ErrorMessage = "ColumnName is required.")]
    [StringLength(128)]
    public string ColumnName { get; set; }

    [Display(Name = "Sort Order")]
    public SortOrder SortOrder { get; set; } = SortOrder.Ascending;
}
