CREATE TABLE [dbo].[Booking]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	[BeginDate] DATETIME2 NOT NULL,
	[EndDate] DATETIME2 NOT NULL,
	[GroupSize] INT NOT NULL,
	[Price]	DECIMAL(10,2) NOT NULL,
	[Status] NVARCHAR(16) NOT NULL,
    [RoomId] UNIQUEIDENTIFIER, 
    [AccountId] UNIQUEIDENTIFIER,

    CONSTRAINT [PK_Booking] PRIMARY KEY ([Id]),

    CONSTRAINT [FK_Booking_Room]
        FOREIGN KEY ([RoomId])
        REFERENCES [Room]([Id])
        ON DELETE SET NULL,

    CONSTRAINT [FK_Booking_Account]
        FOREIGN KEY ([AccountId])
        REFERENCES [Account]([Id])
        ON DELETE SET NULL,

    CONSTRAINT [CK_Booking_GroupSize] CHECK ([GroupSize] BETWEEN 4 AND 10),
    CONSTRAINT [CK_Booking_Date] CHECK ([BeginDate] <> [EndDate])
)
