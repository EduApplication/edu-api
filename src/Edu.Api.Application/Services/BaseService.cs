using Edu.Api.Domain.Interfaces;
using Edu.Api.Application.Mappings;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Application.Interfaces.Services;
using Edu.Api.Domain.Exceptions;

namespace Edu.Api.Application.Services;

/// <summary>
/// Base service that provides common CRUD operations for entities
/// </summary>
/// <typeparam name="TEntity">The entity type</typeparam>
/// <typeparam name="TCreateDto">The DTO type used for creating entities</typeparam>
/// <typeparam name="TDetailsDto">The DTO type used for detailed entity representation</typeparam>
/// <typeparam name="TDto">The DTO type used for basic entity representation</typeparam>
/// <typeparam name="TId">The type of the entity identifier</typeparam>
public abstract class BaseService<TEntity, TCreateDto, TDetailsDto, TDto, TId>(IBaseRepository<TEntity, TId> repository, IMapper<TEntity, TDto, TDetailsDto, TCreateDto, TId> mapper)
    : IBaseService<TCreateDto, TDetailsDto, TDto, TId>
    where TEntity : class, IEntity<TId>
    where TCreateDto : class
    where TDto : class
{
    /// <summary>
    /// Repository for accessing entity data
    /// </summary>
    protected readonly IBaseRepository<TEntity, TId> _repository = repository;

    /// <summary>
    /// Mapper for converting between entity and DTO types
    /// </summary>
    protected readonly IMapper<TEntity, TDto, TDetailsDto, TCreateDto, TId> _mapper = mapper;

    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>A collection of entity DTOs</returns>
    public virtual async Task<IEnumerable<TDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(_mapper.MapToDto);
    }

    /// <summary>
    /// Retrieves an entity by its identifier
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>A detailed DTO representation of the entity</returns>
    public virtual async Task<TDetailsDto> GetByIdAsync(TId id)
    {
        var entity = await _repository.GetByIdAsync(id) ??
            throw new EntityNotFoundException(typeof(TEntity).Name, id);
        return _mapper.MapToDetailsDto(entity);
    }

    /// <summary>
    /// Creates a new entity
    /// </summary>
    /// <param name="dto">The DTO containing entity creation data</param>
    /// <returns>The identifier of the created entity</returns>
    public virtual async Task<TId> CreateAsync(TCreateDto dto)
    {
        var entity = _mapper.MapToEntity(dto);
        return await _repository.AddAsync(entity);
    }

    /// <summary>
    /// Updates an existing entity
    /// </summary>
    /// <param name="id">The identifier of the entity to update</param>
    /// <param name="dto">The DTO containing updated entity data</param>
    public virtual async Task UpdateAsync(TId id, TCreateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id) ??
            throw new EntityNotFoundException(typeof(TEntity).Name, id); ;
        _mapper.UpdateEntityFromDto(entity, dto);
        await _repository.UpdateAsync(entity);
    }

    /// <summary>
    /// Deletes an entity by its identifier
    /// </summary>
    /// <param name="id">The identifier of the entity to delete</param>
    public virtual async Task DeleteAsync(TId id)
    {
        await _repository.DeleteAsync(id);
    }
}