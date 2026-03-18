using Microsoft.EntityFrameworkCore;
using XiloWeb.Api.Models;

namespace XiloWeb.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ContactSubmission> ContactSubmissions => Set<ContactSubmission>();
}
