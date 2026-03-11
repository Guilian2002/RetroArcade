CREATE PROCEDURE [dbo].[SP_Room_Update]
    @roomId UNIQUEIDENTIFIER,
    @name NVARCHAR(50),
    @number INT,
    @machineCapacity INT,
    @price DECIMAL(10,2),
    @buildingId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF @roomId IS NULL OR NOT EXISTS (SELECT 1 FROM [dbo].[Room] WHERE [Id] = @roomId)
    BEGIN
        RAISERROR('Identifiant de salle invalide ou inexistant.', 16, 1);
        RETURN;
    END

    IF (@name IS NULL OR LTRIM(@name) = '') OR (@number IS NULL) OR 
       (@machineCapacity IS NULL) OR (@price IS NULL) OR (@buildingId IS NULL)
    BEGIN
        RAISERROR('Les données de mise à jour ne peuvent pas être nulles ou vides.', 16, 1);
        RETURN;
    END

    IF @number < 1 OR @number > 25 OR @machineCapacity <> 10
    BEGIN
        RAISERROR('Contraintes de numéro (1-25) ou de capacité (10) non respectées.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Building] WHERE [Id] = @buildingId)
    BEGIN
        RAISERROR('Le bâtiment référencé n''existe pas.', 16, 1);
        RETURN;
    END

    UPDATE [dbo].[Room]
    SET [Name] = @name,
        [Number] = @number,
        [MachineCapacity] = @machineCapacity,
        [Price] = @price,
        [BuildingId] = @buildingId
    WHERE [Id] = @roomId;
END
