USE [JunpDB]
GO

/****** Object:  View [dbo].[vMic�܂Ƃߔ������]    Script Date: 2019/10/10 12:48:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--CREATE VIEW [dbo].[vMic�܂Ƃߔ������]
ALTER VIEW [dbo].[vMic�܂Ƃߔ������]
AS
/*
    vMic�܂Ƃߔ������

      Ver.1.00 2019/10/10 ���� by ���C
      Ver.1.01 2020/03/04 �c�ƕ��R�[�h�Ɖc�ƕ����̃t�B�[���h�ǉ� by ���C

*/ 
--MWS�T�C�g�\����
SELECT 
	  LEFT(CONVERT(nvarchar, [sykd_uribi], 112), 6) AS ���㌎
      ,[sykd_uribi] AS �����
      ,[sykd_denno] AS �`�[No
	  ,B.[fkjCliMicID] AS �ڋqNo
      ,[sykd_tcd] AS ���Ӑ�No
	  ,U.[�ڋq���P] + U.[�ڋq���Q] AS �ڋq��
	  ,U.[�s���{����] AS �s���{����
	  ,iif('11' = U.[�x�X�R�[�h] OR '21' = U.[�x�X�R�[�h], '50', iif('31' = U.[�x�X�R�[�h] OR '33' = U.[�x�X�R�[�h], '60', iif('41' = U.[�x�X�R�[�h], '70', iif('51' = U.[�x�X�R�[�h] OR '52' = U.[�x�X�R�[�h], '75', iif('61' = U.[�x�X�R�[�h], '76', '80')))))  AS �c�ƕ��R�[�h
	  ,iif('11' = U.[�x�X�R�[�h] OR '21' = U.[�x�X�R�[�h], '�����{�c�ƕ�', iif('31' = U.[�x�X�R�[�h] OR '33' = U.[�x�X�R�[�h], '�֓��c�ƕ�', iif('41' = U.[�x�X�R�[�h], '��s���c�ƕ�', iif('51' = U.[�x�X�R�[�h] OR '52' = U.[�x�X�R�[�h], '�����c�ƕ�', iif('61' = U.[�x�X�R�[�h], '�֐��c�ƕ�', '�����{�c�ƕ�'))))) AS �c�ƕ���
	  ,U.[�x�X�R�[�h] AS ���X�R�[�h
	  ,U.[�x�X��] AS ���X��
      ,'' AS �S���҃R�[�h
      ,'' AS  �S����
	  ,U.[�c�ƒS���҃R�[�h] AS �c�ƒS���҃R�[�h
	  ,U.[�c�ƒS���Җ�] AS �c�ƒS���Җ�
      ,[sykd_tekmei] AS �E�v
      ,[sykd_scd] AS ���i�R�[�h
      ,[sykd_mei] AS ���i��
      ,CONVERT(int, [sykd_kingaku]) AS �񋟉��i
      ,CONVERT(int, [sykd_suryo]) AS ����
	  ,iif(CO.fContractType is null, '�ۋ�', CO.fContractType) AS �敪 
	  ,iif(CO.fContractType is null, '�ۋ�', CO.fContractType) AS ���v���[�X
  FROM [JunpDB].[dbo].[vMicPCA���㖾��] AS PM
  LEFT JOIN [JunpDB].[dbo].[tMik��{���] AS B ON B.[fkj���Ӑ���] = PM.[sykd_tcd]
  LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[2] AS U ON B.[fkjCliMicID] = U.[�ڋqNo]
  LEFT JOIN [charlieDB].[dbo].[T_USE_CONTRACT_HEADER] AS CO ON CO.fCustomerID = B.[fkjCliMicID] and CO.fContractEndDate = CONVERT(datetime, eomonth(CONVERT(datetime, left(sykd_tekmei, 4) + '-' + SUBSTRING(sykd_tekmei, 6, CHARINDEX('��', [sykd_tekmei]) - 6) + '-1'), -1))
  WHERE LEFT([sykd_denno], 1) <> '5' AND ([sykd_scd] = '800155' OR [sykd_scd] = '800156' OR [sykd_scd] = '800157' OR [sykd_scd] = '800158' OR [sykd_scd] = '800159')

UNION ALL

--WonderWeb�N�[��
SELECT
       LEFT(CONVERT(nvarchar, [sykd_uribi], 112), 6) AS ���㌎
      ,[sykd_uribi] AS �����
      ,[sykd_denno] AS �`�[No
	  ,JH.[f���[�U�[�R�[�h] AS �ڋqNo
      ,[sykd_tcd] AS ���Ӑ�No
	  ,U.[�ڋq���P] + U.[�ڋq���Q] AS �ڋq��
	  ,U.[�s���{����] AS �s���{����
	  ,T.[fBshCode2] AS �c�ƕ��R�[�h
	  ,T.[fBshName2] AS �c�ƕ���
	  ,U.[�x�X�R�[�h] AS ���X�R�[�h
	  ,U.[�x�X��] AS ���X��
      ,JH.[f�S���҃R�[�h] AS �S���҃R�[�h
      ,JH.[f�S���Җ�] AS �S����
	  ,U.[�c�ƒS���҃R�[�h] AS �c�ƒS���҃R�[�h
	  ,U.[�c�ƒS���Җ�] AS �c�ƒS���Җ�
      ,[sykd_tekmei] AS �E�v
      ,[sykd_scd] AS ���i�R�[�h
      ,[sykd_mei] AS ���i��
      ,CONVERT(int, [sykd_kingaku]) AS �񋟉��i
      ,CONVERT(int, [sykd_suryo]) AS ����
	  ,iif(JH.f���v���[�X = '���Ђq', '���Ђq', iif(JH.f���v���[�X = '�V�K', '�V�K', iif(JH.f���v���[�X = '�V�J', '�V�J', iif(JH.f���v���[�X = '���̑�' OR JH.f���v���[�X = '���v���[�X�Ȃ�', '�s��', '���Ђq')))) AS �敪
	  ,JH.f���v���[�X AS ���v���[�X
  FROM [JunpDB].[dbo].[vMicPCA���㖾��] AS PM
  LEFT JOIN [JunpDB].[dbo].[tMih�󒍃w�b�_] AS JH ON PM.[sykd_denno] = JH.[f�󒍔ԍ�]
  LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[2] AS U ON JH.[f���[�U�[�R�[�h] = U.[�ڋqNo]
  LEFT JOIN [charlieDB].[dbo].[view_MWS���[�U�[] AS M ON JH.[f���[�U�[�R�[�h] = M.[�ڋqID]
  LEFT JOIN [JunpDB].[dbo].[vMih�S����] AS T ON JH.[f�S���҃R�[�h] = T.[fUsrID]
  WHERE LEFT([sykd_denno], 1) = '5' AND ([sykd_scd] = '800155' OR [sykd_scd] = '800156' OR [sykd_scd] = '800157' OR [sykd_scd] = '800158' OR [sykd_scd] = '800159') AND JH.f�̔���� = 4

GO

