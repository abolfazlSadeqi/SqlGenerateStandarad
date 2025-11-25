namespace Core;

using System.ComponentModel.DataAnnotations;

public class CreateFunctionViewModel
{
    [Required]
    public string DatabaseName { get; set; }

    [Required]
    public string Schema { get; set; }

    [Required]
    public string FunctionName { get; set; }

    [Required]
    public string FunctionType { get; set; } // Scalar / InlineTable / MultiStatementTable

    [Required]
    public string FunctionDefinition { get; set; }

    public string? GeneratedScript { get; set; }
}
