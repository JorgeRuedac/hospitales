--***********************************************************
--SELECT Stored Procedure for ingresos camas table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ingresos_camas24Select'    and sys.objects.type = 'P') 
DROP PROCEDURE [ingresos_camas24Select] 
GO

CREATE PROCEDURE [ingresos_camas24Select]
AS 
BEGIN 
SELECT
     [id_cama]
    ,[Num_cama]
FROM 
     [camas]
END 
GO

GRANT EXECUTE ON [ingresos_camas24Select] TO [Public]
GO


--***********************************************************
--SELECT Stored Procedure for ingresos historia_clinica table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ingresos_historia_clinica25Select'    and sys.objects.type = 'P') 
DROP PROCEDURE [ingresos_historia_clinica25Select] 
GO

CREATE PROCEDURE [ingresos_historia_clinica25Select]
AS 
BEGIN 
SELECT
     [id_historia]
    ,[Cedula]
FROM 
     [historia_clinica]
END 
GO

GRANT EXECUTE ON [ingresos_historia_clinica25Select] TO [Public]
GO


 
