SELECT [COLLECT_DATE] as ���W��
      ,[�c�ƕ��R�[�h]
      ,[�c�ƕ���]
      ,[���_�R�[�h]
      ,[���_��]
      ,[�ڋqNo]
      ,[���Ӑ�No]
      ,[CP_ID] as MWSID
      ,[�ڋq���P]+[�ڋq���Q] as �ڋq��
      ,OP.[service_id] as �T�[�r�XID
	  ,SV.[SERVICE_NAME] as �T�[�r�X��
      --,[child_id]
      --,[use_date]
      ,SUM([use_cnt]) as ���p��
      --,[create_date]
      --,[create_user]
      --,[update_date]
      --,[update_user]
FROM [charlieDB].[dbo].[view_MWS_OperationHistory] as OP
LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[�R] as U on U.[MWS_ID] = OP.[CP_ID]
LEFT JOIN [charlieDB].[dbo].[M_SERVICE] as SV on SV.[SERVICE_ID] = OP.[service_id]
WHERE [CP_ID] like 'MWS%' AND [COLLECT_DATE] = LEFT(CONVERT(nvarchar, DATEADD(dd, 1, EOMONTH(getdate(), -2)), 112), 6)
GROUP BY [COLLECT_DATE], [�c�ƕ��R�[�h], [�c�ƕ���], [���_�R�[�h], [���_��], [CP_ID], [�ڋqNo], [���Ӑ�No], [�ڋq���P]+[�ڋq���Q], OP.[service_id], SV.[SERVICE_NAME]
ORDER BY [�c�ƕ��R�[�h], [���_�R�[�h], [�ڋqNo], OP.[service_id]