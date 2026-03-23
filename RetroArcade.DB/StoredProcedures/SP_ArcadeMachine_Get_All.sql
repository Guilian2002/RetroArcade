CREATE PROCEDURE [dbo].[SP_ArcadeMachine_Get_All]
	AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        AM.[Id],
        AM.[Name],
        AM.[GameName],
        AM.[CategorieId],
        C.[Name] AS [CategorieName]
    FROM [dbo].[ArcadeMachine] AM
    LEFT JOIN [dbo].[Categorie] C ON AM.[CategorieId] = C.[Id];
END
