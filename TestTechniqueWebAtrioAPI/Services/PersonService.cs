using TestTechniqueWebAtrio.DTO;
using TestTechniqueWebAtrio.Mappers;
using TestTechniqueWebAtrio.Models;
using TestTechniqueWebAtrio.Repositories;

namespace TestTechniqueWebAtrio.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<PersonService> _logger;

    public PersonService(IPersonRepository personRepository, ILogger<PersonService> logger)
    {
        _personRepository = personRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<PersonWithJobsDTO>> GetAllPersonsAsync()
    {
        _logger.LogInformation("Retrieving all persons.");
        var persons = await _personRepository.GetAllPersonsAsync();
        return MappingHelper.MapPersonsToPersonWithJobsDtos(persons);
    }

    public async Task<PersonWithJobsDTO?> GetPersonByIdAsync(Guid id)
    {
        _logger.LogInformation("Retrieving person with ID {PersonId}", id);
        var person = await _personRepository.GetPersonByIdAsync(id);
        if (person == null)
        {
            _logger.LogWarning("Person with ID {PersonId} not found.", id);
            return null;
        }

        return MappingHelper.MapPersonToPersonWithJobsDto(person);
    }

    public async Task<PersonWithJobsDTO?> AddPersonAsync(PersonCreateDTO personDto)
    {
        if (DateTime.Now.Year - personDto.DateOfBirth.Year > 150)
        {
            _logger.LogWarning("Person is older than 150 years.");
            throw new ArgumentException("Person cannot be older than 150 years.");
        }

        var person = MappingHelper.MapPersonCreateDtoToPerson(personDto);

        _logger.LogInformation("Adding a new person.");
        await _personRepository.AddPersonAsync(person);

        return MappingHelper.MapPersonToPersonWithJobsDto(person); // Return the created person
    }

    public async Task<IEnumerable<PersonWithJobsDTO>> GetPersonsByCompanyAsync(string companyName)
    {
        _logger.LogInformation("Retrieving persons working for company {CompanyName}", companyName);
        var persons = await _personRepository.GetPersonsByCompanyAsync(companyName);
        return MappingHelper.MapPersonsToPersonWithJobsDtos(persons);
    }
}