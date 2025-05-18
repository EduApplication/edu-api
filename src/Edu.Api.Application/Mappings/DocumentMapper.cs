using Edu.Api.Application.DTOs.Document;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Mappings;

/// <summary>
/// Mapper for converting between Document entities and DTOs
/// </summary>
public class DocumentMapper : IMapper<Document, DocumentDto, DocumentDetailsDto, CreateDocumentDto, Guid>
{
    /// <summary>
    /// Maps a Document entity to a basic DocumentDto
    /// </summary>
    /// <param name="entity">The source Document entity</param>
    /// <returns>A DTO with basic document information</returns>
    public DocumentDto MapToDto(Document entity)
    {
        return new DocumentDto
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    /// <summary>
    /// Maps a Document entity to a detailed DocumentDetailsDto
    /// </summary>
    /// <param name="entity">The source Document entity</param>
    /// <returns>A DTO with detailed document information including content type and attachment relationship</returns>
    public DocumentDetailsDto MapToDetailsDto(Document entity)
    {
        return new DocumentDetailsDto
        {
            Id = entity.Id,
            Name = entity.Name,
            ContentType = entity.ContentType,
            AttachmentId = entity.AttachmentId
        };
    }

    /// <summary>
    /// Maps a CreateDocumentDto to a new Document entity
    /// </summary>
    /// <param name="dto">The source DTO with document creation data</param>
    /// <returns>A new Document entity</returns>
    public Document MapToEntity(CreateDocumentDto dto)
    {
        return new Document
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            ContentType = dto.ContentType,
            ExternalId = dto.ExternalId,
            AttachmentId = dto.AttachmentId
        };
    }

    /// <summary>
    /// Updates an existing Document entity with data from a CreateDocumentDto
    /// </summary>
    /// <param name="entity">The Document entity to update</param>
    /// <param name="dto">The DTO containing updated values</param>
    public void UpdateEntityFromDto(Document entity, CreateDocumentDto dto)
    {
        entity.Name = dto.Name;
        entity.ContentType = dto.ContentType;
        if (!string.IsNullOrEmpty(dto.ExternalId))
        {
            entity.ExternalId = dto.ExternalId;
        }
    }
}