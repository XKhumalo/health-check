CREATE TABLE [dbo].[Answers](
	[AnswerId] [INT] IDENTITY(1,1) NOT NULL,
	[UserId] [INT] NOT NULL,
	[SessionId] [INT] NOT NULL,
	[CategoryId] [INT] NOT NULL,
	[AnswerOptionId] [INT] NOT NULL,
 CONSTRAINT [PK_Answers] PRIMARY KEY CLUSTERED 
(
	[AnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Answers]  WITH CHECK ADD  CONSTRAINT [FK_Answers_AnswerOptions] FOREIGN KEY([AnswerOptionId])
REFERENCES [dbo].[AnswerOptions] ([AnswerOptionId])
GO

ALTER TABLE [dbo].[Answers] CHECK CONSTRAINT [FK_Answers_AnswerOptions]
GO

ALTER TABLE [dbo].[Answers]  WITH CHECK ADD  CONSTRAINT [FK_Answers_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO

ALTER TABLE [dbo].[Answers] CHECK CONSTRAINT [FK_Answers_Categories]
GO

ALTER TABLE [dbo].[Answers]  WITH CHECK ADD  CONSTRAINT [FK_Answers_Sessions] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Sessions] ([SessionId])
GO

ALTER TABLE [dbo].[Answers] CHECK CONSTRAINT [FK_Answers_Sessions]
GO

ALTER TABLE [dbo].[Answers]  WITH CHECK ADD  CONSTRAINT [FK_Answers_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[Answers] CHECK CONSTRAINT [FK_Answers_Users]
GO
