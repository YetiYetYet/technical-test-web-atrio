using TestTechniqueWebAtrio.DTO;
using TestTechniqueWebAtrio.Models;

namespace TestTechniqueWebAtrio.Mappers;

public static class MappingHelper
{
    public static PersonWithJobsDTO? MapPersonToPersonWithJobsDto(Person person)
    {
        return new PersonWithJobsDTO
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            DateOfBirth = person.DateOfBirth,
            Jobs = person.Jobs.Select(j => new JobDTO
            {
                Id = j.Id,
                CompanyName = j.CompanyName,
                Position = j.Position,
                StartDate = j.StartDate,
                EndDate = j.EndDate,
                PersonId = j.PersonId
            }).ToList()
        };
    }

    public static IEnumerable<PersonWithJobsDTO> MapPersonsToPersonWithJobsDtos(IEnumerable<Person> persons)
    {
        return persons.Select(MapPersonToPersonWithJobsDto);
    }

    public static Person MapPersonCreateDtoToPerson(PersonCreateDTO personDto)
    {
        return new Person
        {
            FirstName = personDto.FirstName,
            LastName = personDto.LastName,
            DateOfBirth = personDto.DateOfBirth
        };
    }

    public static Job MapJobCreateDtoToJob(JobCreateDTO jobDto)
    {
        return new Job
        {
            CompanyName = jobDto.CompanyName,
            Position = jobDto.Position,
            StartDate = jobDto.StartDate,
            EndDate = jobDto.EndDate,
            PersonId = jobDto.PersonId
        };
    }

    public static JobDTO MapJobToJobDto(Job job)
    {
        return new JobDTO
        {
            Id = job.Id,
            CompanyName = job.CompanyName,
            Position = job.Position,
            StartDate = job.StartDate,
            EndDate = job.EndDate,
            PersonId = job.PersonId
        };
    }

    public static IEnumerable<JobDTO> MapJobsToJobDtos(IEnumerable<Job> jobs)
    {
        return jobs.Select(MapJobToJobDto);
    }
}