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

	IF @bookingDate IS NULL OR @beginHour IS NULL OR @endHour IS NULL 
	   OR @groupSize IS NULL OR @price IS NULL OR @status IS NULL 
	   OR @roomId IS NULL OR @accountId IS NULL
	BEGIN
		RAISERROR('Tous les champs sont obligatoires. Aucune valeur ne peut être nulle.', 16, 1);
		RETURN;
	END

	IF @groupSize < 4 OR @groupSize > 10
	BEGIN
		RAISERROR('La taille du groupe doit être comprise entre 4 et 10 personnes.', 16, 1);
		RETURN;
	END

	IF @beginHour >= @endHour
	BEGIN
		RAISERROR('L''heure de début doit être strictement antérieure à l''heure de fin.', 16, 1);
		RETURN;
	END

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
