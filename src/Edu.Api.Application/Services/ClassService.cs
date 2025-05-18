using Edu.Api.Domain.Common;
using Edu.Api.Domain.Entities;
using Edu.Api.Application.DTOs.Class;
using Edu.Api.Application.Mappings;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Application.Interfaces.Services;

namespace Edu.Api.Application.Services;

/// <summary>
/// Service for managing class entities and related operations
/// </summary>
public class ClassService(
    IClassRepository classRepository,
    IMapper<Class, ClassDto, ClassDetailsDto, CreateClassDto, Guid> classMapper,
    ICurrentUser currentUser,
    IStudentClassRepository studentClassRepository,
    IParentStudentRepository parentStudentRepository) : BaseService<Class, CreateClassDto, ClassDetailsDto, ClassDto, Guid>(classRepository, classMapper), IClassService
{
    private readonly IClassRepository _classRepository = classRepository;
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly IStudentClassRepository _studentClassRepository = studentClassRepository;
    private readonly IParentStudentRepository _parentStudentRepository = parentStudentRepository;

    /// <summary>
    /// Retrieves all classes assigned to the current teacher
    /// </summary>
    /// <returns>A collection of class DTOs assigned to the teacher</returns>
    public async Task<IEnumerable<ClassDto>> GetTeacherClassesAsync()
    {
        var teacherId = _currentUser.UserId;
        var classes = await _classRepository.GetClassesByTeacherIdAsync(teacherId);
        return classes.Select(_mapper.MapToDto);
    }

    /// <summary>
    /// Retrieves all classes that the current student is enrolled in
    /// </summary>
    /// <returns>A collection of class DTOs the student is enrolled in</returns>
    public async Task<IEnumerable<ClassDto>> GetStudentClassesAsync()
    {
        var studentId = _currentUser.UserId;
        var classes = await _studentClassRepository.GetClassesByStudentIdAsync(studentId);
        return classes.Select(_mapper.MapToDto);
    }

    /// <summary>
    /// Retrieves all classes that the current parent's children are enrolled in
    /// </summary>
    /// <returns>A collection of unique class DTOs the parent's children are enrolled in</returns>
    public async Task<IEnumerable<ClassDto>> GetParentClassesAsync()
    {
        var parentId = _currentUser.UserId;
        var childrenIds = await _parentStudentRepository.GetStudentIdsByParentIdAsync(parentId);

        var classes = new List<Class>();
        foreach (var childId in childrenIds)
        {
            var childClasses = await _studentClassRepository.GetClassesByStudentIdAsync(childId);
            classes.AddRange(childClasses);
        }

        classes = [.. classes.DistinctBy(c => c.Id)];

        return classes.Select(_mapper.MapToDto);
    }

    /// <summary>
    /// Retrieves a specific class by its identifier, with access control validation
    /// </summary>
    /// <param name="id">The class identifier</param>
    /// <returns>A detailed DTO representation of the class</returns>
    public override async Task<ClassDetailsDto> GetByIdAsync(Guid id)
    {
        var classEntity = await _repository.GetByIdAsync(id) ?? throw new ArgumentException($"Class with id {id} not found");
        if (_currentUser.Role == "Administrator")
            return _mapper.MapToDetailsDto(classEntity);

        bool hasAccess = await CheckClassAccessAsync(id);
        if (!hasAccess)
            throw new UnauthorizedAccessException($"User does not have access to class with id {id}");

        return _mapper.MapToDetailsDto(classEntity);
    }

    /// <summary>
    /// Adds a student to a specific class
    /// </summary>
    /// <param name="classId">The class identifier</param>
    /// <param name="studentId">The student identifier</param>
    /// <returns>The identifier of the created student-class relationship</returns>
    public async Task<Guid> AddStudentToClass(Guid classId, Guid studentId)
    {
        StudentClass entity = new()
        {
            StudentId = studentId,
            ClassId = classId,
            JoinDate = DateTime.UtcNow
        };
        return await _studentClassRepository.AddAsync(entity);
    }

    /// <summary>
    /// Verifies if the current user has access to a specific class
    /// </summary>
    /// <param name="classId">The class identifier to check access for</param>
    /// <returns>True if the user has access, false otherwise</returns>
    private async Task<bool> CheckClassAccessAsync(Guid classId)
    {
        var userId = _currentUser.UserId;

        switch (_currentUser.Role)
        {
            case "Administrator":
                return true;

            case "Teacher":
                var teacherClasses = await _classRepository.GetClassesByTeacherIdAsync(userId);
                return teacherClasses.Any(c => c.Id == classId);

            case "Student":
                var studentClasses = await _studentClassRepository.GetClassesByStudentIdAsync(userId);
                return studentClasses.Any(c => c.Id == classId);

            case "Parent":
                var childrenIds = await _parentStudentRepository.GetStudentIdsByParentIdAsync(userId);
                foreach (var childId in childrenIds)
                {
                    var childClasses = await _studentClassRepository.GetClassesByStudentIdAsync(childId);
                    if (childClasses.Any(c => c.Id == classId))
                        return true;
                }
                return false;

            default:
                return false;
        }
    }
}