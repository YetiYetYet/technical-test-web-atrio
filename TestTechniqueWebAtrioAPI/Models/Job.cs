using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestTechniqueWebAtrio.Models;

public class Job
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [MaxLength(100)]
    public required string CompanyName { get; set; }
    
    [MaxLength(100)]
    public required string Position { get; set; }

    public required DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    
    public required Guid PersonId { get; set; }

    [ForeignKey("PersonId")]
    [JsonIgnore]
    public Person Person { get; set; }
}