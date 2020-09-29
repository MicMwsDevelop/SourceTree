USE [JunpDB]
GO

/****** Object:  View [dbo].[vMicPC安心サポート売上実績]    Script Date: 2019/11/18 16:43:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[vMicPC安心サポート売上実績]
--ALTER VIEW [dbo].[vMicPC安心サポート売上実績]
AS

/*
    vMicPC安心サポート売上実績

      Ver.1.00 2019/10/10 初版 by 勝呂
      Ver.1.01 2019/10/29 MWSサイト申し込み分を含める by 勝呂
      Ver.1.02 2019/11/18 PC安心ｻﾎﾟｰﾄ(1年契約更新)も含める by 勝呂
*/ 

--MWSサイト申込分(PCA登録分)
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
  FROM [JunpDB].[dbo].[vMicPCA売上明細] AS PM
  LEFT JOIN [JunpDB].[dbo].[tMik基本情報] AS B
  ON B.[fkj得意先情報] = PM.[sykd_tcd]
  LEFT JOIN [JunpDB].[dbo].[vMic全ユーザー2] AS U
  ON B.[fkjCliMicID] = U.[顧客No]
  WHERE LEFT([sykd_denno], 1) = '8' AND [sykd_suryo] > 0 AND ([sykd_scd] = '001871' OR [sykd_scd] = '001872' OR [sykd_scd] = '001874')
  
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
  FROM [JunpDB].[dbo].[vMicPCA売上明細] AS PM
  LEFT JOIN [JunpDB].[dbo].[tMih受注ヘッダ] AS JH
  ON PM.[sykd_denno] = JH.[f受注番号]
  LEFT JOIN [JunpDB].[dbo].[vMic全ユーザー2] AS U
  ON JH.[fユーザーコード] = U.[顧客No]
  WHERE LEFT([sykd_denno], 1) = '5' AND [sykd_suryo] > 0 AND ([sykd_scd] = '001871' OR [sykd_scd] = '001872' OR [sykd_scd] = '001874')


UNION ALL

--MWSサイト申込分(PCA未登録分)
SELECT LEFT(CONVERT(nvarchar, [fContractStartDate], 112), 6) AS 売上月
,CONVERT(nvarchar, PC.[fContractStartDate], 112) AS 売上日
,'' AS 伝票No
,[fCustomerID] AS 顧客No
,U.[得意先No] AS 得意先No
,U.[顧客名１] + U.[顧客名２] AS 顧客名
,U.[都道府県名] AS 都道府県名
,U.[支店コード] AS 拠店コード
,U.[支店名] AS 拠店名
,'' AS 担当者コード
,'' AS 担当者
,U.[営業担当者コード] AS 営業担当者コード
,U.[営業担当者名] AS 営業担当者名
,'' AS 摘要
,PC.[fGoodsID] AS 商品コード
,M.[商品名] AS 商品名
,CONVERT(int, M.[標準価格]) AS 提供価格
,'1' AS 数量
 FROM [charlieDB].[dbo].[T_USE_PCCSUPPORT] AS PC
 LEFT JOIN [JunpDB].[dbo].[vMic全ユーザー2] AS U
 ON PC.fCustomerID = U.[顧客No]
 LEFT JOIN [charlieDB].[dbo].[PCA商品マスタ参照ビュー] AS M
 ON PC.[fGoodsID] = M.商品ID
WHERE [fBillingStartDate] is null

GO

