using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.JSInterop;

namespace XiloWeb.Services;

public class AuthService(HttpClient http, IJSRuntime js)
{
    private const string TokenKey = "xilo_admin_token";

    public async Task<bool> LoginAsync(string username, string password)
    {
        try
        {
            var response = await http.PostAsJsonAsync("api/auth/login", new { username, password });
            if (!response.IsSuccessStatusCode) return false;

            var result = await response.Content.ReadFromJsonAsync<LoginResult>();
            if (result?.Token is null) return false;

            await js.InvokeVoidAsync("localStorage.setItem", TokenKey, result.Token);
            return true;
        }
        catch { return false; }
    }

    public async Task LogoutAsync() =>
        await js.InvokeVoidAsync("localStorage.removeItem", TokenKey);

    public async Task<string?> GetTokenAsync() =>
        await js.InvokeAsync<string?>("localStorage.getItem", TokenKey);

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await GetTokenAsync();
        if (string.IsNullOrEmpty(token)) return false;
        try
        {
            // Decode payload and check expiry
            var parts   = token.Split('.');
            var payload = parts[1];
            // Pad base64
            payload += new string('=', (4 - payload.Length % 4) % 4);
            var json    = JsonDocument.Parse(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(payload)));
            var exp     = json.RootElement.GetProperty("exp").GetInt64();
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds() < exp;
        }
        catch { return false; }
    }

    private sealed record LoginResult(string Token, DateTime Expiry);
}
