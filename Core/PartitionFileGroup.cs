namespace Core;

using System.ComponentModel.DataAnnotations;

public class PartitionFileGroup
{
    [Required(ErrorMessage = "FileGroupName is required.")]
    public string FileGroupName { get; set; }
}
