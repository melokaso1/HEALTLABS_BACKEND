namespace Application.DTOs.Antecedente
{
    public class CreateAntecedenteDto
    {
        public Guid PacienteId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public Guid? UsuarioRegistroId { get; set; }
    }
}
