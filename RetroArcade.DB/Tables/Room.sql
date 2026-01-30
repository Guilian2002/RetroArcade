CREATE TABLE [dbo].[Room]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	[Name] NVARCHAR(50) NOT NULL,
	[Number] INT NOT NULL,
	[MachineCapacity] INT NOT NULL DEFAULT 10,
	[Price]	DECIMAL(10,2) NOT NULL,
	[BuildingId] UNIQUEIDENTIFIER NOT NULL,

	CONSTRAINT [PK_Room] PRIMARY KEY ([Id]),

	CONSTRAINT [FK_Room_Building]
        FOREIGN KEY ([BuildingId])
        REFERENCES [Building]([Id])
        ON DELETE CASCADE,

	CONSTRAINT [CK_Room_Number] CHECK ([Number] BETWEEN 1 AND 25),
	CONSTRAINT [CK_Room_MachineCapacity] CHECK ([MachineCapacity] = 10)
)
