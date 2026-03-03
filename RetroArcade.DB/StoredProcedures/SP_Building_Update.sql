CREATE PROCEDURE [dbo].[SP_Building_Update]
    @BuildingId UNIQUEIDENTIFIER,
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

    IF @BuildingId IS NULL OR NOT EXISTS (SELECT 1 FROM [dbo].[Building] WHERE [Id] = @BuildingId)
    BEGIN
        RAISERROR('Identifiant invalide ou bâtiment inexistant.', 16, 1);
        RETURN;
    END

    IF (@Name IS NULL OR LTRIM(@Name) = '') OR
       (@OpeningHour IS NULL) OR
       (@ClosingHour IS NULL) OR
       (@Address_Street IS NULL OR LTRIM(@Address_Street) = '') OR
       (@Address_Number IS NULL OR LTRIM(@Address_Number) = '') OR
       (@PostalCode IS NULL OR LTRIM(@PostalCode) = '') OR
       (@City IS NULL OR LTRIM(@City) = '') OR
       (@Country IS NULL OR LTRIM(@Country) = '')
    BEGIN
        RAISERROR('Les données de mise à jour ne peuvent pas être nulles ou vides.', 16, 1);
        RETURN;
    END

    UPDATE [dbo].[Building]
    SET [Name] = @Name,
        [OpeningHour] = @OpeningHour,
        [ClosingHour] = @ClosingHour,
        [Address_Street] = @Address_Street,
        [Address_Number] = @Address_Number,
        [PostalCode] = @PostalCode,
        [City] = @City,
        [Country] = @Country,
        [ManagerId] = @ManagerId
    WHERE [Id] = @BuildingId;
END