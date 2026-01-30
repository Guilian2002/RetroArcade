CREATE PROCEDURE [dbo].[SP_Building_Get]
	@buildingId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		-- Détails du bâtiment
		B.[Id] AS BuildingId,
		B.[Name] AS BuildingName,
		B.[OpeningHour],
		B.[ClosingHour],
		B.[Address_Street],
		B.[Address_Number],
		B.[PostalCode],
		B.[City],
		B.[Country],

		-- Détails des salles liées
		R.[Id] AS RoomId,
		R.[Name] AS RoomName,
		R.[Number] AS RoomNumber,
		R.[MachineCapacity],
		R.[Price] AS RoomPrice
	FROM [dbo].[Building] B
	LEFT JOIN [dbo].[Room] R ON B.[Id] = R.[BuildingId]
	WHERE B.[Id] = @buildingId;
END
