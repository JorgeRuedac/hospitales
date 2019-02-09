--***********************************************************
--UPDATE Stored Procedure for medico table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'medicoUpdate'    and sys.objects.type = 'P') 
DROP PROCEDURE [medicoUpdate]
GO

CREATE PROCEDURE [medicoUpdate] 
      @NewCedula nvarchar(50)
     ,@NewApellido_medico nvarchar(50)
     ,@NewFecha_nacimien date
     ,@OldCod_medico int
     ,@OldCedula nvarchar(50)
     ,@OldApellido_medico nvarchar(50)
     ,@OldFecha_nacimien date
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

UPDATE [medico]
SET
      [Cedula] = @NewCedula
     ,[Apellido_medico] = @NewApellido_medico
     ,[Fecha_nacimien] = @NewFecha_nacimien
WHERE
     [Cod_medico] = @OldCod_medico
AND ((@OldCedula IS NULL AND [Cedula] IS NULL) OR [Cedula] = @OldCedula)
AND ((@OldApellido_medico IS NULL AND [Apellido_medico] IS NULL) OR [Apellido_medico] = @OldApellido_medico)
AND ((@OldFecha_nacimien IS NULL AND [Fecha_nacimien] IS NULL) OR [Fecha_nacimien] = @OldFecha_nacimien)

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

GRANT EXECUTE ON [medicoUpdate] TO [Public]
GO
 
