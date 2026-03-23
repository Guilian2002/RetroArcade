CREATE PROCEDURE [dbo].[SP_RoomArcadeMachine_Delete]
	@RoomId UNIQUEIDENTIFIER,
    @ArcadeMachineId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF @RoomId IS NULL OR @ArcadeMachineId IS NULL
    BEGIN
        RAISERROR('Les deux identifiants (Room et Machine) sont requis pour la suppression.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] 
                   WHERE [RoomId] = @RoomId AND [ArcadeMachineId] = @ArcadeMachineId)
    BEGIN
        RAISERROR('Impossible de supprimer : cette machine n''est pas affectée à cette salle.', 16, 1);
        RETURN;
    END

    DELETE FROM [dbo].[RoomArcadeMachine] 
    WHERE [RoomId] = @RoomId AND [ArcadeMachineId] = @ArcadeMachineId;
END
