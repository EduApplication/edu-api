using Edu.Api.Domain.Common;
using Edu.Api.Domain.Entities;
using Edu.Api.Application.DTOs.Grade;
using Edu.Api.Application.Mappings;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Application.Interfaces.Services;

namespace Edu.Api.Application.Services;

/// <summary>
/// Service for managing student grades and related operations
/// </summary>
public class GradeService(
    IGradeRepository gradeRepository,
    ISubjectRepository subjectRepository,
    IMapper<Grade, GradeDto, GradeDetailsDto, CreateGradeDto, Guid> gradeMapper,
    ICurrentUser currentUser) : BaseService<Grade, CreateGradeDto, GradeDetailsDto, GradeDto, Guid>(gradeRepository, gradeMapper), IGradeService
{
    private readonly IGradeRepository _gradeRepository = gradeRepository;
    private readonly ISubjectRepository _subjectRepository = subjectRepository;
    private readonly ICurrentUser _currentUser = currentUser;

    /// <summary>
    /// Retrieves grades for the current authenticated student
    /// </summary>
    /// <returns>A collection of student grades grouped by subject</returns>
    public async Task<IEnumerable<StudentGradesBySubjectDto>> GetCurrentStudentGradesAsync()
    {
        var studentId = _currentUser.UserId;
        return await GetStudentGradesAsync(studentId);
    }

    /// <summary>
    /// Retrieves grades for a specific student
    /// </summary>
    /// <param name="studentId">The student identifier</param>
    /// <returns>A collection of student grades grouped by subject</returns>
    public async Task<IEnumerable<StudentGradesBySubjectDto>> GetStudentGradesAsync(Guid studentId)
    {
        var grades = await _gradeRepository.GetGradesByStudentIdAsync(studentId);
        var groupedGrades = grades
            .GroupBy(g => new { g.SubjectId, SubjectName = g.Subject?.Name })
            .Select(group => new StudentGradesBySubjectDto
            {
                SubjectId = group.Key.SubjectId,
                SubjectName = group.Key.SubjectName,
                Grades = group.Select(_mapper.MapToDto),
                AverageGrade = CalculateAverageGrade(group)
            });
        return groupedGrades;
    }

    /// <summary>
    /// Calculates the weighted average grade for a collection of grades
    /// </summary>
    /// <param name="grades">The collection of grades</param>
    /// <returns>The weighted average grade value</returns>
    private static float CalculateAverageGrade(IEnumerable<Grade> grades)
    {
        if (!grades.Any())
            return 0;
        float weightedSum = grades.Sum(g => g.Value * g.GradeType.Weight);
        float totalWeight = grades.Sum(g => g.GradeType.Weight);
        return totalWeight > 0 ? weightedSum / totalWeight : 0;
    }

    /// <summary>
    /// Retrieves grades for a specific student in a specific subject
    /// </summary>
    /// <param name="studentId">The student identifier</param>
    /// <param name="subjectId">The subject identifier</param>
    /// <returns>A collection of grade DTOs</returns>
    public async Task<IEnumerable<GradeDto>> GetGradesBySubjectForStudentAsync(Guid studentId, Guid subjectId)
    {
        var grades = await _gradeRepository.GetGradesBySubjectForStudentAsync(studentId, subjectId);
        return grades.Select(_mapper.MapToDto);
    }

    /// <summary>
    /// Creates a new grade with authorization check
    /// </summary>
    /// <param name="dto">The DTO containing grade creation data</param>
    /// <returns>The identifier of the created grade</returns>
    public override async Task<Guid> CreateAsync(CreateGradeDto dto)
    {
        if (!(_currentUser.Role == "Administrator" || _currentUser.Role == "Teacher"))
            throw new UnauthorizedAccessException("Only teachers and administrators can create grades");
        var teacherId = _currentUser.UserId;
        var grade = _mapper.MapToEntity(dto);
        grade.TeacherId = teacherId;
        grade.CreatedAt = DateTime.UtcNow;
        return await _repository.AddAsync(grade);
    }
}