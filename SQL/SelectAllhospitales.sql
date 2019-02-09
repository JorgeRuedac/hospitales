--***********************************************************
--SELECT Stored Procedure for hospitales table
--***********************************************************
GO

USE hospital
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'hospitalesSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [hospitalesSelectAll] 
GO

CREATE PROCEDURE [hospitalesSelectAll]
AS 
BEGIN 
SELECT 
      [hospitales].[Cod_hospital]
     ,[hospitales].[Nombre]
     ,[hospitales].[Ciudad]
     ,[hospitales].[Tlefono]
     ,[hospitales].[Cod_medico]
FROM
     [hospitales]
END 
GO

GRANT EXECUTE ON [hospitalesSelectAll] TO [Public]
GO

 
