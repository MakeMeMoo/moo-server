BEGIN TRAN

IF (OBJECT_ID('dbo.spUpdateUser') IS NOT NULL)
	DROP PROCEDURE dbo.spUpdateUser
GO

CREATE PROCEDURE [dbo].spUpdateUser(
	@Id [uniqueidentifier],
	@TgId int,
	@TgUsername [nvarchar](256),
	@TgFirstName [nvarchar](256),
	@TgLastName [nvarchar](256),
	@TgLanguageCode [nvarchar](64),
	@LastMooDate [datetime],
	@MooCount [int]
)
AS
BEGIN

	UPDATE [dbo].[Users] 
	SET 
		[TgId] = @TgId,
		[TgUsername] = @TgUsername,
		[TgFirstName] = @TgFirstName,
		[TgLastName] = @TgLastName,
		[TgLanguageCode] = @TgLanguageCode,
		[LastMooDate] = @LastMooDate,
		[MooCount] = @MooCount
	WHERE
		@Id = [Id]
		
END

GO

COMMIT TRAN

