--***********************************************************
--INSERT Stored Procedure for camas table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'camasInsert'    and sys.objects.type = 'P') 
DROP PROCEDURE [camasInsert] 
GO

CREATE PROCEDURE [camasInsert] 
      @Num_cama int
     ,@Estado varchar(1)
     ,@ID_hospitales_servicios int
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

INSERT [camas]
     (
      [Num_cama]
     ,[Estado]
     ,[ID_hospitales_servicios]
     )
VALUES
     (
      @Num_cama
     ,@Estado
     ,@ID_hospitales_servicios
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

GRANT EXECUTE ON [camasInsert] TO [Public]
GO
 
