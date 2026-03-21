CREATE PROCEDURE [dbo].[SP_Booking_Get_By_Manager]
    @accountId UNIQUEIDENTIFIER,
    @role VARCHAR(8)
AS
BEGIN
    SET NOCOUNT ON;

    EXEC [dbo].[SP_Booking_Update_Status];

    BEGIN TRY
        IF @accountId IS NULL OR @role IS NULL
        BEGIN
            RAISERROR('Paramètres manquants.', 16, 1);
            RETURN;
        END

        SELECT 
            BK.[Id] AS BookingId,
            BK.[BeginDate] AS BookingBeginDate,
            BK.[EndDate] AS BookingEndDate,
            BK.[GroupSize] AS BookingGroupSize,
            BK.[Price] AS BookingPrice,
            BK.[Status] AS BookingStatus,
            BK.[RoomId] AS RoomId
        FROM [dbo].[Booking] BK
        INNER JOIN [dbo].[Room] R ON BK.[RoomId] = R.[Id]
        INNER JOIN [dbo].[Building] B ON R.[BuildingId] = B.[Id]
        INNER JOIN [dbo].[Manager] M ON B.[ManagerId] = M.[Id]
        INNER JOIN [dbo].[Account] A ON M.[Username] = A.[Username] 
        WHERE @role = 'Manager' 
          AND A.[Id] = @accountId
        ORDER BY BK.[EndDate] DESC;

        IF @@ROWCOUNT = 0 AND @role <> 'Manager'
        BEGIN
             RAISERROR('Rôle non reconnu.', 16, 1);
        END

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
