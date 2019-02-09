--***********************************************************
--INSERT Stored Procedure for Visita_medico table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'Visita_medicoInsert'    and sys.objects.type = 'P') 
DROP PROCEDURE [Visita_medicoInsert] 
GO

CREATE PROCEDURE [Visita_medicoInsert] 
      @Fecha date
     ,@Hora varchar(50)
     ,@Diagnostico varchar(500)
     ,@Tratamiento varchar(500)
     ,@ID_hospitales_servicios int
     ,@id_historia int
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

INSERT [Visita_medico]
     (
      [Fecha]
     ,[Hora]
     ,[Diagnostico]
     ,[Tratamiento]
     ,[ID_hospitales_servicios]
     ,[id_historia]
     )
VALUES
     (
      @Fecha
     ,@Hora
     ,@Diagnostico
     ,@Tratamiento
     ,@ID_hospitales_servicios
     ,@id_historia
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

GRANT EXECUTE ON [Visita_medicoInsert] TO [Public]
GO
 
