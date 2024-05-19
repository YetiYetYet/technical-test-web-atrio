using TestTechniqueWebAtrio.Models;

namespace TestTechniqueWebAtrio.Repositories;

/// <summary>
/// Interface for managing Job entities.
/// </summary>
public interface IJobRepository
{
    /// <summary>
    /// Adds a new job to the database.
    /// </summary>
    /// <param name="job">The job to add.</param>
    Task AddJobAsync(Job job);

    /// <summary>
    /// Retrieves jobs for a person within a specified date range.
    /// </summary>
    /// <param name="personId">The unique identifier of the person.</param>
    /// <param name="startDate">The start date of the date range.</param>
    /// <param name="endDate">The end date of the date range.</param>
    /// <returns>A list of jobs matching the criteria.</returns>
    Task<IEnumerable<Job>> GetJobsByPersonIdAndDateRangeAsync(Guid personId, DateTime startDate, DateTime endDate);
}