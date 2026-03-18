namespace XiloWeb.Api.DTOs;

public class LoginResponse
{
    public string Token  { get; set; } = "";
    public DateTime Expiry { get; set; }
}
