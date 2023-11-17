SELECT 
  iif(f売上承認日 is null, left(f納期, 7), LEFT(CONVERT(nvarchar, f売上承認日, 111), 7)) as 売上月
, H.f受注番号 as 受注番号
, convert(nvarchar, f受注日, 111) as 受注日
, f販売先コード as 販売先コード
, fユーザーコード as ユーザーコード
, f販売先 as 販売先
, fユーザー as ユーザー
, convert(int, f受注金額) as 受注金額
, f件名 as 件名
, f納期 as 納期
, fリプレース区分 as リプレース区分
, fリプレース as リプレース
, f担当者コード as 担当者コード
, f担当者名 as 担当者名
, fBshCode2 as BshCode2
, fBshCode3 as BshCode3
, f担当支店名 as 担当支店名
, convert(nvarchar, f受注承認日, 111) as 受注承認日
, convert(nvarchar, f売上承認日, 111) as 売上承認日
, f請求区分 as 請求区分
, f販売店コード as 販売店コード
, f販売店 as 販売店
, f販売種別 as 販売種別
, fSV利用開始年月 as SV利用開始年月
, fSV利用終了年月 as SV利用終了年月
FROM [JunpDB].[dbo].[tMih受注ヘッダ] AS H
LEFT JOIN [JunpDB].[dbo].[tMih受注詳細] AS D ON H.f受注番号 = D.f受注番号
LEFT JOIN [JunpDB].[dbo].[vMic全ユーザー4] AS U ON H.fユーザーコード = U.顧客No
WHERE f商品コード = '800121' AND f販売種別 = 1
ORDER BY 売上月, H.f受注番号
