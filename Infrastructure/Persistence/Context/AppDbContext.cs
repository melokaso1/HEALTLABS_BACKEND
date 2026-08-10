namespace Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{

    public DbSet<CitaEntity> Citas { get; set; }
    public DbSet<DetalleCitaEntity> DetallesCita { get; set; }
    public DbSet<DetalleDiagnosticoEntity> DetallesDiagnostico { get; set; }
    public DbSet<AtencionTratamientoEntity> AtencionesTratamiento { get; set; }
    public DbSet<DiagnosticoEntity> Diagnosticos { get; set; }
    public DbSet<EstadoCitaEntity> EstadosCita { get; set; }
    
    //public DbSet<Paciente> Pacientes { get; set; }
    //public DbSet<Profesional> Profesionales { get; set; }
    public DbSet<TratamientoEntity> Tratamientos { get; set; }
    public DbSet<HorarioEntity> Horarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }

}