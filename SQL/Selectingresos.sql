--***********************************************************
--SELECT Stored Procedure for ingresos table
--***********************************************************
GO

USE hospital
GO


IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ingresosSelect'    and sys.objects.type = 'P') 
DROP PROCEDURE [ingresosSelect] 
GO

CREATE PROCEDURE [ingresosSelect] 
      @Num_habitacion int
AS 
BEGIN 
SELECT 
      [Num_habitacion]
     ,[Comentario]
     ,[Fecha_ingreso]
     ,[Fecha_salida]
     ,[id_cama]
     ,[id_historia]
FROM
     [ingresos]
WHERE
    [Num_habitacion] = @Num_habitacion
END 
GO

GRANT EXECUTE ON [ingresosSelect] TO [Public]
GO

 
