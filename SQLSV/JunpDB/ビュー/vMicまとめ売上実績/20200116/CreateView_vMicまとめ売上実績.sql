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
      ,iif(M.�\����� = 1, 'VP', iif(M.�\����� = 2, 'UG', iif(M.�\����� = 3, '�ۋ�', iif(M.�\����� = 4, '�܂Ƃ�', '���̑�')))) AS �敪
	  ,'' AS ���v���[�X�敪
	  ,'' AS ���v���[�X
  FROM [JunpDB].[dbo].[vMicPCA���㖾��] AS PM
  LEFT JOIN [JunpDB].[dbo].[tMik��{���] AS B
  ON B.[fkj���Ӑ���] = PM.[sykd_tcd]
  LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[2] AS U
  ON B.[fkjCliMicID] = U.[�ڋqNo]
  LEFT JOIN [charlieDB].[dbo].[view_MWS���[�U�[] AS M
  ON B.[fkjCliMicID] = M.[�ڋqID]
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
      ,iif(M.�\����� = 1, 'VP', iif(M.�\����� = 2, 'UG', iif(M.�\����� = 3, '�ۋ�', iif(M.�\����� = 4, '�܂Ƃ�', '���̑�')))) AS �敪
	  ,iif(JH.f���v���[�X = '���Ђq', '���Ђq', iif(JH.f���v���[�X = '�V�K' OR JH.f���v���[�X = '�V�J', '�V�K�E�V�J', iif(JH.f���v���[�X = '���̑�' OR JH.f���v���[�X = '���v���[�X�Ȃ�', '�s��', '���Ђq'))) AS ���v���[�X�敪
	  ,JH.f���v���[�X AS ���v���[�X
  FROM [JunpDB].[dbo].[vMicPCA���㖾��] AS PM
  LEFT JOIN [JunpDB].[dbo].[tMih�󒍃w�b�_] AS JH
  ON PM.[sykd_denno] = JH.[f�󒍔ԍ�]
  LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[2] AS U
  ON JH.[f���[�U�[�R�[�h] = U.[�ڋqNo]
  LEFT JOIN [charlieDB].[dbo].[view_MWS���[�U�[] AS M
  ON JH.[f���[�U�[�R�[�h] = M.[�ڋqID]
  WHERE LEFT([sykd_denno], 1) = '5' AND ([sykd_scd] = '800155' OR [sykd_scd] = '800156' OR [sykd_scd] = '800157' OR [sykd_scd] = '800158' OR [sykd_scd] = '800159') AND JH.f�̔���� = 4

GO

