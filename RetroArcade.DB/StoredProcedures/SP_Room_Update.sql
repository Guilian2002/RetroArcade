CREATE PROCEDURE [dbo].[SP_Room_Update]
	@Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(50),
    @Number INT,
    @MachineCapacity INT,
    @Price DECIMAL(10,2),
    @BuildingId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF @Id IS NULL OR NOT EXISTS (SELECT 1 FROM [dbo].[Room] WHERE [Id] = @Id)
    BEGIN
        RAISERROR('Identifiant de salle invalide ou inexistant.', 16, 1);
        RETURN;
    END

    IF (@Name IS NULL OR LTRIM(@Name) = '') OR (@Number IS NULL) OR 
       (@MachineCapacity IS NULL) OR (@Price IS NULL) OR (@BuildingId IS NULL)
    BEGIN
        RAISERROR('Les données de mise à jour ne peuvent pas être nulles ou vides.', 16, 1);
        RETURN;
    END

    IF @Number < 1 OR @Number > 25 OR @MachineCapacity <> 10
    BEGIN
        RAISERROR('Contraintes de numéro (1-25) ou de capacité (10) non respectées.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Building] WHERE [Id] = @BuildingId)
    BEGIN
        RAISERROR('Le bâtiment référencé n''existe pas.', 16, 1);
        RETURN;
    END

    UPDATE [dbo].[Room]
    SET [Name] = @Name,
        [Number] = @Number,
        [MachineCapacity] = @MachineCapacity,
        [Price] = @Price,
        [BuildingId] = @BuildingId
    WHERE [Id] = @Id;
END
