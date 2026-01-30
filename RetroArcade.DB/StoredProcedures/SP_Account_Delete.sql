CREATE PROCEDURE [dbo].[SP_Account_Delete]
	AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [dbo].[Account]
	WHERE [IsActive] = 0 
	  AND [DisableDate] IS NOT NULL
	  AND DATEDIFF(DAY, [DisableDate], SYSDATETIME()) >= 30;
END
