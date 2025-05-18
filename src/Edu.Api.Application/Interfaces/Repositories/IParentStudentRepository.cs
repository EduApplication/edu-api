using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Interfaces.Repositories;

/// <summary>
/// Repository interface for managing parent-student relationships
/// </summary>
public interface IParentStudentRepository : IBaseRepository<ParentStudent, Guid>
{
    /// <summary>
    /// Retrieves the identifiers of all students associated with a specific parent
    /// </summary>
    /// <param name="parentId">The unique identifier of the parent</param>
    /// <returns>A collection of student identifiers linked to the specified parent</returns>
    Task<IEnumerable<Guid>> GetStudentIdsByParentIdAsync(Guid parentId);

    /// <summary>
    /// Retrieves all student users associated with a specific parent
    /// </summary>
    /// <param name="parentId">The unique identifier of the parent</param>
    /// <returns>A collection of student user entities linked to the specified parent</returns>
    Task<IEnumerable<User>> GetStudentsByParentIdAsync(Guid parentId);

    /// <summary>
    /// Retrieves all parent users associated with a specific student
    /// </summary>
    /// <param name="studentId">The unique identifier of the student</param>
    /// <returns>A collection of parent user entities linked to the specified student</returns>
    Task<IEnumerable<User>> GetParentsByStudentIdAsync(Guid studentId);
}