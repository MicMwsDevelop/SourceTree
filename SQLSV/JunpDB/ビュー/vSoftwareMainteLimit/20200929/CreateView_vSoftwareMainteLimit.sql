USE [JunpDB]
GO

/****** Object:  View [dbo].[vSoftwareMainteLimit]    Script Date: 2020/09/29 16:46:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
         BusinessPlanningWeb palette ES \tgEFAÛç¿ XVÎÛê

         Ver1.00 2020/08/28 VK
*/
--CREATE VIEW [dbo].[vSoftwareMainteLimit]
CREATE VIEW [dbo].[vSoftwareMainteLimit]
AS
SELECT 
 U.[xXR[h] as _R[h
,U.[xX¼] as _¼
,CUI.[CUSTOMER_ID] as ÚqNo
,U.[Úq¼P] + U.[Úq¼2] as Úq¼
,U.[cÆSÒ¼] as cÆS
,CUI.[SERVICE_ID] as T[rXID
,SV.[SERVICE_NAME] as T[rX¼
,left(convert(nvarchar, [USE_START_DATE], 111), 7) as pJn
,left(convert(nvarchar, [USE_END_DATE], 111), 7) as pI¹
,iif(U.[I¹tO] = '1', 'I¹', '') as I¹
--,E.[I¹] as I¹
FROM [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION] as CUI
left join [charlieDB].[dbo].[M_SERVICE] as SV on CUI.SERVICE_ID = SV.[SERVICE_ID]
left join [JunpDB].[dbo].[vMicS[U[2] as U on CUI.[CUSTOMER_ID] = U.[ÚqNo]
--left join [JunpDB].[dbo].[tMicI¹[U[Xg] as E on E.¾ÓæNo = U.[¾ÓæNo]
where CUI.[SERVICE_ID] = 9910140

GO

