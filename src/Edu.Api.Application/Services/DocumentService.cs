using Microsoft.Extensions.Logging;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Application.Interfaces.Services;
using Edu.Api.Domain.Entities;
using Edu.Api.Application.DTOs.Document;
using Edu.Api.Application.Mappings;
using Edu.Api.Domain.Interfaces;

namespace Edu.Api.Application.Services;

/// <summary>
/// Service for managing document entities and file operations
/// </summary>
public class DocumentService(
    IDocumentRepository documentRepository,
    IFileStorageService fileStorageService,
    IMapper<Document, DocumentDto, DocumentDetailsDto, CreateDocumentDto, Guid> mapper,
    ILogger<DocumentService> logger) : IDocumentService
{
    private readonly IDocumentRepository _documentRepository = documentRepository;
    private readonly IFileStorageService _fileStorageService = fileStorageService;
    private readonly ILogger<DocumentService> _logger = logger;
    private readonly IMapper<Document, DocumentDto, DocumentDetailsDto, CreateDocumentDto, Guid> _mapper = mapper;

    /// <summary>
    /// Retrieves all documents associated with a specific attachment
    /// </summary>
    /// <param name="attachmentId">The attachment identifier</param>
    /// <returns>A collection of document DTOs</returns>
    public async Task<IEnumerable<DocumentDto>> GetDocumentsByAttachmentId(Guid attachmentId)
    {
        var documents = await _documentRepository.GetDocumentsByAttachmentIdAsync(attachmentId);
        return documents.Select(_mapper.MapToDto);
    }

    /// <summary>
    /// Retrieves a document by its identifier
    /// </summary>
    /// <param name="documentId">The document identifier</param>
    /// <returns>A detailed DTO representation of the document</returns>
    public async Task<DocumentDetailsDto> GetDocumentById(Guid documentId)
    {
        var document = await _documentRepository.GetByIdAsync(documentId);
        return document == null ? throw new ArgumentException($"Document with id {documentId} not found") : _mapper.MapToDetailsDto(document);
    }

    /// <summary>
    /// Downloads a document file by its identifier
    /// </summary>
    /// <param name="id">The document identifier</param>
    /// <returns>The document file with stream, content type and file name</returns>
    public async Task<DocumentFile> DownloadDocument(Guid id)
    {
        var document = await _documentRepository.GetByIdAsync(id);
        if (document == null || string.IsNullOrEmpty(document.ExternalId))
            throw new ArgumentException($"Document with id {id} not found");
        var fileStream = await _fileStorageService.GetFileAsync(document.ExternalId);
        return new DocumentFile
        {
            FileStream = fileStream,
            ContentType = document.ContentType ?? "application/octet-stream",
            FileName = document.Name
        };
    }

    /// <summary>
    /// Uploads a new document file and creates a corresponding document entity
    /// </summary>
    /// <param name="fileStream">The file content stream</param>
    /// <param name="fileName">The name of the file</param>
    /// <param name="contentType">The MIME content type of the file</param>
    /// <param name="attachmentId">The attachment identifier to associate with this document</param>
    /// <returns>The identifier of the created document</returns>
    public async Task<Guid> UploadDocument(Stream fileStream, string fileName, string contentType, Guid attachmentId)
    {
        try
        {
            string externalId = await _fileStorageService.SaveFileAsync(fileStream, fileName, contentType);
            var createDocumentDto = new CreateDocumentDto
            {
                Name = fileName,
                ContentType = contentType,
                ExternalId = externalId,
                AttachmentId = attachmentId
            };
            return await _documentRepository.AddAsync(_mapper.MapToEntity(createDocumentDto));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error uploading document: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Deletes a document and its associated file
    /// </summary>
    /// <param name="id">The identifier of the document to delete</param>
    public async Task DeleteDocument(Guid id)
    {
        var document = await _documentRepository.GetByIdAsync(id);
        if (document == null)
            return;
        if (!string.IsNullOrEmpty(document.ExternalId))
        {
            await _fileStorageService.DeleteFileAsync(document.ExternalId);
        }
        await _documentRepository.DeleteAsync(id);
    }
}