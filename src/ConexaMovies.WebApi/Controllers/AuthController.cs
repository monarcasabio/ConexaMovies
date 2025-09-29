using ConexaMovies.Application.Dtos;
using ConexaMovies.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConexaMovies.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<TokenResponse>> Register(
        [FromBody] RegisterDto dto,
        CancellationToken ct)
    {
        var tokenResponse = await _userService.RegisterAsync(dto, ct);
        return Ok(tokenResponse);
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponse>> Login(
        [FromBody] LoginDto dto,
        CancellationToken ct)
    {
        var tokenResponse = await _userService.LoginAsync(dto, ct);
        return Ok(tokenResponse);
    }
}
