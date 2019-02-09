--***********************************************************
--DELETE Stored Procedure for hospitales table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'hospitalesDelete'    and sys.objects.type = 'P') 
DROP PROCEDURE [hospitalesDelete] 
GO

CREATE PROCEDURE [hospitalesDelete]
      @OldCod_hospital int
     ,@OldNombre nvarchar(50)
     ,@OldCiudad nvarchar(50)
     ,@OldTlefono nvarchar(50)
     ,@OldCod_medico int
     ,@ReturnValue int OUTPUT
AS
BEGIN

SET NOCOUNT ON

BEGIN TRANSACTION 

BEGIN TRY 

DELETE FROM [hospitales]
WHERE
     [Cod_hospital] = @OldCod_hospital
AND ((@OldNombre IS NULL AND [Nombre] IS NULL) OR [Nombre] = @OldNombre)
AND ((@OldCiudad IS NULL AND [Ciudad] IS NULL) OR [Ciudad] = @OldCiudad)
AND ((@OldTlefono IS NULL AND [Tlefono] IS NULL) OR [Tlefono] = @OldTlefono)
AND ((@OldCod_medico IS NULL AND [Cod_medico] IS NULL) OR [Cod_medico] = @OldCod_medico)

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

GRANT EXECUTE ON [hospitalesDelete] TO [Public]
GO
 
