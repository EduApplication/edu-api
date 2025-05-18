using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Edu.Api.Application.DTOs.User;
using Edu.Api.Application.Interfaces.Services;

namespace Edu.Api.WebApi.Controllers;

/// <summary>
/// Controller for managing users in the educational system
/// </summary>
/// <param name="service">Service for handling user operations</param>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController(IUserService service) : BaseController<CreateUserDto, UserDetailsDto, UserDto, Guid>(service)
{
}
