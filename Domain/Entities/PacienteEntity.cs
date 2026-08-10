namespace Domain.Entities
{
    public class PacienteEntity
    {
        public int IdPaciente { get; set; }
        public int? IdPersona { get; set; }
        public bool Activo { get; set; }

        public PersonaEntity? Persona { get; set; }
    }
}
