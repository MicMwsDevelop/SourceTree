SELECT
iif(f���㏳�F�� is null, LEFT(f�[��, 7), LEFT(CONVERT(nvarchar, f���㏳�F��, 111), 7)) AS ���㌎
, H.[fBshCode2] AS �c�ƕ��R�[�h
, B.[fBshName2] AS �c�ƕ���
, H.[fBshCode3] AS ���_�R�[�h
, B.[fBshName3] AS ���_��
, H.f�S���҃R�[�h AS �S���҃R�[�h
, H.f�S���Җ� AS �S����
, H.f�󒍔ԍ� AS �󒍔ԍ�
, H.f���[�U�[�R�[�h AS �ڋqNo
, H.f���[�U�[ AS �ڋq��
, D.f���i�R�[�h AS ���i�R�[�h
, D.f���� AS ����
, H.fSV���p�J�n�N�� AS �ۋ��J�n��
, H.fSV���p�I���N�� AS �ۋ��I����
, CONVERT(int, D.f�W�����i) AS ���z
FROM [JunpDB].[dbo].[tMih�󒍃w�b�_] AS H
LEFT JOIN [JunpDB].[dbo].[tMih�󒍏ڍ�] AS D ON H.f�󒍔ԍ� = D.f�󒍔ԍ�
LEFT JOIN [JunpDB].[dbo].[tBusho] AS B ON B.[fBshCode3] = H.[fBshCode3] AND B.[fBshCode2] <> '05'
WHERE H.f�̔���� = 4 AND (D.f���i�R�[�h = '800155' OR D.f���i�R�[�h = '800156' OR D.f���i�R�[�h = '800157' OR D.f���i�R�[�h = '800158' OR D.f���i�R�[�h = '800159')
