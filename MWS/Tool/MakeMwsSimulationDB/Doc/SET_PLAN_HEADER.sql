/****** SSMS の SelectTopNRows コマンドのスクリプト  ******/
SELECT [GOODS_ID]
      --,[BRAND_CLASSIFICATION]
      ,[GOODS_NAME]
      ,[PRICE]
      --,[COST_PRICE]
      --,[NON_WHOLESALE]
      --,[NON_SALE_AT_OFFER]
      --,[DOWNLOAD_GOODS]
      --,[CONTRACT_TYPE]
  FROM [charlieDB].[dbo].[V_PCA_GOODS]
  where [BRAND_CLASSIFICATION] = 206