CREATE PROCEDURE [dbo].[SP_Account_Insert]
    @lastname NVARCHAR(32),
    @firstname NVARCHAR(32),
    @username NVARCHAR(32),
    @role VARCHAR(8),
    @email NVARCHAR(320),
    @password NVARCHAR(64)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AccountId UNIQUEIDENTIFIER = NEWID();
    DECLARE @Salt UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[Account] ([Id], [Lastname], [Firstname], [Username], [Email], [Role], [CreationDate], [DisableDate], [IsActive])
    VALUES (@AccountId, @lastname, @firstname, @username, @email, @role, CAST(SYSDATETIME() AS DATE), NULL, 1);

    INSERT INTO [dbo].[AccountCredential] ([Id], [PasswordHash], [Salt], [AccountId])
    VALUES (NEWID(), [dbo].[SF_HashAndSalt](@password, @Salt), @Salt, @AccountId);
END