namespace Edu.Api.Application.DTOs.User;

/// <summary>
/// Data transfer object for detailed user information including related data
/// </summary>
public class UserDetailsDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the user
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the first name of the user
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the role name of the user
    /// </summary>
    public string? RoleName { get; set; }

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
    /// Gets or sets a value indicating whether the user is currently active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the names of subjects associated with the user
    /// </summary>
    public ICollection<string?>? SubjectNames { get; set; }

    /// <summary>
    /// Gets or sets the names of classes the user is enrolled in or teaches
    /// </summary>
    public ICollection<string?>? ClassNames { get; set; }

    /// <summary>
    /// Gets or sets the collection of parent users associated with this user
    /// </summary>
    public ICollection<RelatedUserInfoDto>? Parents { get; set; }

    /// <summary>
    /// Gets or sets the collection of child users associated with this user
    /// </summary>
    public ICollection<RelatedUserInfoDto>? Children { get; set; }

    /// <summary>
    /// Gets or sets the names of classes where the user is the class tutor/teacher
    /// </summary>
    public ICollection<string?>? TutorClassNames { get; set; }
}