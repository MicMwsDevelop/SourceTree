SELECT [COLLECT_DATE] as 収集月
      ,[営業部コード]
      ,[営業部名]
      ,[拠点コード]
      ,[拠点名]
      ,[顧客No]
      ,[得意先No]
      ,[CP_ID] as MWSID
      ,[顧客名１]+[顧客名２] as 顧客名
      ,OP.[service_id] as サービスID
	  ,SV.[SERVICE_NAME] as サービス名
      --,[child_id]
      --,[use_date]
      ,SUM([use_cnt]) as 利用回数
      --,[create_date]
      --,[create_user]
      --,[update_date]
      --,[update_user]
FROM [charlieDB].[dbo].[view_MWS_OperationHistory] as OP
LEFT JOIN [JunpDB].[dbo].[vMic全ユーザー３] as U on U.[MWS_ID] = OP.[CP_ID]
LEFT JOIN [charlieDB].[dbo].[M_SERVICE] as SV on SV.[SERVICE_ID] = OP.[service_id]
WHERE [CP_ID] like 'MWS%' AND [COLLECT_DATE] = LEFT(CONVERT(nvarchar, DATEADD(dd, 1, EOMONTH(getdate(), -2)), 112), 6)
GROUP BY [COLLECT_DATE], [営業部コード], [営業部名], [拠点コード], [拠点名], [CP_ID], [顧客No], [得意先No], [顧客名１]+[顧客名２], OP.[service_id], SV.[SERVICE_NAME]
ORDER BY [営業部コード], [拠点コード], [顧客No], OP.[service_id]