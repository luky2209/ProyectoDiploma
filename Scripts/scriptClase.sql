USE [is--servicios]
GO

/****** Tabla Clase para el caso de uso "Registrar Clase" ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clase](
	[CodigoClase] [int] IDENTITY(1,1) NOT NULL,
	[TipoClase] [varchar](50) NOT NULL,
	[NivelDificultad] [varchar](50) NOT NULL,
	[Dias] [varchar](100) NOT NULL,
	[HorarioInicio] [time](0) NOT NULL,
	[HorarioFin] [time](0) NOT NULL,
	[EdadMinima] [int] NOT NULL,
	[EdadMaxima] [int] NOT NULL,
	[CupoMaximo] [int] NOT NULL,
	[DVH] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[CodigoClase] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
