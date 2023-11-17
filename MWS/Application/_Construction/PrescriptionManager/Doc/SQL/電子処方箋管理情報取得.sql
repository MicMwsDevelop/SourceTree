/****** SSMS の SelectTopNRows コマンドのスクリプト  ******/
SELECT [fContractID] as 申込No
      ,[fCustomerID] as 顧客No
			,U.[顧客名１]+U.[顧客名２] as 顧客名
      ,[fGoodsID] as 商品ID
			,M.[sms_mei] as 商品名
      ,[fOrderReserveID] as 注文ID
      ,[fApplyDate] as 申込日時
      ,[fOperationDate] as 運用開始日
      ,[fContractStartDate] as 契約開始日
      ,[fContractEndDate] as 契約終了日
      ,[fBillingStartDate] as 課金開始日
      ,[fBillingEndDate] as 課金終了日
      ,[fEndFlag] as 終了フラグ
      ,[fDeleteFlag] as 削除フラグ
      ,[fCreateDate] as 作成日
      ,[fCreatePerson] as 作成者
      ,[fUpdateDate] as 更新日
      ,[fUpdatePerson] as 更新者
  FROM [charlieDB].[dbo].[T_USE_PRESCRIPTION_HEADER] as H
	LEFT JOIN [JunpDB].[dbo].[vMicユーザー基本] as U on U.[顧客No] = H.[fCustomerID]
	LEFT JOIN [JunpDB].[dbo].[vMicPCA商品マスタ] as M on M.[sms_scd] = H.[fGoodsID]
	ORDER BY [fContractID] ASC