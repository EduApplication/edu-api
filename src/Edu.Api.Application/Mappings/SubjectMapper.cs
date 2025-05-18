using Edu.Api.Application.DTOs.Subject;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Mappings;

/// <summary>
/// Mapper for converting between Subject entities and DTOs
/// </summary>
public class SubjectMapper : IMapper<Subject, SubjectDto, SubjectDetailsDto, CreateSubjectDto, Guid>
{
    /// <summary>
    /// Maps a Subject entity to a basic SubjectDto
    /// </summary>
    /// <param name="entity">The source Subject entity</param>
    /// <returns>A DTO with basic subject information</returns>
    public SubjectDto MapToDto(Subject entity)
    {
        return new SubjectDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            IsActive = entity.IsActive,
            Teachers = []
        };
    }

    /// <summary>
    /// Maps a Subject entity to a detailed SubjectDetailsDto, including teacher information
    /// </summary>
    /// <param name="entity">The source Subject entity with navigation properties</param>
    /// <returns>A DTO with detailed subject information including assigned teachers</returns>
    public SubjectDetailsDto MapToDetailsDto(Subject entity)
    {
        return new SubjectDetailsDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            IsActive = entity.IsActive,
            Teachers = entity.TeacherSubjects?
                .Select(ts => new SubjectTeacherDto
                {
                    TeacherId = ts.TeacherId,
                    FirstName = ts.Teacher.FirstName,
                    LastName = ts.Teacher.LastName,
                    Email = ts.Teacher.Email
                }).ToList() ?? []
        };
    }

    /// <summary>
    /// Maps a CreateSubjectDto to a new Subject entity
    /// </summary>
    /// <param name="dto">The source DTO with subject creation data</param>
    /// <returns>A new Subject entity</returns>
    public Subject MapToEntity(CreateSubjectDto dto)
    {
        return new Subject
        {
            Name = dto.Name,
            Description = dto.Description,
            IsActive = true
        };
    }

    /// <summary>
    /// Updates an existing Subject entity with data from a CreateSubjectDto
    /// </summary>
    /// <param name="entity">The Subject entity to update</param>
    /// <param name="dto">The DTO containing updated values</param>
    public void UpdateEntityFromDto(Subject entity, CreateSubjectDto dto)
    {
        entity.Name = dto.Name;
        entity.Description = dto.Description;
    }
}