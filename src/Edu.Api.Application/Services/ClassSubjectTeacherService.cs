using Edu.Api.Application.DTOs.ClassSubjectTeacher;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Application.Interfaces.Services;
using Edu.Api.Application.Mappings;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Services;

/// <summary>
/// Service for managing class-subject-teacher relationships
/// </summary>
public class ClassSubjectTeacherService(
    IClassSubjectTeacherRepository classRepository,
    IMapper<ClassSubjectTeacher, ClassSubjectTeacherDto, ClassSubjectTeacherDetailsDto, CreateClassSubjectTeacherDto, Guid> classMapper) :
    BaseService<ClassSubjectTeacher, CreateClassSubjectTeacherDto, ClassSubjectTeacherDetailsDto, ClassSubjectTeacherDto, Guid>(classRepository, classMapper), IClassSubjectTeacherService
{
}