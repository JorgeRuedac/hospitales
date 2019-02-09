--***********************************************************
--SELECT Stored Procedure for medico table
--***********************************************************
GO

USE hospital
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'medicoSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [medicoSelect] 
GO

CREATE PROCEDURE [medicoSelect] 
      @Cod_medico int
AS 
BEGIN 
SELECT 
      [Cod_medico]
     ,[Cedula]
     ,[Apellido_medico]
     ,[Fecha_nacimien]
FROM
     [medico]
WHERE
    [Cod_medico] = @Cod_medico
END 
GO

GRANT EXECUTE ON [medicoSelect] TO [Public]
GO

 
