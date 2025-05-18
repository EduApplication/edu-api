using Edu.Api.Application.DTOs.ClassSubjectTeacher;
using Edu.Api.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edu.Api.WebApi.Controllers;

/// <summary>
/// Controller for managing class-subject-teacher relationships
/// </summary>
/// <param name="service">Service for handling class-subject-teacher operations</param>
[Route("api/class-subject-teacher")]
[ApiController]
[Authorize]
public class ClassSubjectTeacherController(IClassSubjectTeacherService service) : BaseController<CreateClassSubjectTeacherDto, ClassSubjectTeacherDetailsDto, ClassSubjectTeacherDto, Guid>(service)
{
}
