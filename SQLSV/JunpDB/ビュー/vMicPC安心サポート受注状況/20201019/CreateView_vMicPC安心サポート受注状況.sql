USE [JunpDB]
GO

/****** Object:  View [dbo].[vMicPC���S�T�|�[�g�󒍏�]    Script Date: 2020/10/19 15:08:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--CREATE VIEW [dbo].[vMicPC���S�T�|�[�g�󒍏�]
ALTER VIEW [dbo].[vMicPC���S�T�|�[�g�󒍏�]
AS
/*
    vMicPC���S�T�|�[�g�󒍏�

      Ver.1.00 2019/08/21 ����
	  Ver.1.01 2020/10/19 PC���S�T�|�[�gPlus�ɑΉ� by ���C
*/
SELECT
	iif(H.f���㏳�F�� is null, '�����F', LEFT(CONVERT(NVARCHAR, H.f���㏳�F��, 111), 7)) AS ���㌎
	,CONVERT(VARCHAR, H.f�󒍓�, 111) AS �󒍓�
	,H.[f�[��] AS �[��
	,D.f���i�R�[�h AS ���i�R�[�h
	,D.f���i�� AS ���i��
	,D.[f����] AS ����
	,CONVERT(int, D.[f�W�����i]) * D.[f����] AS ���z
	,H.f�󒍔ԍ� AS �󒍔ԍ�
	,H.f���[�U�[�R�[�h AS �ڋqID
	,CL.fCliName AS ��@��
	,iif(SA.�c�ƒS���Җ� is null, H.f�S���Җ�, SA.�c�ƒS���Җ�) AS �S���c��
	,H.fBshCode3 AS ����
	,H.f�S���x�X�� AS ���X��
	,CONVERT(VARCHAR, H.f���㏳�F��, 111) AS ���㏳�F��
	,H.f���v���[�X AS ���v���[�X
    ,REPLACE(REPLACE(REPLACE(H.f���l, CHAR(13), ''), CHAR(10), ''), CHAR(9), '') as ���l
FROM dbo.tMih�󒍃w�b�_ AS H
LEFT JOIN dbo.tMih�󒍏ڍ� AS D ON H.f�󒍔ԍ� = D.f�󒍔ԍ� AND H.f�N�x = D.f�N�x
LEFT JOIN dbo.tClient AS CL ON H.f���[�U�[�R�[�h = CL.[fCliID]
LEFT JOIN dbo.vMic�c�ƒS�� AS SA ON H.f���[�U�[�R�[�h = SA.�ڋqNo
--WHERE (D.f���i�R�[�h = '001871' OR D.f���i�R�[�h = '001872') AND H.f�󒍋��z > 0
WHERE H.f�󒍋��z > 0 AND D.f���i�R�[�h IN ('001871', '001872', '101871', '101872')

GO

