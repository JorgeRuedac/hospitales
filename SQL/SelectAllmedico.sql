--***********************************************************
--SELECT Stored Procedure for medico table
--***********************************************************
GO

USE hospital
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'medicoSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [medicoSelectAll] 
GO

CREATE PROCEDURE [medicoSelectAll]
AS 
BEGIN 
SELECT 
      [medico].[Cod_medico]
     ,[medico].[Cedula]
     ,[medico].[Apellido_medico]
     ,[medico].[Fecha_nacimien]
FROM
     [medico]
END 
GO

GRANT EXECUTE ON [medicoSelectAll] TO [Public]
GO

 
