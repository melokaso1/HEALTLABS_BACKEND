# Domain — Modelo de dominio compartido

**Audiencia:** Todo el equipo backend (base para Sahiam + Yeison/Santiago)

## Propósito

Definir el modelo de negocio puro: entidades, value objects e interfaces de repositorio/servicios. Sin dependencias de EF Core, ASP.NET ni configuración de infraestructura. Es el contrato que Application e Infrastructure comparten.

## Qué se hizo / cambió en la entrega reciente

- **Entidades `*Entity`:** ~30 entidades (Persona, Usuario, Cita, Medico, Horario, Sesion, TokenRecuperacion, etc.) en `Domain/Entities/`.
- **Value objects:** validación en el dominio — `RolValueObject` (Administrador / Profesional / Recepcionista), `EmailValueObject`, `PasswordValueObject`, `NombreValueObject`, `TelefonoValueObject`, etc. en `Domain/ValueObjects/`.
- **Rename de rol:** canónico `Profesional` en `RolValueObject.Profesional`; alineado con `AppRoles.Medico` en Api y migración de seed `Médico` → `Profesional`.
- **Interfaces de persistencia:** `IGenericRepository<TEntity>` con CRUD + `FindAsync`/`AnyAsync`; repos específicos `IAntecedentesRepository`, `IPersonaDireccionRepository`.
- **Interfaces de auth:** `IJwtTokenGenerator`, `IPasswordHasher` — implementadas en Infrastructure.
- **Modelo de agenda:** `HorarioEntity` — jornada por médico y fecha (`HoraEntrada`, `HoraSalida`, `SalidaAlmuerzo`, `RetornoActividades`).
- **Auth en dominio:** `UsuarioEntity`, `SesionEntity`, `LoginIntentoEntity`, `TokenRecuperacionEntity` para flujos JWT y recuperación de contraseña.
- **Sin referencias externas:** el `.csproj` de Domain no referencia Application, Api ni Infrastructure.

## Relación con otras capas

```
Domain (entidades + interfaces)
    ↑                    ↑
Application          Infrastructure
(use cases)          (EF configs, repos, JWT, seeders)
    ↑
  Api (solo DTOs vía Application; no referencia Domain directamente en controllers)
```

- **Application** consume entidades e interfaces.
- **Infrastructure** implementa interfaces y mapea entidades a tablas PostgreSQL.
- **Api** idealmente no referencia Domain; si necesita tipos de dominio, hacerlo vía DTOs en Application.

## Qué debe cuidar el equipo

1. **No agregar dependencias de framework** (EF, MVC, JWT packages) en este proyecto.
2. **Roles:** usar siempre `RolValueObject` / constantes `Administrador`, `Profesional`, `Recepcionista`; no hardcodear "Médico".
3. **Nueva tabla/entidad:** crear `*Entity` aquí primero; luego configuración EF en Infrastructure y use case en Application.
4. **Value objects:** encapsular validación (email, documento, rol) en VO en lugar de validar solo en API.
5. **Interfaces nuevas:** si un query no cabe en `IGenericRepository<>`, agregar interfaz específica aquí (como Antecedentes o PersonaDireccion).
6. **Horario:** cambios al modelo de jornada impactan migraciones, seeders y use cases de citas — coordinar con DB.

## Archivos clave

| Ruta | Descripción |
|------|-------------|
| `Domain/Entities/` | Entidades del modelo (~30 archivos) |
| `Domain/ValueObjects/RolValueObject.cs` | Roles canónicos del sistema |
| `Domain/Entities/HorarioEntity.cs` | Agenda / jornada del profesional |
| `Domain/Entities/UsuarioEntity.cs` | Usuario, credenciales, rol |
| `Domain/Entities/CitaEntity.cs` | Citas y estados |
| `Domain/Interfaces/IGenericRepository.cs` | Contrato CRUD genérico |
| `Domain/Interfaces/IJwtTokenGenerator.cs` | Generación de tokens |
| `Domain/Interfaces/IPasswordHasher.cs` | Hash de contraseñas |
| `Domain/Interfaces/IAntecedentesRepository.cs` | Queries de antecedentes |
| `Domain/Interfaces/IPersonaDireccionRepository.cs` | Direcciones por persona |

## Convenciones

- Entidades con constructor privado vacío para EF y constructor público con invariantes.
- Métodos `Update(...)` en entidades para mutaciones controladas.
- GUIDs como identificadores (`Id`).
