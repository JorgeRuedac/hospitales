--***********************************************************
--SEARCH Stored Procedure for servicios table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'serviciosSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [serviciosSearch] 
GO

CREATE PROCEDURE [serviciosSearch] 
      @Id_servicio int
     ,@Nombre_servicio nvarchar(50)
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [servicios].[Id_servicio]
     ,[servicios].[Nombre_servicio]
FROM
     [servicios]
WHERE
      (@Id_servicio IS NULL OR @Id_servicio = '' OR [servicios].[Id_servicio] LIKE '%' + LTRIM(RTRIM(@Id_servicio)) + '%')
AND   (@Nombre_servicio IS NULL OR @Nombre_servicio = '' OR [servicios].[Nombre_servicio] LIKE '%' + LTRIM(RTRIM(@Nombre_servicio)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [servicios].[Id_servicio]
     ,[servicios].[Nombre_servicio]
FROM
     [servicios]
WHERE
      (@Id_servicio IS NULL OR @Id_servicio = '' OR [servicios].[Id_servicio] = LTRIM(RTRIM(@Id_servicio)))
AND   (@Nombre_servicio IS NULL OR @Nombre_servicio = '' OR [servicios].[Nombre_servicio] = LTRIM(RTRIM(@Nombre_servicio)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [servicios].[Id_servicio]
     ,[servicios].[Nombre_servicio]
FROM
     [servicios]
WHERE
      (@Id_servicio IS NULL OR @Id_servicio = '' OR [servicios].[Id_servicio] LIKE LTRIM(RTRIM(@Id_servicio)) + '%')
AND   (@Nombre_servicio IS NULL OR @Nombre_servicio = '' OR [servicios].[Nombre_servicio] LIKE LTRIM(RTRIM(@Nombre_servicio)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [servicios].[Id_servicio]
     ,[servicios].[Nombre_servicio]
FROM
     [servicios]
WHERE
      (@Id_servicio IS NULL OR @Id_servicio = '' OR [servicios].[Id_servicio] > LTRIM(RTRIM(@Id_servicio)))
AND   (@Nombre_servicio IS NULL OR @Nombre_servicio = '' OR [servicios].[Nombre_servicio] > LTRIM(RTRIM(@Nombre_servicio)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [servicios].[Id_servicio]
     ,[servicios].[Nombre_servicio]
FROM
     [servicios]
WHERE
      (@Id_servicio IS NULL OR @Id_servicio = '' OR [servicios].[Id_servicio] < LTRIM(RTRIM(@Id_servicio)))
AND   (@Nombre_servicio IS NULL OR @Nombre_servicio = '' OR [servicios].[Nombre_servicio] < LTRIM(RTRIM(@Nombre_servicio)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [servicios].[Id_servicio]
     ,[servicios].[Nombre_servicio]
FROM
     [servicios]
WHERE
      (@Id_servicio IS NULL OR @Id_servicio = '' OR [servicios].[Id_servicio] >= LTRIM(RTRIM(@Id_servicio)))
AND   (@Nombre_servicio IS NULL OR @Nombre_servicio = '' OR [servicios].[Nombre_servicio] >= LTRIM(RTRIM(@Nombre_servicio)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [servicios].[Id_servicio]
     ,[servicios].[Nombre_servicio]
FROM
     [servicios]
WHERE
      (@Id_servicio IS NULL OR @Id_servicio = '' OR [servicios].[Id_servicio] <= LTRIM(RTRIM(@Id_servicio)))
AND   (@Nombre_servicio IS NULL OR @Nombre_servicio = '' OR [servicios].[Nombre_servicio] <= LTRIM(RTRIM(@Nombre_servicio)))

END

END
GO

GRANT EXECUTE ON [serviciosSearch] TO [Public]
GO

 
