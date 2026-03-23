CREATE PROCEDURE [dbo].[SP_Booking_Get]
    @bookingId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    EXEC [dbo].[SP_Booking_Update_Status];

    SELECT 
        B.[Id] AS BookingId,
        B.[BeginDate] AS BookingBeginDate,
        B.[EndDate] AS BookingEndDate,
        B.[Status] AS BookingStatus,
        B.[Price] AS BookingPrice,
        B.[GroupSize] AS BookingGroupSize,

        R.[Name] AS RoomName,
        R.[Number] AS RoomNumber,

        BLD.[Name] AS BuildingName,
        BLD.[City] AS BuildingCity
    FROM [dbo].[Booking] B
    INNER JOIN [dbo].[Room] R ON B.[RoomId] = R.[Id]
    INNER JOIN [dbo].[Building] BLD ON R.[BuildingId] = BLD.[Id]
    WHERE B.[Id] = @bookingId;
END