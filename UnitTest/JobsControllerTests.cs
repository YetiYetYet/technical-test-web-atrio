using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TestTechniqueWebAtrio.Controllers;
using TestTechniqueWebAtrio.DTO;
using TestTechniqueWebAtrio.Services;

namespace UnitTest;

[TestFixture]
public class JobsControllerTests
{
    private Mock<IJobService> _jobServiceMock;
    private Mock<ILogger<JobsController>> _loggerMock;
    private JobsController _controller;

    [SetUp]
    public void SetUp()
    {
        _jobServiceMock = new Mock<IJobService>();
        _loggerMock = new Mock<ILogger<JobsController>>();
        _controller = new JobsController(_jobServiceMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task AddJob_ReturnsOkResult_WithJob()
    {
        // Arrange
        var jobDto = new JobCreateDTO
        {
            CompanyName = "Tech Corp",
            Position = "Developer",
            StartDate = DateTime.UtcNow,
            PersonId = Guid.NewGuid()
        };

        // Act
        var result = await _controller.AddJob(jobDto);
        var okResult = result as OkObjectResult;

        // Assert
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.StatusCode, Is.EqualTo(200));
        var returnedJob = okResult.Value as JobCreateDTO;
        Assert.That(returnedJob.CompanyName, Is.EqualTo("Tech Corp"));
    }

    [Test]
    public async Task GetJobsByPersonAndDateRange_ReturnsOkResult_WithJobs()
    {
        // Arrange
        var personId = Guid.NewGuid();
        var startDate = new DateTime(2020, 1, 1);
        var endDate = new DateTime(2021, 1, 1);
        var jobs = new List<JobDTO>
        {
            new JobDTO
            {
                Id = Guid.NewGuid(),
                CompanyName = "Tech Corp",
                Position = "Developer",
                StartDate = new DateTime(2020, 6, 1),
                PersonId = personId
            }
        };

        _jobServiceMock.Setup(service => service.GetJobsByPersonIdAndDateRangeAsync(personId, startDate, endDate)).ReturnsAsync(jobs);

        // Act
        var result = (await _controller.GetJobsByPersonAndDateRange(personId, startDate, endDate) as OkObjectResult)?.Value as IEnumerable<JobDTO>;
        var materializedResult = result?.ToList();

        // Assert
        Assert.That(materializedResult, Is.Not.Null);
        Assert.That(materializedResult.Count, Is.EqualTo(1));
    }
}