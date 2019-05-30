CREATE TABLE [dbo].[Categories](
	[CategoryId] [INT] IDENTITY(1,1) NOT NULL,
	[Name] [NVARCHAR](100) NOT NULL,
	[Description] [NVARCHAR](500) NOT NULL,
	[Positive] [NVARCHAR](500) NOT NULL,
	[Negative] [NVARCHAR](500) NOT NULL,
	[IsDeleted] [BIT] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO