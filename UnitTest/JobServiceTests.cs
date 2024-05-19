using Microsoft.Extensions.Logging;
using Moq;
using TestTechniqueWebAtrio.DTO;
using TestTechniqueWebAtrio.Models;
using TestTechniqueWebAtrio.Repositories;
using TestTechniqueWebAtrio.Services;

namespace UnitTest;

[TestFixture]
public class JobServiceTests
{
    private Mock<IJobRepository> _jobRepositoryMock;
    private Mock<ILogger<JobService>> _loggerMock;
    private JobService _jobService;

    [SetUp]
    public void SetUp()
    {
        _jobRepositoryMock = new Mock<IJobRepository>();
        _loggerMock = new Mock<ILogger<JobService>>();
        _jobService = new JobService(_jobRepositoryMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task AddJobAsync_AddsJobSuccessfully()
    {
        // Arrange
        var jobDto = new JobCreateDTO
        {
            CompanyName = "Tech Corp",
            Position = "Developer",
            StartDate = DateTime.UtcNow,
            PersonId = Guid.NewGuid()
        };

        Job addedJob = null;
        _jobRepositoryMock.Setup(repo => repo.AddJobAsync(It.IsAny<Job>())).Callback<Job>(j => addedJob = j);

        // Act
        await _jobService.AddJobAsync(jobDto);

        // Assert
        Assert.That(addedJob, Is.Not.Null);
        Assert.That(addedJob.CompanyName, Is.EqualTo("Tech Corp"));
    }

    [Test]
    public async Task GetJobsByPersonIdAndDateRangeAsync_ReturnsJobsByDateRange()
    {
        // Arrange
        var personId = Guid.NewGuid();
        var startDate = new DateTime(2020, 1, 1);
        var endDate = new DateTime(2021, 1, 1);
        var jobs = new List<Job>
        {
            new Job
            {
                Id = Guid.NewGuid(),
                CompanyName = "Tech Corp",
                Position = "Developer",
                StartDate = new DateTime(2020, 6, 1),
                PersonId = personId
            }
        };

        _jobRepositoryMock.Setup(repo => repo.GetJobsByPersonIdAndDateRangeAsync(personId, startDate, endDate)).ReturnsAsync(jobs);

        // Act
        var result = (await _jobService.GetJobsByPersonIdAndDateRangeAsync(personId, startDate, endDate)).ToList(); // Materialize the collection

        // Assert
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result.First().CompanyName, Is.EqualTo("Tech Corp"));
    }
}