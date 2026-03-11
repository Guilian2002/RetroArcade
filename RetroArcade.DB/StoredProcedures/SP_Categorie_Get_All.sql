CREATE PROCEDURE [dbo].[SP_Categorie_Get_All]
	AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [Id], 
        [Name]
    FROM [dbo].[Categorie]
    ORDER BY [Name] ASC;
END
