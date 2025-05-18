namespace Edu.Api.Application.DTOs.Document;

/// <summary>
/// Base request data for document creation
/// </summary>
public class CreateDocumentRequest
{
    /// <summary>
    /// Name of the document to be created
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// MIME type of the document content (e.g., "application/pdf", "image/jpeg")
    /// </summary>
    public string? ContentType { get; set; }

    /// <summary>
    /// External identifier used to reference the document in the storage system
    /// </summary>
    public string? ExternalId { get; set; }
}
