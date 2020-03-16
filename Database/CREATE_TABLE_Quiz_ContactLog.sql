USE Quiz
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Quiz_ContactLog](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[EmailFrom] [varchar](50) NOT NULL,
	[EmailSubject] [varchar](250) NOT NULL,
	[EmailBody] [varchar](max) NOT NULL,
	[EmailDate] [datetime] NOT NULL	
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Quiz_ContactLog] ADD  DEFAULT (getdate()) FOR [EmailDate]
GO


