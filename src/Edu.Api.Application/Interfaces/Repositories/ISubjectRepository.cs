using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Interfaces.Repositories;

/// <summary>
/// Repository interface for managing subject entities
/// </summary>
public interface ISubjectRepository : IBaseRepository<Subject, Guid>
{
    /// <summary>
    /// Retrieves all subjects for which a specific student is enrolled
    /// </summary>
    /// <param name="studentId">The unique identifier of the student</param>
    /// <returns>A collection of subjects the student is studying</returns>
    Task<IEnumerable<Subject>> GetSubjectsByStudentIdAsync(Guid studentId);

    /// <summary>
    /// Retrieves all subjects taught by a specific teacher
    /// </summary>
    /// <param name="teacherId">The unique identifier of the teacher</param>
    /// <returns>A collection of subjects taught by the specified teacher</returns>
    Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(Guid teacherId);

    /// <summary>
    /// Checks if a teacher is assigned to teach a specific subject
    /// </summary>
    /// <param name="teacherId">The unique identifier of the teacher</param>
    /// <param name="subjectId">The unique identifier of the subject</param>
    /// <returns>True if the teacher teaches the subject; otherwise, false</returns>
    Task<bool> IsTeacherOfSubjectAsync(Guid teacherId, Guid subjectId);

    /// <summary>
    /// Retrieves a subject with its associated teachers
    /// </summary>
    /// <param name="id">The unique identifier of the subject</param>
    /// <returns>The subject entity with its collection of teachers, or null if not found</returns>
    Task<Subject?> GetSubjectWithTeachersAsync(Guid id);

    /// <summary>
    /// Retrieves all subjects with their associated teachers
    /// </summary>
    /// <returns>A collection of all subjects with their teachers</returns>
    Task<IEnumerable<Subject>> GetAllSubjectsWithTeachersAsync();

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

    /// <summary>
    /// Retrieves student-class relationships for the given class identifiers
    /// </summary>
    /// <param name="classIds">A list of class identifiers</param>
    /// <returns>A collection of student-class relationships for the specified classes</returns>
    Task<IEnumerable<StudentClass>> GetStudentClass(List<Guid> classIds);
}