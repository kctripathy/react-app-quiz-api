
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetAllUsersByAccountId 
	@AccountId int =1
AS
BEGIN
	 SELECT u.*, c.Description as ClassName, a.AccountName
	 FROM Quiz_Users u
	 INNER JOIN Quiz_Accounts a ON u.AccountId = a.ID
	 INNER JOIN Quiz_Classes c ON u.ClassId = c.ID
	 WHERE a.ID = @AccountId

END
GO
