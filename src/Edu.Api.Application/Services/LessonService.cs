using Edu.Api.Domain.Common;
using Edu.Api.Domain.Entities;
using Edu.Api.Application.DTOs.Lesson;
using Edu.Api.Application.Mappings;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Application.Interfaces.Services;

namespace Edu.Api.Application.Services;

/// <summary>
/// Service for managing lessons, schedules, and related operations
/// </summary>
public class LessonService(
    ILessonRepository lessonRepository,
    IClassRepository classRepository,
    ISubjectRepository subjectRepository,
    IMapper<Lesson, LessonDto, LessonDetailsDto, CreateLessonDto, Guid> lessonMapper,
    ICurrentUser currentUser,
    IStudentClassRepository studentClassRepository,
    IParentStudentRepository parentStudentRepository) : BaseService<Lesson, CreateLessonDto, LessonDetailsDto, LessonDto, Guid>(lessonRepository, lessonMapper), ILessonService
{
    private readonly ILessonRepository _lessonRepository = lessonRepository;
    private readonly IClassRepository _classRepository = classRepository;
    private readonly ISubjectRepository _subjectRepository = subjectRepository;
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly IStudentClassRepository _studentClassRepository = studentClassRepository;
    private readonly IParentStudentRepository _parentStudentRepository = parentStudentRepository;

    /// <summary>
    /// Retrieves the schedule for the current student within a date range
    /// </summary>
    /// <param name="startDate">The start date of the schedule period</param>
    /// <param name="endDate">The end date of the schedule period</param>
    /// <returns>A collection of lesson DTOs for the student's schedule</returns>
    public async Task<IEnumerable<LessonDto>> GetStudentScheduleAsync(DateTime startDate, DateTime endDate)
    {
        var studentId = _currentUser.UserId;

        var studentClasses = await _studentClassRepository.GetClassesByStudentIdAsync(studentId);
        if (!studentClasses.Any())
            return [];

        var filter = new ScheduleFilterDto
        {
            StartDate = startDate,
            EndDate = endDate,
            ClassId = studentClasses.First().Id
        };

        var lessons = await _lessonRepository.GetLessonsByFilterAsync(filter);
        return lessons.Select(_mapper.MapToDto);
    }

    /// <summary>
    /// Retrieves the schedule for the current teacher within a date range
    /// </summary>
    /// <param name="startDate">The start date of the schedule period</param>
    /// <param name="endDate">The end date of the schedule period</param>
    /// <returns>A collection of lesson DTOs for the teacher's schedule</returns>
    public async Task<IEnumerable<LessonDto>> GetTeacherScheduleAsync(DateTime startDate, DateTime endDate)
    {
        var teacherId = _currentUser.UserId;

        var filter = new ScheduleFilterDto
        {
            StartDate = startDate,
            EndDate = endDate,
            TeacherId = teacherId
        };

        var lessons = await _lessonRepository.GetLessonsByFilterAsync(filter);
        return lessons.Select(_mapper.MapToDto);
    }

    /// <summary>
    /// Retrieves the schedule for all children of the current parent within a date range
    /// </summary>
    /// <param name="startDate">The start date of the schedule period</param>
    /// <param name="endDate">The end date of the schedule period</param>
    /// <returns>A collection of unique lesson DTOs for all children's schedules</returns>
    public async Task<IEnumerable<LessonDto>> GetParentScheduleAsync(DateTime startDate, DateTime endDate)
    {
        var parentId = _currentUser.UserId;

        var childrenIds = await _parentStudentRepository.GetStudentIdsByParentIdAsync(parentId);
        if (!childrenIds.Any())
            return [];

        var allLessons = new List<Lesson>();

        foreach (var childId in childrenIds)
        {
            var childClasses = await _studentClassRepository.GetClassesByStudentIdAsync(childId);
            if (!childClasses.Any())
                continue;

            var filter = new ScheduleFilterDto
            {
                StartDate = startDate,
                EndDate = endDate,
                ClassId = childClasses.First().Id
            };

            var childLessons = await _lessonRepository.GetLessonsByFilterAsync(filter);
            allLessons.AddRange(childLessons);
        }

        return allLessons
            .DistinctBy(l => l.Id)
            .Select(_mapper.MapToDto);
    }

    /// <summary>
    /// Retrieves lessons based on a specified filter, restricted to administrators
    /// </summary>
    /// <param name="filter">The schedule filter criteria</param>
    /// <returns>A collection of lesson DTOs matching the filter</returns>
    public async Task<IEnumerable<LessonDto>> GetScheduleByFilterAsync(ScheduleFilterDto filter)
    {
        if (_currentUser.Role != "Administrator")
            throw new UnauthorizedAccessException("Only administrators can access arbitrary schedules");

        var lessons = await _lessonRepository.GetLessonsByFilterAsync(filter);
        return lessons.Select(_mapper.MapToDto);
    }

    /// <summary>
    /// Creates a new lesson with authorization check
    /// </summary>
    /// <param name="dto">The DTO containing lesson creation data</param>
    /// <returns>The identifier of the created lesson</returns>
    public override async Task<Guid> CreateAsync(CreateLessonDto dto)
    {
        if (_currentUser.Role != "Administrator")
            throw new UnauthorizedAccessException("Only administrators can create lessons");

        if (dto.IsRecurring && dto.DayOfWeek.HasValue)
        {
            // Note: Implementation for recurring lessons appears to be incomplete
        }

        var entity = _mapper.MapToEntity(dto);
        return await _repository.AddAsync(entity);
    }

    /// <summary>
    /// Retrieves lessons for a specific class with authorization check
    /// </summary>
    /// <param name="classId">The class identifier</param>
    /// <returns>A collection of lesson DTOs for the specified class</returns>
    public async Task<IEnumerable<LessonDto>> GetLessonsByClassIdAsync(Guid classId)
    {
        if (_currentUser.Role != "Administrator")
        {
            var teacherId = _currentUser.UserId;
            bool isTeacherOfClass = await _classRepository.IsTeacherOfClassAsync(teacherId, classId);
            if (!isTeacherOfClass)
                throw new UnauthorizedAccessException("You don't have access to this class's lessons");
        }

        var lessons = await _lessonRepository.GetLessonsByClassIdAsync(classId);
        return lessons.Select(_mapper.MapToDto);
    }

    /// <summary>
    /// Retrieves lessons for a specific teacher, restricted to administrators
    /// </summary>
    /// <param name="teacherId">The teacher identifier</param>
    /// <returns>A collection of lesson DTOs for the specified teacher</returns>
    public async Task<IEnumerable<LessonDto>> GetLessonsByTeacherIdAsync(Guid teacherId)
    {
        if (_currentUser.Role != "Administrator")
            throw new UnauthorizedAccessException("Only administrators can view any teacher's lessons");

        var lessons = await _lessonRepository.GetLessonsByTeacherIdAsync(teacherId);
        return lessons.Select(_mapper.MapToDto);
    }

    /// <summary>
    /// Retrieves lessons for a specific subject with authorization check
    /// </summary>
    /// <param name="subjectId">The subject identifier</param>
    /// <returns>A collection of lesson DTOs for the specified subject</returns>
    public async Task<IEnumerable<LessonDto>> GetLessonsBySubjectIdAsync(Guid subjectId)
    {
        if (_currentUser.Role != "Administrator")
        {
            var teacherId = _currentUser.UserId;
            bool isTeacherOfSubject = await _subjectRepository.IsTeacherOfSubjectAsync(teacherId, subjectId);
            if (!isTeacherOfSubject)
                throw new UnauthorizedAccessException("You don't have access to this subject's lessons");
        }

        var lessons = await _lessonRepository.GetLessonsBySubjectIdAsync(subjectId);
        return lessons.Select(_mapper.MapToDto);
    }
}