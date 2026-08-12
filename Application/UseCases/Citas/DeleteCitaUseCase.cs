using Application.DTOs.Cita;

namespace Application.UseCases.Citas;

public sealed class DeleteCitaUseCase
{
    private readonly CancelCitaUseCase _cancel;

    public DeleteCitaUseCase(CancelCitaUseCase cancel) => _cancel = cancel;

    public Task ExecuteAsync(Guid id, string motivoCancelacion = "Cancelada por usuario") =>
        _cancel.ExecuteAsync(id, new CancelCitaDto
        {
            MotivoCancelacion = motivoCancelacion,
            UsuarioCancelacionId = null
        });
}
