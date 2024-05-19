using System.ComponentModel.DataAnnotations;

namespace TestTechniqueWebAtrio.DTO;

/// <summary>
/// Represents the data transfer object for creating a job.
/// </summary>
public class JobCreateDTO
{
    /// <summary>
    /// Represents the company name of a job.
    /// </summary>
    /// <remarks>
    /// The company name is a required field with a maximum length of 100 characters.
    /// </remarks>
    [MaxLength(100)]
    public required string CompanyName { get; set; }

    /// <summary>
    /// Represents the position of a job.
    /// </summary>
    /// <remarks>
    /// The position is a required field with a maximum length of 100 characters.
    /// </remarks>
    [MaxLength(100)]
    public required string Position { get; set; }

    /// <summary>
    /// Represents the start date of a job.
    /// </summary>
    /// <remarks>
    /// The start date is a required field that specifies the date when the job starts.
    /// </remarks>
    public required DateTime StartDate { get; set; }

    /// <summary>
    /// Represents the end date of a job.
    /// </summary>
    /// <remarks>
    /// The end date is an optional field that indicates the date when the job ended.
    /// If no end date is specified, it means the job is still ongoing.
    /// </remarks>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Represents the unique identifier of a person related to a job.
    /// </summary>
    /// <remarks>
    /// The PersonId property is used to associate a person with a job. It is required and of type Guid.
    /// </remarks>
    public required Guid PersonId { get; set; }
}