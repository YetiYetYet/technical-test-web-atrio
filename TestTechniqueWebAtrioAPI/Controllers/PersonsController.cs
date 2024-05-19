using Microsoft.AspNetCore.Mvc;
using TestTechniqueWebAtrio.DTO;
using TestTechniqueWebAtrio.Models;
using TestTechniqueWebAtrio.Services;

namespace TestTechniqueWebAtrio.Controllers;

/// <summary>
/// API for managing persons.
/// </summary>
[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IPersonService _personService;
    private readonly ILogger<PersonsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonsController"/> class.
    /// </summary>
    /// <param name="personService"></param>
    /// <param name="logger"></param>
    public PersonsController(IPersonService personService, ILogger<PersonsController> logger)
    {
        _personService = personService;
        _logger = logger;
    }

    /// <summary>
    /// Gets all persons.
    /// </summary>
    /// <returns>A list of persons with their jobs.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllPersons()
    {
        _logger.LogInformation("Getting all persons.");
        var persons = await _personService.GetAllPersonsAsync();
        return Ok(persons);
    }

    /// <summary>
    /// Gets a person by their ID.
    /// </summary>
    /// <param name="id">The ID of the person.</param>
    /// <returns>The person with the specified ID.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPersonById(Guid id)
    {
        _logger.LogInformation("Getting person with ID {PersonId}", id);
        var person = await _personService.GetPersonByIdAsync(id);
        if (person == null)
        {
            _logger.LogWarning("Person with ID {PersonId} not found.", id);
            return NotFound();
        }
        return Ok(person);
    }

    /// <summary>
    /// Adds a new person.
    /// </summary>
    /// <param name="personDto">The person data.</param>
    /// <returns>The created person.</returns>
    [HttpPost]
    public async Task<IActionResult> AddPerson([FromBody] PersonCreateDTO personDto)
    {
        try
        {
            _logger.LogInformation("Adding a new person.");
            var createdPerson = await _personService.AddPersonAsync(personDto);
            return CreatedAtAction(nameof(GetPersonById), new { id = createdPerson.Id }, createdPerson);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Error adding person: {Message}", ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Gets persons by company name.
    /// </summary>
    /// <param name="companyName">The name of the company.</param>
    /// <returns>A list of persons working for the specified company.</returns>
    [HttpGet("company/{companyName}")]
    public async Task<IActionResult> GetPersonsByCompany(string companyName)
    {
        _logger.LogInformation("Getting persons by company {CompanyName}", companyName);
        var persons = await _personService.GetPersonsByCompanyAsync(companyName);
        return Ok(persons);
    }
}