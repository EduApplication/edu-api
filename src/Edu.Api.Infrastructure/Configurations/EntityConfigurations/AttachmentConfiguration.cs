using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Infrastructure.Configurations.EntityConfigurations;

/// <summary>
/// Configuration class for the Attachment entity
/// </summary>

public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    /// <summary>
    /// Configures the entity mapping for Attachment
    /// </summary>
    /// <param name="builder">The entity type builder</param>
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("ATTACHMENTS", "main");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasDefaultValueSql("NEWID()");

        builder.Property(a => a.AssignedDate)
            .HasDefaultValueSql("GETDATE()");

        builder.Property(a => a.Title).IsRequired().HasMaxLength(200);
        builder.Property(a => a.Description).HasMaxLength(2000);

        builder.HasOne(a => a.Lesson)
            .WithMany(s => s.Attachments)
            .HasForeignKey(a => a.LessonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}