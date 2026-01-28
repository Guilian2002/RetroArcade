CREATE PROCEDURE [dbo].[SP_RoomFeedback_Insert]
	@stars INT,
	@comment NVARCHAR(256),
	@accountId UNIQUEIDENTIFIER,
	@roomId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	-- 1. Récupération du Username correspondant à l'ID du compte
	DECLARE @username NVARCHAR(64);
	
	SELECT @username = [Username] 
	FROM [dbo].[Account] 
	WHERE [Id] = @accountId;

	-- 2. Vérification si le compte existe
	IF @username IS NULL
	BEGIN
		RAISERROR('Compte introuvable.', 16, 1);
		RETURN;
	END

	-- 3. Insertion du feedback
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
