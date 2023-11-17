SELECT
iif(f売上承認日 is null, LEFT(f納期, 7), LEFT(CONVERT(nvarchar, f売上承認日, 111), 7)) AS 売上月
, U.営業部コード AS 営業部コード
, U.営業部名 AS 営業部名
, U.拠点コード AS 拠点コード
, U.拠点名 AS 拠点名
, f担当者コード AS 担当者コード
, f担当者名 AS 担当者
, H.f受注番号 AS 受注番号
, fユーザーコード AS 顧客No
, fユーザー AS 顧客名
, f商品コード AS 商品コード
, f数量 AS 数量
, fSV利用開始年月 AS 課金開始日
, fSV利用終了年月 AS 課金終了日
, CONVERT(int, f標準価格) AS 金額
FROM [JunpDB].[dbo].[tMih受注ヘッダ] AS H
LEFT JOIN [JunpDB].[dbo].[tMih受注詳細] AS D ON H.f受注番号 = D.f受注番号
LEFT JOIN [JunpDB].[dbo].[vMic全ユーザー4] AS U ON H.fユーザーコード = U.顧客No
WHERE (f商品コード = '800155' OR f商品コード = '800156' OR f商品コード = '800157' OR f商品コード = '800158' OR f商品コード = '800159') AND f販売種別 = 4