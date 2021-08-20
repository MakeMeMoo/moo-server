USE moo
GO

IF OBJECT_ID('dbo.users', 'U') IS NULL 
BEGIN
	CREATE TABLE [dbo].[Users](
	[ID] [uniqueidentifier] DEFAULT newsequentialid() NOT NULL,
	[TgId] [int] NULL,
	[TgUsername] [nvarchar](256) NULL,
	[TgFirstName] [nvarchar](256) NULL,
	[TgLastName] [nvarchar](256) NULL,
	[TgLanguageCode] [nvarchar](64) NULL,
	[LastMooDate] [datetime] NULL,
	[MooCount] [int] DEFAULT 0,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO



