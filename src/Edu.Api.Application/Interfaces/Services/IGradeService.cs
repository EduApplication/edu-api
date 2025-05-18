using Edu.Api.Application.DTOs.Grade;

namespace Edu.Api.Application.Interfaces.Services;

/// <summary>
/// Service interface for managing grades in the educational system
/// </summary>
public interface IGradeService : IBaseService<CreateGradeDto, GradeDetailsDto, GradeDto, Guid>
{
    /// <summary>
    /// Retrieves grades for the currently authenticated student, grouped by subject
    /// </summary>
    /// <returns>A collection of student grades organized by subject with calculated averages</returns>
    Task<IEnumerable<StudentGradesBySubjectDto>> GetCurrentStudentGradesAsync();

    /// <summary>
    /// Retrieves grades for a specific student, grouped by subject
    /// </summary>
    /// <param name="studentId">The unique identifier of the student</param>
    /// <returns>A collection of student grades organized by subject with calculated averages</returns>
    Task<IEnumerable<StudentGradesBySubjectDto>> GetStudentGradesAsync(Guid studentId);

    /// <summary>
    /// Retrieves all grades for a specific student in a specific subject
    /// </summary>
    /// <param name="studentId">The unique identifier of the student</param>
    /// <param name="subjectId">The unique identifier of the subject</param>
    /// <returns>A collection of grades for the specified student in the specified subject</returns>
    Task<IEnumerable<GradeDto>> GetGradesBySubjectForStudentAsync(Guid studentId, Guid subjectId);
}