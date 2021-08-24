BEGIN TRAN

IF (OBJECT_ID('dbo.spGetUserByTgUsername') IS NOT NULL)
	DROP PROCEDURE dbo.spGetUserByTgUsername
GO

CREATE PROCEDURE [dbo].spGetUserByTgUsername(
	@TgUsername nvarchar(256)
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
		[TgUsername] = @TgUsername
		
END

GO

COMMIT TRAN

