using Edu.Api.Application.DTOs.Class;

namespace Edu.Api.Application.Interfaces.Services;

/// <summary>
/// Service interface for managing classes in the educational system
/// </summary>
public interface IClassService : IBaseService<CreateClassDto, ClassDetailsDto, ClassDto, Guid>
{
    /// <summary>
    /// Retrieves all classes associated with the currently authenticated teacher
    /// </summary>
    /// <returns>A collection of class DTOs for the current teacher</returns>
    Task<IEnumerable<ClassDto>> GetTeacherClassesAsync();

    /// <summary>
    /// Retrieves all classes in which the currently authenticated student is enrolled
    /// </summary>
    /// <returns>A collection of class DTOs for the current student</returns>
    Task<IEnumerable<ClassDto>> GetStudentClassesAsync();

    /// <summary>
    /// Retrieves all classes in which the children of the currently authenticated parent are enrolled
    /// </summary>
    /// <returns>A collection of class DTOs for the current parent's children</returns>
    Task<IEnumerable<ClassDto>> GetParentClassesAsync();

    /// <summary>
    /// Adds a student to a class (enrolls the student)
    /// </summary>
    /// <param name="classId">The unique identifier of the class</param>
    /// <param name="studentId">The unique identifier of the student</param>
    /// <returns>The identifier of the created student-class relationship</returns>
    Task<Guid> AddStudentToClass(Guid classId, Guid studentId);
}