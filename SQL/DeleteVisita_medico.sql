--***********************************************************
--DELETE Stored Procedure for Visita_medico table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'Visita_medicoDelete'    and sys.objects.type = 'P') 
DROP PROCEDURE [Visita_medicoDelete] 
GO

CREATE PROCEDURE [Visita_medicoDelete]
      @OldCod_visita int
     ,@OldFecha date
     ,@OldHora varchar(50)
     ,@OldDiagnostico varchar(500)
     ,@OldTratamiento varchar(500)
     ,@OldID_hospitales_servicios int
     ,@Oldid_historia int
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

DELETE FROM [Visita_medico]
WHERE
     [Cod_visita] = @OldCod_visita
AND ((@OldFecha IS NULL AND [Fecha] IS NULL) OR [Fecha] = @OldFecha)
AND ((@OldHora IS NULL AND [Hora] IS NULL) OR [Hora] = @OldHora)
AND ((@OldDiagnostico IS NULL AND [Diagnostico] IS NULL) OR [Diagnostico] = @OldDiagnostico)
AND ((@OldTratamiento IS NULL AND [Tratamiento] IS NULL) OR [Tratamiento] = @OldTratamiento)
AND ((@OldID_hospitales_servicios IS NULL AND [ID_hospitales_servicios] IS NULL) OR [ID_hospitales_servicios] = @OldID_hospitales_servicios)
AND ((@Oldid_historia IS NULL AND [id_historia] IS NULL) OR [id_historia] = @Oldid_historia)

IF @@ROWCOUNT = 0
BEGIN
     ROLLBACK TRANSACTION
     SET @ReturnValue = 0
     RETURN @ReturnValue
END
ELSE
BEGIN
     COMMIT TRANSACTION
     SET @ReturnValue = 1
     RETURN @ReturnValue
END

END TRY

BEGIN CATCH
     DECLARE @Error_Message varchar(150)
     SET @Error_Message = ERROR_NUMBER() + ' ' + ERROR_MESSAGE()
     ROLLBACK TRANSACTION
     RAISERROR(@Error_Message,16,1)
      SET @ReturnValue = -1
      RETURN @ReturnValue
END CATCH

END
GO

GRANT EXECUTE ON [Visita_medicoDelete] TO [Public]
GO
 
