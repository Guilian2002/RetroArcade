CREATE PROCEDURE [dbo].[SP_Building_Delete]
    @buildingId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF @buildingId IS NULL
    BEGIN
        RAISERROR('L''ID est requis pour la suppression.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Building] WHERE [Id] = @buildingId)
    BEGIN
        RAISERROR('Bâtiment introuvable.', 16, 1);
        RETURN;
    END

    DELETE FROM [dbo].[Building] WHERE [Id] = @buildingId;
END
