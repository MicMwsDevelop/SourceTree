USE [JunpDB]
GO

/****** Object:  View [dbo].[vMicES保守売上管理]    Script Date: 2020/01/09 16:36:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vMicES保守売上管理]
--ALTER VIEW [dbo].[vMicES保守売上管理]
AS
SELECT
 U.[営業部コード] AS 営業部コード
,U.[営業部名] AS 営業部名
,ES.[拠点コード] AS 拠点コード
,ES.拠点名 AS 拠点名
,ES.担当者コード AS 担当者コード
,ES.担当者名 AS 担当者名
,U.[終了フラグ] AS 終了フラグ
,ES.顧客No AS 顧客No
,U.[得意先No] AS 得意先No
,ES.顧客名 AS 顧客名
,ES.[ES_利用終了年月] AS 使用許諾期限
,left(convert(nvarchar, ES.[ES_売上承認日], 111), 7) AS 売上月
,ES.消費税率
,iif(ES.[保守_商品コード] = '800161', 6, 1) as 保守請求区分
,left(convert(nvarchar, ES.[ES_売上承認日], 111), 7) AS 計上1年目
,left(convert(nvarchar, dateadd(month, 11, ES.[ES_売上承認日]), 111), 7) AS 計上2年目
,left(convert(nvarchar, dateadd(month, 23, ES.[ES_売上承認日]), 111), 7) AS 計上3年目
,left(convert(nvarchar, dateadd(month, 35, ES.[ES_売上承認日]), 111), 7) AS 計上4年目
,left(convert(nvarchar, dateadd(month, 47, ES.[ES_売上承認日]), 111), 7) AS 計上5年目
,left(convert(nvarchar, dateadd(month, 59, ES.[ES_売上承認日]), 111), 7) AS 計上6年目
,convert(int, M.sms_hyo) AS 請求1年目
,iif(ES.[保守_商品コード] = '800161', 0, convert(int, M.sms_hyo)) AS 請求2年目
,iif(ES.[保守_商品コード] = '800161', 0, convert(int, M.sms_hyo)) AS 請求3年目
,iif(ES.[保守_商品コード] = '800161', 0, convert(int, M.sms_hyo)) AS 請求4年目
,iif(ES.[保守_商品コード] = '800161', 0, convert(int, M.sms_hyo)) AS 請求5年目
,iif(ES.[保守_商品コード] = '800161', 0, convert(int, M.sms_hyo)) AS 請求6年目
,iif(ES.[保守_商品コード] = '800161', convert(int, M.sms_hyo) / 6, convert(int, M.sms_hyo)) AS 売上1年目
,iif(ES.[保守_商品コード] = '800161', convert(int, M.sms_hyo) / 6, convert(int, M.sms_hyo)) AS 売上2年目
,iif(ES.[保守_商品コード] = '800161', convert(int, M.sms_hyo) / 6, convert(int, M.sms_hyo)) AS 売上3年目
,iif(ES.[保守_商品コード] = '800161', convert(int, M.sms_hyo) / 6, convert(int, M.sms_hyo)) AS 売上4年目
,iif(ES.[保守_商品コード] = '800161', convert(int, M.sms_hyo) / 6, convert(int, M.sms_hyo)) AS 売上5年目
,iif(ES.[保守_商品コード] = '800161', convert(int, M.sms_hyo) / 6, convert(int, M.sms_hyo)) AS 売上6年目
,0 as 前月前受残高1年目
,iif(ES.[保守_商品コード] = '800161', (convert(int, M.sms_hyo) / 6) * 5, 0) as 前月前受残高2年目
,iif(ES.[保守_商品コード] = '800161', (convert(int, M.sms_hyo) / 6) * 4, 0) as 前月前受残高3年目
,iif(ES.[保守_商品コード] = '800161', (convert(int, M.sms_hyo) / 6) * 3, 0) as 前月前受残高4年目
,iif(ES.[保守_商品コード] = '800161', convert(int, M.sms_hyo) / 6 * 2, 0) as 前月前受残高5年目
,iif(ES.[保守_商品コード] = '800161', convert(int, M.sms_hyo) / 6, 0) as 前月前受残高6年目
,iif(ES.[保守_商品コード] = '800161', (convert(int, M.sms_hyo) / 6) * 5, 0) as 当月前受残高1年目
,iif(ES.[保守_商品コード] = '800161', (convert(int, M.sms_hyo) / 6) * 4, 0) as 当月前受残高2年目
,iif(ES.[保守_商品コード] = '800161', (convert(int, M.sms_hyo) / 6) * 3, 0) as 当月前受残高3年目
,iif(ES.[保守_商品コード] = '800161', (convert(int, M.sms_hyo) / 6) * 2, 0) as 当月前受残高4年目
,iif(ES.[保守_商品コード] = '800161', convert(int, M.sms_hyo) / 6, 0) as 当月前受残高5年目
,0 as 当月前受残高6年目
,ES.[ES_受注番号] AS ES_受注番号
,ES.[保守_受注番号] AS 保守_受注番号
,ES.[保守_商品コード] AS 保守_商品コード
FROM [JunpDB].[dbo].[vMicES受注伝票] AS ES
LEFT JOIN [JunpDB].[dbo].[vMic全ユーザー３] AS U ON U.[顧客No] = ES.[顧客No]
LEFT JOIN [JunpDB].[dbo].[vMicPCA商品マスタ] AS M ON M.[sms_scd] = ES.[保守_商品コード] 

GO
