--***********************************************************
--SELECT Stored Procedure for Visita_medico table
--***********************************************************
GO

USE hospital
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'Visita_medicoSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [Visita_medicoSelectAll] 
GO

CREATE PROCEDURE [Visita_medicoSelectAll]
AS 
BEGIN 
SELECT 
      [Visita_medico].[Cod_visita]
     ,[Visita_medico].[Fecha]
     ,[Visita_medico].[Hora]
     ,[Visita_medico].[Diagnostico]
     ,[Visita_medico].[Tratamiento]
     ,[Visita_medico].[ID_hospitales_servicios]
     ,[Visita_medico].[id_historia]
FROM
     [Visita_medico]
END 
GO

GRANT EXECUTE ON [Visita_medicoSelectAll] TO [Public]
GO

 
