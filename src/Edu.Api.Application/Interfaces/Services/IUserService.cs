using Edu.Api.Application.DTOs.User;

namespace Edu.Api.Application.Interfaces.Services;

/// <summary>
/// Service interface for managing users in the educational system
/// </summary>
public interface IUserService : IBaseService<CreateUserDto, UserDetailsDto, UserDto, Guid>
{
}