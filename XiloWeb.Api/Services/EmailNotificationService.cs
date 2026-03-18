using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using XiloWeb.Api.Models;

namespace XiloWeb.Api.Services;

public class EmailNotificationService(IConfiguration config, ILogger<EmailNotificationService> logger)
{
    public async Task<bool> SendContactNotificationAsync(ContactSubmission submission)
    {
        try
        {
            var smtpHost = config["Email:SmtpHost"]!;
            var smtpPort = int.Parse(config["Email:SmtpPort"] ?? "587");
            var username = config["Email:Username"]!;
            var password = config["Email:Password"]!;
            var fromName = config["Email:FromName"] ?? "XILO Website";
            var toEmail  = config["Email:ToEmail"]!;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromName, username));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = $"[XILO] New {submission.Inquiry} inquiry from {submission.Name}";

            message.Body = new TextPart("html")
            {
                Text = $"""
                    <h2>New Contact Form Submission</h2>
                    <table style="border-collapse:collapse;width:100%">
                      <tr><td style="padding:8px;font-weight:bold">Name</td>    <td style="padding:8px">{submission.Name}</td></tr>
                      <tr><td style="padding:8px;font-weight:bold">Email</td>   <td style="padding:8px"><a href="mailto:{submission.Email}">{submission.Email}</a></td></tr>
                      <tr><td style="padding:8px;font-weight:bold">Company</td> <td style="padding:8px">{submission.Company}</td></tr>
                      <tr><td style="padding:8px;font-weight:bold">Inquiry</td> <td style="padding:8px">{submission.Inquiry}</td></tr>
                      <tr><td style="padding:8px;font-weight:bold">Message</td> <td style="padding:8px">{submission.Message}</td></tr>
                      <tr><td style="padding:8px;font-weight:bold">Date</td>    <td style="padding:8px">{submission.CreatedAt:yyyy-MM-dd HH:mm} UTC</td></tr>
                    </table>
                    """
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(username, password);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send email notification for submission {Id}", submission.Id);
            return false;
        }
    }
}
