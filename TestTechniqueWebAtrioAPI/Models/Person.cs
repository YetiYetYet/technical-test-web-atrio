using System.ComponentModel.DataAnnotations;

namespace TestTechniqueWebAtrio.Models;

public class Person
{
    [Key]
    public Guid Id { get; set; }
    
    [MaxLength(50)]
    public required string FirstName { get; set; }
    
    [MaxLength(50)]
    public required string LastName { get; set; }

    public required DateTime DateOfBirth { get; set; }

    public ICollection<Job> Jobs { get; set; }
}