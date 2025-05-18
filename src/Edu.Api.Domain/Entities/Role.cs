using Edu.Api.Domain.Interfaces;

namespace Edu.Api.Domain.Entities;

/// <summary>
/// Represents a user role in the system for authorization purposes
/// </summary>
public class Role : IEntity<int>
{
    /// <summary>
    /// Gets or sets the unique identifier for the role
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the role
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the role
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the collection of users assigned to this role
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework</remarks>
    public ICollection<User> Users { get; set; } = [];
}