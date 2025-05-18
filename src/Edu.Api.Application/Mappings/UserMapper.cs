using Edu.Api.Application.DTOs.User;
using Edu.Api.Domain.Entities;

namespace Edu.Api.Application.Mappings;

/// <summary>
/// Mapper for converting between User entities and DTOs
/// </summary>
public class UserMapper : IMapper<User, UserDto, UserDetailsDto, CreateUserDto, Guid>
{
    /// <summary>
    /// Maps a User entity to a basic UserDto
    /// </summary>
    /// <param name="entity">The source User entity</param>
    /// <returns>A DTO with basic user information</returns>
    public UserDto MapToDto(User entity)
    {
        return new UserDto
        {
            Id = entity.Id,
            Email = entity.Email,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            PhoneNumber = entity.PhoneNumber,
            RoleName = entity.Role?.Name
        };
    }

    /// <summary>
    /// Maps a User entity to a detailed UserDetailsDto, including relationships
    /// </summary>
    /// <param name="entity">The source User entity with navigation properties</param>
    /// <returns>A DTO with detailed user information including subjects, classes, family relationships</returns>
    public UserDetailsDto MapToDetailsDto(User entity)
    {
        return new UserDetailsDto
        {
            Id = entity.Id,
            Email = entity.Email,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            RoleName = entity.Role?.Name,
            PhoneNumber = entity.PhoneNumber,
            CreatedAt = entity.CreatedAt,
            LastLogin = entity.LastLogin,
            IsActive = entity.IsActive,
            SubjectNames = entity.TeacherSubjects?
                .Select(ts => ts.Subject.Name)
                .ToList(),
            ClassNames = entity.StudentClasses?
                .Select(sc => sc.Class.Name)
                .ToList(),
            Parents = entity.StudentRelationships?
                .Select(ps => new RelatedUserInfoDto
                {
                    Id = ps.Parent.Id,
                    FirstName = ps.Parent.FirstName,
                    LastName = ps.Parent.LastName,
                    Email = ps.Parent.Email,
                    PhoneNumber = ps.Parent.PhoneNumber
                }).ToList(),
            Children = entity.ParentRelationships?
                .Select(ps => new RelatedUserInfoDto
                {
                    Id = ps.Student.Id,
                    FirstName = ps.Student.FirstName,
                    LastName = ps.Student.LastName,
                    Email = ps.Student.Email,
                    PhoneNumber = ps.Student.PhoneNumber
                }).ToList(),
            TutorClassNames = entity.ClassesAsTutor?
                .Select(c => c.Name)
                .ToList()
        };
    }

    /// <summary>
    /// Maps a CreateUserDto to a new User entity
    /// </summary>
    /// <param name="dto">The source DTO with user creation data</param>
    /// <returns>A new User entity</returns>
    public User MapToEntity(CreateUserDto dto)
    {
        return new User
        {
            Email = dto.Email,
            FirstName = dto.FirstName,
            PhoneNumber = dto.PhoneNumber,
            LastName = dto.LastName,
            RoleId = dto.RoleId
        };
    }

    /// <summary>
    /// Updates an existing User entity with data from a CreateUserDto
    /// </summary>
    /// <param name="entity">The User entity to update</param>
    /// <param name="dto">The DTO containing updated values</param>
    public void UpdateEntityFromDto(User entity, CreateUserDto dto)
    {
        entity.Email = dto.Email;
        entity.FirstName = dto.FirstName;
        entity.PhoneNumber = dto.PhoneNumber;
        entity.LastName = dto.LastName;
    }
}