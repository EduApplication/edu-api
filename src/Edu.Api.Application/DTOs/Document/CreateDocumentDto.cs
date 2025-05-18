namespace Edu.Api.Application.DTOs.Document;

/// <summary>
/// Data transfer object for creating a new document attached to a specific attachment
/// </summary>
public class CreateDocumentDto : CreateDocumentRequest
{
    /// <summary>
    /// Identifier of the attachment to which this document will be associated
    /// </summary>
    public Guid AttachmentId { get; set; }
}