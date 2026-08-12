using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PluralizeTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_antecedente_paciente_PacienteId",
                table: "antecedente");

            migrationBuilder.DropForeignKey(
                name: "FK_antecedente_usuario_UsuarioRegistroId",
                table: "antecedente");

            migrationBuilder.DropForeignKey(
                name: "FK_atencion_tratamiento_detalle_cita_DetalleCitaId",
                table: "atencion_tratamiento");

            migrationBuilder.DropForeignKey(
                name: "FK_atencion_tratamiento_tratamiento_TratamientoId",
                table: "atencion_tratamiento");

            migrationBuilder.DropForeignKey(
                name: "FK_cita_estado_cita_EstadoCitaId",
                table: "cita");

            migrationBuilder.DropForeignKey(
                name: "FK_cita_medico_MedicoId",
                table: "cita");

            migrationBuilder.DropForeignKey(
                name: "FK_cita_paciente_PacienteId",
                table: "cita");

            migrationBuilder.DropForeignKey(
                name: "FK_cita_tipo_cita_TipoCitaId",
                table: "cita");

            migrationBuilder.DropForeignKey(
                name: "FK_cita_usuario_UsuarioCancelacionId",
                table: "cita");

            migrationBuilder.DropForeignKey(
                name: "FK_cita_usuario_UsuarioCreacionId",
                table: "cita");

            migrationBuilder.DropForeignKey(
                name: "FK_cita_historial_estado_cita_CitaId",
                table: "cita_historial_estado");

            migrationBuilder.DropForeignKey(
                name: "FK_cita_historial_estado_estado_cita_EstadoAnteriorId",
                table: "cita_historial_estado");

            migrationBuilder.DropForeignKey(
                name: "FK_cita_historial_estado_estado_cita_EstadoNuevoId",
                table: "cita_historial_estado");

            migrationBuilder.DropForeignKey(
                name: "FK_cita_historial_estado_usuario_UsuarioId",
                table: "cita_historial_estado");

            migrationBuilder.DropForeignKey(
                name: "FK_detalle_cita_cita_CitaId",
                table: "detalle_cita");

            migrationBuilder.DropForeignKey(
                name: "FK_detalle_cita_medico_MedicoId",
                table: "detalle_cita");

            migrationBuilder.DropForeignKey(
                name: "FK_detalle_diagnostico_detalle_cita_DetalleCitaId",
                table: "detalle_diagnostico");

            migrationBuilder.DropForeignKey(
                name: "FK_detalle_diagnostico_diagnostico_DiagnosticoId",
                table: "detalle_diagnostico");

            migrationBuilder.DropForeignKey(
                name: "FK_empleado_cargo_CargoId",
                table: "empleado");

            migrationBuilder.DropForeignKey(
                name: "FK_empleado_persona_PersonaId",
                table: "empleado");

            migrationBuilder.DropForeignKey(
                name: "FK_horario_medico_MedicoId",
                table: "horario");

            migrationBuilder.DropForeignKey(
                name: "FK_login_intento_usuario_UsuarioId",
                table: "login_intento");

            migrationBuilder.DropForeignKey(
                name: "FK_medico_empleado_EmpleadoId",
                table: "medico");

            migrationBuilder.DropForeignKey(
                name: "FK_medico_especialidad_especialidad_EspecialidadId",
                table: "medico_especialidad");

            migrationBuilder.DropForeignKey(
                name: "FK_medico_especialidad_medico_MedicoId",
                table: "medico_especialidad");

            migrationBuilder.DropForeignKey(
                name: "FK_paciente_persona_PersonaId",
                table: "paciente");

            migrationBuilder.DropForeignKey(
                name: "FK_paciente_alergia_paciente_PacienteId",
                table: "paciente_alergia");

            migrationBuilder.DropForeignKey(
                name: "FK_persona_sexo_SexoId",
                table: "persona");

            migrationBuilder.DropForeignKey(
                name: "FK_persona_tipo_documento_TipoDocumentoId",
                table: "persona");

            migrationBuilder.DropForeignKey(
                name: "FK_persona_direccion_persona_PersonaId",
                table: "persona_direccion");

            migrationBuilder.DropForeignKey(
                name: "FK_persona_telefono_persona_PersonaId",
                table: "persona_telefono");

            migrationBuilder.DropForeignKey(
                name: "FK_rol_permiso_permiso_PermisoId",
                table: "rol_permiso");

            migrationBuilder.DropForeignKey(
                name: "FK_rol_permiso_rol_RolId",
                table: "rol_permiso");

            migrationBuilder.DropForeignKey(
                name: "FK_sesion_usuario_UsuarioId",
                table: "sesion");

            migrationBuilder.DropForeignKey(
                name: "FK_token_recuperacion_usuario_UsuarioId",
                table: "token_recuperacion");

            migrationBuilder.DropForeignKey(
                name: "FK_tratamiento_posologia_tratamiento_TratamientoId",
                table: "tratamiento_posologia");

            migrationBuilder.DropForeignKey(
                name: "FK_usuario_empleado_EmpleadoId",
                table: "usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_usuario_rol_RolId",
                table: "usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usuario",
                table: "usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tratamiento_posologia",
                table: "tratamiento_posologia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tratamiento",
                table: "tratamiento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_token_recuperacion",
                table: "token_recuperacion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tipo_documento",
                table: "tipo_documento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tipo_cita",
                table: "tipo_cita");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sexo",
                table: "sexo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sesion",
                table: "sesion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rol_permiso",
                table: "rol_permiso");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rol",
                table: "rol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_persona_telefono",
                table: "persona_telefono");

            migrationBuilder.DropCheckConstraint(
                name: "CK_persona_telefono_tipo",
                table: "persona_telefono");

            migrationBuilder.DropPrimaryKey(
                name: "PK_persona_direccion",
                table: "persona_direccion");

            migrationBuilder.DropCheckConstraint(
                name: "CK_persona_direccion_tipo",
                table: "persona_direccion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_persona",
                table: "persona");

            migrationBuilder.DropPrimaryKey(
                name: "PK_permiso",
                table: "permiso");

            migrationBuilder.DropPrimaryKey(
                name: "PK_paciente_alergia",
                table: "paciente_alergia");

            migrationBuilder.DropCheckConstraint(
                name: "CK_paciente_alergia_severidad",
                table: "paciente_alergia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_paciente",
                table: "paciente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_medico_especialidad",
                table: "medico_especialidad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_medico",
                table: "medico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_login_intento",
                table: "login_intento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_horario",
                table: "horario");

            migrationBuilder.DropCheckConstraint(
                name: "CK_horario_jornada",
                table: "horario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_estado_cita",
                table: "estado_cita");

            migrationBuilder.DropPrimaryKey(
                name: "PK_especialidad",
                table: "especialidad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_empleado",
                table: "empleado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_diagnostico",
                table: "diagnostico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_detalle_diagnostico",
                table: "detalle_diagnostico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_detalle_cita",
                table: "detalle_cita");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cita_historial_estado",
                table: "cita_historial_estado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cita",
                table: "cita");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cargo",
                table: "cargo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_atencion_tratamiento",
                table: "atencion_tratamiento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_antecedente",
                table: "antecedente");

            migrationBuilder.DropCheckConstraint(
                name: "CK_antecedente_tipo",
                table: "antecedente");

            migrationBuilder.RenameTable(
                name: "usuario",
                newName: "usuarios");

            migrationBuilder.RenameTable(
                name: "tratamiento_posologia",
                newName: "tratamiento_posologias");

            migrationBuilder.RenameTable(
                name: "tratamiento",
                newName: "tratamientos");

            migrationBuilder.RenameTable(
                name: "token_recuperacion",
                newName: "tokens_recuperacion");

            migrationBuilder.RenameTable(
                name: "tipo_documento",
                newName: "tipos_documento");

            migrationBuilder.RenameTable(
                name: "tipo_cita",
                newName: "tipos_cita");

            migrationBuilder.RenameTable(
                name: "sexo",
                newName: "sexos");

            migrationBuilder.RenameTable(
                name: "sesion",
                newName: "sesiones");

            migrationBuilder.RenameTable(
                name: "rol_permiso",
                newName: "rol_permisos");

            migrationBuilder.RenameTable(
                name: "rol",
                newName: "roles");

            migrationBuilder.RenameTable(
                name: "persona_telefono",
                newName: "persona_telefonos");

            migrationBuilder.RenameTable(
                name: "persona_direccion",
                newName: "persona_direcciones");

            migrationBuilder.RenameTable(
                name: "persona",
                newName: "personas");

            migrationBuilder.RenameTable(
                name: "permiso",
                newName: "permisos");

            migrationBuilder.RenameTable(
                name: "paciente_alergia",
                newName: "paciente_alergias");

            migrationBuilder.RenameTable(
                name: "paciente",
                newName: "pacientes");

            migrationBuilder.RenameTable(
                name: "medico_especialidad",
                newName: "medico_especialidades");

            migrationBuilder.RenameTable(
                name: "medico",
                newName: "medicos");

            migrationBuilder.RenameTable(
                name: "login_intento",
                newName: "login_intentos");

            migrationBuilder.RenameTable(
                name: "horario",
                newName: "horarios");

            migrationBuilder.RenameTable(
                name: "estado_cita",
                newName: "estados_cita");

            migrationBuilder.RenameTable(
                name: "especialidad",
                newName: "especialidades");

            migrationBuilder.RenameTable(
                name: "empleado",
                newName: "empleados");

            migrationBuilder.RenameTable(
                name: "diagnostico",
                newName: "diagnosticos");

            migrationBuilder.RenameTable(
                name: "detalle_diagnostico",
                newName: "detalles_diagnostico");

            migrationBuilder.RenameTable(
                name: "detalle_cita",
                newName: "detalles_cita");

            migrationBuilder.RenameTable(
                name: "cita_historial_estado",
                newName: "cita_historial_estados");

            migrationBuilder.RenameTable(
                name: "cita",
                newName: "citas");

            migrationBuilder.RenameTable(
                name: "cargo",
                newName: "cargos");

            migrationBuilder.RenameTable(
                name: "atencion_tratamiento",
                newName: "atencion_tratamientos");

            migrationBuilder.RenameTable(
                name: "antecedente",
                newName: "antecedentes");

            migrationBuilder.RenameIndex(
                name: "IX_usuario_Username",
                table: "usuarios",
                newName: "IX_usuarios_Username");

            migrationBuilder.RenameIndex(
                name: "IX_usuario_RolId",
                table: "usuarios",
                newName: "IX_usuarios_RolId");

            migrationBuilder.RenameIndex(
                name: "IX_usuario_EmpleadoId",
                table: "usuarios",
                newName: "IX_usuarios_EmpleadoId");

            migrationBuilder.RenameIndex(
                name: "IX_usuario_Email",
                table: "usuarios",
                newName: "IX_usuarios_Email");

            migrationBuilder.RenameIndex(
                name: "IX_tratamiento_posologia_TratamientoId",
                table: "tratamiento_posologias",
                newName: "IX_tratamiento_posologias_TratamientoId");

            migrationBuilder.RenameIndex(
                name: "IX_tratamiento_Codigo",
                table: "tratamientos",
                newName: "IX_tratamientos_Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_token_recuperacion_UsuarioId",
                table: "tokens_recuperacion",
                newName: "IX_tokens_recuperacion_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_token_recuperacion_TokenHash",
                table: "tokens_recuperacion",
                newName: "IX_tokens_recuperacion_TokenHash");

            migrationBuilder.RenameIndex(
                name: "IX_tipo_documento_Codigo",
                table: "tipos_documento",
                newName: "IX_tipos_documento_Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_tipo_cita_Codigo",
                table: "tipos_cita",
                newName: "IX_tipos_cita_Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_sexo_Codigo",
                table: "sexos",
                newName: "IX_sexos_Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_sesion_UsuarioId_RevocadoEn",
                table: "sesiones",
                newName: "IX_sesiones_UsuarioId_RevocadoEn");

            migrationBuilder.RenameIndex(
                name: "IX_sesion_Jti",
                table: "sesiones",
                newName: "IX_sesiones_Jti");

            migrationBuilder.RenameIndex(
                name: "IX_rol_permiso_RolId_PermisoId",
                table: "rol_permisos",
                newName: "IX_rol_permisos_RolId_PermisoId");

            migrationBuilder.RenameIndex(
                name: "IX_rol_permiso_PermisoId",
                table: "rol_permisos",
                newName: "IX_rol_permisos_PermisoId");

            migrationBuilder.RenameIndex(
                name: "IX_rol_NombreRol",
                table: "roles",
                newName: "IX_roles_NombreRol");

            migrationBuilder.RenameIndex(
                name: "UX_persona_telefono_principal",
                table: "persona_telefonos",
                newName: "UX_persona_telefonos_principal");

            migrationBuilder.RenameIndex(
                name: "UX_persona_direccion_principal",
                table: "persona_direcciones",
                newName: "UX_persona_direcciones_principal");

            migrationBuilder.RenameIndex(
                name: "IX_persona_TipoDocumentoId_NumeroDocumento",
                table: "personas",
                newName: "IX_personas_TipoDocumentoId_NumeroDocumento");

            migrationBuilder.RenameIndex(
                name: "IX_persona_SexoId",
                table: "personas",
                newName: "IX_personas_SexoId");

            migrationBuilder.RenameIndex(
                name: "IX_permiso_Codigo",
                table: "permisos",
                newName: "IX_permisos_Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_paciente_alergia_PacienteId",
                table: "paciente_alergias",
                newName: "IX_paciente_alergias_PacienteId");

            migrationBuilder.RenameIndex(
                name: "IX_paciente_PersonaId",
                table: "pacientes",
                newName: "IX_pacientes_PersonaId");

            migrationBuilder.RenameIndex(
                name: "UX_medico_especialidad_principal",
                table: "medico_especialidades",
                newName: "UX_medico_especialidades_principal");

            migrationBuilder.RenameIndex(
                name: "IX_medico_especialidad_MedicoId_EspecialidadId",
                table: "medico_especialidades",
                newName: "IX_medico_especialidades_MedicoId_EspecialidadId");

            migrationBuilder.RenameIndex(
                name: "IX_medico_especialidad_EspecialidadId",
                table: "medico_especialidades",
                newName: "IX_medico_especialidades_EspecialidadId");

            migrationBuilder.RenameIndex(
                name: "IX_medico_EmpleadoId",
                table: "medicos",
                newName: "IX_medicos_EmpleadoId");

            migrationBuilder.RenameIndex(
                name: "IX_login_intento_UsuarioId",
                table: "login_intentos",
                newName: "IX_login_intentos_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_horario_MedicoId",
                table: "horarios",
                newName: "IX_horarios_MedicoId");

            migrationBuilder.RenameIndex(
                name: "IX_estado_cita_Codigo",
                table: "estados_cita",
                newName: "IX_estados_cita_Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_especialidad_Nombre",
                table: "especialidades",
                newName: "IX_especialidades_Nombre");

            migrationBuilder.RenameIndex(
                name: "IX_empleado_PersonaId",
                table: "empleados",
                newName: "IX_empleados_PersonaId");

            migrationBuilder.RenameIndex(
                name: "IX_empleado_CargoId",
                table: "empleados",
                newName: "IX_empleados_CargoId");

            migrationBuilder.RenameIndex(
                name: "IX_diagnostico_CodigoCie10",
                table: "diagnosticos",
                newName: "IX_diagnosticos_CodigoCie10");

            migrationBuilder.RenameIndex(
                name: "UX_detalle_diagnostico_principal",
                table: "detalles_diagnostico",
                newName: "UX_detalles_diagnostico_principal");

            migrationBuilder.RenameIndex(
                name: "IX_detalle_diagnostico_DiagnosticoId",
                table: "detalles_diagnostico",
                newName: "IX_detalles_diagnostico_DiagnosticoId");

            migrationBuilder.RenameIndex(
                name: "IX_detalle_diagnostico_DetalleCitaId_DiagnosticoId",
                table: "detalles_diagnostico",
                newName: "IX_detalles_diagnostico_DetalleCitaId_DiagnosticoId");

            migrationBuilder.RenameIndex(
                name: "IX_detalle_cita_MedicoId",
                table: "detalles_cita",
                newName: "IX_detalles_cita_MedicoId");

            migrationBuilder.RenameIndex(
                name: "IX_detalle_cita_CitaId",
                table: "detalles_cita",
                newName: "IX_detalles_cita_CitaId");

            migrationBuilder.RenameIndex(
                name: "IX_cita_historial_estado_UsuarioId",
                table: "cita_historial_estados",
                newName: "IX_cita_historial_estados_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_cita_historial_estado_EstadoNuevoId",
                table: "cita_historial_estados",
                newName: "IX_cita_historial_estados_EstadoNuevoId");

            migrationBuilder.RenameIndex(
                name: "IX_cita_historial_estado_EstadoAnteriorId",
                table: "cita_historial_estados",
                newName: "IX_cita_historial_estados_EstadoAnteriorId");

            migrationBuilder.RenameIndex(
                name: "IX_cita_historial_estado_CitaId",
                table: "cita_historial_estados",
                newName: "IX_cita_historial_estados_CitaId");

            migrationBuilder.RenameIndex(
                name: "IX_cita_UsuarioCreacionId",
                table: "citas",
                newName: "IX_citas_UsuarioCreacionId");

            migrationBuilder.RenameIndex(
                name: "IX_cita_UsuarioCancelacionId",
                table: "citas",
                newName: "IX_citas_UsuarioCancelacionId");

            migrationBuilder.RenameIndex(
                name: "IX_cita_TipoCitaId",
                table: "citas",
                newName: "IX_citas_TipoCitaId");

            migrationBuilder.RenameIndex(
                name: "IX_cita_PacienteId_Fecha",
                table: "citas",
                newName: "IX_citas_PacienteId_Fecha");

            migrationBuilder.RenameIndex(
                name: "IX_cita_MedicoId_Fecha_HoraInicio",
                table: "citas",
                newName: "IX_citas_MedicoId_Fecha_HoraInicio");

            migrationBuilder.RenameIndex(
                name: "IX_cita_EstadoCitaId",
                table: "citas",
                newName: "IX_citas_EstadoCitaId");

            migrationBuilder.RenameIndex(
                name: "IX_atencion_tratamiento_TratamientoId",
                table: "atencion_tratamientos",
                newName: "IX_atencion_tratamientos_TratamientoId");

            migrationBuilder.RenameIndex(
                name: "IX_atencion_tratamiento_DetalleCitaId",
                table: "atencion_tratamientos",
                newName: "IX_atencion_tratamientos_DetalleCitaId");

            migrationBuilder.RenameIndex(
                name: "IX_antecedente_UsuarioRegistroId",
                table: "antecedentes",
                newName: "IX_antecedentes_UsuarioRegistroId");

            migrationBuilder.RenameIndex(
                name: "IX_antecedente_PacienteId",
                table: "antecedentes",
                newName: "IX_antecedentes_PacienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuarios",
                table: "usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tratamiento_posologias",
                table: "tratamiento_posologias",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tratamientos",
                table: "tratamientos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tokens_recuperacion",
                table: "tokens_recuperacion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tipos_documento",
                table: "tipos_documento",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tipos_cita",
                table: "tipos_cita",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sexos",
                table: "sexos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sesiones",
                table: "sesiones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rol_permisos",
                table: "rol_permisos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles",
                table: "roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_persona_telefonos",
                table: "persona_telefonos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_persona_direcciones",
                table: "persona_direcciones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_personas",
                table: "personas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_permisos",
                table: "permisos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_paciente_alergias",
                table: "paciente_alergias",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pacientes",
                table: "pacientes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_medico_especialidades",
                table: "medico_especialidades",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_medicos",
                table: "medicos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_login_intentos",
                table: "login_intentos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_horarios",
                table: "horarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_estados_cita",
                table: "estados_cita",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_especialidades",
                table: "especialidades",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_empleados",
                table: "empleados",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_diagnosticos",
                table: "diagnosticos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_detalles_diagnostico",
                table: "detalles_diagnostico",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_detalles_cita",
                table: "detalles_cita",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cita_historial_estados",
                table: "cita_historial_estados",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_citas",
                table: "citas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cargos",
                table: "cargos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_atencion_tratamientos",
                table: "atencion_tratamientos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_antecedentes",
                table: "antecedentes",
                column: "Id");

            migrationBuilder.AddCheckConstraint(
                name: "CK_persona_telefonos_tipo",
                table: "persona_telefonos",
                sql: "\"Tipo\" IS NULL OR \"Tipo\" IN ('movil', 'fijo', 'trabajo')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_persona_direcciones_tipo",
                table: "persona_direcciones",
                sql: "\"Tipo\" IS NULL OR \"Tipo\" IN ('residencia', 'trabajo')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_paciente_alergias_severidad",
                table: "paciente_alergias",
                sql: "\"Severidad\" IS NULL OR \"Severidad\" IN ('leve', 'moderada', 'severa')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_horarios_jornada",
                table: "horarios",
                sql: "\"HoraEntrada\" < \"SalidaAlmuerzo\" AND \"SalidaAlmuerzo\" < \"RetornoActividades\" AND \"RetornoActividades\" < \"HoraSalida\"");

            migrationBuilder.AddCheckConstraint(
                name: "CK_antecedentes_tipo",
                table: "antecedentes",
                sql: "\"Tipo\" IN ('personal', 'familiar', 'quirurgico')");

            migrationBuilder.AddForeignKey(
                name: "FK_antecedentes_pacientes_PacienteId",
                table: "antecedentes",
                column: "PacienteId",
                principalTable: "pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_antecedentes_usuarios_UsuarioRegistroId",
                table: "antecedentes",
                column: "UsuarioRegistroId",
                principalTable: "usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_atencion_tratamientos_detalles_cita_DetalleCitaId",
                table: "atencion_tratamientos",
                column: "DetalleCitaId",
                principalTable: "detalles_cita",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_atencion_tratamientos_tratamientos_TratamientoId",
                table: "atencion_tratamientos",
                column: "TratamientoId",
                principalTable: "tratamientos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cita_historial_estados_citas_CitaId",
                table: "cita_historial_estados",
                column: "CitaId",
                principalTable: "citas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cita_historial_estados_estados_cita_EstadoAnteriorId",
                table: "cita_historial_estados",
                column: "EstadoAnteriorId",
                principalTable: "estados_cita",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cita_historial_estados_estados_cita_EstadoNuevoId",
                table: "cita_historial_estados",
                column: "EstadoNuevoId",
                principalTable: "estados_cita",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cita_historial_estados_usuarios_UsuarioId",
                table: "cita_historial_estados",
                column: "UsuarioId",
                principalTable: "usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_citas_estados_cita_EstadoCitaId",
                table: "citas",
                column: "EstadoCitaId",
                principalTable: "estados_cita",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_citas_medicos_MedicoId",
                table: "citas",
                column: "MedicoId",
                principalTable: "medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_citas_pacientes_PacienteId",
                table: "citas",
                column: "PacienteId",
                principalTable: "pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_citas_tipos_cita_TipoCitaId",
                table: "citas",
                column: "TipoCitaId",
                principalTable: "tipos_cita",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_citas_usuarios_UsuarioCancelacionId",
                table: "citas",
                column: "UsuarioCancelacionId",
                principalTable: "usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_citas_usuarios_UsuarioCreacionId",
                table: "citas",
                column: "UsuarioCreacionId",
                principalTable: "usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_detalles_cita_citas_CitaId",
                table: "detalles_cita",
                column: "CitaId",
                principalTable: "citas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_detalles_cita_medicos_MedicoId",
                table: "detalles_cita",
                column: "MedicoId",
                principalTable: "medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_detalles_diagnostico_detalles_cita_DetalleCitaId",
                table: "detalles_diagnostico",
                column: "DetalleCitaId",
                principalTable: "detalles_cita",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_detalles_diagnostico_diagnosticos_DiagnosticoId",
                table: "detalles_diagnostico",
                column: "DiagnosticoId",
                principalTable: "diagnosticos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_empleados_cargos_CargoId",
                table: "empleados",
                column: "CargoId",
                principalTable: "cargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_empleados_personas_PersonaId",
                table: "empleados",
                column: "PersonaId",
                principalTable: "personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_horarios_medicos_MedicoId",
                table: "horarios",
                column: "MedicoId",
                principalTable: "medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_login_intentos_usuarios_UsuarioId",
                table: "login_intentos",
                column: "UsuarioId",
                principalTable: "usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_medico_especialidades_especialidades_EspecialidadId",
                table: "medico_especialidades",
                column: "EspecialidadId",
                principalTable: "especialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_medico_especialidades_medicos_MedicoId",
                table: "medico_especialidades",
                column: "MedicoId",
                principalTable: "medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_medicos_empleados_EmpleadoId",
                table: "medicos",
                column: "EmpleadoId",
                principalTable: "empleados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_paciente_alergias_pacientes_PacienteId",
                table: "paciente_alergias",
                column: "PacienteId",
                principalTable: "pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pacientes_personas_PersonaId",
                table: "pacientes",
                column: "PersonaId",
                principalTable: "personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_persona_direcciones_personas_PersonaId",
                table: "persona_direcciones",
                column: "PersonaId",
                principalTable: "personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_persona_telefonos_personas_PersonaId",
                table: "persona_telefonos",
                column: "PersonaId",
                principalTable: "personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_personas_sexos_SexoId",
                table: "personas",
                column: "SexoId",
                principalTable: "sexos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_personas_tipos_documento_TipoDocumentoId",
                table: "personas",
                column: "TipoDocumentoId",
                principalTable: "tipos_documento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_rol_permisos_permisos_PermisoId",
                table: "rol_permisos",
                column: "PermisoId",
                principalTable: "permisos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_rol_permisos_roles_RolId",
                table: "rol_permisos",
                column: "RolId",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sesiones_usuarios_UsuarioId",
                table: "sesiones",
                column: "UsuarioId",
                principalTable: "usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tokens_recuperacion_usuarios_UsuarioId",
                table: "tokens_recuperacion",
                column: "UsuarioId",
                principalTable: "usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tratamiento_posologias_tratamientos_TratamientoId",
                table: "tratamiento_posologias",
                column: "TratamientoId",
                principalTable: "tratamientos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_usuarios_empleados_EmpleadoId",
                table: "usuarios",
                column: "EmpleadoId",
                principalTable: "empleados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_usuarios_roles_RolId",
                table: "usuarios",
                column: "RolId",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_antecedentes_pacientes_PacienteId",
                table: "antecedentes");

            migrationBuilder.DropForeignKey(
                name: "FK_antecedentes_usuarios_UsuarioRegistroId",
                table: "antecedentes");

            migrationBuilder.DropForeignKey(
                name: "FK_atencion_tratamientos_detalles_cita_DetalleCitaId",
                table: "atencion_tratamientos");

            migrationBuilder.DropForeignKey(
                name: "FK_atencion_tratamientos_tratamientos_TratamientoId",
                table: "atencion_tratamientos");

            migrationBuilder.DropForeignKey(
                name: "FK_cita_historial_estados_citas_CitaId",
                table: "cita_historial_estados");

            migrationBuilder.DropForeignKey(
                name: "FK_cita_historial_estados_estados_cita_EstadoAnteriorId",
                table: "cita_historial_estados");

            migrationBuilder.DropForeignKey(
                name: "FK_cita_historial_estados_estados_cita_EstadoNuevoId",
                table: "cita_historial_estados");

            migrationBuilder.DropForeignKey(
                name: "FK_cita_historial_estados_usuarios_UsuarioId",
                table: "cita_historial_estados");

            migrationBuilder.DropForeignKey(
                name: "FK_citas_estados_cita_EstadoCitaId",
                table: "citas");

            migrationBuilder.DropForeignKey(
                name: "FK_citas_medicos_MedicoId",
                table: "citas");

            migrationBuilder.DropForeignKey(
                name: "FK_citas_pacientes_PacienteId",
                table: "citas");

            migrationBuilder.DropForeignKey(
                name: "FK_citas_tipos_cita_TipoCitaId",
                table: "citas");

            migrationBuilder.DropForeignKey(
                name: "FK_citas_usuarios_UsuarioCancelacionId",
                table: "citas");

            migrationBuilder.DropForeignKey(
                name: "FK_citas_usuarios_UsuarioCreacionId",
                table: "citas");

            migrationBuilder.DropForeignKey(
                name: "FK_detalles_cita_citas_CitaId",
                table: "detalles_cita");

            migrationBuilder.DropForeignKey(
                name: "FK_detalles_cita_medicos_MedicoId",
                table: "detalles_cita");

            migrationBuilder.DropForeignKey(
                name: "FK_detalles_diagnostico_detalles_cita_DetalleCitaId",
                table: "detalles_diagnostico");

            migrationBuilder.DropForeignKey(
                name: "FK_detalles_diagnostico_diagnosticos_DiagnosticoId",
                table: "detalles_diagnostico");

            migrationBuilder.DropForeignKey(
                name: "FK_empleados_cargos_CargoId",
                table: "empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_empleados_personas_PersonaId",
                table: "empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_horarios_medicos_MedicoId",
                table: "horarios");

            migrationBuilder.DropForeignKey(
                name: "FK_login_intentos_usuarios_UsuarioId",
                table: "login_intentos");

            migrationBuilder.DropForeignKey(
                name: "FK_medico_especialidades_especialidades_EspecialidadId",
                table: "medico_especialidades");

            migrationBuilder.DropForeignKey(
                name: "FK_medico_especialidades_medicos_MedicoId",
                table: "medico_especialidades");

            migrationBuilder.DropForeignKey(
                name: "FK_medicos_empleados_EmpleadoId",
                table: "medicos");

            migrationBuilder.DropForeignKey(
                name: "FK_paciente_alergias_pacientes_PacienteId",
                table: "paciente_alergias");

            migrationBuilder.DropForeignKey(
                name: "FK_pacientes_personas_PersonaId",
                table: "pacientes");

            migrationBuilder.DropForeignKey(
                name: "FK_persona_direcciones_personas_PersonaId",
                table: "persona_direcciones");

            migrationBuilder.DropForeignKey(
                name: "FK_persona_telefonos_personas_PersonaId",
                table: "persona_telefonos");

            migrationBuilder.DropForeignKey(
                name: "FK_personas_sexos_SexoId",
                table: "personas");

            migrationBuilder.DropForeignKey(
                name: "FK_personas_tipos_documento_TipoDocumentoId",
                table: "personas");

            migrationBuilder.DropForeignKey(
                name: "FK_rol_permisos_permisos_PermisoId",
                table: "rol_permisos");

            migrationBuilder.DropForeignKey(
                name: "FK_rol_permisos_roles_RolId",
                table: "rol_permisos");

            migrationBuilder.DropForeignKey(
                name: "FK_sesiones_usuarios_UsuarioId",
                table: "sesiones");

            migrationBuilder.DropForeignKey(
                name: "FK_tokens_recuperacion_usuarios_UsuarioId",
                table: "tokens_recuperacion");

            migrationBuilder.DropForeignKey(
                name: "FK_tratamiento_posologias_tratamientos_TratamientoId",
                table: "tratamiento_posologias");

            migrationBuilder.DropForeignKey(
                name: "FK_usuarios_empleados_EmpleadoId",
                table: "usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_usuarios_roles_RolId",
                table: "usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usuarios",
                table: "usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tratamientos",
                table: "tratamientos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tratamiento_posologias",
                table: "tratamiento_posologias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tokens_recuperacion",
                table: "tokens_recuperacion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tipos_documento",
                table: "tipos_documento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tipos_cita",
                table: "tipos_cita");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sexos",
                table: "sexos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sesiones",
                table: "sesiones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roles",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rol_permisos",
                table: "rol_permisos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_personas",
                table: "personas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_persona_telefonos",
                table: "persona_telefonos");

            migrationBuilder.DropCheckConstraint(
                name: "CK_persona_telefonos_tipo",
                table: "persona_telefonos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_persona_direcciones",
                table: "persona_direcciones");

            migrationBuilder.DropCheckConstraint(
                name: "CK_persona_direcciones_tipo",
                table: "persona_direcciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_permisos",
                table: "permisos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pacientes",
                table: "pacientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_paciente_alergias",
                table: "paciente_alergias");

            migrationBuilder.DropCheckConstraint(
                name: "CK_paciente_alergias_severidad",
                table: "paciente_alergias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_medicos",
                table: "medicos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_medico_especialidades",
                table: "medico_especialidades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_login_intentos",
                table: "login_intentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_horarios",
                table: "horarios");

            migrationBuilder.DropCheckConstraint(
                name: "CK_horarios_jornada",
                table: "horarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_estados_cita",
                table: "estados_cita");

            migrationBuilder.DropPrimaryKey(
                name: "PK_especialidades",
                table: "especialidades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_empleados",
                table: "empleados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_diagnosticos",
                table: "diagnosticos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_detalles_diagnostico",
                table: "detalles_diagnostico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_detalles_cita",
                table: "detalles_cita");

            migrationBuilder.DropPrimaryKey(
                name: "PK_citas",
                table: "citas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cita_historial_estados",
                table: "cita_historial_estados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cargos",
                table: "cargos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_atencion_tratamientos",
                table: "atencion_tratamientos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_antecedentes",
                table: "antecedentes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_antecedentes_tipo",
                table: "antecedentes");

            migrationBuilder.RenameTable(
                name: "usuarios",
                newName: "usuario");

            migrationBuilder.RenameTable(
                name: "tratamientos",
                newName: "tratamiento");

            migrationBuilder.RenameTable(
                name: "tratamiento_posologias",
                newName: "tratamiento_posologia");

            migrationBuilder.RenameTable(
                name: "tokens_recuperacion",
                newName: "token_recuperacion");

            migrationBuilder.RenameTable(
                name: "tipos_documento",
                newName: "tipo_documento");

            migrationBuilder.RenameTable(
                name: "tipos_cita",
                newName: "tipo_cita");

            migrationBuilder.RenameTable(
                name: "sexos",
                newName: "sexo");

            migrationBuilder.RenameTable(
                name: "sesiones",
                newName: "sesion");

            migrationBuilder.RenameTable(
                name: "roles",
                newName: "rol");

            migrationBuilder.RenameTable(
                name: "rol_permisos",
                newName: "rol_permiso");

            migrationBuilder.RenameTable(
                name: "personas",
                newName: "persona");

            migrationBuilder.RenameTable(
                name: "persona_telefonos",
                newName: "persona_telefono");

            migrationBuilder.RenameTable(
                name: "persona_direcciones",
                newName: "persona_direccion");

            migrationBuilder.RenameTable(
                name: "permisos",
                newName: "permiso");

            migrationBuilder.RenameTable(
                name: "pacientes",
                newName: "paciente");

            migrationBuilder.RenameTable(
                name: "paciente_alergias",
                newName: "paciente_alergia");

            migrationBuilder.RenameTable(
                name: "medicos",
                newName: "medico");

            migrationBuilder.RenameTable(
                name: "medico_especialidades",
                newName: "medico_especialidad");

            migrationBuilder.RenameTable(
                name: "login_intentos",
                newName: "login_intento");

            migrationBuilder.RenameTable(
                name: "horarios",
                newName: "horario");

            migrationBuilder.RenameTable(
                name: "estados_cita",
                newName: "estado_cita");

            migrationBuilder.RenameTable(
                name: "especialidades",
                newName: "especialidad");

            migrationBuilder.RenameTable(
                name: "empleados",
                newName: "empleado");

            migrationBuilder.RenameTable(
                name: "diagnosticos",
                newName: "diagnostico");

            migrationBuilder.RenameTable(
                name: "detalles_diagnostico",
                newName: "detalle_diagnostico");

            migrationBuilder.RenameTable(
                name: "detalles_cita",
                newName: "detalle_cita");

            migrationBuilder.RenameTable(
                name: "citas",
                newName: "cita");

            migrationBuilder.RenameTable(
                name: "cita_historial_estados",
                newName: "cita_historial_estado");

            migrationBuilder.RenameTable(
                name: "cargos",
                newName: "cargo");

            migrationBuilder.RenameTable(
                name: "atencion_tratamientos",
                newName: "atencion_tratamiento");

            migrationBuilder.RenameTable(
                name: "antecedentes",
                newName: "antecedente");

            migrationBuilder.RenameIndex(
                name: "IX_usuarios_Username",
                table: "usuario",
                newName: "IX_usuario_Username");

            migrationBuilder.RenameIndex(
                name: "IX_usuarios_RolId",
                table: "usuario",
                newName: "IX_usuario_RolId");

            migrationBuilder.RenameIndex(
                name: "IX_usuarios_EmpleadoId",
                table: "usuario",
                newName: "IX_usuario_EmpleadoId");

            migrationBuilder.RenameIndex(
                name: "IX_usuarios_Email",
                table: "usuario",
                newName: "IX_usuario_Email");

            migrationBuilder.RenameIndex(
                name: "IX_tratamientos_Codigo",
                table: "tratamiento",
                newName: "IX_tratamiento_Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_tratamiento_posologias_TratamientoId",
                table: "tratamiento_posologia",
                newName: "IX_tratamiento_posologia_TratamientoId");

            migrationBuilder.RenameIndex(
                name: "IX_tokens_recuperacion_UsuarioId",
                table: "token_recuperacion",
                newName: "IX_token_recuperacion_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_tokens_recuperacion_TokenHash",
                table: "token_recuperacion",
                newName: "IX_token_recuperacion_TokenHash");

            migrationBuilder.RenameIndex(
                name: "IX_tipos_documento_Codigo",
                table: "tipo_documento",
                newName: "IX_tipo_documento_Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_tipos_cita_Codigo",
                table: "tipo_cita",
                newName: "IX_tipo_cita_Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_sexos_Codigo",
                table: "sexo",
                newName: "IX_sexo_Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_sesiones_UsuarioId_RevocadoEn",
                table: "sesion",
                newName: "IX_sesion_UsuarioId_RevocadoEn");

            migrationBuilder.RenameIndex(
                name: "IX_sesiones_Jti",
                table: "sesion",
                newName: "IX_sesion_Jti");

            migrationBuilder.RenameIndex(
                name: "IX_roles_NombreRol",
                table: "rol",
                newName: "IX_rol_NombreRol");

            migrationBuilder.RenameIndex(
                name: "IX_rol_permisos_RolId_PermisoId",
                table: "rol_permiso",
                newName: "IX_rol_permiso_RolId_PermisoId");

            migrationBuilder.RenameIndex(
                name: "IX_rol_permisos_PermisoId",
                table: "rol_permiso",
                newName: "IX_rol_permiso_PermisoId");

            migrationBuilder.RenameIndex(
                name: "IX_personas_TipoDocumentoId_NumeroDocumento",
                table: "persona",
                newName: "IX_persona_TipoDocumentoId_NumeroDocumento");

            migrationBuilder.RenameIndex(
                name: "IX_personas_SexoId",
                table: "persona",
                newName: "IX_persona_SexoId");

            migrationBuilder.RenameIndex(
                name: "UX_persona_telefonos_principal",
                table: "persona_telefono",
                newName: "UX_persona_telefono_principal");

            migrationBuilder.RenameIndex(
                name: "UX_persona_direcciones_principal",
                table: "persona_direccion",
                newName: "UX_persona_direccion_principal");

            migrationBuilder.RenameIndex(
                name: "IX_permisos_Codigo",
                table: "permiso",
                newName: "IX_permiso_Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_pacientes_PersonaId",
                table: "paciente",
                newName: "IX_paciente_PersonaId");

            migrationBuilder.RenameIndex(
                name: "IX_paciente_alergias_PacienteId",
                table: "paciente_alergia",
                newName: "IX_paciente_alergia_PacienteId");

            migrationBuilder.RenameIndex(
                name: "IX_medicos_EmpleadoId",
                table: "medico",
                newName: "IX_medico_EmpleadoId");

            migrationBuilder.RenameIndex(
                name: "UX_medico_especialidades_principal",
                table: "medico_especialidad",
                newName: "UX_medico_especialidad_principal");

            migrationBuilder.RenameIndex(
                name: "IX_medico_especialidades_MedicoId_EspecialidadId",
                table: "medico_especialidad",
                newName: "IX_medico_especialidad_MedicoId_EspecialidadId");

            migrationBuilder.RenameIndex(
                name: "IX_medico_especialidades_EspecialidadId",
                table: "medico_especialidad",
                newName: "IX_medico_especialidad_EspecialidadId");

            migrationBuilder.RenameIndex(
                name: "IX_login_intentos_UsuarioId",
                table: "login_intento",
                newName: "IX_login_intento_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_horarios_MedicoId",
                table: "horario",
                newName: "IX_horario_MedicoId");

            migrationBuilder.RenameIndex(
                name: "IX_estados_cita_Codigo",
                table: "estado_cita",
                newName: "IX_estado_cita_Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_especialidades_Nombre",
                table: "especialidad",
                newName: "IX_especialidad_Nombre");

            migrationBuilder.RenameIndex(
                name: "IX_empleados_PersonaId",
                table: "empleado",
                newName: "IX_empleado_PersonaId");

            migrationBuilder.RenameIndex(
                name: "IX_empleados_CargoId",
                table: "empleado",
                newName: "IX_empleado_CargoId");

            migrationBuilder.RenameIndex(
                name: "IX_diagnosticos_CodigoCie10",
                table: "diagnostico",
                newName: "IX_diagnostico_CodigoCie10");

            migrationBuilder.RenameIndex(
                name: "UX_detalles_diagnostico_principal",
                table: "detalle_diagnostico",
                newName: "UX_detalle_diagnostico_principal");

            migrationBuilder.RenameIndex(
                name: "IX_detalles_diagnostico_DiagnosticoId",
                table: "detalle_diagnostico",
                newName: "IX_detalle_diagnostico_DiagnosticoId");

            migrationBuilder.RenameIndex(
                name: "IX_detalles_diagnostico_DetalleCitaId_DiagnosticoId",
                table: "detalle_diagnostico",
                newName: "IX_detalle_diagnostico_DetalleCitaId_DiagnosticoId");

            migrationBuilder.RenameIndex(
                name: "IX_detalles_cita_MedicoId",
                table: "detalle_cita",
                newName: "IX_detalle_cita_MedicoId");

            migrationBuilder.RenameIndex(
                name: "IX_detalles_cita_CitaId",
                table: "detalle_cita",
                newName: "IX_detalle_cita_CitaId");

            migrationBuilder.RenameIndex(
                name: "IX_citas_UsuarioCreacionId",
                table: "cita",
                newName: "IX_cita_UsuarioCreacionId");

            migrationBuilder.RenameIndex(
                name: "IX_citas_UsuarioCancelacionId",
                table: "cita",
                newName: "IX_cita_UsuarioCancelacionId");

            migrationBuilder.RenameIndex(
                name: "IX_citas_TipoCitaId",
                table: "cita",
                newName: "IX_cita_TipoCitaId");

            migrationBuilder.RenameIndex(
                name: "IX_citas_PacienteId_Fecha",
                table: "cita",
                newName: "IX_cita_PacienteId_Fecha");

            migrationBuilder.RenameIndex(
                name: "IX_citas_MedicoId_Fecha_HoraInicio",
                table: "cita",
                newName: "IX_cita_MedicoId_Fecha_HoraInicio");

            migrationBuilder.RenameIndex(
                name: "IX_citas_EstadoCitaId",
                table: "cita",
                newName: "IX_cita_EstadoCitaId");

            migrationBuilder.RenameIndex(
                name: "IX_cita_historial_estados_UsuarioId",
                table: "cita_historial_estado",
                newName: "IX_cita_historial_estado_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_cita_historial_estados_EstadoNuevoId",
                table: "cita_historial_estado",
                newName: "IX_cita_historial_estado_EstadoNuevoId");

            migrationBuilder.RenameIndex(
                name: "IX_cita_historial_estados_EstadoAnteriorId",
                table: "cita_historial_estado",
                newName: "IX_cita_historial_estado_EstadoAnteriorId");

            migrationBuilder.RenameIndex(
                name: "IX_cita_historial_estados_CitaId",
                table: "cita_historial_estado",
                newName: "IX_cita_historial_estado_CitaId");

            migrationBuilder.RenameIndex(
                name: "IX_atencion_tratamientos_TratamientoId",
                table: "atencion_tratamiento",
                newName: "IX_atencion_tratamiento_TratamientoId");

            migrationBuilder.RenameIndex(
                name: "IX_atencion_tratamientos_DetalleCitaId",
                table: "atencion_tratamiento",
                newName: "IX_atencion_tratamiento_DetalleCitaId");

            migrationBuilder.RenameIndex(
                name: "IX_antecedentes_UsuarioRegistroId",
                table: "antecedente",
                newName: "IX_antecedente_UsuarioRegistroId");

            migrationBuilder.RenameIndex(
                name: "IX_antecedentes_PacienteId",
                table: "antecedente",
                newName: "IX_antecedente_PacienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuario",
                table: "usuario",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tratamiento",
                table: "tratamiento",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tratamiento_posologia",
                table: "tratamiento_posologia",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_token_recuperacion",
                table: "token_recuperacion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tipo_documento",
                table: "tipo_documento",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tipo_cita",
                table: "tipo_cita",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sexo",
                table: "sexo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sesion",
                table: "sesion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rol",
                table: "rol",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rol_permiso",
                table: "rol_permiso",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_persona",
                table: "persona",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_persona_telefono",
                table: "persona_telefono",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_persona_direccion",
                table: "persona_direccion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_permiso",
                table: "permiso",
                column: "Id");
            migrationBuilder.AddPrimaryKey(
                name: "PK_paciente",
                table: "paciente",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_paciente_alergia",
                table: "paciente_alergia",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_medico",
                table: "medico",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_medico_especialidad",
                table: "medico_especialidad",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_login_intento",
                table: "login_intento",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_horario",
                table: "horario",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_estado_cita",
                table: "estado_cita",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_especialidad",
                table: "especialidad",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_empleado",
                table: "empleado",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_diagnostico",
                table: "diagnostico",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_detalle_diagnostico",
                table: "detalle_diagnostico",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_detalle_cita",
                table: "detalle_cita",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cita",
                table: "cita",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cita_historial_estado",
                table: "cita_historial_estado",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cargo",
                table: "cargo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_atencion_tratamiento",
                table: "atencion_tratamiento",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_antecedente",
                table: "antecedente",
                column: "Id");

            migrationBuilder.AddCheckConstraint(
                name: "CK_persona_telefono_tipo",
                table: "persona_telefono",
                sql: "\"Tipo\" IS NULL OR \"Tipo\" IN ('movil', 'fijo', 'trabajo')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_persona_direccion_tipo",
                table: "persona_direccion",
                sql: "\"Tipo\" IS NULL OR \"Tipo\" IN ('residencia', 'trabajo')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_paciente_alergia_severidad",
                table: "paciente_alergia",
                sql: "\"Severidad\" IS NULL OR \"Severidad\" IN ('leve', 'moderada', 'severa')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_horario_jornada",
                table: "horario",
                sql: "\"HoraEntrada\" < \"SalidaAlmuerzo\" AND \"SalidaAlmuerzo\" < \"RetornoActividades\" AND \"RetornoActividades\" < \"HoraSalida\"");

            migrationBuilder.AddCheckConstraint(
                name: "CK_antecedente_tipo",
                table: "antecedente",
                sql: "\"Tipo\" IN ('personal', 'familiar', 'quirurgico')");

            migrationBuilder.AddForeignKey(
                name: "FK_antecedente_paciente_PacienteId",
                table: "antecedente",
                column: "PacienteId",
                principalTable: "paciente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_antecedente_usuario_UsuarioRegistroId",
                table: "antecedente",
                column: "UsuarioRegistroId",
                principalTable: "usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_atencion_tratamiento_detalle_cita_DetalleCitaId",
                table: "atencion_tratamiento",
                column: "DetalleCitaId",
                principalTable: "detalle_cita",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_atencion_tratamiento_tratamiento_TratamientoId",
                table: "atencion_tratamiento",
                column: "TratamientoId",
                principalTable: "tratamiento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cita_estado_cita_EstadoCitaId",
                table: "cita",
                column: "EstadoCitaId",
                principalTable: "estado_cita",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cita_medico_MedicoId",
                table: "cita",
                column: "MedicoId",
                principalTable: "medico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cita_paciente_PacienteId",
                table: "cita",
                column: "PacienteId",
                principalTable: "paciente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cita_tipo_cita_TipoCitaId",
                table: "cita",
                column: "TipoCitaId",
                principalTable: "tipo_cita",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cita_usuario_UsuarioCancelacionId",
                table: "cita",
                column: "UsuarioCancelacionId",
                principalTable: "usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cita_usuario_UsuarioCreacionId",
                table: "cita",
                column: "UsuarioCreacionId",
                principalTable: "usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cita_historial_estado_cita_CitaId",
                table: "cita_historial_estado",
                column: "CitaId",
                principalTable: "cita",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cita_historial_estado_estado_cita_EstadoAnteriorId",
                table: "cita_historial_estado",
                column: "EstadoAnteriorId",
                principalTable: "estado_cita",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cita_historial_estado_estado_cita_EstadoNuevoId",
                table: "cita_historial_estado",
                column: "EstadoNuevoId",
                principalTable: "estado_cita",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cita_historial_estado_usuario_UsuarioId",
                table: "cita_historial_estado",
                column: "UsuarioId",
                principalTable: "usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_detalle_cita_cita_CitaId",
                table: "detalle_cita",
                column: "CitaId",
                principalTable: "cita",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_detalle_cita_medico_MedicoId",
                table: "detalle_cita",
                column: "MedicoId",
                principalTable: "medico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_detalle_diagnostico_detalle_cita_DetalleCitaId",
                table: "detalle_diagnostico",
                column: "DetalleCitaId",
                principalTable: "detalle_cita",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_detalle_diagnostico_diagnostico_DiagnosticoId",
                table: "detalle_diagnostico",
                column: "DiagnosticoId",
                principalTable: "diagnostico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_empleado_cargo_CargoId",
                table: "empleado",
                column: "CargoId",
                principalTable: "cargo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_empleado_persona_PersonaId",
                table: "empleado",
                column: "PersonaId",
                principalTable: "persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_horario_medico_MedicoId",
                table: "horario",
                column: "MedicoId",
                principalTable: "medico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_login_intento_usuario_UsuarioId",
                table: "login_intento",
                column: "UsuarioId",
                principalTable: "usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_medico_empleado_EmpleadoId",
                table: "medico",
                column: "EmpleadoId",
                principalTable: "empleado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_medico_especialidad_especialidad_EspecialidadId",
                table: "medico_especialidad",
                column: "EspecialidadId",
                principalTable: "especialidad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_medico_especialidad_medico_MedicoId",
                table: "medico_especialidad",
                column: "MedicoId",
                principalTable: "medico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_paciente_persona_PersonaId",
                table: "paciente",
                column: "PersonaId",
                principalTable: "persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_paciente_alergia_paciente_PacienteId",
                table: "paciente_alergia",
                column: "PacienteId",
                principalTable: "paciente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_persona_sexo_SexoId",
                table: "persona",
                column: "SexoId",
                principalTable: "sexo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_persona_tipo_documento_TipoDocumentoId",
                table: "persona",
                column: "TipoDocumentoId",
                principalTable: "tipo_documento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_persona_direccion_persona_PersonaId",
                table: "persona_direccion",
                column: "PersonaId",
                principalTable: "persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_persona_telefono_persona_PersonaId",
                table: "persona_telefono",
                column: "PersonaId",
                principalTable: "persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_rol_permiso_permiso_PermisoId",
                table: "rol_permiso",
                column: "PermisoId",
                principalTable: "permiso",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_rol_permiso_rol_RolId",
                table: "rol_permiso",
                column: "RolId",
                principalTable: "rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sesion_usuario_UsuarioId",
                table: "sesion",
                column: "UsuarioId",
                principalTable: "usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_token_recuperacion_usuario_UsuarioId",
                table: "token_recuperacion",
                column: "UsuarioId",
                principalTable: "usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tratamiento_posologia_tratamiento_TratamientoId",
                table: "tratamiento_posologia",
                column: "TratamientoId",
                principalTable: "tratamiento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_usuario_empleado_EmpleadoId",
                table: "usuario",
                column: "EmpleadoId",
                principalTable: "empleado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_usuario_rol_RolId",
                table: "usuario",
                column: "RolId",
                principalTable: "rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
