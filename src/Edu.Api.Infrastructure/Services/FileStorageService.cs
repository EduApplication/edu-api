using Edu.Api.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace Edu.Api.Infrastructure.Services;

/// <summary>
/// Service for managing file storage operations using MinIO
/// </summary>
public class FileStorageService : IFileStorageService
{
    private readonly IMinioClient _minioClient;
    private readonly string _bucketName;
    private readonly ILogger<FileStorageService> _logger;

    /// <summary>
    /// Initializes a new instance of the file storage service
    /// </summary>
    /// <param name="configuration">The application configuration</param>
    /// <param name="logger">The logger</param>
    public FileStorageService(IConfiguration configuration, ILogger<FileStorageService> logger)
    {
        _logger = logger;

        var endpoint = configuration["Minio:Endpoint"];
        var accessKey = configuration["Minio:AccessKey"];
        var secretKey = configuration["Minio:SecretKey"];
        var useSSL = bool.Parse(configuration["Minio:UseSSL"] ?? "false");
        _bucketName = configuration["Minio:BucketName"] ?? "";

        _minioClient = new MinioClient()
            .WithEndpoint(endpoint)
            .WithCredentials(accessKey, secretKey)
            .WithSSL(useSSL)
            .Build();
    }

    /// <summary>
    /// Retrieves a file from storage
    /// </summary>
    /// <param name="fileId">The unique identifier of the file</param>
    /// <returns>A stream containing the file data</returns>
    /// <exception cref="ApplicationException">Thrown when an error occurs during file retrieval</exception>
    public async Task<Stream> GetFileAsync(string fileId)
    {
        try
        {
            var memoryStream = new MemoryStream();

            var getObjectArgs = new GetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileId)
                .WithCallbackStream(stream =>
                {
                    stream.CopyTo(memoryStream);
                    memoryStream.Position = 0;
                });

            await _minioClient.GetObjectAsync(getObjectArgs);

            return memoryStream;
        }
        catch (MinioException ex)
        {
            _logger.LogError(ex, $"Error getting file from MinIO: {ex.Message}");
            throw new ApplicationException("Error retrieving file from storage", ex);
        }
    }

    /// <summary>
    /// Saves a new file to storage
    /// </summary>
    /// <param name="fileStream">The stream containing the file data</param>
    /// <param name="fileName">The name of the file</param>
    /// <param name="contentType">The MIME content type of the file</param>
    /// <returns>The unique identifier of the saved file</returns>
    /// <exception cref="ApplicationException">Thrown when an error occurs during file saving</exception>
    public async Task<string> SaveFileAsync(Stream fileStream, string fileName, string contentType)
    {
        try
        {
            string fileId = Guid.NewGuid().ToString();

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileId)
                .WithStreamData(fileStream)
                .WithObjectSize(fileStream.Length)
                .WithContentType(contentType);

            await _minioClient.PutObjectAsync(putObjectArgs);

            _logger.LogInformation($"File saved to MinIO: {fileId}");
            return fileId;
        }
        catch (MinioException ex)
        {
            _logger.LogError(ex, $"Error saving file to MinIO: {ex.Message}");
            throw new ApplicationException("Error saving file to storage", ex);
        }
    }

    /// <summary>
    /// Updates an existing file in storage
    /// </summary>
    /// <param name="fileId">The unique identifier of the file to update</param>
    /// <param name="fileStream">The stream containing the new file data</param>
    /// <param name="contentType">The MIME content type of the file</param>
    /// <returns>The unique identifier of the updated file</returns>
    /// <exception cref="ApplicationException">Thrown when an error occurs during file updating</exception>
    public async Task<string> UpdateFileAsync(string fileId, Stream fileStream, string contentType)
    {
        try
        {
            var statObjectArgs = new StatObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileId);

            try
            {
                await _minioClient.StatObjectAsync(statObjectArgs);
            }
            catch (MinioException)
            {
                _logger.LogWarning($"File {fileId} not found in storage, creating new one");
                return await SaveFileAsync(fileStream, fileId, contentType);
            }

            await DeleteFileAsync(fileId);

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileId)
                .WithStreamData(fileStream)
                .WithObjectSize(fileStream.Length)
                .WithContentType(contentType);

            await _minioClient.PutObjectAsync(putObjectArgs);

            _logger.LogInformation($"File updated in MinIO: {fileId}");
            return fileId;
        }
        catch (MinioException ex)
        {
            _logger.LogError(ex, $"Error updating file in MinIO: {ex.Message}");
            throw new ApplicationException("Error updating file in storage", ex);
        }
    }

    /// <summary>
    /// Deletes a file from storage
    /// </summary>
    /// <param name="fileId">The unique identifier of the file to delete</param>
    /// <exception cref="ApplicationException">Thrown when an error occurs during file deletion</exception>
    public async Task DeleteFileAsync(string fileId)
    {
        try
        {
            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileId);

            await _minioClient.RemoveObjectAsync(removeObjectArgs);

            _logger.LogInformation($"File deleted from MinIO: {fileId}");
        }
        catch (MinioException ex)
        {
            _logger.LogError(ex, $"Error deleting file from MinIO: {ex.Message}");
            throw new ApplicationException("Error deleting file from storage", ex);
        }
    }
}