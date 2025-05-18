using Edu.Api.Application.DTOs.ClassSubjectTeacher;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Mappings;

/// <summary>
/// Mapper for converting between ClassSubjectTeacher entities and DTOs
/// </summary>
public class ClassSubjectTeacherMapper : IMapper<ClassSubjectTeacher, ClassSubjectTeacherDto, ClassSubjectTeacherDetailsDto, CreateClassSubjectTeacherDto, Guid>
{
    /// <summary>
    /// Maps a ClassSubjectTeacher entity to a basic ClassSubjectTeacherDto
    /// </summary>
    /// <param name="entity">The source ClassSubjectTeacher entity with navigation properties</param>
    /// <returns>A DTO with class, subject, and teacher information</returns>
    public ClassSubjectTeacherDto MapToDto(ClassSubjectTeacher entity)
    {
        return new ClassSubjectTeacherDto
        {
            Id = entity.Id,
            ClassId = entity.ClassId,
            ClassName = entity.Class.Name,
            SubjectId = entity.TeacherSubject.SubjectId,
            SubjectName = entity.TeacherSubject.Subject.Name,
            TeacherId = entity.TeacherSubject.TeacherId,
            TeacherFirstName = entity.TeacherSubject.Teacher.FirstName,
            TeacherLastName = entity.TeacherSubject.Teacher.LastName,
        };
    }

    /// <summary>
    /// Maps a ClassSubjectTeacher entity to a detailed ClassSubjectTeacherDetailsDto
    /// </summary>
    /// <param name="entity">The source ClassSubjectTeacher entity with navigation properties</param>
    /// <returns>A detailed DTO with class, subject, and teacher information</returns>
    public ClassSubjectTeacherDetailsDto MapToDetailsDto(ClassSubjectTeacher entity)
    {
        return new ClassSubjectTeacherDetailsDto
        {
            Id = entity.Id,
            ClassId = entity.ClassId,
            ClassName = entity.Class.Name,
            SubjectId = entity.TeacherSubject.SubjectId,
            SubjectName = entity.TeacherSubject.Subject.Name,
            TeacherId = entity.TeacherSubject.TeacherId,
            TeacherFirstName = entity.TeacherSubject.Teacher.FirstName,
            TeacherLastName = entity.TeacherSubject.Teacher.LastName,
        };
    }

    /// <summary>
    /// Maps a CreateClassSubjectTeacherDto to a new ClassSubjectTeacher entity
    /// </summary>
    /// <param name="dto">The source DTO with creation data</param>
    /// <returns>A new ClassSubjectTeacher entity</returns>
    public ClassSubjectTeacher MapToEntity(CreateClassSubjectTeacherDto dto)
    {
        return new ClassSubjectTeacher
        {
            ClassId = dto.ClassId,
            TeacherSubjectId = dto.TeacherSubjectId
        };
    }

    /// <summary>
    /// Updates an existing ClassSubjectTeacher entity with data from a CreateClassSubjectTeacherDto
    /// </summary>
    /// <param name="entity">The ClassSubjectTeacher entity to update</param>
    /// <param name="dto">The DTO containing updated values</param>
    public void UpdateEntityFromDto(ClassSubjectTeacher entity, CreateClassSubjectTeacherDto dto)
    {
        entity.ClassId = dto.ClassId;
        entity.TeacherSubjectId = dto.TeacherSubjectId;
    }
}