namespace Edu.Api.Application.DTOs.Document;

/// <summary>
/// Detailed representation of a document with its properties and relationship information
/// </summary>
public class DocumentDetailsDto
{
    /// <summary>
    /// Unique identifier of the document
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the document
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// MIME type of the document content (e.g., "application/pdf", "image/jpeg")
    /// </summary>
    public string? ContentType { get; set; }

    /// <summary>
    /// Identifier of the attachment to which this document belongs
    /// </summary>
    public Guid AttachmentId { get; set; }
}