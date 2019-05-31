CREATE TABLE [dbo].[Comments]
(
    [CommentId]   [INT]          IDENTITY(1, 1) NOT NULL,
    [Message]     NVARCHAR(1000) NOT NULL,
    [CreatedDate] DATETIME       NOT NULL,
    [UserId]      [INT]          NOT NULL,
    [CategoryId]  INT            NOT NULL
        CONSTRAINT [PK_Comments]
        PRIMARY KEY CLUSTERED ([CommentId] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

ALTER TABLE [dbo].[Categories] WITH CHECK
ADD
    CONSTRAINT [FK_Comments_Categories]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories] ([CategoryId]);
GO

ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_Comments_Categories]

ALTER TABLE [dbo].[Users] WITH CHECK
ADD
    CONSTRAINT [FK_Comments_Users]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users] ([UserId]);
GO

ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Comments_Users]