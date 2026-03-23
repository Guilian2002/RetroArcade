CREATE TABLE [dbo].[RoomArcadeMachine]
(
	[RoomId] UNIQUEIDENTIFIER NOT NULL, 
    [ArcadeMachineId] UNIQUEIDENTIFIER NOT NULL,
    [State] VARCHAR(16) NOT NULL,
    [InstallationDate] DATETIME2 NOT NULL,

    CONSTRAINT [PK_RoomArcadeMachine] PRIMARY KEY ([RoomId], [ArcadeMachineId]),

    CONSTRAINT [FK_RoomArcadeMachine_Room]
        FOREIGN KEY ([RoomId])
        REFERENCES [Room]([Id])
        ON DELETE CASCADE,

    CONSTRAINT [FK_RoomArcadeMachine_ArcadeMachine]
        FOREIGN KEY ([ArcadeMachineId])
        REFERENCES [ArcadeMachine]([Id])
        ON DELETE CASCADE
)
