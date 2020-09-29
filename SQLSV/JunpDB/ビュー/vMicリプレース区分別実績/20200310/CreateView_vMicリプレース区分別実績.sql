USE [JunpDB]
GO

/****** Object:  View [dbo].[vMicリプレース区分別実績]    Script Date: 2020/03/10 12:38:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[vMicリプレース区分別実績]
--ALTER VIEW [dbo].[vMicリプレース区分別実績]
AS
/*
    vMicリプレース区分別実績

      Ver.1.00 2020/03/10 初版 by 勝呂

*/ 
--課金分
SELECT 
  LEFT(CONVERT(NVARCHAR,H.[f売上承認日], 112), 6) AS 売上承認月
 ,CONVERT(NVARCHAR,H.[f売上承認日], 112) AS 売上日
 ,H.[f受注番号] AS 伝票No
 ,U.顧客No AS 顧客No
 ,U.顧客名１ + U.顧客名２ AS 顧客名
 ,U.[都道府県名] AS 都道府県名
 ,U.支店コード AS 拠店コード
 ,U.支店名 AS 拠店名
 ,H.[f担当者コード] AS 担当者コード
 ,H.[f担当者名] AS 担当者
 ,U.[営業担当者コード] AS 営業担当者コード
 ,U.[営業担当者名] AS 営業担当者名
 ,H.[f件名] AS 摘要
 ,D.[f区分] AS 商品区分
 ,D.[f商品コード] AS 商品コード
 ,D.[f商品名] AS 商品名
 ,CONVERT(int, [f受注金額]) AS 提供価格
 ,D.[f数量] AS 数量
 ,'課金' as 申込種別
 ,IIF(H.fリプレース = '自社Ｒ', '自社Ｒ', IIF(H.fリプレース = '新規' OR H.fリプレース = '新開', '新規・新開', '他社Ｒ')) AS リプレース区分
 ,H.[fリプレース] AS リプレース
 ,IIF(U.MWS_販売種別 = 1, '直販', '販売店') AS 直販区分
 ,U.売上月
FROM [JunpDB].[dbo].[vMic全ユーザー2] AS U
LEFT JOIN [JunpDB].[dbo].[tMih受注ヘッダ] AS H
ON U.[売伝No] = H.[f受注番号]
LEFT JOIN [JunpDB].[dbo].[tMih受注詳細] AS D
ON H.[f年度] = D.[f年度] AND H.[f受注番号] = D.[f受注番号]
WHERE H.f年度 >= 2019 AND H.[f売上承認日] is not NULL AND H.[f販売種別] = 3 AND D.[f商品コード] = '800001'

UNION

--まとめ分
SELECT 
  LEFT(CONVERT(NVARCHAR,H.[f売上承認日], 112), 6) AS 売上承認月
 ,CONVERT(NVARCHAR,H.[f売上承認日], 112) AS 売上日
 ,H.[f受注番号] AS 伝票No
 ,U.顧客No AS 顧客No
 ,U.顧客名１ + U.顧客名２ AS 顧客名
 ,U.[都道府県名] AS 都道府県名
 ,U.支店コード AS 拠店コード
 ,U.支店名 AS 拠店名
 ,H.[f担当者コード] AS 担当者コード
 ,H.[f担当者名] AS 担当者
 ,U.[営業担当者コード] AS 営業担当者コード
 ,U.[営業担当者名] AS 営業担当者名
 ,H.[f件名] AS 摘要
 ,D.[f区分] AS 商品区分
 ,D.[f商品コード] AS 商品コード
 ,D.[f商品名] AS 商品名
 ,CONVERT(int, [f受注金額]) AS 提供価格
 ,D.[f数量] AS 数量
 ,'まとめ' as 申込種別
 ,IIF(H.fリプレース = '自社Ｒ', '自社Ｒ', IIF(H.fリプレース = '新規' OR H.fリプレース = '新開', '新規・新開', '他社Ｒ')) AS リプレース区分
 ,H.[fリプレース] AS リプレース
 ,IIF(U.MWS_販売種別 = 1, '直販', '販売店') AS 直販区分
 ,U.売上月
FROM [JunpDB].[dbo].[vMic全ユーザー2] AS U
LEFT JOIN [JunpDB].[dbo].[tMih受注ヘッダ] AS H
ON U.[売伝No] = H.[f受注番号]
LEFT JOIN [JunpDB].[dbo].[tMih受注詳細] AS D
ON H.[f年度] = D.[f年度] AND H.[f受注番号] = D.[f受注番号]
WHERE H.f年度 >= 2019 AND H.[f売上承認日] is not NULL AND H.f販売種別 = 4 AND (D.[f商品コード] = '800155' OR D.[f商品コード] = '800156' OR D.[f商品コード] = '800157' OR D.[f商品コード] = '800158' OR D.[f商品コード] = '800159')

UNION

--palette ES伝票分
SELECT 
  LEFT(CONVERT(NVARCHAR,H.[f売上承認日], 112), 6) AS 売上承認月
 ,CONVERT(NVARCHAR,H.[f売上承認日], 112) AS 売上日
 ,H.[f受注番号] AS 伝票No
 ,U.顧客No AS 顧客No
 ,U.顧客名１ + U.顧客名２ AS 顧客名
 ,U.[都道府県名] AS 都道府県名
 ,U.支店コード AS 拠店コード
 ,U.支店名 AS 拠店名
 ,H.[f担当者コード] AS 担当者コード
 ,H.[f担当者名] AS 担当者
 ,U.[営業担当者コード] AS 営業担当者コード
 ,U.[営業担当者名] AS 営業担当者名
 ,H.[f件名] AS 摘要
 ,D.[f区分] AS 商品区分
 ,D.[f商品コード] AS 商品コード
 ,D.[f商品名] AS 商品名
 ,CONVERT(int, [f受注金額]) AS 提供価格
 ,D.[f数量] AS 数量
 ,'ＥＳ' AS 申込種別
 ,IIF(H.fリプレース = '自社Ｒ', '自社Ｒ', IIF(H.fリプレース = '新規' OR H.fリプレース = '新開', '新規・新開', '他社Ｒ')) AS リプレース区分
 ,H.[fリプレース] AS リプレース
 ,IIF(U.MWS_販売種別 = 1, '直販', '販売店') AS 直販区分
  ,U.売上月
FROM [JunpDB].[dbo].[tMih受注ヘッダ] AS H
LEFT JOIN [JunpDB].[dbo].[tMih受注詳細] AS D
ON H.[f年度] = D.[f年度] AND H.[f受注番号] = D.[f受注番号]
LEFT JOIN [JunpDB].[dbo].[vMic全ユーザー2] AS U
ON H.[fユーザーコード] = U.顧客No
WHERE H.[f年度] >= 2019 AND H.[f売上承認日] is not NULL AND [f販売種別] = 1 AND D.[f商品コード] = '800121'

GO

