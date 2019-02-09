--***********************************************************
--SELECT Stored Procedure for hospitales_servicios table
--***********************************************************
GO

USE hospital
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'hospitales_serviciosSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [hospitales_serviciosSelect] 
GO

CREATE PROCEDURE [hospitales_serviciosSelect] 
      @ID_hospitales_servicios int
AS 
BEGIN 
SELECT 
      [ID_hospitales_servicios]
     ,[Cod_hospital]
     ,[Id_servicio]
     ,[CodigoRefer]
FROM
     [hospitales_servicios]
WHERE
    [ID_hospitales_servicios] = @ID_hospitales_servicios
END 
GO

GRANT EXECUTE ON [hospitales_serviciosSelect] TO [Public]
GO

 
