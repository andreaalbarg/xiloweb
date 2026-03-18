using System.ComponentModel.DataAnnotations;

namespace XiloWeb.Api.DTOs;

public class ContactFormRequest
{
    [Required] public string Inquiry { get; set; } = "";
    [Required] public string Name    { get; set; } = "";
    [Required, EmailAddress] public string Email { get; set; } = "";
    public string Company { get; set; } = "";
    [Required] public string Message { get; set; } = "";
}
