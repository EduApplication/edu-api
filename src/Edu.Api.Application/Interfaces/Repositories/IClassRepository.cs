using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Interfaces.Repositories;

/// <summary>
/// Repository interface for managing class entities
/// </summary>
public interface IClassRepository : IBaseRepository<Class, Guid>
{
    /// <summary>
    /// Retrieves all classes associated with a specific teacher
    /// </summary>
    /// <param name="teacherId">The unique identifier of the teacher</param>
    /// <returns>A collection of classes taught by the specified teacher</returns>
    Task<IEnumerable<Class>> GetClassesByTeacherIdAsync(Guid teacherId);

    /// <summary>
    /// Checks if a teacher is associated with a specific class
    /// </summary>
    /// <param name="teacherId">The unique identifier of the teacher</param>
    /// <param name="classId">The unique identifier of the class</param>
    /// <returns>True if the teacher is associated with the class; otherwise, false</returns>
    Task<bool> IsTeacherOfClassAsync(Guid teacherId, Guid classId);
}