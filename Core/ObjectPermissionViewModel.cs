namespace Core;

public class ObjectPermissionViewModel
{
    public string DatabaseName { get; set; }
    public string UserName { get; set; }
    public string Permission { get; set; }
    public string SchemaName { get; set; }
    public string ObjectName { get; set; }
    public string? GeneratedScript { get; set; }
}
