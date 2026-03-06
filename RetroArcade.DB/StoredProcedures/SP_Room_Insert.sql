CREATE PROCEDURE [dbo].[SP_Room_Insert]
    @name NVARCHAR(50),
    @number INT,
    @machineCapacity INT,
    @price DECIMAL(10,2),
    @buildingId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF (@name IS NULL OR LTRIM(@name) = '') OR (@number IS NULL) OR 
       (@machineCapacity IS NULL) OR (@price IS NULL) OR (@buildingId IS NULL)
    BEGIN
        RAISERROR('Tous les champs obligatoires doivent être renseignés.', 16, 1);
        RETURN;
    END

    IF @number < 1 OR @number > 25
    BEGIN
        RAISERROR('Le numéro de salle doit être compris entre 1 et 25.', 16, 1);
        RETURN;
    END

    IF @machineCapacity <> 10
    BEGIN
        RAISERROR('La capacité machine doit être exactement de 10.', 16, 1);
        RETURN;
    END

    IF @price < 0
    BEGIN
        RAISERROR('Le prix ne peut pas être négatif.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Building] WHERE [Id] = @buildingId)
    BEGIN
        RAISERROR('Le bâtiment spécifié n''existe pas.', 16, 1);
        RETURN;
    END

    INSERT INTO [dbo].[Room] ([Name], [Number], [MachineCapacity], [Price], [BuildingId])
    VALUES (@name, @number, @machineCapacity, @price, @buildingId);
END