CREATE PROCEDURE [dbo].[SP_Booking_Insert]
	@beginDate DATETIME2,
	@endDate DATETIME2,
	@groupSize INT,
	@price DECIMAL(10,2),
	@status NVARCHAR(16),
	@roomId UNIQUEIDENTIFIER,
	@accountId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	IF @beginDate IS NULL OR @endDate IS NULL 
	   OR @groupSize IS NULL OR @price IS NULL OR @status IS NULL 
	   OR @roomId IS NULL OR @accountId IS NULL
	BEGIN
		RAISERROR('Tous les champs sont obligatoires. Aucune valeur ne peut être nulle.', 16, 1);
		RETURN;
	END

	DECLARE @opening TIME, @closing TIME, @roomPrice DECIMAL(10,2);

	SELECT @opening = B.OpeningHour, @closing = B.ClosingHour
	FROM [dbo].[Building] B
	JOIN [dbo].[Room] R ON B.Id = R.BuildingId
	WHERE R.Id = @roomId;

	SELECT @roomPrice = R.Price
	FROM [dbo].[Room] R
	WHERE R.Id = @roomId;

	IF CAST(@beginDate AS TIME) < @opening OR CAST(@endDate AS TIME) > @closing
	BEGIN
		RAISERROR('La réservation doit être comprise dans les heures d''ouverture du bâtiment.', 16, 1);
		RETURN;
	END

	IF @price > @roomPrice OR @price < @roomPrice
	BEGIN
		RAISERROR('Le prix de la réservation doit être égal au prix de la salle.', 16, 1);
		RETURN;
	END

	IF @groupSize < 4 OR @groupSize > 10
	BEGIN
		RAISERROR('La taille du groupe doit être comprise entre 4 et 10 personnes.', 16, 1);
		RETURN;
	END

	IF @beginDate >= @endDate
	BEGIN
		RAISERROR('L''heure de début doit être strictement antérieure à l''heure de fin.', 16, 1);
		RETURN;
	END

	IF EXISTS (
		SELECT 1
		FROM [dbo].[Booking]
		WHERE [RoomId] = @roomId
		  AND @beginDate < [EndDate] 
		  AND @endDate > [BeginDate]
	)
	BEGIN
		RAISERROR('La salle est déjà réservée pour ce créneau horaire.', 16, 1);
		RETURN;
	END

	INSERT INTO [dbo].[Booking] (
		[Id],  
		[BeginDate], 
		[EndDate], 
		[GroupSize], 
		[Price], 
		[Status], 
		[RoomId], 
		[AccountId]
	)
	VALUES (
		NEWID(),
		@beginDate,
		@endDate,
		@groupSize,
		@price,
		@status,
		@roomId,
		@accountId
	);
END
