using TestTechniqueWebAtrio.DTO;

namespace TestTechniqueWebAtrio.Services;

/// <summary>
/// Interface for managing Job entities' business logic.
/// </summary>
public interface IJobService
{
    /// <summary>
    /// Adds a new job.
    /// </summary>
    /// <param name="jobDto">The job creation data transfer object.</param>
    Task AddJobAsync(JobCreateDTO jobDto);

    /// <summary>
    /// Retrieves jobs for a person within a specified date range.
    /// </summary>
    /// <param name="personId">The unique identifier of the person.</param>
    /// <param name="startDate">The start date of the date range.</param>
    /// <param name="endDate">The end date of the date range.</param>
    /// <returns>A list of jobs matching the criteria.</returns>
    Task<IEnumerable<JobDTO>> GetJobsByPersonIdAndDateRangeAsync(Guid personId, DateTime startDate, DateTime endDate);
}