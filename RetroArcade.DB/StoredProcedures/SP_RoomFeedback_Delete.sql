CREATE PROCEDURE [dbo].[SP_RoomFeedback_Delete]
	@roomFeedbackId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF @roomFeedbackId IS NULL
    BEGIN
        RAISERROR('L''identifiant est requis.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomFeedback] WHERE [Id] = @roomFeedbackId)
    BEGIN
        RAISERROR('Impossible de supprimer : commentaire introuvable.', 16, 1);
        RETURN;
    END

    DELETE FROM [dbo].[RoomFeedback] WHERE [Id] = @roomFeedbackId;
END
