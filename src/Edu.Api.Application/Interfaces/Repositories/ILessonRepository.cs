using Edu.Api.Application.DTOs.Lesson;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Interfaces.Repositories;

/// <summary>
/// Repository interface for managing lesson entities
/// </summary>
public interface ILessonRepository : IBaseRepository<Lesson, Guid>
{
    /// <summary>
    /// Retrieves all lessons scheduled for a specific class
    /// </summary>
    /// <param name="classId">The unique identifier of the class</param>
    /// <returns>A collection of lessons for the specified class</returns>
    Task<IEnumerable<Lesson>> GetLessonsByClassIdAsync(Guid classId);

    /// <summary>
    /// Retrieves all lessons taught by a specific teacher
    /// </summary>
    /// <param name="teacherId">The unique identifier of the teacher</param>
    /// <returns>A collection of lessons taught by the specified teacher</returns>
    Task<IEnumerable<Lesson>> GetLessonsByTeacherIdAsync(Guid teacherId);

    /// <summary>
    /// Retrieves all lessons for a specific subject
    /// </summary>
    /// <param name="subjectId">The unique identifier of the subject</param>
    /// <returns>A collection of lessons for the specified subject</returns>
    Task<IEnumerable<Lesson>> GetLessonsBySubjectIdAsync(Guid subjectId);

    /// <summary>
    /// Retrieves lessons based on specified filter criteria
    /// </summary>
    /// <param name="filter">DTO containing filter parameters such as date range, class, teacher, and subject</param>
    /// <returns>A collection of lessons matching the filter criteria</returns>
    Task<IEnumerable<Lesson>> GetLessonsByFilterAsync(ScheduleFilterDto filter);
}