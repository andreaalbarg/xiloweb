using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XiloWeb.Api.Data;
using XiloWeb.Api.DTOs;
using XiloWeb.Api.Models;
using XiloWeb.Api.Services;

namespace XiloWeb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController(AppDbContext db, EmailNotificationService emailSvc) : ControllerBase
{
    /// <summary>Submit a contact form (public)</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Submit([FromBody] ContactFormRequest req)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var submission = new ContactSubmission
        {
            Name      = req.Name.Trim(),
            Email     = req.Email.Trim(),
            Company   = req.Company.Trim(),
            Inquiry   = req.Inquiry,
            Message   = req.Message.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        db.ContactSubmissions.Add(submission);
        await db.SaveChangesAsync();

        submission.EmailSent = await emailSvc.SendContactNotificationAsync(submission);
        await db.SaveChangesAsync();

        return Ok(new { message = "Submission received", id = submission.Id });
    }

    /// <summary>Get all contact submissions (admin only)</summary>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(List<ContactSubmission>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var submissions = await db.ContactSubmissions
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
        return Ok(submissions);
    }

    /// <summary>Get a single submission by ID (admin only)</summary>
    [HttpGet("{id:int}")]
    [Authorize]
    [ProducesResponseType(typeof(ContactSubmission), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var submission = await db.ContactSubmissions.FindAsync(id);
        return submission is null ? NotFound() : Ok(submission);
    }
}
