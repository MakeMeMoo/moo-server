BEGIN TRAN

IF (OBJECT_ID('dbo.spUpdateUserMooCountAndLastMooDateById') IS NOT NULL)
	DROP PROCEDURE dbo.spUpdateUserMooCountAndLastMooDateById
GO

CREATE PROCEDURE [dbo].spUpdateUserMooCountAndLastMooDateById(
	@Id [uniqueidentifier],
	@LastMooDate [datetime],
	@MooCount [int]
)
AS
BEGIN

	UPDATE [dbo].[Users] 
	SET 
		[LastMooDate] = @LastMooDate,
		[MooCount] = @MooCount
	WHERE
		@Id = [Id]
		
END

GO

COMMIT TRAN

