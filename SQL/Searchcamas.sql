--***********************************************************
--SEARCH Stored Procedure for camas table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'camasSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [camasSearch] 
GO

CREATE PROCEDURE [camasSearch] 
      @id_cama int
     ,@Num_cama int
     ,@Estado varchar(1)
     ,@CodigoRefer varchar(50)
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [camas].[id_cama]
     ,[camas].[Num_cama]
     ,[camas].[Estado]
     ,[camas].[ID_hospitales_servicios]
FROM
     [camas]
     LEFT JOIN [hospitales_servicios] ON [camas].[ID_hospitales_servicios] = [hospitales_servicios].[ID_hospitales_servicios]
WHERE
      (@id_cama IS NULL OR @id_cama = '' OR [camas].[id_cama] LIKE '%' + LTRIM(RTRIM(@id_cama)) + '%')
AND   (@Num_cama IS NULL OR @Num_cama = '' OR [camas].[Num_cama] LIKE '%' + LTRIM(RTRIM(@Num_cama)) + '%')
AND   (@Estado IS NULL OR @Estado = '' OR [camas].[Estado] LIKE '%' + LTRIM(RTRIM(@Estado)) + '%')
AND   (@CodigoRefer IS NULL OR @CodigoRefer = '' OR [hospitales_servicios].[CodigoRefer] LIKE '%' + LTRIM(RTRIM(@CodigoRefer)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [camas].[id_cama]
     ,[camas].[Num_cama]
     ,[camas].[Estado]
     ,[camas].[ID_hospitales_servicios]
FROM
     [camas]
     LEFT JOIN [hospitales_servicios] ON [camas].[ID_hospitales_servicios] = [hospitales_servicios].[ID_hospitales_servicios]
WHERE
      (@id_cama IS NULL OR @id_cama = '' OR [camas].[id_cama] = LTRIM(RTRIM(@id_cama)))
AND   (@Num_cama IS NULL OR @Num_cama = '' OR [camas].[Num_cama] = LTRIM(RTRIM(@Num_cama)))
AND   (@Estado IS NULL OR @Estado = '' OR [camas].[Estado] = LTRIM(RTRIM(@Estado)))
AND   (@CodigoRefer IS NULL OR @CodigoRefer = '' OR [hospitales_servicios].[CodigoRefer] = LTRIM(RTRIM(@CodigoRefer)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [camas].[id_cama]
     ,[camas].[Num_cama]
     ,[camas].[Estado]
     ,[camas].[ID_hospitales_servicios]
FROM
     [camas]
     LEFT JOIN [hospitales_servicios] ON [camas].[ID_hospitales_servicios] = [hospitales_servicios].[ID_hospitales_servicios]
WHERE
      (@id_cama IS NULL OR @id_cama = '' OR [camas].[id_cama] LIKE LTRIM(RTRIM(@id_cama)) + '%')
AND   (@Num_cama IS NULL OR @Num_cama = '' OR [camas].[Num_cama] LIKE LTRIM(RTRIM(@Num_cama)) + '%')
AND   (@Estado IS NULL OR @Estado = '' OR [camas].[Estado] LIKE LTRIM(RTRIM(@Estado)) + '%')
AND   (@CodigoRefer IS NULL OR @CodigoRefer = '' OR [hospitales_servicios].[CodigoRefer] LIKE LTRIM(RTRIM(@CodigoRefer)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [camas].[id_cama]
     ,[camas].[Num_cama]
     ,[camas].[Estado]
     ,[camas].[ID_hospitales_servicios]
FROM
     [camas]
     LEFT JOIN [hospitales_servicios] ON [camas].[ID_hospitales_servicios] = [hospitales_servicios].[ID_hospitales_servicios]
WHERE
      (@id_cama IS NULL OR @id_cama = '' OR [camas].[id_cama] > LTRIM(RTRIM(@id_cama)))
AND   (@Num_cama IS NULL OR @Num_cama = '' OR [camas].[Num_cama] > LTRIM(RTRIM(@Num_cama)))
AND   (@Estado IS NULL OR @Estado = '' OR [camas].[Estado] > LTRIM(RTRIM(@Estado)))
AND   (@CodigoRefer IS NULL OR @CodigoRefer = '' OR [hospitales_servicios].[CodigoRefer] > LTRIM(RTRIM(@CodigoRefer)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [camas].[id_cama]
     ,[camas].[Num_cama]
     ,[camas].[Estado]
     ,[camas].[ID_hospitales_servicios]
FROM
     [camas]
     LEFT JOIN [hospitales_servicios] ON [camas].[ID_hospitales_servicios] = [hospitales_servicios].[ID_hospitales_servicios]
WHERE
      (@id_cama IS NULL OR @id_cama = '' OR [camas].[id_cama] < LTRIM(RTRIM(@id_cama)))
AND   (@Num_cama IS NULL OR @Num_cama = '' OR [camas].[Num_cama] < LTRIM(RTRIM(@Num_cama)))
AND   (@Estado IS NULL OR @Estado = '' OR [camas].[Estado] < LTRIM(RTRIM(@Estado)))
AND   (@CodigoRefer IS NULL OR @CodigoRefer = '' OR [hospitales_servicios].[CodigoRefer] < LTRIM(RTRIM(@CodigoRefer)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [camas].[id_cama]
     ,[camas].[Num_cama]
     ,[camas].[Estado]
     ,[camas].[ID_hospitales_servicios]
FROM
     [camas]
     LEFT JOIN [hospitales_servicios] ON [camas].[ID_hospitales_servicios] = [hospitales_servicios].[ID_hospitales_servicios]
WHERE
      (@id_cama IS NULL OR @id_cama = '' OR [camas].[id_cama] >= LTRIM(RTRIM(@id_cama)))
AND   (@Num_cama IS NULL OR @Num_cama = '' OR [camas].[Num_cama] >= LTRIM(RTRIM(@Num_cama)))
AND   (@Estado IS NULL OR @Estado = '' OR [camas].[Estado] >= LTRIM(RTRIM(@Estado)))
AND   (@CodigoRefer IS NULL OR @CodigoRefer = '' OR [hospitales_servicios].[CodigoRefer] >= LTRIM(RTRIM(@CodigoRefer)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [camas].[id_cama]
     ,[camas].[Num_cama]
     ,[camas].[Estado]
     ,[camas].[ID_hospitales_servicios]
FROM
     [camas]
     LEFT JOIN [hospitales_servicios] ON [camas].[ID_hospitales_servicios] = [hospitales_servicios].[ID_hospitales_servicios]
WHERE
      (@id_cama IS NULL OR @id_cama = '' OR [camas].[id_cama] <= LTRIM(RTRIM(@id_cama)))
AND   (@Num_cama IS NULL OR @Num_cama = '' OR [camas].[Num_cama] <= LTRIM(RTRIM(@Num_cama)))
AND   (@Estado IS NULL OR @Estado = '' OR [camas].[Estado] <= LTRIM(RTRIM(@Estado)))
AND   (@CodigoRefer IS NULL OR @CodigoRefer = '' OR [hospitales_servicios].[CodigoRefer] <= LTRIM(RTRIM(@CodigoRefer)))

END

END
GO

GRANT EXECUTE ON [camasSearch] TO [Public]
GO

 
