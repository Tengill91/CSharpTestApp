using Microsoft.AspNetCore.Identity;
using CsharpTestApp.Models;
using CsharpTestApp.Repositories;
using CsharpTestApp.Dtos;
using Microsoft.AspNetCore.Components.Forms;
using CsharpTestApp.Dtos.Mappers;

namespace CsharpTestApp.Services;

public class UserService
{
    private readonly ILogger<UserService> _logger;
    private readonly PasswordHasher<User> _hasher = new();
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository, ILogger<UserService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public string SayHello()
    {
        return "Hello there from UserService!";
    }

    public List<UserDto> GetAllUsers(){

        var users = _repository.GetAll();
        return UserDtoMapper.MapToUserDtos(users.ToList());
    }

    public UserDto GetUser(int id) 
    {
        var user =  _repository.GetById(id);
        if(user == null)
        {
            _logger.LogWarning("User with ID {UserId} not found.", id);
            return null;
        }
        return UserDtoMapper.MapToUserDto(user);
    }

    public async Task AddUserAsync(User user)
    {
        try
        {
            var existingUser = await _repository.GetByEmailAsync(user.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists.");
            }

            var hashedPassword = _hasher.HashPassword(user, user.Password);
            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = hashedPassword
            };

            _repository.Add(newUser);
            _logger.LogInformation("User added successfully with email {Email}.", user.Email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding a user.");
            throw;
        }
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _repository.GetByEmailAsync(email);
    }
}