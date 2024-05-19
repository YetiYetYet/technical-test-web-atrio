using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TestTechniqueWebAtrio.Controllers;
using TestTechniqueWebAtrio.DTO;
using TestTechniqueWebAtrio.Services;

namespace UnitTest;

[TestFixture]
public class PersonsControllerTests
{
    private Mock<IPersonService> _personServiceMock;
    private Mock<ILogger<PersonsController>> _loggerMock;
    private PersonsController _controller;

    [SetUp]
    public void SetUp()
    {
        _personServiceMock = new Mock<IPersonService>();
        _loggerMock = new Mock<ILogger<PersonsController>>();
        _controller = new PersonsController(_personServiceMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetAllPersons_ReturnsOkResult_WithPersons()
    {
        // Arrange
        var persons = new List<PersonWithJobsDTO>
        {
            new PersonWithJobsDTO
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Jobs = new List<JobDTO>()
            }
        };

        _personServiceMock.Setup(service => service.GetAllPersonsAsync()).ReturnsAsync(persons);

        // Act
        var result = (await _controller.GetAllPersons() as OkObjectResult)?.Value as IEnumerable<PersonWithJobsDTO>;
        var materializedResult = result?.ToList(); // Materialize the collection

        // Assert
        Assert.That(materializedResult, Is.Not.Null);
        Assert.That(materializedResult.Count, Is.EqualTo(1));
    }

    [Test]
    public async Task GetPersonById_ReturnsOkResult_WithPerson()
    {
        // Arrange
        var personId = Guid.NewGuid();
        var person = new PersonWithJobsDTO
        {
            Id = personId,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Jobs = new List<JobDTO>()
        };

        _personServiceMock.Setup(service => service.GetPersonByIdAsync(personId)).ReturnsAsync(person);

        // Act
        var result = (await _controller.GetPersonById(personId) as OkObjectResult)?.Value as PersonWithJobsDTO;

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.FirstName, Is.EqualTo("John"));
    }

    [Test]
    public async Task GetPersonById_ReturnsNotFound_WhenPersonDoesNotExist()
    {
        // Arrange
        var personId = Guid.NewGuid();
        _personServiceMock.Setup(service => service.GetPersonByIdAsync(personId)).ReturnsAsync((PersonWithJobsDTO)null);

        // Act
        var result = await _controller.GetPersonById(personId);

        // Assert
        var notFoundResult = result as NotFoundResult;
        Assert.That(notFoundResult, Is.Not.Null);
        Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
    }

    [Test]
    public async Task AddPerson_ReturnsCreatedAtAction_WithPerson()
    {
        // Arrange
        var personDto = new PersonCreateDTO
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1)
        };

        var createdPerson = new PersonWithJobsDTO
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Jobs = new List<JobDTO>()
        };

        _personServiceMock.Setup(service => service.AddPersonAsync(personDto)).ReturnsAsync(createdPerson);

        // Act
        var result = await _controller.AddPerson(personDto);
        var createdAtActionResult = result as CreatedAtActionResult;

        // Assert
        Assert.That(createdAtActionResult, Is.Not.Null);
        Assert.That(createdAtActionResult.StatusCode, Is.EqualTo(201));
        var returnedPerson = createdAtActionResult.Value as PersonWithJobsDTO;
        Assert.That(returnedPerson.FirstName, Is.EqualTo("John"));
    }

    [Test]
    public async Task GetPersonsByCompany_ReturnsOkResult_WithPersons()
    {
        // Arrange
        var companyName = "Tech Corp";
        var persons = new List<PersonWithJobsDTO>
        {
            new PersonWithJobsDTO
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Jobs = new List<JobDTO>
                {
                    new JobDTO
                    {
                        Id = Guid.NewGuid(),
                        CompanyName = companyName,
                        Position = "Developer",
                        StartDate = DateTime.UtcNow,
                        EndDate = null,
                        PersonId = Guid.NewGuid()
                    }
                }
            }
        };

        _personServiceMock.Setup(service => service.GetPersonsByCompanyAsync(companyName)).ReturnsAsync(persons);

        // Act
        var result = (await _controller.GetPersonsByCompany(companyName) as OkObjectResult)?.Value as IEnumerable<PersonWithJobsDTO>;
        var materializedResult = result?.ToList();

        // Assert
        Assert.That(materializedResult, Is.Not.Null);
        Assert.That(materializedResult.Count, Is.EqualTo(1));
    }
}