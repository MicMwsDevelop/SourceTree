SELECT 
  LEFT(CONVERT(NVARCHAR, EOMonth(H.fContractStartDate, -1), 111), 7) As ���㌎
, U.[�c�ƕ��R�[�h] AS �c�ƕ��R�[�h
, U.[�c�ƕ���] AS �c�ƕ���
, U.[���_�R�[�h] AS ���_�R�[�h
, U.[���_��] AS ���_��
, IIF(U.[�c�ƒS���҃R�[�h] is null, '', U.[�c�ƒS���҃R�[�h]) AS �S���҃R�[�h
, IIF(U.[�c�ƒS���Җ�] is null, '', U.[�c�ƒS���Җ�]) AS �S����
, 0 AS �󒍔ԍ�
, H.fCustomerID AS �ڋqNo
, U.�ڋq�� AS �ڋq��
, H.fGoodsID as ���i�R�[�h
, 1 AS ����
, LEFT(CONVERT(NVARCHAR, H.fContractStartDate, 111), 7) AS �ۋ��J�n��
, IIF(H.fBillingEndDate is null, '', LEFT(CONVERT(NVARCHAR, H.fBillingEndDate, 111), 7)) AS �ۋ��I����
, [fTotalAmount] AS ���z
FROM [charlieDB].[dbo].[T_USE_CONTRACT_HEADER] AS H
LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[4] AS U ON H.fCustomerID = U.[�ڋqNo]
WHERE H.fContractType = '�܂Ƃ�'
