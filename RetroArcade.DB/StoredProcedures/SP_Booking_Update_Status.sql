CREATE PROCEDURE [dbo].[SP_Booking_Update_Status]
	@status NVARCHAR(16)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Booking]
	SET [Status] = @status
	WHERE [Status] <> @status
	  AND [EndDate] < SYSDATETIME();
END
