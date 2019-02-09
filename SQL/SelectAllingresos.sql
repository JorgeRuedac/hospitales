--***********************************************************
--SELECT Stored Procedure for ingresos table
--***********************************************************
GO

USE hospital
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ingresosSelectAll'    and sys.objects.type = 'P') 
DROP PROCEDURE [ingresosSelectAll] 
GO

CREATE PROCEDURE [ingresosSelectAll]
AS 
BEGIN 
SELECT 
      [ingresos].[Num_habitacion]
     ,[ingresos].[Comentario]
     ,[ingresos].[Fecha_ingreso]
     ,[ingresos].[Fecha_salida]
     ,[ingresos].[id_cama]
     ,[ingresos].[id_historia]
FROM
     [ingresos]
END 
GO

GRANT EXECUTE ON [ingresosSelectAll] TO [Public]
GO

 
