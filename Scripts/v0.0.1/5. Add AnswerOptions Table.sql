CREATE TABLE [dbo].[AnswerOptions](
	[AnswerOptionId] [INT] IDENTITY(1,1) NOT NULL,
	[Option] [NVARCHAR](15) NOT NULL,
	[Description] [NVARCHAR](50) NOT NULL,
 CONSTRAINT [PK_AnswerOptions] PRIMARY KEY CLUSTERED 
(
	[AnswerOptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO