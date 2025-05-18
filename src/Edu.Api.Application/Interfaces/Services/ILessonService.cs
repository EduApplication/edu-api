using Edu.Api.Application.DTOs.Lesson;

namespace Edu.Api.Application.Interfaces.Services;

/// <summary>
/// Service interface for managing lessons and schedules in the educational system
/// </summary>
public interface ILessonService : IBaseService<CreateLessonDto, LessonDetailsDto, LessonDto, Guid>
{
    /// <summary>
    /// Retrieves the schedule for the currently authenticated student within a date range
    /// </summary>
    /// <param name="startDate">The start date of the schedule period</param>
    /// <param name="endDate">The end date of the schedule period</param>
    /// <returns>A collection of lessons scheduled for the current student in the specified period</returns>
    Task<IEnumerable<LessonDto>> GetStudentScheduleAsync(DateTime startDate, DateTime endDate);

    /// <summary>
    /// Retrieves the schedule for the currently authenticated teacher within a date range
    /// </summary>
    /// <param name="startDate">The start date of the schedule period</param>
    /// <param name="endDate">The end date of the schedule period</param>
    /// <returns>A collection of lessons scheduled for the current teacher in the specified period</returns>
    Task<IEnumerable<LessonDto>> GetTeacherScheduleAsync(DateTime startDate, DateTime endDate);

    /// <summary>
    /// Retrieves the schedule for children of the currently authenticated parent within a date range
    /// </summary>
    /// <param name="startDate">The start date of the schedule period</param>
    /// <param name="endDate">The end date of the schedule period</param>
    /// <returns>A collection of lessons scheduled for the current parent's children in the specified period</returns>
    Task<IEnumerable<LessonDto>> GetParentScheduleAsync(DateTime startDate, DateTime endDate);

    /// <summary>
    /// Retrieves lessons based on specified filter criteria
    /// </summary>
    /// <param name="filter">DTO containing filter parameters such as date range, class, teacher, and subject</param>
    /// <returns>A collection of lessons matching the filter criteria</returns>
    Task<IEnumerable<LessonDto>> GetScheduleByFilterAsync(ScheduleFilterDto filter);

    /// <summary>
    /// Retrieves all lessons scheduled for a specific class
    /// </summary>
    /// <param name="classId">The unique identifier of the class</param>
    /// <returns>A collection of lessons for the specified class</returns>
    Task<IEnumerable<LessonDto>> GetLessonsByClassIdAsync(Guid classId);

    /// <summary>
    /// Retrieves all lessons taught by a specific teacher
    /// </summary>
    /// <param name="teacherId">The unique identifier of the teacher</param>
    /// <returns>A collection of lessons taught by the specified teacher</returns>
    Task<IEnumerable<LessonDto>> GetLessonsByTeacherIdAsync(Guid teacherId);

    /// <summary>
    /// Retrieves all lessons for a specific subject
    /// </summary>
    /// <param name="subjectId">The unique identifier of the subject</param>
    /// <returns>A collection of lessons for the specified subject</returns>
    Task<IEnumerable<LessonDto>> GetLessonsBySubjectIdAsync(Guid subjectId);
}