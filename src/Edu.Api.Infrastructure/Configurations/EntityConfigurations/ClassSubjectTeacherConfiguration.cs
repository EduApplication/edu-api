using Edu.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edu.Api.Infrastructure.Configurations.EntityConfigurations;

/// <summary>
/// Configuration class for the ClassSubjectTeacher entity
/// </summary>

public class ClassSubjectTeacherConfiguration : IEntityTypeConfiguration<ClassSubjectTeacher>
{
    /// <summary>
    /// Configures the entity mapping for ClassSubjectTeacher
    /// </summary>
    /// <param name="builder">The entity type builder</param>
    public void Configure(EntityTypeBuilder<ClassSubjectTeacher> builder)
    {
        builder.ToTable("CLASSES_SUBJECTS_TEACHERS", "main");
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Class)
               .WithMany(c => c.ClassSubjectTeacher)
               .HasForeignKey(x => x.ClassId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.TeacherSubject)
               .WithMany(s => s.ClassSubjectTeacher)
               .HasForeignKey(x => x.TeacherSubjectId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
