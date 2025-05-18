using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Edu.Api.Application.DTOs.Class;
using Edu.Api.Application.Interfaces.Services;

namespace Edu.Api.WebApi.Controllers;

/// <summary>
/// Controller for managing classes in the educational system
/// </summary>
/// <param name="classService">Service for handling class operations</param>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ClassesController(IClassService classService) : BaseController<CreateClassDto, ClassDetailsDto, ClassDto, Guid>(classService)
{
    private readonly IClassService _classService = classService;

    /// <summary>
    /// Retrieves a specific class by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the class</param>
    /// <response code="200">Returns the requested class</response>
    /// <response code="403">If the user is not authorized to access this class</response>
    /// <response code="404">If the class doesn't exist</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClassDetailsDto))]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public override async Task<ActionResult<ClassDetailsDto>> GetById([FromRoute] Guid id)
    {
        try
        {
            var classDto = await _classService.GetByIdAsync(id);
            return Ok(classDto);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves all classes where the current user is a teacher
    /// </summary>
    /// <response code="200">Returns the list of classes</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user is not a teacher or administrator</response>
    [HttpGet("teacher")]
    [Authorize(Roles = "Teacher,Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClassDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<ClassDto>>> GetTeacherClasses()
    {
        var classes = await _classService.GetTeacherClassesAsync();
        return Ok(classes);
    }

    /// <summary>
    /// Retrieves all classes where the current user is a student
    /// </summary>
    /// <response code="200">Returns the list of classes</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user is not a student or administrator</response>
    [HttpGet("student")]
    [Authorize(Roles = "Student,Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClassDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<ClassDto>>> GetStudentClasses()
    {
        var classes = await _classService.GetStudentClassesAsync();
        return Ok(classes);
    }

    /// <summary>
    /// Retrieves all classes where the current user is a parent of enrolled students
    /// </summary>
    /// <response code="200">Returns the list of classes</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user is not a parent or administrator</response>
    [HttpGet("parent")]
    [Authorize(Roles = "Parent,Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClassDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<ClassDto>>> GetParentClasses()
    {
        var classes = await _classService.GetParentClassesAsync();
        return Ok(classes);
    }

    /// <summary>
    /// Adds a student to a specific class
    /// </summary>
    /// <param name="classId">The unique identifier of the class</param>
    /// <param name="studentId">The unique identifier of the student</param>
    /// <response code="201">Returns the identifier of the class enrollment</response>
    /// <response code="400">If the request data is invalid</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user is not an administrator</response>
    /// <response code="404">If the class or student doesn't exist</response>
    [HttpPost("{classId:guid}/student/{studentId:guid}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Guid>> AddStudentToClass([FromRoute] Guid classId, [FromRoute] Guid studentId)
    {
        var id = await _classService.AddStudentToClass(classId, studentId);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }
}