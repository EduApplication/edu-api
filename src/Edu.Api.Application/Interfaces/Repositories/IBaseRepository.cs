using Edu.Api.Domain.Interfaces;

namespace Edu.Api.Application.Interfaces.Repositories;

/// <summary>
/// Generic base repository interface for entity operations
/// </summary>
/// <typeparam name="TEntity">The entity type that implements IEntity</typeparam>
/// <typeparam name="TId">The type of the entity's unique identifier</typeparam>
public interface IBaseRepository<TEntity, TId> where TEntity : class, IEntity<TId>
{
    /// <summary>
    /// Retrieves all entities of type TEntity
    /// </summary>
    /// <returns>A collection of all entities</returns>
    Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// Retrieves an entity by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the entity</param>
    /// <returns>The entity if found; otherwise, null</returns>
    Task<TEntity?> GetByIdAsync(TId id);

    /// <summary>
    /// Adds a new entity to the repository
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>The identifier of the newly added entity</returns>
    Task<TId> AddAsync(TEntity entity);

    /// <summary>
    /// Updates an existing entity in the repository
    /// </summary>
    /// <param name="entity">The entity with updated values</param>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Deletes an entity with the specified identifier
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete</param>
    Task DeleteAsync(TId id);
}