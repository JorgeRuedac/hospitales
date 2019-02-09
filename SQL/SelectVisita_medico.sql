--***********************************************************
--SELECT Stored Procedure for Visita_medico table
--***********************************************************
GO

USE hospital
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'Visita_medicoSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [Visita_medicoSelect] 
GO

CREATE PROCEDURE [Visita_medicoSelect] 
      @Cod_visita int
AS 
BEGIN 
SELECT 
      [Cod_visita]
     ,[Fecha]
     ,[Hora]
     ,[Diagnostico]
     ,[Tratamiento]
     ,[ID_hospitales_servicios]
     ,[id_historia]
FROM
     [Visita_medico]
WHERE
    [Cod_visita] = @Cod_visita
END 
GO

GRANT EXECUTE ON [Visita_medicoSelect] TO [Public]
GO

 
