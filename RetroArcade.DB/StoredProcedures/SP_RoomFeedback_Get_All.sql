CREATE PROCEDURE [dbo].[SP_RoomFeedback_Get_All]
	@accountId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		RF.[Id] AS FeedbackId,
		RF.[Stars],
		RF.[CommentDate],
		RF.[Comment],
		RF.[Username]

	FROM [dbo].[RoomFeedback] RF
	INNER JOIN [dbo].[Account] A ON RF.[Username] = A.[Username]
	WHERE A.[Id] = @accountId
	ORDER BY RF.[CommentDate] DESC;
END
