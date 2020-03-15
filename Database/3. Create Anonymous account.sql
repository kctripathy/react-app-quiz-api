USE [Quiz]
GO

INSERT INTO [dbo].[Quiz_Accounts]([AccountName],[ContactName]) VALUES('ODIWARE', 'CHANDRA SEKHAR')
GO


--INSERT INTO [dbo].[Quiz_Users]
--           ([AccountId]
--           ,[AddressId]
--           ,[Fullname]
--           ,[UserName]
--           ,[UserGender]
--           ,[UserEmail]
--           ,[UserPassword]
--           ,[UserPhone]
--           ,[AccessLevel]
--           ,[AllowLogin]
--           ,[LastLoginDate]
--           ,[CreatedBy]
--           ,[CreatedDate]
--           ,[UpdatedBy]
--           ,[UpdatedDate]
--           ,[Salt]
--           ,[AccessToken]
--           ,[ClassId]
--           ,[SubjectIds])
--     VALUES
--           (1
--           ,NULL
--           ,'CHANDRA SEKHAR'
--           ,'CHANDRA'
--           ,'M'
--           ,'sa@gmail.com'
--           ,'aaaa'
--           ,'99999888888'
--           ,1
--           ,1
--           ,null
--           ,0
--           ,getdate()
--           ,null --<UpdatedBy, varchar(20),>
--           ,null --<UpdatedDate, datetime,>
--           ,'abcd' --<Salt, varchar(25),>
--           ,null --<AccessToken, varchar(100),>
--           ,1 --<ClassId, int,>
--           ,null --<SubjectIds, varchar(100),>
--		   )
--GO


