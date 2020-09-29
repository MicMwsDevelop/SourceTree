USE [JunpDB]
GO

/****** Object:  View [dbo].[vMic�̔����я��]    Script Date: 2019/10/30 11:34:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






--CREATE VIEW [dbo].[vMic�̔����я��]
ALTER VIEW [dbo].[vMic�̔����я��]
--ALTER VIEW [dbo].[�����p�쐬���r���[]
AS
/*

  �r���[����   vMic�̔����я��
    2011/07/21 Ver.1.1 ���[�X���捇�v�A�󒍋��z3 ��ǉ�
    2011/07/27 Ver.1.2 ��R�l�����̊��ԑΏۃo�O�C��
    2011/09/02 Ver.1.3 ���v���[�X�敪�Ɂu�݌Ɂv��ǉ�
    2012/08/08 Ver.1.4 [�㉺��]��ǉ� (1=����A2=����)
    2014/10/02 Ver.1.5 [�G���A]�̕��ނ� '�����{�O���[�v','��s���O���[�v','�����{�O���[�v' �ɕύX
    2016/02/06 Ver.1.6 [�G���A]�̕��ނ� '�����{�O���[�v','��s���O���[�v�P','��s���O���[�v�Q','�����{�O���[�v' �ɕύX
    2019/11/21 Ver.1.7 [�G���A]�̕��ނ� '�����{�c�ƕ�','�֓��c�ƕ�','��s���c�ƕ�','�����c�ƕ�,'�֐��c�ƕ�','�����{�c�ƕ�'�ɕύX

*/
SELECT
      �̔�����.�󒍔ԍ�                                       AS �󒍔ԍ�
    , YEAR(�̔�����.���㏳�F��)                               AS ���㏳�F�N
    , MONTH(�̔�����.���㏳�F��)                              AS ���㏳�F��
    , �̔�����.���㏳�F��                                     AS ���㏳�F��
    , �̔�����.�̔���R�[�h                                   AS �̔���R�[�h
    , RTRIM(Client.fCliName + ' ' + ��{���.fkj�ڋq���Q)     AS �̔���
    , �㗝�X���[�X���.fdl�̔��X�O���[�v                      AS �̔��X�O���[�v�R�[�h
    , (CASE WHEN (Pca���Ӑ�.fpt���Ӑ�敪1<80) OR (Pca���Ӑ�.fpt���Ӑ�敪1=98) THEN
            ISNULL(�̔��X�O���[�v�}�X�^.fcm����, RTRIM(Client.fCliName + ' ' + ��{���.fkj�ڋq���Q))
            ELSE '��' END)                                    AS �̔��X�O���[�v
    , RTRIM(vMic���Ӑ�敪�P.ems_str)                         AS ���Ӑ�敪�P
    , RTRIM(vMic���Ӑ�敪�Q.ems_str)                         AS ���Ӑ�敪�Q
    , �x������.�x����R�[�h                                   AS �x����R�[�h
    , �x������.�x�����הԍ�                                   AS �x�����הԍ�
    , �x������.�x����WW�ڋqNo                                 AS �x����WW�ڋqNo
    , �x������.�x���於                                       AS �x���於
    , �x������.�d���於                                       AS �d���於
    , �x������.�x����ڋq�敪                                 AS �x����ڋq�敪
    , RTRIM(vMic�x���擾�Ӑ�敪�Q.ems_str)                   AS �x���擾�Ӑ�敪�Q
    , �̔�����.���[�U�[�R�[�h                                 AS ���[�U�[�R�[�h
    , �̔�����.���[�U�[                                       AS ���[�U�[
    , �̔�����.�S���҃R�[�h                                   AS �S���҃R�[�h
    , �̔�����.�S���Җ�                                       AS �S���Җ�
    , �̔�����.����R�[�h                                     AS ����R�[�h
    , �̔�����.�����R�[�h                                     AS �����R�[�h
 --   2016/02/06 �Ȃ����x�X��񂩂�\�����Ă����̂ŁA�̔����т̓`�[�S���x�X��\�����邱�Ƃɂ���
 -- , ISNULL(���_��.f�x�X��,'���̑�')                         AS �S���x�X��
    , �̔�����.�S���x�X��                                     AS �S���x�X��
    , �̔�����.�󒍋��z                                       AS �󒍋��z
    , �̔�����.�V�X�e������                                   AS �V�X�e������
    , �̔�����.�\�t�g�̔����i                                 AS �\�t�g�̔����i
    , �x������.�x�����z                                       AS �x�����z
    , �x������.�x�����z�Ŕ�                                   AS �x�����z�Ŕ�
    , �̔�����.�V�X�e������ + �x������.�x�����z�Ŕ�           AS �Љ���_����z
    , �̔�����.�e���z                                         AS �e���z
    , �̔�����.���i�R�[�h                                     AS ���i�R�[�h
    , �̔�����.���i��                                         AS ���i��
    , �̔�����.����                                           AS ����
    , �̔�����.�敪2                                          AS ���i�敪�Q
    , (CASE WHEN (ISNULL(�̔�����.���v���[�X�敪,0) = 0) THEN '���̑�'
            WHEN (�̔�����.���v���[�X�敪=1) THEN '���Ђq'
            WHEN (�̔�����.���v���[�X�敪=2) THEN '�V�K'
            WHEN (�̔�����.���v���[�X�敪=3) THEN '�V�J'
            WHEN (�̔�����.���v���[�X�敪=4) THEN '�݌�'
            ELSE '���Ђq' END)                                AS ���v���[�X�敪
    , �̔�����.���v���[�X                                     AS ���v���[�X
    , �̔�����.���ԍ�                                         AS ���ԍ�
    , �̔�����.�s���{����                                     AS �s���{����
    , �Ј��}�X�^.fUsrYaku                                     AS ��E�R�[�h
    , ((99-ISNULL(�Ј��}�X�^.fUsrYaku,0))*10000
        +LEFT(CASE WHEN (�̔�����.�S���҃R�[�h) like 'qr-%' THEN REPLACE((�̔�����.�S���҃R�[�h),'qr-','9')
                   WHEN (�̔�����.�S���҃R�[�h) like 'SP%'  THEN REPLACE((�̔�����.�S���҃R�[�h),'SP', '9')
                   ELSE (�̔�����.�S���҃R�[�h) END,4))       AS �\�����R�[�h
    -- 2014/10/02 Ver.1.5 [�G���A]�̕��ނ� '�����{�O���[�v','��s���O���[�v','�����{�O���[�v' �ɕύX
    , (case
           when �̔�����.���㏳�F��<'2014-08-01' then
              (CASE WHEN �̔�����.�����R�[�h>=20 THEN 'MET'
                    ELSE 'SAT' END)
           when �̔�����.���㏳�F��<'2015-08-01' then
              (CASE WHEN �̔�����.�����R�[�h<=07 THEN '�����{�O���[�v'
                    WHEN �̔�����.�����R�[�h>=60 and �̔�����.�����R�[�h<=69 THEN '��s���O���[�v'
                    ELSE '�����{�O���[�v' END)
           when �̔�����.���㏳�F��<'2017-08-01' then
              (CASE �̔�����.����R�[�h
                  when 50 THEN '�����{�O���[�v'
                  when 60 THEN '��s���O���[�v�P'
                  when 70 THEN '��s���O���[�v�Q'
                  when 80 THEN '�����{�O���[�v'
                  else 
                    (case
                       when �̔�����.�����R�[�h = 61
					     or �̔�����.�����R�[�h = 63
						 or �̔�����.�����R�[�h = 64 THEN '��s���O���[�v�P'
                       when �̔�����.�����R�[�h = 62
					     or �̔�����.�����R�[�h = 65 THEN '��s���O���[�v�Q'
                       else '���̑�'
                     end)
                  END)
           when �̔�����.���㏳�F��<'2019-10-01' then
              (CASE �̔�����.����R�[�h
                  when 50 THEN '�����{�c�ƕ�'
                  when 60 THEN '�֓��c�ƕ�'
                  when 70 THEN '��s���c�ƕ�'
                  when 75 THEN '�֐������c�ƕ�'
                  when 80 THEN '�����{�c�ƕ�'
                  else '���̑�'
                  END)
           --2019/11/21 Ver.1.7 [�G���A]�̕��ނ� '�����{�c�ƕ�','�֓��c�ƕ�','��s���c�ƕ�','�����c�ƕ�,'�֐��c�ƕ�','�����{�c�ƕ�'�ɕύX
           else
              (CASE �̔�����.����R�[�h
                  when 50 THEN '�����{�c�ƕ�'
                  when 60 THEN '�֓��c�ƕ�'
                  when 70 THEN '��s���c�ƕ�'
                  when 75 THEN '�����c�ƕ�'
                  when 76 THEN '�֐��c�ƕ�'
                  when 80 THEN '�����{�c�ƕ�'
                  else '���̑�'
              END)
       end)                                                   AS �G���A
    , �Ј��}�X�^.fBshCode3                                    AS ���ݕ����R�[�h
    , �Ј��}�X�^.fBshName3                                    AS ���ݎx�X��
    , (CASE WHEN �̔�����.���㏳�F�� IS NULL THEN NULL
            ELSE (YEAR(�̔�����.���㏳�F��)-
                   (CASE WHEN MONTH(�̔�����.���㏳�F��)<8 THEN 1975
                         ELSE 1974 END)) END)                 AS ��v�N�x
    , (CASE WHEN �̔�����.���㏳�F�� IS NULL THEN NULL
            WHEN MONTH(�̔�����.���㏳�F��)= 8 OR MONTH(�̔�����.���㏳�F��)= 9 OR MONTH(�̔�����.���㏳�F��)=10 THEN 1
            WHEN MONTH(�̔�����.���㏳�F��)=11 OR MONTH(�̔�����.���㏳�F��)=12 OR MONTH(�̔�����.���㏳�F��)= 1 THEN 2
            WHEN MONTH(�̔�����.���㏳�F��)= 2 OR MONTH(�̔�����.���㏳�F��)= 3 OR MONTH(�̔�����.���㏳�F��)= 4 THEN 3
            ELSE 4 END)                                       AS �l����
    , (CASE WHEN (Pca���Ӑ�.fpt���Ӑ�敪1 < 80) OR (Pca���Ӑ�.fpt���Ӑ�敪1 = 98) THEN 3
            WHEN �x������.�x�����z<>0                                               THEN 2
            ELSE 1 END)                                       AS �̔��敪
    , �̔�����.���[�X���捇�v                                 AS ���[�X���捇�v
    , �̔�����.�󒍋��z3                                      AS �󒍋��z3
    , (CASE WHEN �̔�����.���㏳�F�� IS NULL THEN NULL
            WHEN MONTH(�̔�����.���㏳�F��)= 8 OR MONTH(�̔�����.���㏳�F��)= 9 OR MONTH(�̔�����.���㏳�F��)=10 OR
                 MONTH(�̔�����.���㏳�F��)=11 OR MONTH(�̔�����.���㏳�F��)=12 OR MONTH(�̔�����.���㏳�F��)= 1 THEN 1
            ELSE 2 END)                                       AS �㉺��
FROM
    dbo.tMikPca���Ӑ�             Pca���Ӑ�                               LEFT OUTER JOIN
    dbo.vMicPCA�敪�}�X�^         vMic���Ӑ�敪�Q
      ON vMic���Ӑ�敪�Q.ems_id = 12 AND
         Pca���Ӑ�.fpt���Ӑ�敪2 = vMic���Ӑ�敪�Q.ems_kbn              LEFT OUTER JOIN
    dbo.vMicPCA�敪�}�X�^         vMic���Ӑ�敪�P
      ON vMic���Ӑ�敪�P.ems_id = 11 AND
         Pca���Ӑ�.fpt���Ӑ�敪1 = vMic���Ӑ�敪�P.ems_kbn              RIGHT OUTER JOIN
    dbo.vMicPCA�敪�}�X�^         vMic�x���擾�Ӑ�敪�Q                  RIGHT OUTER JOIN
    dbo.vMic�̔��萔���x������    �x������                                LEFT OUTER JOIN
    dbo.tMikPca���Ӑ�             �x����Pca���Ӑ�
      ON �x������.�x����WW�ڋqNo = �x����Pca���Ӑ�.fptCliMicID
      ON vMic�x���擾�Ӑ�敪�Q.ems_id = 12 AND
         vMic�x���擾�Ӑ�敪�Q.ems_kbn = �x����Pca���Ӑ�.fpt���Ӑ�敪2  RIGHT OUTER JOIN
    dbo.vMic�̔�����              �̔�����                                LEFT OUTER JOIN
    dbo.vMic�S����                �Ј��}�X�^
      ON �̔�����.�S���҃R�[�h = �Ј��}�X�^.fUsrID                        LEFT OUTER JOIN
    dbo.tMih�x�X���              ���_��
      ON �̔�����.�����R�[�h = ���_��.fBshCode3 AND
         ���_��.fBshCode1 = 01 AND
         �̔�����.����R�[�h = ���_��.fBshCode2                           LEFT OUTER JOIN
    dbo.tMik��{���              ��{���
      ON �̔�����.�̔���R�[�h = ��{���.fkjCliMicID
      ON �x������.�󒍔ԍ� = �̔�����.�󒍔ԍ�
      ON Pca���Ӑ�.fptCliMicID = �̔�����.�̔���R�[�h                    LEFT OUTER JOIN
    dbo.tClient                   Client
      ON �̔�����.�̔���R�[�h = Client.fCliID                            LEFT OUTER JOIN
    dbo.tMik�R�[�h�}�X�^          �̔��X�O���[�v�}�X�^                    RIGHT OUTER JOIN
    dbo.tMik�㗝�X���[�X���      �㗝�X���[�X���
      ON �̔��X�O���[�v�}�X�^.fcm�R�[�h��� = 12 AND
         �̔��X�O���[�v�}�X�^.fcm�R�[�h = �㗝�X���[�X���.fdl�̔��X�O���[�v
      ON �̔�����.�̔���R�[�h = �㗝�X���[�X���.fdlCliMicID





GO

