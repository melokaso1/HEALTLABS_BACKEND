namespace Domain.Entities
{
    public class EspecialidadEntity
    {
        public int IdEspecialidad { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        public ICollection<MedicoEntity> Medicos { get; set; } = [];
    }
}
