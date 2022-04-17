USE [master]
GO
/****** Object:  Database [CUCMarca]    Script Date: 15/6/2020 10:33:08 ******/
CREATE DATABASE [CUCMarca]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CUCMarca', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.QSOFT2017\MSSQL\DATA\CUCMarca.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CUCMarca_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.QSOFT2017\MSSQL\DATA\CUCMarca_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CUCMarca] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CUCMarca].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CUCMarca] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CUCMarca] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CUCMarca] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CUCMarca] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CUCMarca] SET ARITHABORT OFF 
GO
ALTER DATABASE [CUCMarca] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CUCMarca] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CUCMarca] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CUCMarca] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CUCMarca] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CUCMarca] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CUCMarca] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CUCMarca] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CUCMarca] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CUCMarca] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CUCMarca] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CUCMarca] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CUCMarca] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CUCMarca] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CUCMarca] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CUCMarca] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CUCMarca] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CUCMarca] SET RECOVERY FULL 
GO
ALTER DATABASE [CUCMarca] SET  MULTI_USER 
GO
ALTER DATABASE [CUCMarca] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CUCMarca] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CUCMarca] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CUCMarca] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CUCMarca] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'CUCMarca', N'ON'
GO
ALTER DATABASE [CUCMarca] SET QUERY_STORE = OFF
GO
USE [CUCMarca]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 15/6/2020 10:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Area]    Script Date: 15/6/2020 10:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Area](
	[AreaID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[TipoAreaID] [int] NOT NULL,
	[Jefe] [int] NULL,
 CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED 
(
	[AreaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Asistencia]    Script Date: 15/6/2020 10:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Asistencia](
	[AsistenciaID] [bigint] IDENTITY(1,1) NOT NULL,
	[CodigoFuncionario] [varchar](20) NOT NULL,
	[FechaAsistencia] [datetime] NOT NULL,
	[TipoMarca] [char](1) NOT NULL,
	[Actividad] [varchar](500) NOT NULL,
	[Comentarios] [varchar](max) NOT NULL,
	[DireccionIP] [varchar](50) NULL,
	[Latitud] [decimal](18, 15) NULL,
	[Longitud] [decimal](18, 15) NULL,
 CONSTRAINT [PK_Asistencia] PRIMARY KEY CLUSTERED 
(
	[AsistenciaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 15/6/2020 10:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 15/6/2020 10:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 15/6/2020 10:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 15/6/2020 10:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 15/6/2020 10:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Excepcion]    Script Date: 15/6/2020 10:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Excepcion](
	[ExcepcionID] [int] IDENTITY(1,1) NOT NULL,
	[FechaExcepcion] [date] NOT NULL,
	[CodigoFuncionario] [varchar](20) NOT NULL,
	[Estado] [int] NOT NULL,
	[AutorizadoPor] [int] NOT NULL,
	[FechaAutorizacion] [datetime] NOT NULL,
	[Observaciones] [varchar](500) NULL,
	[MotivoID] [int] NOT NULL,
	[ReponeTiempo] [bit] NOT NULL,
	[FechaReposicion] [date] NULL,
 CONSTRAINT [PK_Excepcion] PRIMARY KEY CLUSTERED 
(
	[ExcepcionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Funcionario]    Script Date: 15/6/2020 10:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Funcionario](
	[FuncionarioID] [int] IDENTITY(1,1) NOT NULL,
	[TipoIdentificacionID] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[TipoFuncionarioID] [int] NOT NULL,
	[Correo] [varchar](300) NOT NULL,
	[Contrasena] [varchar](50) NOT NULL,
	[Identificacion] [varchar](20) NOT NULL,
	[Estado] [tinyint] NULL,
 CONSTRAINT [PK_Funcionario] PRIMARY KEY CLUSTERED 
(
	[FuncionarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FuncionarioArea]    Script Date: 15/6/2020 10:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FuncionarioArea](
	[CodigoFuncionario] [varchar](20) NOT NULL,
	[FuncionarioID] [int] NOT NULL,
	[AreaID] [int] NOT NULL,
 CONSTRAINT [PK_FuncionarioArea] PRIMARY KEY CLUSTERED 
(
	[CodigoFuncionario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Horario]    Script Date: 15/6/2020 10:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Horario](
	[HorarioId] [int] IDENTITY(1,1) NOT NULL,
	[Cuatrimestre] [tinyint] NOT NULL,
	[Anio] [int] NOT NULL,
	[Estado] [tinyint] NOT NULL,
	[CodigoFuncionario] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Horario] PRIMARY KEY CLUSTERED 
(
	[HorarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HorarioDetalle]    Script Date: 15/6/2020 10:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HorarioDetalle](
	[HorarioID] [int] NOT NULL,
	[Dia] [int] NOT NULL,
	[HoraIngreso] [tinyint] NOT NULL,
	[MinutoIngreso] [tinyint] NOT NULL,
	[HoraSalida] [tinyint] NOT NULL,
	[MinutoSalida] [tinyint] NOT NULL,
 CONSTRAINT [PK_HorarioDetalle] PRIMARY KEY CLUSTERED 
(
	[HorarioID] ASC,
	[Dia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inconsistencia]    Script Date: 15/6/2020 10:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inconsistencia](
	[InconsistenciaID] [int] IDENTITY(1,1) NOT NULL,
	[HorarioID] [int] NOT NULL,
	[CodigoFuncionario] [varchar](20) NOT NULL,
	[FechaInconsistencia] [datetime] NOT NULL,
	[Estado] [tinyint] NOT NULL,
	[Notificar] [bit] NOT NULL,
	[TipoInconsistenciaID] [int] NOT NULL,
	[RegistradoPor] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Inconsistencia] PRIMARY KEY CLUSTERED 
(
	[InconsistenciaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Justificacion]    Script Date: 15/6/2020 10:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Justificacion](
	[JustificacionID] [int] IDENTITY(1,1) NOT NULL,
	[InconsistenciaID] [int] NOT NULL,
	[CodigoFuncionario] [varchar](20) NOT NULL,
	[ReponeTiempo] [bit] NOT NULL,
	[FechaReposicion] [date] NULL,
	[Observaciones] [varchar](500) NOT NULL,
	[FechaJustificacion] [datetime] NOT NULL,
	[MotivoID] [int] NOT NULL,
	[AutorizadoPor] [int] NULL,
	[FechaAutorizacion] [datetime] NULL,
 CONSTRAINT [PK_Justificacion] PRIMARY KEY CLUSTERED 
(
	[JustificacionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motivo]    Script Date: 15/6/2020 10:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motivo](
	[MotivoID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Motivo] PRIMARY KEY CLUSTERED 
(
	[MotivoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoArea]    Script Date: 15/6/2020 10:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoArea](
	[TipoAreaID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
 CONSTRAINT [PK_TipoArea] PRIMARY KEY CLUSTERED 
(
	[TipoAreaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoFuncionario]    Script Date: 15/6/2020 10:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoFuncionario](
	[TipoFuncionarioID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TipoFuncionario] PRIMARY KEY CLUSTERED 
(
	[TipoFuncionarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoIdentificacion]    Script Date: 15/6/2020 10:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoIdentificacion](
	[TipoIdentificacionID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TipoIdentificacion] PRIMARY KEY CLUSTERED 
(
	[TipoIdentificacionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoInconsistencia]    Script Date: 15/6/2020 10:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoInconsistencia](
	[TipoInconsistenciaID] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TipoInconsistencia] PRIMARY KEY CLUSTERED 
(
	[TipoInconsistenciaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 15/6/2020 10:33:08 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 15/6/2020 10:33:08 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 15/6/2020 10:33:08 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 15/6/2020 10:33:08 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 15/6/2020 10:33:08 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 15/6/2020 10:33:08 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Funcionario_TipoFuncionario]    Script Date: 15/6/2020 10:33:08 ******/
CREATE NONCLUSTERED INDEX [IX_FK_Funcionario_TipoFuncionario] ON [dbo].[Funcionario]
(
	[TipoFuncionarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Funcionario_TipoIdentificacion]    Script Date: 15/6/2020 10:33:08 ******/
CREATE NONCLUSTERED INDEX [IX_FK_Funcionario_TipoIdentificacion] ON [dbo].[Funcionario]
(
	[TipoIdentificacionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Area]  WITH CHECK ADD  CONSTRAINT [FK_Area_Funcionario] FOREIGN KEY([Jefe])
REFERENCES [dbo].[Funcionario] ([FuncionarioID])
GO
ALTER TABLE [dbo].[Area] CHECK CONSTRAINT [FK_Area_Funcionario]
GO
ALTER TABLE [dbo].[Area]  WITH CHECK ADD  CONSTRAINT [FK_Area_TipoArea] FOREIGN KEY([TipoAreaID])
REFERENCES [dbo].[TipoArea] ([TipoAreaID])
GO
ALTER TABLE [dbo].[Area] CHECK CONSTRAINT [FK_Area_TipoArea]
GO
ALTER TABLE [dbo].[Asistencia]  WITH CHECK ADD  CONSTRAINT [FK_Asistencia_FuncionarioArea] FOREIGN KEY([CodigoFuncionario])
REFERENCES [dbo].[FuncionarioArea] ([CodigoFuncionario])
GO
ALTER TABLE [dbo].[Asistencia] CHECK CONSTRAINT [FK_Asistencia_FuncionarioArea]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Excepcion]  WITH CHECK ADD  CONSTRAINT [FK_Excepcion_Funcionario] FOREIGN KEY([AutorizadoPor])
REFERENCES [dbo].[Funcionario] ([FuncionarioID])
GO
ALTER TABLE [dbo].[Excepcion] CHECK CONSTRAINT [FK_Excepcion_Funcionario]
GO
ALTER TABLE [dbo].[Excepcion]  WITH CHECK ADD  CONSTRAINT [FK_Excepcion_FuncionarioArea] FOREIGN KEY([CodigoFuncionario])
REFERENCES [dbo].[FuncionarioArea] ([CodigoFuncionario])
GO
ALTER TABLE [dbo].[Excepcion] CHECK CONSTRAINT [FK_Excepcion_FuncionarioArea]
GO
ALTER TABLE [dbo].[Excepcion]  WITH CHECK ADD  CONSTRAINT [FK_Excepcion_Motivo] FOREIGN KEY([MotivoID])
REFERENCES [dbo].[Motivo] ([MotivoID])
GO
ALTER TABLE [dbo].[Excepcion] CHECK CONSTRAINT [FK_Excepcion_Motivo]
GO
ALTER TABLE [dbo].[Funcionario]  WITH CHECK ADD  CONSTRAINT [FK_Funcionario_TipoFuncionario] FOREIGN KEY([TipoFuncionarioID])
REFERENCES [dbo].[TipoFuncionario] ([TipoFuncionarioID])
GO
ALTER TABLE [dbo].[Funcionario] CHECK CONSTRAINT [FK_Funcionario_TipoFuncionario]
GO
ALTER TABLE [dbo].[Funcionario]  WITH CHECK ADD  CONSTRAINT [FK_Funcionario_TipoIdentificacion] FOREIGN KEY([TipoIdentificacionID])
REFERENCES [dbo].[TipoIdentificacion] ([TipoIdentificacionID])
GO
ALTER TABLE [dbo].[Funcionario] CHECK CONSTRAINT [FK_Funcionario_TipoIdentificacion]
GO
ALTER TABLE [dbo].[FuncionarioArea]  WITH CHECK ADD  CONSTRAINT [FK_FuncionarioArea_Area] FOREIGN KEY([AreaID])
REFERENCES [dbo].[Area] ([AreaID])
GO
ALTER TABLE [dbo].[FuncionarioArea] CHECK CONSTRAINT [FK_FuncionarioArea_Area]
GO
ALTER TABLE [dbo].[FuncionarioArea]  WITH CHECK ADD  CONSTRAINT [FK_FuncionarioArea_Funcionario] FOREIGN KEY([FuncionarioID])
REFERENCES [dbo].[Funcionario] ([FuncionarioID])
GO
ALTER TABLE [dbo].[FuncionarioArea] CHECK CONSTRAINT [FK_FuncionarioArea_Funcionario]
GO
ALTER TABLE [dbo].[Horario]  WITH CHECK ADD  CONSTRAINT [FK_Horario_FuncionarioArea] FOREIGN KEY([CodigoFuncionario])
REFERENCES [dbo].[FuncionarioArea] ([CodigoFuncionario])
GO
ALTER TABLE [dbo].[Horario] CHECK CONSTRAINT [FK_Horario_FuncionarioArea]
GO
ALTER TABLE [dbo].[HorarioDetalle]  WITH CHECK ADD  CONSTRAINT [FK_HorarioDetalle_Horario] FOREIGN KEY([HorarioID])
REFERENCES [dbo].[Horario] ([HorarioId])
GO
ALTER TABLE [dbo].[HorarioDetalle] CHECK CONSTRAINT [FK_HorarioDetalle_Horario]
GO
ALTER TABLE [dbo].[Inconsistencia]  WITH CHECK ADD  CONSTRAINT [FK_Inconsistencia_FuncionarioArea] FOREIGN KEY([CodigoFuncionario])
REFERENCES [dbo].[FuncionarioArea] ([CodigoFuncionario])
GO
ALTER TABLE [dbo].[Inconsistencia] CHECK CONSTRAINT [FK_Inconsistencia_FuncionarioArea]
GO
ALTER TABLE [dbo].[Inconsistencia]  WITH CHECK ADD  CONSTRAINT [FK_Inconsistencia_Horario] FOREIGN KEY([HorarioID])
REFERENCES [dbo].[Horario] ([HorarioId])
GO
ALTER TABLE [dbo].[Inconsistencia] CHECK CONSTRAINT [FK_Inconsistencia_Horario]
GO
ALTER TABLE [dbo].[Inconsistencia]  WITH CHECK ADD  CONSTRAINT [FK_Inconsistencia_TipoInconsistencia] FOREIGN KEY([TipoInconsistenciaID])
REFERENCES [dbo].[TipoInconsistencia] ([TipoInconsistenciaID])
GO
ALTER TABLE [dbo].[Inconsistencia] CHECK CONSTRAINT [FK_Inconsistencia_TipoInconsistencia]
GO
ALTER TABLE [dbo].[Justificacion]  WITH CHECK ADD  CONSTRAINT [FK_Justificacion_FuncionarioArea] FOREIGN KEY([CodigoFuncionario])
REFERENCES [dbo].[FuncionarioArea] ([CodigoFuncionario])
GO
ALTER TABLE [dbo].[Justificacion] CHECK CONSTRAINT [FK_Justificacion_FuncionarioArea]
GO
ALTER TABLE [dbo].[Justificacion]  WITH CHECK ADD  CONSTRAINT [FK_Justificacion_Inconsistencia] FOREIGN KEY([InconsistenciaID])
REFERENCES [dbo].[Inconsistencia] ([InconsistenciaID])
GO
ALTER TABLE [dbo].[Justificacion] CHECK CONSTRAINT [FK_Justificacion_Inconsistencia]
GO
ALTER TABLE [dbo].[Justificacion]  WITH CHECK ADD  CONSTRAINT [FK_Justificacion_Motivo] FOREIGN KEY([MotivoID])
REFERENCES [dbo].[Motivo] ([MotivoID])
GO
ALTER TABLE [dbo].[Justificacion] CHECK CONSTRAINT [FK_Justificacion_Motivo]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del área' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Area', @level2type=N'COLUMN',@level2name=N'AreaID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del área' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Area', @level2type=N'COLUMN',@level2name=N'Nombre'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo del área (administración, academia, técnica, etc)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Area', @level2type=N'COLUMN',@level2name=N'TipoAreaID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Funcionario jefe del área' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Area', @level2type=N'COLUMN',@level2name=N'Jefe'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Secuencia de la asistencia' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Asistencia', @level2type=N'COLUMN',@level2name=N'AsistenciaID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo con el que se relaciona la marca' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Asistencia', @level2type=N'COLUMN',@level2name=N'CodigoFuncionario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de la marca' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Asistencia', @level2type=N'COLUMN',@level2name=N'FechaAsistencia'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de marca E entrada S Salida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Asistencia', @level2type=N'COLUMN',@level2name=N'TipoMarca'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Título de la actividad realizada' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Asistencia', @level2type=N'COLUMN',@level2name=N'Actividad'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Detalle de la actividad realizada' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Asistencia', @level2type=N'COLUMN',@level2name=N'Comentarios'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Dirección IP desde donde se realiza la marca' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Asistencia', @level2type=N'COLUMN',@level2name=N'DireccionIP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Latitud desde donde se realizó la marca' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Asistencia', @level2type=N'COLUMN',@level2name=N'Latitud'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Longitud desde donde se realizó la marca' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Asistencia', @level2type=N'COLUMN',@level2name=N'Longitud'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador único de la excepción' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Excepcion', @level2type=N'COLUMN',@level2name=N'ExcepcionID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que se realizará la excepcion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Excepcion', @level2type=N'COLUMN',@level2name=N'FechaExcepcion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Funcionario área al que se aplica la excepcion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Excepcion', @level2type=N'COLUMN',@level2name=N'CodigoFuncionario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 Activa 2 vencida' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Excepcion', @level2type=N'COLUMN',@level2name=N'Estado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Funcionario que autoriza la excepción' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Excepcion', @level2type=N'COLUMN',@level2name=N'AutorizadoPor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha hora en que se realiza el registro de la excepcion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Excepcion', @level2type=N'COLUMN',@level2name=N'FechaAutorizacion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Comentarios respecto a la excepcion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Excepcion', @level2type=N'COLUMN',@level2name=N'Observaciones'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Motivo de la excepcion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Excepcion', @level2type=N'COLUMN',@level2name=N'MotivoID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indica si el funcionario repone el tiempo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Excepcion', @level2type=N'COLUMN',@level2name=N'ReponeTiempo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indica la fecha en que se repondrá el tiempo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Excepcion', @level2type=N'COLUMN',@level2name=N'FechaReposicion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador único del funcionario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Funcionario', @level2type=N'COLUMN',@level2name=N'FuncionarioID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de identificacion de acuerdo al catalogo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Funcionario', @level2type=N'COLUMN',@level2name=N'TipoIdentificacionID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del funcionario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Funcionario', @level2type=N'COLUMN',@level2name=N'Nombre'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Apellido del funcionario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Funcionario', @level2type=N'COLUMN',@level2name=N'Apellido'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de funcionario de acuerdo al catalogo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Funcionario', @level2type=N'COLUMN',@level2name=N'TipoFuncionarioID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Correo del funcionario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Funcionario', @level2type=N'COLUMN',@level2name=N'Correo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contraseña para realizar la marca' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Funcionario', @level2type=N'COLUMN',@level2name=N'Contrasena'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificacion del funcionario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Funcionario', @level2type=N'COLUMN',@level2name=N'Identificacion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 Activo 2 Inactivo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Funcionario', @level2type=N'COLUMN',@level2name=N'Estado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código que se utiliza para realizar la marca' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FuncionarioArea', @level2type=N'COLUMN',@level2name=N'CodigoFuncionario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Funcionario al que pertenece el código' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FuncionarioArea', @level2type=N'COLUMN',@level2name=N'FuncionarioID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Area a la que pertenece el funcionario con la que se relaciona' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FuncionarioArea', @level2type=N'COLUMN',@level2name=N'AreaID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del horario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Horario', @level2type=N'COLUMN',@level2name=N'HorarioId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cuatrimestre en que está vigente el horario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Horario', @level2type=N'COLUMN',@level2name=N'Cuatrimestre'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Año en que está vigente el horario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Horario', @level2type=N'COLUMN',@level2name=N'Anio'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del horario 1:Vigente 2: Vencido' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Horario', @level2type=N'COLUMN',@level2name=N'Estado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código de funcionario-area a la que pertenece el horario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Horario', @level2type=N'COLUMN',@level2name=N'CodigoFuncionario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del horario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HorarioDetalle', @level2type=N'COLUMN',@level2name=N'HorarioID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 al 7 donde 1 es Lunes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HorarioDetalle', @level2type=N'COLUMN',@level2name=N'Dia'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hora de ingreso formato 24hrs' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HorarioDetalle', @level2type=N'COLUMN',@level2name=N'HoraIngreso'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Minuto en el que ingresa' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HorarioDetalle', @level2type=N'COLUMN',@level2name=N'MinutoIngreso'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hora de salida formato 24hrs' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HorarioDetalle', @level2type=N'COLUMN',@level2name=N'HoraSalida'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Minuto en el que sale' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HorarioDetalle', @level2type=N'COLUMN',@level2name=N'MinutoSalida'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador único de la inconsistencia' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Inconsistencia', @level2type=N'COLUMN',@level2name=N'InconsistenciaID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Horario para el que se registra la inconsistencia' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Inconsistencia', @level2type=N'COLUMN',@level2name=N'HorarioID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Relación de la inconsistencia con el respectivo funcionario al que se le crea' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Inconsistencia', @level2type=N'COLUMN',@level2name=N'CodigoFuncionario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha hora en que se registra la inconsistencia' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Inconsistencia', @level2type=N'COLUMN',@level2name=N'FechaInconsistencia'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1:Activa 2:Justificada 3:Aplicada 4:Descartada' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Inconsistencia', @level2type=N'COLUMN',@level2name=N'Estado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indica si la inconsistencia registrada se debe notificar al usuario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Inconsistencia', @level2type=N'COLUMN',@level2name=N'Notificar'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de inconsistencia de acuerdo al catálogo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Inconsistencia', @level2type=N'COLUMN',@level2name=N'TipoInconsistenciaID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fuente de registro de la inconsistencia' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Inconsistencia', @level2type=N'COLUMN',@level2name=N'RegistradoPor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador único de la justificación' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Justificacion', @level2type=N'COLUMN',@level2name=N'JustificacionID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Inconsistencia que se justifica' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Justificacion', @level2type=N'COLUMN',@level2name=N'InconsistenciaID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Funcionario y área que justifica' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Justificacion', @level2type=N'COLUMN',@level2name=N'CodigoFuncionario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indica si el funcionario repone el tiempo de la inconsistencia' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Justificacion', @level2type=N'COLUMN',@level2name=N'ReponeTiempo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Si el usuario repone el tiempo, debe indicar la fecha en que lo hace' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Justificacion', @level2type=N'COLUMN',@level2name=N'FechaReposicion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Observaciones respecto a la inconsistencia' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Justificacion', @level2type=N'COLUMN',@level2name=N'Observaciones'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que se registra la justificacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Justificacion', @level2type=N'COLUMN',@level2name=N'FechaJustificacion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del motivo de la justificación' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Justificacion', @level2type=N'COLUMN',@level2name=N'MotivoID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Funcionario que autoriza la justificación' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Justificacion', @level2type=N'COLUMN',@level2name=N'AutorizadoPor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha hora en que se autorizó' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Justificacion', @level2type=N'COLUMN',@level2name=N'FechaAutorizacion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador único del motivo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Motivo', @level2type=N'COLUMN',@level2name=N'MotivoID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del motivo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Motivo', @level2type=N'COLUMN',@level2name=N'Nombre'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo del área' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TipoArea', @level2type=N'COLUMN',@level2name=N'TipoAreaID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del área, academia, administración, etc' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TipoArea', @level2type=N'COLUMN',@level2name=N'Nombre'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de funcionario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TipoFuncionario', @level2type=N'COLUMN',@level2name=N'TipoFuncionarioID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del tipo: administrativo, académico, etc' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TipoFuncionario', @level2type=N'COLUMN',@level2name=N'Nombre'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del tipo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TipoIdentificacion', @level2type=N'COLUMN',@level2name=N'TipoIdentificacionID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del tipo de identificación' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TipoIdentificacion', @level2type=N'COLUMN',@level2name=N'Nombre'
GO
USE [CUCMarca]
GO
ALTER DATABASE [CUCMarca] SET  READ_WRITE 
GO

CREATE TABLE [dbo].[Bitacora](
	[Identificador] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Accion] [varchar](100) NOT NULL,
	[Descripcion] [varchar](200) NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
	[Tipo] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Identificador] PRIMARY KEY CLUSTERED 
(
	[Identificador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

