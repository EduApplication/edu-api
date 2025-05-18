namespace Edu.Api.Domain.Enums;

/// <summary>
/// Defines the available user roles in the educational system
/// </summary>
public enum UserRoles
{
    /// <summary>
    /// Administrator role with full system access
    /// </summary>
    Admin = 1,

    /// <summary>
    /// Teacher role for educational staff
    /// </summary>
    Teacher = 2,

    /// <summary>
    /// Student role for enrolled learners
    /// </summary>
    Student = 3,

    /// <summary>
    /// Parent role for guardians of students
    /// </summary>
    Parent = 4,
}