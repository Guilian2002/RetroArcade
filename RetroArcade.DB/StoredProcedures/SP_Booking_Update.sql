CREATE PROCEDURE [dbo].[SP_Booking_Update]
    @bookingId UNIQUEIDENTIFIER,
    @groupSize INT,
    @accountId UNIQUEIDENTIFIER,
    @role VARCHAR(8)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Booking]
    SET [GroupSize] = @groupSize
    WHERE [Id] = @bookingId
      AND @groupSize BETWEEN 4 AND 10
      AND (
          @role = 'Admin'
          OR [AccountId] = @accountId
      );

    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR('Mise à jour impossible : droits insuffisants ou taille de groupe invalide.', 16, 1);
    END
END
