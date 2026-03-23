CREATE PROCEDURE [dbo].[SP_Room_Get_All]
	@buildingId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		R.[Id] as [RoomId],
		R.[Name] as [RoomName],
		R.[Number],
		R.[MachineCapacity],
		R.[Price],
		R.[BuildingId],
		B.[Name] as [BuildingName]
	FROM [dbo].[Room] R, [dbo].[Building] B
	WHERE [BuildingId] = @buildingId AND R.[BuildingId] = B.[Id]
	ORDER BY [Number] ASC;
END
