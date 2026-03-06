CREATE PROCEDURE [dbo].[SP_Building_Insert]
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

    IF (@name IS NULL OR LTRIM(@name) = '') OR
       (@openingHour IS NULL) OR
       (@closingHour IS NULL) OR
       (@addressStreet IS NULL OR LTRIM(@addressStreet) = '') OR
       (@addressNumber IS NULL OR LTRIM(@addressNumber) = '') OR
       (@postalCode IS NULL OR LTRIM(@postalCode) = '') OR
       (@city IS NULL OR LTRIM(@city) = '') OR
       (@country IS NULL OR LTRIM(@country) = '')
    BEGIN
        RAISERROR('Tous les champs obligatoires doivent être renseignés.', 16, 1);
        RETURN;
    END

    IF @openingHour >= @closingHour
    BEGIN
        RAISERROR('L''heure d''ouverture doit être antérieure à l''heure de fermeture.', 16, 1);
        RETURN;
    END

    INSERT INTO [dbo].[Building] 
        ([Name], [OpeningHour], [ClosingHour], [Address_Street], [Address_Number], [PostalCode], [City], [Country], [ManagerId])
    VALUES 
        (@name, @openingHour, @closingHour, @addressStreet, @addressNumber, @postalCode, @city, @country, @managerId);
END