CREATE PROCEDURE [dbo].[SP_Account_Update]
    @accountId UNIQUEIDENTIFIER,
    @firstname NVARCHAR(32),
    @lastname NVARCHAR(32),
    @username NVARCHAR(32),
    @role VARCHAR(8)
AS
BEGIN
    SET NOCOUNT ON;

    -- Récupération des anciennes données (avant mise à jour)
    DECLARE @oldRole VARCHAR(8);
    DECLARE @email NVARCHAR(320);
    
    SELECT @oldRole = [Role], @email = [Email] 
    FROM [dbo].[Account] 
    WHERE [Id] = @accountId;

    IF @email IS NULL RETURN;

    UPDATE [dbo].[Account]
    SET [Lastname] = TRIM(@lastname),
        [Firstname] = TRIM(@firstname),
        [Username] = TRIM(@username),
        [Role] = TRIM(@role)
    WHERE [Id] = @accountId
      AND @lastname IS NOT NULL AND TRIM(@lastname) <> ''
      AND @firstname IS NOT NULL AND TRIM(@firstname) <> ''
      AND @username IS NOT NULL AND TRIM(@username) <> ''
      AND @role IS NOT NULL AND TRIM(@role) <> '';

    -- Cas A : Devient Manager (n'était pas manager avant)
    IF (TRIM(@role) = 'Manager' AND @oldRole <> 'Manager')
    BEGIN
        INSERT INTO [dbo].[Manager] ([Lastname], [Firstname], [Username], [Email])
        VALUES (TRIM(@lastname), TRIM(@firstname), TRIM(@username), @email);
    END
    -- Cas B : Était et reste Manager (Mise à jour des infos)
    ELSE IF (TRIM(@role) = 'Manager' AND @oldRole = 'Manager')
    BEGIN
        UPDATE [dbo].[Manager]
        SET [Lastname] = TRIM(@lastname),
            [Firstname] = TRIM(@firstname),
            [Username] = TRIM(@username)
        WHERE [Email] = @email;
    END
    -- Cas C : Perd le rôle Manager (Suppression de l'enregistrement)
    ELSE IF (TRIM(@role) <> 'Manager' AND @oldRole = 'Manager')
    BEGIN
        DELETE FROM [dbo].[Manager] WHERE [Email] = @email;
    END
END