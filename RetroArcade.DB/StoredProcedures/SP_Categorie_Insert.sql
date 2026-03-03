CREATE PROCEDURE [dbo].[SP_Categorie_Insert]
    @Name NVARCHAR(32)
AS
BEGIN
    SET NOCOUNT ON;

    IF @Name IS NULL OR LTRIM(@Name) = ''
    BEGIN
        RAISERROR('Le nom de la catégorie est obligatoire.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM [dbo].[Categorie] WHERE [Name] = @Name)
    BEGIN
        RAISERROR('Cette catégorie existe déjà.', 16, 1);
        RETURN;
    END

    INSERT INTO [dbo].[Categorie] ([Name])
    VALUES (@Name);
END
