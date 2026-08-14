# HealtLab — Sistema de Gestión de Citas Médicas

Backend para un sistema de salud integral y simplificado: usuarios, agendamiento de citas, expedientes clínicos y permisos por roles (RBAC).

## Stack

- C# / .NET
- Entity Framework Core (Code-First)
- PostgreSQL vía Supabase (pooler compartido)
- JWT + SignalR + Swagger (Development) / Insomnia

## Arquitectura (Clean Architecture)

| Capa | Responsable | Contenido |
|------|-------------|-----------|
| **Domain** | Equipo (base) | Entidades, value objects, interfaces. Sin dependencias externas. |
| **Application** | Sahiam | Use cases, DTOs, reglas de negocio. |
| **Infrastructure** | Yeison / Santiago | EF Core, repositorios, seeders, JWT, conexión Supabase. |
| **Api** | Felipe | Controllers, middleware, auth HTTP, SignalR hub, Swagger. |

```
Cliente HTTP / SignalR
    │
    ▼
Api (controllers, middleware, auth, hubs)
    │  usa
    ▼
Application (use cases, DTOs)
    │  depende de
    ▼
Domain (entidades, interfaces) ◄──── implementa ──── Infrastructure (EF Core, repos, JWT, seeders)
                                                              │
                                                              ▼
                                                    PostgreSQL / Supabase
```

- **Domain** no depende de otras capas.
- **Application** solo conoce Domain; no referencia EF ni `DbContext`.
- **Infrastructure** implementa interfaces de Domain.
- **Api** consume Application (use cases); no inyecta `DbContext` ni repos directamente.

Este README es la fuente de verdad para **cómo ejecutar la API**, autenticación, formatos y endpoints principales.

## Cómo ejecutar

Puertos (`Api/Properties/launchSettings.json`):

| Perfil | URL |
|--------|-----|
| `http` | http://localhost:5077 |
| `https` | https://localhost:7047 (también escucha http://localhost:5077) |

```bash
# 1. Clonar el repositorio
git clone <url-del-repo>
cd HealtLab

# 2. Crear .env en la raíz del repo (o en Api/). Ver sección siguiente.

# 3. Migraciones (opcional: la Api también migra al arrancar)
dotnet ef database update --project Infrastructure --startup-project Api

# 4. Ejecutar
dotnet run --project Api
# o con HTTPS:
dotnet run --project Api --launch-profile https
```

Al arrancar (`Api/Program.cs`): `MigrateAsync()` + `SeedAllAsync()`.

### Migraciones EF

```bash
dotnet ef database update --project Infrastructure --startup-project Api
```

Para crear una migración nueva (desde Infrastructure / equipo de datos):

```bash
dotnet ef migrations add NombreMigracion --project Infrastructure --startup-project Api
```

## Variables de entorno (`.env`)

`Program.cs` carga el primer `.env` encontrado subiendo desde el content root de Api (raíz del repo o carpeta `Api/`). Luego añade variables de entorno al configuration.

Variables obligatorias (doble guion bajo = jerarquía de configuración):

```env
ConnectionStrings__DefaultConnection=Host=...;Port=6543;Database=postgres;Username=...;Password=...;SSL Mode=Require;Trust Server Certificate=true
JWT__Key=<clave-secreta-larga>
JWT__Issuer=HealtLab
JWT__Audience=HealtLab
```

**No commitear secretos.** Nunca pegues contraseñas reales en este README ni en `appsettings.json`.

### Supabase / pooler

- Usar **Transaction pooler**, puerto **6543** (evita `EMAXCONNSESSION` del free tier / session mode).
- En Infrastructure, el pool de Npgsql se normaliza a **Maximum Pool Size = 10** y, en puerto 6543, se desactivan prepared statements (`MaxAutoPrepare = 0`).

## Autenticación JWT

```http
POST /api/Auth/login
Content-Type: application/json

{
  "usernameOrEmail": "admin",
  "password": "Admin123!"
}
```

Respuesta incluye `accessToken` (y datos de sesión). En peticiones protegidas:

```http
Authorization: Bearer {accessToken}
```

Otros endpoints de auth: `refresh`, `logout`, `solicitar-recuperacion`, `reset-password`, `cambiar-password` (este último autenticado).

### Usuarios seed (solo si la tabla `usuarios` está vacía)

| Usuario | Contraseña | Rol (claim JWT) |
|---------|------------|-----------------|
| `admin` | `Admin123!` | Administrador |
| `recepcion` | `Recepcion123!` | Recepcionista |
| `medico1` | `Medico123!` | Profesional |

### Roles

Definidos en Domain (`RolValueObject`) y en Api (`Api/Security/AppRoles.cs`):

| Constante | Claim / valor |
|-----------|----------------|
| `AppRoles.Admin` | `Administrador` |
| `AppRoles.Recepcionista` | `Recepcionista` |
| `AppRoles.Medico` | `Profesional` |
| `AppRoles.Staff` | `Administrador,Recepcionista` |
| `AppRoles.Todos` | los tres roles |

**Staff** = Administrador + Recepcionista. El string del claim del médico es **`Profesional`**, no "Médico".

El **Administrador** es personal administrativo (no médico): no crea expediente clínico como Profesional.

## SignalR

Hub de notificaciones en tiempo real:

| | |
|--|--|
| Ruta | `/hubs/notifications` |
| Auth | JWT Bearer; en WebSockets también por query `access_token` (ver `OnMessageReceived` en `Program.cs`) |

El frontend reconecta al hub cuando hay token de sesión (tras login).

## Formatos de fecha y hora

| Tipo | Formato JSON | Ejemplo |
|------|--------------|---------|
| `TimeOnly` | **HH:mm** (sin segundos) | `"08:30"` |
| `DateOnly` | **YYYY-MM-DD** | `"2026-08-14"` |

Las horas se serializan con `TimeOnlyHhMmJsonConverter`. Enviar `"08:30:00"` o un número falla.

## Swagger

Solo en **Development** (`UseSwagger` / `UseSwaggerUI` condicionados al entorno).

1. Abrir http://localhost:5077/swagger (o https://localhost:7047/swagger).
2. `POST /api/Auth/login` → copiar `accessToken`.
3. Botón **Authorize** → `Bearer {token}` (o solo el token si Swagger antepone Bearer).

## Endpoints clave por rol

### Auth (anónimo / autenticado)

| Método | Ruta | Notas |
|--------|------|--------|
| POST | `/api/Auth/login` | `{ usernameOrEmail, password }` |
| POST | `/api/Auth/logout` | Requiere Bearer |

### Catálogos (lectura Staff)

| Método | Ruta | Roles |
|--------|------|--------|
| GET | `/api/Sexos` | Staff |
| GET | `/api/TiposDocumento` | Staff |

Escritura de estos catálogos: solo Administrador.

### Especialidades (Administrador)

Seed (`EspecialidadSeeder`): Medicina General, Cardiología, Pediatría, Ginecología, Dermatología, Ortopedia (solo si la tabla está vacía).

| Método | Ruta | Notas |
|--------|------|--------|
| GET / POST | `/api/Especialidades`… | Solo Administrador |

### Pacientes (Staff; delete solo Admin)

| Método | Ruta | Notas |
|--------|------|--------|
| GET | `/api/Pacientes` | Listado |
| GET | `/api/Pacientes/buscar?tipoDocumento={guid}&numeroDocumento={texto}` | 404 si no existe; 409 si inactivo |
| GET | `/api/Pacientes/{id}` | Por id |
| POST | `/api/Pacientes/completo` | Alta compuesta (recomendado) |
| POST | `/api/Pacientes` | Alta 2 pasos (requiere `personaId`) |
| PUT | `/api/Pacientes/{id}` | Actualizar |
| DELETE | `/api/Pacientes/{id}` | Solo Administrador |

> No existe `GET /api/Pacientes/por-documento/{numero}`. La búsqueda por documento es **`/buscar`** con query params.

#### Alta de paciente: compuesto vs 2 pasos

**Compuesto** — un solo request (`CreatePacienteCompletoDto`):

```http
POST /api/Pacientes/completo
```

Crea persona (+ teléfono/dirección opcionales) y paciente (`tipoSangre`, `activo`). Conflicto (409) si el documento ya existe.

**2 pasos:**

1. `POST /api/Personas` (Staff) → obtener `id` de persona.
2. `POST /api/Pacientes` con `{ "personaId": "...", "activo": true, "tipoSangre": "O+" }`.

### Personal / usuarios (solo Administrador) — flujo unificado

El alta de staff (incl. profesionales) va por **`POST /api/Usuarios/completo`**: crea Persona + Empleado + Usuario y, si el rol es Profesional, también Medico + especialidad.

| Método | Ruta | Notas |
|--------|------|--------|
| GET/POST/PUT/DELETE | `/api/Usuarios`… | CRUD de usuarios |
| POST | `/api/Usuarios/completo` | Alta unificada de personal (recomendado) |
| PUT | `/api/Usuarios/{id}/activo` | Body: `{ "activo": true \| false }` |

#### `POST /api/Usuarios/completo` (`CreateUsuarioCompletoDto`)

Campos principales: `persona`, `rolId`, `username`, `email`, `password`, `activo`, `fechaIngreso`, `telefono`, `direccion`, `ciudad`, `registroProfesional`, `especialidadId`.

Reglas por rol:

| Rol | Teléfono / dirección | Licencia (`registroProfesional`) + `especialidadId` | Crea `Medico` |
|-----|----------------------|-----------------------------------------------------|---------------|
| Administrador | Opcionales | No | No |
| Recepcionista | **Obligatorios** | No | No |
| Profesional | **Obligatorios** | **Obligatorios** | Sí (+ `medico_especialidad`) |

### Médicos

| Método | Ruta | Roles |
|--------|------|--------|
| GET | `/api/Medicos`, `/api/Medicos/{id}` | Todos |
| POST | `/api/Medicos` | Admin — solo con `empleadoId` existente (caso avanzado) |
| POST | `/api/Medicos/completo` | **No usar** — responde 400 y redirige al flujo unificado (`POST /api/Usuarios/completo`) |
| PUT / DELETE | `/api/Medicos/{id}` | Admin |

> No hay un camino “médico huérfano” vía `Medicos/completo`. Todo profesional nuevo debe tener cuenta de usuario.

### Citas

Duración máxima: **30 minutos** (`CitaSchedulingRules.MaxAppointmentDurationMinutes`). Si falta `horaFin` o la ventana supera 30 min, se normaliza a inicio + 30 min.

| Método | Ruta | Roles |
|--------|------|--------|
| GET | `/api/Citas?desde=YYYY-MM-DD&hasta=YYYY-MM-DD` | Todos — el **Profesional** solo ve sus citas |
| GET | `/api/Citas/{id}` | Todos |
| POST | `/api/Citas` | Staff — agendar |
| POST | `/api/Citas/{id}/cancelar` | Staff |
| POST | `/api/Citas/{id}/reprogramar` | Staff |
| POST | `/api/Citas/{id}/no-asistio` | Staff o Profesional |
| DELETE | `/api/Citas/{id}` | Staff |

Horas de cita: `horaInicio` / `horaFin` en **HH:mm**; `fecha` en **YYYY-MM-DD**.

### Reportes (Staff)

- `GET /api/Reportes/citas?desde=&hasta=&medicoId=`
- `GET /api/Reportes/citas/por-estado?desde=&hasta=`
- `GET /api/Reportes/citas/por-medico?desde=&hasta=`

### Resumen por perfil

**Administrador:** usuarios (incl. alta completa y activo/inactivo), especialidades, médicos (lectura/CRUD acotado), pacientes, citas, catálogos, reportes, empleados, roles/permisos. No es un doctor clínico.

**Recepcionista (Staff):** pacientes (listar, buscar, completo), citas (agendar/cancelar/reprogramar), personas/teléfonos/direcciones, antecedentes/alergias (escritura), Sexos y TiposDocumento (lectura), reportes.

**Profesional:** sus citas (filtro automático), médicos (lectura), detalle de cita / diagnósticos / tratamientos (escritura clínica), marcar no-asistió.

## Módulos de base de datos (resumen)

1. **Seguridad:** usuario, rol, permiso, rol_permiso, sesion, login_intento, token_recuperacion.
2. **Personas:** persona, tipo_documento, sexo, direcciones, teléfonos, empleado, paciente, medico, cargo.
3. **Citas:** horario, especialidad, medico_especialidad, cita, tipo_cita, estado_cita, cita_historial_estado.
4. **Clínica:** antecedente, paciente_alergia, detalle_cita, diagnostico, detalle_diagnostico, tratamiento, tratamiento_posologia, atencion_tratamiento.

## Convenciones del equipo

- Secretos solo en `.env` (`ConnectionStrings__DefaultConnection`, `JWT__*`).
- Funcionalidad nueva: Domain → Application → Infrastructure → Api (autorización con `AppRoles`).
- Use cases con sufijo `UseCase` (registro en DI de Application).
- Roles en strings: `Administrador` / `Profesional` / `Recepcionista`.
- Alta de personal: `POST /api/Usuarios/completo` (no `POST /api/Medicos/completo`).
- SignalR: hub en `/hubs/notifications` (token también por query `access_token` en hubs).
