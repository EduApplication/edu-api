using Edu.Api.Domain.Interfaces;

namespace Edu.Api.Domain.Entities;

/// <summary>
/// Represents a document entity that is associated with an attachment
/// </summary>
/// <remarks>
/// Documents store file metadata and reference external storage locations for the actual file content
/// </remarks>
public class Document : IEntity<Guid>
{
    /// <summary>
    /// Gets or sets the unique identifier for the document
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the document
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the MIME content type of the document
    /// </summary>
    public string? ContentType { get; set; }

    /// <summary>
    /// Gets or sets the external storage identifier for the document
    /// </summary>
    /// <remarks>
    /// This is a reference to the actual file in an external storage system (e.g., file system path, S3 key, etc.)
    /// </remarks>
    public string? ExternalId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the attachment to which this document belongs
    /// </summary>
    public Guid AttachmentId { get; set; }

    /// <summary>
    /// Gets or sets the related attachment entity
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework</remarks>
    public Attachment Attachment { get; set; } = null!;
}