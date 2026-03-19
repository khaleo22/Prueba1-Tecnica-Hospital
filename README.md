[README.md](https://github.com/user-attachments/files/26106036/README.md)

#Prueba Tecnica

Aplicación ASP.NET Core MVC para administrar médicos, pacientes, citas, agendas y el historial de pacientes.
## Features

## Endpoints Implementados

| Recurso              | Operaciones                        | Descripción                                                                                      |
|----------------------|------------------------------------|--------------------------------------------------------------------------------------------------|
| Médicos              | Nombre, especialidad, horarios de consulta por día de la semana.                                 |
| Horarios disponibles | Muestra los dias y horas en los que esta disponibles los medicos.                                 |
| Pacientes            |Nombre, fecha de nacimiento, teléfono, correo electrónico                                        |
| Citas                | Agendar, Cancelar, Consultar       | Agendar: seleccionar médico, fecha, hora, paciente, motivo. Cancelar: con motivo de cancelación.|
| Agenda del día       |Todas las citas de un médico en un día, ordenadas por hora                                       |
| Historial            |Todas las citas (pasadas y futuras) de un paciente                                               |
| Horarios disponibles | En automatico muestra los horarios de disponibildad por dia entre cada medico      |




## Instalación y Configuración

### 1. Base de Datos
- Crear la base de datos `PruebaTecnica` en SQL Server.
- Ejecutar los scripts de tablas (`Citas`, `Medico`, `Paciente`, `Especialidades`).
- Ejecutar los Stored Procedures:
  - `sp_SelectCitasPorMedico`
  - `sp_SelectPacientes`
  - `sp_HistorialCitasPaciente`

### 2. Configuración de conexión
En los controladores, la cadena de conexión está definida como:

```csharp
private readonly string cs = "TuConexionLocal";
## Ejecución

Compilar el proyecto.

Ejecutar con IIS Express o dotnet run.

Acceder a:

https://localhost:44301/AgendaMedico/AgendaMedicos

https://localhost:44301/Historial/CitasPaciente

https://localhost:44301/Citas/Agendar

https://localhost:44301/Pacientes

https://localhost:44301/Doctores

https://localhost:44301/Horarios




 UX
Bootstrap para estilos.

SweetAlert2 para notificaciones en CRUD de horarios.

Emojis en menú para navegación más intuitiva:

 -Doctores
 -Pacientes
 -Horarios
 -Agendar Cita
 -Agenda Médico
 -Historial Paciente
 -Cerrar sesión

## Estructura SQL

1. Usuario
id → Identificador único del usuario.

usuario → Nombre de usuario para login.

contra → Contraseña.

estatus → Estado del usuario (activo/inactivo).

**Se usa para autenticación y control de acceso.**

2. Paciente
id → Identificador único del paciente.

nombre, aPaterno, aMaterno → Datos personales.

fechaNacimiento → Fecha de nacimiento.

correoElectronico → Email de contacto.

telefono → Teléfono.

**Contiene la información básica de los pacientes.**

3. Medico
Id → Identificador único del médico.

Nombre, APaterno, AMaterno → Datos personales.

Cedula → Cédula profesional.

IdEspecialista → Relación con la tabla Especialidades.

**Define a los médicos y su especialidad.**

4. Especialidades
IdEspecialidad → Identificador único.

concepto → Nombre de la especialidad (ej. Cardiología).

estatus → Estado (activo/inactivo).

tiempo → Duración estándar de la consulta.

**Catálogo de especialidades médicas.**

5. HorarioMedico
id → Identificador único.

idMedico → Relación con Medico.

diaSemana → Día de la semana (ej. Lunes).

horaInicio, horaFin → Horario de atención.

**Define la disponibilidad semanal de cada médico.**

6. HorarioDoctores
id → Identificador único.

idDoctor → Relación con Doctor.

dia → Día específico.

horaInicio, horaFinal → Horario asignado.

estatus → Estado del horario.

**Similar a HorarioMedico, pero más detallado para casos específicos.**

7. Citas
Id → Identificador único.

IdMedico → Relación con Medico.

IdPaciente → Relación con Paciente.

Horario → Hora de la cita.

Fecha → Fecha de la cita.

Estatus → Estado (Activa, Cancelada).

**Registra las citas médicas entre pacientes y doctores.**

8. Cancelaciones
Id → Identificador único.

IdCita → Relación con Citas.

Motivo → Razón de la cancelación.

FechaCancelacion → Fecha en que se canceló.

Guarda el historial de cancelaciones con motivo.

**Relaciones principales**
Paciente ↔ Citas ↔ Medico → Relación central del sistema.

Medico ↔ Especialidades → Cada médico tiene una especialidad.

Medico ↔ HorarioMedico/HorarioDoctores → Define disponibilidad.

Citas ↔ Cancelaciones → Una cita puede tener una cancelación asociada.


## Tecnologia

**Client:** MVC, C#, Javascript

**Server:** SQL Server
## Autor

- José Leonardo Ramos Téllez

