using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using CsharpTestApp.Models;
using CsharpTestApp.Services;

namespace CsharpTestApp.Controllers;

[ApiController]
[Route("api/User")]
[Authorize]
public class UserController(UserService service, ILogger<UserController> logger) : ControllerBase
{
    private readonly UserService _service = service;
    private readonly ILogger<UserController> _logger = logger;

    [HttpGet("hello")]
    public IActionResult GetHelloUserEndpoint(){
        try{

            var hello = _service.SayHello();
            _logger.LogInformation("sending hello was successfull");
            return Ok(hello);
        }catch(Exception ex){
            _logger.LogError(ex, "An error occured when saying hello");
            return StatusCode(500, "Internal server error");
        } 
    }


    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var users = _service.GetAllUsers();
            _logger.LogInformation("Fetched all users.");
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching all users.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var user = _service.GetUser(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", id);
                return NotFound();
            }

            _logger.LogInformation("Fetched user with ID {UserId}.", id);
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching user with ID {UserId}.", id);
            return StatusCode(500, "Internal server error");
        }
    }

}