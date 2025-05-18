using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Edu.Api.Application.DTOs.Lesson;
using Edu.Api.Application.Interfaces.Services;

namespace Edu.Api.WebApi.Controllers;

/// <summary>
/// Controller for managing schedules in the educational system
/// </summary>
/// <param name="lessonService">Service for handling lesson operations</param>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ScheduleController(ILessonService lessonService) : ControllerBase
{
    private readonly ILessonService _lessonService = lessonService;

    /// <summary>
    /// Retrieves the schedule for the current student within a date range
    /// </summary>
    /// <param name="startDate">The start date of the schedule period</param>
    /// <param name="endDate">The end date of the schedule period</param>
    /// <response code="200">Returns the student's schedule for the specified period</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user is not a student or administrator</response>
    [HttpGet("student")]
    [Authorize(Roles = "Student,Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LessonDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<LessonDto>>> GetStudentSchedule([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var schedule = await _lessonService.GetStudentScheduleAsync(startDate, endDate);
        return Ok(schedule);
    }

    /// <summary>
    /// Retrieves the schedule for the current teacher within a date range
    /// </summary>
    /// <param name="startDate">The start date of the schedule period</param>
    /// <param name="endDate">The end date of the schedule period</param>
    /// <response code="200">Returns the teacher's schedule for the specified period</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user is not a teacher or administrator</response>
    [HttpGet("teacher")]
    [Authorize(Roles = "Teacher,Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LessonDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<LessonDto>>> GetTeacherSchedule([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var schedule = await _lessonService.GetTeacherScheduleAsync(startDate, endDate);
        return Ok(schedule);
    }

    /// <summary>
    /// Retrieves the schedule for the current parent's children within a date range
    /// </summary>
    /// <param name="startDate">The start date of the schedule period</param>
    /// <param name="endDate">The end date of the schedule period</param>
    /// <response code="200">Returns the schedule for the parent's children for the specified period</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user is not a parent or administrator</response>
    [HttpGet("parent")]
    [Authorize(Roles = "Parent,Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LessonDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<LessonDto>>> GetParentSchedule([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var schedule = await _lessonService.GetParentScheduleAsync(startDate, endDate);
        return Ok(schedule);
    }

    /// <summary>
    /// Retrieves a schedule based on custom filter criteria
    /// </summary>
    /// <param name="filter">Filter criteria for schedule search</param>
    /// <response code="200">Returns the schedule matching the filter criteria</response>
    /// <response code="400">If the filter parameters are invalid</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="403">If the user is not an administrator</response>
    [HttpGet("filter")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LessonDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<LessonDto>>> GetScheduleByFilter([FromQuery] ScheduleFilterDto filter)
    {
        try
        {
            var schedule = await _lessonService.GetScheduleByFilterAsync(filter);
            return Ok(schedule);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }
}