USE [JunpDB]
GO

/****** Object:  View [dbo].[vMic�u�o�`�[���o����]    Script Date: 2019/11/22 10:32:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--CREATE VIEW [dbo].[vMic�u�o�`�[���o����]
ALTER VIEW [dbo].[vMic�u�o�`�[���o����]
AS
/*
    vMic�u�o�`�[���o����

      Ver.1.00 2017/01/05 ����
      Ver.1.10 2017/06/22 �󒍌���,���o�^���󒍗\��
      Ver.1.20 2018/05/31 �ԓ`�[�̌����𔽉f���邽��[�V�X�e���{��]���ڂ�ǉ�
      Ver.1.21 2018/11/22 �p�b�N�敪�ɂ܂Ƃ߂��܂߂�
      Ver.1.22 2019/09/19 �p�b�N�敪�̂܂Ƃ߂̔����800158 MWS ���܂Ƃ�����48�P���A800159 MWS ���܂Ƃ�����60�P�����܂܂�Ă��Ȃ������̂Ŕ�����@��PCA���i�R�[�h���珤�i�敪3�ɕύX by ���C
      Ver.1.23 2019/11/22 �p�b�N�敪��paletteES��ǉ� by ���C

      �[�����t���O���`�����Ŕ̔���ʁ��u�o�̓`�[�𒊏o����

*/
  select              iif(D.f���i�R�[�h = '800121', '�d�r', iif(D.f���i�R�[�h = '800152', '�p�o', iif(D .f�敪 = 204, '�܂Ƃ�', '�u�o'))) AS �p�b�N�敪
                        , iif(H.f���v���[�X = '���Ђq', '���Ђq', iif(H.f���v���[�X = '�V�K' OR H.f���v���[�X = '�V�J', '�V�K�E�V�J', iif(H.f���v���[�X = '���̑�' OR H.f���v���[�X = '���v���[�X�Ȃ�', '�s��', '���Ђq'))) AS ���v���[�X�敪
                        , H.f���v���[�X AS ���v���[�X
                        , H.f�S���x�X�� AS �S���x�X��
                        , H.f�S���Җ� AS �S���Җ�
                        , CONVERT(nvarchar, H.f�󒍏��F��, 111) AS �󒍏��F��
                        , CONVERT(nvarchar, H.f�o�׊�����, 111) AS �o�׊�����
                        , CONVERT(nvarchar, H.f���㏳�F��, 111) AS ���㏳�F��
                        , H.f�[�� AS �[��
                        , iif(H.f���㏳�F�� IS NOT NULL, '����', iif(H.f�󒍏��F�� IS NOT NULL, '�󒍏��F', iif((H.f���� LIKE '%�쐬��%') OR (H.f���� LIKE '%�󒍌���%') OR (H.f���� LIKE '%���o�^%'), '�󒍗\��', '��'))) AS �X�e�[�^�X
                        , H.f�󒍔ԍ� AS �󒍔ԍ�
                        , H.f�Č��h�c AS �Č��h�c
                        , H.f�󒍋��z AS �󒍋��z
                        , D .f���� AS �V�X�e���{��
                        , H.f�̔��� AS �̔���
                        , H.f���[�U�[�R�[�h AS ���[�U�[�R�[�h
                        , H.f���[�U�[ AS ���[�U�[
                        , H.fSV���p�I���N�� AS VP���p�I���N��
                        , H.f���� AS ����
                        , H.f�x���˗����l AS �x���˗����l
                        , H.f���l AS ���l
                        , iif((((H.f���㏳�F�� > eomonth(getdate(), - 2)) AND (H.f���㏳�F�� <= eomonth(getdate(), - 1))) /* ���㏳�F���O�� ���邢��*/ OR 
                              ((H.f���㏳�F�� IS NULL) /* ���㖢���F��*/ AND (((H.f�o�׊����� > eomonth(getdate(), - 2)) AND (H.f�o�׊����� <= eomonth(getdate(), - 1)))))), - 1, 
                              /* �o�׊��������O��   �� -1:�O����*/ iif((((H.f���㏳�F�� > eomonth(getdate(), - 1)) AND (H.f���㏳�F�� <= eomonth(getdate(), 0))) /* ���㏳�F������ ���邢��*/ OR
                             ((H.f���㏳�F�� IS NULL) /* ���㖢���F��*/ AND (((H.f�o�׊����� > eomonth(getdate(), - 1)) AND (H.f�o�׊����� <= eomonth(getdate(), 0))) /* �o�׊����������� ���邢��*/ OR
                             (((H.f�[�� > eomonth(getdate(), - 1)) AND (H.f�[�� <= eomonth(getdate(), 0))))))), 0, /* �[��������         �� 0:������*/ iif(((H.f���㏳�F�� IS NULL) /* ���㖢���F��*/ AND (((H.f�o�׊����� > eomonth(getdate(), 0)) 
                             AND (H.f�o�׊����� <= eomonth(getdate(), 1))) /* �o�׊�����������  ���邢��*/ OR
                             ((H.f�[�� > eomonth(getdate(), 0)) AND (H.f�[�� <= eomonth(getdate(), 1))))), 1, 999))) /* �[��������         �� +1:������*/ AS �O����������/* -1:�O�����A0:�������A+1:������*/
                        , H.fBshCode3 AS �x�X�R�[�h
FROM              JunpDB.dbo.tMih�󒍃w�b�_ H INNER JOIN
                        JunpDB.dbo.tMih�󒍏ڍ� D ON D .f�󒍔ԍ� = H.f�󒍔ԍ�
WHERE             ((H.f�̔���� = 1 OR H.f�̔���� = 4) AND (D .f�敪 = 202 OR D .f�敪 = 204)) AND (((H.f���㏳�F�� > eomonth(getdate(), - 2)) AND (H.f���㏳�F�� <= eomonth(getdate(), 1))) /* ���㏳�F���O���`�����`���� ���邢��*/ OR
                        ((H.f���㏳�F�� IS NULL) /* ���㖢���F��*/ AND (((H.f�o�׊����� > eomonth(getdate(), - 2)) AND ((H.f�o�׊����� <= eomonth(getdate(), 1)))) /* �o�׊��������O���`���� ���邢��*/ OR
                        (((H.f�[�� > eomonth(getdate(), - 1)) AND (H.f�[�� <= eomonth(getdate(), 1)))))))

GO
