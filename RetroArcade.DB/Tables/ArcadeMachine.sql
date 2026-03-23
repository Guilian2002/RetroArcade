CREATE TABLE [dbo].[ArcadeMachine]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	[Name] NVARCHAR(64) NOT NULL,
	[GameName] VARCHAR(64) NOT NULL,
	[CategorieId] UNIQUEIDENTIFIER,

	CONSTRAINT [PK_ArcadeMachine] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_ArcadeMachine_Categorie] 
	FOREIGN KEY ([CategorieId])
	REFERENCES [Categorie]([Id])
        ON DELETE SET NULL
)
