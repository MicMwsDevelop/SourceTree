SELECT
 H.[fCustomerID] as 顧客No
,U.[顧客名１] + U.[顧客名２] as 顧客名
,U.[得意先No] as 得意先コード
,U.[請求先コード] as 請求先コード
,U.[請求先名] as 請求先名
,H.[fOperationDate] as 運用開始日
,H.[fContractStartDate] as 契約開始日
,H.[fContractEndDate] as 契約終了日
,'月' as 更新単位
,M.[sms_scd] as 商品コード
,M.[sms_mei] as 商品名
,convert(int, M.[sms_hyo]) as 標準価格
,convert(int, M.[sms_gen]) as 原単価
,M.[sms_tani] as 単位
,B.[fPca部門コード] as 部門コード
,B.[fPca倉庫コード] as 倉庫コード
,B.[f担当者コード] as 担当者コード
--,convert(int, convert(nvarchar, fOperationDate, 112))
--,CONVERT(int, CONVERT(NVARCHAR, DATEADD(dd, 1, EOMONTH(fOperationDate , -1)), 112))
FROM [charlieDB].[dbo].[T_USE_PRESCRIPTION_HEADER] as H
INNER JOIN [JunpDB].[dbo].[vMicユーザー基本] as U on H.fCustomerID = U.顧客No
INNER JOIN [JunpDB].[dbo].[vMicPCA商品マスタ] as M on M.sms_scd = H.fGoodsID
INNER JOIN [JunpDB].[dbo].[tMih支店情報] as B on B.fBshCode3 = U.支店コード
WHERE H.[fEndFlag] = '0' AND H.[fDeleteFlag] = '0' AND H.[fOperationDate] is not null AND CONVERT(int, CONVERT(NVARCHAR, DATEADD(dd, 1, EOMONTH(H.[fOperationDate] , -1)), 112)) = 20230101
