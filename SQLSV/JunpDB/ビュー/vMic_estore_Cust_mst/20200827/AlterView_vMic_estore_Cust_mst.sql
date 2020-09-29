USE [JunpDB]
GO

/****** Object:  View [dbo].[vMic_estore_Cust_mst]    Script Date: 2020/07/30 10:20:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








--CREATE VIEW [dbo].[vMic_estore_Cust_mst]
ALTER VIEW [dbo].[vMic_estore_Cust_mst]
/*
    vMic_estore_Cust_mst
      eStore ���[�U�[��� �Q�Ɨp�r���[

        Ver1.0 2010/07/06 ����
        Ver1.1 2018/03/13 ������^���ڂ� RTRIM ����
		Ver1.2 2020/08/27 estore���ւɔ����A�����}�K�w�ǂ��Q����WW�̈�@���̃����}�K�w�ǃt���O���~�ŕ\������Ă��܂��̂ŁA2��1�ŏo�͂���悤�ɏC�� by ���C
*/
AS
SELECT C.�ڋqNo                       as �ڋqNo
      ,RTRIM(C.�ڋq��)                as �ڋq��
      ,C.�V�X�e���R�[�h               as �V�X�e���R�[�h
      ,RTRIM(C.�V�X�e������)          as �V�X�e������
      ,RTRIM(C.���x�����@)            as ���x�����@
      ,C.�̎��؃R�[�h                 as �̎��؃R�[�h
      ,RTRIM(C.�̎��ؗp��)            as �̎��ؗp��
      ,C.�J���e�R�[�h                 as �J���e�R�[�h
      ,RTRIM(C.�J���e�p��)            as �J���e�p��
      ,C.�g�i�[�R�[�h                 as �g�i�[�R�[�h
      ,RTRIM(C.�g�i�[)                as �g�i�[
      ,C.���ԍ�                       as ���ԍ�
      ,RTRIM(C.�s���{����)            as �s���{����
      ,C.���i���C�Z���X���s�ς݃t���O as ���i���C�Z���X���s�ς݃t���O
      ,RTRIM(C.�p�X���[�h)            as �p�X���[�h
      ,RTRIM(C.�p�X���[�h�ǂ�)        as �p�X���[�h�ǂ�
      ,RTRIM(C.���[���A�h���X)        as ���[���A�h���X

      --,C.�����}�K�w��                 as �����}�K�w��
      ,iif(C.�����}�K�w��=0, 0, 1)    as �����}�K�w��

      ,C.�V�K�o�^����                 as �V�K�o�^����
      ,C.�ŏI���O�C������             as �ŏI���O�C������
FROM estoreDB.dbo.tMicCust_mst as C
