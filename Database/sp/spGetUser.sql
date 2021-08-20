BEGIN TRAN

IF (OBJECT_ID('dbo.spGetUser') IS NOT NULL)
	DROP PROCEDURE dbo.spGetUser
GO

CREATE PROCEDURE [dbo].spGetUser(
	@TgId int
)
AS
BEGIN

	SELECT
		[ID],
		[TgId],
		[TgUsername],
		[TgFirstName],
		[TgLastName],
		[TgLanguageCode],
		[LastMooDate],
		[MooCount]
	FROM
		[dbo].[Users]
	WHERE
		[TgId] = @TgId
		
END

GO

COMMIT TRAN

