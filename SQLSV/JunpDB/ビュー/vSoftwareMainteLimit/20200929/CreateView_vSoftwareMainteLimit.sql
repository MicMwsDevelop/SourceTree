USE [JunpDB]
GO

/****** Object:  View [dbo].[vSoftwareMainteLimit]    Script Date: 2020/09/29 16:46:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
         BusinessPlanningWeb palette ES ソフトウェア保守料 更新対象一覧

         Ver1.00 2020/08/28 新規
*/
--CREATE VIEW [dbo].[vSoftwareMainteLimit]
CREATE VIEW [dbo].[vSoftwareMainteLimit]
AS
SELECT 
 U.[支店コード] as 拠点コード
,U.[支店名] as 拠点名
,CUI.[CUSTOMER_ID] as 顧客No
,U.[顧客名１] + U.[顧客名2] as 顧客名
,U.[営業担当者名] as 営業担当
,CUI.[SERVICE_ID] as サービスID
,SV.[SERVICE_NAME] as サービス名
,left(convert(nvarchar, [USE_START_DATE], 111), 7) as 利用開始
,left(convert(nvarchar, [USE_END_DATE], 111), 7) as 利用終了
,iif(U.[終了フラグ] = '1', '終了', '') as 終了
--,E.[終了月] as 終了月
FROM [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION] as CUI
left join [charlieDB].[dbo].[M_SERVICE] as SV on CUI.SERVICE_ID = SV.[SERVICE_ID]
left join [JunpDB].[dbo].[vMic全ユーザー2] as U on CUI.[CUSTOMER_ID] = U.[顧客No]
--left join [JunpDB].[dbo].[tMic終了ユーザーリスト] as E on E.得意先No = U.[得意先No]
where CUI.[SERVICE_ID] = 9910140

GO

