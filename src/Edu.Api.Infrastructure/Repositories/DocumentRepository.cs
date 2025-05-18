using Microsoft.EntityFrameworkCore;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Domain.Entities;
using Edu.Api.Infrastructure.Data;

namespace Edu.Api.Infrastructure.Repositories;

/// <summary>
/// Repository for managing document entities
/// </summary>
public class DocumentRepository(AppDbContext context) : BaseRepository<Document, Guid>(context), IDocumentRepository
{
    /// <summary>
    /// Retrieves a document by its identifier with related attachment
    /// </summary>
    /// <param name="id">The document identifier</param>
    /// <returns>The document if found, otherwise null</returns>
    public override async Task<Document?> GetByIdAsync(Guid id)
    {
        return await _context.Documents
            .Include(d => d.Attachment)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    /// <summary>
    /// Retrieves documents for a specific attachment
    /// </summary>
    /// <param name="attachmentId">The attachment identifier</param>
    /// <returns>A collection of documents associated with the specified attachment</returns>
    public async Task<IEnumerable<Document>> GetDocumentsByAttachmentIdAsync(Guid attachmentId)
    {
        return await _context.Documents
            .Where(d => d.AttachmentId == attachmentId)
            .ToListAsync();
    }
}