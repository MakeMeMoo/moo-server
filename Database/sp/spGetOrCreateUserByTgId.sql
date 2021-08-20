BEGIN TRAN

IF (OBJECT_ID('dbo.spGetOrCreateUserByTgId') IS NOT NULL)
	DROP PROCEDURE dbo.spGetOrCreateUserByTgId
GO

CREATE PROCEDURE [dbo].spGetOrCreateUserByTgId(
	@TgId int,
	@TgUsername [nvarchar](256),
	@TgFirstName [nvarchar](256),
	@TgLastName [nvarchar](256),
	@TgLanguageCode [nvarchar](64)
)
AS
BEGIN
	IF(NOT EXISTS(select top 1 Id from Users where [TgId] = @TgId))
	BEGIN
		INSERT INTO [dbo].[Users]
		(
			[TgId],
			[TgUsername],
			[TgFirstName],
			[TgLastName],
			[TgLanguageCode]
		)
		VALUES 
		(
			@TgId,
			@TgUsername,
			@TgFirstName,
			@TgLastName,
			@TgLanguageCode
		)
	END

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
	
	SELECT SCOPE_IDENTITY()
END

GO

COMMIT TRAN

