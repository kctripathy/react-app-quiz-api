USE Quiz
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Quiz_PasswordReset](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[GeneratedByUserId] [int] NOT NULL,
	[UserName] [varchar](20) NOT NULL,
	[AuthKey] [uniqueidentifier] NOT NULL,
	[Expired_fg] [char](1) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[PasswordResetDate] [datetime] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Quiz_PasswordReset] ADD  DEFAULT ('N') FOR [Expired_fg]
GO

ALTER TABLE [dbo].[Quiz_PasswordReset] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO


