using AIDashboard.API.Models;
using AIDashboard.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AIDashboard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var token = await _authService.RegisterAsync(request);
        if (token == null)
            return BadRequest("Email already exists");

        return Ok(new { token, message = "Registration successful" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _authService.LoginAsync(request);
        if (token == null)
            return Unauthorized("Invalid email or password");

        return Ok(new { token, message = "Login successful" });
    }
}