CREATE PROCEDURE [dbo].[SP_Manager_Update]
	@Id UNIQUEIDENTIFIER,
    @Lastname NVARCHAR(32),
    @Firstname NVARCHAR(32),
    @Username NVARCHAR(32),
    @Email NVARCHAR(320)
AS
BEGIN
    SET NOCOUNT ON;

    IF @Id IS NULL OR NOT EXISTS (SELECT 1 FROM [dbo].[Manager] WHERE [Id] = @Id)
    BEGIN
        RAISERROR('Manager introuvable.', 16, 1);
        RETURN;
    END

    IF (@Lastname IS NULL OR LTRIM(@Lastname) = '') OR
       (@Firstname IS NULL OR LTRIM(@Firstname) = '') OR
       (@Username IS NULL OR LTRIM(@Username) = '') OR
       (@Email IS NULL OR LTRIM(@Email) = '')
    BEGIN
        RAISERROR('Les champs ne peuvent pas être vides.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM [dbo].[Manager] WHERE [Username] = @Username AND [Id] <> @Id)
    BEGIN
        RAISERROR('Ce nom d''utilisateur est déjà pris par un autre manager.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM [dbo].[Manager] WHERE [Email] = @Email AND [Id] <> @Id)
    BEGIN
        RAISERROR('Cet email est déjà utilisé par un autre manager.', 16, 1);
        RETURN;
    END

    UPDATE [dbo].[Manager]
    SET [Lastname] = @Lastname,
        [Firstname] = @Firstname,
        [Username] = @Username,
        [Email] = @Email
    WHERE [Id] = @Id;
END
