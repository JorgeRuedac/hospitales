--***********************************************************
--SELECT Stored Procedure for historia_clinica table
--***********************************************************
GO

USE hospital
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'historia_clinicaSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [historia_clinicaSelect] 
GO

CREATE PROCEDURE [historia_clinicaSelect] 
      @id_historia int
AS 
BEGIN 
SELECT 
      [id_historia]
     ,[Cedula]
     ,[Apellido]
     ,[Nombre]
     ,[Fecha_nacim]
     ,[Num_seguridad_social]
FROM
     [historia_clinica]
WHERE
    [id_historia] = @id_historia
END 
GO

GRANT EXECUTE ON [historia_clinicaSelect] TO [Public]
GO

 
