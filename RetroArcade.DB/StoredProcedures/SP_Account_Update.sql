CREATE PROCEDURE [dbo].[SP_Account_Update]
    @accountId UNIQUEIDENTIFIER,
    @lastname NVARCHAR(32),
    @firstname NVARCHAR(32),
    @username NVARCHAR(32)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Account]
    SET [Lastname] = TRIM(@lastname),
        [Firstname] = TRIM(@firstname),
        [Username] = TRIM(@username)
    WHERE [Id] = @accountId
      AND @lastname IS NOT NULL AND TRIM(@lastname) <> ''
      AND @firstname IS NOT NULL AND TRIM(@firstname) <> ''
      AND @username IS NOT NULL AND TRIM(@username) <> '';
END