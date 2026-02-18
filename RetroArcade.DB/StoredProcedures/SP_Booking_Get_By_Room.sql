CREATE PROCEDURE [dbo].[SP_Booking_Get_By_Room]
    @roomId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        B.[Id] AS BookingId,
        B.[BeginDate] AS BookingBeginDate,
        B.[EndDate] AS BookingEndDate,
        B.[GroupSize] AS BookingGroupSize,
        B.[Price] AS BookingPrice,
        B.[Status] AS BookingStatus,

        R.[Id] AS RoomId,
        R.[Name] AS RoomName,
        R.[Number] AS RoomNumber,
        R.[MachineCapacity] AS RoomMachineCapacity,
        R.[Price] AS RoomPrice,

        BU.[Id] AS BuildingId,
        BU.[Name] AS BuildingName,
        BU.[OpeningHour] AS BuildingOpeningHour,
        BU.[ClosingHour] AS BuildingClosingHour,
        BU.[Address_Street] AS BuildingStreet,
        BU.[Address_Number] AS BuildingNumber,
        BU.[PostalCode] AS BuildingPostalCode,
        BU.[City] AS BuildingCity,
        BU.[Country] AS BuildingCountry
    FROM [dbo].[Booking] B
    INNER JOIN [dbo].[Room] R ON B.[RoomId] = R.[Id]
    INNER JOIN [dbo].[Building] BU ON R.[BuildingId] = BU.[Id]
    WHERE B.[RoomId] = @roomId
    ORDER BY B.[BeginDate] DESC;
END