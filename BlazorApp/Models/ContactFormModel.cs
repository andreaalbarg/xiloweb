using System.ComponentModel.DataAnnotations;

namespace XiloWeb.Models;

public class ContactFormModel
{
    [Required(ErrorMessage = "Please select an inquiry type")]
    public string Inquiry { get; set; } = "";

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set; } = "";

    public string Company { get; set; } = "";

    [Required(ErrorMessage = "Message is required")]
    public string Message { get; set; } = "";
}
