--***********************************************************
--SELECT Stored Procedure for hospitales table
--***********************************************************
GO

USE hospital
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'hospitalesSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [hospitalesSelect] 
GO

CREATE PROCEDURE [hospitalesSelect] 
      @Cod_hospital int
AS 
BEGIN 
SELECT 
      [Cod_hospital]
     ,[Nombre]
     ,[Ciudad]
     ,[Tlefono]
     ,[Cod_medico]
FROM
     [hospitales]
WHERE
    [Cod_hospital] = @Cod_hospital
END 
GO

GRANT EXECUTE ON [hospitalesSelect] TO [Public]
GO

 
