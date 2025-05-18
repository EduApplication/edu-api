using Microsoft.AspNetCore.Mvc;
using Edu.Api.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace Edu.Api.WebApi.Controllers;

/// <summary>
/// Base controller providing common CRUD operations for entities
/// </summary>
/// <typeparam name="TCreateDto">Type used for creating and updating entities</typeparam>
/// <typeparam name="TDetailsDto">Type used for detailed entity representation</typeparam>
/// <typeparam name="TDto">Type used for basic entity representation</typeparam>
/// <typeparam name="TId">Type of the entity identifier</typeparam>
[ApiController]
public abstract class BaseController<TCreateDto, TDetailsDto, TDto, TId>(IBaseService<TCreateDto, TDetailsDto, TDto, TId> service) : ControllerBase
        where TCreateDto : class
        where TDto : class
{
    /// <summary>
    /// Base service for managing entities
    /// </summary>
    protected readonly IBaseService<TCreateDto, TDetailsDto, TDto, TId> _service = service;

    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>Collection of entities in basic representation</returns>
    /// <response code="200">Returns the list of entities</response>
    /// <response code="401">If the user is not authenticated</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public virtual async Task<ActionResult<IEnumerable<TDto>>> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    /// <summary>
    /// Retrieves an entity by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the entity</param>
    /// <returns>Detailed representation of the entity</returns>
    /// <response code="200">Returns the requested entity</response>
    /// <response code="404">If the entity doesn't exist</response>
    /// <response code="401">If the user is not authenticated</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public virtual async Task<ActionResult<TDetailsDto>> GetById([FromRoute] TId id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    /// <summary>
    /// Creates a new entity
    /// </summary>
    /// <param name="dto">Data transfer object containing the entity information</param>
    /// <returns>The identifier of the newly created entity</returns>
    /// <response code="201">Returns the identifier of the created entity</response>
    /// <response code="400">If the request data is invalid</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user doesn't have Administrator role</response>
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public virtual async Task<ActionResult<Guid>> Create([FromBody] TCreateDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    /// <summary>
    /// Updates an existing entity
    /// </summary>
    /// <param name="id">The unique identifier of the entity to update</param>
    /// <param name="dto">Data transfer object containing the updated entity information</param>
    /// <returns>No content if the update is successful</returns>
    /// <response code="204">If the entity was successfully updated</response>
    /// <response code="400">If the request data is invalid</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user doesn't have Administrator role</response>
    /// <response code="404">If the entity doesn't exist</response>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual async Task<IActionResult> Update([FromRoute] TId id, [FromBody] TCreateDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Deletes a specific entity
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete</param>
    /// <returns>No content if the deletion is successful</returns>
    /// <response code="204">If the entity was successfully deleted</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user doesn't have Administrator role</response>
    /// <response code="404">If the entity doesn't exist</response>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual async Task<IActionResult> Delete([FromRoute] TId id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}