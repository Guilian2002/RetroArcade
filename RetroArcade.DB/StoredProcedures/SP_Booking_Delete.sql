CREATE PROCEDURE [dbo].[SP_Booking_Delete]
	@bookingId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [dbo].[Booking]
	WHERE [Id] = @bookingId
	  AND DATETIME2FROMPARTS(
			YEAR([BookingDate]), MONTH([BookingDate]), DAY([BookingDate]), 
			DATEPART(HOUR, [BeginHour]), DATEPART(MINUTE, [BeginHour]), 0, 0, 0
		  ) > SYSDATETIME();
END
