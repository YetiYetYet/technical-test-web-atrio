using Microsoft.AspNetCore.Mvc;
using TestTechniqueWebAtrio.DTO;
using TestTechniqueWebAtrio.Models;
using TestTechniqueWebAtrio.Services;

namespace TestTechniqueWebAtrio.Controllers;


/// <summary>
/// API for managing jobs.
/// </summary>
[ApiController]
[Route("[controller]")]
public class JobsController : ControllerBase
{
    private readonly IJobService _jobService;
    private readonly ILogger<JobsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="JobsController"/> class.
    /// </summary>
    /// <param name="jobService"></param>
    /// <param name="logger"></param>
    public JobsController(IJobService jobService, ILogger<JobsController> logger)
    {
        _jobService = jobService;
        _logger = logger;
    }

    /// <summary>
    /// Adds a new job.
    /// </summary>
    /// <param name="jobDto">The job data.</param>
    /// <returns>The created job.</returns>
    [HttpPost]
    public async Task<IActionResult> AddJob([FromBody] JobCreateDTO jobDto)
    {
        _logger.LogInformation("Adding a new job.");
        await _jobService.AddJobAsync(jobDto);
        return Ok(jobDto);
    }

    /// <summary>
    /// Gets jobs by person ID and date range.
    /// </summary>
    /// <param name="personId">The ID of the person.</param>
    /// <param name="startDate">The start date of the range.</param>
    /// <param name="endDate">The end date of the range.</param>
    /// <returns>A list of jobs matching the criteria.</returns>
    [HttpGet("{personId}")]
    public async Task<IActionResult> GetJobsByPersonAndDateRange(Guid personId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        _logger.LogInformation("Getting jobs for person ID {PersonId} between {StartDate} and {EndDate}", personId, startDate, endDate);
        var jobs = await _jobService.GetJobsByPersonIdAndDateRangeAsync(personId, startDate, endDate);
        return Ok(jobs);
    }
}