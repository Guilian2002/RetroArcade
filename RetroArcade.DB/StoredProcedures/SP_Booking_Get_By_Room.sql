CREATE PROCEDURE [dbo].[SP_Booking_Get_By_Room]
	@roomId UNIQUEIDENTIFIER
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
	WHERE [RoomId] = @roomId;
END
