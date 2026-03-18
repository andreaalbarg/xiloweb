using System.Net.Http.Json;
using XiloWeb.Models;

namespace XiloWeb.Services;

public class EmailService(HttpClient http)
{
    public async Task<EmailResult> SendContactFormAsync(ContactFormModel model)
    {
        try
        {
            var payload = new
            {
                inquiry = model.Inquiry,
                name    = model.Name,
                email   = model.Email,
                company = model.Company,
                message = model.Message
            };

            var response = await http.PostAsJsonAsync("api/contact", payload);

            if (response.IsSuccessStatusCode)
                return EmailResult.Ok();

            var body = await response.Content.ReadAsStringAsync();
            return EmailResult.Fail($"Error {(int)response.StatusCode}: {body}");
        }
        catch (Exception ex)
        {
            return EmailResult.Fail(ex.Message);
        }
    }
}

public sealed class EmailResult
{
    public bool IsSuccess { get; }
    public string? ErrorMessage { get; }
    private EmailResult(bool success, string? error = null) { IsSuccess = success; ErrorMessage = error; }
    public static EmailResult Ok()              => new(true);
    public static EmailResult Fail(string error) => new(false, error);
}
