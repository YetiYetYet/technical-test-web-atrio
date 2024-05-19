using Microsoft.AspNetCore.Mvc;
using TestTechniqueWebAtrio.DTO;
using TestTechniqueWebAtrio.Models;
using TestTechniqueWebAtrio.Services;

namespace TestTechniqueWebAtrio.Controllers;


[ApiController]
[Route("[controller]")]
public class JobsController : ControllerBase
{
    private readonly IJobService _jobService;
    private readonly ILogger<JobsController> _logger;

    public JobsController(IJobService jobService, ILogger<JobsController> logger)
    {
        _jobService = jobService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> AddJob([FromBody] JobCreateDTO jobDto)
    {
        _logger.LogInformation("Adding a new job.");
        await _jobService.AddJobAsync(jobDto);
        return Ok(jobDto);
    }

    [HttpGet("{personId:guid}")]
    public async Task<IActionResult> GetJobsByPersonAndDateRange(Guid personId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        _logger.LogInformation("Getting jobs for person ID {PersonId} between {StartDate} and {EndDate}", personId, startDate, endDate);
        var jobs = await _jobService.GetJobsByPersonIdAndDateRangeAsync(personId, startDate, endDate);
        return Ok(jobs);
    }
}