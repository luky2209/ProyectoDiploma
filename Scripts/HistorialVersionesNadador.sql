USE [is--servicios]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('dbo.NadadorHistorial', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[NadadorHistorial](
        [IdHistorial]       BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [DNI]               VARCHAR(50) NOT NULL,
        [Nombre]            VARCHAR(50) NOT NULL,
        [Apellido]          VARCHAR(50) NOT NULL,
        [FechaNacimiento]   DATE NOT NULL,
        [Edad]              INT NOT NULL,
        [Categoria]         VARCHAR(50) NOT NULL,
        [CertificadoMedico] BIT NOT NULL,
        [DVH]               BIGINT NULL,
        [VersionNumero]     INT NOT NULL,
        [FechaHora]         DATETIME2(3) NOT NULL CONSTRAINT DF_NadadorHistorial_FechaHora DEFAULT (SYSDATETIME()),
        [TipoOperacion]     VARCHAR(10) NOT NULL,
        [UsuarioDNI]        VARCHAR(50) NULL,
        [Activo]            BIT NOT NULL CONSTRAINT DF_NadadorHistorial_Activo DEFAULT (0),
        [OrigenVersionId]   BIGINT NULL,
        [HostName]          NVARCHAR(128) NULL,
        [AppName]           NVARCHAR(128) NULL
    )
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_NadadorHistorial_DNI_FechaHora' AND object_id = OBJECT_ID('dbo.NadadorHistorial'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_NadadorHistorial_DNI_FechaHora ON [dbo].[NadadorHistorial]([DNI], [FechaHora] DESC)
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_NadadorHistorial_DNI_Activo' AND object_id = OBJECT_ID('dbo.NadadorHistorial'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_NadadorHistorial_DNI_Activo ON [dbo].[NadadorHistorial]([DNI], [Activo])
END
GO

IF OBJECT_ID('dbo.trg_Nadador_Insert', 'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_Nadador_Insert
GO

CREATE TRIGGER [dbo].[trg_Nadador_Insert]
ON [dbo].[Nadador]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    IF CAST(SESSION_CONTEXT(N'RESTORE_IN_PROGRESS') AS INT) = 1
        RETURN;

    DECLARE @UserDNI VARCHAR(50) = CAST(SESSION_CONTEXT(N'USER_DNI') AS VARCHAR(50));

    INSERT INTO dbo.NadadorHistorial
        (DNI, Nombre, Apellido, FechaNacimiento, Edad, Categoria, CertificadoMedico, DVH, VersionNumero, FechaHora, TipoOperacion, UsuarioDNI, Activo, OrigenVersionId, HostName, AppName)
    SELECT 
        i.DNI,
        i.Nombre,
        i.Apellido,
        i.FechaNacimiento,
        i.Edad,
        i.Categoria,
        i.CertificadoMedico,
        i.DVH,
        ISNULL((SELECT MAX(VersionNumero) FROM dbo.NadadorHistorial h WHERE h.DNI = i.DNI), 0) + 1,
        SYSDATETIME(),
        'INSERT',
        @UserDNI,
        1,
        NULL,
        HOST_NAME(),
        APP_NAME()
    FROM INSERTED i;

    UPDATE h SET Activo = 0
    FROM dbo.NadadorHistorial h
    INNER JOIN INSERTED i ON h.DNI = i.DNI
    WHERE h.Activo = 1
      AND h.IdHistorial <> (SELECT MAX(h2.IdHistorial) FROM dbo.NadadorHistorial h2 WHERE h2.DNI = i.DNI);
END
GO

IF OBJECT_ID('dbo.trg_Nadador_Update', 'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_Nadador_Update
GO

CREATE TRIGGER [dbo].[trg_Nadador_Update]
ON [dbo].[Nadador]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF CAST(SESSION_CONTEXT(N'RESTORE_IN_PROGRESS') AS INT) = 1
        RETURN;

    DECLARE @UserDNI VARCHAR(50) = CAST(SESSION_CONTEXT(N'USER_DNI') AS VARCHAR(50));

    INSERT INTO dbo.NadadorHistorial
        (DNI, Nombre, Apellido, FechaNacimiento, Edad, Categoria, CertificadoMedico, DVH, VersionNumero, FechaHora, TipoOperacion, UsuarioDNI, Activo, OrigenVersionId, HostName, AppName)
    SELECT 
        i.DNI,
        i.Nombre,
        i.Apellido,
        i.FechaNacimiento,
        i.Edad,
        i.Categoria,
        i.CertificadoMedico,
        i.DVH,
        ISNULL((SELECT MAX(VersionNumero) FROM dbo.NadadorHistorial h WHERE h.DNI = i.DNI), 0) + 1,
        SYSDATETIME(),
        'UPDATE',
        @UserDNI,
        1,
        NULL,
        HOST_NAME(),
        APP_NAME()
    FROM INSERTED i;

    UPDATE h SET Activo = 0
    FROM dbo.NadadorHistorial h
    INNER JOIN INSERTED i ON h.DNI = i.DNI
    WHERE h.Activo = 1
      AND h.IdHistorial <> (SELECT MAX(h2.IdHistorial) FROM dbo.NadadorHistorial h2 WHERE h2.DNI = i.DNI);
END
GO

IF OBJECT_ID('dbo.trg_Nadador_Delete', 'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_Nadador_Delete
GO

CREATE TRIGGER [dbo].[trg_Nadador_Delete]
ON [dbo].[Nadador]
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    IF CAST(SESSION_CONTEXT(N'RESTORE_IN_PROGRESS') AS INT) = 1
        RETURN;

    DECLARE @UserDNI VARCHAR(50) = CAST(SESSION_CONTEXT(N'USER_DNI') AS VARCHAR(50));

    INSERT INTO dbo.NadadorHistorial
        (DNI, Nombre, Apellido, FechaNacimiento, Edad, Categoria, CertificadoMedico, DVH, VersionNumero, FechaHora, TipoOperacion, UsuarioDNI, Activo, OrigenVersionId, HostName, AppName)
    SELECT 
        d.DNI,
        d.Nombre,
        d.Apellido,
        d.FechaNacimiento,
        d.Edad,
        d.Categoria,
        d.CertificadoMedico,
        d.DVH,
        ISNULL((SELECT MAX(VersionNumero) FROM dbo.NadadorHistorial h WHERE h.DNI = d.DNI), 0) + 1,
        SYSDATETIME(),
        'DELETE',
        @UserDNI,
        0,
        NULL,
        HOST_NAME(),
        APP_NAME()
    FROM DELETED d;
END
GO

IF OBJECT_ID('dbo.sp_RestaurarVersionNadador', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_RestaurarVersionNadador
GO

CREATE PROCEDURE [dbo].[sp_RestaurarVersionNadador]
    @IdHistorial BIGINT,
    @UsuarioDNI  VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @DNI VARCHAR(50);
        SELECT TOP 1 @DNI = DNI FROM dbo.NadadorHistorial WITH (UPDLOCK, HOLDLOCK) WHERE IdHistorial = @IdHistorial;

        IF @DNI IS NULL
        BEGIN
            ROLLBACK TRANSACTION;
            RAISERROR('Version no encontrada', 16, 1);
            RETURN;
        END

        SELECT TOP 1 * FROM dbo.Nadador WITH (UPDLOCK, HOLDLOCK) WHERE DNI = @DNI;

        DECLARE @Nombre VARCHAR(50),
                @Apellido VARCHAR(50),
                @FechaNacimiento DATE,
                @Edad INT,
                @Categoria VARCHAR(50),
                @CertificadoMedico BIT,
                @DVH BIGINT;

        SELECT 
            @Nombre = Nombre,
            @Apellido = Apellido,
            @FechaNacimiento = FechaNacimiento,
            @Edad = Edad,
            @Categoria = Categoria,
            @CertificadoMedico = CertificadoMedico,
            @DVH = DVH
        FROM dbo.NadadorHistorial
        WHERE IdHistorial = @IdHistorial;

        EXEC sp_set_session_context @key = N'RESTORE_IN_PROGRESS', @value = 1;
        EXEC sp_set_session_context @key = N'USER_DNI', @value = @UsuarioDNI;

        UPDATE dbo.Nadador
        SET Nombre = @Nombre,
            Apellido = @Apellido,
            FechaNacimiento = @FechaNacimiento,
            Edad = @Edad,
            Categoria = @Categoria,
            CertificadoMedico = @CertificadoMedico,
            DVH = @DVH
        WHERE DNI = @DNI;

        DECLARE @NuevoId BIGINT;

        INSERT INTO dbo.NadadorHistorial 
            (DNI, Nombre, Apellido, FechaNacimiento, Edad, Categoria, CertificadoMedico, DVH, VersionNumero, FechaHora, TipoOperacion, UsuarioDNI, Activo, OrigenVersionId, HostName, AppName)
        SELECT 
            @DNI,
            @Nombre,
            @Apellido,
            @FechaNacimiento,
            @Edad,
            @Categoria,
            @CertificadoMedico,
            @DVH,
            ISNULL((SELECT MAX(VersionNumero) FROM dbo.NadadorHistorial WHERE DNI = @DNI), 0) + 1,
            SYSDATETIME(),
            'RESTORE',
            @UsuarioDNI,
            1,
            @IdHistorial,
            HOST_NAME(),
            APP_NAME();

        SET @NuevoId = SCOPE_IDENTITY();

        UPDATE dbo.NadadorHistorial 
        SET Activo = 0 
        WHERE DNI = @DNI AND IdHistorial <> @NuevoId;

        EXEC sp_set_session_context @key = N'RESTORE_IN_PROGRESS', @value = NULL;
        EXEC sp_set_session_context @key = N'USER_DNI', @value = NULL;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        EXEC sp_set_session_context @key = N'RESTORE_IN_PROGRESS', @value = NULL;
        EXEC sp_set_session_context @key = N'USER_DNI', @value = NULL;

        THROW;
    END CATCH
END
GO
