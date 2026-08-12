namespace Application.DTOs.TipoCita
{
    public class UpdateTipoCitaDto
    {
        public string Nombre { get; set; } = string.Empty;
        public int DuracionMinutos { get; set; }
        public bool Activo { get; set; } = true;
    }
}
