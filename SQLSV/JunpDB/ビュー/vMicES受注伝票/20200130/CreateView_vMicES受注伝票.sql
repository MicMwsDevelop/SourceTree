USE [JunpDB]
GO

/****** Object:  View [dbo].[vMicES受注伝票]    Script Date: 2020/01/09 16:36:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vMicES受注伝票]
--ALTER VIEW [dbo].[vMicES受注伝票]
AS
-- MWS palette ES 2019版、MWS palette ES ｿﾌﾄｳｪｱ保守料72ヶ月 同伝票
SELECT
ES.*
,D_72.[f受注番号] AS 保守_受注番号
,D_72.[f年度] AS 保守_年度
,H_72.[f受注日] AS 保守_受注日
,H_72.[f受注承認日] AS 保守_受注承認日
,H_72.[f売上承認日] AS 保守_売上承認日
,H_72.[f納期] AS 保守_納期
,H_72.[f販売種別] AS 保守_販売種別
,D_72.[f商品コード] AS 保守_商品コード
,D_72.[f商品名] AS 保守_商品名
,convert(int, H_72.[f受注金額]) AS 保守_受注金額
,H_72.[fSV利用開始年月] AS 保守_利用開始年月
,H_72.[fSV利用終了年月] AS 保守_利用終了年月
,'10' AS 消費税率
FROM [JunpDB].[dbo].[tMih受注詳細] AS D_72
LEFT JOIN [JunpDB].[dbo].[tMih受注ヘッダ] AS H_72 ON D_72.[f年度] = H_72.[f年度] AND D_72.[f受注番号] = H_72.[f受注番号]
INNER JOIN
(
SELECT 
H_ES.[fBshCode3] AS 拠点コード
,H_ES.[f担当支店名] AS 拠点名
,H_ES.[f担当者コード] AS 担当者コード
,H_ES.[f担当者名] AS 担当者名
,H_ES.[fユーザーコード] AS 顧客No
,H_ES.[fユーザー] AS 顧客名
,D_ES.[f受注番号] AS ES_受注番号
,H_ES.[f年度] AS ES_年度
,H_ES.[f受注日] AS ES_受注日
,H_ES.[f受注承認日] AS ES_受注承認日
,H_ES.[f売上承認日] AS ES_売上承認日
,H_ES.[f納期] AS ES_納期
,H_ES.[f販売種別] AS ES_販売種別
,D_ES.[f商品コード] AS ES_商品コード
,D_ES.[f商品名] AS ES_商品名
,convert(int, H_ES.[f受注金額]) AS ES_受注金額
,H_ES.[fSV利用開始年月] AS ES_利用開始年月
,H_ES.[fSV利用終了年月] AS ES_利用終了年月
FROM [JunpDB].[dbo].[tMih受注詳細] AS D_ES
LEFT JOIN [JunpDB].[dbo].[tMih受注ヘッダ] AS H_ES ON D_ES.[f年度] = H_ES.[f年度] AND D_ES.[f受注番号] = H_ES.[f受注番号]
WHERE D_ES.[f商品コード] = '800121' AND H_ES.[f販売種別] = 1 AND [f売上承認日] is not null
) AS ES ON D_72.[f年度] = ES_年度 AND D_72.[f受注番号] = ES_受注番号
WHERE D_72.[f商品コード] = '800161' AND H_72.[f販売種別] = 1 AND H_72.[f受注承認日] is not null

UNION ALL

-- MWS palette ES 2019版、MWS palette ES ｿﾌﾄｳｪｱ保守料72ヶ月 別伝票
SELECT
ES.*
,D_72.[f受注番号] AS 保守_受注番号
,D_72.[f年度] AS 保守_年度
,H_72.[f受注日] AS 保守_受注日
,H_72.[f受注承認日] AS 保守_受注承認日
,H_72.[f売上承認日] AS 保守_売上承認日
,H_72.[f納期] AS 保守_納期
,H_72.[f販売種別] AS 保守_販売種別
,D_72.[f商品コード] AS 保守_商品コード
,D_72.[f商品名] AS 保守_商品名
,convert(int, H_72.[f受注金額]) AS 保守_受注金額
,H_72.[fSV利用開始年月] AS 保守_利用開始年月
,H_72.[fSV利用終了年月] AS 保守_利用終了年月
,'10' AS 消費税率
FROM [JunpDB].[dbo].[tMih受注詳細] AS D_72
LEFT JOIN [JunpDB].[dbo].[tMih受注ヘッダ] AS H_72 ON D_72.[f年度] = H_72.[f年度] AND D_72.[f受注番号] = H_72.[f受注番号]
INNER JOIN
(
SELECT 
H_ES.[fBshCode3] AS 拠点コード
,H_ES.[f担当支店名] AS 拠点名
,H_ES.[f担当者コード] AS 担当者コード
,H_ES.[f担当者名] AS 担当者名
,H_ES.[fユーザーコード] AS 顧客No
,H_ES.[fユーザー] AS 顧客名
,D_ES.[f受注番号] AS ES_受注番号
,H_ES.[f年度] AS ES_年度
,H_ES.[f受注日] AS ES_受注日
,H_ES.[f受注承認日] AS ES_受注承認日
,H_ES.[f売上承認日] AS ES_売上承認日
,H_ES.[f納期] AS ES_納期
,H_ES.[f販売種別] AS ES_販売種別
,D_ES.[f商品コード] AS ES_商品コード
,D_ES.[f商品名] AS ES_商品名
,convert(int, H_ES.[f受注金額]) AS ES_受注金額
,H_ES.[fSV利用開始年月] AS ES_利用開始年月
,H_ES.[fSV利用終了年月] AS ES_利用終了年月
FROM [JunpDB].[dbo].[tMih受注詳細] AS D_ES
LEFT JOIN [JunpDB].[dbo].[tMih受注ヘッダ] AS H_ES ON D_ES.[f年度] = H_ES.[f年度] AND D_ES.[f受注番号] = H_ES.[f受注番号]
WHERE D_ES.[f商品コード] = '800121' AND H_ES.[f販売種別] = 1 AND [f売上承認日] is not null
) AS ES ON H_72.[fユーザーコード] = ES.顧客No and H_72.[fSV利用開始年月] = ES.ES_利用開始年月 and D_72.[f受注番号] <> ES_受注番号
WHERE D_72.[f商品コード] = '800161' AND H_72.[f販売種別] = 3 AND H_72.[f受注承認日] is not null

UNION ALL

-- MWS palette ES 2019版、MWS palette ES ｿﾌﾄｳｪｱ保守料12ヶ月 別伝票
SELECT
ES.*
,D_12.[f受注番号] AS 保守_受注番号
,D_12.[f年度] AS 保守_年度
,H_12.[f受注日] AS 保守_受注日
,H_12.[f受注承認日] AS 保守_受注承認日
,H_12.[f売上承認日] AS 保守_売上承認日
,H_12.[f納期] AS 保守_納期
,H_12.[f販売種別] AS 保守_販売種別
,D_12.[f商品コード] AS 保守_商品コード
,D_12.[f商品名] AS 保守_商品名
,convert(int, H_12.[f受注金額]) AS 保守_受注金額
,H_12.[fSV利用開始年月] AS 保守_利用開始年月
,H_12.[fSV利用終了年月] AS 保守_利用終了年月
,'10' AS 消費税率
FROM [JunpDB].[dbo].[tMih受注詳細] AS D_12
LEFT JOIN [JunpDB].[dbo].[tMih受注ヘッダ] AS H_12 ON D_12.[f年度] = H_12.[f年度] AND D_12.[f受注番号] = H_12.[f受注番号]
INNER JOIN
(
SELECT 
H_ES.[fBshCode3] AS 拠点コード
,H_ES.[f担当支店名] AS 拠点名
,H_ES.[f担当者コード] AS 担当者コード
,H_ES.[f担当者名] AS 担当者名
,H_ES.[fユーザーコード] AS 顧客No
,H_ES.[fユーザー] AS 顧客名
,D_ES.[f受注番号] AS ES_受注番号
,H_ES.[f年度] AS ES_年度
,H_ES.[f受注日] AS ES_受注日
,H_ES.[f受注承認日] AS ES_受注承認日
,H_ES.[f売上承認日] AS ES_売上承認日
,H_ES.[f納期] AS ES_納期
,H_ES.[f販売種別] AS ES_販売種別
,D_ES.[f商品コード] AS ES_商品コード
,D_ES.[f商品名] AS ES_商品名
,convert(int, H_ES.[f受注金額]) AS ES_受注金額
,H_ES.[fSV利用開始年月] AS ES_利用開始年月
,H_ES.[fSV利用終了年月] AS ES_利用終了年月
FROM [JunpDB].[dbo].[tMih受注詳細] AS D_ES
LEFT JOIN [JunpDB].[dbo].[tMih受注ヘッダ] AS H_ES ON D_ES.[f年度] = H_ES.[f年度] AND D_ES.[f受注番号] = H_ES.[f受注番号]
WHERE D_ES.[f商品コード] = '800121' AND H_ES.[f販売種別] = 1 AND [f売上承認日] is not null
) AS ES ON H_12.[fユーザーコード] = ES.顧客No and H_12.[fSV利用開始年月] = ES.ES_利用開始年月 and D_12.[f受注番号] <> ES_受注番号
WHERE D_12.[f商品コード] = '800162' AND H_12.[f販売種別] = 3 AND H_12.[f受注承認日] is not null

GO
