CREATE PROCEDURE [dbo].[SP_Booking_Delete]
    @bookingId UNIQUEIDENTIFIER,
    @accountId UNIQUEIDENTIFIER,
    @role VARCHAR(8)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [dbo].[Booking]
    WHERE [Id] = @bookingId
      AND [EndDate] < SYSDATETIME()
      AND (
          @role = 'Admin'
          OR [AccountId] = @accountId
      );

    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR('Action impossible : droits insuffisants ou condition de date non remplie.', 16, 1);
    END
END
