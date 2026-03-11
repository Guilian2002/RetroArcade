CREATE PROCEDURE [dbo].[SP_RoomArcadeMachine_Get_All]
	AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        RAM.[RoomId],
        RAM.[State],
        RAM.[InstallationDate],

        AM.[Id] AS ArcadeMachineId,
        AM.[Name] AS MachineName,
        AM.[GameName],

        C.[Id] AS CategorieId,
        C.[Name] AS CategorieName
    FROM [dbo].[RoomArcadeMachine] RAM
    INNER JOIN [dbo].[ArcadeMachine] AM ON RAM.[ArcadeMachineId] = AM.[Id]
    LEFT JOIN [dbo].[Categorie] C ON AM.[CategorieId] = C.[Id]
    ORDER BY RAM.[InstallationDate] DESC;
END
