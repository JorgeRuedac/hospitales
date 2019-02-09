--***********************************************************
--SELECT Stored Procedure for hospitales medico table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'hospitales_medicoSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [hospitales_medicoSelect] 
GO

CREATE PROCEDURE [hospitales_medicoSelect]
AS 
BEGIN 
SELECT
     [Cod_medico]
    ,[Cedula]
FROM 
     [medico]
END 
GO

GRANT EXECUTE ON [hospitales_medicoSelect] TO [Public]
GO


 
