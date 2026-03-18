using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace XiloWeb.Services;

public class ContactApiService(HttpClient http, AuthService auth)
{
    public async Task<List<ContactSubmissionDto>?> GetSubmissionsAsync()
    {
        var token = await auth.GetTokenAsync();
        http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return await http.GetFromJsonAsync<List<ContactSubmissionDto>>("api/contact");
    }

    public async Task<bool> SubmitContactFormAsync(object payload)
    {
        var response = await http.PostAsJsonAsync("api/contact", payload);
        return response.IsSuccessStatusCode;
    }
}

public sealed class ContactSubmissionDto
{
    public int      Id        { get; set; }
    public string   Name      { get; set; } = "";
    public string   Email     { get; set; } = "";
    public string   Company   { get; set; } = "";
    public string   Inquiry   { get; set; } = "";
    public string   Message   { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public bool     EmailSent { get; set; }
}
