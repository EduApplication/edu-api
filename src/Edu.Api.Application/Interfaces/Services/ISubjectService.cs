using Edu.Api.Application.DTOs.Subject;

namespace Edu.Api.Application.Interfaces.Services;

/// <summary>
/// Service interface for managing subjects in the educational system
/// </summary>
public interface ISubjectService : IBaseService<CreateSubjectDto, SubjectDetailsDto, SubjectDto, Guid>
{
    /// <summary>
    /// Retrieves all subjects taught by the currently authenticated teacher
    /// </summary>
    /// <returns>A collection of subject DTOs for the current teacher</returns>
    Task<IEnumerable<SubjectDto>> GetTeacherSubjectsAsync();

    /// <summary>
    /// Retrieves all subjects in which the currently authenticated student is enrolled
    /// </summary>
    /// <returns>A collection of subject DTOs for the current student</returns>
    Task<IEnumerable<SubjectDto>> GetStudentSubjectsAsync();

    /// <summary>
    /// Retrieves detailed information about a specific subject, including its teachers
    /// </summary>
    /// <param name="id">The unique identifier of the subject</param>
    /// <returns>Detailed representation of the requested subject</returns>
    Task<SubjectDetailsDto> GetSubjectDetailsAsync(Guid id);

    /// <summary>
    /// Assigns a teacher to a subject
    /// </summary>
    /// <param name="subjectId">The unique identifier of the subject</param>
    /// <param name="teacherId">The unique identifier of the teacher</param>
    Task<Guid> AddTeacherToSubjectAsync(Guid subjectId, Guid teacherId);

    /// <summary>
    /// Removes a teacher from a subject
    /// </summary>
    /// <param name="subjectId">The unique identifier of the subject</param>
    /// <param name="teacherId">The unique identifier of the teacher</param>
    Task RemoveTeacherFromSubjectAsync(Guid subjectId, Guid teacherId);
}