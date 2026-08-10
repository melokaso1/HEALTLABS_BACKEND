namespace Domain.Entities
{
    public class Paciente
    {
        public int IdPaciente { get; set; }
        public int? IdPersona { get; set; }
        public bool Activo { get; set; }

        public PersonaEntity? Persona { get; set; }
    }
}
