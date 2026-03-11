CREATE PROCEDURE [dbo].[SP_RoomFeedback_Insert]
	@stars INT,
	@comment NVARCHAR(256),
	@accountId UNIQUEIDENTIFIER,
	@roomId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	-- 1. Récupération du Username
	DECLARE @username NVARCHAR(64);
	
	SELECT @username = [Username] 
	FROM [dbo].[Account] 
	WHERE [Id] = @accountId;

	IF @username IS NULL
	BEGIN
		RAISERROR('Compte introuvable.', 16, 1);
		RETURN;
	END

	-- 2. Calcul du nombre de réservations effectuées par l'utilisateur pour cette salle
	DECLARE @bookingCount INT;
	SELECT @bookingCount = COUNT(*) 
	FROM [dbo].[Booking] 
	WHERE [AccountId] = @accountId AND [RoomId] = @roomId;

	-- 3. Calcul du nombre de commentaires déjà postés par cet utilisateur pour cette salle
	DECLARE @feedbackCount INT;
	SELECT @feedbackCount = COUNT(*) 
	FROM [dbo].[RoomFeedback] 
	WHERE [Username] = @username AND [RoomId] = @roomId;

	-- 4. Vérification du droit de commenter (1 réservation = 1 droit de commentaire)
	IF (@bookingCount - @feedbackCount) <= 0
	BEGIN
		RAISERROR('Vous n''avez pas de réservation disponible pour poster un nouveau commentaire sur cette salle.', 16, 1);
		RETURN;
	END

	-- 5. Insertion du feedback
	INSERT INTO [dbo].[RoomFeedback] (
		[Id],
		[Stars],
		[CommentDate],
		[Comment],
		[Username],
		[RoomId]
	)
	VALUES (
		NEWID(),
		@stars,
		SYSDATETIME(),
		@comment,
		@username,
		@roomId
	);
END