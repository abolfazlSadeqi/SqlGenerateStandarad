namespace Core;

using System.ComponentModel.DataAnnotations;

public class TableCreateColumn
{
    [Required(ErrorMessage = "Column Name is required.")]
    [StringLength(128)]
    public string ColumnName { get; set; }

    [Required(ErrorMessage = "DataType  is required.")]
    public string DataType { get; set; }

    public bool IsNullable { get; set; } = true;

    public string? DefaultValue { get; set; }

    public bool IsPrimaryKey { get; set; } = false;
}
