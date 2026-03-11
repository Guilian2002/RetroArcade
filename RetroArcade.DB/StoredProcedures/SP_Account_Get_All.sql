CREATE PROCEDURE [dbo].[SP_Account_Get_All]
	AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [Id] AS AccountId,
		[Lastname] AS AccountLastname,
		[Firstname] AS AccountFirstname,
		[Username] AS AccountUsername,
		[Email] AS AccountEmail,
		[Role] AS AccountRole,
		[CreationDate] AS AccountCreationDate,
		[DisableDate] AS AccountDisable,
		[IsActive] AS AccountIsActive
    FROM [dbo].[Account];
END
