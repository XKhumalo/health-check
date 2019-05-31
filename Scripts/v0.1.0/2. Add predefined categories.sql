BEGIN TRY

    SET IDENTITY_INSERT [dbo].[Categories] ON;

    BEGIN TRAN;

    SELECT TOP (5) * 
	FROM [dbo].[Categories]
    ORDER BY [CategoryId] ASC;    

    INSERT INTO [dbo].[Categories]
    (
        [CategoryId],
        [Name],
        [Description],
        [Positive],
        [Negative],
        [IsDeleted]
    )
    VALUES
    (1, N'Support', N'The action of supporting something or someone or the state of being supported', N'We always get great support and help when we ask for it!',
     N'We keep getting stuck because we can''t get the support and help that we ask for.', 0),
    (2, N'Teamwork', N'Cooperative or coordinated effort on the part of a group of persons acting together as a team or in the interests of a common cause.',
     N'We are a totally gelled super-team with awesome collaboration!', N'We are a bunch of indivduals that neither know nor care about what the other people in the squad are doing.', 0),
    (3, N'Pawns vs Players', N'The type of role you see yourself fulfilling on the team', N'We are in control of our own destiny! We decide what to build and how to build it.',
     N'We are just pawns in a game of chess with no influence over what we build or how we build it.', 0),
    (4, N'Mission', N'An important goal or purpose that is accompanied by strong conviction; a calling or vocation.', N'We know exactly why we are here and we''re really excited about it!',
     N'We have no idea why we are here, there''s no high level picture or focus. Our so called ''mission'' is completely unclear and uninspiring', 0),
    (5, N'Health of Codebase', N'The maintainability, adaptiveness and robustness of code', N'We''re proud of the quality of our code! It is clean, easy to read and has great test coverage',
     N'Our code is a pile of kak and technical debt is raging out of control', 0),
    (6, N'Suitable Process', N'A continuous action, operation, or series of changes taking place in a definite manner.', N'Our way of working fits us perfectly!', N'Our way of working sucks!', 0),
    (7, N'Delivering Value', N'To consider with respect to worth, excellence, usefulness, or importance	We deliver great stuff!', N'We''re proud of it and our stakeholders are really happy.',
     N'We deliver kak. We feel ashamed to deliver it. Our stakeholders hate us.', 0),
    (8, N'Learning', N'The modification of behavior through practice, training, or experience.', N'We''re learning lots of interesting stuff all the time!', N'We never have time to learn anything.',
     0),
    (9, N'Speed', N'To promote the success of (an affair, undertaking, etc.); further, forward, or expedite', N'We get stuff done really quickly! No waiting and no delays.',
     N'We never seem to get anything done. We keep getting stuck or interrupted. Stories keep getting stuck on dependencies.', 0),
    (10, N'Ease of Release', N'The speed and simplicity to get new features out - without risk.', N'Releasing is simple, safe, painless and mostly automated.',
     N'Releasing is risky, painful, lots of manual work and takes forever.', 0),
    (11, N'FUN', N'Something that provides mirth or amusement.', N'We love going to work and have great fun working together!', N'Boooooooring...', 0);
    
    SELECT TOP (5) * 
	FROM [dbo].[Categories]
    ORDER BY [CategoryId] ASC;

    ROLLBACK TRAN;

    SET IDENTITY_INSERT [dbo].[Categories] OFF;

END TRY
BEGIN CATCH
    SELECT
        ERROR_NUMBER()    AS [ErrorNumber],
        ERROR_MESSAGE()   AS [ErrorMessage],
        ERROR_LINE()      AS [ErrorLine],
        ERROR_PROCEDURE() AS [ErrorProcedure];
    IF (@@TRANCOUNT > 0)
        ROLLBACK TRAN;
END CATCH;