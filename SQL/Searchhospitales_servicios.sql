--***********************************************************
--SEARCH Stored Procedure for hospitales_servicios table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'hospitales_serviciosSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [hospitales_serviciosSearch] 
GO

CREATE PROCEDURE [hospitales_serviciosSearch] 
      @ID_hospitales_servicios int
     ,@Nombre17 nvarchar(50)
     ,@Nombre_servicio18 nvarchar(50)
     ,@CodigoRefer varchar(50)
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [hospitales_servicios].[ID_hospitales_servicios]
     ,[A17].[Cod_hospital] as [Cod_hospital17]
     ,[A18].[Id_servicio] as [Id_servicio18]
     ,[hospitales_servicios].[CodigoRefer]
FROM
     [hospitales_servicios]
     LEFT JOIN [hospitales] as [A17] ON [hospitales_servicios].[Cod_hospital] = [A17].[Cod_hospital]
     LEFT JOIN [servicios] as [A18] ON [hospitales_servicios].[Id_servicio] = [A18].[Id_servicio]
WHERE
      (@ID_hospitales_servicios IS NULL OR @ID_hospitales_servicios = '' OR [hospitales_servicios].[ID_hospitales_servicios] LIKE '%' + LTRIM(RTRIM(@ID_hospitales_servicios)) + '%')
AND   (@Nombre17 IS NULL OR @Nombre17 = '' OR [A17].[Nombre] LIKE '%' + LTRIM(RTRIM(@Nombre17)) + '%')
AND   (@Nombre_servicio18 IS NULL OR @Nombre_servicio18 = '' OR [A18].[Nombre_servicio] LIKE '%' + LTRIM(RTRIM(@Nombre_servicio18)) + '%')
AND   (@CodigoRefer IS NULL OR @CodigoRefer = '' OR [hospitales_servicios].[CodigoRefer] LIKE '%' + LTRIM(RTRIM(@CodigoRefer)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [hospitales_servicios].[ID_hospitales_servicios]
     ,[A17].[Cod_hospital] as [Cod_hospital17]
     ,[A18].[Id_servicio] as [Id_servicio18]
     ,[hospitales_servicios].[CodigoRefer]
FROM
     [hospitales_servicios]
     LEFT JOIN [hospitales] as [A17] ON [hospitales_servicios].[Cod_hospital] = [A17].[Cod_hospital]
     LEFT JOIN [servicios] as [A18] ON [hospitales_servicios].[Id_servicio] = [A18].[Id_servicio]
WHERE
      (@ID_hospitales_servicios IS NULL OR @ID_hospitales_servicios = '' OR [hospitales_servicios].[ID_hospitales_servicios] = LTRIM(RTRIM(@ID_hospitales_servicios)))
AND   (@Nombre17 IS NULL OR @Nombre17 = '' OR [A17].[Nombre] = LTRIM(RTRIM(@Nombre17)))
AND   (@Nombre_servicio18 IS NULL OR @Nombre_servicio18 = '' OR [A18].[Nombre_servicio] = LTRIM(RTRIM(@Nombre_servicio18)))
AND   (@CodigoRefer IS NULL OR @CodigoRefer = '' OR [hospitales_servicios].[CodigoRefer] = LTRIM(RTRIM(@CodigoRefer)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [hospitales_servicios].[ID_hospitales_servicios]
     ,[A17].[Cod_hospital] as [Cod_hospital17]
     ,[A18].[Id_servicio] as [Id_servicio18]
     ,[hospitales_servicios].[CodigoRefer]
FROM
     [hospitales_servicios]
     LEFT JOIN [hospitales] as [A17] ON [hospitales_servicios].[Cod_hospital] = [A17].[Cod_hospital]
     LEFT JOIN [servicios] as [A18] ON [hospitales_servicios].[Id_servicio] = [A18].[Id_servicio]
WHERE
      (@ID_hospitales_servicios IS NULL OR @ID_hospitales_servicios = '' OR [hospitales_servicios].[ID_hospitales_servicios] LIKE LTRIM(RTRIM(@ID_hospitales_servicios)) + '%')
AND   (@Nombre17 IS NULL OR @Nombre17 = '' OR [A17].[Nombre] LIKE LTRIM(RTRIM(@Nombre17)) + '%')
AND   (@Nombre_servicio18 IS NULL OR @Nombre_servicio18 = '' OR [A18].[Nombre_servicio] LIKE LTRIM(RTRIM(@Nombre_servicio18)) + '%')
AND   (@CodigoRefer IS NULL OR @CodigoRefer = '' OR [hospitales_servicios].[CodigoRefer] LIKE LTRIM(RTRIM(@CodigoRefer)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [hospitales_servicios].[ID_hospitales_servicios]
     ,[A17].[Cod_hospital] as [Cod_hospital17]
     ,[A18].[Id_servicio] as [Id_servicio18]
     ,[hospitales_servicios].[CodigoRefer]
FROM
     [hospitales_servicios]
     LEFT JOIN [hospitales] as [A17] ON [hospitales_servicios].[Cod_hospital] = [A17].[Cod_hospital]
     LEFT JOIN [servicios] as [A18] ON [hospitales_servicios].[Id_servicio] = [A18].[Id_servicio]
WHERE
      (@ID_hospitales_servicios IS NULL OR @ID_hospitales_servicios = '' OR [hospitales_servicios].[ID_hospitales_servicios] > LTRIM(RTRIM(@ID_hospitales_servicios)))
AND   (@Nombre17 IS NULL OR @Nombre17 = '' OR [A17].[Nombre] > LTRIM(RTRIM(@Nombre17)))
AND   (@Nombre_servicio18 IS NULL OR @Nombre_servicio18 = '' OR [A18].[Nombre_servicio] > LTRIM(RTRIM(@Nombre_servicio18)))
AND   (@CodigoRefer IS NULL OR @CodigoRefer = '' OR [hospitales_servicios].[CodigoRefer] > LTRIM(RTRIM(@CodigoRefer)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [hospitales_servicios].[ID_hospitales_servicios]
     ,[A17].[Cod_hospital] as [Cod_hospital17]
     ,[A18].[Id_servicio] as [Id_servicio18]
     ,[hospitales_servicios].[CodigoRefer]
FROM
     [hospitales_servicios]
     LEFT JOIN [hospitales] as [A17] ON [hospitales_servicios].[Cod_hospital] = [A17].[Cod_hospital]
     LEFT JOIN [servicios] as [A18] ON [hospitales_servicios].[Id_servicio] = [A18].[Id_servicio]
WHERE
      (@ID_hospitales_servicios IS NULL OR @ID_hospitales_servicios = '' OR [hospitales_servicios].[ID_hospitales_servicios] < LTRIM(RTRIM(@ID_hospitales_servicios)))
AND   (@Nombre17 IS NULL OR @Nombre17 = '' OR [A17].[Nombre] < LTRIM(RTRIM(@Nombre17)))
AND   (@Nombre_servicio18 IS NULL OR @Nombre_servicio18 = '' OR [A18].[Nombre_servicio] < LTRIM(RTRIM(@Nombre_servicio18)))
AND   (@CodigoRefer IS NULL OR @CodigoRefer = '' OR [hospitales_servicios].[CodigoRefer] < LTRIM(RTRIM(@CodigoRefer)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [hospitales_servicios].[ID_hospitales_servicios]
     ,[A17].[Cod_hospital] as [Cod_hospital17]
     ,[A18].[Id_servicio] as [Id_servicio18]
     ,[hospitales_servicios].[CodigoRefer]
FROM
     [hospitales_servicios]
     LEFT JOIN [hospitales] as [A17] ON [hospitales_servicios].[Cod_hospital] = [A17].[Cod_hospital]
     LEFT JOIN [servicios] as [A18] ON [hospitales_servicios].[Id_servicio] = [A18].[Id_servicio]
WHERE
      (@ID_hospitales_servicios IS NULL OR @ID_hospitales_servicios = '' OR [hospitales_servicios].[ID_hospitales_servicios] >= LTRIM(RTRIM(@ID_hospitales_servicios)))
AND   (@Nombre17 IS NULL OR @Nombre17 = '' OR [A17].[Nombre] >= LTRIM(RTRIM(@Nombre17)))
AND   (@Nombre_servicio18 IS NULL OR @Nombre_servicio18 = '' OR [A18].[Nombre_servicio] >= LTRIM(RTRIM(@Nombre_servicio18)))
AND   (@CodigoRefer IS NULL OR @CodigoRefer = '' OR [hospitales_servicios].[CodigoRefer] >= LTRIM(RTRIM(@CodigoRefer)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [hospitales_servicios].[ID_hospitales_servicios]
     ,[A17].[Cod_hospital] as [Cod_hospital17]
     ,[A18].[Id_servicio] as [Id_servicio18]
     ,[hospitales_servicios].[CodigoRefer]
FROM
     [hospitales_servicios]
     LEFT JOIN [hospitales] as [A17] ON [hospitales_servicios].[Cod_hospital] = [A17].[Cod_hospital]
     LEFT JOIN [servicios] as [A18] ON [hospitales_servicios].[Id_servicio] = [A18].[Id_servicio]
WHERE
      (@ID_hospitales_servicios IS NULL OR @ID_hospitales_servicios = '' OR [hospitales_servicios].[ID_hospitales_servicios] <= LTRIM(RTRIM(@ID_hospitales_servicios)))
AND   (@Nombre17 IS NULL OR @Nombre17 = '' OR [A17].[Nombre] <= LTRIM(RTRIM(@Nombre17)))
AND   (@Nombre_servicio18 IS NULL OR @Nombre_servicio18 = '' OR [A18].[Nombre_servicio] <= LTRIM(RTRIM(@Nombre_servicio18)))
AND   (@CodigoRefer IS NULL OR @CodigoRefer = '' OR [hospitales_servicios].[CodigoRefer] <= LTRIM(RTRIM(@CodigoRefer)))

END

END
GO

GRANT EXECUTE ON [hospitales_serviciosSearch] TO [Public]
GO

 
