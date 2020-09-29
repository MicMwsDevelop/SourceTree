USE [JunpDB]
GO

/****** Object:  View [dbo].[vMicES売上予想]    Script Date: 2020/01/09 16:36:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vMicES売上予想]
--ALTER VIEW [dbo].[vMicES売上予想]
AS
SELECT 
 '0' + convert(nvarchar, B.[fPca部門コード]) AS 部門コード
,U.[営業部名] AS 営業部名
,H.[fBshCode3] AS 拠点コード
,H.[f担当支店名] AS 拠点名
,H.[fユーザーコード] AS 顧客No
,H.[fユーザー] AS 顧客名
,D.[f受注番号] AS 受注番号
,H.[f受注承認日] AS 受注承認日
,H.[f売上承認日] AS 売上承認日
,H.[f納期] AS 納期
,H.[f販売種別] AS 販売種別
,D.[f商品コード] AS 商品コード
,D.[f商品名] AS 商品名
,convert(int, H.[f受注金額]) AS 受注金額
,60000 as 売上金額
,left(convert(nvarchar, H.[f納期], 111), 7) AS 計上1年目
,left(convert(nvarchar, dateadd(month, 11, H.[f納期]), 111), 7) AS 計上2年目
,left(convert(nvarchar, dateadd(month, 23, H.[f納期]), 111), 7) AS 計上3年目
,left(convert(nvarchar, dateadd(month, 35, H.[f納期]), 111), 7) AS 計上4年目
,left(convert(nvarchar, dateadd(month, 47, H.[f納期]), 111), 7) AS 計上5年目
,left(convert(nvarchar, dateadd(month, 59, H.[f納期]), 111), 7) AS 計上6年目
FROM [JunpDB].[dbo].[tMih受注詳細] AS D
LEFT JOIN [JunpDB].[dbo].[tMih受注ヘッダ] AS H ON D.[f年度] = H.[f年度] AND D.[f受注番号] = H.[f受注番号]
LEFT JOIN [JunpDB].[dbo].[vMic全ユーザー３] AS U ON U.[顧客No] = H.[fユーザーコード]
LEFT JOIN [JunpDB].[dbo].[tMih支店情報] AS B ON B.[fBshCode3] = H.[fBshCode3]
WHERE D.[f商品コード] = '800121' AND H.[f販売種別] = 1

GO
