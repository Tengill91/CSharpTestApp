using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using CsharpTestApp.Models; // Adjust if your User model lives elsewhere

namespace CsharpTestApp.Services;

public class AuthService(UserService userService, JwtService jwtService, ILogger<AuthService> logger)
{
    private readonly UserService _userService = userService;
    private readonly JwtService _jwtService = jwtService;
    private readonly ILogger<AuthService> _logger = logger;
    private readonly PasswordHasher<User> _hasher = new();

    public async Task<string?> LoginAsync(string email, string password)
    {
        _logger.LogInformation("Login attempt for email: {Email}", email);

        var user = await _userService.GetUserByEmailAsync(email);
        if (user == null)
        {
            _logger.LogWarning("Login failed: User not found for {Email}", email);
            return null;
        }

        var result = _hasher.VerifyHashedPassword(user, user.Password, password);
        if (result != PasswordVerificationResult.Success)
        {
            _logger.LogWarning("Login failed: Invalid password for {Email}", email);
            return null;
        }

        _logger.LogInformation("Login successful for {Email}", email);
        return _jwtService.GenerateToken(user.Id.ToString(), user.Email);
    }
}