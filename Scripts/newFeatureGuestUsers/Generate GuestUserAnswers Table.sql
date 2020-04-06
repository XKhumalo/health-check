GO

/****** Object:  Table [dbo].[GuestUserAnswers]    Script Date: 2020/04/04 15:59:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GuestUserAnswers](
	[GuestUserAnswerId] [int] IDENTITY(1,1) NOT NULL,
	[SessionOnlyUserId] [int] NOT NULL,
	[SessionId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[AnswerOptionId] [int] NOT NULL,
 CONSTRAINT [PK_GuestUserAnswers] PRIMARY KEY CLUSTERED 
(
	[GuestUserAnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GuestUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_GuestUserAnswers_AnswerOptions] FOREIGN KEY([AnswerOptionId])
REFERENCES [dbo].[AnswerOptions] ([AnswerOptionId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[GuestUserAnswers] CHECK CONSTRAINT [FK_GuestUserAnswers_AnswerOptions]
GO

ALTER TABLE [dbo].[GuestUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_GuestUserAnswers_SessionOnlyUsers] FOREIGN KEY([SessionOnlyUserId])
REFERENCES [dbo].[SessionOnlyUsers] ([SessionOnlyUserId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[GuestUserAnswers] CHECK CONSTRAINT [FK_GuestUserAnswers_SessionOnlyUsers]
GO

ALTER TABLE [dbo].[GuestUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_GuestUserAnswers_Sessions] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Sessions] ([SessionId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[GuestUserAnswers] CHECK CONSTRAINT [FK_GuestUserAnswers_Sessions]
GO

