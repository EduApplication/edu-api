using Edu.Api.Application.DTOs.Class;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Mappings;

/// <summary>
/// Mapper for converting between Class entities and DTOs
/// </summary>
public class ClassMapper : IMapper<Class, ClassDto, ClassDetailsDto, CreateClassDto, Guid>
{
    /// <summary>
    /// Maps a Class entity to a basic ClassDto
    /// </summary>
    /// <param name="entity">The source Class entity</param>
    /// <returns>A DTO with basic class information</returns>
    public ClassDto MapToDto(Class entity)
    {
        return new ClassDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Year = entity.Year,
            Section = entity.Section,
            ClassTeacherFirstName = entity.ClassTeacher?.FirstName,
            ClassTeacherLastName = entity.ClassTeacher?.LastName,
            IsActive = entity.IsActive
        };
    }

    /// <summary>
    /// Maps a Class entity to a detailed ClassDetailsDto, including teacher and student information
    /// </summary>
    /// <param name="entity">The source Class entity with its navigation properties</param>
    /// <returns>A DTO with detailed class information including class teacher and enrolled students</returns>
    public ClassDetailsDto MapToDetailsDto(Class entity)
    {
        return new ClassDetailsDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Year = entity.Year,
            Section = entity.Section,
            IsActive = entity.IsActive,
            ClassTeacherFirstName = entity.ClassTeacher?.FirstName,
            ClassTeacherLastName = entity.ClassTeacher?.LastName,
            ClassTeacherEmail = entity.ClassTeacher?.Email,
            ClassTecherId = entity.ClassTeacherId,
            Students = entity.StudentClasses?
                .Select(sc => new ClassStudentInfoDto
                {
                    StudentId = sc.StudentId,
                    FirstName = sc.Student.FirstName,
                    LastName = sc.Student.LastName,
                }).ToList(),
        };
    }

    /// <summary>
    /// Maps a CreateClassDto to a new Class entity
    /// </summary>
    /// <param name="dto">The source DTO with class creation data</param>
    /// <returns>A new Class entity</returns>
    public Class MapToEntity(CreateClassDto dto)
    {
        return new Class
        {
            Name = dto.Name ?? "",
            Year = dto.Year,
            Section = dto.Section ?? "",
            ClassTeacherId = dto.ClassTeacherId,
            IsActive = true
        };
    }

    /// <summary>
    /// Updates an existing Class entity with data from a CreateClassDto
    /// </summary>
    /// <param name="entity">The Class entity to update</param>
    /// <param name="dto">The DTO containing updated values</param>
    public void UpdateEntityFromDto(Class entity, CreateClassDto dto)
    {
        entity.Name = dto.Name;
        entity.Year = dto.Year;
        entity.Section = dto.Section;
        entity.ClassTeacherId = dto.ClassTeacherId;
    }
}