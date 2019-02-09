--***********************************************************
--INSERT Stored Procedure for historia_clinica table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'historia_clinicaInsert'    and sys.objects.type = 'P') 
DROP PROCEDURE [historia_clinicaInsert] 
GO

CREATE PROCEDURE [historia_clinicaInsert] 
      @Cedula nvarchar(50)
     ,@Apellido nvarchar(50)
     ,@Nombre nvarchar(50)
     ,@Fecha_nacim date
     ,@Num_seguridad_social nvarchar(50)
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

INSERT [historia_clinica]
     (
      [Cedula]
     ,[Apellido]
     ,[Nombre]
     ,[Fecha_nacim]
     ,[Num_seguridad_social]
     )
VALUES
     (
      @Cedula
     ,@Apellido
     ,@Nombre
     ,@Fecha_nacim
     ,@Num_seguridad_social
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

GRANT EXECUTE ON [historia_clinicaInsert] TO [Public]
GO
 
