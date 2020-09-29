USE [JunpDB]
GO

/****** Object:  View [dbo].[vMicPC���S�T�|�[�g�������]    Script Date: 2019/10/29 12:42:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[vMicPC���S�T�|�[�g�������]
AS

/*
    vMicPC���S�T�|�[�g�������

      Ver.1.00 2019/10/10 ���� by ���C
      Ver.1.01 2019/10/29 MWS�T�C�g�\�����ݕ����܂߂� by ���C

*/ 

--MWS�T�C�g�\����(PCA�o�^��)
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
  FROM [JunpDB].[dbo].[vMicPCA���㖾��] AS PM
  LEFT JOIN [JunpDB].[dbo].[tMik��{���] AS B
  ON B.[fkj���Ӑ���] = PM.[sykd_tcd]
  LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[2] AS U
  ON B.[fkjCliMicID] = U.[�ڋqNo]
  WHERE LEFT([sykd_denno], 1) = '8' AND [sykd_suryo] > 0 AND ([sykd_scd] = '001871' OR [sykd_scd] = '001872')
  
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
  FROM [JunpDB].[dbo].[vMicPCA���㖾��] AS PM
  LEFT JOIN [JunpDB].[dbo].[tMih�󒍃w�b�_] AS JH
  ON PM.[sykd_denno] = JH.[f�󒍔ԍ�]
  LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[2] AS U
  ON JH.[f���[�U�[�R�[�h] = U.[�ڋqNo]
  WHERE LEFT([sykd_denno], 1) = '5' AND [sykd_suryo] > 0 AND ([sykd_scd] = '001871' OR [sykd_scd] = '001872')


UNION ALL

--MWS�T�C�g�\����(PCA���o�^��)
SELECT LEFT(CONVERT(nvarchar, [fContractStartDate], 112), 6) AS ���㌎
,CONVERT(nvarchar, PC.[fContractStartDate], 112) AS �����
,'' AS �`�[No
,[fCustomerID] AS �ڋqNo
,U.[���Ӑ�No] AS ���Ӑ�No
,U.[�ڋq���P] + U.[�ڋq���Q] AS �ڋq��
,U.[�s���{����] AS �s���{����
,U.[�x�X�R�[�h] AS ���X�R�[�h
,U.[�x�X��] AS ���X��
,'' AS �S���҃R�[�h
,'' AS �S����
,U.[�c�ƒS���҃R�[�h] AS �c�ƒS���҃R�[�h
,U.[�c�ƒS���Җ�] AS �c�ƒS���Җ�
,'' AS �E�v
,PC.[fGoodsID] AS ���i�R�[�h
,M.[���i��] AS ���i��
,CONVERT(int, M.[�W�����i]) AS �񋟉��i
,'1' AS ����
 FROM [charlieDB].[dbo].[T_USE_PCCSUPPORT] AS PC
 LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[2] AS U
 ON PC.fCustomerID = U.[�ڋqNo]
 LEFT JOIN [charlieDB].[dbo].[PCA���i�}�X�^�Q�ƃr���[] AS M
 ON PC.[fGoodsID] = M.���iID
WHERE [fBillingStartDate] is null

GO

