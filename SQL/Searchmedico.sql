--***********************************************************
--SEARCH Stored Procedure for medico table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'medicoSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [medicoSearch] 
GO

CREATE PROCEDURE [medicoSearch] 
      @Cod_medico int
     ,@Cedula nvarchar(50)
     ,@Apellido_medico nvarchar(50)
     ,@Fecha_nacimien date
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [medico].[Cod_medico]
     ,[medico].[Cedula]
     ,[medico].[Apellido_medico]
     ,[medico].[Fecha_nacimien]
FROM
     [medico]
WHERE
      (@Cod_medico IS NULL OR @Cod_medico = '' OR [medico].[Cod_medico] LIKE '%' + LTRIM(RTRIM(@Cod_medico)) + '%')
AND   (@Cedula IS NULL OR @Cedula = '' OR [medico].[Cedula] LIKE '%' + LTRIM(RTRIM(@Cedula)) + '%')
AND   (@Apellido_medico IS NULL OR @Apellido_medico = '' OR [medico].[Apellido_medico] LIKE '%' + LTRIM(RTRIM(@Apellido_medico)) + '%')
AND   (@Fecha_nacimien IS NULL OR @Fecha_nacimien = '' OR [medico].[Fecha_nacimien] LIKE '%' + LTRIM(RTRIM(@Fecha_nacimien)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [medico].[Cod_medico]
     ,[medico].[Cedula]
     ,[medico].[Apellido_medico]
     ,[medico].[Fecha_nacimien]
FROM
     [medico]
WHERE
      (@Cod_medico IS NULL OR @Cod_medico = '' OR [medico].[Cod_medico] = LTRIM(RTRIM(@Cod_medico)))
AND   (@Cedula IS NULL OR @Cedula = '' OR [medico].[Cedula] = LTRIM(RTRIM(@Cedula)))
AND   (@Apellido_medico IS NULL OR @Apellido_medico = '' OR [medico].[Apellido_medico] = LTRIM(RTRIM(@Apellido_medico)))
AND   (@Fecha_nacimien IS NULL OR @Fecha_nacimien = '' OR [medico].[Fecha_nacimien] = LTRIM(RTRIM(@Fecha_nacimien)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [medico].[Cod_medico]
     ,[medico].[Cedula]
     ,[medico].[Apellido_medico]
     ,[medico].[Fecha_nacimien]
FROM
     [medico]
WHERE
      (@Cod_medico IS NULL OR @Cod_medico = '' OR [medico].[Cod_medico] LIKE LTRIM(RTRIM(@Cod_medico)) + '%')
AND   (@Cedula IS NULL OR @Cedula = '' OR [medico].[Cedula] LIKE LTRIM(RTRIM(@Cedula)) + '%')
AND   (@Apellido_medico IS NULL OR @Apellido_medico = '' OR [medico].[Apellido_medico] LIKE LTRIM(RTRIM(@Apellido_medico)) + '%')
AND   (@Fecha_nacimien IS NULL OR @Fecha_nacimien = '' OR [medico].[Fecha_nacimien] LIKE LTRIM(RTRIM(@Fecha_nacimien)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [medico].[Cod_medico]
     ,[medico].[Cedula]
     ,[medico].[Apellido_medico]
     ,[medico].[Fecha_nacimien]
FROM
     [medico]
WHERE
      (@Cod_medico IS NULL OR @Cod_medico = '' OR [medico].[Cod_medico] > LTRIM(RTRIM(@Cod_medico)))
AND   (@Cedula IS NULL OR @Cedula = '' OR [medico].[Cedula] > LTRIM(RTRIM(@Cedula)))
AND   (@Apellido_medico IS NULL OR @Apellido_medico = '' OR [medico].[Apellido_medico] > LTRIM(RTRIM(@Apellido_medico)))
AND   (@Fecha_nacimien IS NULL OR @Fecha_nacimien = '' OR [medico].[Fecha_nacimien] > LTRIM(RTRIM(@Fecha_nacimien)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [medico].[Cod_medico]
     ,[medico].[Cedula]
     ,[medico].[Apellido_medico]
     ,[medico].[Fecha_nacimien]
FROM
     [medico]
WHERE
      (@Cod_medico IS NULL OR @Cod_medico = '' OR [medico].[Cod_medico] < LTRIM(RTRIM(@Cod_medico)))
AND   (@Cedula IS NULL OR @Cedula = '' OR [medico].[Cedula] < LTRIM(RTRIM(@Cedula)))
AND   (@Apellido_medico IS NULL OR @Apellido_medico = '' OR [medico].[Apellido_medico] < LTRIM(RTRIM(@Apellido_medico)))
AND   (@Fecha_nacimien IS NULL OR @Fecha_nacimien = '' OR [medico].[Fecha_nacimien] < LTRIM(RTRIM(@Fecha_nacimien)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [medico].[Cod_medico]
     ,[medico].[Cedula]
     ,[medico].[Apellido_medico]
     ,[medico].[Fecha_nacimien]
FROM
     [medico]
WHERE
      (@Cod_medico IS NULL OR @Cod_medico = '' OR [medico].[Cod_medico] >= LTRIM(RTRIM(@Cod_medico)))
AND   (@Cedula IS NULL OR @Cedula = '' OR [medico].[Cedula] >= LTRIM(RTRIM(@Cedula)))
AND   (@Apellido_medico IS NULL OR @Apellido_medico = '' OR [medico].[Apellido_medico] >= LTRIM(RTRIM(@Apellido_medico)))
AND   (@Fecha_nacimien IS NULL OR @Fecha_nacimien = '' OR [medico].[Fecha_nacimien] >= LTRIM(RTRIM(@Fecha_nacimien)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [medico].[Cod_medico]
     ,[medico].[Cedula]
     ,[medico].[Apellido_medico]
     ,[medico].[Fecha_nacimien]
FROM
     [medico]
WHERE
      (@Cod_medico IS NULL OR @Cod_medico = '' OR [medico].[Cod_medico] <= LTRIM(RTRIM(@Cod_medico)))
AND   (@Cedula IS NULL OR @Cedula = '' OR [medico].[Cedula] <= LTRIM(RTRIM(@Cedula)))
AND   (@Apellido_medico IS NULL OR @Apellido_medico = '' OR [medico].[Apellido_medico] <= LTRIM(RTRIM(@Apellido_medico)))
AND   (@Fecha_nacimien IS NULL OR @Fecha_nacimien = '' OR [medico].[Fecha_nacimien] <= LTRIM(RTRIM(@Fecha_nacimien)))

END

END
GO

GRANT EXECUTE ON [medicoSearch] TO [Public]
GO

 
