using FluxStore.Application.Auth.Commands;
using FluxStore.Application.Auth.Commands.ForgotPassword;
using FluxStore.Application.Auth.Commands.ResetPassword;
using FluxStore.Application.DTOs.Auth;
using FluxStore.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlxStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMediator _mediator;

    public AuthController(IAuthService authService, IMediator mediator)
    {
        _authService = authService;
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var result = await _authService.LoginAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}