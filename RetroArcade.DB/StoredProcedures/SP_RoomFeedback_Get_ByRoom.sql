CREATE PROCEDURE [dbo].[SP_RoomFeedback_Get_ByRoom]
	@roomId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		[Id],
		[Stars],
		[CommentDate],
		[Comment],
		[Username],
		[RoomId]
	FROM [dbo].[RoomFeedback]
	WHERE [RoomId] = @roomId
	ORDER BY [CommentDate] DESC;
END