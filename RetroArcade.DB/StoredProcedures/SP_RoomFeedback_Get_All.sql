CREATE PROCEDURE [dbo].[SP_RoomFeedback_Get_All]
	@accountId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		-- Détails du Feedback
		RF.[Id] AS FeedbackId,
		RF.[Stars],
		RF.[CommentDate],
		RF.[Comment],
		RF.[Username],

		-- Détails de la Room
		R.[Id] AS RoomId,
		R.[Name] AS RoomName,
		R.[Number] AS RoomNumber,

		-- Détails du Building
		B.[Id] AS BuildingId,
		B.[Name] AS BuildingName,
		B.[City] AS BuildingCity,
		B.[Country] AS BuildingCountry

	FROM [dbo].[RoomFeedback] RF
	INNER JOIN [dbo].[Account] A ON RF.[Username] = A.[Username]
	INNER JOIN [dbo].[Room] R ON RF.[RoomId] = R.[Id]
	INNER JOIN [dbo].[Building] B ON R.[BuildingId] = B.[Id]
	WHERE A.[Id] = @accountId
	ORDER BY RF.[CommentDate] DESC;
END
