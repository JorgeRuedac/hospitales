--***********************************************************
--SELECT Stored Procedure for servicios table
--***********************************************************
GO

USE hospital
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'serviciosSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [serviciosSelectAll] 
GO

CREATE PROCEDURE [serviciosSelectAll]
AS 
BEGIN 
SELECT 
      [servicios].[Id_servicio]
     ,[servicios].[Nombre_servicio]
FROM
     [servicios]
END 
GO

GRANT EXECUTE ON [serviciosSelectAll] TO [Public]
GO

 
