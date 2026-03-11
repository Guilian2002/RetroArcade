CREATE PROCEDURE [dbo].[SP_Categorie_Delete]
	@Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF @Id IS NULL
    BEGIN
        RAISERROR('L''identifiant est requis pour la suppression.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Categorie] WHERE [Id] = @Id)
    BEGIN
        RAISERROR('Impossible de supprimer : catégorie inexistante.', 16, 1);
        RETURN;
    END

    DELETE FROM [dbo].[Categorie] 
    WHERE [Id] = @Id;
END