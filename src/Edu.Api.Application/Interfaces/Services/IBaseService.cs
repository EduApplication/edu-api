namespace Edu.Api.Application.Interfaces.Services;

/// <summary>
/// Generic base service interface for CRUD operations
/// </summary>
/// <typeparam name="TCreateDto">The DTO type used for creating and updating entities</typeparam>
/// <typeparam name="TDetailsDto">The DTO type for detailed entity representation</typeparam>
/// <typeparam name="TDto">The DTO type for basic entity representation in lists</typeparam>
/// <typeparam name="TId">The type of the entity's unique identifier</typeparam>
public interface IBaseService<TCreateDto, TDetailsDto, TDto, TId>
   where TCreateDto : class
   where TDto : class
{
    /// <summary>
    /// Retrieves all entities in their basic representation
    /// </summary>
    /// <returns>A collection of all entities in list format</returns>
    Task<IEnumerable<TDto>> GetAllAsync();

    /// <summary>
    /// Retrieves detailed information about a specific entity
    /// </summary>
    /// <param name="id">The unique identifier of the entity</param>
    /// <returns>Detailed representation of the requested entity</returns>
    Task<TDetailsDto> GetByIdAsync(TId id);

    /// <summary>
    /// Creates a new entity
    /// </summary>
    /// <param name="dto">DTO containing the information for the new entity</param>
    /// <returns>The identifier of the newly created entity</returns>
    Task<TId> CreateAsync(TCreateDto dto);

    /// <summary>
    /// Updates an existing entity
    /// </summary>
    /// <param name="id">The unique identifier of the entity to update</param>
    /// <param name="dto">DTO containing the updated information</param>
    Task UpdateAsync(TId id, TCreateDto dto);

    /// <summary>
    /// Deletes an entity
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete</param>
    Task DeleteAsync(TId id);
}