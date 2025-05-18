using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Edu.Api.Application.DTOs.Grade;
using Edu.Api.Application.Interfaces.Services;

namespace Edu.Api.WebApi.Controllers;

/// <summary>
/// Controller for managing student grades
/// </summary>
/// <param name="gradeService">Service for handling grade operations</param>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GradesController(IGradeService gradeService) : ControllerBase
{
    private readonly IGradeService _gradeService = gradeService;

    /// <summary>
    /// Retrieves grades for the currently authenticated student
    /// </summary>
    /// <response code="200">Returns the student's grades grouped by subject</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user is not a student, parent, or administrator</response>
    [HttpGet("student")]
    [Authorize(Roles = "Student,Parent,Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StudentGradesBySubjectDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<StudentGradesBySubjectDto>>> GetStudentGrades()
    {
        var grades = await _gradeService.GetCurrentStudentGradesAsync();
        return Ok(grades);
    }

    /// <summary>
    /// Retrieves grades for a specific student
    /// </summary>
    /// <param name="studentId">The unique identifier of the student</param>
    /// <response code="200">Returns the student's grades grouped by subject</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user is not a teacher or administrator</response>
    /// <response code="404">If the student doesn't exist</response>
    [HttpGet("student/{studentId:guid}")]
    [Authorize(Roles = "Teacher,Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StudentGradesBySubjectDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<StudentGradesBySubjectDto>>> GetStudentGrades([FromRoute] Guid studentId)
    {
        var grades = await _gradeService.GetStudentGradesAsync(studentId);
        return Ok(grades);
    }

    /// <summary>
    /// Retrieves grades for a specific student in a specific subject
    /// </summary>
    /// <param name="studentId">The unique identifier of the student</param>
    /// <param name="subjectId">The unique identifier of the subject</param>
    /// <response code="200">Returns the student's grades for the specified subject</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user doesn't have permission to view these grades</response>
    /// <response code="404">If the student or subject doesn't exist</response>
    [HttpGet("student/{studentId:guid}/subject/{subjectId:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GradeDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<GradeDto>>> GetGradesBySubject([FromRoute] Guid studentId, [FromRoute] Guid subjectId)
    {
        try
        {
            var grades = await _gradeService.GetGradesBySubjectForStudentAsync(studentId, subjectId);
            return Ok(grades);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }

    /// <summary>
    /// Creates a new grade for a student
    /// </summary>
    /// <param name="gradeDto">The grade information to create</param>
    /// <response code="200">Returns the identifier of the created grade</response>
    /// <response code="400">If the grade data is invalid</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user doesn't have permission to create grades</response>
    /// <response code="404">If the referenced student, subject, or class doesn't exist</response>
    [HttpPost]
    [Authorize(Roles = "Teacher,Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Guid>> CreateGrade([FromBody] CreateGradeDto gradeDto)
    {
        try
        {
            var id = await _gradeService.CreateAsync(gradeDto);
            return Ok(id);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }
}