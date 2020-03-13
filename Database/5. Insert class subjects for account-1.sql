USE [Quiz]
GO

INSERT INTO [dbo].[Quiz_Classes_Subject]
	([ClassID],[SubjectID],[IsActive],[AccountId])
SELECT 
	[ClassID],[SubjectID],[IsActive],1 
FROM [dbo].[Quiz_Classes_Subject]
WHERE [AccountId]=0

    