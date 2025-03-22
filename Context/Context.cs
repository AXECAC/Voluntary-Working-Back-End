using Microsoft.EntityFrameworkCore;
using DataBase;
namespace Context;

// Класс Context
public class VoluntaryWorkingDbContext : DbContext
{
    public VoluntaryWorkingDbContext(DbContextOptions<VoluntaryWorkingDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Request> Requests { get; set; }
}
