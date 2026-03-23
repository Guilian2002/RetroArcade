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

-- Définition des IDs fixes (GUIDs constants)
DECLARE @BldId UNIQUEIDENTIFIER =  'A1111111-1111-1111-1111-111111111111';
DECLARE @RmId  UNIQUEIDENTIFIER =  'B2222222-2222-2222-2222-222222222222';
DECLARE @Mc1Id UNIQUEIDENTIFIER =  'C3333333-3333-3333-3333-333333333333';
DECLARE @Mc2Id UNIQUEIDENTIFIER =  'D4444444-4444-4444-4444-444444444444';
DECLARE @Mc3Id UNIQUEIDENTIFIER =  'E5555555-5555-5555-5555-555555555555';
DECLARE @Mc4Id UNIQUEIDENTIFIER =  'F6666666-6666-6666-6666-666666666666';
DECLARE @Mc5Id UNIQUEIDENTIFIER =  'A7777777-7777-7777-7777-777777777777';
DECLARE @Mc6Id UNIQUEIDENTIFIER =  'B8888888-8888-8888-8888-888888888888';
DECLARE @Mc7Id UNIQUEIDENTIFIER =  'C9999999-9999-9999-9999-999999999999';
DECLARE @Mc8Id UNIQUEIDENTIFIER =  'DAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA';
DECLARE @Mc9Id UNIQUEIDENTIFIER =  'EBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB';
DECLARE @Mc10Id UNIQUEIDENTIFIER = 'FCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC';
DECLARE @MngrId UNIQUEIDENTIFIER =  'D0000000-DDDD-DDDD-CCCC-CCCCCCCCCCCC';
DECLARE @Ctgr1Id UNIQUEIDENTIFIER = 'E0000000-EEEE-EEEE-CCCC-CCCCCCCCCCCC';
DECLARE @Ctgr2Id UNIQUEIDENTIFIER = 'F0000000-FFFF-FFFF-CCCC-CCCCCCCCCCCC';
DECLARE @Ctgr3Id UNIQUEIDENTIFIER = 'A0000000-0000-0000-CCCC-CCCCCCCCCCCC';
DECLARE @Ctgr4Id UNIQUEIDENTIFIER = 'B0000000-0000-0000-CCCC-CCCCCCCCCCCC';

-- Account/AccountCredential
EXEC [dbo].[SP_Account_Insert] 
    @lastname = 'Mich', 
    @firstname = 'Mich', 
    @username = 'michmichadmin', 
    @role = 'Admin', 
    @email = 'mich.mich@example.com', 
    @password = 'MichMich123!';

EXEC [dbo].[SP_Account_Insert] 
    @lastname = 'Doe', 
    @firstname = 'John', 
    @username = 'HeyJohn', 
    @role = 'User', 
    @email = 'jdoe@example.com', 
    @password = 'JohnTheJohn123!';

EXEC [dbo].[SP_Account_Insert] 
    @lastname = 'Dalead', 
    @firstname = 'Robert', 
    @username = 'RobertTheBest', 
    @role = 'Manager', 
    @email = 'robertd.manager@example.com', 
    @password = 'RobertMRobert123!';

-- Manager
IF NOT EXISTS (SELECT 1 FROM [dbo].[Manager] WHERE [Id] = @MngrId)
BEGIN
    INSERT INTO [dbo].[Manager] ([Id], [Lastname], [Firstname], [Username], [Email])
    VALUES (@MngrId, N'Dalead', N'Robert', N'RobertTheBest', N'robertd.manager@example.com');
END

-- Categorie
IF NOT EXISTS (SELECT 1 FROM [dbo].[Categorie] WHERE [Id] = @Ctgr1Id)
BEGIN
    INSERT INTO [dbo].[Categorie] ([Id], [Name]) VALUES (@Ctgr1Id, N'Action');
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Categorie] WHERE [Id] = @Ctgr2Id)
BEGIN
    INSERT INTO [dbo].[Categorie] ([Id], [Name]) VALUES (@Ctgr2Id, N'Fight');
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Categorie] WHERE [Id] = @Ctgr3Id)
BEGIN
    INSERT INTO [dbo].[Categorie] ([Id], [Name]) VALUES (@Ctgr3Id, N'Platform');
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Categorie] WHERE [Id] = @Ctgr4Id)
BEGIN
    INSERT INTO [dbo].[Categorie] ([Id], [Name]) VALUES (@Ctgr4Id, N'Shoot''em up');
END

-- Building
IF NOT EXISTS (SELECT 1 FROM [dbo].[Building] WHERE [Id] = @BldId)
BEGIN
    INSERT INTO [dbo].[Building] ([Id], [Name], [OpeningHour], [ClosingHour], [Address_Street], [Address_Number], [PostalCode], [City], [Country], [ManagerId])
    VALUES (@BldId, N'Arcade World Centre', '09:00:00', '23:00:00', N'Rue de la Toison d''Or', N'42A', N'1000', N'Bruxelles', N'Belgique', @MngrId);
END

-- Room
IF NOT EXISTS (SELECT 1 FROM [dbo].[Room] WHERE [Id] = @RmId)
BEGIN
    INSERT INTO [dbo].[Room] ([Id], [Name], [Number], [MachineCapacity], [Price], [BuildingId])
    VALUES (@RmId, N'Zone Retro', 1, 10, 15.50, @BldId);
END

-- ArcadeMachine
IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Mc1Id)
    INSERT INTO [dbo].[ArcadeMachine] ([Id], [Name], [GameName], [CategorieId]) VALUES (@Mc1Id, N'PacClassic', 'Pac-Man', @Ctgr1Id);

IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Mc2Id)
    INSERT INTO [dbo].[ArcadeMachine] ([Id], [Name], [GameName], [CategorieId]) VALUES (@Mc2Id, N'InFighter', 'Street Fighter II', @Ctgr2Id);

IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Mc3Id)
    INSERT INTO [dbo].[ArcadeMachine] ([Id], [Name], [GameName], [CategorieId]) VALUES (@Mc3Id, N'Nintendong', 'Donkey Kong', @Ctgr3Id);

IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Mc4Id)
    INSERT INTO [dbo].[ArcadeMachine] ([Id], [Name], [GameName], [CategorieId]) VALUES (@Mc4Id, N'InSpace', 'Space Invaders', @Ctgr4Id);

IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Mc5Id)
    INSERT INTO [dbo].[ArcadeMachine] ([Id], [Name], [GameName], [CategorieId]) VALUES (@Mc5Id, N'NeoGeoSlug', 'Metal Slug 3', @Ctgr3Id);

IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Mc6Id)
    INSERT INTO [dbo].[ArcadeMachine] ([Id], [Name], [GameName], [CategorieId]) VALUES (@Mc6Id, N'MidwayKombat', 'Mortal Kombat II', @Ctgr2Id);

IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Mc7Id)
    INSERT INTO [dbo].[ArcadeMachine] ([Id], [Name], [GameName], [CategorieId]) VALUES (@Mc7Id, N'Atarioids', 'Asteroids', @Ctgr4Id);

IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Mc8Id)
    INSERT INTO [dbo].[ArcadeMachine] ([Id], [Name], [GameName], [CategorieId]) VALUES (@Mc8Id, N'Namcolga', 'Galaga', @Ctgr4Id);

IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Mc9Id)
    INSERT INTO [dbo].[ArcadeMachine] ([Id], [Name], [GameName], [CategorieId]) VALUES (@Mc9Id, N'3DTek', 'Tekken 3', @Ctgr2Id);

IF NOT EXISTS (SELECT 1 FROM [dbo].[ArcadeMachine] WHERE [Id] = @Mc10Id)
    INSERT INTO [dbo].[ArcadeMachine] ([Id], [Name], [GameName], [CategorieId]) VALUES (@Mc10Id, N'Segagger', 'Frogger', @Ctgr3Id);

-- Liaison Room / Machine
DECLARE @Now DATETIME2 = SYSDATETIME();

IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] WHERE [RoomId] = @RmId AND [ArcadeMachineId] = @Mc1Id)
    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId], [State], [InstallationDate]) VALUES (@RmId, @Mc1Id, 'Active', @Now);

IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] WHERE [RoomId] = @RmId AND [ArcadeMachineId] = @Mc2Id)
    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId], [State], [InstallationDate]) VALUES (@RmId, @Mc2Id, 'Maintenance', @Now);

IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] WHERE [RoomId] = @RmId AND [ArcadeMachineId] = @Mc3Id)
    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId], [State], [InstallationDate]) VALUES (@RmId, @Mc3Id, 'Active', @Now);

IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] WHERE [RoomId] = @RmId AND [ArcadeMachineId] = @Mc4Id)
    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId], [State], [InstallationDate]) VALUES (@RmId, @Mc4Id, 'Active', @Now);

IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] WHERE [RoomId] = @RmId AND [ArcadeMachineId] = @Mc5Id)
    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId], [State], [InstallationDate]) VALUES (@RmId, @Mc5Id, 'Active', @Now);

IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] WHERE [RoomId] = @RmId AND [ArcadeMachineId] = @Mc6Id)
    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId], [State], [InstallationDate]) VALUES (@RmId, @Mc6Id, 'Maintenance', @Now);

IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] WHERE [RoomId] = @RmId AND [ArcadeMachineId] = @Mc7Id)
    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId], [State], [InstallationDate]) VALUES (@RmId, @Mc7Id, 'Active', @Now);

IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] WHERE [RoomId] = @RmId AND [ArcadeMachineId] = @Mc8Id)
    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId], [State], [InstallationDate]) VALUES (@RmId, @Mc8Id, 'Active', @Now);

IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] WHERE [RoomId] = @RmId AND [ArcadeMachineId] = @Mc9Id)
    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId], [State], [InstallationDate]) VALUES (@RmId, @Mc9Id, 'Active', @Now);

IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomArcadeMachine] WHERE [RoomId] = @RmId AND [ArcadeMachineId] = @Mc10Id)
    INSERT INTO [dbo].[RoomArcadeMachine] ([RoomId], [ArcadeMachineId], [State], [InstallationDate]) VALUES (@RmId, @Mc10Id, 'Active', @Now);

-- Feedbacks
IF NOT EXISTS (SELECT 1 FROM [dbo].[RoomFeedback] WHERE [Username] = N'GamerX_2026' AND [RoomId] = @RmId)
BEGIN
    INSERT INTO [dbo].[RoomFeedback] ([Id], [Stars], [CommentDate], [Comment], [Username], [RoomId])
    VALUES (NEWID(), 5, SYSDATETIME(), N'Super ambiance en ce début 2026 !', N'GamerX_2026', @RmId);
END
