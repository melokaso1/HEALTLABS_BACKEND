# Infrastructure — Persistencia, auth y seeders

**Audiencia:** Yeison y Santiago (base de datos, EF Core, migraciones, repositorios)

## Propósito

Implementar el acceso a datos (PostgreSQL/Supabase vía Npgsql), configuraciones EF, migraciones, seeders iniciales y servicios de infraestructura (JWT, hash de contraseñas). Materializa las interfaces definidas en Domain.

## Qué se hizo / cambió en la entrega reciente

- **`AppDbContext`:** `DbSet<>` para todas las entidades; `OnModelCreating` aplica configuraciones desde el ensamblado (`ApplyConfigurationsFromAssembly`).
- **31 configuraciones EF** en `Infrastructure/EntityConfigurations/` (incluye `HorarioConfiguration`, `UsuarioConfiguration`, `CitaConfiguration`, etc.).
- **Migración inicial:** `Persistence/Migrations/20260811135737_InitialCreate` + `AppDbContextModelSnapshot`.
- **Repositorios:** `GenericRepository<T>` implementa `IGenericRepository<T>`; repos especializados `AntecedentesRepository`, `PersonaDireccionRepository`.
- **Auth infra:** `JwtTokenGenerator` (`IJwtTokenGenerator`) y `PasswordHasher` (`IPasswordHasher`) en `Infrastructure/Auth/`.
- **Seeders:** cadena orquestada por `DbSeeder` — Sexo, TipoDocumento, **Rol** (con rename legacy `Médico` → `Profesional`), Permiso, RolPermiso, Cargo, Persona, Empleado, **Usuario** (contraseñas hasheadas con `IPasswordHasher`), EstadoCita, TipoCita.
- **`DependencyInjection.AddInfrastructure`:** registra `AppDbContext` **Scoped** con Npgsql (nunca Singleton), repos Scoped, JWT/hasher Singleton, y `AddPersistenceSeeders()`.
- **Pool Npgsql acotado:** `NormalizeNpgsqlConnectionString` fuerza `Pooling=true`, `Maximum Pool Size≤10` y, en puerto `6543`, `Max Auto Prepare=0` (Transaction pooler).
- **Connection string desde `.env`:** `ConnectionStrings__DefaultConnection` (Supabase/Postgres); no guardar secretos en `appsettings.json`.
- **Migrate + seed al arrancar:** la Api ejecuta `MigrateAsync()` y `SeedAllAsync()` al iniciar (ver `Api/Program.cs`) en un `IServiceScope` que se dispone al terminar.

## Relación con otras capas

```
Application (use case) → Domain (IGenericRepository, IJwtTokenGenerator) ← Infrastructure (implementación con EF + Auth)
                                                              ↑
                                                         PostgreSQL / Supabase
```

- **Domain:** solo implementa interfaces; no modifica entidades de negocio salvo extensiones de persistencia en configs.
- **Application:** no conoce `AppDbContext`; solo ve interfaces.
- **Api:** registra Infrastructure en DI; no accede a DbContext desde controllers.

## Qué deben cuidar Yeison y Santiago

1. **Variables de entorno obligatorias** en `.env` (raíz del repo o carpeta Api):
   - `ConnectionStrings__DefaultConnection` — cadena Npgsql hacia Supabase/Postgres
   - `JWT__Key`, `JWT__Issuer`, `JWT__Audience` (opcional: `JWT__DurationInMinutes`, `JWT__RefreshDurationInDays`)
1b. **Supabase pooler (evitar `EMAXCONNSESSION`):**
   - **Recomendado (API):** Transaction pooler — host `*.pooler.supabase.com`, **puerto `6543`**, `Pooling=true`.
   - **Evitar para carga concurrente:** Session pooler / puerto `5432` en free tier (`pool_size` ≈ 15) — si lo usas, mantén `Maximum Pool Size` bajo (5–10) y pocas instancias de la API.
   - Parámetros sugeridos (sin password):  
     `Host=YOUR_PROJECT.pooler.supabase.com;Port=6543;Database=postgres;Username=postgres.YOUR_REF;SSL Mode=Require;Pooling=true;Maximum Pool Size=10;Timeout=15;`  
     En `6543` el código ya aplica `Max Auto Prepare=0`.
2. **Nueva entidad:** agregar `DbSet` en `AppDbContext`, crear `*Configuration.cs`, generar migración, no olvidar orden en seeders si aplica.
3. **Migraciones:** desde la raíz del repo:
   ```bash
   dotnet ef migrations add NombreMigracion --project Infrastructure --startup-project Api
   dotnet ef database update --project Infrastructure --startup-project Api
   ```
   En desarrollo, `dotnet run --project Api` también aplica migraciones al arrancar.
4. **Seeders idempotentes:** cada seeder comprueba si ya hay datos (`AnyAsync`) antes de insertar; `RolSeeder` además corrige filas legacy "Médico".
5. **No poner lógica de negocio** en repositorios; queries complejas sí, reglas de citas/horarios no.
6. **Usuarios de prueba:** `UsuarioSeeder` crea admin/profesional/recepcionista con passwords hasheadas (ver seeder para usernames; no documentar passwords en código commiteado).
7. **Índices y FKs:** definir en `EntityConfigurations/`; revisar impacto en Supabase antes de merge.

## Archivos clave

| Ruta | Descripción |
|------|-------------|
| `Infrastructure/DependencyInjection.cs` | Registro DbContext, repos, auth, seeders |
| `Infrastructure/Persistence/Context/AppDbContext.cs` | Contexto EF principal |
| `Infrastructure/Persistence/Repositories/GenericRepository.cs` | Repo genérico |
| `Infrastructure/Persistence/Repositories/AntecedentesRepository.cs` | Repo especializado |
| `Infrastructure/Persistence/Repositories/PersonaDireccionRepository.cs` | Repo especializado |
| `Infrastructure/Auth/JwtTokenGenerator.cs` | Emisión de access/refresh tokens |
| `Infrastructure/Auth/PasswordHasher.cs` | BCrypt/hash de contraseñas |
| `Infrastructure/Persistence/Seeders/DbSeeder.cs` | Orquestador de seed |
| `Infrastructure/Persistence/Seeders/RolSeeder.cs` | Roles + rename Médico→Profesional |
| `Infrastructure/Persistence/Seeders/UsuarioSeeder.cs` | Usuarios iniciales hasheados |
| `Infrastructure/Persistence/Seeders/SeederDependencyInjection.cs` | Registro DI de seeders |
| `Infrastructure/EntityConfigurations/` | Fluent API por entidad |
| `Infrastructure/Persistence/Migrations/` | Historial de migraciones EF |

## Orden de seed (`DbSeeder.SeedAllAsync`)

Sexo → TipoDocumento → Rol → Permiso → RolPermiso → Cargo → Persona → Empleado → Usuario → EstadoCita → TipoCita

Respetar dependencias FK al agregar nuevos seeders.
