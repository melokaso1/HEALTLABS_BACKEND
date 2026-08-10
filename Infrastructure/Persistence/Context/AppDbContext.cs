namespace Infrastructure.Persistence.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Cita> Citas { get; set; }
    public DbSet<DetalleCita> DetallesCita { get; set; }
    public DbSet<DetalleDiagnostico> DetallesDiagnostico { get; set; }
    public DbSet<DetalleTratamiento> DetallesTratamiento { get; set; }
    public DbSet<Diagnostico> Diagnosticos { get; set; }
    public DbSet<EstadoCita> EstadosCita { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Profesional> Profesionales { get; set; }
    public DbSet<Tratamiento> Tratamientos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }

}