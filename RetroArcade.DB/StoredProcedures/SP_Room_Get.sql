CREATE PROCEDURE [dbo].[SP_Room_Get]
	@roomId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		-- Détails de la Salle (Room)
		R.[Id] AS RoomId,
		R.[Name] AS RoomName,
		R.[Number] AS RoomNumber,
		R.[MachineCapacity] AS RoomMachineCapacity,
		R.[Price] AS RoomPrice,

		-- Détails du Bâtiment (Building)
		B.[Id] AS BuildingId,
		B.[Name] AS BuildingName,
		B.[OpeningHour] AS BuildingOpeningHour,
		B.[ClosingHour] AS BuildingClosingHour,
		B.[Address_Street] AS BuildingStreet,
		B.[Address_Number] AS BuildingNumber,
		B.[PostalCode] AS BuildingPostalCode,
		B.[City] AS BuildingCity,
		B.[Country] AS BuildingCountry,

		
		-- Détails du Manager (Manager)
		M.[Id] AS ManagerId,
		M.[Lastname] AS ManagerLastname,
		M.[Firstname] AS ManagerFirstname,
		M.[Username] AS ManagerUsername,
		M.[Email] AS ManagerEmail,

		-- Détails des Machines en activité (RoomArcadeMachine)
		RAM.[State] AS ArcadeMachineState,
		RAM.[InstallationDate] AS ArcadeMachineInstallationDate,

		-- Détails des Machines (ArcadeMachine)
		AM.[Id] AS MachineId,
		AM.[Name] AS MachineName,
		AM.[GameName] AS MachineGameName,

		-- Détails de la Catégorie (Categorie)
		C.[Id] AS CategorieId,
		C.[Name] AS CategorieName,

		-- Détails des commentaires de la pièce (RoomFeedback)
		RF.[Id] AS RoomFeedbackId,
		RF.[Stars] AS RoomFeedbackStars,
		RF.[CommentDate] AS RoomFeedbackCommentDate,
		RF.[Comment] AS RoomFeedbackComment,
		RF.[Username] AS RoomFeedbackUsername

	FROM [dbo].[Room] R
	LEFT JOIN [RoomFeedback] RF ON R.[Id] = RF.[RoomId]
	INNER JOIN [dbo].[Building] B ON R.[BuildingId] = B.[Id]
	LEFT JOIN [dbo].[Manager] M ON B.[ManagerId] = M.[Id]
	LEFT JOIN [dbo].[RoomArcadeMachine] RAM ON R.[Id] = RAM.[RoomId]
	LEFT JOIN [dbo].[ArcadeMachine] AM ON RAM.[ArcadeMachineId] = AM.[Id]
	LEFT JOIN [dbo].[Categorie] C ON AM.[CategorieId] = C.[Id]
	WHERE R.[Id] = @roomId;
END