--***********************************************************
--SELECT Stored Procedure for hospitales_servicios hospitales table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'hospitales_servicios_hospitales17Select'    and sys.objects.type = 'P') 
DROP PROCEDURE [hospitales_servicios_hospitales17Select] 
GO

CREATE PROCEDURE [hospitales_servicios_hospitales17Select]
AS 
BEGIN 
SELECT
     [Cod_hospital]
    ,[Nombre]
FROM 
     [hospitales]
END 
GO

GRANT EXECUTE ON [hospitales_servicios_hospitales17Select] TO [Public]
GO


--***********************************************************
--SELECT Stored Procedure for hospitales_servicios servicios table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'hospitales_servicios_servicios18Select'    and sys.objects.type = 'P') 
DROP PROCEDURE [hospitales_servicios_servicios18Select] 
GO

CREATE PROCEDURE [hospitales_servicios_servicios18Select]
AS 
BEGIN 
SELECT
     [Id_servicio]
    ,[Nombre_servicio]
FROM 
     [servicios]
END 
GO

GRANT EXECUTE ON [hospitales_servicios_servicios18Select] TO [Public]
GO


 
