CREATE PROCEDURE [dbo].[SP_Booking_Update]
	@bookingId UNIQUEIDENTIFIER,
	@groupSize INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Booking]
	SET [GroupSize] = @groupSize
	WHERE [Id] = @bookingId
	  AND @groupSize BETWEEN 4 AND 10;
END
