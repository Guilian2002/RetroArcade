CREATE PROCEDURE [dbo].[SP_Booking_Get_All]
	@accountId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		[Id],
		[BeginDate],
		[EndDate],
		[GroupSize],
		[Price],
		[Status],
		[RoomId]
	FROM [dbo].[Booking]
	WHERE [AccountId] = @accountId
	ORDER BY [EndDate] DESC;
END
