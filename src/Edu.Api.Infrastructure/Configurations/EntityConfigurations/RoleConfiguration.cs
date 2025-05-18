using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Infrastructure.Configurations.EntityConfigurations;

/// <summary>
/// Configuration class for the Role entity
/// </summary>
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    /// <summary>
    /// Configures the entity mapping for Role
    /// </summary>
    /// <param name="builder">The entity type builder</param>
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("ROLES", "users");
        builder.HasKey(r => r.Id);
    }
}