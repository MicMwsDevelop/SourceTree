SELECT
iif(f���㏳�F�� is null, LEFT(f�[��, 7), LEFT(CONVERT(nvarchar, f���㏳�F��, 111), 7)) AS ���㌎
, U.�c�ƕ��R�[�h AS �c�ƕ��R�[�h
, U.�c�ƕ��� AS �c�ƕ���
, U.���_�R�[�h AS ���_�R�[�h
, U.���_�� AS ���_��
, f�S���҃R�[�h AS �S���҃R�[�h
, f�S���Җ� AS �S����
, H.f�󒍔ԍ� AS �󒍔ԍ�
, f���[�U�[�R�[�h AS �ڋqNo
, f���[�U�[ AS �ڋq��
, f���i�R�[�h AS ���i�R�[�h
, f���� AS ����
, fSV���p�J�n�N�� AS �ۋ��J�n��
, fSV���p�I���N�� AS �ۋ��I����
, CONVERT(int, f�W�����i) AS ���z
FROM [JunpDB].[dbo].[tMih�󒍃w�b�_] AS H
LEFT JOIN [JunpDB].[dbo].[tMih�󒍏ڍ�] AS D ON H.f�󒍔ԍ� = D.f�󒍔ԍ�
LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[4] AS U ON H.f���[�U�[�R�[�h = U.�ڋqNo
WHERE (f���i�R�[�h = '800155' OR f���i�R�[�h = '800156' OR f���i�R�[�h = '800157' OR f���i�R�[�h = '800158' OR f���i�R�[�h = '800159') AND f�̔���� = 4