--***********************************************************
--SEARCH Stored Procedure for historia_clinica table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'historia_clinicaSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [historia_clinicaSearch] 
GO

CREATE PROCEDURE [historia_clinicaSearch] 
      @id_historia int
     ,@Cedula nvarchar(50)
     ,@Apellido nvarchar(50)
     ,@Nombre nvarchar(50)
     ,@Fecha_nacim date
     ,@Num_seguridad_social nvarchar(50)
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [historia_clinica].[id_historia]
     ,[historia_clinica].[Cedula]
     ,[historia_clinica].[Apellido]
     ,[historia_clinica].[Nombre]
     ,[historia_clinica].[Fecha_nacim]
     ,[historia_clinica].[Num_seguridad_social]
FROM
     [historia_clinica]
WHERE
      (@id_historia IS NULL OR @id_historia = '' OR [historia_clinica].[id_historia] LIKE '%' + LTRIM(RTRIM(@id_historia)) + '%')
AND   (@Cedula IS NULL OR @Cedula = '' OR [historia_clinica].[Cedula] LIKE '%' + LTRIM(RTRIM(@Cedula)) + '%')
AND   (@Apellido IS NULL OR @Apellido = '' OR [historia_clinica].[Apellido] LIKE '%' + LTRIM(RTRIM(@Apellido)) + '%')
AND   (@Nombre IS NULL OR @Nombre = '' OR [historia_clinica].[Nombre] LIKE '%' + LTRIM(RTRIM(@Nombre)) + '%')
AND   (@Fecha_nacim IS NULL OR @Fecha_nacim = '' OR [historia_clinica].[Fecha_nacim] LIKE '%' + LTRIM(RTRIM(@Fecha_nacim)) + '%')
AND   (@Num_seguridad_social IS NULL OR @Num_seguridad_social = '' OR [historia_clinica].[Num_seguridad_social] LIKE '%' + LTRIM(RTRIM(@Num_seguridad_social)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [historia_clinica].[id_historia]
     ,[historia_clinica].[Cedula]
     ,[historia_clinica].[Apellido]
     ,[historia_clinica].[Nombre]
     ,[historia_clinica].[Fecha_nacim]
     ,[historia_clinica].[Num_seguridad_social]
FROM
     [historia_clinica]
WHERE
      (@id_historia IS NULL OR @id_historia = '' OR [historia_clinica].[id_historia] = LTRIM(RTRIM(@id_historia)))
AND   (@Cedula IS NULL OR @Cedula = '' OR [historia_clinica].[Cedula] = LTRIM(RTRIM(@Cedula)))
AND   (@Apellido IS NULL OR @Apellido = '' OR [historia_clinica].[Apellido] = LTRIM(RTRIM(@Apellido)))
AND   (@Nombre IS NULL OR @Nombre = '' OR [historia_clinica].[Nombre] = LTRIM(RTRIM(@Nombre)))
AND   (@Fecha_nacim IS NULL OR @Fecha_nacim = '' OR [historia_clinica].[Fecha_nacim] = LTRIM(RTRIM(@Fecha_nacim)))
AND   (@Num_seguridad_social IS NULL OR @Num_seguridad_social = '' OR [historia_clinica].[Num_seguridad_social] = LTRIM(RTRIM(@Num_seguridad_social)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [historia_clinica].[id_historia]
     ,[historia_clinica].[Cedula]
     ,[historia_clinica].[Apellido]
     ,[historia_clinica].[Nombre]
     ,[historia_clinica].[Fecha_nacim]
     ,[historia_clinica].[Num_seguridad_social]
FROM
     [historia_clinica]
WHERE
      (@id_historia IS NULL OR @id_historia = '' OR [historia_clinica].[id_historia] LIKE LTRIM(RTRIM(@id_historia)) + '%')
AND   (@Cedula IS NULL OR @Cedula = '' OR [historia_clinica].[Cedula] LIKE LTRIM(RTRIM(@Cedula)) + '%')
AND   (@Apellido IS NULL OR @Apellido = '' OR [historia_clinica].[Apellido] LIKE LTRIM(RTRIM(@Apellido)) + '%')
AND   (@Nombre IS NULL OR @Nombre = '' OR [historia_clinica].[Nombre] LIKE LTRIM(RTRIM(@Nombre)) + '%')
AND   (@Fecha_nacim IS NULL OR @Fecha_nacim = '' OR [historia_clinica].[Fecha_nacim] LIKE LTRIM(RTRIM(@Fecha_nacim)) + '%')
AND   (@Num_seguridad_social IS NULL OR @Num_seguridad_social = '' OR [historia_clinica].[Num_seguridad_social] LIKE LTRIM(RTRIM(@Num_seguridad_social)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [historia_clinica].[id_historia]
     ,[historia_clinica].[Cedula]
     ,[historia_clinica].[Apellido]
     ,[historia_clinica].[Nombre]
     ,[historia_clinica].[Fecha_nacim]
     ,[historia_clinica].[Num_seguridad_social]
FROM
     [historia_clinica]
WHERE
      (@id_historia IS NULL OR @id_historia = '' OR [historia_clinica].[id_historia] > LTRIM(RTRIM(@id_historia)))
AND   (@Cedula IS NULL OR @Cedula = '' OR [historia_clinica].[Cedula] > LTRIM(RTRIM(@Cedula)))
AND   (@Apellido IS NULL OR @Apellido = '' OR [historia_clinica].[Apellido] > LTRIM(RTRIM(@Apellido)))
AND   (@Nombre IS NULL OR @Nombre = '' OR [historia_clinica].[Nombre] > LTRIM(RTRIM(@Nombre)))
AND   (@Fecha_nacim IS NULL OR @Fecha_nacim = '' OR [historia_clinica].[Fecha_nacim] > LTRIM(RTRIM(@Fecha_nacim)))
AND   (@Num_seguridad_social IS NULL OR @Num_seguridad_social = '' OR [historia_clinica].[Num_seguridad_social] > LTRIM(RTRIM(@Num_seguridad_social)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [historia_clinica].[id_historia]
     ,[historia_clinica].[Cedula]
     ,[historia_clinica].[Apellido]
     ,[historia_clinica].[Nombre]
     ,[historia_clinica].[Fecha_nacim]
     ,[historia_clinica].[Num_seguridad_social]
FROM
     [historia_clinica]
WHERE
      (@id_historia IS NULL OR @id_historia = '' OR [historia_clinica].[id_historia] < LTRIM(RTRIM(@id_historia)))
AND   (@Cedula IS NULL OR @Cedula = '' OR [historia_clinica].[Cedula] < LTRIM(RTRIM(@Cedula)))
AND   (@Apellido IS NULL OR @Apellido = '' OR [historia_clinica].[Apellido] < LTRIM(RTRIM(@Apellido)))
AND   (@Nombre IS NULL OR @Nombre = '' OR [historia_clinica].[Nombre] < LTRIM(RTRIM(@Nombre)))
AND   (@Fecha_nacim IS NULL OR @Fecha_nacim = '' OR [historia_clinica].[Fecha_nacim] < LTRIM(RTRIM(@Fecha_nacim)))
AND   (@Num_seguridad_social IS NULL OR @Num_seguridad_social = '' OR [historia_clinica].[Num_seguridad_social] < LTRIM(RTRIM(@Num_seguridad_social)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [historia_clinica].[id_historia]
     ,[historia_clinica].[Cedula]
     ,[historia_clinica].[Apellido]
     ,[historia_clinica].[Nombre]
     ,[historia_clinica].[Fecha_nacim]
     ,[historia_clinica].[Num_seguridad_social]
FROM
     [historia_clinica]
WHERE
      (@id_historia IS NULL OR @id_historia = '' OR [historia_clinica].[id_historia] >= LTRIM(RTRIM(@id_historia)))
AND   (@Cedula IS NULL OR @Cedula = '' OR [historia_clinica].[Cedula] >= LTRIM(RTRIM(@Cedula)))
AND   (@Apellido IS NULL OR @Apellido = '' OR [historia_clinica].[Apellido] >= LTRIM(RTRIM(@Apellido)))
AND   (@Nombre IS NULL OR @Nombre = '' OR [historia_clinica].[Nombre] >= LTRIM(RTRIM(@Nombre)))
AND   (@Fecha_nacim IS NULL OR @Fecha_nacim = '' OR [historia_clinica].[Fecha_nacim] >= LTRIM(RTRIM(@Fecha_nacim)))
AND   (@Num_seguridad_social IS NULL OR @Num_seguridad_social = '' OR [historia_clinica].[Num_seguridad_social] >= LTRIM(RTRIM(@Num_seguridad_social)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [historia_clinica].[id_historia]
     ,[historia_clinica].[Cedula]
     ,[historia_clinica].[Apellido]
     ,[historia_clinica].[Nombre]
     ,[historia_clinica].[Fecha_nacim]
     ,[historia_clinica].[Num_seguridad_social]
FROM
     [historia_clinica]
WHERE
      (@id_historia IS NULL OR @id_historia = '' OR [historia_clinica].[id_historia] <= LTRIM(RTRIM(@id_historia)))
AND   (@Cedula IS NULL OR @Cedula = '' OR [historia_clinica].[Cedula] <= LTRIM(RTRIM(@Cedula)))
AND   (@Apellido IS NULL OR @Apellido = '' OR [historia_clinica].[Apellido] <= LTRIM(RTRIM(@Apellido)))
AND   (@Nombre IS NULL OR @Nombre = '' OR [historia_clinica].[Nombre] <= LTRIM(RTRIM(@Nombre)))
AND   (@Fecha_nacim IS NULL OR @Fecha_nacim = '' OR [historia_clinica].[Fecha_nacim] <= LTRIM(RTRIM(@Fecha_nacim)))
AND   (@Num_seguridad_social IS NULL OR @Num_seguridad_social = '' OR [historia_clinica].[Num_seguridad_social] <= LTRIM(RTRIM(@Num_seguridad_social)))

END

END
GO

GRANT EXECUTE ON [historia_clinicaSearch] TO [Public]
GO

 
