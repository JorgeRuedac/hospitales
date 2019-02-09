--***********************************************************
--SELECT Stored Procedure for camas table
--***********************************************************
GO

USE hospital
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'camasSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [camasSelect] 
GO

CREATE PROCEDURE [camasSelect] 
      @id_cama int
AS 
BEGIN 
SELECT 
      [id_cama]
     ,[Num_cama]
     ,[Estado]
     ,[ID_hospitales_servicios]
FROM
     [camas]
WHERE
    [id_cama] = @id_cama
END 
GO

GRANT EXECUTE ON [camasSelect] TO [Public]
GO

 
