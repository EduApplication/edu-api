using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Infrastructure.Configurations.EntityConfigurations;

/// <summary>
/// Configuration class for the GradeType entity
/// </summary>
public class GradeTypeConfiguration : IEntityTypeConfiguration<GradeType>
{
    /// <summary>
    /// Configures the entity mapping for GradeType
    /// </summary>
    /// <param name="builder">The entity type builder</param>
    public void Configure(EntityTypeBuilder<GradeType> builder)
    {
        builder.ToTable("GRADE_TYPES", "main");
        builder.HasKey(gt => gt.Id);
        builder.Property(gt => gt.Name).IsRequired().HasMaxLength(50);
        builder.Property(gt => gt.Description).HasMaxLength(200);
    }
}