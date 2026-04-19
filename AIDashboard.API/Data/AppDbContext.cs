using Microsoft.EntityFrameworkCore;
using AIDashboard.API.Models;

namespace AIDashboard.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}