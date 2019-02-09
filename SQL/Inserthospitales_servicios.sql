--***********************************************************
--INSERT Stored Procedure for hospitales_servicios table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'hospitales_serviciosInsert'    and sys.objects.type = 'P') 
DROP PROCEDURE [hospitales_serviciosInsert] 
GO

CREATE PROCEDURE [hospitales_serviciosInsert] 
      @Cod_hospital int
     ,@Id_servicio int
     ,@CodigoRefer varchar(50)
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

INSERT [hospitales_servicios]
     (
      [Cod_hospital]
     ,[Id_servicio]
     ,[CodigoRefer]
     )
VALUES
     (
      @Cod_hospital
     ,@Id_servicio
     ,@CodigoRefer
     )

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

GRANT EXECUTE ON [hospitales_serviciosInsert] TO [Public]
GO
 
