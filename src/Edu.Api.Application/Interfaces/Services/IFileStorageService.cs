namespace Edu.Api.Application.Interfaces.Services;

/// <summary>
/// Service interface for file storage operations
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Retrieves a file from storage by its identifier
    /// </summary>
    /// <param name="fileId">The unique identifier of the file in storage</param>
    /// <returns>A stream containing the file content</returns>
    Task<Stream> GetFileAsync(string fileId);

    /// <summary>
    /// Saves a new file to storage
    /// </summary>
    /// <param name="fileStream">The content stream of the file to save</param>
    /// <param name="fileName">The name of the file</param>
    /// <param name="contentType">The MIME type of the file content</param>
    /// <returns>The identifier of the saved file in storage</returns>
    Task<string> SaveFileAsync(Stream fileStream, string fileName, string contentType);

    /// <summary>
    /// Updates an existing file in storage
    /// </summary>
    /// <param name="fileId">The unique identifier of the file to update</param>
    /// <param name="fileStream">The new content stream for the file</param>
    /// <param name="contentType">The MIME type of the updated file content</param>
    /// <returns>The identifier of the updated file in storage (may be the same as the input fileId)</returns>
    Task<string> UpdateFileAsync(string fileId, Stream fileStream, string contentType);

    /// <summary>
    /// Deletes a file from storage
    /// </summary>
    /// <param name="fileId">The unique identifier of the file to delete</param>
    Task DeleteFileAsync(string fileId);
}