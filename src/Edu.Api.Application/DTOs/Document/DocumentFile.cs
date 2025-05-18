namespace Edu.Api.Application.DTOs.Document;

/// <summary>
/// Container for document file data used when downloading or processing document content
/// </summary>
public class DocumentFile
{
    /// <summary>
    /// Stream containing the binary content of the document file
    /// </summary>
    public Stream FileStream { get; set; } = null!;

    /// <summary>
    /// MIME type of the document content (e.g., "application/pdf", "image/jpeg")
    /// </summary>
    public string ContentType { get; set; } = null!;

    /// <summary>
    /// Original file name of the document, used when downloading
    /// </summary>
    public string? FileName { get; set; }
}