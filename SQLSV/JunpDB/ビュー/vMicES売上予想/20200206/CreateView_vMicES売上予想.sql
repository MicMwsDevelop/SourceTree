USE [JunpDB]
GO

/****** Object:  View [dbo].[vMicES����\�z]    Script Date: 2020/01/09 16:36:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vMicES����\�z]
--ALTER VIEW [dbo].[vMicES����\�z]
AS
SELECT 
 '0' + convert(nvarchar, B.[fPca����R�[�h]) AS ����R�[�h
,U.[�c�ƕ���] AS �c�ƕ���
,H.[fBshCode3] AS ���_�R�[�h
,H.[f�S���x�X��] AS ���_��
,H.[f���[�U�[�R�[�h] AS �ڋqNo
,H.[f���[�U�[] AS �ڋq��
,D.[f�󒍔ԍ�] AS �󒍔ԍ�
,H.[f�󒍏��F��] AS �󒍏��F��
,H.[f���㏳�F��] AS ���㏳�F��
,H.[f�[��] AS �[��
,H.[f�̔����] AS �̔����
,D.[f���i�R�[�h] AS ���i�R�[�h
,D.[f���i��] AS ���i��
,convert(int, H.[f�󒍋��z]) AS �󒍋��z
,60000 as ������z
,left(convert(nvarchar, H.[f�[��], 111), 7) AS �v��1�N��
,left(convert(nvarchar, dateadd(month, 11, H.[f�[��]), 111), 7) AS �v��2�N��
,left(convert(nvarchar, dateadd(month, 23, H.[f�[��]), 111), 7) AS �v��3�N��
,left(convert(nvarchar, dateadd(month, 35, H.[f�[��]), 111), 7) AS �v��4�N��
,left(convert(nvarchar, dateadd(month, 47, H.[f�[��]), 111), 7) AS �v��5�N��
,left(convert(nvarchar, dateadd(month, 59, H.[f�[��]), 111), 7) AS �v��6�N��
FROM [JunpDB].[dbo].[tMih�󒍏ڍ�] AS D
LEFT JOIN [JunpDB].[dbo].[tMih�󒍃w�b�_] AS H ON D.[f�N�x] = H.[f�N�x] AND D.[f�󒍔ԍ�] = H.[f�󒍔ԍ�]
LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[�R] AS U ON U.[�ڋqNo] = H.[f���[�U�[�R�[�h]
LEFT JOIN [JunpDB].[dbo].[tMih�x�X���] AS B ON B.[fBshCode3] = H.[fBshCode3]
WHERE D.[f���i�R�[�h] = '800121' AND H.[f�̔����] = 1

GO
