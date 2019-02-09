--***********************************************************
--SEARCH Stored Procedure for ingresos table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'ingresosSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [ingresosSearch] 
GO

CREATE PROCEDURE [ingresosSearch] 
      @Num_habitacion int
     ,@Comentario nvarchar(50)
     ,@Fecha_ingreso date
     ,@Fecha_salida date
     ,@Num_cama24 int
     ,@Cedula25 nvarchar(50)
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [ingresos].[Num_habitacion]
     ,[ingresos].[Comentario]
     ,[ingresos].[Fecha_ingreso]
     ,[ingresos].[Fecha_salida]
     ,[A24].[id_cama] as [id_cama24]
     ,[A25].[id_historia] as [id_historia25]
FROM
     [ingresos]
     LEFT JOIN [camas] as [A24] ON [ingresos].[id_cama] = [A24].[id_cama]
     LEFT JOIN [historia_clinica] as [A25] ON [ingresos].[id_historia] = [A25].[id_historia]
WHERE
      (@Num_habitacion IS NULL OR @Num_habitacion = '' OR [ingresos].[Num_habitacion] LIKE '%' + LTRIM(RTRIM(@Num_habitacion)) + '%')
AND   (@Comentario IS NULL OR @Comentario = '' OR [ingresos].[Comentario] LIKE '%' + LTRIM(RTRIM(@Comentario)) + '%')
AND   (@Fecha_ingreso IS NULL OR @Fecha_ingreso = '' OR [ingresos].[Fecha_ingreso] LIKE '%' + LTRIM(RTRIM(@Fecha_ingreso)) + '%')
AND   (@Fecha_salida IS NULL OR @Fecha_salida = '' OR [ingresos].[Fecha_salida] LIKE '%' + LTRIM(RTRIM(@Fecha_salida)) + '%')
AND   (@Num_cama24 IS NULL OR @Num_cama24 = '' OR [A24].[Num_cama] LIKE '%' + LTRIM(RTRIM(@Num_cama24)) + '%')
AND   (@Cedula25 IS NULL OR @Cedula25 = '' OR [A25].[Cedula] LIKE '%' + LTRIM(RTRIM(@Cedula25)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [ingresos].[Num_habitacion]
     ,[ingresos].[Comentario]
     ,[ingresos].[Fecha_ingreso]
     ,[ingresos].[Fecha_salida]
     ,[A24].[id_cama] as [id_cama24]
     ,[A25].[id_historia] as [id_historia25]
FROM
     [ingresos]
     LEFT JOIN [camas] as [A24] ON [ingresos].[id_cama] = [A24].[id_cama]
     LEFT JOIN [historia_clinica] as [A25] ON [ingresos].[id_historia] = [A25].[id_historia]
WHERE
      (@Num_habitacion IS NULL OR @Num_habitacion = '' OR [ingresos].[Num_habitacion] = LTRIM(RTRIM(@Num_habitacion)))
AND   (@Comentario IS NULL OR @Comentario = '' OR [ingresos].[Comentario] = LTRIM(RTRIM(@Comentario)))
AND   (@Fecha_ingreso IS NULL OR @Fecha_ingreso = '' OR [ingresos].[Fecha_ingreso] = LTRIM(RTRIM(@Fecha_ingreso)))
AND   (@Fecha_salida IS NULL OR @Fecha_salida = '' OR [ingresos].[Fecha_salida] = LTRIM(RTRIM(@Fecha_salida)))
AND   (@Num_cama24 IS NULL OR @Num_cama24 = '' OR [A24].[Num_cama] = LTRIM(RTRIM(@Num_cama24)))
AND   (@Cedula25 IS NULL OR @Cedula25 = '' OR [A25].[Cedula] = LTRIM(RTRIM(@Cedula25)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [ingresos].[Num_habitacion]
     ,[ingresos].[Comentario]
     ,[ingresos].[Fecha_ingreso]
     ,[ingresos].[Fecha_salida]
     ,[A24].[id_cama] as [id_cama24]
     ,[A25].[id_historia] as [id_historia25]
FROM
     [ingresos]
     LEFT JOIN [camas] as [A24] ON [ingresos].[id_cama] = [A24].[id_cama]
     LEFT JOIN [historia_clinica] as [A25] ON [ingresos].[id_historia] = [A25].[id_historia]
WHERE
      (@Num_habitacion IS NULL OR @Num_habitacion = '' OR [ingresos].[Num_habitacion] LIKE LTRIM(RTRIM(@Num_habitacion)) + '%')
AND   (@Comentario IS NULL OR @Comentario = '' OR [ingresos].[Comentario] LIKE LTRIM(RTRIM(@Comentario)) + '%')
AND   (@Fecha_ingreso IS NULL OR @Fecha_ingreso = '' OR [ingresos].[Fecha_ingreso] LIKE LTRIM(RTRIM(@Fecha_ingreso)) + '%')
AND   (@Fecha_salida IS NULL OR @Fecha_salida = '' OR [ingresos].[Fecha_salida] LIKE LTRIM(RTRIM(@Fecha_salida)) + '%')
AND   (@Num_cama24 IS NULL OR @Num_cama24 = '' OR [A24].[Num_cama] LIKE LTRIM(RTRIM(@Num_cama24)) + '%')
AND   (@Cedula25 IS NULL OR @Cedula25 = '' OR [A25].[Cedula] LIKE LTRIM(RTRIM(@Cedula25)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [ingresos].[Num_habitacion]
     ,[ingresos].[Comentario]
     ,[ingresos].[Fecha_ingreso]
     ,[ingresos].[Fecha_salida]
     ,[A24].[id_cama] as [id_cama24]
     ,[A25].[id_historia] as [id_historia25]
FROM
     [ingresos]
     LEFT JOIN [camas] as [A24] ON [ingresos].[id_cama] = [A24].[id_cama]
     LEFT JOIN [historia_clinica] as [A25] ON [ingresos].[id_historia] = [A25].[id_historia]
WHERE
      (@Num_habitacion IS NULL OR @Num_habitacion = '' OR [ingresos].[Num_habitacion] > LTRIM(RTRIM(@Num_habitacion)))
AND   (@Comentario IS NULL OR @Comentario = '' OR [ingresos].[Comentario] > LTRIM(RTRIM(@Comentario)))
AND   (@Fecha_ingreso IS NULL OR @Fecha_ingreso = '' OR [ingresos].[Fecha_ingreso] > LTRIM(RTRIM(@Fecha_ingreso)))
AND   (@Fecha_salida IS NULL OR @Fecha_salida = '' OR [ingresos].[Fecha_salida] > LTRIM(RTRIM(@Fecha_salida)))
AND   (@Num_cama24 IS NULL OR @Num_cama24 = '' OR [A24].[Num_cama] > LTRIM(RTRIM(@Num_cama24)))
AND   (@Cedula25 IS NULL OR @Cedula25 = '' OR [A25].[Cedula] > LTRIM(RTRIM(@Cedula25)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [ingresos].[Num_habitacion]
     ,[ingresos].[Comentario]
     ,[ingresos].[Fecha_ingreso]
     ,[ingresos].[Fecha_salida]
     ,[A24].[id_cama] as [id_cama24]
     ,[A25].[id_historia] as [id_historia25]
FROM
     [ingresos]
     LEFT JOIN [camas] as [A24] ON [ingresos].[id_cama] = [A24].[id_cama]
     LEFT JOIN [historia_clinica] as [A25] ON [ingresos].[id_historia] = [A25].[id_historia]
WHERE
      (@Num_habitacion IS NULL OR @Num_habitacion = '' OR [ingresos].[Num_habitacion] < LTRIM(RTRIM(@Num_habitacion)))
AND   (@Comentario IS NULL OR @Comentario = '' OR [ingresos].[Comentario] < LTRIM(RTRIM(@Comentario)))
AND   (@Fecha_ingreso IS NULL OR @Fecha_ingreso = '' OR [ingresos].[Fecha_ingreso] < LTRIM(RTRIM(@Fecha_ingreso)))
AND   (@Fecha_salida IS NULL OR @Fecha_salida = '' OR [ingresos].[Fecha_salida] < LTRIM(RTRIM(@Fecha_salida)))
AND   (@Num_cama24 IS NULL OR @Num_cama24 = '' OR [A24].[Num_cama] < LTRIM(RTRIM(@Num_cama24)))
AND   (@Cedula25 IS NULL OR @Cedula25 = '' OR [A25].[Cedula] < LTRIM(RTRIM(@Cedula25)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [ingresos].[Num_habitacion]
     ,[ingresos].[Comentario]
     ,[ingresos].[Fecha_ingreso]
     ,[ingresos].[Fecha_salida]
     ,[A24].[id_cama] as [id_cama24]
     ,[A25].[id_historia] as [id_historia25]
FROM
     [ingresos]
     LEFT JOIN [camas] as [A24] ON [ingresos].[id_cama] = [A24].[id_cama]
     LEFT JOIN [historia_clinica] as [A25] ON [ingresos].[id_historia] = [A25].[id_historia]
WHERE
      (@Num_habitacion IS NULL OR @Num_habitacion = '' OR [ingresos].[Num_habitacion] >= LTRIM(RTRIM(@Num_habitacion)))
AND   (@Comentario IS NULL OR @Comentario = '' OR [ingresos].[Comentario] >= LTRIM(RTRIM(@Comentario)))
AND   (@Fecha_ingreso IS NULL OR @Fecha_ingreso = '' OR [ingresos].[Fecha_ingreso] >= LTRIM(RTRIM(@Fecha_ingreso)))
AND   (@Fecha_salida IS NULL OR @Fecha_salida = '' OR [ingresos].[Fecha_salida] >= LTRIM(RTRIM(@Fecha_salida)))
AND   (@Num_cama24 IS NULL OR @Num_cama24 = '' OR [A24].[Num_cama] >= LTRIM(RTRIM(@Num_cama24)))
AND   (@Cedula25 IS NULL OR @Cedula25 = '' OR [A25].[Cedula] >= LTRIM(RTRIM(@Cedula25)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [ingresos].[Num_habitacion]
     ,[ingresos].[Comentario]
     ,[ingresos].[Fecha_ingreso]
     ,[ingresos].[Fecha_salida]
     ,[A24].[id_cama] as [id_cama24]
     ,[A25].[id_historia] as [id_historia25]
FROM
     [ingresos]
     LEFT JOIN [camas] as [A24] ON [ingresos].[id_cama] = [A24].[id_cama]
     LEFT JOIN [historia_clinica] as [A25] ON [ingresos].[id_historia] = [A25].[id_historia]
WHERE
      (@Num_habitacion IS NULL OR @Num_habitacion = '' OR [ingresos].[Num_habitacion] <= LTRIM(RTRIM(@Num_habitacion)))
AND   (@Comentario IS NULL OR @Comentario = '' OR [ingresos].[Comentario] <= LTRIM(RTRIM(@Comentario)))
AND   (@Fecha_ingreso IS NULL OR @Fecha_ingreso = '' OR [ingresos].[Fecha_ingreso] <= LTRIM(RTRIM(@Fecha_ingreso)))
AND   (@Fecha_salida IS NULL OR @Fecha_salida = '' OR [ingresos].[Fecha_salida] <= LTRIM(RTRIM(@Fecha_salida)))
AND   (@Num_cama24 IS NULL OR @Num_cama24 = '' OR [A24].[Num_cama] <= LTRIM(RTRIM(@Num_cama24)))
AND   (@Cedula25 IS NULL OR @Cedula25 = '' OR [A25].[Cedula] <= LTRIM(RTRIM(@Cedula25)))

END

END
GO

GRANT EXECUTE ON [ingresosSearch] TO [Public]
GO

 
