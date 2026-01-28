CREATE PROCEDURE [dbo].[SP_Building_Get_All]
	AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		[Id],
		[Name],
		[OpeningHour],
		[ClosingHour],
		[Country],
		[City]
	FROM [dbo].[Building]
	ORDER BY [Name] ASC;
END
