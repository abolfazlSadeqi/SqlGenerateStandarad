namespace Core;

using System.ComponentModel.DataAnnotations;

public class CreateSequenceViewModel
{
    [Required]
    public string DatabaseName { get; set; }

    [Required]
    public string Schema { get; set; }

    [Required]
    public string SequenceName { get; set; }

    [Required]
    public string DataType { get; set; } // BIGINT, INT, DECIMAL, ...

    public long StartWith { get; set; }
    public long IncrementBy { get; set; }
    public long MinValue { get; set; }
    public long MaxValue { get; set; }
    public bool Cycle { get; set; }

    public string? GeneratedScript { get; set; }
}
