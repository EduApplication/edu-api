namespace Edu.Api.Domain.Interfaces;

/// <summary>
/// Defines a contract for entity types with a unique identifier
/// </summary>
/// <typeparam name="TId">The type of the entity identifier</typeparam>
public interface IEntity<TId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity
    /// </summary>
    TId Id { get; set; }
}