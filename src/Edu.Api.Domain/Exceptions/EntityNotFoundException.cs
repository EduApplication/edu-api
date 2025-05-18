namespace Edu.Api.Domain.Exceptions;

/// <summary>
/// Exception thrown when an entity is not found
/// </summary>
public class EntityNotFoundException : AppException
{
    /// <summary>
    /// Initializes a new instance of the entity not found exception
    /// </summary>
    /// <param name="entityName">The name of the entity</param>
    /// <param name="id">The identifier that was searched for</param>
    public EntityNotFoundException(string entityName, object id)
        : base($"{entityName} with ID {id} was not found",
               "entity_not_found",
               404)
    {
    }
}