CREATE PROCEDURE [dbo].[SP_Building_Get_All]
	AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		[Id],
		[Name],
		[OpeningHour],
		[ClosingHour],
		[Address_Street] as [Street],
        [Address_Number] as [Number],
        [PostalCode],
        [City],
        [Country]
	FROM [dbo].[Building]
	ORDER BY [Name] ASC;
END
