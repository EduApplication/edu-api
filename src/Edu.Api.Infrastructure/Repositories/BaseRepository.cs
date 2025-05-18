using Microsoft.EntityFrameworkCore;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Domain.Interfaces;
using Edu.Api.Infrastructure.Data;

namespace Edu.Api.Infrastructure.Repositories;

/// <summary>
/// Base implementation of repository pattern for entity operations
/// </summary>
/// <typeparam name="TEntity">The entity type</typeparam>
/// <typeparam name="TId">The type of the entity identifier</typeparam>
/// <remarks>
/// Initializes a new instance of the base repository
/// </remarks>
/// <param name="context">The database context</param>
public class BaseRepository<TEntity, TId>(AppDbContext context) : IBaseRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
{
    /// <summary>
    /// The database context
    /// </summary>
    protected readonly AppDbContext _context = context;

    /// <summary>
    /// The DbSet for the entity type
    /// </summary>
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    /// <summary>
    /// Retrieves all entities of type TEntity
    /// </summary>
    /// <returns>A collection of all entities</returns>
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    /// <summary>
    /// Retrieves an entity by its identifier
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>The entity if found, otherwise null</returns>
    public virtual async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await _dbSet.FindAsync(id);
    }

    /// <summary>
    /// Adds a new entity to the database
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>The identifier of the added entity</returns>
    public virtual async Task<TId> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Updates an existing entity in the database
    /// </summary>
    /// <param name="entity">The entity to update</param>
    public virtual async Task UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes an entity by its identifier
    /// </summary>
    /// <param name="id">The identifier of the entity to delete</param>
    public virtual async Task DeleteAsync(TId id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}