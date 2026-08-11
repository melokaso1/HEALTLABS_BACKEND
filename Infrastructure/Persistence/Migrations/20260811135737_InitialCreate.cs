using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cargo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    NivelJerarquico = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cargo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "diagnostico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CodigoCie10 = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diagnostico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "especialidad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_especialidad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "estado_cita",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estado_cita", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "permiso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Modulo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permiso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rol",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NombreRol = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sexo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sexo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tipo_cita",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DuracionMinutos = table.Column<int>(type: "integer", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipo_cita", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tipo_documento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipo_documento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tratamiento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tratamiento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rol_permiso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RolId = table.Column<Guid>(type: "uuid", nullable: false),
                    PermisoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rol_permiso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rol_permiso_permiso_PermisoId",
                        column: x => x.PermisoId,
                        principalTable: "permiso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rol_permiso_rol_RolId",
                        column: x => x.RolId,
                        principalTable: "rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "persona",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TipoDocumentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    NumeroDocumento = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: true),
                    SexoId = table.Column<Guid>(type: "uuid", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persona", x => x.Id);
                    table.ForeignKey(
                        name: "FK_persona_sexo_SexoId",
                        column: x => x.SexoId,
                        principalTable: "sexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_persona_tipo_documento_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "tipo_documento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tratamiento_posologia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TratamientoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Dosis = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Frecuencia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DuracionDias = table.Column<int>(type: "integer", nullable: true),
                    Indicaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tratamiento_posologia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tratamiento_posologia_tratamiento_TratamientoId",
                        column: x => x.TratamientoId,
                        principalTable: "tratamiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "empleado",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonaId = table.Column<Guid>(type: "uuid", nullable: false),
                    CargoId = table.Column<Guid>(type: "uuid", nullable: false),
                    FechaIngreso = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaRetiro = table.Column<DateOnly>(type: "date", nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empleado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_empleado_cargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_empleado_persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "paciente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paciente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_paciente_persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "persona_direccion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Direccion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Ciudad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Tipo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Principal = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persona_direccion", x => x.Id);
                    table.CheckConstraint("CK_persona_direccion_tipo", "\"Tipo\" IS NULL OR \"Tipo\" IN ('residencia', 'trabajo')");
                    table.ForeignKey(
                        name: "FK_persona_direccion_persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "persona_telefono",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Telefono = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Tipo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Principal = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persona_telefono", x => x.Id);
                    table.CheckConstraint("CK_persona_telefono_tipo", "\"Tipo\" IS NULL OR \"Tipo\" IN ('movil', 'fijo', 'trabajo')");
                    table.ForeignKey(
                        name: "FK_persona_telefono_persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpleadoId = table.Column<Guid>(type: "uuid", nullable: false),
                    RegistroProfesional = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medico_empleado_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "empleado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpleadoId = table.Column<Guid>(type: "uuid", nullable: false),
                    RolId = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UltimoLogin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IntentosFallidos = table.Column<int>(type: "integer", nullable: false),
                    BloqueadoHasta = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DebeCambiarPassword = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TokenVersion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usuario_empleado_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "empleado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_usuario_rol_RolId",
                        column: x => x.RolId,
                        principalTable: "rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "paciente_alergia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    Sustancia = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Reaccion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Severidad = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paciente_alergia", x => x.Id);
                    table.CheckConstraint("CK_paciente_alergia_severidad", "\"Severidad\" IS NULL OR \"Severidad\" IN ('leve', 'moderada', 'severa')");
                    table.ForeignKey(
                        name: "FK_paciente_alergia_paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "paciente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "horario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    HoraEntrada = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    HoraSalida = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    SalidaAlmuerzo = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    RetornoActividades = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_horario", x => x.Id);
                    table.CheckConstraint("CK_horario_jornada", "\"HoraEntrada\" < \"SalidaAlmuerzo\" AND \"SalidaAlmuerzo\" < \"RetornoActividades\" AND \"RetornoActividades\" < \"HoraSalida\"");
                    table.ForeignKey(
                        name: "FK_horario_medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "medico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medico_especialidad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uuid", nullable: false),
                    EspecialidadId = table.Column<Guid>(type: "uuid", nullable: false),
                    Principal = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medico_especialidad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medico_especialidad_especialidad_EspecialidadId",
                        column: x => x.EspecialidadId,
                        principalTable: "especialidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_medico_especialidad_medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "medico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "antecedente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioRegistroId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_antecedente", x => x.Id);
                    table.CheckConstraint("CK_antecedente_tipo", "\"Tipo\" IN ('personal', 'familiar', 'quirurgico')");
                    table.ForeignKey(
                        name: "FK_antecedente_paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "paciente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_antecedente_usuario_UsuarioRegistroId",
                        column: x => x.UsuarioRegistroId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "cita",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uuid", nullable: false),
                    EstadoCitaId = table.Column<Guid>(type: "uuid", nullable: false),
                    TipoCitaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    HoraInicio = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    HoraFin = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    MotivoConsulta = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Observaciones = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    UsuarioCreacionId = table.Column<Guid>(type: "uuid", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MotivoCancelacion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    UsuarioCancelacionId = table.Column<Guid>(type: "uuid", nullable: true),
                    FechaCancelacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cita", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cita_estado_cita_EstadoCitaId",
                        column: x => x.EstadoCitaId,
                        principalTable: "estado_cita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cita_medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "medico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cita_paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "paciente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cita_tipo_cita_TipoCitaId",
                        column: x => x.TipoCitaId,
                        principalTable: "tipo_cita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cita_usuario_UsuarioCancelacionId",
                        column: x => x.UsuarioCancelacionId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cita_usuario_UsuarioCreacionId",
                        column: x => x.UsuarioCreacionId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "login_intento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: true),
                    UsernameIntentado = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Exitoso = table.Column<bool>(type: "boolean", nullable: false),
                    MotivoFallo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Ip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login_intento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_login_intento_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "sesion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Jti = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RefreshTokenHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    AccessExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RefreshExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EmitidoEn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UltimoUso = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RevocadoEn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MotivoRevocacion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Ip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Dispositivo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sesion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sesion_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "token_recuperacion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    TokenHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsadoEn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IpSolicitud = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_token_recuperacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_token_recuperacion_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cita_historial_estado",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CitaId = table.Column<Guid>(type: "uuid", nullable: false),
                    EstadoAnteriorId = table.Column<Guid>(type: "uuid", nullable: false),
                    EstadoNuevoId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: true),
                    FechaCambio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Observacion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cita_historial_estado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cita_historial_estado_cita_CitaId",
                        column: x => x.CitaId,
                        principalTable: "cita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cita_historial_estado_estado_cita_EstadoAnteriorId",
                        column: x => x.EstadoAnteriorId,
                        principalTable: "estado_cita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cita_historial_estado_estado_cita_EstadoNuevoId",
                        column: x => x.EstadoNuevoId,
                        principalTable: "estado_cita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cita_historial_estado_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "detalle_cita",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CitaId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uuid", nullable: false),
                    NotaAtencion = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    ResumenConsulta = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle_cita", x => x.Id);
                    table.ForeignKey(
                        name: "FK_detalle_cita_cita_CitaId",
                        column: x => x.CitaId,
                        principalTable: "cita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalle_cita_medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "medico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "atencion_tratamiento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DetalleCitaId = table.Column<Guid>(type: "uuid", nullable: false),
                    TratamientoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Dosis = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Frecuencia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DuracionDias = table.Column<int>(type: "integer", nullable: true),
                    Indicaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atencion_tratamiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_atencion_tratamiento_detalle_cita_DetalleCitaId",
                        column: x => x.DetalleCitaId,
                        principalTable: "detalle_cita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_atencion_tratamiento_tratamiento_TratamientoId",
                        column: x => x.TratamientoId,
                        principalTable: "tratamiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "detalle_diagnostico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DetalleCitaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DiagnosticoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Principal = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle_diagnostico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_detalle_diagnostico_detalle_cita_DetalleCitaId",
                        column: x => x.DetalleCitaId,
                        principalTable: "detalle_cita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalle_diagnostico_diagnostico_DiagnosticoId",
                        column: x => x.DiagnosticoId,
                        principalTable: "diagnostico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_antecedente_PacienteId",
                table: "antecedente",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_antecedente_UsuarioRegistroId",
                table: "antecedente",
                column: "UsuarioRegistroId");

            migrationBuilder.CreateIndex(
                name: "IX_atencion_tratamiento_DetalleCitaId",
                table: "atencion_tratamiento",
                column: "DetalleCitaId");

            migrationBuilder.CreateIndex(
                name: "IX_atencion_tratamiento_TratamientoId",
                table: "atencion_tratamiento",
                column: "TratamientoId");

            migrationBuilder.CreateIndex(
                name: "IX_cita_EstadoCitaId",
                table: "cita",
                column: "EstadoCitaId");

            migrationBuilder.CreateIndex(
                name: "IX_cita_MedicoId_Fecha_HoraInicio",
                table: "cita",
                columns: new[] { "MedicoId", "Fecha", "HoraInicio" });

            migrationBuilder.CreateIndex(
                name: "IX_cita_PacienteId_Fecha",
                table: "cita",
                columns: new[] { "PacienteId", "Fecha" });

            migrationBuilder.CreateIndex(
                name: "IX_cita_TipoCitaId",
                table: "cita",
                column: "TipoCitaId");

            migrationBuilder.CreateIndex(
                name: "IX_cita_UsuarioCancelacionId",
                table: "cita",
                column: "UsuarioCancelacionId");

            migrationBuilder.CreateIndex(
                name: "IX_cita_UsuarioCreacionId",
                table: "cita",
                column: "UsuarioCreacionId");

            migrationBuilder.CreateIndex(
                name: "IX_cita_historial_estado_CitaId",
                table: "cita_historial_estado",
                column: "CitaId");

            migrationBuilder.CreateIndex(
                name: "IX_cita_historial_estado_EstadoAnteriorId",
                table: "cita_historial_estado",
                column: "EstadoAnteriorId");

            migrationBuilder.CreateIndex(
                name: "IX_cita_historial_estado_EstadoNuevoId",
                table: "cita_historial_estado",
                column: "EstadoNuevoId");

            migrationBuilder.CreateIndex(
                name: "IX_cita_historial_estado_UsuarioId",
                table: "cita_historial_estado",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_cita_CitaId",
                table: "detalle_cita",
                column: "CitaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_detalle_cita_MedicoId",
                table: "detalle_cita",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_diagnostico_DetalleCitaId_DiagnosticoId",
                table: "detalle_diagnostico",
                columns: new[] { "DetalleCitaId", "DiagnosticoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_detalle_diagnostico_DiagnosticoId",
                table: "detalle_diagnostico",
                column: "DiagnosticoId");

            migrationBuilder.CreateIndex(
                name: "UX_detalle_diagnostico_principal",
                table: "detalle_diagnostico",
                column: "DetalleCitaId",
                unique: true,
                filter: "\"Principal\" = TRUE");

            migrationBuilder.CreateIndex(
                name: "IX_diagnostico_CodigoCie10",
                table: "diagnostico",
                column: "CodigoCie10",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_empleado_CargoId",
                table: "empleado",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_empleado_PersonaId",
                table: "empleado",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_especialidad_Nombre",
                table: "especialidad",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_estado_cita_Codigo",
                table: "estado_cita",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_horario_MedicoId_Fecha",
                table: "horario",
                columns: new[] { "MedicoId", "Fecha" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_login_intento_UsuarioId",
                table: "login_intento",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_medico_EmpleadoId",
                table: "medico",
                column: "EmpleadoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_medico_especialidad_EspecialidadId",
                table: "medico_especialidad",
                column: "EspecialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_medico_especialidad_MedicoId_EspecialidadId",
                table: "medico_especialidad",
                columns: new[] { "MedicoId", "EspecialidadId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_medico_especialidad_principal",
                table: "medico_especialidad",
                column: "MedicoId",
                unique: true,
                filter: "\"Principal\" = TRUE");

            migrationBuilder.CreateIndex(
                name: "IX_paciente_PersonaId",
                table: "paciente",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_paciente_alergia_PacienteId",
                table: "paciente_alergia",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_permiso_Codigo",
                table: "permiso",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_persona_SexoId",
                table: "persona",
                column: "SexoId");

            migrationBuilder.CreateIndex(
                name: "IX_persona_TipoDocumentoId_NumeroDocumento",
                table: "persona",
                columns: new[] { "TipoDocumentoId", "NumeroDocumento" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_persona_direccion_principal",
                table: "persona_direccion",
                column: "PersonaId",
                unique: true,
                filter: "\"Principal\" = TRUE");

            migrationBuilder.CreateIndex(
                name: "UX_persona_telefono_principal",
                table: "persona_telefono",
                column: "PersonaId",
                unique: true,
                filter: "\"Principal\" = TRUE");

            migrationBuilder.CreateIndex(
                name: "IX_rol_NombreRol",
                table: "rol",
                column: "NombreRol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rol_permiso_PermisoId",
                table: "rol_permiso",
                column: "PermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_rol_permiso_RolId_PermisoId",
                table: "rol_permiso",
                columns: new[] { "RolId", "PermisoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sesion_Jti",
                table: "sesion",
                column: "Jti",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sesion_UsuarioId_RevocadoEn",
                table: "sesion",
                columns: new[] { "UsuarioId", "RevocadoEn" });

            migrationBuilder.CreateIndex(
                name: "IX_sexo_Codigo",
                table: "sexo",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tipo_cita_Codigo",
                table: "tipo_cita",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tipo_documento_Codigo",
                table: "tipo_documento",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_token_recuperacion_TokenHash",
                table: "token_recuperacion",
                column: "TokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_token_recuperacion_UsuarioId",
                table: "token_recuperacion",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_tratamiento_Codigo",
                table: "tratamiento",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tratamiento_posologia_TratamientoId",
                table: "tratamiento_posologia",
                column: "TratamientoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuario_Email",
                table: "usuario",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuario_EmpleadoId",
                table: "usuario",
                column: "EmpleadoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuario_RolId",
                table: "usuario",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_Username",
                table: "usuario",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "antecedente");

            migrationBuilder.DropTable(
                name: "atencion_tratamiento");

            migrationBuilder.DropTable(
                name: "cita_historial_estado");

            migrationBuilder.DropTable(
                name: "detalle_diagnostico");

            migrationBuilder.DropTable(
                name: "horario");

            migrationBuilder.DropTable(
                name: "login_intento");

            migrationBuilder.DropTable(
                name: "medico_especialidad");

            migrationBuilder.DropTable(
                name: "paciente_alergia");

            migrationBuilder.DropTable(
                name: "persona_direccion");

            migrationBuilder.DropTable(
                name: "persona_telefono");

            migrationBuilder.DropTable(
                name: "rol_permiso");

            migrationBuilder.DropTable(
                name: "sesion");

            migrationBuilder.DropTable(
                name: "token_recuperacion");

            migrationBuilder.DropTable(
                name: "tratamiento_posologia");

            migrationBuilder.DropTable(
                name: "detalle_cita");

            migrationBuilder.DropTable(
                name: "diagnostico");

            migrationBuilder.DropTable(
                name: "especialidad");

            migrationBuilder.DropTable(
                name: "permiso");

            migrationBuilder.DropTable(
                name: "tratamiento");

            migrationBuilder.DropTable(
                name: "cita");

            migrationBuilder.DropTable(
                name: "estado_cita");

            migrationBuilder.DropTable(
                name: "medico");

            migrationBuilder.DropTable(
                name: "paciente");

            migrationBuilder.DropTable(
                name: "tipo_cita");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "empleado");

            migrationBuilder.DropTable(
                name: "rol");

            migrationBuilder.DropTable(
                name: "cargo");

            migrationBuilder.DropTable(
                name: "persona");

            migrationBuilder.DropTable(
                name: "sexo");

            migrationBuilder.DropTable(
                name: "tipo_documento");
        }
    }
}
