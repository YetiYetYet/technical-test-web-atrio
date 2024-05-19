using TestTechniqueWebAtrio.DTO;
using TestTechniqueWebAtrio.Models;
using TestTechniqueWebAtrio.Repositories;
using TestTechniqueWebAtrio.Services;

namespace UnitTest;

using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

[TestFixture]
public class PersonServiceTests
{
    private Mock<IPersonRepository> _personRepositoryMock;
    private Mock<ILogger<PersonService>> _loggerMock;
    private PersonService _personService;

    [SetUp]
    public void SetUp()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        _loggerMock = new Mock<ILogger<PersonService>>();
        _personService = new PersonService(_personRepositoryMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetAllPersonsAsync_ReturnsAllPersons()
    {
        // Arrange
        var persons = new List<Person>
        {
            new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Jobs = new List<Job>()
            },
            new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Smith",
                DateOfBirth = new DateTime(1985, 5, 15),
                Jobs = new List<Job>()
            }
        };

        _personRepositoryMock.Setup(repo => repo.GetAllPersonsAsync()).ReturnsAsync(persons);

        // Act
        var result = (await _personService.GetAllPersonsAsync()).ToList(); // Materialize the collection

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.First().FirstName, Is.EqualTo("John"));
    }

    [Test]
    public async Task GetPersonByIdAsync_ReturnsPerson_WhenPersonExists()
    {
        // Arrange
        var personId = Guid.NewGuid();
        var person = new Person
        {
            Id = personId,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Jobs = new List<Job>()
        };

        _personRepositoryMock.Setup(repo => repo.GetPersonByIdAsync(personId)).ReturnsAsync(person);

        // Act
        var result = await _personService.GetPersonByIdAsync(personId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.FirstName, Is.EqualTo("John"));
    }

    [Test]
    public async Task GetPersonByIdAsync_ReturnsNull_WhenPersonDoesNotExist()
    {
        // Arrange
        var personId = Guid.NewGuid();
        _personRepositoryMock.Setup(repo => repo.GetPersonByIdAsync(personId)).ReturnsAsync((Person)null);

        // Act
        var result = await _personService.GetPersonByIdAsync(personId);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void AddPersonAsync_ThrowsArgumentException_WhenPersonIsOlderThan150Years()
    {
        // Arrange
        var personDto = new PersonCreateDTO
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1800, 1, 1)
        };

        // Act & Assert
        var ex = Assert.ThrowsAsync<ArgumentException>(() => _personService.AddPersonAsync(personDto));
        Assert.That(ex.Message, Is.EqualTo("Person cannot be older than 150 years."));
    }

    [Test]
    public async Task AddPersonAsync_AddsPersonSuccessfully()
    {
        // Arrange
        var personDto = new PersonCreateDTO
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1)
        };

        Person addedPerson = null;
        _personRepositoryMock.Setup(repo => repo.AddPersonAsync(It.IsAny<Person>())).Callback<Person>(p => addedPerson = p);

        // Act
        var result = await _personService.AddPersonAsync(personDto);

        // Assert
        Assert.That(addedPerson, Is.Not.Null);
        Assert.That(addedPerson.FirstName, Is.EqualTo("John"));
        Assert.That(result, Is.Not.Null);
        Assert.That(result.FirstName, Is.EqualTo("John"));
    }

    [Test]
    public async Task GetPersonsByCompanyAsync_ReturnsPersonsByCompany()
    {
        // Arrange
        var companyName = "Tech Corp";
        var persons = new List<Person>
        {
            new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Jobs = new List<Job>
                {
                    new Job
                    {
                        Id = Guid.NewGuid(),
                        CompanyName = companyName,
                        Position = "Developer",
                        StartDate = DateTime.UtcNow,
                        PersonId = Guid.NewGuid()
                    }
                }
            }
        };

        _personRepositoryMock.Setup(repo => repo.GetPersonsByCompanyAsync(companyName)).ReturnsAsync(persons);

        // Act
        var result = (await _personService.GetPersonsByCompanyAsync(companyName)).ToList(); // Materialize the collection

        // Assert
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result.First().FirstName, Is.EqualTo("John"));
    }
}