using System.Threading.Tasks;
using CsharpTestApp.Dtos;
using CsharpTestApp.Models;
using CsharpTestApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CsharpTestApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService authService, UserService userService, ILogger<AuthController> logger) : ControllerBase
{
    private readonly AuthService _authService = authService;
    private readonly UserService _userService = userService;
    private readonly ILogger<AuthController> _logger = logger;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _authService.LoginAsync(dto.Email, dto.Password);
        if (token == null)
        {
            _logger.LogWarning("Invalid login attempt for email: {Email}", dto.Email);
            return Unauthorized("Invalid credentials");
        }

        _logger.LogInformation("User logged in: {Email}", dto.Email);
        return Ok(new { Token = token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid user model received.");
            return BadRequest(ModelState);
        }

        try
        {
            await _userService.AddUserAsync(user);
            _logger.LogInformation("User created with ID {UserId}.", user.Id);
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a user.");
            return StatusCode(500, "Internal server error");
        }
    }
}

