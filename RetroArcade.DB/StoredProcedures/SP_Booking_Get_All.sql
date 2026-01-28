CREATE PROCEDURE [dbo].[SP_Booking_Get_All]
	@accountId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		[Id],
		[BookingDate],
		[BeginHour],
		[EndHour],
		[GroupSize],
		[Price],
		[Status],
		[RoomId]
	FROM [dbo].[Booking]
	WHERE [AccountId] = @accountId
	ORDER BY [BookingDate] DESC, [BeginHour] DESC;
END
