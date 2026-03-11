CREATE PROCEDURE [dbo].[SP_RoomFeedback_Get_ByRoom]
	@roomId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		[Id] AS FeedbackId,
		[Stars],
		[CommentDate],
		[Comment],
		[Username]
	FROM [dbo].[RoomFeedback]
	WHERE [RoomId] = @roomId
	ORDER BY [CommentDate] DESC;
END