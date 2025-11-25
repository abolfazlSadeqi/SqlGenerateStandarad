namespace Core;

using System.ComponentModel.DataAnnotations;

public class AdvancedSequenceManageViewModel
{
    [Required(ErrorMessage = "DatabaseName is required.")]
    [StringLength(128)]
    [Display(Name = "Database Name")]
    public string DatabaseName { get; set; }

    [Required(ErrorMessage = "SequenceName is required.")]
    [StringLength(128)]
    [Display(Name = "Sequence Name")]
    public string SequenceName { get; set; }

    [Display(Name = "Start Value")]
    [Range(1, long.MaxValue)]
    public long StartValue { get; set; } = 1;

    [Display(Name = "Increment")]
    public int Increment { get; set; } = 1;

    [Display(Name = "Min Value")]
    public long? MinValue { get; set; }

    [Display(Name = "Max Value")]
    public long? MaxValue { get; set; }

    [Display(Name = "Cycle")]
    public bool IsCycle { get; set; } = false;

    public string? GeneratedScript { get; set; }
}
