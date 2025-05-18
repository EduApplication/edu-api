using Edu.Api.Domain.Interfaces;

namespace Edu.Api.Domain.Entities;

/// <summary>
/// Represents a user in the educational system
/// </summary>
public class User : IEntity<Guid>
{
    /// <summary>
    /// Gets or sets the unique identifier for the user
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the email address of the user
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the password hash of the user
    /// </summary>
    public string? PasswordHash { get; set; }

    /// <summary>
    /// Gets or sets the first name of the user
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the role identifier of the user
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the user
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the user was created
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the user's last login
    /// </summary>
    public DateTime? LastLogin { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the role of the user
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework</remarks>
    public Role Role { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of teacher-subject relationships for this user
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework, applicable when the user is a teacher</remarks>
    public ICollection<TeacherSubject> TeacherSubjects { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of student-class relationships for this user
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework, applicable when the user is a student</remarks>
    public ICollection<StudentClass> StudentClasses { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of parent-student relationships where this user is the parent
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework, applicable when the user is a parent</remarks>
    public ICollection<ParentStudent> ParentRelationships { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of parent-student relationships where this user is the student
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework, applicable when the user is a student</remarks>
    public ICollection<ParentStudent> StudentRelationships { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of classes where this user is the class teacher (tutor)
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework, applicable when the user is a teacher assigned as class tutor</remarks>
    public ICollection<Class> ClassesAsTutor { get; set; } = [];
}