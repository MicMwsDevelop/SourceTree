SELECT
 H.[fCustomerID] as �ڋqNo
,U.[�ڋq���P] + U.[�ڋq���Q] as �ڋq��
,U.[���Ӑ�No] as ���Ӑ�R�[�h
,U.[������R�[�h] as ������R�[�h
,U.[�����於] as �����於
,H.[fOperationDate] as �^�p�J�n��
,H.[fContractStartDate] as �_��J�n��
,H.[fContractEndDate] as �_��I����
,'��' as �X�V�P��
,M.[sms_scd] as ���i�R�[�h
,M.[sms_mei] as ���i��
,convert(int, M.[sms_hyo]) as �W�����i
,convert(int, M.[sms_gen]) as ���P��
,M.[sms_tani] as �P��
,B.[fPca����R�[�h] as ����R�[�h
,B.[fPca�q�ɃR�[�h] as �q�ɃR�[�h
,B.[f�S���҃R�[�h] as �S���҃R�[�h
--,convert(int, convert(nvarchar, fOperationDate, 112))
--,CONVERT(int, CONVERT(NVARCHAR, DATEADD(dd, 1, EOMONTH(fOperationDate , -1)), 112))
FROM [charlieDB].[dbo].[T_USE_PRESCRIPTION_HEADER] as H
INNER JOIN [JunpDB].[dbo].[vMic���[�U�[��{] as U on H.fCustomerID = U.�ڋqNo
INNER JOIN [JunpDB].[dbo].[vMicPCA���i�}�X�^] as M on M.sms_scd = H.fGoodsID
INNER JOIN [JunpDB].[dbo].[tMih�x�X���] as B on B.fBshCode3 = U.�x�X�R�[�h
WHERE H.[fEndFlag] = '0' AND H.[fDeleteFlag] = '0' AND H.[fOperationDate] is not null AND CONVERT(int, CONVERT(NVARCHAR, DATEADD(dd, 1, EOMONTH(H.[fOperationDate] , -1)), 112)) = 20230101
