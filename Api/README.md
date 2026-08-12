# Api — Capa de presentación HTTP

**Audiencia:** Felipe (APIs, controllers, Swagger, autenticación HTTP)

## Propósito

Exponer la API REST de HealtLab. Traduce peticiones HTTP en llamadas a use cases de Application y devuelve respuestas JSON. No contiene lógica de negocio ni acceso directo a base de datos.

## Qué se hizo / cambió en la entrega reciente

- **Controllers delgados:** los ~30 controllers ya no usan `AppDbContext`; inyectan use cases (`CitaCrudUseCase`, `LoginUseCase`, etc.) y solo mapean HTTP ↔ DTO.
- **`AuthController`:** endpoints `login`, `refresh`, `logout`, `solicitar-recuperacion` y `reset-password`; delega en use cases de `Application/UseCases/Auth/`.
- **`Program.cs`:** carga `.env` con DotNetEnv (busca hacia arriba desde el content root), valida `ConnectionStrings__DefaultConnection` y `JWT__*`, registra JWT Bearer, Swagger con esquema Bearer, CORS `Frontend`, y llama `AddApplication()` + `AddInfrastructure()`.
- **Migraciones y seed al arrancar:** al iniciar la API se ejecutan `MigrateAsync()` y `IDbSeeder.SeedAllAsync()` (ver `Program.cs` líneas 93–108).
- **`ExceptionHandlingMiddleware`:** captura excepciones no controladas y responde JSON con `statusCode` y `message` (404/400/401 según tipo de excepción).
- **Autorización por roles:** constantes en `Api/Security/AppRoles.cs` — `Administrador`, `Profesional` (antes "Médico"), `Recepcionista`; combinaciones `Staff` y `Todos` para `[Authorize(Roles = ...)]`.
- **Reglas de citas en HTTP:** `CitasController` expone `cancelar`, `reprogramar` y `delete` delegando en `CitaCrudUseCase`.
- **`ReportesController`:** consultas de citas por rango, conteo por estado y por médico (solo `Staff`).

## Relación con otras capas

```
Cliente HTTP → Api (controllers, middleware, auth) → Application (use cases) → Domain (interfaces) → Infrastructure (EF, JWT, repos)
```

- **Application:** recibe DTOs y ejecuta reglas de negocio.
- **Domain:** no se referencia directamente desde controllers (salvo DTOs que Application expone).
- **Infrastructure:** se registra en DI desde `Program.cs`; la Api no instancia repos ni `DbContext`.

## Qué debe cuidar Felipe

1. **No inyectar `AppDbContext` ni repositorios** en controllers; agregar o extender use cases en Application.
2. **Proteger endpoints nuevos** con `[Authorize]` y el rol correcto (`AppRoles.Admin`, `AppRoles.Medico`, `AppRoles.Recepcionista`, `Staff` o `Todos`).
3. **Mantener controllers delgados:** validación HTTP mínima (fechas, query params); reglas de negocio en use cases.
4. **Probar en Swagger (Development):** `POST /api/Auth/login` → copiar `accessToken` → botón **Authorize** → `Bearer {token}`.
5. **No commitear secretos:** JWT y connection string viven en `.env` (variables `JWT__Key`, `JWT__Issuer`, `JWT__Audience`, `ConnectionStrings__DefaultConnection`).
6. **Errores de dominio:** preferir que los use cases lancen excepciones conocidas (`KeyNotFoundException`, `InvalidOperationException`) para que el middleware las traduzca; o devolver `UseCaseResult` como en `AuthController`.

## Archivos clave

| Archivo | Rol |
|---------|-----|
| `Api/Program.cs` | Bootstrap: `.env`, JWT, Swagger, CORS, DI, migrate/seed |
| `Api/Controllers/AuthController.cs` | Autenticación JWT |
| `Api/Controllers/CitasController.cs` | CRUD + cancelar/reprogramar |
| `Api/Controllers/ReportesController.cs` | Reportes de citas |
| `Api/Security/AppRoles.cs` | Constantes de roles para `[Authorize]` |
| `Api/Middlewares/ExceptionHandlingMiddleware.cs` | Manejo global de excepciones |
| `Api/Controllers/*.cs` | Un controller por módulo (~30 archivos) |

## Cómo ejecutar y probar

```bash
# Desde la raíz del repo: crear .env con ConnectionStrings__DefaultConnection (Supabase/Postgres) y JWT__*
dotnet run --project Api
```

Swagger UI: `https://localhost:{puerto}/swagger` (solo en Development).
