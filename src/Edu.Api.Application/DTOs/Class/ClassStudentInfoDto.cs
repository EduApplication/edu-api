namespace Edu.Api.Application.DTOs.Class;

/// <summary>
/// Basic information about a student enrolled in a class
/// </summary>
public class ClassStudentInfoDto
{
    /// <summary>
    /// Unique identifier of the student
    /// </summary>
    public Guid StudentId { get; set; }

    /// <summary>
    /// First name of the student
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name of the student
    /// </summary>
    public string? LastName { get; set; }
}