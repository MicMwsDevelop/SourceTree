USE [JunpDB]
GO

/****** Object:  View [dbo].[vMicES�ێ甄��Ǘ�]    Script Date: 2020/01/09 16:36:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vMicES�ێ甄��Ǘ�]
--ALTER VIEW [dbo].[vMicES�ێ甄��Ǘ�]
AS
SELECT
 U.[�c�ƕ��R�[�h] AS �c�ƕ��R�[�h
,U.[�c�ƕ���] AS �c�ƕ���
,ES.[���_�R�[�h] AS ���_�R�[�h
,ES.���_�� AS ���_��
,ES.�S���҃R�[�h AS �S���҃R�[�h
,ES.�S���Җ� AS �S���Җ�
,U.[�I���t���O] AS �I���t���O
,ES.�ڋqNo AS �ڋqNo
,U.[���Ӑ�No] AS ���Ӑ�No
,ES.�ڋq�� AS �ڋq��
,ES.[ES_���p�I���N��] AS �g�p��������
,left(convert(nvarchar, ES.[ES_���㏳�F��], 111), 7) AS ���㌎
,ES.����ŗ�
,iif(ES.[�ێ�_���i�R�[�h] = '800161', 6, 1) as �ێ琿���敪
,left(convert(nvarchar, ES.[ES_���㏳�F��], 111), 7) AS �v��1�N��
,left(convert(nvarchar, dateadd(month, 11, ES.[ES_���㏳�F��]), 111), 7) AS �v��2�N��
,left(convert(nvarchar, dateadd(month, 23, ES.[ES_���㏳�F��]), 111), 7) AS �v��3�N��
,left(convert(nvarchar, dateadd(month, 35, ES.[ES_���㏳�F��]), 111), 7) AS �v��4�N��
,left(convert(nvarchar, dateadd(month, 47, ES.[ES_���㏳�F��]), 111), 7) AS �v��5�N��
,left(convert(nvarchar, dateadd(month, 59, ES.[ES_���㏳�F��]), 111), 7) AS �v��6�N��
,convert(int, M.sms_hyo) AS ����1�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', 0, convert(int, M.sms_hyo)) AS ����2�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', 0, convert(int, M.sms_hyo)) AS ����3�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', 0, convert(int, M.sms_hyo)) AS ����4�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', 0, convert(int, M.sms_hyo)) AS ����5�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', 0, convert(int, M.sms_hyo)) AS ����6�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', convert(int, M.sms_hyo) / 6, convert(int, M.sms_hyo)) AS ����1�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', convert(int, M.sms_hyo) / 6, convert(int, M.sms_hyo)) AS ����2�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', convert(int, M.sms_hyo) / 6, convert(int, M.sms_hyo)) AS ����3�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', convert(int, M.sms_hyo) / 6, convert(int, M.sms_hyo)) AS ����4�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', convert(int, M.sms_hyo) / 6, convert(int, M.sms_hyo)) AS ����5�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', convert(int, M.sms_hyo) / 6, convert(int, M.sms_hyo)) AS ����6�N��
,0 as �O���O��c��1�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', (convert(int, M.sms_hyo) / 6) * 5, 0) as �O���O��c��2�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', (convert(int, M.sms_hyo) / 6) * 4, 0) as �O���O��c��3�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', (convert(int, M.sms_hyo) / 6) * 3, 0) as �O���O��c��4�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', convert(int, M.sms_hyo) / 6 * 2, 0) as �O���O��c��5�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', convert(int, M.sms_hyo) / 6, 0) as �O���O��c��6�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', (convert(int, M.sms_hyo) / 6) * 5, 0) as �����O��c��1�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', (convert(int, M.sms_hyo) / 6) * 4, 0) as �����O��c��2�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', (convert(int, M.sms_hyo) / 6) * 3, 0) as �����O��c��3�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', (convert(int, M.sms_hyo) / 6) * 2, 0) as �����O��c��4�N��
,iif(ES.[�ێ�_���i�R�[�h] = '800161', convert(int, M.sms_hyo) / 6, 0) as �����O��c��5�N��
,0 as �����O��c��6�N��
,ES.[ES_�󒍔ԍ�] AS ES_�󒍔ԍ�
,ES.[�ێ�_�󒍔ԍ�] AS �ێ�_�󒍔ԍ�
,ES.[�ێ�_���i�R�[�h] AS �ێ�_���i�R�[�h
FROM [JunpDB].[dbo].[vMicES�󒍓`�[] AS ES
LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[�R] AS U ON U.[�ڋqNo] = ES.[�ڋqNo]
LEFT JOIN [JunpDB].[dbo].[vMicPCA���i�}�X�^] AS M ON M.[sms_scd] = ES.[�ێ�_���i�R�[�h] 

GO
