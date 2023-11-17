SELECT
iif(f売上承認日 is null, LEFT(f納期, 7), LEFT(CONVERT(nvarchar, f売上承認日, 111), 7)) AS 売上月
, H.[fBshCode2] AS 営業部コード
, B.[fBshName2] AS 営業部名
, H.[fBshCode3] AS 拠点コード
, B.[fBshName3] AS 拠点名
, H.f担当者コード AS 担当者コード
, H.f担当者名 AS 担当者
, H.f受注番号 AS 受注番号
, H.fユーザーコード AS 顧客No
, H.fユーザー AS 顧客名
, D.f商品コード AS 商品コード
, D.f数量 AS 数量
, H.fSV利用開始年月 AS 課金開始日
, H.fSV利用終了年月 AS 課金終了日
, CONVERT(int, D.f標準価格) AS 金額
FROM [JunpDB].[dbo].[tMih受注ヘッダ] AS H
LEFT JOIN [JunpDB].[dbo].[tMih受注詳細] AS D ON H.f受注番号 = D.f受注番号
LEFT JOIN [JunpDB].[dbo].[tBusho] AS B ON B.[fBshCode3] = H.[fBshCode3] AND B.[fBshCode2] <> '05'
WHERE H.f販売種別 = 4 AND (D.f商品コード = '800155' OR D.f商品コード = '800156' OR D.f商品コード = '800157' OR D.f商品コード = '800158' OR D.f商品コード = '800159')
