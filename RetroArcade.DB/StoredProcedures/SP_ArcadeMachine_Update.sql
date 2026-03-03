CREATE PROCEDURE [dbo].[SP_ArcadeMachine_Update]
	@Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(64),
    @GameName VARCHAR(64),
    @CategorieId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Id IS NULL OR NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Id)
    BEGIN
        RAISERROR('Machine d''arcade introuvable.', 16, 1);
        RETURN;
    END

    IF (@Name IS NULL OR LTRIM(@Name) = '') OR
       (@GameName IS NULL OR LTRIM(@GameName) = '')
    BEGIN
        RAISERROR('Le nom de la machine et le nom du jeu ne peuvent pas être vides.', 16, 1);
        RETURN;
    END

    IF @CategorieId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM [dbo].[Categorie] WHERE [Id] = @CategorieId)
    BEGIN
        RAISERROR('La catégorie spécifiée est invalide.', 16, 1);
        RETURN;
    END

    UPDATE [dbo].[ArcadeMachine]
    SET [Name] = @Name,
        [GameName] = @GameName,
        [CategorieId] = @CategorieId
    WHERE [Id] = @Id;
END
