using Edu.Api.Domain.Entities;
using Edu.Api.Application.DTOs.User;
using Edu.Api.Application.Mappings;
using Edu.Api.Application.Interfaces.Repositories;
using Edu.Api.Application.Interfaces.Services;

namespace Edu.Api.Application.Services;

/// <summary>
/// Service for managing user entities and user-related operations
/// </summary>
public class UserService(
    IUserRepository userRepository,
    IMapper<User, UserDto, UserDetailsDto, CreateUserDto, Guid> userMapper) : BaseService<User, CreateUserDto, UserDetailsDto, UserDto, Guid>(userRepository, userMapper), IUserService
{
    /// <summary>
    /// Creates a new user with password hashing
    /// </summary>
    /// <param name="createUserDto">The DTO containing user creation data including password</param>
    /// <returns>The identifier of the created user</returns>
    public override async Task<Guid> CreateAsync(CreateUserDto createUserDto)
    {
        var user = _mapper.MapToEntity(createUserDto);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);
        return await _repository.AddAsync(user);
    }
}