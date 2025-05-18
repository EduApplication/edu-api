namespace Edu.Api.Application.DTOs.Document;

/// <summary>
/// Basic representation of a document for list views
/// </summary>
public class DocumentDto
{
    /// <summary>
    /// Unique identifier of the document
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the document
    /// </summary>
    public string? Name { get; set; }
}