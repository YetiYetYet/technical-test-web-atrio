using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TestTechniqueWebAtrio.Models;

public class Person
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [MaxLength(50)]
    public required string FirstName { get; set; }
    
    [MaxLength(50)]
    public required string LastName { get; set; }

    public required DateTime DateOfBirth { get; set; }

    [JsonIgnore]
    public ICollection<Job> Jobs { get; set; } = new List<Job>();
}