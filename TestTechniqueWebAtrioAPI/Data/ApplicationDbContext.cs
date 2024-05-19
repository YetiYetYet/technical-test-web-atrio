using Microsoft.EntityFrameworkCore;
using TestTechniqueWebAtrio.Models;

namespace TestTechniqueWebAtrio.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Job> Jobs { get; set; }
}