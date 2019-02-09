--***********************************************************
--SELECT Stored Procedure for servicios table
--***********************************************************
GO

USE hospital
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'serviciosSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [serviciosSelect] 
GO

CREATE PROCEDURE [serviciosSelect] 
      @Id_servicio int
AS 
BEGIN 
SELECT 
      [Id_servicio]
     ,[Nombre_servicio]
FROM
     [servicios]
WHERE
    [Id_servicio] = @Id_servicio
END 
GO

GRANT EXECUTE ON [serviciosSelect] TO [Public]
GO

 
