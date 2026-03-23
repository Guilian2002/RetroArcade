CREATE PROCEDURE [dbo].[SP_ArcadeMachine_Insert]
	@name NVARCHAR(64),
    @gameName VARCHAR(64),
    @categorieId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF (@name IS NULL OR LTRIM(@name) = '') OR
       (@gameName IS NULL OR LTRIM(@gameName) = '')
    BEGIN
        RAISERROR('Le nom de la machine et le nom du jeu sont obligatoires.', 16, 1);
        RETURN;
    END

    IF @categorieId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM [dbo].[Categorie] WHERE [Id] = @categorieId)
    BEGIN
        RAISERROR('La catégorie spécifiée n''existe pas.', 16, 1);
        RETURN;
    END

    INSERT INTO [dbo].[ArcadeMachine] ([Name], [GameName], [CategorieId])
    VALUES (@name, @gameName, @categorieId);
END
