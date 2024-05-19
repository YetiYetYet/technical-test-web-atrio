using Microsoft.AspNetCore.Mvc;
using TestTechniqueWebAtrio.DTO;
using TestTechniqueWebAtrio.Models;
using TestTechniqueWebAtrio.Services;

namespace TestTechniqueWebAtrio.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IPersonService _personService;
    private readonly ILogger<PersonsController> _logger;

    public PersonsController(IPersonService personService, ILogger<PersonsController> logger)
    {
        _personService = personService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPersons()
    {
        _logger.LogInformation("Getting all persons.");
        var persons = await _personService.GetAllPersonsAsync();
        return Ok(persons);
    }

    [HttpGet("{id:guid}")]
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

    [HttpGet("company/{companyName}")]
    public async Task<IActionResult> GetPersonsByCompany(string companyName)
    {
        _logger.LogInformation("Getting persons by company {CompanyName}", companyName);
        var persons = await _personService.GetPersonsByCompanyAsync(companyName);
        return Ok(persons);
    }
}