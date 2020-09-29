USE [JunpDB]
GO

/****** Object:  View [dbo].[vUriagesyounin]    Script Date: 2019/12/03 15:27:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




--CREATE VIEW [dbo].[vUriagesyounin]
ALTER VIEW [dbo].[vUriagesyounin]
AS
SELECT
  H.fBshCode3 AS 拠点コード
, H.f担当支店名 AS 拠点名
, iif(D.f商品コード = '800121', 'ES', choose(H.f販売種別+1, 'その他', 'その他', 'その他', '月額課金', 'まとめ', 'その他')) AS 伝票種別
, TA.fBshName2 AS エリア
, H.f担当者名 AS 担当者
, iif(H.f納期 is null, '-', H.f納期) AS 納期
, H.f販売先 AS 販売先
, H.fユーザー AS ユーザー
, H.f受注金額 AS 受注金額
, H.f受注番号 AS 受注No
, H.f受注日 AS 受注日
, H.f出荷完了日 AS 出荷完了日
, H.f売上承認日 AS 売上承認日
, H.f入金予定日 AS 入金予定日
, H.f件名 AS 件名
, H.fリプレース AS リプレース
, iif(H.fリプレース = 'リプレースなし', 'なし', 'あり') AS リプレース有無
, iif(D.f商品コード = '800121', D.f数量, 0) AS ES本数
, H.fBshCode2 AS 営業部コード
, H.f受注承認日 AS 受注承認日
, CASE WHEN H.f受注承認日 is not null AND H.f出荷完了日 is null THEN '△' ELSE CASE WHEN H.f受注承認日 is not null AND H.f出荷完了日 is not null THEN '○' ELSE '×' END END AS 承認
, D.f商品コード AS システム商品コード
, D.f商品名 AS システム商品名
FROM JunpDB.dbo.tMih受注ヘッダ AS H
LEFT JOIN JunpDB.dbo.tMih受注詳細 AS D ON H.f受注番号 = D.f受注番号 AND (D.f区分 = 1 OR D.f区分 = 202 OR D.f区分 = 204)
LEFT JOIN JunpDB.dbo.tBusho AS TA ON H.fBshCode2 = TA.fBshCode2 AND H.fBshCode3 = TA.fBshCode3
WHERE H.f売上承認日 is not null AND YEAR(H.f売上承認日) = YEAR(GETDATE()) AND MONTH(H.f売上承認日) = MONTH(GETDATE())

GO

