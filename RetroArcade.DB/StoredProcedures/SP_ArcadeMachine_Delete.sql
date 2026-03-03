CREATE PROCEDURE [dbo].[SP_ArcadeMachine_Delete]
	@Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF @Id IS NULL
    BEGIN
        RAISERROR('L''identifiant est requis.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Id)
    BEGIN
        RAISERROR('Impossible de supprimer : machine inexistante.', 16, 1);
        RETURN;
    END

    DELETE FROM [dbo].[ArcadeMachine] WHERE [Id] = @Id;
END
