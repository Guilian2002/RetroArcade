CREATE PROCEDURE [dbo].[SP_Room_Insert]
	@Name NVARCHAR(50),
    @Number INT,
    @MachineCapacity INT,
    @Price DECIMAL(10,2),
    @BuildingId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF (@Name IS NULL OR LTRIM(@Name) = '') OR (@Number IS NULL) OR 
       (@MachineCapacity IS NULL) OR (@Price IS NULL) OR (@BuildingId IS NULL)
    BEGIN
        RAISERROR('Tous les champs obligatoires doivent être renseignés.', 16, 1);
        RETURN;
    END

    IF @Number < 1 OR @Number > 25
    BEGIN
        RAISERROR('Le numéro de salle doit être compris entre 1 et 25.', 16, 1);
        RETURN;
    END

    IF @MachineCapacity <> 10
    BEGIN
        RAISERROR('La capacité machine doit être exactement de 10.', 16, 1);
        RETURN;
    END

    IF @Price < 0
    BEGIN
        RAISERROR('Le prix ne peut pas être négatif.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Building] WHERE [Id] = @BuildingId)
    BEGIN
        RAISERROR('Le bâtiment spécifié n''existe pas.', 16, 1);
        RETURN;
    END

    INSERT INTO [dbo].[Room] ([Name], [Number], [MachineCapacity], [Price], [BuildingId])
    VALUES (@Name, @Number, @MachineCapacity, @Price, @BuildingId);
END