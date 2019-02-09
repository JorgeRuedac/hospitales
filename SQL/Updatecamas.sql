--***********************************************************
--UPDATE Stored Procedure for camas table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'camasUpdate'    and sys.objects.type = 'P') 
DROP PROCEDURE [camasUpdate]
GO

CREATE PROCEDURE [camasUpdate] 
      @NewNum_cama int
     ,@NewEstado varchar(1)
     ,@NewID_hospitales_servicios int
     ,@Oldid_cama int
     ,@OldNum_cama int
     ,@OldEstado varchar(1)
     ,@OldID_hospitales_servicios int
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

UPDATE [camas]
SET
      [Num_cama] = @NewNum_cama
     ,[Estado] = @NewEstado
     ,[ID_hospitales_servicios] = @NewID_hospitales_servicios
WHERE
     [id_cama] = @Oldid_cama
AND ((@OldNum_cama IS NULL AND [Num_cama] IS NULL) OR [Num_cama] = @OldNum_cama)
AND ((@OldEstado IS NULL AND [Estado] IS NULL) OR [Estado] = @OldEstado)
AND ((@OldID_hospitales_servicios IS NULL AND [ID_hospitales_servicios] IS NULL) OR [ID_hospitales_servicios] = @OldID_hospitales_servicios)

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

GRANT EXECUTE ON [camasUpdate] TO [Public]
GO
 
