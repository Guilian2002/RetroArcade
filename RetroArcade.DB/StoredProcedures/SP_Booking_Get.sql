CREATE PROCEDURE [dbo].[SP_Booking_Get]
	@bookingId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		B.[Id] AS BookingId,
		B.[BeginDate],
		B.[EndDate],
		B.[Status],
		B.[Price] AS BookingPrice,

		R.[Name] AS RoomName,
		R.[Number] AS RoomNumber,

		BLD.[Name] AS BuildingName,
		BLD.[City] AS BuildingCity,

		AM.[Name] AS MachineName,
		AM.[GameName] AS GameName,

		RF.[Id] AS FeedbackId,
		RF.[Stars] AS FeedbackStars,
		RF.[Comment] AS FeedbackComment,
		RF.[CommentDate] AS FeedbackDate

	FROM [dbo].[Booking] B
	INNER JOIN [dbo].[Room] R ON B.[RoomId] = R.[Id]
	INNER JOIN [dbo].[Building] BLD ON R.[BuildingId] = BLD.[Id]

	LEFT JOIN [dbo].[RoomArcadeMachine] RAM ON R.[Id] = RAM.[RoomId]
	LEFT JOIN [dbo].[ArcadeMachine] AM ON RAM.[ArcadeMachineId] = AM.[Id]
	LEFT JOIN [dbo].[Account] A ON B.[AccountId] = A.[Id]

	LEFT JOIN [dbo].[RoomFeedback] RF ON R.[Id] = RF.[RoomId] 
	                                AND A.[Username] = RF.[Username]
	WHERE B.[Id] = @bookingId;
END