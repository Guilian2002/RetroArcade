CREATE PROCEDURE [dbo].[SP_Booking_Delete]
	@bookingId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [dbo].[Booking]
	WHERE [Id] = @bookingId
	  AND [EndDate] < SYSDATETIME();
END
