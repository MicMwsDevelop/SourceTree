USE [JunpDB]
GO

/****** Object:  View [dbo].[vMicPC安心サポート受注状況]    Script Date: 2020/10/19 15:08:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--CREATE VIEW [dbo].[vMicPC安心サポート受注状況]
ALTER VIEW [dbo].[vMicPC安心サポート受注状況]
AS
/*
    vMicPC安心サポート受注状況

      Ver.1.00 2019/08/21 初版
	  Ver.1.01 2020/10/19 PC安心サポートPlusに対応 by 勝呂
*/
SELECT
	iif(H.f売上承認日 is null, '未承認', LEFT(CONVERT(NVARCHAR, H.f売上承認日, 111), 7)) AS 売上月
	,CONVERT(VARCHAR, H.f受注日, 111) AS 受注日
	,H.[f納期] AS 納期
	,D.f商品コード AS 商品コード
	,D.f商品名 AS 商品名
	,D.[f数量] AS 数量
	,CONVERT(int, D.[f標準価格]) * D.[f数量] AS 金額
	,H.f受注番号 AS 受注番号
	,H.fユーザーコード AS 顧客ID
	,CL.fCliName AS 医院名
	,iif(SA.営業担当者名 is null, H.f担当者名, SA.営業担当者名) AS 担当営業
	,H.fBshCode3 AS 部署
	,H.f担当支店名 AS 拠店名
	,CONVERT(VARCHAR, H.f売上承認日, 111) AS 売上承認日
	,H.fリプレース AS リプレース
    ,REPLACE(REPLACE(REPLACE(H.f備考, CHAR(13), ''), CHAR(10), ''), CHAR(9), '') as 備考
FROM dbo.tMih受注ヘッダ AS H
LEFT JOIN dbo.tMih受注詳細 AS D ON H.f受注番号 = D.f受注番号 AND H.f年度 = D.f年度
LEFT JOIN dbo.tClient AS CL ON H.fユーザーコード = CL.[fCliID]
LEFT JOIN dbo.vMic営業担当 AS SA ON H.fユーザーコード = SA.顧客No
--WHERE (D.f商品コード = '001871' OR D.f商品コード = '001872') AND H.f受注金額 > 0
WHERE H.f受注金額 > 0 AND D.f商品コード IN ('001871', '001872', '101871', '101872')

GO

