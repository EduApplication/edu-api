using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Interfaces.Repositories;

/// <summary>
/// Repository interface for managing student-class relationships
/// </summary>
public interface IStudentClassRepository : IBaseRepository<StudentClass, Guid>
{
    /// <summary>
    /// Retrieves all classes in which a specific student is enrolled
    /// </summary>
    /// <param name="studentId">The unique identifier of the student</param>
    /// <returns>A collection of class entities the student is enrolled in</returns>
    Task<IEnumerable<Class>> GetClassesByStudentIdAsync(Guid studentId);

    /// <summary>
    /// Retrieves all students enrolled in a specific class
    /// </summary>
    /// <param name="classId">The unique identifier of the class</param>
    /// <returns>A collection of user entities representing students enrolled in the specified class</returns>
    Task<IEnumerable<User>> GetStudentsByClassIdAsync(Guid classId);
}