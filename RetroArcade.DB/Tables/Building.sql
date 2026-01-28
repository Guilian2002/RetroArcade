CREATE TABLE [dbo].[Building]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    [Name] NVARCHAR(64) NOT NULL,
    [OpeningHour] TIME NOT NULL,
    [ClosingHour] TIME NOT NULL,
    [Address_Street] NVARCHAR(256) NOT NULL,
    [Address_Number] NVARCHAR(12) NOT NULL,
    [PostalCode] NVARCHAR(16) NOT NULL,
    [City] NVARCHAR(50) NOT NULL,
    [Country] NVARCHAR(50) NOT NULL,

    CONSTRAINT [PK_Building] PRIMARY KEY ([Id])
)
