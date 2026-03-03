CREATE PROCEDURE [dbo].[SP_Manager_Insert]
	@Lastname NVARCHAR(32),
    @Firstname NVARCHAR(32),
    @Username NVARCHAR(32),
    @Email NVARCHAR(320)
AS
BEGIN
    SET NOCOUNT ON;

    IF (@Lastname IS NULL OR LTRIM(@Lastname) = '') OR
       (@Firstname IS NULL OR LTRIM(@Firstname) = '') OR
       (@Username IS NULL OR LTRIM(@Username) = '') OR
       (@Email IS NULL OR LTRIM(@Email) = '')
    BEGIN
        RAISERROR('Tous les champs (Nom, Prénom, Username, Email) sont obligatoires.', 16, 1);
        RETURN;
    END

    IF @Email NOT LIKE '%_@__%.__%'
    BEGIN
        RAISERROR('Le format de l''adresse email est invalide.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM [dbo].[Manager] WHERE [Username] = @Username)
    BEGIN
        RAISERROR('Ce nom d''utilisateur est déjà utilisé.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM [dbo].[Manager] WHERE [Email] = @Email)
    BEGIN
        RAISERROR('Cette adresse email est déjà enregistrée.', 16, 1);
        RETURN;
    END

    INSERT INTO [dbo].[Manager] ([Lastname], [Firstname], [Username], [Email])
    VALUES (@Lastname, @Firstname, @Username, @Email);
END
