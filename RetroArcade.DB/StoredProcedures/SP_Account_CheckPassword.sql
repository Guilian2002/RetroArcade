CREATE PROCEDURE [dbo].[SP_Account_CheckPassword]
    @email NVARCHAR(320),
    @password NVARCHAR(64)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT  A.[Id],
            A.[Firstname],
            A.[Lastname],
            A.[Username],
            A.[Email],
            A.[Role]
    FROM [dbo].[Account] A
    INNER JOIN [dbo].[AccountCredential] AC ON A.[Id] = AC.[AccountId]
    WHERE A.[Email] = @email
      AND AC.[PasswordHash] = [dbo].[SF_HashAndSalt](@password, AC.[Salt])
      AND A.[IsActive] = 1
      AND A.[DisableDate] IS NULL;
END