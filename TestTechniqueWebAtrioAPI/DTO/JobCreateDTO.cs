using System.ComponentModel.DataAnnotations;

namespace TestTechniqueWebAtrio.DTO;

public class JobCreateDTO
{
    [MaxLength(100)]
    public required string CompanyName { get; set; }
    
    [MaxLength(100)]
    public required string Position { get; set; }

    public required DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public required Guid PersonId { get; set; }
}