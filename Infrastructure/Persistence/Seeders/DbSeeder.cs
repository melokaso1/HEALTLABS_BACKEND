namespace Infrastructure.Persistence.Seeders;

public class DbSeeder(
    SexoSeeder sexoSeeder,
    TipoDocumentoSeeder tipoDocumentoSeeder,
    RolSeeder rolSeeder,
    PermisoSeeder permisoSeeder,
    RolPermisoSeeder rolPermisoSeeder,
    CargoSeeder cargoSeeder,
    PersonaSeeder personaSeeder,
    EmpleadoSeeder empleadoSeeder,
    UsuarioSeeder usuarioSeeder,
    EstadoCitaSeeder estadoCitaSeeder,
    TipoCitaSeeder tipoCitaSeeder) : IDbSeeder
{
    public async Task SeedAllAsync()
    {
        await sexoSeeder.SeedAsync();
        await tipoDocumentoSeeder.SeedAsync();
        await rolSeeder.SeedAsync();
        await permisoSeeder.SeedAsync();
        await rolPermisoSeeder.SeedAsync();
        await cargoSeeder.SeedAsync();
        await personaSeeder.SeedAsync();
        await empleadoSeeder.SeedAsync();
        await usuarioSeeder.SeedAsync();
        await estadoCitaSeeder.SeedAsync();
        await tipoCitaSeeder.SeedAsync();
    }
}
