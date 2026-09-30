USE [is--servicios]
GO

/****** Se le agrega al torneo la fecha en que el entrenador lo finalized ******/
ALTER TABLE [dbo].[Torneo] ADD [FechaCierre] [date] NULL
GO

/****** Tabla Resultado: el tiempo oficial de cada nadador en cada prueba del torneo ******/
CREATE TABLE [dbo].[Resultado](
	[IdResultado] [int] IDENTITY(1,1) NOT NULL,
	[NumeroInscripcion] [int] NOT NULL,
	[DNINadador] [varchar](50) NOT NULL,
	[IdPrueba] [int] NOT NULL,
	[Minutos] [int] NOT NULL,
	[Segundos] [int] NOT NULL,
	[Centesimas] [int] NOT NULL,
	[Descalificado] [bit] NOT NULL,
	[Posicion] [int] NULL,
	[Premio] [varchar](20) NULL,
	[DVH] [bigint] NULL,
PRIMARY KEY CLUSTERED
(
	[IdResultado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Un nadador descalificado no tiene puesto, por eso Posicion y Premio pueden quedar vacios ******/
ALTER TABLE [dbo].[Resultado]  WITH CHECK ADD  CONSTRAINT [CK_Resultado_Segundos] CHECK  (([Segundos] >= 0 AND [Segundos] <= 59))
GO
ALTER TABLE [dbo].[Resultado] CHECK CONSTRAINT [CK_Resultado_Segundos]
GO
ALTER TABLE [dbo].[Resultado]  WITH CHECK ADD  CONSTRAINT [CK_Resultado_Centesimas] CHECK  (([Centesimas] >= 0 AND [Centesimas] <= 99))
GO
ALTER TABLE [dbo].[Resultado] CHECK CONSTRAINT [CK_Resultado_Centesimas]
GO

/****** Cada resultado es de una inscripcion, y la inscripcion ya tiene el nadador y la prueba ******/
ALTER TABLE [dbo].[Resultado]  WITH CHECK ADD  CONSTRAINT [FK_Resultado_Inscripcion] FOREIGN KEY([NumeroInscripcion])
REFERENCES [dbo].[Inscripcion] ([NumeroInscripcion])
GO
ALTER TABLE [dbo].[Resultado]  WITH CHECK ADD  CONSTRAINT [FK_Resultado_Nadador] FOREIGN KEY([DNINadador])
REFERENCES [dbo].[Nadador] ([DNI])
GO
ALTER TABLE [dbo].[Resultado]  WITH CHECK ADD  CONSTRAINT [FK_Resultado_Prueba] FOREIGN KEY([IdPrueba])
REFERENCES [dbo].[Prueba] ([IdPrueba])
GO
