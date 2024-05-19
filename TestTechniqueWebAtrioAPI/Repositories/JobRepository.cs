using Microsoft.EntityFrameworkCore;
using TestTechniqueWebAtrio.Data;
using TestTechniqueWebAtrio.Models;

namespace TestTechniqueWebAtrio.Repositories;

public class JobRepository : IJobRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<JobRepository> _logger;

    public JobRepository(ApplicationDbContext context, ILogger<JobRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task AddJobAsync(Job job)
    {
        _logger.LogInformation("Adding a new job to the database.");
        await _context.Jobs.AddAsync(job);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Job>> GetJobsByPersonIdAndDateRangeAsync(Guid personId, DateTime startDate, DateTime endDate)
    {
        _logger.LogInformation("Retrieving jobs for person ID {PersonId} between {StartDate} and {EndDate}", personId, startDate, endDate);
        return await _context.Jobs
            .Where(j => j.PersonId == personId && j.StartDate >= startDate && (!j.EndDate.HasValue || j.EndDate <= endDate))
            .ToListAsync();
    }
}