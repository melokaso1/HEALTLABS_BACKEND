namespace Application.DTOs.TipoCita
{
    public class CreateTipoCitaDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int DuracionMinutos { get; set; }
        public bool Activo { get; set; } = true;
    }
}
