--***********************************************************
--INSERT Stored Procedure for ingresos table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ingresosInsert'    and sys.objects.type = 'P') 
DROP PROCEDURE [ingresosInsert] 
GO

CREATE PROCEDURE [ingresosInsert] 
      @Comentario nvarchar(50)
     ,@Fecha_ingreso date
     ,@Fecha_salida date
     ,@id_cama int
     ,@id_historia int
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

INSERT [ingresos]
     (
      [Comentario]
     ,[Fecha_ingreso]
     ,[Fecha_salida]
     ,[id_cama]
     ,[id_historia]
     )
VALUES
     (
      @Comentario
     ,@Fecha_ingreso
     ,@Fecha_salida
     ,@id_cama
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

GRANT EXECUTE ON [ingresosInsert] TO [Public]
GO
 
