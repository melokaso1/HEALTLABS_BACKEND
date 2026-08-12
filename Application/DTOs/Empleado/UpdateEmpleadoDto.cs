namespace Application.DTOs.Empleado
{
    public class UpdateEmpleadoDto
    {
        public Guid CargoId { get; set; }
        public DateOnly FechaIngreso { get; set; }
        public DateOnly? FechaRetiro { get; set; }
        public bool Activo { get; set; } = true;
    }
}
