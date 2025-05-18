using Microsoft.AspNetCore.Mvc;
using Edu.Api.Application.DTOs.Attachment;
using Edu.Api.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace Edu.Api.WebApi.Controllers;

/// <summary>
/// Attachments controller
/// </summary>
/// <param name="attachmentService"></param>
[ApiController]
[Route("api/lessons/{lessonId:guid}/attachments")]
[Authorize]
public class AttachmentsController(IAttachmentService attachmentService) : ControllerBase
{
    private readonly IAttachmentService _attachmentService = attachmentService;

    /// <summary>
    /// Retrieves all attachments associated with a specific lesson
    /// </summary>
    /// <param name="lessonId">The unique identifier of the lesson</param>
    /// <returns>A collection of attachment DTOs</returns>
    /// <response code="200">Returns the list of attachments</response>
    /// <response code="404">If the lesson doesn't exist</response>
    /// <response code="401">If the user is not authenticated</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AttachmentDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<AttachmentDto>>> GetAttachmentsByLesson([FromRoute] Guid lessonId)
    {
        var attachments = await _attachmentService.GetAttachmentsByClassIdAsync(lessonId);
        return Ok(attachments);
    }

    /// <summary>
    /// Retrieves detailed information about a specific attachment
    /// </summary>
    /// <param name="attachmentId">The unique identifier of the attachment</param>
    /// <returns>Detailed information about the requested attachment</returns>
    /// <response code="200">Returns the attachment details</response>
    /// <response code="404">If the attachment doesn't exist</response>
    /// <response code="401">If the user is not authenticated</response>
    [HttpGet("{attachmentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AttachmentDetailsDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AttachmentDetailsDto>> GetAttachmentById([FromRoute] Guid attachmentId)
    {
        var attachment = await _attachmentService.GetByIdAsync(attachmentId);
        return Ok(attachment);
    }

    /// <summary>
    /// Creates a new attachment for a lesson
    /// </summary>
    /// <param name="createAttachmentDto">Data transfer object containing attachment information</param>
    /// <returns>The unique identifier of the newly created attachment</returns>
    /// <remarks>
    /// The attachment will be associated with the lesson specified in the route.
    /// </remarks>
    /// <response code="200">Returns the ID of the created attachment</response>
    /// <response code="400">If the request data is invalid</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="404">If the associated lesson doesn't exist</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Guid>> CreateAttachment([FromBody] CreateAttachmentDto createAttachmentDto)
    {
        var attachmentId = await _attachmentService.CreateAsync(createAttachmentDto);
        return Ok(attachmentId);
    }

    /// <summary>
    /// Updates an existing attachment
    /// </summary>
    /// <param name="attachmentId">The unique identifier of the attachment to update</param>
    /// <param name="dto">Data transfer object containing the updated attachment information</param>
    /// <returns>No content if the update is successful</returns>
    /// <response code="204">If the attachment was successfully updated</response>
    /// <response code="400">If the request data is invalid</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="404">If the attachment doesn't exist</response>
    [HttpPut("{attachmentId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual async Task<IActionResult> Update([FromRoute] Guid attachmentId, [FromBody] CreateAttachmentDto dto)
    {
        await _attachmentService.UpdateAsync(attachmentId, dto);
        return NoContent();
    }

    /// <summary>
    /// Deletes a specific attachment
    /// </summary>
    /// <param name="attachmentId">The unique identifier of the attachment to delete</param>
    /// <returns>No content if the deletion is successful</returns>
    /// <response code="204">If the attachment was successfully deleted</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="404">If the attachment doesn't exist</response>
    [HttpDelete("{attachmentId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual async Task<IActionResult> Delete([FromRoute] Guid attachmentId)
    {
        await _attachmentService.DeleteAsync(attachmentId);
        return NoContent();
    }
}
