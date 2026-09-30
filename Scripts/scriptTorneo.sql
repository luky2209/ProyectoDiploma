USE [is--servicios]
GO

/****** Tabla Prueba: catalogo de estilos y distancias que se pueden marcar en un torneo ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prueba](
	[IdPrueba] [int] IDENTITY(1,1) NOT NULL,
	[Estilo] [varchar](20) NOT NULL,
	[Distancia] [int] NOT NULL,
	[DVH] [bigint] NULL,
PRIMARY KEY CLUSTERED
(
	[IdPrueba] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Las 20 pruebas del catalogo: 4 estilos x 5 distancias ******/
INSERT INTO [dbo].[Prueba] ([Estilo], [Distancia]) VALUES (N'Libre', 50)
INSERT INTO [dbo].[Prueba] ([Estilo], [Distancia]) VALUES (N'Libre', 100)
INSERT INTO [dbo].[Prueba] ([Estilo], [Distancia]) VALUES (N'Libre', 200)
INSERT INTO [dbo].[Prueba] ([Estilo], [Distancia]) VALUES (N'Libre', 400)
INSERT INTO [dbo].[Prueba] ([Estilo], [Distancia]) VALUES (N'Libre', 800)
INSERT INTO [dbo].[Prueba] ([Estilo], [Distancia]) VALUES (N'Mariposa', 50)
INSERT INTO [dbo].[Prueba] ([Estilo], [Distancia]) VALUES (N'Mariposa', 100)
INSERT INTO [dbo].[Prueba] ([Estilo], [Distancia]) VALUES (N'Mariposa', 200)
INSERT INTO [dbo].[Prueba] ([Estilo], [Distancia]) VALUES (N'Mariposa', 400)
INSERT INTO [dbo].[Prueba] ([Estilo], [Distancia]) VALUES (N'Mariposa', 800)
INSERT INTO [dbo].[Prueba] ([Estilo], [Distancia]) VALUES (N'Espalda', 50)
INSERT INTO [dbo].[Prueba] ([Estilo], [Distancia]) VALUES (N'Espalda', 100)
INSERT INTO [dbo].[Prueba] ([Estilo], [Distancia]) VALUES (N'Espalda', 200)
INSERT INTO [dbo].[Prueba] ([Estilo], [Distancia]) VALUES (N'Espalda', 400)
INSERT INTO [dbo].[Prueba] ([Estilo], [Distancia]) VALUES (N'Espalda', 800)
INSERT INTO [dbo].[Prueba] ([Estilo], [Distancia]) VALUES (N'Pecho', 50)
INSERT INTO [dbo].[Prueba] ([Estilo], [Distancia]) VALUES (N'Pecho', 100)
INSERT INTO [dbo].[Prueba] ([Estilo], [Distancia]) VALUES (N'Pecho', 200)
INSERT INTO [dbo].[Prueba] ([Estilo], [Distancia]) VALUES (N'Pecho', 400)
INSERT INTO [dbo].[Prueba] ([Estilo], [Distancia]) VALUES (N'Pecho', 800)
GO

/****** Tabla Torneo: la competencia que organiza el entrenador ******/
CREATE TABLE [dbo].[Torneo](
	[CodigoTorneo] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Fecha] [date] NOT NULL,
	[Sede] [varchar](100) NOT NULL,
	[Arancel] [decimal](10,2) NOT NULL,
	[Categorias] [varchar](200) NOT NULL,
	[Estado] [varchar](20) NOT NULL,
	[DVH] [bigint] NULL,
PRIMARY KEY CLUSTERED
(
	[CodigoTorneo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Tabla TorneoPrueba: que pruebas del catalogo tiene habilitadas cada torneo ******/
CREATE TABLE [dbo].[TorneoPrueba](
	[IdTorneoPrueba] [int] IDENTITY(1,1) NOT NULL,
	[CodigoTorneo] [int] NOT NULL,
	[IdPrueba] [int] NOT NULL,
	[DVH] [bigint] NULL,
PRIMARY KEY CLUSTERED
(
	[IdTorneoPrueba] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TorneoPrueba] ADD CONSTRAINT [UQ_TorneoPrueba] UNIQUE
(
	[CodigoTorneo] ASC,
	[IdPrueba] ASC
)
GO
ALTER TABLE [dbo].[TorneoPrueba]  WITH CHECK ADD  CONSTRAINT [FK_TorneoPrueba_Torneo] FOREIGN KEY([CodigoTorneo])
REFERENCES [dbo].[Torneo] ([CodigoTorneo])
GO
ALTER TABLE [dbo].[TorneoPrueba]  WITH CHECK ADD  CONSTRAINT [FK_TorneoPrueba_Prueba] FOREIGN KEY([IdPrueba])
REFERENCES [dbo].[Prueba] ([IdPrueba])
GO

/****** Tabla Inscripcion: une un nadador del padron con una prueba de un torneo ******/
CREATE TABLE [dbo].[Inscripcion](
	[NumeroInscripcion] [int] IDENTITY(1,1) NOT NULL,
	[DNINadador] [varchar](50) NOT NULL,
	[CodigoTorneo] [int] NOT NULL,
	[IdPrueba] [int] NOT NULL,
	[Categoria] [varchar](50) NOT NULL,
	[FechaInscripcion] [date] NOT NULL,
	[Estado] [varchar](20) NOT NULL,
	[DVH] [bigint] NULL,
PRIMARY KEY CLUSTERED
(
	[NumeroInscripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Inscripcion]  WITH CHECK ADD  CONSTRAINT [FK_Inscripcion_Nadador] FOREIGN KEY([DNINadador])
REFERENCES [dbo].[Nadador] ([DNI])
GO
ALTER TABLE [dbo].[Inscripcion]  WITH CHECK ADD  CONSTRAINT [FK_Inscripcion_Torneo] FOREIGN KEY([CodigoTorneo])
REFERENCES [dbo].[Torneo] ([CodigoTorneo])
GO
ALTER TABLE [dbo].[Inscripcion]  WITH CHECK ADD  CONSTRAINT [FK_Inscripcion_Prueba] FOREIGN KEY([IdPrueba])
REFERENCES [dbo].[Prueba] ([IdPrueba])
GO
