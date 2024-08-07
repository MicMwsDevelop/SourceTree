USE [JunpDB]
GO

/****** Object:  View [dbo].[vMicまとめ売上実績]    Script Date: 2019/10/10 12:48:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vMicまとめ売上実績]
--ALTER VIEW [dbo].[vMicまとめ売上実績]
AS

/*
    vMicまとめ売上実績

      Ver.1.00 2019/10/10 初版 by 勝呂

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
      ,iif(M.申込種別 = 1, 'VP', iif(M.申込種別 = 2, 'UG', iif(M.申込種別 = 3, '課金', iif(M.申込種別 = 4, 'まとめ', 'その他')))) AS 区分
	  ,'' AS リプレース区分
	  ,'' AS リプレース
  FROM [JunpDB].[dbo].[vMicPCA売上明細] AS PM
  LEFT JOIN [JunpDB].[dbo].[tMik基本情報] AS B
  ON B.[fkj得意先情報] = PM.[sykd_tcd]
  LEFT JOIN [JunpDB].[dbo].[vMic全ユーザー2] AS U
  ON B.[fkjCliMicID] = U.[顧客No]
  LEFT JOIN [charlieDB].[dbo].[view_MWSユーザー] AS M
  ON B.[fkjCliMicID] = M.[顧客ID]
  WHERE LEFT([sykd_denno], 1) = '8' AND [sykd_suryo] > 0 AND ([sykd_scd] = '800155' OR [sykd_scd] = '800156' OR [sykd_scd] = '800157' OR [sykd_scd] = '800158' OR [sykd_scd] = '800159')

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
      ,iif(M.申込種別 = 1, 'VP', iif(M.申込種別 = 2, 'UG', iif(M.申込種別 = 3, '課金', iif(M.申込種別 = 4, 'まとめ', 'その他')))) AS 区分
	  ,iif(JH.fリプレース = '自社Ｒ', '自社Ｒ', iif(JH.fリプレース = '新規' OR JH.fリプレース = '新開', '新規・新開', iif(JH.fリプレース = 'その他' OR JH.fリプレース = 'リプレースなし', '不明', '他社Ｒ'))) AS リプレース区分
	  ,JH.fリプレース AS リプレース
  FROM [JunpDB].[dbo].[vMicPCA売上明細] AS PM
  LEFT JOIN [JunpDB].[dbo].[tMih受注ヘッダ] AS JH
  ON PM.[sykd_denno] = JH.[f受注番号]
  LEFT JOIN [JunpDB].[dbo].[vMic全ユーザー2] AS U
  ON JH.[fユーザーコード] = U.[顧客No]
  LEFT JOIN [charlieDB].[dbo].[view_MWSユーザー] AS M
  ON JH.[fユーザーコード] = M.[顧客ID]
  WHERE LEFT([sykd_denno], 1) = '5' AND [sykd_suryo] > 0 AND ([sykd_scd] = '800155' OR [sykd_scd] = '800156' OR [sykd_scd] = '800157' OR [sykd_scd] = '800158' OR [sykd_scd] = '800159')

GO

