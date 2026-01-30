CREATE PROCEDURE [dbo].[SP_Booking_Update_Status]
	AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Booking]
	SET [Status] = 'Finished'
	WHERE [Status] <> 'Finished'
	  AND DATEADD(second, DATEDIFF(second, 0, CAST([EndHour] AS TIME)), CAST([BookingDate] AS DATETIME2)) < SYSDATETIME();
END
