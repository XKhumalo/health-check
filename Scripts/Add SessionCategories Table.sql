CREATE TABLE [dbo].[SessionCategories](
	[SessionCategoryId] [INT] IDENTITY(1,1) NOT NULL,
	[SessionId] [INT] NOT NULL,
	[CategoryId] [INT] NOT NULL,
 CONSTRAINT [PK_SessionCategories] PRIMARY KEY CLUSTERED 
(
	[SessionCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SessionCategories]  WITH CHECK ADD  CONSTRAINT [FK_SessionCategories_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO

ALTER TABLE [dbo].[SessionCategories] CHECK CONSTRAINT [FK_SessionCategories_Categories]
GO

ALTER TABLE [dbo].[SessionCategories]  WITH CHECK ADD  CONSTRAINT [FK_SessionCategories_Sessions] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Sessions] ([SessionId])
GO

ALTER TABLE [dbo].[SessionCategories] CHECK CONSTRAINT [FK_SessionCategories_Sessions]
GO
