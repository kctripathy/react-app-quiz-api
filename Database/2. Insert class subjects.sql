USE [Quiz]
GO


insert into Quiz_Classes (Description) values('5th');
insert into Quiz_Classes (Description) values('6th');
insert into Quiz_Classes (Description) values('7th');
insert into Quiz_Classes (Description) values('8th');



INSERT INTO [dbo].[Quiz_Subjects]([Name],[Description])VALUES('Mathematics', 'Mathematics Quiz');
INSERT INTO [dbo].[Quiz_Subjects]([Name],[Description])VALUES('Science', 'Science Quiz');
INSERT INTO [dbo].[Quiz_Subjects]([Name],[Description])VALUES('Social Science', 'Social Science Quiz');
INSERT INTO [dbo].[Quiz_Subjects]([Name],[Description])VALUES('Computer Science', 'Computer Science Quiz');
INSERT INTO [dbo].[Quiz_Subjects]([Name],[Description])VALUES('Geography', 'Geography Quiz');
INSERT INTO [dbo].[Quiz_Subjects]([Name],[Description])VALUES('Economics', 'Economics Quiz');
INSERT INTO [dbo].[Quiz_Subjects]([Name],[Description])VALUES('Physical Education', 'Physical Education Quiz');





INSERT INTO [dbo].[Quiz_Subjects]([Name],[Description])VALUES('English ', 'English  Quiz');
INSERT INTO [dbo].[Quiz_Subjects]([Name],[Description])VALUES('Physics', 'Physics Quiz');
INSERT INTO [dbo].[Quiz_Subjects]([Name],[Description])VALUES('Chemistry', 'Chemistry Quiz');
INSERT INTO [dbo].[Quiz_Subjects]([Name],[Description])VALUES('Botany', 'Botany Quiz');
INSERT INTO [dbo].[Quiz_Subjects]([Name],[Description])VALUES('Zoology', 'Zoology Quiz');

INSERT INTO [dbo].[Quiz_Subjects]([Name],[Description])VALUES('Political Science', 'Political Science Quiz');
INSERT INTO [dbo].[Quiz_Subjects]([Name],[Description])VALUES('History', 'History Quiz');
INSERT INTO [dbo].[Quiz_Subjects]([Name],[Description])VALUES('Sociology', 'Sociology Quiz');
INSERT INTO [dbo].[Quiz_Subjects]([Name],[Description])VALUES('Psychology', 'Psychology Quiz');
INSERT INTO [dbo].[Quiz_Subjects]([Name],[Description])VALUES('Logic', 'Logic Quiz');
INSERT INTO [dbo].[Quiz_Subjects]([Name],[Description])VALUES('Education', 'Education Quiz');

GO


INSERT INTO [dbo].[Quiz_Classes_Subject]
	([ClassID],[SubjectID],[IsActive],[AccountId])
SELECT 
	[ClassID],[SubjectID],[IsActive],1 
FROM [dbo].[Quiz_Classes_Subject]
WHERE [AccountId]=0

    