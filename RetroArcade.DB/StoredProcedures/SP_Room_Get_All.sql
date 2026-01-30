CREATE PROCEDURE [dbo].[SP_Room_Get_All]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		[Id],
		[Name],
		[Number],
		[MachineCapacity],
		[Price]
	FROM [dbo].[Room]
	ORDER BY [Number] ASC;
END
