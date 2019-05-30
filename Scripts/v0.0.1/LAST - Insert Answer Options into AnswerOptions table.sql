BEGIN TRY
	
	SET IDENTITY_INSERT [dbo].[AnswerOptions] ON;

	BEGIN TRAN

		SELECT TOP(5) * FROM [dbo].[AnswerOptions] ORDER BY [AnswerOptionId] ASC;
		
		INSERT INTO [dbo].[AnswerOptions]
		(
			[AnswerOptionId],
		    [Option],
		    [Description]
		)
		VALUES
		(   
			1,
			N'Green',
		    N'User strongly agrees'
		),
		(   
			2,
			N'Amber',
		    N'User is neutral'
		),
		(   
			3,
			N'Red',
		    N'User strongly disagrees'
		)

	SELECT TOP(5) * FROM [dbo].[AnswerOptions] ORDER BY [AnswerOptionId] ASC;

	COMMIT TRAN;

	SET IDENTITY_INSERT [dbo].[AnswerOptions] OFF;
	
END TRY
BEGIN CATCH
		SELECT   
        ERROR_NUMBER() AS ErrorNumber  
       ,ERROR_MESSAGE() AS ErrorMessage
       ,ERROR_LINE() AS ErrorLine
       ,ERROR_PROCEDURE() AS ErrorProcedure;  
	 IF(@@TRANCOUNT > 0)
		ROLLBACK TRAN
END CATCH; 