CREATE PROCEDURE [dbo].[SP_Building_Get]
	@buildingId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		-- Détails du bâtiment
		B.[Id] AS BuildingId,
		B.[Name] AS BuildingName,
		B.[OpeningHour] AS BuildingOpeningHour,
		B.[ClosingHour] AS BuildingClosingHour,
		B.[Address_Street] AS BuildingStreet,
		B.[Address_Number] AS BuildingNumber,
		B.[PostalCode] AS BuildingPostalCode,
		B.[City] AS BuildingCity,
		B.[Country] AS BuildingCountry,

		-- Détails des salles liées
		R.[Id] AS RoomId,
		R.[Name] AS RoomName,
		R.[Number] AS RoomNumber,
		R.[MachineCapacity] AS RoomMachineCapacity,
		R.[Price] AS RoomPrice,

		-- Détails du manager lié
		M.[Id] AS ManagerId,
		M.[Lastname] AS ManagerLastname,
		M.[Firstname] AS ManagerFirstname,
		M.[Username] AS ManagerUsername,
		M.[Email] AS ManagerEmail

	FROM [dbo].[Building] B
	LEFT JOIN [dbo].[Room] R ON B.[Id] = R.[BuildingId]
	LEFT JOIN [dbo].[Manager] M ON B.[ManagerId] = M.[Id]
	WHERE B.[Id] = @buildingId;
END
