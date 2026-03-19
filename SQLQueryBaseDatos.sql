USE [master]
GO
/****** Object:  Database [PruebaTecnica]    Script Date: 18/03/2026 10:41:07 p. m. ******/
CREATE DATABASE [PruebaTecnica]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PruebaTecnica', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PruebaTecnica.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PruebaTecnica_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PruebaTecnica_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PruebaTecnica] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PruebaTecnica].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PruebaTecnica] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PruebaTecnica] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PruebaTecnica] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PruebaTecnica] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PruebaTecnica] SET ARITHABORT OFF 
GO
ALTER DATABASE [PruebaTecnica] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PruebaTecnica] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PruebaTecnica] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PruebaTecnica] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PruebaTecnica] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PruebaTecnica] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PruebaTecnica] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PruebaTecnica] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PruebaTecnica] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PruebaTecnica] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PruebaTecnica] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PruebaTecnica] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PruebaTecnica] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PruebaTecnica] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PruebaTecnica] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PruebaTecnica] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PruebaTecnica] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PruebaTecnica] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PruebaTecnica] SET  MULTI_USER 
GO
ALTER DATABASE [PruebaTecnica] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PruebaTecnica] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PruebaTecnica] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PruebaTecnica] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PruebaTecnica] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PruebaTecnica] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PruebaTecnica] SET QUERY_STORE = OFF
GO
USE [PruebaTecnica]
GO
/****** Object:  Table [dbo].[Cancelaciones]    Script Date: 18/03/2026 10:41:07 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cancelaciones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCita] [int] NOT NULL,
	[Motivo] [nvarchar](200) NOT NULL,
	[FechaCancelacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Citas]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Citas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdMedico] [int] NOT NULL,
	[IdPaciente] [int] NOT NULL,
	[Horario] [varchar](30) NOT NULL,
	[Estatus] [int] NOT NULL,
	[Fecha] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Especialidades]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Especialidades](
	[IdEspecialidad] [int] IDENTITY(1,1) NOT NULL,
	[concepto] [varchar](100) NOT NULL,
	[estatus] [int] NULL,
	[tiempo] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEspecialidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HorarioDoctores]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HorarioDoctores](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idDoctor] [int] NULL,
	[dia] [varchar](20) NULL,
	[horaInicio] [varchar](10) NULL,
	[horaFinal] [varchar](10) NULL,
	[estatus] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HorarioMedico]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HorarioMedico](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idMedico] [int] NOT NULL,
	[diaSemana] [varchar](20) NOT NULL,
	[horaInicio] [time](7) NOT NULL,
	[horaFin] [time](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medico]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medico](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[APaterno] [varchar](50) NOT NULL,
	[AMaterno] [varchar](50) NULL,
	[Cedula] [varchar](20) NOT NULL,
	[IdEspecialista] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Paciente]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paciente](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[aPaterno] [varchar](50) NOT NULL,
	[aMaterno] [varchar](50) NULL,
	[fechaNacimiento] [date] NOT NULL,
	[correoElectronico] [varchar](100) NOT NULL,
	[telefono] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[usuario] [varchar](35) NULL,
	[contra] [varchar](100) NULL,
	[estatus] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cancelaciones] ADD  DEFAULT (getdate()) FOR [FechaCancelacion]
GO
ALTER TABLE [dbo].[Citas] ADD  DEFAULT ((1)) FOR [Estatus]
GO
ALTER TABLE [dbo].[Cancelaciones]  WITH CHECK ADD FOREIGN KEY([IdCita])
REFERENCES [dbo].[Citas] ([Id])
GO
ALTER TABLE [dbo].[HorarioDoctores]  WITH CHECK ADD  CONSTRAINT [FK_HorarioMedico_Medico] FOREIGN KEY([idDoctor])
REFERENCES [dbo].[Medico] ([Id])
GO
ALTER TABLE [dbo].[HorarioDoctores] CHECK CONSTRAINT [FK_HorarioMedico_Medico]
GO
ALTER TABLE [dbo].[HorarioMedico]  WITH CHECK ADD FOREIGN KEY([idMedico])
REFERENCES [dbo].[Medico] ([Id])
GO
ALTER TABLE [dbo].[Medico]  WITH CHECK ADD  CONSTRAINT [FK_Medico_Especialidad] FOREIGN KEY([IdEspecialista])
REFERENCES [dbo].[Especialidades] ([IdEspecialidad])
GO
ALTER TABLE [dbo].[Medico] CHECK CONSTRAINT [FK_Medico_Especialidad]
GO
/****** Object:  StoredProcedure [dbo].[sp_CancelarCita]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_CancelarCita]
    @IdCita INT,
    @Motivo NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    -- Actualizar estatus de la cita
    UPDATE Citas
    SET Estatus = 2
    WHERE Id = @IdCita;

    -- Insertar en tabla Cancelaciones
    INSERT INTO Cancelaciones (IdCita, Motivo)
    VALUES (@IdCita, @Motivo);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ConsultaEspecialidades]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ConsultaEspecialidades]

AS
BEGIN

SELECT 
    IdEspecialidad,
    concepto
FROM Especialidades
WHERE estatus = 1
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ContarCancelaciones]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_ContarCancelaciones]
    @IdPaciente INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) 
    FROM Cancelaciones c
    INNER JOIN Citas ci ON c.IdCita = ci.Id
    WHERE ci.IdPaciente = @IdPaciente
      AND c.FechaCancelacion >= DATEADD(DAY, -30, GETDATE());
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteMedico]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteMedico]
(
    @id INT
)
AS
BEGIN

DELETE FROM Medico
WHERE id = @id

END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeletePaciente]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeletePaciente]
    @id INT
AS
BEGIN
    DELETE FROM Paciente WHERE id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_HistorialCitasPaciente]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_HistorialCitasPaciente]
    @IdPaciente INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        c.Id,
        c.Fecha,
        c.Horario,
        m.Nombre + ' ' + m.APaterno AS NombreMedico,
        e.Concepto AS Especialidad,
        c.Estatus
    FROM Citas c
    INNER JOIN Medico m ON c.IdMedico = m.Id
    INNER JOIN Especialidades e ON m.IdEspecialista = e.IdEspecialidad
    WHERE c.IdPaciente = @IdPaciente
    ORDER BY c.Fecha DESC, 
             TRY_CAST(LEFT(c.Horario, CHARINDEX('-', c.Horario) - 1) AS TIME);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertCita]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertCita]
    @IdMedico INT,
    @IdPaciente INT,
    @Fecha DATE,
    @Horario VARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Citas (IdMedico, IdPaciente, Fecha, Horario, Estatus)
    VALUES (@IdMedico, @IdPaciente, @Fecha, @Horario, 1);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertHorarioMedico]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertHorarioMedico]
    @idMedico INT,
    @diaSemana VARCHAR(20),
    @horaInicio VARCHAR(10),
    @horaFin VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO HorarioDoctores (idDoctor, dia, horaInicio, horaFinal, estatus)
        VALUES (@idMedico, @diaSemana, @horaInicio, @horaFin, 1);
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        THROW 50000, @ErrorMessage, 1;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertMedico]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertMedico]
(
    @nombre VARCHAR(50),
    @aPaterno VARCHAR(50),
    @aMaterno VARCHAR(50),
    @cedula VARCHAR(50),
    @idEspecialista INT
)
AS
BEGIN

INSERT INTO Medico
(
    nombre,
    aPaterno,
    aMaterno,
    cedula,
    idEspecialista
)
VALUES
(
    @nombre,
    @aPaterno,
    @aMaterno,
    @cedula,
    @idEspecialista
)

END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertPaciente]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertPaciente]
    @nombre VARCHAR(50),
    @aPaterno VARCHAR(50),
    @aMaterno VARCHAR(50),
    @fechaNacimiento DATE,
    @correoElectronico VARCHAR(100),
    @telefono VARCHAR(20)
AS
BEGIN
    INSERT INTO Paciente (nombre, aPaterno, aMaterno, fechaNacimiento, correoElectronico, telefono)
    VALUES (@nombre, @aPaterno, @aMaterno, @fechaNacimiento, @correoElectronico, @telefono);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_LoginUsuario]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_LoginUsuario]
(
    @usuario VARCHAR(50),
    @password VARCHAR(50)
)
AS
BEGIN

SELECT 
    id,
    usuario
FROM Usuario
WHERE usuario = @usuario
AND contra = @password

END
GO
/****** Object:  StoredProcedure [dbo].[sp_SelectCitas]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SelectCitas]
    @IdPaciente INT,
    @Fecha DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT c.Id,
           c.Fecha,
           c.Horario,
           c.Estatus,
           p.Nombre AS NombrePaciente,
           m.Nombre AS NombreMedico,
           e.concepto AS Especialidad
    FROM Citas c
    INNER JOIN Paciente p ON c.IdPaciente = p.Id
    INNER JOIN Medico m ON c.IdMedico = m.Id
    INNER JOIN Especialidades e ON m.IdEspecialista = e.IdEspecialidad
    WHERE c.IdPaciente = @IdPaciente
      AND c.Fecha = @Fecha
    ORDER BY c.Fecha DESC, c.Horario DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SelectCitasPorMedico]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_SelectCitasPorMedico]
    @IdMedico INT,
    @Fecha DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        c.Id,
        c.IdPaciente,
        p.Nombre + ' ' + p.APaterno AS NombrePaciente,
        c.IdMedico,
        m.Nombre + ' ' + m.APaterno AS NombreMedico,
        e.concepto AS Especialidad,
        c.Fecha,
        c.Horario,
        c.Estatus
    FROM Citas c
    INNER JOIN Paciente p ON c.IdPaciente = p.Id
    INNER JOIN Medico m ON c.IdMedico = m.Id
    INNER JOIN Especialidades e ON m.IdEspecialista = e.IdEspecialidad
    WHERE c.IdMedico = @IdMedico
      AND CAST(c.Fecha AS DATE) = @Fecha
    ORDER BY 
        TRY_CAST(LEFT(c.Horario, CHARINDEX('-', c.Horario) - 1) AS TIME);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SelectDiasDoctor]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SelectDiasDoctor]
    @idDoctor INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT dia
    FROM HorarioDoctores
    WHERE idDoctor = @idDoctor;
END

GO
/****** Object:  StoredProcedure [dbo].[sp_SelectDuracionEspecialidad]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SelectDuracionEspecialidad]
    @idDoctor INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT e.Tiempo
    FROM Especialidades e
    INNER JOIN Medico d ON e.IdEspecialidad = d.IdEspecialista
    WHERE d.Id = @idDoctor;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SelectHorarioDoctor]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SelectHorarioDoctor]
    @idDoctor INT,
    @dia NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT horaInicio, horaFinal
    FROM HorarioDoctores
    WHERE idDoctor = @idDoctor
      AND dia = @dia;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SelectHorariosMedico]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SelectHorariosMedico]
AS
BEGIN
    SELECT h.id, h.idDoctor, d.Nombre, d.APaterno, h.dia, h.horaInicio, h.horaFinal, h.estatus
    FROM HorarioDoctores h
    INNER JOIN Medico d ON h.idDoctor = d.Id;
END

GO
/****** Object:  StoredProcedure [dbo].[sp_SelectMedicos]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SelectMedicos]
AS
BEGIN

    SELECT 
        m.Id,
        m.Nombre,
        m.APaterno,
        m.AMaterno,
        m.Cedula,
        m.IdEspecialista,
        e.Concepto AS EspecialidadConcepto
    FROM Medico AS m
    INNER JOIN Especialidades AS e 
        ON m.IdEspecialista = e.IdEspecialidad;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SelectPacientes]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_SelectPacientes]
AS
BEGIN
    SELECT id, nombre, aPaterno, aMaterno, fechaNacimiento, correoElectronico, telefono
    FROM Paciente;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SelectPacientes1]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SelectPacientes1]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Nombre + ' ' + Apaterno AS NombreCompleto
    FROM Paciente
    ORDER BY NombreCompleto;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateMedico]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateMedico]
(
    @id INT,
    @nombre VARCHAR(50),
    @aPaterno VARCHAR(50),
    @aMaterno VARCHAR(50),
    @cedula VARCHAR(50),
    @idEspecialista INT
)
AS
BEGIN

UPDATE Medico
SET
    nombre = @nombre,
    aPaterno = @aPaterno,
    aMaterno = @aMaterno,
    cedula = @cedula,
    idEspecialista = @idEspecialista
WHERE id = @id

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdatePaciente]    Script Date: 18/03/2026 10:41:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdatePaciente]
    @id INT,
    @nombre VARCHAR(50),
    @aPaterno VARCHAR(50),
    @aMaterno VARCHAR(50),
    @fechaNacimiento DATE,
    @correoElectronico VARCHAR(100),
    @telefono VARCHAR(20)
AS
BEGIN
    UPDATE Paciente
    SET nombre = @nombre,
        aPaterno = @aPaterno,
        aMaterno = @aMaterno,
        fechaNacimiento = @fechaNacimiento,
        correoElectronico = @correoElectronico,
        telefono = @telefono
    WHERE id = @id;
END
GO
USE [master]
GO
ALTER DATABASE [PruebaTecnica] SET  READ_WRITE 
GO
