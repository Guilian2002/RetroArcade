CREATE PROCEDURE [dbo].[SP_Building_Insert]
    @Name NVARCHAR(64),
    @OpeningHour TIME,
    @ClosingHour TIME,
    @Address_Street NVARCHAR(256),
    @Address_Number NVARCHAR(12),
    @PostalCode NVARCHAR(16),
    @City NVARCHAR(50),
    @Country NVARCHAR(50),
    @ManagerId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

    IF (@Name IS NULL OR LTRIM(@Name) = '') OR
       (@OpeningHour IS NULL) OR
       (@ClosingHour IS NULL) OR
       (@Address_Street IS NULL OR LTRIM(@Address_Street) = '') OR
       (@Address_Number IS NULL OR LTRIM(@Address_Number) = '') OR
       (@PostalCode IS NULL OR LTRIM(@PostalCode) = '') OR
       (@City IS NULL OR LTRIM(@City) = '') OR
       (@Country IS NULL OR LTRIM(@Country) = '')
    BEGIN
        RAISERROR('Tous les champs obligatoires doivent être renseignés.', 16, 1);
        RETURN;
    END

    IF @OpeningHour >= @ClosingHour
    BEGIN
        RAISERROR('L''heure d''ouverture doit être antérieure à l''heure de fermeture.', 16, 1);
        RETURN;
    END

    INSERT INTO [dbo].[Building] 
        ([Name], [OpeningHour], [ClosingHour], [Address_Street], [Address_Number], [PostalCode], [City], [Country], [ManagerId])
    VALUES 
        (@Name, @OpeningHour, @ClosingHour, @Address_Street, @Address_Number, @PostalCode, @City, @Country, @ManagerId);

END