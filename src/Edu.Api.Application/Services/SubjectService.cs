using Edu.Api.Domain.Common;
using Edu.Api.Domain.Entities;
using Edu.Api.Application.DTOs.Subject;
using Edu.Api.Application.Mappings;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Application.Interfaces.Services;

namespace Edu.Api.Application.Services;

/// <summary>
/// Service for managing academic subjects and their relationships with teachers
/// </summary>
public class SubjectService(
    ISubjectRepository subjectRepository,
    ITeacherSubjectRepository teacherSubjectRepository,
    ILessonRepository lessonRepository,
    IUserRepository userRepository,
    IMapper<Subject, SubjectDto, SubjectDetailsDto, CreateSubjectDto, Guid> subjectMapper,
    ICurrentUser currentUser) : BaseService<Subject, CreateSubjectDto, SubjectDetailsDto, SubjectDto, Guid>(subjectRepository, subjectMapper), ISubjectService
{
    private readonly ISubjectRepository _subjectRepository = subjectRepository;
    private readonly ITeacherSubjectRepository _teacherSubjectRepository = teacherSubjectRepository;
    private readonly ILessonRepository _lessonRepository = lessonRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ICurrentUser _currentUser = currentUser;

    /// <summary>
    /// Retrieves subjects assigned to the current teacher with teacher information
    /// </summary>
    /// <returns>A collection of subject DTOs with associated teachers</returns>
    public async Task<IEnumerable<SubjectDto>> GetTeacherSubjectsAsync()
    {
        var teacherId = _currentUser.UserId;
        var subjects = await _subjectRepository.GetSubjectsByTeacherIdAsync(teacherId);

        var result = new List<SubjectDto>();

        foreach (var subject in subjects)
        {
            var subjectDto = _mapper.MapToDto(subject);
            var teachers = await _teacherSubjectRepository.GetTeachersBySubjectIdAsync(subject.Id);

            subjectDto.Teachers = [.. teachers.Select(t => new SubjectTeacherDto
            {
                TeacherId = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email
            })];

            result.Add(subjectDto);
        }

        return result;
    }

    /// <summary>
    /// Retrieves subjects that the current student is enrolled in with teacher information
    /// </summary>
    /// <returns>A collection of subject DTOs with associated teachers</returns>
    public async Task<IEnumerable<SubjectDto>> GetStudentSubjectsAsync()
    {
        var studentId = _currentUser.UserId;
        var subjects = await _subjectRepository.GetSubjectsByStudentIdAsync(studentId);

        var result = new List<SubjectDto>();

        foreach (var subject in subjects)
        {
            var subjectDto = _mapper.MapToDto(subject);
            var teachers = await _teacherSubjectRepository.GetTeachersBySubjectIdAsync(subject.Id);

            subjectDto.Teachers = [.. teachers.Select(t => new SubjectTeacherDto
            {
                TeacherId = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email
            })];

            result.Add(subjectDto);
        }

        return result;
    }

    /// <summary>
    /// Retrieves all subjects with authorization check and teacher information
    /// </summary>
    /// <returns>A collection of subject DTOs with associated teachers</returns>
    public override async Task<IEnumerable<SubjectDto>> GetAllAsync()
    {
        var subjects = await _subjectRepository.GetAllSubjectsWithTeachersAsync();

        var result = new List<SubjectDto>();

        foreach (var subject in subjects)
        {
            var subjectDto = _mapper.MapToDto(subject);

            if (subject.TeacherSubjects != null)
            {
                subjectDto.Teachers = [.. subject.TeacherSubjects
                    .Where(ts => ts.Teacher != null)
                    .Select(ts => new SubjectTeacherDto
                    {
                        TeacherId = ts.Teacher.Id,
                        FirstName = ts.Teacher.FirstName,
                        LastName = ts.Teacher.LastName,
                        Email = ts.Teacher.Email
                    })];
            }

            result.Add(subjectDto);
        }

        return result;
    }

    /// <summary>
    /// Retrieves detailed information about a specific subject with authorization check
    /// </summary>
    /// <param name="id">The subject identifier</param>
    /// <returns>A detailed DTO representation of the subject</returns>
    public async Task<SubjectDetailsDto> GetSubjectDetailsAsync(Guid id)
    {
        if (_currentUser.Role != "Administrator" && _currentUser.Role != "Teacher")
        {
            if (_currentUser.Role == "Student")
            {
                var studentSubjects = await _subjectRepository.GetSubjectsByStudentIdAsync(_currentUser.UserId);
                if (!studentSubjects.Any(s => s.Id == id))
                    throw new UnauthorizedAccessException("You don't have access to this subject");
            }
            else
            {
                throw new UnauthorizedAccessException("You don't have access to subject details");
            }
        }

        var subject = await _subjectRepository.GetSubjectWithTeachersAsync(id) ?? throw new ArgumentException($"Subject with id {id} not found");

        var teachers = subject.TeacherSubjects
            .Where(ts => ts.Teacher != null)
            .Select(ts => new SubjectTeacherDto
            {
                TeacherId = ts.Teacher.Id,
                FirstName = ts.Teacher.FirstName,
                LastName = ts.Teacher.LastName,
                Email = ts.Teacher.Email
            })
            .ToList();

        var lessons = await _lessonRepository.GetLessonsBySubjectIdAsync(id);
        var classIds = lessons.Select(l => l.ClassSubjectTeacher.ClassId).Distinct().ToList();
        var classesCount = classIds.Count;

        int studentsCount = 0;
        if (_currentUser.Role == "Administrator")
        {
            var studentClasses = await _subjectRepository.GetStudentClass(classIds);

            studentsCount = studentClasses.Select(sc => sc.StudentId).Distinct().Count();
        }

        return new SubjectDetailsDto
        {
            Id = subject.Id,
            Name = subject.Name,
            Description = subject.Description,
            IsActive = subject.IsActive,
            Teachers = teachers
        };
    }

    /// <summary>
    /// Creates a new subject with authorization check and assigns teachers if specified
    /// </summary>
    /// <param name="dto">The DTO containing subject creation data</param>
    /// <returns>The identifier of the created subject</returns>
    public override async Task<Guid> CreateAsync(CreateSubjectDto dto)
    {
        var subject = _mapper.MapToEntity(dto);
        return await _repository.AddAsync(subject);

/*        if (dto.TeacherIds != null && dto.TeacherIds.Any())
        {
            foreach (var teacherId in dto.TeacherIds)
            {
                await _subjectRepository.AddTeacherToSubjectAsync(subjectId, teacherId);
            }
        }

        return subjectId;*/
    }

    /// <summary>
    /// Assigns a teacher to a subject with authorization check
    /// </summary>
    /// <param name="subjectId">The subject identifier</param>
    /// <param name="teacherId">The teacher identifier</param>
    public async Task<Guid> AddTeacherToSubjectAsync(Guid subjectId, Guid teacherId)
    {
        return await _subjectRepository.AddTeacherToSubjectAsync(subjectId, teacherId);
    }

    /// <summary>
    /// Removes a teacher from a subject with authorization check
    /// </summary>
    /// <param name="subjectId">The subject identifier</param>
    /// <param name="teacherId">The teacher identifier</param>
    public async Task RemoveTeacherFromSubjectAsync(Guid subjectId, Guid teacherId)
    {
        await _subjectRepository.RemoveTeacherFromSubjectAsync(subjectId, teacherId);
    }
}