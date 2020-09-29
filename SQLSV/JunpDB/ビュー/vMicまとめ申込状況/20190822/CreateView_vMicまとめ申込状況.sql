USE [JunpDB]
GO

/****** Object:  View [dbo].[vMic�܂Ƃߐ\����]    Script Date: 2019/08/22 11:54:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--CREATE VIEW [dbo].[vMic�܂Ƃߐ\����]
ALTER VIEW [dbo].[vMic�܂Ƃߐ\����]
AS
/*
    vMic�܂Ƃߐ\����

      Ver.1.00 2019/08/22 ����

*/
SELECT
  LEFT(CONVERT(NVARCHAR, EOMONTH(H.fContractStartDate , -1), 111), 7) AS ���㌎
  ,H.fCustomerID AS �ڋqNo
  ,M.���Ӑ�R�[�h AS ���Ӑ�No
  ,M.�ڋq�� AS �ڋq��
  ,M.�x�X�R�[�h AS �x�X�R�[�h
  ,M.�x�X�� AS �x�X��
  ,iif(SA.�c�ƒS���Җ� is null, '', SA.�c�ƒS���Җ�) AS �c�ƒS��
  ,M.�ۋ��ΏۊO���[�U�[ AS �I���t���O
  ,C.���p�V�X�e���� AS �V�X�e������
  ,iif(C.�O�V�X�e������ is null, '', C.�O�V�X�e������) AS �O�V�X�e������
  ,H.fTotalAmount AS �_����z
  ,CONVERT(NVARCHAR, H.fApplyDate, 111) AS �\����
  ,H.fMonths AS ����
  ,H.fContractType AS �_��^�C�v
  ,CONVERT(NVARCHAR, H.fContractStartDate, 111) AS �_��J�n��
  ,iif(H.fBillingEndDate is null, '', CONVERT(NVARCHAR, H.fBillingEndDate, 111)) AS �ۋ��I����
  ,'' AS �񖼂Ȃ�
  ,iif(M.�\����� = 1, 'VP', iif(M.�\����� = 2, 'UG', iif(M.�\����� = 3, '�ۋ�', iif(M.�\����� = 4, '�܂Ƃ�', '���̑�')))) AS �敪
  FROM charlieDB.dbo.T_USE_CONTRACT_HEADER AS H
  LEFT JOIN charlieDB.dbo.view_MWS���[�U�[ AS M
  ON H.fCustomerID = M.�ڋqID
  LEFT JOIN charlieDB.dbo.�ڋq�}�X�^�Q�ƃr���[ AS C
  ON H.fCustomerID = C.�ڋq�h�c
  LEFT JOIN dbo.vMic�c�ƒS�� AS SA 
  ON H.fCustomerID = SA.�ڋqNo
  /* �_��J�n�� >= �������� */
  WHERE H.fContractType = '�܂Ƃ�' AND CONVERT(date, DATEADD(dd, 1, EOMONTH(getdate()))) <= CONVERT(date, H.fContractStartDate)
GO
