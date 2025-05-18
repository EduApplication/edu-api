using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Edu.Api.Application.DTOs.Subject;
using Edu.Api.Application.Interfaces.Services;
using Edu.Api.Application.DTOs.User;

namespace Edu.Api.WebApi.Controllers;

/// <summary>
/// Controller for managing subjects in the educational system
/// </summary>
/// <param name="subjectService">Service for handling subject operations</param>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SubjectsController(ISubjectService subjectService) : BaseController<CreateSubjectDto, SubjectDetailsDto, SubjectDto, Guid>(subjectService)
{
    private readonly ISubjectService _subjectService = subjectService;


    /// <summary>
    /// Retrieves subjects taught by the current teacher
    /// </summary>
    /// <response code="200">Returns the list of subjects taught by the current teacher</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user is not a teacher or administrator</response>
    [HttpGet("teacher")]
    [Authorize(Roles = "Administrator,Teacher")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SubjectDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<SubjectDto>>> GetTeacherSubjects()
    {
        var subjects = await _subjectService.GetTeacherSubjectsAsync();
        return Ok(subjects);
    }

    /// <summary>
    /// Retrieves subjects that the current student is enrolled in
    /// </summary>
    /// <response code="200">Returns the list of subjects for the current student</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user is not a student or administrator</response>
    [HttpGet("student")]
    [Authorize(Roles = "Student,Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SubjectDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<SubjectDto>>> GetStudentSubjects()
    {
        var subjects = await _subjectService.GetStudentSubjectsAsync();
        return Ok(subjects);
    }


    /// <summary>
    /// Assigns a teacher to a subject
    /// </summary>
    /// <param name="subjectId">The unique identifier of the subject</param>
    /// <param name="teacherId">The unique identifier of the teacher</param>
    /// <response code="204">If the teacher was successfully assigned to the subject</response>
    /// <response code="400">If the teacher or subject data is invalid</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user is not an administrator</response>
    /// <response code="404">If the teacher or subject doesn't exist</response>
    [HttpPost("{subjectId:guid}/teachers/{teacherId:guid}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddTeacherToSubject([FromRoute] Guid subjectId, [FromRoute] Guid teacherId)
    {
        try
        {
            await _subjectService.AddTeacherToSubjectAsync(subjectId, teacherId);
            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Removes a teacher from a subject
    /// </summary>
    /// <param name="subjectId">The unique identifier of the subject</param>
    /// <param name="teacherId">The unique identifier of the teacher</param>
    /// <response code="204">If the teacher was successfully removed from the subject</response>
    /// <response code="400">If the teacher or subject data is invalid</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user is not an administrator</response>
    /// <response code="404">If the teacher-subject relationship doesn't exist</response>
    [HttpDelete("{subjectId:guid}/teachers/{teacherId:guid}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveTeacherFromSubject([FromRoute] Guid subjectId, [FromRoute] Guid teacherId)
    {
        try
        {
            await _subjectService.RemoveTeacherFromSubjectAsync(subjectId, teacherId);
            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}