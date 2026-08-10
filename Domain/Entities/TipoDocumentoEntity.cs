namespace Domain.Entities
{
    public class TipoDocumentoEntity
    {
        public int IdTipoDocumento { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public ICollection<PersonaEntity> Personas { get; set; } = [];
    }
}
