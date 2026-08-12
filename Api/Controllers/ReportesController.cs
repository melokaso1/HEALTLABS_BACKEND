using Api.Security;
using Application.UseCases.Reportes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Staff)]
public sealed class ReportesController : ControllerBase
{
    private readonly GetCitasPorRangoUseCase _citasPorRango;
    private readonly GetConteoCitasPorEstadoUseCase _conteoEstado;
    private readonly GetConteoCitasPorMedicoUseCase _conteoMedico;

    public ReportesController(
        GetCitasPorRangoUseCase citasPorRango,
        GetConteoCitasPorEstadoUseCase conteoEstado,
        GetConteoCitasPorMedicoUseCase conteoMedico)
    {
        _citasPorRango = citasPorRango;
        _conteoEstado = conteoEstado;
        _conteoMedico = conteoMedico;
    }

    [HttpGet("citas")]
    public async Task<IActionResult> CitasPorRango(
        [FromQuery] DateOnly desde,
        [FromQuery] DateOnly hasta,
        [FromQuery] Guid? medicoId = null)
    {
        if (hasta < desde)
            return BadRequest("El rango de fechas es inválido.");

        return Ok(await _citasPorRango.ExecuteAsync(desde, hasta, medicoId));
    }

    [HttpGet("citas/por-estado")]
    public async Task<IActionResult> ConteoPorEstado(
        [FromQuery] DateOnly? desde = null,
        [FromQuery] DateOnly? hasta = null)
        => Ok(await _conteoEstado.ExecuteAsync(desde, hasta));

    [HttpGet("citas/por-medico")]
    public async Task<IActionResult> ConteoPorMedico(
        [FromQuery] DateOnly? desde = null,
        [FromQuery] DateOnly? hasta = null)
        => Ok(await _conteoMedico.ExecuteAsync(desde, hasta));
}
