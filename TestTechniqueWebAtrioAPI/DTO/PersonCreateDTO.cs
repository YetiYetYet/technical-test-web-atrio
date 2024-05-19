using System.ComponentModel.DataAnnotations;

namespace TestTechniqueWebAtrio.DTO;

/// <summary>
/// Represents the data transfer object for creating a person.
/// </summary>
public class PersonCreateDTO
{
    /// <summary>
    /// Represents the first name of a person.
    /// </summary>
    [MaxLength(50)]
    public required string FirstName { get; set; }

    /// <summary>
    /// Represents the last name of a person.
    /// </summary>
    [MaxLength(50)]
    public required string LastName { get; set; }

    /// <summary>
    /// Represents the date of birth of a person.
    /// </summary>
    public required DateTime DateOfBirth { get; set; }
}