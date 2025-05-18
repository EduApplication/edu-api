using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Infrastructure.Configurations.EntityConfigurations;

/// <summary>
/// Configuration class for the TeacherSubject entity
/// </summary>
public class TeacherSubjectConfiguration : IEntityTypeConfiguration<TeacherSubject>
{
    /// <summary>
    /// Configures the entity mapping for TeacherSubject
    /// </summary>
    /// <param name="builder">The entity type builder</param>
    public void Configure(EntityTypeBuilder<TeacherSubject> builder)
    {
        builder.ToTable("TEACHERS_SUBJECTS", "main");
        builder.HasKey(ts => ts.Id);
        builder.Property(ts => ts.Id)
            .HasDefaultValueSql("NEWID()");
        builder.HasOne(ts => ts.Teacher)
            .WithMany(u => u.TeacherSubjects)
            .HasForeignKey(ts => ts.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(ts => ts.Subject)
            .WithMany(s => s.TeacherSubjects)
            .HasForeignKey(ts => ts.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}