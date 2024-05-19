using TestTechniqueWebAtrio.DTO;
using TestTechniqueWebAtrio.Mappers;
using TestTechniqueWebAtrio.Models;
using TestTechniqueWebAtrio.Repositories;

namespace TestTechniqueWebAtrio.Services;

public class JobService : IJobService
{
    private readonly IJobRepository _jobRepository;
    private readonly ILogger<JobService> _logger;

    public JobService(IJobRepository jobRepository, ILogger<JobService> logger)
    {
        _jobRepository = jobRepository;
        _logger = logger;
    }

    public async Task AddJobAsync(JobCreateDTO jobDto)
    {
        var job = MappingHelper.MapJobCreateDtoToJob(jobDto);

        _logger.LogInformation("Adding a new job with ID {JobId}.", job.Id);
        await _jobRepository.AddJobAsync(job);
    }

    public async Task<IEnumerable<JobDTO>> GetJobsByPersonIdAndDateRangeAsync(Guid personId, DateTime startDate, DateTime endDate)
    {
        _logger.LogInformation("Retrieving jobs for person ID {PersonId} between {StartDate} and {EndDate}.", personId, startDate, endDate);
        var jobs = await _jobRepository.GetJobsByPersonIdAndDateRangeAsync(personId, startDate, endDate);
        return MappingHelper.MapJobsToJobDtos(jobs);
    }
}