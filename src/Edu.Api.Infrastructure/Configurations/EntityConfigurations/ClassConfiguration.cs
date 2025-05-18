using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Infrastructure.Configurations.EntityConfigurations;

/// <summary>
/// Configuration class for the Class entity
/// </summary>
public class ClassConfiguration : IEntityTypeConfiguration<Class>
{
    /// <summary>
    /// Configures the entity mapping for Class
    /// </summary>
    /// <param name="builder">The entity type builder</param>
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.ToTable("CLASSES", "main");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasDefaultValueSql("NEWID()");
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Year).IsRequired();
        builder.Property(c => c.Section).HasMaxLength(50);

        builder.HasOne(c => c.ClassTeacher)
            .WithMany(u => u.ClassesAsTutor)
            .HasForeignKey(c => c.ClassTeacherId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}