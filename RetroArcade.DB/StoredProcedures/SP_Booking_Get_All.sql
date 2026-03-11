CREATE PROCEDURE [dbo].[SP_Booking_Get_All]
	@accountId UNIQUEIDENTIFIER,
	@role VARCHAR(8)
AS
BEGIN
	SET NOCOUNT ON;

	 BEGIN TRY
        IF @accountId IS NULL OR @role IS NULL
        BEGIN
            RAISERROR('Paramètres manquants.', 16, 1);
            RETURN;
        END

        SELECT 
            [Id] AS BookingId,
            [BeginDate] AS BookingBeginDate,
            [EndDate] AS BookingEndDate,
            [GroupSize] AS BookingGroupSize,
            [Price] AS BookingPrice,
            [Status] AS BookingStatus,
            [RoomId]
        FROM [dbo].[Booking]
        WHERE (@role = 'User' AND [AccountId] = @accountId)
        ORDER BY [EndDate] DESC;

        IF @@ROWCOUNT = 0 AND @role NOT IN ('User')
        BEGIN
             RAISERROR('Rôle non reconnu.', 16, 1);
        END

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
