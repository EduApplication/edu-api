using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Infrastructure.Configurations.EntityConfigurations;

/// <summary>
/// Configuration class for the User entity
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Configures the entity mapping for User
    /// </summary>
    /// <param name="builder">The entity type builder</param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("USERS", "users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasDefaultValueSql("NEWID()");
        builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("GETDATE()");
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(u => u.ParentRelationships)
            .WithOne(ps => ps.Parent)
            .HasForeignKey(ps => ps.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(u => u.StudentRelationships)
            .WithOne(ps => ps.Student)
            .HasForeignKey(ps => ps.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(u => u.TeacherSubjects)
            .WithOne(ts => ts.Teacher)
            .HasForeignKey(ts => ts.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(u => u.StudentClasses)
            .WithOne(sc => sc.Student)
            .HasForeignKey(sc => sc.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}