using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Interfaces.Repositories;

/// <summary>
/// Repository interface for managing grade entities
/// </summary>
public interface IGradeRepository : IBaseRepository<Grade, Guid>
{
    /// <summary>
    /// Retrieves all grades for a specific student
    /// </summary>
    /// <param name="studentId">The unique identifier of the student</param>
    /// <returns>A collection of grades assigned to the specified student</returns>
    Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(Guid studentId);

    /// <summary>
    /// Retrieves all grades for a specific student in a specific subject
    /// </summary>
    /// <param name="studentId">The unique identifier of the student</param>
    /// <param name="subjectId">The unique identifier of the subject</param>
    /// <returns>A collection of grades for the specified student in the specified subject</returns>
    Task<IEnumerable<Grade>> GetGradesBySubjectForStudentAsync(Guid studentId, Guid subjectId);
}