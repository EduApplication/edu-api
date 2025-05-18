using Edu.Api.Application.DTOs.Document;
using Edu.Api.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edu.Api.WebApi.Controllers;

/// <summary>
/// Controller for managing documents attached to lesson attachments
/// </summary>
/// <param name="documentService">Service for handling document operations</param>
[ApiController]
[Route("api/lessons/{lessonId:guid}/attachments/{attachmentId:guid}/documents")]
[Authorize]
public class DocumentsController(IDocumentService documentService) : ControllerBase
{
    private readonly IDocumentService _documentService = documentService;

    /// <summary>
    /// Retrieves all documents associated with a specific attachment
    /// </summary>
    /// <param name="attachmentId">The unique identifier of the attachment</param>
    /// <response code="200">Returns the list of documents</response>
    /// <response code="404">If the attachment doesn't exist</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DocumentDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<DocumentDto>>> GetDocumentsByAttachment([FromRoute] Guid attachmentId)
    {
        var documents = await _documentService.GetDocumentsByAttachmentId(attachmentId);
        return Ok(documents);
    }

    /// <summary>
    /// Retrieves a specific document by its identifier
    /// </summary>
    /// <param name="documentId">The unique identifier of the document</param>
    /// <response code="200">Returns the requested document details</response>
    /// <response code="404">If the document doesn't exist</response>
    [HttpGet("{documentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DocumentDetailsDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DocumentDetailsDto>> GetDocumentById([FromRoute] Guid documentId)
    {
        var document = await _documentService.GetDocumentById(documentId);
        return Ok(document);
    }

    /// <summary>
    /// Downloads a specific document
    /// </summary>
    /// <param name="documentId">The unique identifier of the document to download</param>
    /// <response code="200">Returns the file stream</response>
    /// <response code="404">If the document doesn't exist</response>
    [HttpGet("download/{documentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DownloadDocument([FromRoute] Guid documentId)
    {
        DocumentFile documentFile = await _documentService.DownloadDocument(documentId);
        return File(documentFile.FileStream, documentFile.ContentType, documentFile.FileName);
    }

    /// <summary>
    /// Uploads a new document and associates it with the specified attachment
    /// </summary>
    /// <param name="file">The file to upload</param>
    /// <param name="attachmentId">The unique identifier of the attachment</param>
    /// <response code="200">Returns the identifier of the uploaded document</response>
    /// <response code="400">If the file is null or empty</response>
    /// <response code="404">If the attachment doesn't exist</response>
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Guid>> UploadDocument(IFormFile file, [FromRoute] Guid attachmentId)
    {
        using var stream = file.OpenReadStream();
        var documentId = await _documentService.UploadDocument(stream, file.FileName, file.ContentType, attachmentId);
        return Ok(documentId);
    }

    /// <summary>
    /// Deletes a specific document
    /// </summary>
    /// <param name="documentId">The unique identifier of the document to delete</param>
    /// <response code="204">If the document was successfully deleted</response>
    /// <response code="404">If the document doesn't exist</response>
    [HttpDelete("{documentId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDocument([FromRoute] Guid documentId)
    {
        await _documentService.DeleteDocument(documentId);
        return NoContent();
    }
}