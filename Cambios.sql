USE [ManejoCitas]
GO
/****** Object:  UserDefinedFunction [dbo].[FUN_ObtenerInformacion]    Script Date: 15/08/2018 22:47:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[FUN_ObtenerInformacion] (
@Tipo INT,
@Activo INT
)
RETURNS @Informacion TABLE (
	 [InformacionId] INT
	,[Fecha] VARCHAR(10)
	,[Titulo] NVARCHAR(200)
	,[Cupo] INT
	,[Descripcion] NVARCHAR(1000)
	,[Activo] BIT
	,[Tipo] INT
	)
AS
BEGIN
	INSERT INTO @Informacion
	SELECT [InformacionId]
		,[dbo].[ConversionFechaEstandar]([Fecha])
		,[Titulo]
		,[Cupo]
		,[Descripcion]
		,[Activo]
		,[Tipo]
	FROM [dbo].[Informacion]
	WHERE 
	Tipo = @Tipo
	AND (Activo = @Activo or @Activo = -1)
	AND (Cast([Fecha] AS DATE) >= Cast(GETDATE() AS DATE) or [Tipo] <> 3);
	RETURN
END
