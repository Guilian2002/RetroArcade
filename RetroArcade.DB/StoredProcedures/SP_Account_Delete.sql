CREATE PROCEDURE [dbo].[SP_Account_Delete]
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @EmailsToDelete TABLE (Email NVARCHAR(320));

    INSERT INTO @EmailsToDelete (Email)
    SELECT [Email]
    FROM [dbo].[Account]
    WHERE [IsActive] = 0 
      AND [DisableDate] IS NOT NULL
      AND DATEDIFF(DAY, [DisableDate], SYSDATETIME()) >= 30;

    DELETE M
    FROM [dbo].[Manager] M
    INNER JOIN @EmailsToDelete E ON M.Email = E.Email;

    DELETE FROM [dbo].[Account]
    WHERE [Email] IN (SELECT Email FROM @EmailsToDelete);
END
