CREATE PROCEDURE [dbo].[SP_RoomArcadeMachine_Insert]
	@RoomId UNIQUEIDENTIFIER,
    @ArcadeMachineId UNIQUEIDENTIFIER,
    @State VARCHAR(16),
    @InstallationDate DATETIME2
AS
BEGIN
    SET NOCOUNT ON;

    IF @RoomId IS NULL OR @ArcadeMachineId IS NULL OR 
       (@State IS NULL OR LTRIM(@State) = '') OR @InstallationDate IS NULL
    BEGIN
        RAISERROR('Tous les champs (RoomId, ArcadeMachineId, State, InstallationDate) sont obligatoires.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Room] WHERE [Id] = @RoomId)
    BEGIN
        RAISERROR('La salle spécifiée n''existe pas.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @ArcadeMachineId)
    BEGIN
        RAISERROR('La machine d''arcade spécifiée n''existe pas.', 16, 1);
        RETURN;
    END

    IF @InstallationDate > SYSDATETIME()
    BEGIN
        RAISERROR('La date d''installation ne peut pas être dans le futur.', 16, 1);
        RETURN;
    END

    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId], [State], [InstallationDate])
    VALUES (@RoomId, @ArcadeMachineId, @State, @InstallationDate);
END
