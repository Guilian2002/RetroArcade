CREATE TABLE [dbo].[Account]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	[Lastname] NVARCHAR(32) NOT NULL,
	[Firstname] NVARCHAR(32) NOT NULL,
	[Username] NVARCHAR(32) NOT NULL,
	[Email] NVARCHAR(320) NOT NULL,
	[Role] VARCHAR(8) NOT NULL DEFAULT 'User',
	[CreationDate] DATE NOT NULL,
	[DisableDate] DATETIME2,
	[IsActive] BIT NOT NULL,

	CONSTRAINT [PK_Account] PRIMARY KEY ([Id]),
	CONSTRAINT [UK_Account_Username] UNIQUE ([Username]),
    CONSTRAINT [UK_Account_Email] UNIQUE ([Email])
)
