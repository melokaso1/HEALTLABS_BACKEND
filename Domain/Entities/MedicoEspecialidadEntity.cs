namespace Domain.Entities
{
    public class MedicoEspecialidadEntity
    {
        public Guid Id { get; set; }
        public Guid MedicoId { get; set; }
        public Guid EspecialidadId { get; set; }
        public bool Principal { get; set; }

        public MedicoEntity? Medico { get; set; }
        public EspecialidadEntity? Especialidad { get; set; }

        private MedicoEspecialidadEntity() { }

        public MedicoEspecialidadEntity(Guid medicoId, Guid especialidadId, bool principal)
        {
            Id = Guid.NewGuid();
            MedicoId = medicoId;
            EspecialidadId = especialidadId;
            Principal = principal;
        }

        public void Update(Guid medicoId, Guid especialidadId, bool principal)
        {
            MedicoId = medicoId;
            EspecialidadId = especialidadId;
            Principal = principal;
        }
    }
}
