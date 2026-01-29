CREATE PROCEDURE [dbo].[SP_Account_Insert]
    @firstname NVARCHAR(32),
    @lastname NVARCHAR(32),
    @username NVARCHAR(32),
    @email NVARCHAR(320),
    @password NVARCHAR(64),
    @role VARCHAR(8)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        IF @firstname IS NULL OR LEN(TRIM(@firstname)) = 0
            RAISERROR (N'Le prénom est requis.', 16, 1);

        IF @lastname IS NULL OR LEN(TRIM(@lastname)) = 0
            RAISERROR (N'Le nom est requis.', 16, 1);

        IF @username IS NULL OR LEN(TRIM(@username)) = 0
            RAISERROR (N'Le nom d''utilisateur est requis.', 16, 1);

        IF @email IS NULL OR @email NOT LIKE '%_@_%._%'
            RAISERROR (N'L''adresse email n''est pas valide.', 16, 1);

        IF @password IS NULL OR LEN(@password) < 8
            RAISERROR (N'Le mot de passe doit faire au moins 8 caractères.', 16, 1);

        IF EXISTS (SELECT 1 FROM [dbo].[Account] WHERE [Email] = @email)
            RAISERROR (N'Un compte avec cet email existe déjà.', 16, 1);

        IF EXISTS (SELECT 1 FROM [dbo].[Account] WHERE [Username] = @username)
            RAISERROR (N'Ce nom d''utilisateur est déjà utilisé.', 16, 1);

        DECLARE @AccountId UNIQUEIDENTIFIER = NEWID();
        DECLARE @Salt UNIQUEIDENTIFIER = NEWID();

        INSERT INTO [dbo].[Account] ([Id], [Lastname], [Firstname], [Username], [Email], [Role], [CreationDate], [IsActive])
        VALUES (@AccountId, TRIM(@lastname), TRIM(@firstname), TRIM(@username), LOWER(TRIM(@email)), ISNULL(@role, 'User'), CAST(SYSDATETIME() AS DATE), 1);

        INSERT INTO [dbo].[AccountCredential] ([Id], [PasswordHash], [Salt], [AccountId])
        VALUES (NEWID(), [dbo].[SF_HashAndSalt](@password, @Salt), @Salt, @AccountId);

        RETURN 0;
    END TRY
    BEGIN CATCH
        THROW;
        RETURN -1;
    END CATCH
END