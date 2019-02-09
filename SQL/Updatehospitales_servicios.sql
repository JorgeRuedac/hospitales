--***********************************************************
--UPDATE Stored Procedure for hospitales_servicios table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'hospitales_serviciosUpdate'    and sys.objects.type = 'P') 
DROP PROCEDURE [hospitales_serviciosUpdate]
GO

CREATE PROCEDURE [hospitales_serviciosUpdate] 
      @NewCod_hospital int
     ,@NewId_servicio int
     ,@NewCodigoRefer varchar(50)
     ,@OldID_hospitales_servicios int
     ,@OldCod_hospital int
     ,@OldId_servicio int
     ,@OldCodigoRefer varchar(50)
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

UPDATE [hospitales_servicios]
SET
      [Cod_hospital] = @NewCod_hospital
     ,[Id_servicio] = @NewId_servicio
     ,[CodigoRefer] = @NewCodigoRefer
WHERE
     [ID_hospitales_servicios] = @OldID_hospitales_servicios
AND ((@OldCod_hospital IS NULL AND [Cod_hospital] IS NULL) OR [Cod_hospital] = @OldCod_hospital)
AND ((@OldId_servicio IS NULL AND [Id_servicio] IS NULL) OR [Id_servicio] = @OldId_servicio)
AND ((@OldCodigoRefer IS NULL AND [CodigoRefer] IS NULL) OR [CodigoRefer] = @OldCodigoRefer)

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

GRANT EXECUTE ON [hospitales_serviciosUpdate] TO [Public]
GO
 
