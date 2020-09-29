USE [JunpDB]
GO

/****** Object:  View [dbo].[vMisyounin]    Script Date: 2019/12/03 15:27:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
/*
         BusinessPlanningWeb ＷｏｎｄｅｒＷｅｂ販売管理－受注残(受注未承認/受注承認)一覧

         Ver2.00 2019/11/29 改修
         Ver2.01 2019/12/03 改修
         Ver2.02 2020/01/31 改修
         Ver2.03 2020/08/21 伝票種別=まとめ時に申込種別のまとめの抽出から800160）MWSおまとめﾌﾟﾗﾝ(ﾘｰｽ･立替契約)が含まれる伝票に変更
                                          理由：まとめ販売のローン契約の際、顧客請求を防止するための赤伝票検出のため（経理部：大橋依頼）
*/
--CREATE VIEW [dbo].[vMisyounin]
ALTER VIEW [dbo].[vMisyounin]
AS
SELECT
  H.fBshCode3 AS 拠点コード
, CASE WHEN H.f受注承認日 is not null AND H.f出荷完了日 is null THEN '△' ELSE CASE WHEN H.f受注承認日 is not null AND H.f出荷完了日 is not null THEN '○' ELSE '×' END END AS 承認
--, iif(D.f商品コード = '800121', 'ES', choose(H.f販売種別+1, 'その他', 'その他', 'その他', '月額課金', 'まとめ', 'その他')) AS 伝票種別
,iif(D.f商品コード = '800121', 'ES', iif(D.f商品コード = '800160', 'まとめ', choose(H.f販売種別+1, 'その他', 'VP', 'UG', '月額課金', 'まとめ', 'PC安心'))) AS 伝票種別
, TA.fBshName2 AS エリア
, iif(H.f納期 is null, '-', LEFT(H.f納期, 7)) AS 納期年月
, H.f担当支店名 AS 拠点名
, H.f担当者名 AS 担当者
, iif(H.f納期 is null, '-', H.f納期) AS 納期
, H.f販売先 AS 販売先
, H.fユーザー AS ユーザー
, H.f受注金額 AS 受注金額
, H.f受注番号 AS 受注No
, H.f受注日 AS 受注日
, H.f受注承認日 AS 受注承認日
, H.f出荷完了日 AS 出荷完了日
, H.f入金予定日 AS 入金予定日
, H.f件名 AS 件名
, H.fリプレース AS リプレース
, iif(H.fリプレース = 'リプレースなし', 'なし', 'あり') AS リプレース有無
, iif(D.f商品コード = '800121', D.f数量, 0) AS ES本数
, H.[fSV利用開始年月] + '～' + H.[fSV利用終了年月] AS ｻｰﾋﾞｽ利用期間
, H.fBshCode2 AS 営業部コード
, D.f商品コード AS システム商品コード
, D.f商品名 AS システム商品名
FROM JunpDB.[dbo].tMih受注ヘッダ AS H
LEFT JOIN JunpDB.dbo.tBusho AS TA ON H.fBshCode2 = TA.fBshCode2 AND H.fBshCode3 = TA.fBshCode3
LEFT JOIN JunpDB.dbo.tMih受注詳細 AS D ON H.f受注番号 = D.f受注番号 AND (D.f区分 = 1 OR D.f区分 = 202 OR D.f区分 = 204)
WHERE H.f売上承認日 is null

GO

