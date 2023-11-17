USE [charlieDB]
GO

/****** Object:  View [dbo].[vBPW_オンライン請求作業済申請情報]    Script Date: 2023/11/07 9:20:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*
         BusinessPlanningWeb オンライン請求作業済申請情報

         Ver1.00(2023/12/01 勝呂):オンライン請求作業済申請の医院を抽出し、作業の進捗状況を把握
                                  オンライン請求作業済申請のMWSサイト申込分のみ集計し、WW記票分は集計対象としない
*/
CREATE VIEW [dbo].[vBPW_オンライン請求作業済申請情報]
AS
SELECT 
 D.[CustomerID] as 顧客No
,U.[顧客名１] + U.[顧客名２] as 顧客名
,U.[得意先No] as 得意先コード
,D.[GoodsID] as 商品コード
,S.[sms_mei] as 商品名
,convert(int, S.[sms_hyo]) as 売上金額
,D.[ApplyNo] as 受付No
,D.[ApplyDate] as 申請日時
,D.[SalesDate] as 売上日時
,U.[営業部コード]
,U.[営業部名]
,U.[拠点コード]
,U.[拠点名]
FROM [charlieDB].[dbo].[T_USE_ONLINE_DEMAND] as D
INNER JOIN [JunpDB].[dbo].vMic全ユーザー3 as U on U.[顧客No] = D.[CustomerID]
LEFT JOIN [JunpDB].[dbo].vMicPCA商品マスタ as S on S.[sms_scd] = D.[GoodsID]
WHERE D.[DeleteFlag] is null OR D.[DeleteFlag] = 0

GO


