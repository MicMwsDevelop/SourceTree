SELECT
 H.[fCustomerID] as ฺqNo
,U.[ฺqผP] + U.[ฺqผQ] as ฺqผ
,U.[พำๆNo] as พำๆR[h
,U.[ฟๆR[h] as ฟๆR[h
,U.[ฟๆผ] as ฟๆผ
,H.[fOperationDate] as ^pJn๚
,H.[fContractStartDate] as _๑Jn๚
,H.[fContractEndDate] as _๑Iน๚
,'' as XVPส
,M.[sms_scd] as คiR[h
,M.[sms_mei] as คiผ
,convert(int, M.[sms_hyo]) as Wฟi
,convert(int, M.[sms_gen]) as ดPฟ
,M.[sms_tani] as Pส
,B.[fPcaๅR[h] as ๅR[h
,B.[fPcaqษR[h] as qษR[h
,B.[fSาR[h] as SาR[h
--,convert(int, convert(nvarchar, fOperationDate, 112))
--,CONVERT(int, CONVERT(NVARCHAR, DATEADD(dd, 1, EOMONTH(fOperationDate , -1)), 112))
FROM [charlieDB].[dbo].[T_USE_PRESCRIPTION_HEADER] as H
INNER JOIN [JunpDB].[dbo].[vMic[U[๎{] as U on H.fCustomerID = U.ฺqNo
INNER JOIN [JunpDB].[dbo].[vMicPCAคi}X^] as M on M.sms_scd = H.fGoodsID
INNER JOIN [JunpDB].[dbo].[tMihxX๎๑] as B on B.fBshCode3 = U.xXR[h
WHERE H.[fEndFlag] = '0' AND H.[fDeleteFlag] = '0' AND H.[fOperationDate] is not null AND CONVERT(int, CONVERT(NVARCHAR, DATEADD(dd, 1, EOMONTH(H.[fOperationDate] , -1)), 112)) = 20230101
