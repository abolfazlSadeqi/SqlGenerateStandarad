namespace Core;

using System.ComponentModel.DataAnnotations;

public class AlterSequenceViewModel
{
    [Required]
    public string DatabaseName { get; set; }

    [Required]
    public string Schema { get; set; }

    [Required]
    public string SequenceName { get; set; }

    public long? RestartWith { get; set; }
    public long? MinValue { get; set; }
    public long? MaxValue { get; set; }
    public long? IncrementBy { get; set; }
    public bool? Cycle { get; set; }

    public string? GeneratedScript { get; set; }
}
