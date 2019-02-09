--***********************************************************
--SELECT Stored Procedure for historia_clinica table
--***********************************************************
GO

USE hospital
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'historia_clinicaSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [historia_clinicaSelectAll] 
GO

CREATE PROCEDURE [historia_clinicaSelectAll]
AS 
BEGIN 
SELECT 
      [historia_clinica].[id_historia]
     ,[historia_clinica].[Cedula]
     ,[historia_clinica].[Apellido]
     ,[historia_clinica].[Nombre]
     ,[historia_clinica].[Fecha_nacim]
     ,[historia_clinica].[Num_seguridad_social]
FROM
     [historia_clinica]
END 
GO

GRANT EXECUTE ON [historia_clinicaSelectAll] TO [Public]
GO

 
