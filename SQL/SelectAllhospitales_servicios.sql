--***********************************************************
--SELECT Stored Procedure for hospitales_servicios table
--***********************************************************
GO

USE hospital
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'hospitales_serviciosSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [hospitales_serviciosSelectAll] 
GO

CREATE PROCEDURE [hospitales_serviciosSelectAll]
AS 
BEGIN 
SELECT 
      [hospitales_servicios].[ID_hospitales_servicios]
     ,[hospitales_servicios].[Cod_hospital]
     ,[hospitales_servicios].[Id_servicio]
     ,[hospitales_servicios].[CodigoRefer]
FROM
     [hospitales_servicios]
END 
GO

GRANT EXECUTE ON [hospitales_serviciosSelectAll] TO [Public]
GO

 
