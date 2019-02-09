--***********************************************************
--SELECT Stored Procedure for Visita_medico hospitales_servicios table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'Visita_medico_hospitales_servicios37Select'    and sys.objects.type = 'P') 
DROP PROCEDURE [Visita_medico_hospitales_servicios37Select] 
GO

CREATE PROCEDURE [Visita_medico_hospitales_servicios37Select]
AS 
BEGIN 
SELECT
     [ID_hospitales_servicios]
    ,[CodigoRefer]
FROM 
     [hospitales_servicios]
END 
GO

GRANT EXECUTE ON [Visita_medico_hospitales_servicios37Select] TO [Public]
GO


--***********************************************************
--SELECT Stored Procedure for Visita_medico historia_clinica table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'Visita_medico_historia_clinica38Select'    and sys.objects.type = 'P') 
DROP PROCEDURE [Visita_medico_historia_clinica38Select] 
GO

CREATE PROCEDURE [Visita_medico_historia_clinica38Select]
AS 
BEGIN 
SELECT
     [id_historia]
    ,[Cedula]
FROM 
     [historia_clinica]
END 
GO

GRANT EXECUTE ON [Visita_medico_historia_clinica38Select] TO [Public]
GO


 
