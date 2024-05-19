using TestTechniqueWebAtrio.Models;

namespace TestTechniqueWebAtrio.Repositories;

/// <summary>
/// Interface for managing Person entities.
/// </summary>
public interface IPersonRepository
{
    /// <summary>
    /// Retrieves all persons from the database.
    /// </summary>
    /// <returns>A list of persons.</returns>
    Task<IEnumerable<Person>> GetAllPersonsAsync();

    /// <summary>
    /// Retrieves a person by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the person.</param>
    /// <returns>The person with the specified id.</returns>
    Task<Person> GetPersonByIdAsync(Guid id);

    /// <summary>
    /// Adds a new person to the database.
    /// </summary>
    /// <param name="person">The person to add.</param>
    Task AddPersonAsync(Person person);

    /// <summary>
    /// Retrieves persons by the company they work for.
    /// </summary>
    /// <param name="companyName">The name of the company.</param>
    /// <returns>A list of persons working for the specified company.</returns>
    Task<IEnumerable<Person>> GetPersonsByCompanyAsync(string companyName);
}