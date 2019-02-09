--***********************************************************
--SEARCH Stored Procedure for hospitales table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'hospitalesSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [hospitalesSearch] 
GO

CREATE PROCEDURE [hospitalesSearch] 
      @Cod_hospital int
     ,@Nombre nvarchar(50)
     ,@Ciudad nvarchar(50)
     ,@Tlefono nvarchar(50)
     ,@Cedula nvarchar(50)
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [hospitales].[Cod_hospital]
     ,[hospitales].[Nombre]
     ,[hospitales].[Ciudad]
     ,[hospitales].[Tlefono]
     ,[hospitales].[Cod_medico]
FROM
     [hospitales]
     LEFT JOIN [medico] ON [hospitales].[Cod_medico] = [medico].[Cod_medico]
WHERE
      (@Cod_hospital IS NULL OR @Cod_hospital = '' OR [hospitales].[Cod_hospital] LIKE '%' + LTRIM(RTRIM(@Cod_hospital)) + '%')
AND   (@Nombre IS NULL OR @Nombre = '' OR [hospitales].[Nombre] LIKE '%' + LTRIM(RTRIM(@Nombre)) + '%')
AND   (@Ciudad IS NULL OR @Ciudad = '' OR [hospitales].[Ciudad] LIKE '%' + LTRIM(RTRIM(@Ciudad)) + '%')
AND   (@Tlefono IS NULL OR @Tlefono = '' OR [hospitales].[Tlefono] LIKE '%' + LTRIM(RTRIM(@Tlefono)) + '%')
AND   (@Cedula IS NULL OR @Cedula = '' OR [medico].[Cedula] LIKE '%' + LTRIM(RTRIM(@Cedula)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [hospitales].[Cod_hospital]
     ,[hospitales].[Nombre]
     ,[hospitales].[Ciudad]
     ,[hospitales].[Tlefono]
     ,[hospitales].[Cod_medico]
FROM
     [hospitales]
     LEFT JOIN [medico] ON [hospitales].[Cod_medico] = [medico].[Cod_medico]
WHERE
      (@Cod_hospital IS NULL OR @Cod_hospital = '' OR [hospitales].[Cod_hospital] = LTRIM(RTRIM(@Cod_hospital)))
AND   (@Nombre IS NULL OR @Nombre = '' OR [hospitales].[Nombre] = LTRIM(RTRIM(@Nombre)))
AND   (@Ciudad IS NULL OR @Ciudad = '' OR [hospitales].[Ciudad] = LTRIM(RTRIM(@Ciudad)))
AND   (@Tlefono IS NULL OR @Tlefono = '' OR [hospitales].[Tlefono] = LTRIM(RTRIM(@Tlefono)))
AND   (@Cedula IS NULL OR @Cedula = '' OR [medico].[Cedula] = LTRIM(RTRIM(@Cedula)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [hospitales].[Cod_hospital]
     ,[hospitales].[Nombre]
     ,[hospitales].[Ciudad]
     ,[hospitales].[Tlefono]
     ,[hospitales].[Cod_medico]
FROM
     [hospitales]
     LEFT JOIN [medico] ON [hospitales].[Cod_medico] = [medico].[Cod_medico]
WHERE
      (@Cod_hospital IS NULL OR @Cod_hospital = '' OR [hospitales].[Cod_hospital] LIKE LTRIM(RTRIM(@Cod_hospital)) + '%')
AND   (@Nombre IS NULL OR @Nombre = '' OR [hospitales].[Nombre] LIKE LTRIM(RTRIM(@Nombre)) + '%')
AND   (@Ciudad IS NULL OR @Ciudad = '' OR [hospitales].[Ciudad] LIKE LTRIM(RTRIM(@Ciudad)) + '%')
AND   (@Tlefono IS NULL OR @Tlefono = '' OR [hospitales].[Tlefono] LIKE LTRIM(RTRIM(@Tlefono)) + '%')
AND   (@Cedula IS NULL OR @Cedula = '' OR [medico].[Cedula] LIKE LTRIM(RTRIM(@Cedula)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [hospitales].[Cod_hospital]
     ,[hospitales].[Nombre]
     ,[hospitales].[Ciudad]
     ,[hospitales].[Tlefono]
     ,[hospitales].[Cod_medico]
FROM
     [hospitales]
     LEFT JOIN [medico] ON [hospitales].[Cod_medico] = [medico].[Cod_medico]
WHERE
      (@Cod_hospital IS NULL OR @Cod_hospital = '' OR [hospitales].[Cod_hospital] > LTRIM(RTRIM(@Cod_hospital)))
AND   (@Nombre IS NULL OR @Nombre = '' OR [hospitales].[Nombre] > LTRIM(RTRIM(@Nombre)))
AND   (@Ciudad IS NULL OR @Ciudad = '' OR [hospitales].[Ciudad] > LTRIM(RTRIM(@Ciudad)))
AND   (@Tlefono IS NULL OR @Tlefono = '' OR [hospitales].[Tlefono] > LTRIM(RTRIM(@Tlefono)))
AND   (@Cedula IS NULL OR @Cedula = '' OR [medico].[Cedula] > LTRIM(RTRIM(@Cedula)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [hospitales].[Cod_hospital]
     ,[hospitales].[Nombre]
     ,[hospitales].[Ciudad]
     ,[hospitales].[Tlefono]
     ,[hospitales].[Cod_medico]
FROM
     [hospitales]
     LEFT JOIN [medico] ON [hospitales].[Cod_medico] = [medico].[Cod_medico]
WHERE
      (@Cod_hospital IS NULL OR @Cod_hospital = '' OR [hospitales].[Cod_hospital] < LTRIM(RTRIM(@Cod_hospital)))
AND   (@Nombre IS NULL OR @Nombre = '' OR [hospitales].[Nombre] < LTRIM(RTRIM(@Nombre)))
AND   (@Ciudad IS NULL OR @Ciudad = '' OR [hospitales].[Ciudad] < LTRIM(RTRIM(@Ciudad)))
AND   (@Tlefono IS NULL OR @Tlefono = '' OR [hospitales].[Tlefono] < LTRIM(RTRIM(@Tlefono)))
AND   (@Cedula IS NULL OR @Cedula = '' OR [medico].[Cedula] < LTRIM(RTRIM(@Cedula)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [hospitales].[Cod_hospital]
     ,[hospitales].[Nombre]
     ,[hospitales].[Ciudad]
     ,[hospitales].[Tlefono]
     ,[hospitales].[Cod_medico]
FROM
     [hospitales]
     LEFT JOIN [medico] ON [hospitales].[Cod_medico] = [medico].[Cod_medico]
WHERE
      (@Cod_hospital IS NULL OR @Cod_hospital = '' OR [hospitales].[Cod_hospital] >= LTRIM(RTRIM(@Cod_hospital)))
AND   (@Nombre IS NULL OR @Nombre = '' OR [hospitales].[Nombre] >= LTRIM(RTRIM(@Nombre)))
AND   (@Ciudad IS NULL OR @Ciudad = '' OR [hospitales].[Ciudad] >= LTRIM(RTRIM(@Ciudad)))
AND   (@Tlefono IS NULL OR @Tlefono = '' OR [hospitales].[Tlefono] >= LTRIM(RTRIM(@Tlefono)))
AND   (@Cedula IS NULL OR @Cedula = '' OR [medico].[Cedula] >= LTRIM(RTRIM(@Cedula)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [hospitales].[Cod_hospital]
     ,[hospitales].[Nombre]
     ,[hospitales].[Ciudad]
     ,[hospitales].[Tlefono]
     ,[hospitales].[Cod_medico]
FROM
     [hospitales]
     LEFT JOIN [medico] ON [hospitales].[Cod_medico] = [medico].[Cod_medico]
WHERE
      (@Cod_hospital IS NULL OR @Cod_hospital = '' OR [hospitales].[Cod_hospital] <= LTRIM(RTRIM(@Cod_hospital)))
AND   (@Nombre IS NULL OR @Nombre = '' OR [hospitales].[Nombre] <= LTRIM(RTRIM(@Nombre)))
AND   (@Ciudad IS NULL OR @Ciudad = '' OR [hospitales].[Ciudad] <= LTRIM(RTRIM(@Ciudad)))
AND   (@Tlefono IS NULL OR @Tlefono = '' OR [hospitales].[Tlefono] <= LTRIM(RTRIM(@Tlefono)))
AND   (@Cedula IS NULL OR @Cedula = '' OR [medico].[Cedula] <= LTRIM(RTRIM(@Cedula)))

END

END
GO

GRANT EXECUTE ON [hospitalesSearch] TO [Public]
GO

 
