namespace PersonExample.Auth;

public class JwtSettings
{
    public string Secret { get; set; } = string.Empty;
    public int ExpiryMinutes { get; set; }
}
