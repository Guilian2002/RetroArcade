/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
/* Script de Post-Déploiement avec IDs fixes pour éviter les doublons */

-- 1. Définition des IDs fixes (GUIDs constants)
DECLARE @BldId UNIQUEIDENTIFIER = 'A1111111-1111-1111-1111-111111111111';
DECLARE @RmId  UNIQUEIDENTIFIER = 'B2222222-2222-2222-2222-222222222222';
DECLARE @Mc1Id UNIQUEIDENTIFIER = 'C3333333-3333-3333-3333-333333333333';
DECLARE @Mc2Id UNIQUEIDENTIFIER = 'D4444444-4444-4444-4444-444444444444';
DECLARE @Mc3Id UNIQUEIDENTIFIER = 'E5555555-5555-5555-5555-555555555555';
DECLARE @Mc4Id UNIQUEIDENTIFIER = 'F6666666-6666-6666-6666-666666666666';
DECLARE @Mc5Id UNIQUEIDENTIFIER = 'A7777777-7777-7777-7777-777777777777';
DECLARE @Mc6Id UNIQUEIDENTIFIER = 'B8888888-8888-8888-8888-888888888888';
DECLARE @Mc7Id UNIQUEIDENTIFIER = 'C9999999-9999-9999-9999-999999999999';
DECLARE @Mc8Id UNIQUEIDENTIFIER = 'DAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA';
DECLARE @Mc9Id UNIQUEIDENTIFIER = 'EBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB';
DECLARE @Mc10Id UNIQUEIDENTIFIER = 'FCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC';

-- 2. Building
IF NOT EXISTS (SELECT 1 FROM [dbo].[Building] WHERE [Id] = @BldId)
BEGIN
    INSERT INTO [dbo].[Building] ([Id], [Name], [OpeningHour], [ClosingHour], [Address_Street], [Address_Number], [PostalCode], [City], [Country])
    VALUES (@BldId, N'Arcade World Centre', '09:00:00', '23:00:00', N'Rue de la Toison d''Or', N'42A', N'1000', N'Bruxelles', N'Belgique');
END

-- 3. Room
IF NOT EXISTS (SELECT 1 FROM [dbo].[Room] WHERE [Id] = @RmId)
BEGIN
    INSERT INTO [dbo].[Room] ([Id], [Name], [Number], [MachineCapacity], [Price], [BuildingId])
    VALUES (@RmId, N'Zone Retro', 1, 10, 15.50, @BldId);
END

-- 4. ArcadeMachine
IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Mc1Id)
    INSERT INTO [dbo].[ArcadeMachine] ([Id], [Name], [State], [GameName]) VALUES (@Mc1Id, N'Cabinet-01', 'Active', 'Pac-Man');

IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Mc2Id)
    INSERT INTO [dbo].[ArcadeMachine] ([Id], [Name], [State], [GameName]) VALUES (@Mc2Id, N'Cabinet-02', 'Maintenance', 'Street Fighter II');

IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Mc3Id)
    INSERT INTO [dbo].[ArcadeMachine] VALUES (@Mc3Id, N'Cabinet-03', 'Active', 'Donkey Kong');

IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Mc4Id)
    INSERT INTO [dbo].[ArcadeMachine] VALUES (@Mc4Id, N'Cabinet-04', 'Active', 'Space Invaders');

IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Mc5Id)
    INSERT INTO [dbo].[ArcadeMachine] VALUES (@Mc5Id, N'Cabinet-05', 'Active', 'Metal Slug 3');

IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Mc6Id)
    INSERT INTO [dbo].[ArcadeMachine] VALUES (@Mc6Id, N'Cabinet-06', 'Maintenance', 'Mortal Kombat II');

IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Mc7Id)
    INSERT INTO [dbo].[ArcadeMachine] VALUES (@Mc7Id, N'Cabinet-07', 'Active', 'Asteroids');

IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Mc8Id)
    INSERT INTO [dbo].[ArcadeMachine] VALUES (@Mc8Id, N'Cabinet-08', 'Active', 'Galaga');

IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Mc9Id)
    INSERT INTO [dbo].[ArcadeMachine] VALUES (@Mc9Id, N'Cabinet-09', 'Active', 'Tekken 3');

IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Mc10Id)
    INSERT INTO [dbo].[ArcadeMachine] VALUES (@Mc10Id, N'Cabinet-10', 'Active', 'Frogger');

-- 5. Liaison Room / Machine
IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] WHERE [RoomId] = @RmId AND [ArcadeMachineId] = @Mc1Id)
    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId]) VALUES (@RmId, @Mc1Id);

IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] WHERE [RoomId] = @RmId AND [ArcadeMachineId] = @Mc2Id)
    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId]) VALUES (@RmId, @Mc2Id);

IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] WHERE [ArcadeMachineId] = @Mc3Id)
    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId]) VALUES (@RmId, @Mc3Id);

IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] WHERE [ArcadeMachineId] = @Mc4Id)
    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId]) VALUES (@RmId, @Mc4Id);

IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] WHERE [ArcadeMachineId] = @Mc5Id)
    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId]) VALUES (@RmId, @Mc5Id);

IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] WHERE [ArcadeMachineId] = @Mc6Id)
    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId]) VALUES (@RmId, @Mc6Id);

IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] WHERE [ArcadeMachineId] = @Mc7Id)
    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId]) VALUES (@RmId, @Mc7Id);

IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] WHERE [ArcadeMachineId] = @Mc8Id)
    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId]) VALUES (@RmId, @Mc8Id);

IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] WHERE [ArcadeMachineId] = @Mc9Id)
    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId]) VALUES (@RmId, @Mc9Id);

IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] WHERE [ArcadeMachineId] = @Mc10Id)
    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId]) VALUES (@RmId, @Mc10Id);

-- 6. Feedbacks
IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomFeedback] WHERE [Username] = N'GamerX_2026' AND [RoomId] = @RmId)
BEGIN
    INSERT INTO [dbo].[RoomFeedback] ([Id], [Stars], [CommentDate], [Comment], [Username], [RoomId])
    VALUES (NEWID(), 5, SYSDATETIME(), N'Super ambiance en ce début 2026 !', N'GamerX_2026', @RmId);
END
