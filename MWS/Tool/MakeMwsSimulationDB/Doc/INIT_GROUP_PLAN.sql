/****** SSMS の SelectTopNRows コマンドのスクリプト  ******/
SELECT MSET.[SET_ID] AS GroupID
      ,MSET.[SET_NM] AS GroupName
      --,[COMMENT]
      --,[SET_START_DATE]
      --,[SET_END_DATE]
      --,[DEL_FLG]
      --,[CREATE_DATE]
      --,[CREATE_USER]
      --,[UPDATE_DATE]
      --,[UPDATE_USER]
	  ,SET_SERVICE.SERVICE_ID AS ServiceCode
  FROM [COUPLER].[dbo].[M_SET] AS MSET
  LEFT JOIN [COUPLER].[dbo].[M_SET_SERVICE] AS SET_SERVICE
  ON SET_SERVICE.SET_ID = MSET.SET_ID
