namespace Core;

public class LoginCreateViewModel
{
    public string LoginName { get; set; }
    public string Password { get; set; }
    public bool IsSqlLogin { get; set; }
    public string? GeneratedScript { get; set; }
}
