namespace Edu.Api.Application.Mappings;

/// <summary>
/// Generic interface for entity-to-DTO mapping operations
/// </summary>
/// <typeparam name="TEntity">The entity type</typeparam>
/// <typeparam name="TDto">The basic DTO type used for list views</typeparam>
/// <typeparam name="TDetailsDto">The detailed DTO type used for detailed views</typeparam>
/// <typeparam name="TCreateDto">The DTO type used for creating and updating entities</typeparam>
/// <typeparam name="TId">The type of the entity's unique identifier</typeparam>
public interface IMapper<TEntity, TDto, TDetailsDto, TCreateDto, TId>
{
    /// <summary>
    /// Maps an entity to its basic DTO representation
    /// </summary>
    /// <param name="entity">The source entity to map</param>
    /// <returns>A DTO with basic entity information suitable for list views</returns>
    TDto MapToDto(TEntity entity);

    /// <summary>
    /// Maps an entity to its detailed DTO representation
    /// </summary>
    /// <param name="entity">The source entity to map</param>
    /// <returns>A DTO with detailed entity information including related entities</returns>
    TDetailsDto MapToDetailsDto(TEntity entity);

    /// <summary>
    /// Maps a creation DTO to a new entity
    /// </summary>
    /// <param name="createDto">The source DTO with entity creation data</param>
    /// <returns>A new entity instance populated with data from the DTO</returns>
    TEntity MapToEntity(TCreateDto createDto);

    /// <summary>
    /// Updates an existing entity with data from a DTO
    /// </summary>
    /// <param name="entity">The entity to update</param>
    /// <param name="dto">The DTO containing updated values</param>
    void UpdateEntityFromDto(TEntity entity, TCreateDto dto);
}