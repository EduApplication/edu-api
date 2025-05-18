using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Infrastructure.Configurations.EntityConfigurations;

/// <summary>
/// Configuration class for the ParentStudent entity
/// </summary>
public class ParentStudentConfiguration : IEntityTypeConfiguration<ParentStudent>
{
    /// <summary>
    /// Configures the entity mapping for ParentStudent
    /// </summary>
    /// <param name="builder">The entity type builder</param>
    public void Configure(EntityTypeBuilder<ParentStudent> builder)
    {
        builder.ToTable("PARENTS_STUDENTS", "users");
        builder.HasKey(ps => ps.Id);
        builder.Property(ps => ps.Id)
            .HasDefaultValueSql("NEWID()");
        builder.Property(ps => ps.RelationType).HasMaxLength(50);
    }
}