USE [JunpDB]
GO

/****** Object:  View [dbo].[vSoftwareMainteLimit]    Script Date: 2020/09/29 16:46:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
         BusinessPlanningWeb palette ES �\�t�g�E�F�A�ێ痿 �X�V�Ώۈꗗ

         Ver1.00 2020/08/28 �V�K
*/
--CREATE VIEW [dbo].[vSoftwareMainteLimit]
CREATE VIEW [dbo].[vSoftwareMainteLimit]
AS
SELECT 
 U.[�x�X�R�[�h] as ���_�R�[�h
,U.[�x�X��] as ���_��
,CUI.[CUSTOMER_ID] as �ڋqNo
,U.[�ڋq���P] + U.[�ڋq��2] as �ڋq��
,U.[�c�ƒS���Җ�] as �c�ƒS��
,CUI.[SERVICE_ID] as �T�[�r�XID
,SV.[SERVICE_NAME] as �T�[�r�X��
,left(convert(nvarchar, [USE_START_DATE], 111), 7) as ���p�J�n
,left(convert(nvarchar, [USE_END_DATE], 111), 7) as ���p�I��
,iif(U.[�I���t���O] = '1', '�I��', '') as �I��
--,E.[�I����] as �I����
FROM [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION] as CUI
left join [charlieDB].[dbo].[M_SERVICE] as SV on CUI.SERVICE_ID = SV.[SERVICE_ID]
left join [JunpDB].[dbo].[vMic�S���[�U�[2] as U on CUI.[CUSTOMER_ID] = U.[�ڋqNo]
--left join [JunpDB].[dbo].[tMic�I�����[�U�[���X�g] as E on E.���Ӑ�No = U.[���Ӑ�No]
where CUI.[SERVICE_ID] = 9910140

GO

