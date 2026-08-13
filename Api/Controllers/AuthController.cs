using Application.DTOs.Auth;
using Application.UseCases.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly LoginUseCase _login;
    private readonly RefreshTokenUseCase _refresh;
    private readonly LogoutUseCase _logout;
    private readonly SolicitarRecuperacionUseCase _solicitarRecuperacion;
    private readonly ResetPasswordUseCase _resetPassword;
    private readonly CambiarPasswordUseCase _cambiarPassword;

    public AuthController(
        LoginUseCase login,
        RefreshTokenUseCase refresh,
        LogoutUseCase logout,
        SolicitarRecuperacionUseCase solicitarRecuperacion,
        ResetPasswordUseCase resetPassword,
        CambiarPasswordUseCase cambiarPassword)
    {
        _login = login;
        _refresh = refresh;
        _logout = logout;
        _solicitarRecuperacion = solicitarRecuperacion;
        _resetPassword = resetPassword;
        _cambiarPassword = cambiarPassword;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var result = await _login.ExecuteAsync(request);
        if (!result.Succeeded)
            return StatusCode(result.StatusCode, result.Error);

        return Ok(result.Value);
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request)
    {
        var result = await _refresh.ExecuteAsync(request);
        if (!result.Succeeded)
            return StatusCode(result.StatusCode, result.Error);

        return Ok(result.Value);
    }

    [HttpPost("solicitar-recuperacion")]
    [AllowAnonymous]
    public async Task<IActionResult> SolicitarRecuperacion([FromBody] SolicitarRecuperacionDto request)
    {
        var result = await _solicitarRecuperacion.ExecuteAsync(request);
        if (!result.Succeeded)
            return StatusCode(result.StatusCode, result.Error);

        return Ok(result.Value);
    }

    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto request)
    {
        var result = await _resetPassword.ExecuteAsync(request);
        if (!result.Succeeded)
            return StatusCode(result.StatusCode, result.Error);

        return Ok(result.Value);
    }

    [HttpPost("cambiar-password")]
    [Authorize]
    public async Task<IActionResult> CambiarPassword([FromBody] CambiarPasswordDto request)
    {
        var identifier = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue(ClaimTypes.Name)
            ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            ?? string.Empty;

        var result = await _cambiarPassword.ExecuteAsync(identifier, request);
        if (!result.Succeeded)
            return StatusCode(result.StatusCode, result.Error);

        return Ok(result.Value);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout([FromBody] LogoutRequestDto? request)
    {
        request ??= new LogoutRequestDto();
        request.Jti ??= User.FindFirstValue(JwtRegisteredClaimNames.Jti)
            ?? User.FindFirstValue("jti");

        var result = await _logout.ExecuteAsync(request);
        if (!result.Succeeded)
            return StatusCode(result.StatusCode, result.Error);

        return NoContent();
    }

    [HttpPost("logout/{sesionId:guid}")]
    [Authorize]
    public async Task<IActionResult> LogoutBySesion(Guid sesionId)
    {
        var result = await _logout.ExecuteAsync(new LogoutRequestDto { SesionId = sesionId });
        if (!result.Succeeded)
            return StatusCode(result.StatusCode, result.Error);

        return NoContent();
    }
}
