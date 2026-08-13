# Sistema de Gestion de citas

Este proyecto implementa el backend y el motor de base de datos para un sistema de salud integral y simplificado. Desarrollado bajo una arquitectura robusta, el sistema permite la administración completa de usuarios, el agendamiento de citas, el control de expedientes clínicos y la gestión de permisos basados en roles.

El modelo se ha construido bajo un enfoque adaptativo. Aunque el diseño cumple con los requisitos iniciales, se mantiene como un esquema flexible sujeto a modificaciones para garantizar la fluidez de los datos en la futura integración con el frontend.

## Stack Tecnologico

- C#
- .NET
- Entity Framework Core
- PostgreSQL 
- Supabase (Para uso compartido de la base de datos entre los integrantes)
- Swagger (Para pruebas de API)
- Insomnia (Para pruebas de API)

## Filosofia de Desarrollo e Infraestructura

- **Code-First con Entity Framework:** El esquema de la base de datos se gestiona directamente desde el código en C#. El ORM se encarga de la creación, actualización y relación de las tablas, actuando el DbContext como puente de enlace directo.
- **Base de Datos en la Nube:** Se utiliza Supabase (PostgreSQL) centralizado a través de una red IPv4 mediante un pooler de conexiones, garantizando que todo el equipo trabaje sobre un mismo entorno de datos sincronizado.
- **Inyeccion de Seeders:** El sistema cuenta con inserción de datos semilla y registros de prueba que se ejecutan automáticamente al iniciar el proyecto. Estos procesos cuentan con validaciones para evitar duplicidad de información y no requieren migraciones adicionales para inyectarse en el entorno.

## Estructura de la Base de Datos

La base de datos fue diseñada cumpliendo con reglas de normalización y se divide en 4 módulos lógicos principales:

### Modulo 1: Seguridad, Usuarios y Autenticacion

Gestiona el control de acceso y auditoría del sistema.

- usuario: Núcleo del acceso. Se relaciona con empleado y rol.
- rol y permiso: Definen los perfiles y las acciones permitidas.
- rol_permiso: Relación de muchos a muchos entre perfiles y permisos.
- sesion y login_intento: Manejo de tokens y auditoría de seguridad (intentos de acceso, IP, motivo de fallo).
- token_recuperacion: Gestión de enlaces de recuperación de contraseñas.

### Modulo 2: Personas y Perfiles Base

Centraliza la información personal evitando duplicidad de datos.

- persona: Tabla biográfica central. Todo individuo se registra aquí primero.
- Catalogos base: tipo_documento y sexo.
- persona_direccion y persona_telefono: Tablas normalizadas para múltiples datos de contacto.
- empleado, paciente y medico: Perfiles derivados de una persona. Se asocian a catálogos operativos como cargo.

### Modulo 3: Agendamiento y Citas

Gestiona la operatividad del centro médico y las reservas.

- horario: Define turnos, horas de entrada, salida y descansos.
- especialidad y medico_especialidad: Ramas médicas y su asignación a profesionales.
- cita: Tabla transaccional central que conecta al paciente, médico, horario y estado.
- Catalogos de cita: tipo_cita y estado_cita.
- cita_historial_estado: Auditoría estricta del rastro de cambios de estado de una reserva.

### Modulo 4: Atencion Clinica y Expediente

Almacena el historial y los actos médicos durante la consulta.

- antecedente y paciente_alergia: Historial médico previo y registro de sustancias/reacciones.
- detalle_cita: Acta de la consulta médica (notas y resumen).
- diagnostico y detalle_diagnostico: Catálogo estándar (CIE-10) y su asociación a una consulta.
- tratamiento, tratamiento_posologia y atencion_tratamiento: Catálogo de terapias y la receta final prescrita al paciente con dosis personalizadas.

## Casos de Uso y Endpoints por Rol

La API está segmentada mediante un estricto control de acceso basado en roles (RBAC).

### Endpoints Globales

- Login y cierre de sesión.
- Consulta de perfil.
- Actualización de datos propios.

### Casos de Uso: Administrador

Tiene acceso total al sistema, catálogos y gestión operativa.

- Gestión de Usuarios: Crear, listar, editar y desactivar usuarios.
- Gestión de Pacientes, Profesionales y Recepcionistas: Registro, actualización y desactivación.
- Reportes: Generación de reportes de citas por profesional y por paciente.

### Casos de Uso: Recepcionista

Enfocado en la atención al cliente y flujo de la clínica.

- Gestión de Citas: Agendar, consultar, cancelar y reprogramar.
- Gestión de Pacientes: Registrar, actualizar y obtener pacientes.
- Tiene habilitado el acceso a tablas auxiliares (antecedentes, direcciones, teléfonos, alergias e historial de estados).

### Casos de Uso: Profesional (Medico)

Enfocado estrictamente en la atención clínica.

- Gestión de Pacientes: Obtener lista de pacientes asignados.
- Gestión de Historia Clinica: Registrar y cerrar historia clínica (detalle de cita, diagnósticos y tratamientos). Consultar el historial del paciente.
- Historial de Citas: Consultar citas asignadas al profesional y el historial médico del paciente.

## Proceso de Depuracion y Pruebas

Durante el desarrollo, la API superó varias fases de reestructuración técnica:

1. Resolucion de Compatibilidad: Se realizaron configuraciones profundas en los controladores para solucionar problemas de comunicación e integración en el equipo, logrando una API completamente funcional.
2. Manejo de Dependencias: Se probaron los flujos transaccionales completos, validando los endpoints que dependen de otros para la creación y modificación de datos.
3. Depuracion de Errores: Se identificaron y resolvieron incidencias de integración crítica, tales como la referencia circular durante la serialización JSON (Error 500) y la corrección de inserciones de datos fuera de rango en columnas de entidades clave.

## Configuracion Local

1. Clonar el repositorio del proyecto.
2. Configurar la cadena de conexión de Supabase en el archivo appsettings.Development.json asegurando el uso del pooler IPv4.
3. Ejecutar el comando de migraciones para sincronizar la base de datos: dotnet ef database update
4. Ejecutar el proyecto: dotnet run
5. Acceder a la interfaz local de Swagger (ej. [http://localhost:PORT/swagger](https://www.google.com/url?sa=E&q=http://localhost:PORT/swagger)) para evaluar el estado del servicio y probar los endpoints.

