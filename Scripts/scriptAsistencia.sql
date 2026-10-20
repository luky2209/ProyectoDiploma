USE [is--servicios]
GO

/****** Tabla Asistencia para el caso de uso "Registrar asistencia" ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Asistencia](
	[NumeroAsistencia] [int] IDENTITY(1,1) NOT NULL,
	[CodigoClase] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[DNINadador] [varchar](50) NOT NULL,
	[Condicion] [varchar](50) NOT NULL,
	[DVH] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[NumeroAsistencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Asistencia] WITH CHECK ADD CONSTRAINT [FK_Asistencia_Clase] FOREIGN KEY([CodigoClase])
REFERENCES [dbo].[Clase] ([CodigoClase])
GO
ALTER TABLE [dbo].[Asistencia] CHECK CONSTRAINT [FK_Asistencia_Clase]
GO

ALTER TABLE [dbo].[Asistencia] WITH CHECK ADD CONSTRAINT [FK_Asistencia_Nadador] FOREIGN KEY([DNINadador])
REFERENCES [dbo].[Nadador] ([DNI])
GO
ALTER TABLE [dbo].[Asistencia] CHECK CONSTRAINT [FK_Asistencia_Nadador]
GO

ALTER TABLE [dbo].[Asistencia] ADD CONSTRAINT [UQ_Asistencia_Clase_Fecha_Nadador] UNIQUE NONCLUSTERED
(
	[CodigoClase] ASC,
	[Fecha] ASC,
	[DNINadador] ASC
)
GO