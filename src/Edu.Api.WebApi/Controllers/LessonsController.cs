using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Edu.Api.Application.DTOs.Lesson;
using Edu.Api.Application.Interfaces.Services;

namespace Edu.Api.WebApi.Controllers;

/// <summary>
/// Controller for managing lessons in the educational system
/// </summary>
/// <param name="lessonService">Service for handling lesson operations</param>
/// <param name="attachmentService">Service for handling attachment operations</param>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LessonsController(ILessonService lessonService, IAttachmentService attachmentService) : BaseController<CreateLessonDto, LessonDetailsDto, LessonDto, Guid>(lessonService)
{
    private readonly ILessonService _lessonService = lessonService;
    private readonly IAttachmentService _attachmentService = attachmentService;

    /// <summary>
    /// Retrieves all lessons for a specific class
    /// </summary>
    /// <param name="classId">The unique identifier of the class</param>
    /// <response code="200">Returns the list of lessons for the specified class</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user is not a teacher or administrator</response>
    /// <response code="404">If the class doesn't exist</response>
    [HttpGet("class/{classId:guid}")]
    [Authorize(Roles = "Teacher,Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LessonDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessonsByClass([FromRoute] Guid classId)
    {
        try
        {
            var lessons = await _lessonService.GetLessonsByClassIdAsync(classId);
            return Ok(lessons);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }

    /// <summary>
    /// Retrieves all lessons taught by a specific teacher
    /// </summary>
    /// <param name="teacherId">The unique identifier of the teacher</param>
    /// <response code="200">Returns the list of lessons for the specified teacher</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user is not an administrator</response>
    /// <response code="404">If the teacher doesn't exist</response>
    [HttpGet("teacher/{teacherId:guid}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LessonDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessonsByTeacher([FromRoute] Guid teacherId)
    {
        try
        {
            var lessons = await _lessonService.GetLessonsByTeacherIdAsync(teacherId);
            return Ok(lessons);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }

    /// <summary>
    /// Retrieves all lessons for a specific subject
    /// </summary>
    /// <param name="subjectId">The unique identifier of the subject</param>
    /// <response code="200">Returns the list of lessons for the specified subject</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user is not a teacher or administrator</response>
    /// <response code="404">If the subject doesn't exist</response>
    [HttpGet("subject/{subjectId:guid}")]
    [Authorize(Roles = "Teacher,Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LessonDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessonsBySubject([FromRoute] Guid subjectId)
    {
        try
        {
            var lessons = await _lessonService.GetLessonsBySubjectIdAsync(subjectId);
            return Ok(lessons);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }
}