using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Infrastructure.Configurations.EntityConfigurations;

/// <summary>
/// Configuration class for the Lesson entity
/// </summary>
public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    /// <summary>
    /// Configures the entity mapping for Lesson
    /// </summary>
    /// <param name="builder">The entity type builder</param>
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable("LESSONS", "main");
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id)
            .HasDefaultValueSql("NEWID()");
        builder.Property(l => l.Room).HasMaxLength(50);
        builder.Property(l => l.Topic).HasMaxLength(200);
        builder.HasOne(x => x.ClassSubjectTeacher)
            .WithMany(s => s.Lessons)
            .HasForeignKey(x => x.ClassSubjectTeacherId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}