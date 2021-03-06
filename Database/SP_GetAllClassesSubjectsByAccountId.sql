USE [Quiz]
GO
/****** Object:  StoredProcedure [dbo].[GetAllClassesSubjectsByAccountId]    Script Date: 3/5/2020 1:03:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE  [dbo].[GetAllClassesSubjectsByAccountId]
	@AccountId int =1 
AS
BEGIN

	if (@AccountId > 0)
		select 
			qcs.ID as ClassSubjectID, 
			qcs.classID, qc.[Description] as ClassDesc, 
			qcs.SubjectID, qs.[Description] as SubjectDesc,
			qa.ID as AccountID, qa.AccountName
		from Quiz_Classes_Subject qcs
		inner join Quiz_Classes qc on qc.ID = qcs.ClassID
		inner join Quiz_Subjects qs on qs.ID = qcs.SubjectID
		inner join Quiz_Accounts qa on qcs.AccountId = qa.ID
		and qcs.AccountId =@AccountId
		order by qcs.ClassID, qcs.SubjectID 
	else
		select 
			qcs.ID as ClassSubjectID, 
			qcs.classID, qc.[Description] as ClassDesc, 
			qcs.SubjectID, qs.[Description] as SubjectDesc,
			0 as AccountID, '' as AccountName 
		from Quiz_Classes_Subject qcs
		inner join Quiz_Classes qc on qc.ID = qcs.ClassID
		inner join Quiz_Subjects qs on qs.ID = qcs.SubjectID		
		where ISNULL(qcs.AccountId,0) = 0
		order by qcs.ClassID, qcs.SubjectID 				
END

