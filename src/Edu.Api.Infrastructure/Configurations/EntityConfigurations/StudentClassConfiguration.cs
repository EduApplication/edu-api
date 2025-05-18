using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Infrastructure.Configurations.EntityConfigurations;

/// <summary>
/// Configuration class for the StudentClass entity
/// </summary>
public class StudentClassConfiguration : IEntityTypeConfiguration<StudentClass>
{
    /// <summary>
    /// Configures the entity mapping for StudentClass
    /// </summary>
    /// <param name="builder">The entity type builder</param>
    public void Configure(EntityTypeBuilder<StudentClass> builder)
    {
        builder.ToTable("STUDENTS_CLASSES", "main");
        builder.HasKey(sc => sc.Id);
        builder.Property(sc => sc.Id)
            .HasDefaultValueSql("NEWID()");
        builder.HasOne(sc => sc.Student)
            .WithMany(u => u.StudentClasses)
            .HasForeignKey(sc => sc.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(sc => sc.Class)
            .WithMany(c => c.StudentClasses)
            .HasForeignKey(sc => sc.ClassId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}