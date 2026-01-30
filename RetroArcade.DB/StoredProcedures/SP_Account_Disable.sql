CREATE PROCEDURE [dbo].[SP_Account_Disable]
	@accountId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Account]
	SET [IsActive] = 0,
	    [DisableDate] = SYSDATETIME()
	WHERE [Id] = @accountId 
	  AND [IsActive] = 1;
END
