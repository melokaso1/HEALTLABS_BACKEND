# Application — Casos de uso y DTOs

**Audiencia:** Sahiam (backend, reglas de negocio, use cases)

## Propósito

Orquestar la lógica de aplicación: validaciones, flujos de negocio y mapeo entre DTOs y entidades de Domain. Es el único lugar donde debe vivir la lógica que no es HTTP ni persistencia.

## Qué se hizo / cambió en la entrega reciente

- **Use cases de autenticación:** `LoginUseCase`, `RefreshTokenUseCase`, `LogoutUseCase`, `SolicitarRecuperacionUseCase`, `ResetPasswordUseCase` — lockout, sesiones, tokens JWT vía `IJwtTokenGenerator` e `IPasswordHasher`.
- **Patrón canónico (estilo `Horarios/`):** un archivo = una operación — `CreateXUseCase`, `GetAllXUseCase`, `GetXByIdUseCase`, `UpdateXUseCase`, `DeleteXUseCase` con `ExecuteAsync`. Se eliminaron los `*CrudUseCase` y carpetas singular duplicadas (`Horario` vs `Horarios`, etc.).
- **Reglas de citas:** en `UseCases/Citas/` (`CreateCitaUseCase`, `UpdateCitaUseCase`, `CancelCitaUseCase`, `ReprogramarCitaUseCase`) + helper `CitaSchedulingRules` (jornada del médico sin fecha en horario).
- **Horario:** jornada por médico (`MedicoId` unique); sin `Fecha` (la fecha va en `cita`).
- **Reportes:** `GetCitasPorRangoUseCase`, `GetConteoCitasPorEstadoUseCase`, `GetConteoCitasPorMedicoUseCase` en `UseCases/Reportes/`.
- **DTOs Create/Update** por módulo en `Application/DTOs/` (más DTOs de Auth y Reportes).
- **`DependencyInjection.AddApplication`:** registra automáticamente todas las clases públicas que terminan en `UseCase` (y casos especiales `GetAntecedentesByType`, `GetPersonaDireccionByPersonId`) como `Scoped`.
- **Patrón de resultado:** `UseCaseResult<T>` en `Application/Common/` para flujos Auth (éxito/error con código HTTP).
- **Roles alineados:** lógica de negocio usa entidades `RolEntity` y `RolValueObject.Profesional` (rename desde "Médico").

## Relación con otras capas

```
Api (controller) → Application (use case + DTO) → Domain (entidad + IGenericRepository / interfaces) ← Infrastructure (implementaciones)
```

- **Depende de Domain:** entidades, value objects e interfaces (`IGenericRepository<>`, `IJwtTokenGenerator`, `IPasswordHasher`, repos específicos).
- **No depende de Infrastructure ni Api:** sin referencias a EF, `DbContext`, `ControllerBase` ni middleware.
- **Infrastructure implementa** las interfaces que los use cases consumen.

## Qué debe cuidar Sahiam

1. **Nunca usar EF Core aquí** — solo interfaces de `Domain/Interfaces/`.
2. **Nuevo endpoint = nuevo o extendido use case**, no lógica en el controller.
3. **DTOs de entrada/salida** en `Application/DTOs/{Modulo}/`; no exponer entidades con navegaciones sensibles si no hace falta.
4. **Reglas de negocio** (validar horarios, estados de cita, permisos de rol) van en el use case, no en Infrastructure.
5. **Registrar use cases:** nombrar clases con sufijo `UseCase` para que `AddApplication` las detecte; si el nombre es distinto, agregarlo manualmente en `DependencyInjection.cs`.
6. **Excepciones intencionales:** `KeyNotFoundException` → 404, `InvalidOperationException` / `ArgumentException` → 400; el middleware de Api las traduce.
7. **Horarios:** validar coherencia de jornada (`HoraEntrada`, `HoraSalida`, almuerzo) en `UseCases/Horarios/`. Un médico = un horario; no crear carpetas singular duplicadas.

## Archivos clave

| Ruta | Descripción |
|------|-------------|
| `Application/DependencyInjection.cs` | Registro automático de use cases |
| `Application/Common/UseCaseResult.cs` | Resultado tipado para Auth |
| `Application/UseCases/Horarios/*.cs` | Patrón de referencia (Create/Get/Update/Delete) |
| `Application/UseCases/Auth/*.cs` | Login, refresh, logout, recuperación |
| `Application/UseCases/Citas/*.cs` | Citas + cancelar/reprogramar + `CitaSchedulingRules` |
| `Application/UseCases/Reportes/*.cs` | Consultas de reportes |
| `Application/DTOs/` | DTOs por módulo (~40 carpetas) |

## Flujo típico al agregar funcionalidad

1. Definir o reutilizar entidad/interfaz en **Domain**.
2. Crear DTOs en **Application/DTOs/**.
3. Implementar use case inyectando `IGenericRepository<>` o repo específico.
4. Exponer desde controller en **Api** (solo inyección + mapeo HTTP).
