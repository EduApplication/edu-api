using Edu.Api.Application.DTOs.Lesson;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Mappings;

/// <summary>
/// Mapper for converting between Lesson entities and DTOs
/// </summary>
public class LessonMapper : IMapper<Lesson, LessonDto, LessonDetailsDto, CreateLessonDto, Guid>
{
    /// <summary>
    /// Maps a Lesson entity to a basic LessonDto
    /// </summary>
    /// <param name="entity">The source Lesson entity with navigation properties</param>
    /// <returns>A DTO with basic lesson information</returns>
    public LessonDto MapToDto(Lesson entity)
    {
        return new LessonDto
        {
            Id = entity.Id,
            ClassSubjectTeacherId = entity.ClassSubjectTeacherId,
            SubjectName = entity.ClassSubjectTeacher.TeacherSubject.Subject.Name,
            ClassName = entity.ClassSubjectTeacher.Class.Name,
            TeacherFirstName = entity.ClassSubjectTeacher.TeacherSubject.Teacher.FirstName,
            TeacherLastName = entity.ClassSubjectTeacher.TeacherSubject.Teacher.LastName,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            Room = entity.Room,
            Topic = entity.Topic,
            Description = entity.Description,
            DayOfWeek = entity.StartTime.DayOfWeek
        };
    }

    /// <summary>
    /// Maps a Lesson entity to a detailed LessonDetailsDto, including attachments
    /// </summary>
    /// <param name="entity">The source Lesson entity with navigation properties</param>
    /// <returns>A DTO with detailed lesson information including teacher details and attachments</returns>
    public LessonDetailsDto MapToDetailsDto(Lesson entity)
    {
        return new LessonDetailsDto
        {
            Id = entity.Id,
            ClassSubjectTeacherId = entity.ClassSubjectTeacherId,
            SubjectName = entity.ClassSubjectTeacher.TeacherSubject.Subject.Name,
            ClassName = entity.ClassSubjectTeacher.Class.Name,
            TeacherFirstName = entity.ClassSubjectTeacher.TeacherSubject.Teacher.FirstName,
            TeacherLastName = entity.ClassSubjectTeacher.TeacherSubject.Teacher.LastName,
            TeacherEmail = entity.ClassSubjectTeacher.TeacherSubject.Teacher.Email,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            Room = entity.Room,
            Topic = entity.Topic,
            Description = entity.Description,
            Attachments = entity.Attachments?
                .Select(la => new LessonAttachmentInfoDto
                {
                    AttachmentId = la.Id,
                    Title = la.Title,
                }).ToList(),
        };
    }

    /// <summary>
    /// Maps a CreateLessonDto to a new Lesson entity
    /// </summary>
    /// <param name="dto">The source DTO with lesson creation data</param>
    /// <returns>A new Lesson entity</returns>
    public Lesson MapToEntity(CreateLessonDto dto)
    {
        return new Lesson
        {
            ClassSubjectTeacherId = dto.ClassSubjectTeacherId,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            Room = dto.Room,
            Topic = dto.Topic,
            Description = dto.Description
        };
    }

    /// <summary>
    /// Updates an existing Lesson entity with data from a CreateLessonDto
    /// </summary>
    /// <param name="entity">The Lesson entity to update</param>
    /// <param name="dto">The DTO containing updated values</param>
    public void UpdateEntityFromDto(Lesson entity, CreateLessonDto dto)
    {
        entity.ClassSubjectTeacherId = dto.ClassSubjectTeacherId;
        entity.StartTime = dto.StartTime;
        entity.EndTime = dto.EndTime;
        entity.Room = dto.Room;
        entity.Topic = dto.Topic;
        entity.Description = dto.Description;
    }
}