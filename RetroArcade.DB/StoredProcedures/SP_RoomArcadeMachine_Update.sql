CREATE PROCEDURE [dbo].[SP_RoomArcadeMachine_Update]
	@RoomId UNIQUEIDENTIFIER,
    @ArcadeMachineId UNIQUEIDENTIFIER,
    @State VARCHAR(16),
    @InstallationDate DATETIME2
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] 
                   WHERE [RoomId] = @RoomId AND [ArcadeMachineId] = @ArcadeMachineId)
    BEGIN
        RAISERROR('Association Salle-Machine introuvable.', 16, 1);
        RETURN;
    END

    IF (@State IS NULL OR LTRIM(@State) = '') OR @InstallationDate IS NULL
    BEGIN
        RAISERROR('L''état et la date d''installation ne peuvent pas être vides.', 16, 1);
        RETURN;
    END

    UPDATE [dbo].[RoomArcadeMachine]
    SET [State] = @State,
        [InstallationDate] = @InstallationDate
    WHERE [RoomId] = @RoomId AND [ArcadeMachineId] = @ArcadeMachineId;
END