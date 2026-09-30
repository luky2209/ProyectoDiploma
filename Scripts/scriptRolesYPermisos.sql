USE [is--servicios]
GO

/****** Patentes nuevas para las pantallas de dominio ******/
/****** Se insertan con Id fijo y con el DVH calculado con el mismo algoritmo de la aplicacion ******/
SET IDENTITY_INSERT [dbo].[Patente] ON

INSERT INTO [dbo].[Patente] ([Id], [Nombre], [DVH])
SELECT v.Id, v.Nombre, ABS(CAST(
           SUBSTRING(x.hash, 8, 1) + SUBSTRING(x.hash, 7, 1) + SUBSTRING(x.hash, 6, 1) + SUBSTRING(x.hash, 5, 1) +
           SUBSTRING(x.hash, 4, 1) + SUBSTRING(x.hash, 3, 1) + SUBSTRING(x.hash, 2, 1) + SUBSTRING(x.hash, 1, 1)
           AS bigint))
FROM (VALUES
    (10, N'Gestion Nadador'),
    (11, N'Gestion Torneo'),
    (12, N'Inscripcion Torneo'),
    (13, N'Cargar Resultados'),
    (14, N'Consultar Resultados'),
    (15, N'Cobrar Inscripcion')
) AS v(Id, Nombre)
CROSS APPLY (SELECT HASHBYTES('SHA2_256', CAST(v.Id AS varchar(20)) + CAST(v.Nombre AS varchar(50))) AS hash) x
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Patente] p WHERE p.Nombre = v.Nombre);

SET IDENTITY_INSERT [dbo].[Patente] OFF
GO

DBCC CHECKIDENT ('dbo.Patente', RESEED);
GO

/****** Roles nuevos: Entrenador y Administradora ******/
SET IDENTITY_INSERT [dbo].[Rol] ON

INSERT INTO [dbo].[Rol] ([Id], [Nombre], [DVH])
SELECT v.Id, v.Nombre, ABS(CAST(
           SUBSTRING(x.hash, 8, 1) + SUBSTRING(x.hash, 7, 1) + SUBSTRING(x.hash, 6, 1) + SUBSTRING(x.hash, 5, 1) +
           SUBSTRING(x.hash, 4, 1) + SUBSTRING(x.hash, 3, 1) + SUBSTRING(x.hash, 2, 1) + SUBSTRING(x.hash, 1, 1)
           AS bigint))
FROM (VALUES
    (4, N'Entrenador'),
    (5, N'Administradora')
) AS v(Id, Nombre)
CROSS APPLY (SELECT HASHBYTES('SHA2_256', CAST(v.Id AS varchar(20)) + CAST(v.Nombre AS varchar(50))) AS hash) x
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Rol] r WHERE r.Nombre = v.Nombre);

SET IDENTITY_INSERT [dbo].[Rol] OFF
GO

DBCC CHECKIDENT ('dbo.Rol', RESEED);
GO

/****** Los dos roles reciben la familia Usuario (cambiar clave, cambiar idioma, cerrar e iniciar sesion) ******/
INSERT INTO [dbo].[Rol_Familia] ([IdRol], [IdFamilia])
SELECT r.Id, 4
FROM [dbo].[Rol] r
WHERE r.Nombre IN (N'Entrenador', N'Administradora')
  AND NOT EXISTS (SELECT 1 FROM [dbo].[Rol_Familia] rf WHERE rf.IdRol = r.Id AND rf.IdFamilia = 4);
GO

/****** El Entrenador registra nadadores, inscribe, carga resultados y consulta el ranking ******/
INSERT INTO [dbo].[Rol_Patente] ([IdRol], [IdPatente])
SELECT r.Id, p.Id
FROM [dbo].[Rol] r
CROSS JOIN [dbo].[Patente] p
WHERE r.Nombre = N'Entrenador'
  AND p.Nombre IN (N'Gestion Nadador', N'Inscripcion Torneo', N'Cargar Resultados', N'Consultar Resultados')
  AND NOT EXISTS (SELECT 1 FROM [dbo].[Rol_Patente] rp WHERE rp.IdRol = r.Id AND rp.IdPatente = p.Id);
GO

/****** La Administradora gestiona torneos, inscribe, cobra y consulta el ranking ******/
INSERT INTO [dbo].[Rol_Patente] ([IdRol], [IdPatente])
SELECT r.Id, p.Id
FROM [dbo].[Rol] r
CROSS JOIN [dbo].[Patente] p
WHERE r.Nombre = N'Administradora'
  AND p.Nombre IN (N'Gestion Torneo', N'Inscripcion Torneo', N'Consultar Resultados', N'Cobrar Inscripcion')
  AND NOT EXISTS (SELECT 1 FROM [dbo].[Rol_Patente] rp WHERE rp.IdRol = r.Id AND rp.IdPatente = p.Id);
GO

/****** El rol administrador del sistema (Id 1) recibe todas las patentes nuevas ******/
INSERT INTO [dbo].[Rol_Patente] ([IdRol], [IdPatente])
SELECT 1, p.Id
FROM [dbo].[Patente] p
WHERE p.Nombre IN (N'Gestion Nadador', N'Gestion Torneo', N'Inscripcion Torneo', N'Cargar Resultados', N'Consultar Resultados', N'Cobrar Inscripcion')
  AND NOT EXISTS (SELECT 1 FROM [dbo].[Rol_Patente] rp WHERE rp.IdRol = 1 AND rp.IdPatente = p.Id);
GO

/****** Se rehace el DVH de cada patente y de cada rol ******/
UPDATE p
SET DVH = ABS(CAST(
            SUBSTRING(x.hash, 8, 1) + SUBSTRING(x.hash, 7, 1) + SUBSTRING(x.hash, 6, 1) + SUBSTRING(x.hash, 5, 1) +
            SUBSTRING(x.hash, 4, 1) + SUBSTRING(x.hash, 3, 1) + SUBSTRING(x.hash, 2, 1) + SUBSTRING(x.hash, 1, 1)
            AS bigint))
FROM [dbo].[Patente] p
CROSS APPLY (SELECT HASHBYTES('SHA2_256', CAST(p.Id AS varchar(20)) + p.Nombre) AS hash) x;
GO

UPDATE r
SET DVH = ABS(CAST(
            SUBSTRING(x.hash, 8, 1) + SUBSTRING(x.hash, 7, 1) + SUBSTRING(x.hash, 6, 1) + SUBSTRING(x.hash, 5, 1) +
            SUBSTRING(x.hash, 4, 1) + SUBSTRING(x.hash, 3, 1) + SUBSTRING(x.hash, 2, 1) + SUBSTRING(x.hash, 1, 1)
            AS bigint))
FROM [dbo].[Rol] r
CROSS APPLY (SELECT HASHBYTES('SHA2_256', CAST(r.Id AS varchar(20)) + r.Nombre) AS hash) x;
GO

/****** Se rehace el DVV y el control de las tablas Patente y Rol ******/
/****** El DVV es la suma con desborde de 64 bits, igual que en la aplicacion ******/
DECLARE @dos64 decimal(38,0) = 18446744073709551616;
DECLARE @mitad decimal(38,0) = 9223372036854775808;

DECLARE @sumaPatente decimal(38,0) = (SELECT SUM(CAST(DVH AS decimal(38,0))) FROM [dbo].[Patente]);
DECLARE @restoPatente decimal(38,0) = @sumaPatente % @dos64;
IF @restoPatente >= @mitad SET @restoPatente = @restoPatente - @dos64;
DECLARE @dvvPatente bigint = CAST(@restoPatente AS bigint);
DECLARE @hPatente varbinary(32) = HASHBYTES('SHA2_256', CAST('Patente' + CAST(@dvvPatente AS varchar(20)) AS varchar(50)));
DECLARE @controlPatente bigint = ABS(CAST(
    SUBSTRING(@hPatente, 8, 1) + SUBSTRING(@hPatente, 7, 1) + SUBSTRING(@hPatente, 6, 1) + SUBSTRING(@hPatente, 5, 1) +
    SUBSTRING(@hPatente, 4, 1) + SUBSTRING(@hPatente, 3, 1) + SUBSTRING(@hPatente, 2, 1) + SUBSTRING(@hPatente, 1, 1)
    AS bigint));

IF EXISTS (SELECT 1 FROM [dbo].[DigitoVerificador] WHERE NombreTabla = 'Patente')
    UPDATE [dbo].[DigitoVerificador] SET DVV = @dvvPatente, DVH = @controlPatente WHERE NombreTabla = 'Patente';
ELSE
    INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV], [DVH]) VALUES ('Patente', @dvvPatente, @controlPatente);

DECLARE @sumaRol decimal(38,0) = (SELECT SUM(CAST(DVH AS decimal(38,0))) FROM [dbo].[Rol]);
DECLARE @restoRol decimal(38,0) = @sumaRol % @dos64;
IF @restoRol >= @mitad SET @restoRol = @restoRol - @dos64;
DECLARE @dvvRol bigint = CAST(@restoRol AS bigint);
DECLARE @hRol varbinary(32) = HASHBYTES('SHA2_256', CAST('Rol' + CAST(@dvvRol AS varchar(20)) AS varchar(50)));
DECLARE @controlRol bigint = ABS(CAST(
    SUBSTRING(@hRol, 8, 1) + SUBSTRING(@hRol, 7, 1) + SUBSTRING(@hRol, 6, 1) + SUBSTRING(@hRol, 5, 1) +
    SUBSTRING(@hRol, 4, 1) + SUBSTRING(@hRol, 3, 1) + SUBSTRING(@hRol, 2, 1) + SUBSTRING(@hRol, 1, 1)
    AS bigint));

IF EXISTS (SELECT 1 FROM [dbo].[DigitoVerificador] WHERE NombreTabla = 'Rol')
    UPDATE [dbo].[DigitoVerificador] SET DVV = @dvvRol, DVH = @controlRol WHERE NombreTabla = 'Rol';
ELSE
    INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV], [DVH]) VALUES ('Rol', @dvvRol, @controlRol);
GO
