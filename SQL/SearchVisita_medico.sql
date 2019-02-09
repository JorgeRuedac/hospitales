--***********************************************************
--SEARCH Stored Procedure for Visita_medico table
--***********************************************************
GO

USE hospital
GO

IF EXISTS (SELECT sys.objects.name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.name = 'Visita_medicoSearch'    and sys.objects.type = 'P') 
DROP PROCEDURE [Visita_medicoSearch] 
GO

CREATE PROCEDURE [Visita_medicoSearch] 
      @Cod_visita int
     ,@Fecha date
     ,@Hora varchar(50)
     ,@Diagnostico varchar(500)
     ,@Tratamiento varchar(500)
     ,@CodigoRefer37 varchar(50)
     ,@Cedula38 nvarchar(50)
     ,@SearchCondition nchar(25)
AS
BEGIN

IF @SearchCondition = 'Contains'
BEGIN

SELECT 
      [Visita_medico].[Cod_visita]
     ,[Visita_medico].[Fecha]
     ,[Visita_medico].[Hora]
     ,[Visita_medico].[Diagnostico]
     ,[Visita_medico].[Tratamiento]
     ,[A37].[ID_hospitales_servicios] as [ID_hospitales_servicios37]
     ,[A38].[id_historia] as [id_historia38]
FROM
     [Visita_medico]
     LEFT JOIN [hospitales_servicios] as [A37] ON [Visita_medico].[ID_hospitales_servicios] = [A37].[ID_hospitales_servicios]
     LEFT JOIN [historia_clinica] as [A38] ON [Visita_medico].[id_historia] = [A38].[id_historia]
WHERE
      (@Cod_visita IS NULL OR @Cod_visita = '' OR [Visita_medico].[Cod_visita] LIKE '%' + LTRIM(RTRIM(@Cod_visita)) + '%')
AND   (@Fecha IS NULL OR @Fecha = '' OR [Visita_medico].[Fecha] LIKE '%' + LTRIM(RTRIM(@Fecha)) + '%')
AND   (@Hora IS NULL OR @Hora = '' OR [Visita_medico].[Hora] LIKE '%' + LTRIM(RTRIM(@Hora)) + '%')
AND   (@Diagnostico IS NULL OR @Diagnostico = '' OR [Visita_medico].[Diagnostico] LIKE '%' + LTRIM(RTRIM(@Diagnostico)) + '%')
AND   (@Tratamiento IS NULL OR @Tratamiento = '' OR [Visita_medico].[Tratamiento] LIKE '%' + LTRIM(RTRIM(@Tratamiento)) + '%')
AND   (@CodigoRefer37 IS NULL OR @CodigoRefer37 = '' OR [A37].[CodigoRefer] LIKE '%' + LTRIM(RTRIM(@CodigoRefer37)) + '%')
AND   (@Cedula38 IS NULL OR @Cedula38 = '' OR [A38].[Cedula] LIKE '%' + LTRIM(RTRIM(@Cedula38)) + '%')

END


IF @SearchCondition = 'Equals'
BEGIN

SELECT 
      [Visita_medico].[Cod_visita]
     ,[Visita_medico].[Fecha]
     ,[Visita_medico].[Hora]
     ,[Visita_medico].[Diagnostico]
     ,[Visita_medico].[Tratamiento]
     ,[A37].[ID_hospitales_servicios] as [ID_hospitales_servicios37]
     ,[A38].[id_historia] as [id_historia38]
FROM
     [Visita_medico]
     LEFT JOIN [hospitales_servicios] as [A37] ON [Visita_medico].[ID_hospitales_servicios] = [A37].[ID_hospitales_servicios]
     LEFT JOIN [historia_clinica] as [A38] ON [Visita_medico].[id_historia] = [A38].[id_historia]
WHERE
      (@Cod_visita IS NULL OR @Cod_visita = '' OR [Visita_medico].[Cod_visita] = LTRIM(RTRIM(@Cod_visita)))
AND   (@Fecha IS NULL OR @Fecha = '' OR [Visita_medico].[Fecha] = LTRIM(RTRIM(@Fecha)))
AND   (@Hora IS NULL OR @Hora = '' OR [Visita_medico].[Hora] = LTRIM(RTRIM(@Hora)))
AND   (@Diagnostico IS NULL OR @Diagnostico = '' OR [Visita_medico].[Diagnostico] = LTRIM(RTRIM(@Diagnostico)))
AND   (@Tratamiento IS NULL OR @Tratamiento = '' OR [Visita_medico].[Tratamiento] = LTRIM(RTRIM(@Tratamiento)))
AND   (@CodigoRefer37 IS NULL OR @CodigoRefer37 = '' OR [A37].[CodigoRefer] = LTRIM(RTRIM(@CodigoRefer37)))
AND   (@Cedula38 IS NULL OR @Cedula38 = '' OR [A38].[Cedula] = LTRIM(RTRIM(@Cedula38)))

END


IF @SearchCondition = 'Starts with...'
BEGIN

SELECT
      [Visita_medico].[Cod_visita]
     ,[Visita_medico].[Fecha]
     ,[Visita_medico].[Hora]
     ,[Visita_medico].[Diagnostico]
     ,[Visita_medico].[Tratamiento]
     ,[A37].[ID_hospitales_servicios] as [ID_hospitales_servicios37]
     ,[A38].[id_historia] as [id_historia38]
FROM
     [Visita_medico]
     LEFT JOIN [hospitales_servicios] as [A37] ON [Visita_medico].[ID_hospitales_servicios] = [A37].[ID_hospitales_servicios]
     LEFT JOIN [historia_clinica] as [A38] ON [Visita_medico].[id_historia] = [A38].[id_historia]
WHERE
      (@Cod_visita IS NULL OR @Cod_visita = '' OR [Visita_medico].[Cod_visita] LIKE LTRIM(RTRIM(@Cod_visita)) + '%')
AND   (@Fecha IS NULL OR @Fecha = '' OR [Visita_medico].[Fecha] LIKE LTRIM(RTRIM(@Fecha)) + '%')
AND   (@Hora IS NULL OR @Hora = '' OR [Visita_medico].[Hora] LIKE LTRIM(RTRIM(@Hora)) + '%')
AND   (@Diagnostico IS NULL OR @Diagnostico = '' OR [Visita_medico].[Diagnostico] LIKE LTRIM(RTRIM(@Diagnostico)) + '%')
AND   (@Tratamiento IS NULL OR @Tratamiento = '' OR [Visita_medico].[Tratamiento] LIKE LTRIM(RTRIM(@Tratamiento)) + '%')
AND   (@CodigoRefer37 IS NULL OR @CodigoRefer37 = '' OR [A37].[CodigoRefer] LIKE LTRIM(RTRIM(@CodigoRefer37)) + '%')
AND   (@Cedula38 IS NULL OR @Cedula38 = '' OR [A38].[Cedula] LIKE LTRIM(RTRIM(@Cedula38)) + '%')

END


IF @SearchCondition = 'More than...'
BEGIN

SELECT
      [Visita_medico].[Cod_visita]
     ,[Visita_medico].[Fecha]
     ,[Visita_medico].[Hora]
     ,[Visita_medico].[Diagnostico]
     ,[Visita_medico].[Tratamiento]
     ,[A37].[ID_hospitales_servicios] as [ID_hospitales_servicios37]
     ,[A38].[id_historia] as [id_historia38]
FROM
     [Visita_medico]
     LEFT JOIN [hospitales_servicios] as [A37] ON [Visita_medico].[ID_hospitales_servicios] = [A37].[ID_hospitales_servicios]
     LEFT JOIN [historia_clinica] as [A38] ON [Visita_medico].[id_historia] = [A38].[id_historia]
WHERE
      (@Cod_visita IS NULL OR @Cod_visita = '' OR [Visita_medico].[Cod_visita] > LTRIM(RTRIM(@Cod_visita)))
AND   (@Fecha IS NULL OR @Fecha = '' OR [Visita_medico].[Fecha] > LTRIM(RTRIM(@Fecha)))
AND   (@Hora IS NULL OR @Hora = '' OR [Visita_medico].[Hora] > LTRIM(RTRIM(@Hora)))
AND   (@Diagnostico IS NULL OR @Diagnostico = '' OR [Visita_medico].[Diagnostico] > LTRIM(RTRIM(@Diagnostico)))
AND   (@Tratamiento IS NULL OR @Tratamiento = '' OR [Visita_medico].[Tratamiento] > LTRIM(RTRIM(@Tratamiento)))
AND   (@CodigoRefer37 IS NULL OR @CodigoRefer37 = '' OR [A37].[CodigoRefer] > LTRIM(RTRIM(@CodigoRefer37)))
AND   (@Cedula38 IS NULL OR @Cedula38 = '' OR [A38].[Cedula] > LTRIM(RTRIM(@Cedula38)))

END


IF @SearchCondition = 'Less than...'
BEGIN

SELECT
      [Visita_medico].[Cod_visita]
     ,[Visita_medico].[Fecha]
     ,[Visita_medico].[Hora]
     ,[Visita_medico].[Diagnostico]
     ,[Visita_medico].[Tratamiento]
     ,[A37].[ID_hospitales_servicios] as [ID_hospitales_servicios37]
     ,[A38].[id_historia] as [id_historia38]
FROM
     [Visita_medico]
     LEFT JOIN [hospitales_servicios] as [A37] ON [Visita_medico].[ID_hospitales_servicios] = [A37].[ID_hospitales_servicios]
     LEFT JOIN [historia_clinica] as [A38] ON [Visita_medico].[id_historia] = [A38].[id_historia]
WHERE
      (@Cod_visita IS NULL OR @Cod_visita = '' OR [Visita_medico].[Cod_visita] < LTRIM(RTRIM(@Cod_visita)))
AND   (@Fecha IS NULL OR @Fecha = '' OR [Visita_medico].[Fecha] < LTRIM(RTRIM(@Fecha)))
AND   (@Hora IS NULL OR @Hora = '' OR [Visita_medico].[Hora] < LTRIM(RTRIM(@Hora)))
AND   (@Diagnostico IS NULL OR @Diagnostico = '' OR [Visita_medico].[Diagnostico] < LTRIM(RTRIM(@Diagnostico)))
AND   (@Tratamiento IS NULL OR @Tratamiento = '' OR [Visita_medico].[Tratamiento] < LTRIM(RTRIM(@Tratamiento)))
AND   (@CodigoRefer37 IS NULL OR @CodigoRefer37 = '' OR [A37].[CodigoRefer] < LTRIM(RTRIM(@CodigoRefer37)))
AND   (@Cedula38 IS NULL OR @Cedula38 = '' OR [A38].[Cedula] < LTRIM(RTRIM(@Cedula38)))

END


IF @SearchCondition = 'Equal or more than...'
BEGIN

SELECT
      [Visita_medico].[Cod_visita]
     ,[Visita_medico].[Fecha]
     ,[Visita_medico].[Hora]
     ,[Visita_medico].[Diagnostico]
     ,[Visita_medico].[Tratamiento]
     ,[A37].[ID_hospitales_servicios] as [ID_hospitales_servicios37]
     ,[A38].[id_historia] as [id_historia38]
FROM
     [Visita_medico]
     LEFT JOIN [hospitales_servicios] as [A37] ON [Visita_medico].[ID_hospitales_servicios] = [A37].[ID_hospitales_servicios]
     LEFT JOIN [historia_clinica] as [A38] ON [Visita_medico].[id_historia] = [A38].[id_historia]
WHERE
      (@Cod_visita IS NULL OR @Cod_visita = '' OR [Visita_medico].[Cod_visita] >= LTRIM(RTRIM(@Cod_visita)))
AND   (@Fecha IS NULL OR @Fecha = '' OR [Visita_medico].[Fecha] >= LTRIM(RTRIM(@Fecha)))
AND   (@Hora IS NULL OR @Hora = '' OR [Visita_medico].[Hora] >= LTRIM(RTRIM(@Hora)))
AND   (@Diagnostico IS NULL OR @Diagnostico = '' OR [Visita_medico].[Diagnostico] >= LTRIM(RTRIM(@Diagnostico)))
AND   (@Tratamiento IS NULL OR @Tratamiento = '' OR [Visita_medico].[Tratamiento] >= LTRIM(RTRIM(@Tratamiento)))
AND   (@CodigoRefer37 IS NULL OR @CodigoRefer37 = '' OR [A37].[CodigoRefer] >= LTRIM(RTRIM(@CodigoRefer37)))
AND   (@Cedula38 IS NULL OR @Cedula38 = '' OR [A38].[Cedula] >= LTRIM(RTRIM(@Cedula38)))


END


IF @SearchCondition = 'Equal or less than...'
BEGIN

SELECT
      [Visita_medico].[Cod_visita]
     ,[Visita_medico].[Fecha]
     ,[Visita_medico].[Hora]
     ,[Visita_medico].[Diagnostico]
     ,[Visita_medico].[Tratamiento]
     ,[A37].[ID_hospitales_servicios] as [ID_hospitales_servicios37]
     ,[A38].[id_historia] as [id_historia38]
FROM
     [Visita_medico]
     LEFT JOIN [hospitales_servicios] as [A37] ON [Visita_medico].[ID_hospitales_servicios] = [A37].[ID_hospitales_servicios]
     LEFT JOIN [historia_clinica] as [A38] ON [Visita_medico].[id_historia] = [A38].[id_historia]
WHERE
      (@Cod_visita IS NULL OR @Cod_visita = '' OR [Visita_medico].[Cod_visita] <= LTRIM(RTRIM(@Cod_visita)))
AND   (@Fecha IS NULL OR @Fecha = '' OR [Visita_medico].[Fecha] <= LTRIM(RTRIM(@Fecha)))
AND   (@Hora IS NULL OR @Hora = '' OR [Visita_medico].[Hora] <= LTRIM(RTRIM(@Hora)))
AND   (@Diagnostico IS NULL OR @Diagnostico = '' OR [Visita_medico].[Diagnostico] <= LTRIM(RTRIM(@Diagnostico)))
AND   (@Tratamiento IS NULL OR @Tratamiento = '' OR [Visita_medico].[Tratamiento] <= LTRIM(RTRIM(@Tratamiento)))
AND   (@CodigoRefer37 IS NULL OR @CodigoRefer37 = '' OR [A37].[CodigoRefer] <= LTRIM(RTRIM(@CodigoRefer37)))
AND   (@Cedula38 IS NULL OR @Cedula38 = '' OR [A38].[Cedula] <= LTRIM(RTRIM(@Cedula38)))

END

END
GO

GRANT EXECUTE ON [Visita_medicoSearch] TO [Public]
GO

 
