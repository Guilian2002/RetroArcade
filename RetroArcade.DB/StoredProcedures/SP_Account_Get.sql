CREATE PROCEDURE [dbo].[SP_Account_Get]
    @accountId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT  [Id],
            [Lastname],
            [Firstname],
            [Username],
            [Email],
            [Role],
            [CreationDate],
            [DisableDate],
            [IsActive]
    FROM [dbo].[Account]
    WHERE [Id] = @accountId;
END
