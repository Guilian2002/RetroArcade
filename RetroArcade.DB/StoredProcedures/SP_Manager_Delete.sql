CREATE PROCEDURE [dbo].[SP_Manager_Delete]
	@Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF @Id IS NULL
    BEGIN
        RAISERROR('L''ID est requis.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Manager] WHERE [Id] = @Id)
    BEGIN
        RAISERROR('Impossible de supprimer : manager inexistant.', 16, 1);
        RETURN;
    END

    DELETE FROM [dbo].[Manager] WHERE [Id] = @Id;
END
