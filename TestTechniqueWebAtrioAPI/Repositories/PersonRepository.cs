using Microsoft.EntityFrameworkCore;
using TestTechniqueWebAtrio.Data;
using TestTechniqueWebAtrio.Models;

namespace TestTechniqueWebAtrio.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<PersonRepository> _logger;

    public PersonRepository(ApplicationDbContext context, ILogger<PersonRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Person>> GetAllPersonsAsync()
    {
        _logger.LogInformation("Retrieving all persons from the database.");
        return await _context.Persons.Include(p => p.Jobs).OrderBy(p => p.LastName).ToListAsync();
    }

    public async Task<Person> GetPersonByIdAsync(Guid id)
    {
        _logger.LogInformation("Retrieving person with ID {PersonId}", id);
        return await _context.Persons.Include(p => p.Jobs).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddPersonAsync(Person person)
    {
        _logger.LogInformation("Adding a new person to the database.");
        await _context.Persons.AddAsync(person);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Person>> GetPersonsByCompanyAsync(string companyName)
    {
        _logger.LogInformation("Retrieving persons working for company {CompanyName}", companyName);
        return await _context.Persons
            .Include(p => p.Jobs)
            .Where(p => p.Jobs.Any(j => j.CompanyName == companyName))
            .ToListAsync();
    }
}