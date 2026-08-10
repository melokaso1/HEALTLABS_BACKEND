using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<SexoEntity> Sexos => Set<SexoEntity>();
    public DbSet<TipoDocumentoEntity> TiposDocumento => Set<TipoDocumentoEntity>();
    public DbSet<PersonaEntity> Personas => Set<PersonaEntity>();
    public DbSet<PersonaTelefonoEntity> PersonasTelefono => Set<PersonaTelefonoEntity>();
    public DbSet<PersonaDireccionEntity> PersonasDireccion => Set<PersonaDireccionEntity>();
    public DbSet<CargoEntity> Cargos => Set<CargoEntity>();
    public DbSet<EmpleadoEntity> Empleados => Set<EmpleadoEntity>();
    public DbSet<RolEntity> Roles => Set<RolEntity>();
    public DbSet<PermisoEntity> Permisos => Set<PermisoEntity>();
    public DbSet<RolPermisoEntity> RolesPermiso => Set<RolPermisoEntity>();
    public DbSet<UsuarioEntity> Usuarios => Set<UsuarioEntity>();
    public DbSet<SesionEntity> Sesiones => Set<SesionEntity>();
    public DbSet<LoginIntentoEntity> LoginIntentos => Set<LoginIntentoEntity>();
    public DbSet<TokenRecuperacionEntity> TokensRecuperacion => Set<TokenRecuperacionEntity>();
    public DbSet<PacienteEntity> Pacientes => Set<PacienteEntity>();
    public DbSet<AntecedenteEntity> Antecedentes => Set<AntecedenteEntity>();
    public DbSet<PacienteAlergiaEntity> PacientesAlergia => Set<PacienteAlergiaEntity>();
    public DbSet<EspecialidadEntity> Especialidades => Set<EspecialidadEntity>();
    public DbSet<MedicoEntity> Medicos => Set<MedicoEntity>();
    public DbSet<MedicoEspecialidadEntity> MedicosEspecialidad => Set<MedicoEspecialidadEntity>();
    public DbSet<HorarioEntity> Horarios => Set<HorarioEntity>();
    public DbSet<TipoCitaEntity> TiposCita => Set<TipoCitaEntity>();
    public DbSet<EstadoCitaEntity> EstadosCita => Set<EstadoCitaEntity>();
    public DbSet<CitaEntity> Citas => Set<CitaEntity>();
    public DbSet<CitaHistorialEstadoEntity> CitasHistorialEstado => Set<CitaHistorialEstadoEntity>();
    public DbSet<DetalleCitaEntity> DetallesCita => Set<DetalleCitaEntity>();
    public DbSet<DiagnosticoEntity> Diagnosticos => Set<DiagnosticoEntity>();
    public DbSet<DetalleDiagnosticoEntity> DetallesDiagnostico => Set<DetalleDiagnosticoEntity>();
    public DbSet<TratamientoEntity> Tratamientos => Set<TratamientoEntity>();
    public DbSet<TratamientoPosologiaEntity> TratamientosPosologia => Set<TratamientoPosologiaEntity>();
    public DbSet<AtencionTratamientoEntity> AtencionesTratamiento => Set<AtencionTratamientoEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
