using CsharpTestApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsharpTestApp.Controllers;

[Authorize]
public class TestController(ILogger<TestController> logger, TestService testService) : ControllerBase
{
    private readonly ILogger<TestController> _logger = logger;
    private readonly TestService _testService = testService;


    [HttpPost("reverse-letters")]
    public IActionResult ReverseLetters([FromBody] List<char> letters)
    {
        try
        {
            var result = _testService.ReverseLetters(letters);
            return Ok(result);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex, "Error in ReverseLetters endpoint.");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error in ReverseLetters endpoint.");
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpGet("is-palindrome")]
    public IActionResult IsPalindrome([FromQuery] string input)
    {
        try
        {
            var result = _testService.IsPalindrome(input);
            return Ok(result);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex, "Error in IsPalindrome endpoint.");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error in IsPalindrome endpoint.");
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpGet("reverse-words")]
    public IActionResult ReverseWords([FromQuery] string words)
    {
        try
        {
            var result = _testService.ReverseString(words);
            return Ok(result);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex, "Error in ReverseString endpoint.");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error in ReverseString endpoint.");
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpGet("give-me-fruits")]
    public IActionResult GiveMeFruits([FromQuery] bool hungry)
    {
        try
        {
            var result = _testService.GiveMeFruits(hungry);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error in GiveMeFruits endpoint.");
            return StatusCode(500, "Internal server error.");
        }
    }

    

}