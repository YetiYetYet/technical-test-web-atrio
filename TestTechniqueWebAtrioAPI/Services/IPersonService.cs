using TestTechniqueWebAtrio.DTO;

namespace TestTechniqueWebAtrio.Services;

/// <summary>
/// Interface for managing Person entities' business logic.
/// </summary>
/// <summary>
/// Interface for managing Person entities' business logic.
/// </summary>
public interface IPersonService
{
    /// <summary>
    /// Retrieves all persons.
    /// </summary>
    /// <returns>A list of persons with their jobs.</returns>
    Task<IEnumerable<PersonWithJobsDTO>> GetAllPersonsAsync();

    /// <summary>
    /// Retrieves a person by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the person.</param>
    /// <returns>The person with the specified id and their jobs.</returns>
    Task<PersonWithJobsDTO?> GetPersonByIdAsync(Guid id);

    /// <summary>
    /// Adds a new person.
    /// </summary>
    /// <param name="personDto">The person creation data transfer object.</param>
    /// <returns>The created person with their jobs.</returns>
    Task<PersonWithJobsDTO?> AddPersonAsync(PersonCreateDTO personDto);

    /// <summary>
    /// Retrieves persons by the company they work for.
    /// </summary>
    /// <param name="companyName">The name of the company.</param>
    /// <returns>A list of persons working for the specified company with their jobs.</returns>
    Task<IEnumerable<PersonWithJobsDTO>> GetPersonsByCompanyAsync(string companyName);
}