CREATE PROCEDURE [dbo].[SP_Room_Delete]
	@Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF @Id IS NULL
    BEGIN
        RAISERROR('L''identifiant est requis.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Room] WHERE [Id] = @Id)
    BEGIN
        RAISERROR('Impossible de supprimer : salle introuvable.', 16, 1);
        RETURN;
    END

    DELETE FROM [dbo].[Room] WHERE [Id] = @Id;
END
