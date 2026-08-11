using Application.DTOs.Cita;
using Application.DTOs.Medico;
using Domain.Entities;
using Domain.Interfaces;


namespace Application.UseCases.Medicos
{
    public class CreateMedicoUseCase
    {
        private readonly IGenericRepository<MedicoEntity> _repo;

        public CreateMedicoUseCase(IGenericRepository<MedicoEntity> repo)
        {
            _repo = repo;
        }

        public async Task<MedicoEntity> ExecuteAsync(CreateMedicoDto dto)
        {
            var medico = new MedicoEntity(
                                        dto.EmpleadoId,
                                        dto.RegistroProfesional,
                                        dto.Activo
                                        );

            return await _repo.AddAsync(medico);
        }
    }
}