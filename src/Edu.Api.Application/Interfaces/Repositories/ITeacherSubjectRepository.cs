using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Interfaces.Repositories;

/// <summary>
/// Repository interface for managing teacher-subject relationships
/// </summary>
public interface ITeacherSubjectRepository : IBaseRepository<TeacherSubject, Guid>
{
    /// <summary>
    /// Retrieves all teachers assigned to a specific subject
    /// </summary>
    /// <param name="subjectId">The unique identifier of the subject</param>
    /// <returns>A collection of user entities representing teachers who teach the specified subject</returns>
    Task<IEnumerable<User>> GetTeachersBySubjectIdAsync(Guid subjectId);

    /// <summary>
    /// Retrieves all subjects taught by a specific teacher
    /// </summary>
    /// <param name="teacherId">The unique identifier of the teacher</param>
    /// <returns>A collection of subject entities taught by the specified teacher</returns>
    Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(Guid teacherId);
}