SELECT 
  iif(f���㏳�F�� is null, left(f�[��, 7), LEFT(CONVERT(nvarchar, f���㏳�F��, 111), 7)) as ���㌎
, H.f�󒍔ԍ� as �󒍔ԍ�
, convert(nvarchar, f�󒍓�, 111) as �󒍓�
, f�̔���R�[�h as �̔���R�[�h
, f���[�U�[�R�[�h as ���[�U�[�R�[�h
, f�̔��� as �̔���
, f���[�U�[ as ���[�U�[
, convert(int, f�󒍋��z) as �󒍋��z
, f���� as ����
, f�[�� as �[��
, f���v���[�X�敪 as ���v���[�X�敪
, f���v���[�X as ���v���[�X
, f�S���҃R�[�h as �S���҃R�[�h
, f�S���Җ� as �S���Җ�
, fBshCode2 as BshCode2
, fBshCode3 as BshCode3
, f�S���x�X�� as �S���x�X��
, convert(nvarchar, f�󒍏��F��, 111) as �󒍏��F��
, convert(nvarchar, f���㏳�F��, 111) as ���㏳�F��
, f�����敪 as �����敪
, f�̔��X�R�[�h as �̔��X�R�[�h
, f�̔��X as �̔��X
, f�̔���� as �̔����
, fSV���p�J�n�N�� as SV���p�J�n�N��
, fSV���p�I���N�� as SV���p�I���N��
FROM [JunpDB].[dbo].[tMih�󒍃w�b�_] AS H
LEFT JOIN [JunpDB].[dbo].[tMih�󒍏ڍ�] AS D ON H.f�󒍔ԍ� = D.f�󒍔ԍ�
LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[4] AS U ON H.f���[�U�[�R�[�h = U.�ڋqNo
WHERE f���i�R�[�h = '800121' AND f�̔���� = 1
ORDER BY ���㌎, H.f�󒍔ԍ�
