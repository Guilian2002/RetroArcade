CREATE PROCEDURE [dbo].[SP_Room_Get]
	@roomId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		-- Détails de la Salle (Room)
		R.[Id] AS RoomId,
		R.[Name] AS RoomName,
		R.[Number] AS RoomNumber,
		R.[MachineCapacity],
		R.[Price] AS RoomPrice,

		-- Détails du Bâtiment (Building)
		B.[Id] AS BuildingId,
		B.[Name] AS BuildingName,
		B.[Address_Street],
		B.[Address_Number],
		B.[City],
		B.[PostalCode],

		-- Détails des Machines (ArcadeMachine)
		AM.[Id] AS MachineId,
		AM.[Name] AS MachineName,
		AM.[State] AS MachineState,
		AM.[GameName] AS GameName

	FROM [dbo].[Room] R
	INNER JOIN [dbo].[Building] B ON R.[BuildingId] = B.[Id]
	LEFT JOIN [dbo].[RoomArcadeMachine] RAM ON R.[Id] = RAM.[RoomId]
	LEFT JOIN [dbo].[ArcadeMachine] AM ON RAM.[ArcadeMachineId] = AM.[Id]
	WHERE R.[Id] = @roomId;
END