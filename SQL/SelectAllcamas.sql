--***********************************************************
--SELECT Stored Procedure for camas table
--***********************************************************
GO

USE hospital
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'camasSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [camasSelectAll] 
GO

CREATE PROCEDURE [camasSelectAll]
AS 
BEGIN 
SELECT 
      [camas].[id_cama]
     ,[camas].[Num_cama]
     ,[camas].[Estado]
     ,[camas].[ID_hospitales_servicios]
FROM
     [camas]
END 
GO

GRANT EXECUTE ON [camasSelectAll] TO [Public]
GO

 
