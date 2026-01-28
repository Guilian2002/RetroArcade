CREATE PROCEDURE [dbo].[SP_Booking_Insert]
	@bookingDate DATE,
	@beginHour TIME,
	@endHour TIME,
	@groupSize INT,
	@price DECIMAL(10,2),
	@status NVARCHAR(16),
	@roomId UNIQUEIDENTIFIER,
	@accountId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	-- 1. Vérification si un booking existe déjà pour cette salle sur ce créneau
	-- La logique : (NouveauBegin < ExistantEnd) ET (NouveauEnd > ExistantBegin)
	IF EXISTS (
		SELECT 1 
		FROM [dbo].[Booking]
		WHERE [RoomId] = @roomId
		  AND [BookingDate] = @bookingDate
		  AND @beginHour < [EndHour] 
		  AND @endHour > [BeginHour]
	)
	BEGIN
		RAISERROR('La salle est déjà réservée pour ce créneau horaire.', 16, 1);
		RETURN;
	END

	-- 2. Si le créneau est libre, on procède à l'insertion
	INSERT INTO [dbo].[Booking] (
		[Id], 
		[BookingDate], 
		[BeginHour], 
		[EndHour], 
		[GroupSize], 
		[Price], 
		[Status], 
		[RoomId], 
		[AccountId]
	)
	VALUES (
		NEWID(),
		@bookingDate,
		@beginHour,
		@endHour,
		@groupSize,
		@price,
		@status,
		@roomId,
		@accountId
	);
END
