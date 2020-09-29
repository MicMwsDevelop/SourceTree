USE [JunpDB]
GO

/****** Object:  View [dbo].[vMicまとめ売上実績]    Script Date: 2019/10/10 12:48:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--CREATE VIEW [dbo].[vMicまとめ売上実績]
ALTER VIEW [dbo].[vMicまとめ売上実績]
AS
/*
    vMicまとめ売上実績

      Ver.1.00 2019/10/10 初版 by 勝呂
      Ver.1.01 2020/03/04 営業部コードと営業部名のフィールド追加 by 勝呂

*/ 
--MWSサイト申込分
SELECT 
	  LEFT(CONVERT(nvarchar, [sykd_uribi], 112), 6) AS 売上月
      ,[sykd_uribi] AS 売上日
      ,[sykd_denno] AS 伝票No
	  ,B.[fkjCliMicID] AS 顧客No
      ,[sykd_tcd] AS 得意先No
	  ,U.[顧客名１] + U.[顧客名２] AS 顧客名
	  ,U.[都道府県名] AS 都道府県名
	  ,iif('11' = U.[支店コード] OR '21' = U.[支店コード], '50', iif('31' = U.[支店コード] OR '33' = U.[支店コード], '60', iif('41' = U.[支店コード], '70', iif('51' = U.[支店コード] OR '52' = U.[支店コード], '75', iif('61' = U.[支店コード], '76', '80')))))  AS 営業部コード
	  ,iif('11' = U.[支店コード] OR '21' = U.[支店コード], '東日本営業部', iif('31' = U.[支店コード] OR '33' = U.[支店コード], '関東営業部', iif('41' = U.[支店コード], '首都圏営業部', iif('51' = U.[支店コード] OR '52' = U.[支店コード], '中部営業部', iif('61' = U.[支店コード], '関西営業部', '西日本営業部'))))) AS 営業部名
	  ,U.[支店コード] AS 拠店コード
	  ,U.[支店名] AS 拠店名
      ,'' AS 担当者コード
      ,'' AS  担当者
	  ,U.[営業担当者コード] AS 営業担当者コード
	  ,U.[営業担当者名] AS 営業担当者名
      ,[sykd_tekmei] AS 摘要
      ,[sykd_scd] AS 商品コード
      ,[sykd_mei] AS 商品名
      ,CONVERT(int, [sykd_kingaku]) AS 提供価格
      ,CONVERT(int, [sykd_suryo]) AS 数量
	  ,iif(CO.fContractType is null, '課金', CO.fContractType) AS 区分 
	  ,iif(CO.fContractType is null, '課金', CO.fContractType) AS リプレース
  FROM [JunpDB].[dbo].[vMicPCA売上明細] AS PM
  LEFT JOIN [JunpDB].[dbo].[tMik基本情報] AS B ON B.[fkj得意先情報] = PM.[sykd_tcd]
  LEFT JOIN [JunpDB].[dbo].[vMic全ユーザー2] AS U ON B.[fkjCliMicID] = U.[顧客No]
  LEFT JOIN [charlieDB].[dbo].[T_USE_CONTRACT_HEADER] AS CO ON CO.fCustomerID = B.[fkjCliMicID] and CO.fContractEndDate = CONVERT(datetime, eomonth(CONVERT(datetime, left(sykd_tekmei, 4) + '-' + SUBSTRING(sykd_tekmei, 6, CHARINDEX('月', [sykd_tekmei]) - 6) + '-1'), -1))
  WHERE LEFT([sykd_denno], 1) <> '5' AND ([sykd_scd] = '800155' OR [sykd_scd] = '800156' OR [sykd_scd] = '800157' OR [sykd_scd] = '800158' OR [sykd_scd] = '800159')

UNION ALL

--WonderWeb起票分
SELECT
       LEFT(CONVERT(nvarchar, [sykd_uribi], 112), 6) AS 売上月
      ,[sykd_uribi] AS 売上日
      ,[sykd_denno] AS 伝票No
	  ,JH.[fユーザーコード] AS 顧客No
      ,[sykd_tcd] AS 得意先No
	  ,U.[顧客名１] + U.[顧客名２] AS 顧客名
	  ,U.[都道府県名] AS 都道府県名
	  ,T.[fBshCode2] AS 営業部コード
	  ,T.[fBshName2] AS 営業部名
	  ,U.[支店コード] AS 拠店コード
	  ,U.[支店名] AS 拠店名
      ,JH.[f担当者コード] AS 担当者コード
      ,JH.[f担当者名] AS 担当者
	  ,U.[営業担当者コード] AS 営業担当者コード
	  ,U.[営業担当者名] AS 営業担当者名
      ,[sykd_tekmei] AS 摘要
      ,[sykd_scd] AS 商品コード
      ,[sykd_mei] AS 商品名
      ,CONVERT(int, [sykd_kingaku]) AS 提供価格
      ,CONVERT(int, [sykd_suryo]) AS 数量
	  ,iif(JH.fリプレース = '自社Ｒ', '自社Ｒ', iif(JH.fリプレース = '新規', '新規', iif(JH.fリプレース = '新開', '新開', iif(JH.fリプレース = 'その他' OR JH.fリプレース = 'リプレースなし', '不明', '他社Ｒ')))) AS 区分
	  ,JH.fリプレース AS リプレース
  FROM [JunpDB].[dbo].[vMicPCA売上明細] AS PM
  LEFT JOIN [JunpDB].[dbo].[tMih受注ヘッダ] AS JH ON PM.[sykd_denno] = JH.[f受注番号]
  LEFT JOIN [JunpDB].[dbo].[vMic全ユーザー2] AS U ON JH.[fユーザーコード] = U.[顧客No]
  LEFT JOIN [charlieDB].[dbo].[view_MWSユーザー] AS M ON JH.[fユーザーコード] = M.[顧客ID]
  LEFT JOIN [JunpDB].[dbo].[vMih担当者] AS T ON JH.[f担当者コード] = T.[fUsrID]
  WHERE LEFT([sykd_denno], 1) = '5' AND ([sykd_scd] = '800155' OR [sykd_scd] = '800156' OR [sykd_scd] = '800157' OR [sykd_scd] = '800158' OR [sykd_scd] = '800159') AND JH.f販売種別 = 4

GO

