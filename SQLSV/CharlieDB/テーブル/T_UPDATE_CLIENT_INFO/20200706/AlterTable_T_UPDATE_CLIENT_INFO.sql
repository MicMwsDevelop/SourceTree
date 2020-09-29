USE [charlieDB]
GO

ALTER TABLE [dbo].[T_UPDATE_CLIENT_INFO]
	ADD [fEstoreMailAddress] [nvarchar](50) NULL

ALTER TABLE [dbo].[T_UPDATE_CLIENT_INFO]
	ADD [fMailMagazineFlg] [int] NULL

