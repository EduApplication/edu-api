using Edu.Api.Application.DTOs.Attachment;
using Edu.Api.Application.DTOs.Document;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Mappings;

/// <summary>
/// Mapper for converting between Attachment entities and DTOs
/// </summary>
public class AttachmentMapper : IMapper<Attachment, AttachmentDto, AttachmentDetailsDto, CreateAttachmentDto, Guid>
{
    /// <summary>
    /// Maps an Attachment entity to a basic AttachmentDto
    /// </summary>
    /// <param name="entity">The source Attachment entity</param>
    /// <returns>A DTO with basic attachment information</returns>
    public AttachmentDto MapToDto(Attachment entity)
    {
        return new AttachmentDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            DueDate = entity.DueDate,
            AssignedDate = entity.AssignedDate
        };
    }

    /// <summary>
    /// Maps an Attachment entity to a detailed AttachmentDetailsDto, including related entities
    /// </summary>
    /// <param name="entity">The source Attachment entity with its navigation properties</param>
    /// <returns>A DTO with detailed attachment information including subject, class, teacher, and documents</returns>
    public AttachmentDetailsDto MapToDetailsDto(Attachment entity)
    {
        return new AttachmentDetailsDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            DueDate = entity.DueDate,
            AssignedDate = entity.AssignedDate,
            SubjectName = entity.Lesson.ClassSubjectTeacher.TeacherSubject.Subject.Name,
            ClassName = entity.Lesson.ClassSubjectTeacher.Class.Name,
            TeacherFirstName = entity.Lesson.ClassSubjectTeacher.TeacherSubject.Teacher.FirstName,
            TeacherLastName = entity.Lesson.ClassSubjectTeacher.TeacherSubject.Teacher.LastName,
            Documents = entity.Documents?
                .Select(doc => new DocumentDto
                {
                    Id = doc.Id,
                    Name = doc.Name,
                }).ToList(),
        };
    }

    /// <summary>
    /// Maps a CreateAttachmentDto to a new Attachment entity
    /// </summary>
    /// <param name="dto">The source DTO with attachment creation data</param>
    /// <returns>A new Attachment entity</returns>
    public Attachment MapToEntity(CreateAttachmentDto dto)
    {
        return new Attachment
        {
            Title = dto.Title,
            Description = dto.Description,
            LessonId = dto.LessonId,
            DueDate = dto.DueDate,
            AssignedDate = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Updates an existing Attachment entity with data from a CreateAttachmentDto
    /// </summary>
    /// <param name="entity">The Attachment entity to update</param>
    /// <param name="dto">The DTO containing updated values</param>
    public void UpdateEntityFromDto(Attachment entity, CreateAttachmentDto dto)
    {
        entity.Title = dto.Title;
        entity.Description = dto.Description;
        entity.LessonId = dto.LessonId;
        entity.DueDate = dto.DueDate;
    }
}