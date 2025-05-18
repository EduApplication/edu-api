using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Interfaces.Repositories;

/// <summary>
/// Repository interface for managing document entities
/// </summary>
public interface IDocumentRepository : IBaseRepository<Document, Guid>
{
    /// <summary>
    /// Retrieves all documents associated with a specific attachment
    /// </summary>
    /// <param name="attachmentId">The unique identifier of the attachment</param>
    /// <returns>A collection of documents linked to the specified attachment</returns>
    Task<IEnumerable<Document>> GetDocumentsByAttachmentIdAsync(Guid attachmentId);
}