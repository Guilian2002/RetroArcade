CREATE PROCEDURE [dbo].[SP_Booking_Update_Status]
	AS
BEGIN
    SET NOCOUNT ON;

    -- 1. OnGoing -> Finished (Basé sur le temps)
    UPDATE [dbo].[Booking]
    SET [Status] = 'Finished'
    WHERE [Status] = 'OnGoing'
      AND [EndDate] < SYSDATETIME();

    -- 2. Finished -> Commented (Basé sur le rang du feedback par utilisateur/salle)
    ;WITH BookingsRanked AS (
        SELECT Id, AccountId, RoomId, [Status],
               ROW_NUMBER() OVER(PARTITION BY AccountId, RoomId ORDER BY EndDate ASC) as B_Rank
        FROM [dbo].[Booking]
        WHERE [Status] IN ('Finished', 'Commented') -- On compte aussi ceux déjà commentés pour garder le bon rang
    ),
    FeedbacksRanked AS (
        SELECT F.RoomId, A.Id AS AccountId,
               ROW_NUMBER() OVER(PARTITION BY A.Id, F.RoomId ORDER BY F.CommentDate ASC) as F_Rank
        FROM [dbo].[RoomFeedback] F
        INNER JOIN [dbo].[Account] A ON F.Username = A.Username -- Lien crucial Nom <-> ID
    )
    UPDATE B
    SET B.[Status] = 'Commented'
    FROM [dbo].[Booking] B
    INNER JOIN BookingsRanked BR ON B.Id = BR.Id
    INNER JOIN FeedbacksRanked FR ON BR.AccountId = FR.AccountId 
                                 AND BR.RoomId = FR.RoomId 
                                 AND BR.B_Rank = FR.F_Rank
    WHERE B.[Status] = 'Finished'; -- On ne met à jour que ceux qui ne le sont pas encore
END
