CREATE PROCEDURE [dbo].[SP_ArcadeMachine_Insert]
	@Name NVARCHAR(64),
    @GameName VARCHAR(64),
    @CategorieId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF (@Name IS NULL OR LTRIM(@Name) = '') OR
       (@GameName IS NULL OR LTRIM(@GameName) = '')
    BEGIN
        RAISERROR('Le nom de la machine et le nom du jeu sont obligatoires.', 16, 1);
        RETURN;
    END

    IF @CategorieId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM [dbo].[Categorie] WHERE [Id] = @CategorieId)
    BEGIN
        RAISERROR('La catégorie spécifiée n''existe pas.', 16, 1);
        RETURN;
    END

    INSERT INTO [dbo].[ArcadeMachine] ([Name], [GameName], [CategorieId])
    VALUES (@Name, @GameName, @CategorieId);
END
