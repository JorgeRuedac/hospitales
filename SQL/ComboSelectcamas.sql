--***********************************************************
--SELECT Stored Procedure for camas hospitales_servicios table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'camas_hospitales_serviciosSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [camas_hospitales_serviciosSelect] 
GO

CREATE PROCEDURE [camas_hospitales_serviciosSelect]
AS 
BEGIN 
SELECT
     [ID_hospitales_servicios]
    ,[CodigoRefer]
FROM 
     [hospitales_servicios]
END 
GO

GRANT EXECUTE ON [camas_hospitales_serviciosSelect] TO [Public]
GO


 
