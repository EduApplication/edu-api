using Edu.Api.Application.DTOs.Document;

namespace Edu.Api.Application.Interfaces.Services;

/// <summary>
/// Service interface for managing documents in the educational system
/// </summary>
public interface IDocumentService
{
    /// <summary>
    /// Retrieves all documents associated with a specific attachment
    /// </summary>
    /// <param name="attachmentId">The unique identifier of the attachment</param>
    /// <returns>A collection of document DTOs for the specified attachment</returns>
    Task<IEnumerable<DocumentDto>> GetDocumentsByAttachmentId(Guid attachmentId);

    /// <summary>
    /// Retrieves detailed information about a specific document
    /// </summary>
    /// <param name="documentId">The unique identifier of the document</param>
    /// <returns>Detailed representation of the requested document</returns>
    Task<DocumentDetailsDto> GetDocumentById(Guid documentId);

    /// <summary>
    /// Downloads a document's file content
    /// </summary>
    /// <param name="id">The unique identifier of the document</param>
    /// <returns>A DocumentFile object containing the file stream, content type, and filename</returns>
    Task<DocumentFile> DownloadDocument(Guid id);

    /// <summary>
    /// Uploads a new document and associates it with an attachment
    /// </summary>
    /// <param name="fileStream">The content stream of the file being uploaded</param>
    /// <param name="fileName">The original name of the file</param>
    /// <param name="contentType">The MIME type of the file content</param>
    /// <param name="attachmentId">The unique identifier of the attachment to associate with the document</param>
    /// <returns>The identifier of the newly created document</returns>
    Task<Guid> UploadDocument(Stream fileStream, string fileName, string contentType, Guid attachmentId);

    /// <summary>
    /// Deletes a document
    /// </summary>
    /// <param name="documentId">The unique identifier of the document to delete</param>
    Task DeleteDocument(Guid documentId);
}