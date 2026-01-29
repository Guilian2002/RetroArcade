CREATE TABLE [dbo].[AccountCredential]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	[PasswordHash] VARBINARY(32) NOT NULL,
	[Salt] UNIQUEIDENTIFIER NOT NULL,
	[AccountId] UNIQUEIDENTIFIER NOT NULL,

	CONSTRAINT [PK_AccountCredential] PRIMARY KEY ([Id]),

	CONSTRAINT [UK_AccountCredential_AccountId] UNIQUE ([AccountId]),
    CONSTRAINT [UK_AccountCredential_Salt] UNIQUE ([Salt]),

	CONSTRAINT [FK_AccountCredential_Account]
        FOREIGN KEY ([AccountId])
        REFERENCES [Account]([Id])
        ON DELETE CASCADE
)
