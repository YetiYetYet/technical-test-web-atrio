namespace TestTechniqueWebAtrio.DTO;

public class PersonWithJobsDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<JobDTO> Jobs { get; set; }
}