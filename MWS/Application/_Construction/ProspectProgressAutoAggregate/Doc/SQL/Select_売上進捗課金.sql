SELECT
  iif(f���㏳�F�� is null, LEFT(f�[��, 7), LEFT(CONVERT(nvarchar, f���㏳�F��, 111), 7)) as ���㌎
, H.f�󒍔ԍ� as �󒍔ԍ�
, CONVERT(nvarchar, f�󒍓�, 111) as �󒍓�
, f�̔���R�[�h as �̔���R�[�h
, f���[�U�[�R�[�h as ���[�U�[�R�[�h
, f�̔��� as �̔���
, f���[�U�[ as ���[�U�[
, CONVERT(int, f�󒍋��z) as �󒍋��z
, f���� as ����
, f�[�� as �[��
, f���v���[�X�敪 as ���v���[�X�敪
, f���v���[�X as ���v���[�X
, f�S���҃R�[�h as �S���҃R�[�h
, f�S���Җ� as �S���Җ�
, fBshCode2 as BshCode2
, fBshCode3 as BshCode3
, f�S���x�X�� as �S���x�X��
, CONVERT(nvarchar, f�󒍏��F��, 111) as �󒍏��F��
, CONVERT(nvarchar, f���㏳�F��, 111) as ���㏳�F��
, f�����敪 as �����敪
, f�̔��X�R�[�h as �̔��X�R�[�h
, f�̔��X as �̔��X
, f�̔���� as �̔����
, fSV���p�J�n�N�� as �ۋ��J�n��
, fSV���p�I���N�� as �ۋ��I����
FROM tMih�󒍃w�b�_ AS H
LEFT JOIN tMih�󒍏ڍ� AS D ON H.f�󒍔ԍ� = D.f�󒍔ԍ�
WHERE f���i�R�[�h = '800001' AND f�̔���� = 3
ORDER BY ���㌎, H.f�󒍔ԍ�
