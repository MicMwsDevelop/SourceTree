/****** SSMS の SelectTopNRows コマンドのスクリプト  ******/
SELECT [SERVICE_ID]
	  ,[GOODS_ID]
  FROM [charlieDB].[dbo].[M_CODE]
  where [SET_SALE] = 1
  ORDER BY [SERVICE_ID] ASC