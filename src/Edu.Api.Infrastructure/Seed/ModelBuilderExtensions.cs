using Microsoft.EntityFrameworkCore;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Infrastructure.Seed;

/// <summary>
/// Extensions for seeding initial data into the database
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// Seeds initial data into the database model
    /// </summary>
    /// <param name="modelBuilder">The model builder</param>
    public static void SeedInitialData(this ModelBuilder modelBuilder)
    {
        modelBuilder.SeedRoles();
        modelBuilder.SeedGradeTypes();
        modelBuilder.SeedUsers();
        modelBuilder.SeedSubjects();
    }

    /// <summary>
    /// Seeds predefined roles into the database
    /// </summary>
    /// <param name="modelBuilder">The model builder</param>
    private static void SeedRoles(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = 1,
                Name = "Administrator",
                Description = "System administrator with full access"
            },
            new Role
            {
                Id = 2,
                Name = "Teacher",
                Description = "Educational staff responsible for teaching"
            },
            new Role
            {
                Id = 3,
                Name = "Student",
                Description = "Primary user of the educational platform"
            },
            new Role
            {
                Id = 4,
                Name = "Parent",
                Description = "Guardian or parent of a student"
            }
        );
    }

    /// <summary>
    /// Seeds predefined grade types into the database
    /// </summary>
    /// <param name="modelBuilder">The model builder</param>
    private static void SeedGradeTypes(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GradeType>().HasData(
            new GradeType
            {
                Id = 1,
                Name = "Current",
                Weight = 0.5f,
                Description = "Regular lesson grade"
            },
            new GradeType
            {
                Id = 2,
                Name = "Control",
                Weight = 1.0f,
                Description = "Comprehensive assessment or test"
            }
        );
    }

    /// <summary>
    /// Seeds sample users into the database
    /// </summary>
    /// <param name="modelBuilder">The model builder</param>
    private static void SeedUsers(this ModelBuilder modelBuilder)
    {
        // Admin
        string passwordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!");

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.NewGuid(),
                Email = "admin@eduportal.com",
                PasswordHash = passwordHash,
                FirstName = "System",
                LastName = "Administrator",
                RoleId = 1,
                PhoneNumber = null,
                LastLogin = null,
                IsActive = true
            }
        );

        // Teachers
        var teacherPassword = BCrypt.Net.BCrypt.HashPassword("Teacher123!");

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.NewGuid(),
                Email = "math.teacher@eduportal.com",
                PasswordHash = teacherPassword,
                FirstName = "John",
                LastName = "Smith",
                RoleId = 2,
                PhoneNumber = "+1234567890",
                LastLogin = null,
                IsActive = true
            },
            new User
            {
                Id = Guid.NewGuid(),
                Email = "physics.teacher@eduportal.com",
                PasswordHash = teacherPassword,
                FirstName = "Emma",
                LastName = "Johnson",
                RoleId = 2,
                PhoneNumber = "+1234567891",
                LastLogin = null,
                IsActive = true
            },
            new User
            {
                Id = Guid.NewGuid(),
                Email = "chemistry.teacher@eduportal.com",
                PasswordHash = teacherPassword,
                FirstName = "Michael",
                LastName = "Brown",
                RoleId = 2,
                PhoneNumber = "+1234567892",
                LastLogin = null,
                IsActive = true
            },
            new User
            {
                Id = Guid.NewGuid(),
                Email = "biology.teacher@eduportal.com",
                PasswordHash = teacherPassword,
                FirstName = "Olivia",
                LastName = "Davis",
                RoleId = 2,
                PhoneNumber = "+1234567893",
                LastLogin = null,
                IsActive = true
            },
            new User
            {
                Id = Guid.NewGuid(),
                Email = "english.teacher@eduportal.com",
                PasswordHash = teacherPassword,
                FirstName = "James",
                LastName = "Wilson",
                RoleId = 2,
                PhoneNumber = "+1234567894",
                LastLogin = null,
                IsActive = true
            }
        );
    }

    /// <summary>
    /// Seeds predefined subjects into the database
    /// </summary>
    /// <param name="modelBuilder">The model builder</param>
    private static void SeedSubjects(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subject>().HasData(
            new Subject
            {
                Id = Guid.NewGuid(),
                Name = "Mathematics",
                Description = "Study of numbers, quantities, and shapes",
                IsActive = true
            },
            new Subject
            {
                Id = Guid.NewGuid(),
                Name = "Physics",
                Description = "Study of matter, energy, and the interaction between them",
                IsActive = true
            },
            new Subject
            {
                Id = Guid.NewGuid(),
                Name = "Chemistry",
                Description = "Study of substances, their properties, structure, and the changes they undergo",
                IsActive = true
            },
            new Subject
            {
                Id = Guid.NewGuid(),
                Name = "Biology",
                Description = "Study of living organisms and their interactions with each other and the environment",
                IsActive = true
            },
            new Subject
            {
                Id = Guid.NewGuid(),
                Name = "English",
                Description = "Study of English language and literature",
                IsActive = true
            }
        );
    }
}