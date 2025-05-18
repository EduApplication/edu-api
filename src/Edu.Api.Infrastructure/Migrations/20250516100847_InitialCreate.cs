using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Edu.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "main");

            migrationBuilder.EnsureSchema(
                name: "users");

            migrationBuilder.CreateTable(
                name: "GRADE_TYPES",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRADE_TYPES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ROLES",
                schema: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SUBJECTS",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SUBJECTS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                schema: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USERS_ROLES_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "users",
                        principalTable: "ROLES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CLASSES",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Section = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ClassTeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLASSES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CLASSES_USERS_ClassTeacherId",
                        column: x => x.ClassTeacherId,
                        principalSchema: "users",
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GRADES",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GradeTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRADES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GRADES_GRADE_TYPES_GradeTypeId",
                        column: x => x.GradeTypeId,
                        principalSchema: "main",
                        principalTable: "GRADE_TYPES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GRADES_SUBJECTS_SubjectId",
                        column: x => x.SubjectId,
                        principalSchema: "main",
                        principalTable: "SUBJECTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GRADES_USERS_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "users",
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GRADES_USERS_TeacherId",
                        column: x => x.TeacherId,
                        principalSchema: "users",
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PARENTS_STUDENTS",
                schema: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PARENTS_STUDENTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PARENTS_STUDENTS_USERS_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "users",
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PARENTS_STUDENTS_USERS_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "users",
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TEACHERS_SUBJECTS",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEACHERS_SUBJECTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TEACHERS_SUBJECTS_SUBJECTS_SubjectId",
                        column: x => x.SubjectId,
                        principalSchema: "main",
                        principalTable: "SUBJECTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TEACHERS_SUBJECTS_USERS_TeacherId",
                        column: x => x.TeacherId,
                        principalSchema: "users",
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "STUDENTS_CLASSES",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDENTS_CLASSES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STUDENTS_CLASSES_CLASSES_ClassId",
                        column: x => x.ClassId,
                        principalSchema: "main",
                        principalTable: "CLASSES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STUDENTS_CLASSES_USERS_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "users",
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CLASSES_SUBJECTS_TEACHERS",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherSubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLASSES_SUBJECTS_TEACHERS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CLASSES_SUBJECTS_TEACHERS_CLASSES_ClassId",
                        column: x => x.ClassId,
                        principalSchema: "main",
                        principalTable: "CLASSES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CLASSES_SUBJECTS_TEACHERS_TEACHERS_SUBJECTS_TeacherSubjectId",
                        column: x => x.TeacherSubjectId,
                        principalSchema: "main",
                        principalTable: "TEACHERS_SUBJECTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LESSONS",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ClassSubjectTeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Room = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Topic = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LESSONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LESSONS_CLASSES_SUBJECTS_TEACHERS_ClassSubjectTeacherId",
                        column: x => x.ClassSubjectTeacherId,
                        principalSchema: "main",
                        principalTable: "CLASSES_SUBJECTS_TEACHERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ATTACHMENTS",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    LessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATTACHMENTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ATTACHMENTS_LESSONS_LessonId",
                        column: x => x.LessonId,
                        principalSchema: "main",
                        principalTable: "LESSONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DOCUMENTS",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExternalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOCUMENTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DOCUMENTS_ATTACHMENTS_AttachmentId",
                        column: x => x.AttachmentId,
                        principalSchema: "main",
                        principalTable: "ATTACHMENTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "main",
                table: "GRADE_TYPES",
                columns: new[] { "Id", "Description", "Name", "Weight" },
                values: new object[,]
                {
                    { 1, "Regular lesson grade", "Current", 0.5f },
                    { 2, "Comprehensive assessment or test", "Control", 1f }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "ROLES",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "System administrator with full access", "Administrator" },
                    { 2, "Educational staff responsible for teaching", "Teacher" },
                    { 3, "Primary user of the educational platform", "Student" },
                    { 4, "Guardian or parent of a student", "Parent" }
                });

            migrationBuilder.InsertData(
                schema: "main",
                table: "SUBJECTS",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("06bc44e2-d0d5-419e-8a91-3d56c098127e"), "Study of numbers, quantities, and shapes", true, "Mathematics" },
                    { new Guid("2dd4aa9b-3023-42f6-91d4-373fc9e0b3d7"), "Study of substances, their properties, structure, and the changes they undergo", true, "Chemistry" },
                    { new Guid("8126aa6e-14d8-4e13-b198-4d46d052c92d"), "Study of English language and literature", true, "English" },
                    { new Guid("818472a6-c30e-462b-80b1-5abd20c01798"), "Study of matter, energy, and the interaction between them", true, "Physics" },
                    { new Guid("eee7445a-246a-4178-b27a-85045f5d46af"), "Study of living organisms and their interactions with each other and the environment", true, "Biology" }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "USERS",
                columns: new[] { "Id", "Email", "FirstName", "IsActive", "LastLogin", "LastName", "PasswordHash", "PhoneNumber", "RoleId" },
                values: new object[,]
                {
                    { new Guid("10fce7fb-edd8-4f29-899a-0ac145021f9a"), "english.teacher@eduportal.com", "James", true, null, "Wilson", "$2a$11$zeMb2CTgKY.KDZjfCVgx0OfQYWj9kHq1FYjZt0/TKj3bHke0qT3qS", "+1234567894", 2 },
                    { new Guid("2c90cdcd-ab04-475d-aa89-c68eeb8a77a9"), "math.teacher@eduportal.com", "John", true, null, "Smith", "$2a$11$zeMb2CTgKY.KDZjfCVgx0OfQYWj9kHq1FYjZt0/TKj3bHke0qT3qS", "+1234567890", 2 },
                    { new Guid("389f8a93-b46c-47ef-b27c-1a85dd4183b0"), "biology.teacher@eduportal.com", "Olivia", true, null, "Davis", "$2a$11$zeMb2CTgKY.KDZjfCVgx0OfQYWj9kHq1FYjZt0/TKj3bHke0qT3qS", "+1234567893", 2 },
                    { new Guid("4d59cade-9f03-4f86-ae69-2971c81f1677"), "physics.teacher@eduportal.com", "Emma", true, null, "Johnson", "$2a$11$zeMb2CTgKY.KDZjfCVgx0OfQYWj9kHq1FYjZt0/TKj3bHke0qT3qS", "+1234567891", 2 },
                    { new Guid("99e4fd1f-c01f-4c27-8944-867dc176beef"), "admin@eduportal.com", "System", true, null, "Administrator", "$2a$11$Hwe0Mu32xSVmK0mLXjtUJ.lS7Iuf7QAwwO8bR4QkmrMAWmc4Olj/y", null, 1 },
                    { new Guid("b7831597-76b2-426a-99ab-e7679c82f98e"), "chemistry.teacher@eduportal.com", "Michael", true, null, "Brown", "$2a$11$zeMb2CTgKY.KDZjfCVgx0OfQYWj9kHq1FYjZt0/TKj3bHke0qT3qS", "+1234567892", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ATTACHMENTS_LessonId",
                schema: "main",
                table: "ATTACHMENTS",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_CLASSES_ClassTeacherId",
                schema: "main",
                table: "CLASSES",
                column: "ClassTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_CLASSES_SUBJECTS_TEACHERS_ClassId",
                schema: "main",
                table: "CLASSES_SUBJECTS_TEACHERS",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_CLASSES_SUBJECTS_TEACHERS_TeacherSubjectId",
                schema: "main",
                table: "CLASSES_SUBJECTS_TEACHERS",
                column: "TeacherSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DOCUMENTS_AttachmentId",
                schema: "main",
                table: "DOCUMENTS",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_GRADES_GradeTypeId",
                schema: "main",
                table: "GRADES",
                column: "GradeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GRADES_StudentId",
                schema: "main",
                table: "GRADES",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_GRADES_SubjectId",
                schema: "main",
                table: "GRADES",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_GRADES_TeacherId",
                schema: "main",
                table: "GRADES",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_LESSONS_ClassSubjectTeacherId",
                schema: "main",
                table: "LESSONS",
                column: "ClassSubjectTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_PARENTS_STUDENTS_ParentId",
                schema: "users",
                table: "PARENTS_STUDENTS",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PARENTS_STUDENTS_StudentId",
                schema: "users",
                table: "PARENTS_STUDENTS",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENTS_CLASSES_ClassId",
                schema: "main",
                table: "STUDENTS_CLASSES",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENTS_CLASSES_StudentId",
                schema: "main",
                table: "STUDENTS_CLASSES",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TEACHERS_SUBJECTS_SubjectId",
                schema: "main",
                table: "TEACHERS_SUBJECTS",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TEACHERS_SUBJECTS_TeacherId",
                schema: "main",
                table: "TEACHERS_SUBJECTS",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_Email",
                schema: "users",
                table: "USERS",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USERS_RoleId",
                schema: "users",
                table: "USERS",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DOCUMENTS",
                schema: "main");

            migrationBuilder.DropTable(
                name: "GRADES",
                schema: "main");

            migrationBuilder.DropTable(
                name: "PARENTS_STUDENTS",
                schema: "users");

            migrationBuilder.DropTable(
                name: "STUDENTS_CLASSES",
                schema: "main");

            migrationBuilder.DropTable(
                name: "ATTACHMENTS",
                schema: "main");

            migrationBuilder.DropTable(
                name: "GRADE_TYPES",
                schema: "main");

            migrationBuilder.DropTable(
                name: "LESSONS",
                schema: "main");

            migrationBuilder.DropTable(
                name: "CLASSES_SUBJECTS_TEACHERS",
                schema: "main");

            migrationBuilder.DropTable(
                name: "CLASSES",
                schema: "main");

            migrationBuilder.DropTable(
                name: "TEACHERS_SUBJECTS",
                schema: "main");

            migrationBuilder.DropTable(
                name: "SUBJECTS",
                schema: "main");

            migrationBuilder.DropTable(
                name: "USERS",
                schema: "users");

            migrationBuilder.DropTable(
                name: "ROLES",
                schema: "users");
        }
    }
}
