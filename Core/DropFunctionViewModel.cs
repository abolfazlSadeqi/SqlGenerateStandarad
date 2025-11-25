namespace Core;

using System.ComponentModel.DataAnnotations;

public class DropFunctionViewModel
{
    [Required]
    public string DatabaseName { get; set; }

    [Required]
    public string Schema { get; set; }

    [Required]
    public string FunctionName { get; set; }

    public string? GeneratedScript { get; set; }
}
