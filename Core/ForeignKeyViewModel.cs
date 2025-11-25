namespace Core;

public class ForeignKeyViewModel
{
    public string TableName { get; set; }
    public string ForeignKeyName { get; set; }
    public string? GeneratedScript { get; set; }
}
