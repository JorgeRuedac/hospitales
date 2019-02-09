--***********************************************************
--INSERT Stored Procedure for hospitales table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'hospitalesInsert'    and sys.objects.type = 'P') 
DROP PROCEDURE [hospitalesInsert] 
GO

CREATE PROCEDURE [hospitalesInsert] 
      @Nombre nvarchar(50)
     ,@Ciudad nvarchar(50)
     ,@Tlefono nvarchar(50)
     ,@Cod_medico int
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

INSERT [hospitales]
     (
      [Nombre]
     ,[Ciudad]
     ,[Tlefono]
     ,[Cod_medico]
     )
VALUES
     (
      @Nombre
     ,@Ciudad
     ,@Tlefono
     ,@Cod_medico
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

GRANT EXECUTE ON [hospitalesInsert] TO [Public]
GO
 
