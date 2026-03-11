CREATE PROCEDURE [dbo].[SP_Building_Update]
    @buildingId UNIQUEIDENTIFIER,
    @name NVARCHAR(64),
    @openingHour TIME,
    @closingHour TIME,
    @addressStreet NVARCHAR(256),
    @addressNumber NVARCHAR(12),
    @postalCode NVARCHAR(16),
    @city NVARCHAR(50),
    @country NVARCHAR(50),
    @managerId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF @buildingId IS NULL OR NOT EXISTS (SELECT 1 FROM [dbo].[Building] WHERE [Id] = @buildingId)
    BEGIN
        RAISERROR('Identifiant invalide ou bâtiment inexistant.', 16, 1);
        RETURN;
    END

    IF (@name IS NULL OR LTRIM(@name) = '') OR
       (@openingHour IS NULL) OR
       (@closingHour IS NULL) OR
       (@addressStreet IS NULL OR LTRIM(@addressStreet) = '') OR
       (@addressNumber IS NULL OR LTRIM(@addressNumber) = '') OR
       (@postalCode IS NULL OR LTRIM(@postalCode) = '') OR
       (@city IS NULL OR LTRIM(@city) = '') OR
       (@country IS NULL OR LTRIM(@country) = '')
    BEGIN
        RAISERROR('Les données de mise à jour ne peuvent pas être nulles ou vides.', 16, 1);
        RETURN;
    END

    IF @openingHour >= @closingHour
    BEGIN
        RAISERROR('L''heure d''ouverture doit être antérieure à l''heure de fermeture.', 16, 1);
        RETURN;
    END

    UPDATE [dbo].[Building]
    SET [Name] = @name,
        [OpeningHour] = @openingHour,
        [ClosingHour] = @closingHour,
        [Address_Street] = @addressStreet,
        [Address_Number] = @addressNumber,
        [PostalCode] = @postalCode,
        [City] = @city,
        [Country] = @country,
        [ManagerId] = @managerId
    WHERE [Id] = @buildingId;
END