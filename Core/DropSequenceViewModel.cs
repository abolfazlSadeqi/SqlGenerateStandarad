namespace Core;

using System.ComponentModel.DataAnnotations;

public class DropSequenceViewModel
{
    [Required]
    public string DatabaseName { get; set; }

    [Required]
    public string Schema { get; set; }

    [Required]
    public string SequenceName { get; set; }

    public string? GeneratedScript { get; set; }
}
