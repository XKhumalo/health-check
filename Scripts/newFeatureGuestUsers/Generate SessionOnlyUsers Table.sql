GO

/****** Object:  Table [dbo].[SessionOnlyUsers]    Script Date: 2020/04/04 15:59:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SessionOnlyUsers](
	[SessionOnlyUserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nchar](200) NOT NULL,
	[SessionKey] [nchar](6) NOT NULL,
	[SessionId] [int] NOT NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_SessionOnlyUsers] PRIMARY KEY CLUSTERED 
(
	[SessionOnlyUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

