USE [is--servicios]
GO

/****** Tabla InscripcionClase para el caso de uso "Registrar nadador a clase" ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InscripcionClase](
	[NumeroInscripcionClase] [int] IDENTITY(1,1) NOT NULL,
	[DNINadador] [varchar](50) NOT NULL,
	[CodigoClase] [int] NOT NULL,
	[FechaInscripcion] [date] NOT NULL,
	[Estado] [varchar](50) NOT NULL,
	[DVH] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[NumeroInscripcionClase] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[InscripcionClase] WITH CHECK ADD CONSTRAINT [FK_InscripcionClase_Nadador] FOREIGN KEY([DNINadador])
REFERENCES [dbo].[Nadador] ([DNI])
GO
ALTER TABLE [dbo].[InscripcionClase] CHECK CONSTRAINT [FK_InscripcionClase_Nadador]
GO
ALTER TABLE [dbo].[InscripcionClase] WITH CHECK ADD CONSTRAINT [FK_InscripcionClase_Clase] FOREIGN KEY([CodigoClase])
REFERENCES [dbo].[Clase] ([CodigoClase])
GO
ALTER TABLE [dbo].[InscripcionClase] CHECK CONSTRAINT [FK_InscripcionClase_Clase]
GO
ALTER TABLE [dbo].[InscripcionClase] ADD DEFAULT ('Activo') FOR [Estado]
GO