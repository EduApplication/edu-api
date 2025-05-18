using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Interfaces.Repositories;

/// <summary>
/// Repository interface for managing relationships between classes, subjects, and teachers
/// </summary>
public interface IClassSubjectTeacherRepository : IBaseRepository<ClassSubjectTeacher, Guid>
{
    /// <summary>
    /// Retrieves all class-subject combinations for a specific teacher
    /// </summary>
    /// <param name="teacherId">The unique identifier of the teacher</param>
    /// <returns>A collection of class-subject-teacher relationships for the specified teacher</returns>
    Task<IEnumerable<ClassSubjectTeacher>> GetClassesSubjectByTeacherIdAsync(Guid teacherId);

    /// <summary>
    /// Retrieves all class-teacher combinations for a specific subject
    /// </summary>
    /// <param name="subjectId">The unique identifier of the subject</param>
    /// <returns>A collection of class-subject-teacher relationships for the specified subject</returns>
    Task<IEnumerable<ClassSubjectTeacher>> GetClassesTeacherBySubjectIdAsync(Guid subjectId);

    /// <summary>
    /// Retrieves all teacher-subject combinations for a specific class
    /// </summary>
    /// <param name="classId">The unique identifier of the class</param>
    /// <returns>A collection of class-subject-teacher relationships for the specified class</returns>
    Task<IEnumerable<ClassSubjectTeacher>> GetTeacherSubjectByClassIdAsync(Guid classId);
}