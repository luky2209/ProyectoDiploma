USE [is--servicios]
GO

/****** Tabla Profesor para el caso de uso "Asignar profesor a clase" ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profesor](
	[LegajoProfesor] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Especialidad] [varchar](50) NOT NULL,
	[FranjaHorariaDisponible] [varchar](50) NOT NULL,
	[DVH] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[LegajoProfesor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** El profesor solo puede ser recreativo o competitivo, igual que el tipo de clase ******/
ALTER TABLE [dbo].[Profesor] ADD CONSTRAINT [CK_Profesor_Especialidad] CHECK ([Especialidad] IN ('Recreativa', 'Competitiva'))
GO

/****** Profesores de ejemplo ******/
SET IDENTITY_INSERT [dbo].[Profesor] ON
GO
INSERT INTO [dbo].[Profesor] ([LegajoProfesor], [Nombre], [Especialidad], [FranjaHorariaDisponible], [DVH]) VALUES (1, 'Carlos Gomez', 'Recreativa', '08:00-12:00', 4964732051505079940)
INSERT INTO [dbo].[Profesor] ([LegajoProfesor], [Nombre], [Especialidad], [FranjaHorariaDisponible], [DVH]) VALUES (2, 'Laura Perez', 'Competitiva', '14:00-18:00', 3673797584765896152)
INSERT INTO [dbo].[Profesor] ([LegajoProfesor], [Nombre], [Especialidad], [FranjaHorariaDisponible], [DVH]) VALUES (3, 'Martin Diaz', 'Recreativa', '09:00-13:00', 6996247005730020526)
INSERT INTO [dbo].[Profesor] ([LegajoProfesor], [Nombre], [Especialidad], [FranjaHorariaDisponible], [DVH]) VALUES (4, 'Sofia Ramirez', 'Competitiva', '16:00-20:00', 8880308755446237198)
GO
SET IDENTITY_INSERT [dbo].[Profesor] OFF
GO
