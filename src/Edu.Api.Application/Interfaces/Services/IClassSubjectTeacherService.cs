using Edu.Api.Application.DTOs.ClassSubjectTeacher;

namespace Edu.Api.Application.Interfaces.Services;

/// <summary>
/// Service interface for managing relationships between classes, subjects, and teachers
/// </summary>
public interface IClassSubjectTeacherService : IBaseService<CreateClassSubjectTeacherDto, ClassSubjectTeacherDetailsDto, ClassSubjectTeacherDto, Guid>
{
}