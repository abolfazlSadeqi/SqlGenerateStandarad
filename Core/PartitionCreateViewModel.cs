namespace Core;

using System.ComponentModel.DataAnnotations;

public class PartitionCreateViewModel
{
    [Required(ErrorMessage = "DatabaseName is required.")]
    [StringLength(128)]
    public string DatabaseName { get; set; }

    [Required(ErrorMessage = "PartitionFunctionName is required.")]
    [StringLength(128)]
    public string PartitionFunctionName { get; set; }


    [Required(ErrorMessage = "RangeValues is required.")]
    public string RangeValues { get; set; } // e.g. "100,200,300"

    [Required(ErrorMessage = "RangeBoundary is required.")]
    [RegularExpression("LEFT|RIGHT", ErrorMessage = "Range boundary must be LEFT or RIGHT.")]
    public string RangeBoundary { get; set; } = "LEFT";

    [Required(ErrorMessage = "PartitionSchemeName is required.")]
     [StringLength(128)]
    public string PartitionSchemeName { get; set; }

    [Required]
    public List<PartitionFileGroup> FileGroups { get; set; } = new List<PartitionFileGroup>();

    public string? GeneratedScript { get; set; }
}
