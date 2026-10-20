USE [is--servicios]
GO

/****** Se agregan a Clase el profesor asignado y la fecha de asignacion ******/
/****** Corresponde al caso de uso "Asignar profesor a clase" ******/
ALTER TABLE [dbo].[Clase] ADD [LegajoProfesor] [int] NULL
GO
ALTER TABLE [dbo].[Clase] ADD [FechaAsignacion] [datetime] NULL
GO
ALTER TABLE [dbo].[Clase] ADD CONSTRAINT [FK_Clase_Profesor] FOREIGN KEY ([LegajoProfesor])
REFERENCES [dbo].[Profesor] ([LegajoProfesor])
GO
