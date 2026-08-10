namespace Domain.Entities
{
    public class MedicoEntity
    {
        public int IdMedico { get; set; }
        public int? IdEmpleado { get; set; }
        public int? IdEspecialidad { get; set; }

        public Empleado? Empleado { get; set; }
        public EspecialidadEntity? Especialidad { get; set; }
    }
}
