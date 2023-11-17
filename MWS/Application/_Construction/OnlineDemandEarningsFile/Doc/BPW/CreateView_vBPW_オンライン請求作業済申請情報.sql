USE [charlieDB]
GO

/****** Object:  View [dbo].[vBPW_�I�����C��������ƍϐ\�����]    Script Date: 2023/11/07 9:20:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*
         BusinessPlanningWeb �I�����C��������ƍϐ\�����

         Ver1.00(2023/12/01 ���C):�I�����C��������ƍϐ\���̈�@�𒊏o���A��Ƃ̐i���󋵂�c��
                                  �I�����C��������ƍϐ\����MWS�T�C�g�\�����̂ݏW�v���AWW�L�[���͏W�v�ΏۂƂ��Ȃ�
*/
CREATE VIEW [dbo].[vBPW_�I�����C��������ƍϐ\�����]
AS
SELECT 
 D.[CustomerID] as �ڋqNo
,U.[�ڋq���P] + U.[�ڋq���Q] as �ڋq��
,U.[���Ӑ�No] as ���Ӑ�R�[�h
,D.[GoodsID] as ���i�R�[�h
,S.[sms_mei] as ���i��
,convert(int, S.[sms_hyo]) as ������z
,D.[ApplyNo] as ��tNo
,D.[ApplyDate] as �\������
,D.[SalesDate] as �������
,U.[�c�ƕ��R�[�h]
,U.[�c�ƕ���]
,U.[���_�R�[�h]
,U.[���_��]
FROM [charlieDB].[dbo].[T_USE_ONLINE_DEMAND] as D
INNER JOIN [JunpDB].[dbo].vMic�S���[�U�[3 as U on U.[�ڋqNo] = D.[CustomerID]
LEFT JOIN [JunpDB].[dbo].vMicPCA���i�}�X�^ as S on S.[sms_scd] = D.[GoodsID]
WHERE D.[DeleteFlag] is null OR D.[DeleteFlag] = 0

GO


