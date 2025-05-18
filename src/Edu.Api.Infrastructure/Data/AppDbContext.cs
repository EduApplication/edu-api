using Microsoft.EntityFrameworkCore;
using Edu.Api.Domain.Entities;
using Edu.Api.Infrastructure.Seed;

namespace Edu.Api.Infrastructure.Data;

/// <summary>
/// Application database context for Entity Framework Core
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Gets or sets the users in the database
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Gets or sets the roles in the database
    /// </summary>
    public DbSet<Role> Roles { get; set; }

    /// <summary>
    /// Gets or sets the classes in the database
    /// </summary>
    public DbSet<Class> Classes { get; set; }

    /// <summary>
    /// Gets or sets the subjects in the database
    /// </summary>
    public DbSet<Subject> Subjects { get; set; }

    /// <summary>
    /// Gets or sets the lessons in the database
    /// </summary>
    public DbSet<Lesson> Lessons { get; set; }

    /// <summary>
    /// Gets or sets the teacher-subject relationships in the database
    /// </summary>
    public DbSet<TeacherSubject> TeacherSubjects { get; set; }

    /// <summary>
    /// Gets or sets the student-class relationships in the database
    /// </summary>
    public DbSet<StudentClass> StudentClasses { get; set; }

    /// <summary>
    /// Gets or sets the parent-student relationships in the database
    /// </summary>
    public DbSet<ParentStudent> ParentStudents { get; set; }

    /// <summary>
    /// Gets or sets the grades in the database
    /// </summary>
    public DbSet<Grade> Grades { get; set; }

    /// <summary>
    /// Gets or sets the grade types in the database
    /// </summary>
    public DbSet<GradeType> GradeTypes { get; set; }

    /// <summary>
    /// Gets or sets the attachments in the database
    /// </summary>
    public DbSet<Attachment> Attachments { get; set; }

    /// <summary>
    /// Gets or sets the documents in the database
    /// </summary>
    public DbSet<Document> Documents { get; set; }

    /// <summary>
    /// Gets or sets the class-subject-teacher relationships in the database
    /// </summary>
    public DbSet<ClassSubjectTeacher> ClassSubjectTeacher { get; set; }

    /// <summary>
    /// Configures the model that was discovered by convention from the entity types
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.SeedInitialData();
    }
}