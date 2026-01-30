CREATE PROCEDURE [dbo].[SP_Booking_Get]
	@bookingId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		-- Informations de la réservation
		B.[Id] AS BookingId,
		B.[BookingDate],
		B.[BeginHour],
		B.[EndHour],
		B.[Status],
		B.[Price] AS BookingPrice,

		-- Informations de la salle (Room)
		R.[Name] AS RoomName,
		R.[Number] AS RoomNumber,

		-- Informations du bâtiment (Building)
		BLD.[Name] AS BuildingName,
		BLD.[City] AS BuildingCity,

		-- Informations des machines d'arcade (ArcadeMachine)
		AM.[Name] AS MachineName,
		AM.[GameName] AS GameName,

		-- Feedback : Uniquement celui de l'utilisateur lié au booking
		RF.[Id] AS FeedbackId,
		RF.[Stars] AS FeedbackStars,
		RF.[Comment] AS FeedbackComment,
		RF.[CommentDate] AS FeedbackDate

	FROM [dbo].[Booking] B
	INNER JOIN [dbo].[Room] R ON B.[RoomId] = R.[Id]
	INNER JOIN [dbo].[Building] BLD ON R.[BuildingId] = BLD.[Id]
	-- Récupération des machines
	LEFT JOIN [dbo].[RoomArcadeMachine] RAM ON R.[Id] = RAM.[RoomId]
	LEFT JOIN [dbo].[ArcadeMachine] AM ON RAM.[ArcadeMachineId] = AM.[Id]
	-- Jointure sur Account pour faire le lien avec le Feedback
	LEFT JOIN [dbo].[Account] A ON B.[AccountId] = A.[Id]
	-- Récupération du commentaire : seulement si le Username correspond 
	-- et que c'est bien pour cette salle
	LEFT JOIN [dbo].[RoomFeedback] RF ON R.[Id] = RF.[RoomId] 
	                                AND A.[Username] = RF.[Username]
	WHERE B.[Id] = @bookingId;
END