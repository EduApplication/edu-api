using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Infrastructure.Configurations.EntityConfigurations;

/// <summary>
/// Configuration class for the Grade entity
/// </summary>
public class GradeConfiguration : IEntityTypeConfiguration<Grade>
{
    /// <summary>
    /// Configures the entity mapping for Grade
    /// </summary>
    /// <param name="builder">The entity type builder</param>
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.ToTable("GRADES", "main");
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id)
            .HasDefaultValueSql("NEWID()");
        builder.Property(g => g.Comment).HasMaxLength(500);

        builder.HasOne(g => g.Student)
            .WithMany()
            .HasForeignKey(g => g.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.Subject)
            .WithMany(s => s.Grades)
            .HasForeignKey(g => g.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.Teacher)
            .WithMany()
            .HasForeignKey(g => g.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.GradeType)
            .WithMany(gt => gt.Grades)
            .HasForeignKey(g => g.GradeTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}