using Edu.Api.Domain.Interfaces;

namespace Edu.Api.Domain.Entities;

/// <summary>
/// Represents an attachment entity that can be assigned to a lesson
/// </summary>
public class Attachment : IEntity<Guid>
{
    /// <summary>
    /// Gets or sets the unique identifier for the attachment
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the title of the attachment
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the attachment
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the lesson identifier to which this attachment belongs
    /// </summary>
    public Guid LessonId { get; set; }

    /// <summary>
    /// Gets or sets the due date for the attachment
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Gets or sets the date when the attachment was assigned
    /// </summary>
    public DateTime AssignedDate { get; set; }

    /// <summary>
    /// Gets or sets the related lesson entity
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework</remarks>
    public Lesson Lesson { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of documents associated with this attachment
    /// </summary>
    /// <remarks>This is a navigation property for Entity Framework</remarks>
    public ICollection<Document> Documents { get; set; } = [];
}