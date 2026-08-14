namespace Infrastructure.Persistence.Seeders;

public class DbSeeder(
    SexoSeeder sexoSeeder,
    TipoDocumentoSeeder tipoDocumentoSeeder,
    RolSeeder rolSeeder,
    PermisoSeeder permisoSeeder,
    RolPermisoSeeder rolPermisoSeeder,
    CargoSeeder cargoSeeder,
    EspecialidadSeeder especialidadSeeder,
    PersonaSeeder personaSeeder,
    EmpleadoSeeder empleadoSeeder,
    UsuarioSeeder usuarioSeeder,
    MedicoSeeder medicoSeeder,
    PacienteSeeder pacienteSeeder,
    HorarioSeeder horarioSeeder,
    EstadoCitaSeeder estadoCitaSeeder,
    TipoCitaSeeder tipoCitaSeeder,
    CitaSeeder citaSeeder) : IDbSeeder
{
    public async Task SeedAllAsync()
    {
        await sexoSeeder.SeedAsync();
        await tipoDocumentoSeeder.SeedAsync();
        await rolSeeder.SeedAsync();
        await permisoSeeder.SeedAsync();
        await rolPermisoSeeder.SeedAsync();
        await cargoSeeder.SeedAsync();
        await especialidadSeeder.SeedAsync();
        await personaSeeder.SeedAsync();
        await empleadoSeeder.SeedAsync();
        await usuarioSeeder.SeedAsync();
        await medicoSeeder.SeedAsync();
        await pacienteSeeder.SeedAsync();
        await horarioSeeder.SeedAsync();
        await estadoCitaSeeder.SeedAsync();
        await tipoCitaSeeder.SeedAsync();
        await citaSeeder.SeedAsync();
    }
}
