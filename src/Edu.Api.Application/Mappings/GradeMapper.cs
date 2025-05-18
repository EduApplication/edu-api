using Edu.Api.Application.DTOs.Grade;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Mappings;

/// <summary>
/// Mapper for converting between Grade entities and DTOs
/// </summary>
public class GradeMapper : IMapper<Grade, GradeDto, GradeDetailsDto, CreateGradeDto, Guid>
{
    /// <summary>
    /// Maps a Grade entity to a basic GradeDto
    /// </summary>
    /// <param name="entity">The source Grade entity</param>
    /// <returns>A DTO with basic grade information</returns>
    public GradeDto MapToDto(Grade entity)
    {
        return new GradeDto
        {
            Id = entity.Id,
            Value = entity.Value,
            SubjectName = entity.Subject?.Name ?? "Unknown"
        };
    }

    /// <summary>
    /// Maps a Grade entity to a detailed GradeDetailsDto
    /// </summary>
    /// <param name="entity">The source Grade entity with its navigation properties</param>
    /// <returns>A DTO with detailed grade information including student, subject, teacher, and grade type details</returns>
    public GradeDetailsDto MapToDetailsDto(Grade entity)
    {
        return new GradeDetailsDto
        {
            Id = entity.Id,
            StudentId = entity.StudentId,
            StudentFirstName = entity.Student?.FirstName,
            StudentLastName = entity.Student?.LastName,
            StudentEmail = entity.Student?.Email,
            SubjectId = entity.SubjectId,
            SubjectName = entity.Subject?.Name,
            TeacherId = entity.TeacherId,
            TeacherFirstName = entity.Teacher?.FirstName,
            TeacherLastName = entity.Teacher?.LastName,
            TeacherEmail = entity.Teacher?.Email,
            Value = entity.Value,
            Comment = entity.Comment,
            CreatedAt = entity.CreatedAt,
            GradeTypeId = entity.GradeTypeId,
            GradeTypeName = entity.GradeType?.Name,
            GradeTypeDescription = entity.GradeType?.Description
        };
    }

    /// <summary>
    /// Maps a CreateGradeDto to a new Grade entity
    /// </summary>
    /// <param name="dto">The source DTO with grade creation data</param>
    /// <returns>A new Grade entity</returns>
    public Grade MapToEntity(CreateGradeDto dto)
    {
        return new Grade
        {
            StudentId = dto.StudentId,
            SubjectId = dto.SubjectId,
            GradeTypeId = dto.GradeTypeId,
            Value = dto.Value,
            Comment = dto.Comment,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Updates an existing Grade entity with data from a CreateGradeDto
    /// </summary>
    /// <param name="entity">The Grade entity to update</param>
    /// <param name="dto">The DTO containing updated values</param>
    public void UpdateEntityFromDto(Grade entity, CreateGradeDto dto)
    {
        entity.Value = dto.Value;
        entity.Comment = dto.Comment;
        entity.GradeTypeId = dto.GradeTypeId;
    }
}