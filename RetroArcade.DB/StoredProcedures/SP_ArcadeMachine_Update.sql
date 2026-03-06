CREATE PROCEDURE [dbo].[SP_ArcadeMachine_Update]
	@arcadeMachineId UNIQUEIDENTIFIER,
    @name NVARCHAR(64),
    @gameName VARCHAR(64),
    @categorieId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @arcadeMachineId IS NULL OR NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @arcadeMachineId)
    BEGIN
        RAISERROR('Machine d''arcade introuvable.', 16, 1);
        RETURN;
    END

    IF (@name IS NULL OR LTRIM(@name) = '') OR
       (@gameName IS NULL OR LTRIM(@gameName) = '')
    BEGIN
        RAISERROR('Le nom de la machine et le nom du jeu ne peuvent pas être vides.', 16, 1);
        RETURN;
    END

    IF @CategorieId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM [dbo].[Categorie] WHERE [Id] = @categorieId)
    BEGIN
        RAISERROR('La catégorie spécifiée est invalide.', 16, 1);
        RETURN;
    END

    UPDATE [dbo].[ArcadeMachine]
    SET [Name] = @name,
        [GameName] = @gameName,
        [CategorieId] = @categorieId
    WHERE [Id] = @arcadeMachineId;
END
