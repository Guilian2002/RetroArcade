CREATE TABLE [dbo].[Booking]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	[BookingDate] DATE NOT NULL,
	[BeginHour] TIME NOT NULL,
	[EndHour] TIME NOT NULL,
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
    CONSTRAINT [CK_Booking_Hour] CHECK ([BeginHour] <> [EndHour])
)
