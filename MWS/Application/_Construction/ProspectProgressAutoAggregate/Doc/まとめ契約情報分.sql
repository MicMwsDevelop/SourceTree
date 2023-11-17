SELECT 
  LEFT(CONVERT(NVARCHAR, EOMonth(H.fContractStartDate, -1), 111), 7) As 売上月
, U.[営業部コード] AS 営業部コード
, U.[営業部名] AS 営業部名
, U.[拠点コード] AS 拠点コード
, U.[拠点名] AS 拠点名
, IIF(U.[営業担当者コード] is null, '', U.[営業担当者コード]) AS 担当者コード
, IIF(U.[営業担当者名] is null, '', U.[営業担当者名]) AS 担当者
, 0 AS 受注番号
, H.fCustomerID AS 顧客No
, U.顧客名 AS 顧客名
, H.fGoodsID as 商品コード
, 1 AS 数量
, LEFT(CONVERT(NVARCHAR, H.fContractStartDate, 111), 7) AS 課金開始日
, IIF(H.fBillingEndDate is null, '', LEFT(CONVERT(NVARCHAR, H.fBillingEndDate, 111), 7)) AS 課金終了日
, [fTotalAmount] AS 金額
FROM [charlieDB].[dbo].[T_USE_CONTRACT_HEADER] AS H
LEFT JOIN [JunpDB].[dbo].[vMic全ユーザー4] AS U ON H.fCustomerID = U.[顧客No]
WHERE H.fContractType = 'まとめ'
