USE [JunpDB]
GO

/****** Object:  View [dbo].[vMicES�󒍓`�[]    Script Date: 2020/01/09 16:36:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vMicES�󒍓`�[]
--ALTER VIEW [dbo].[vMicES�󒍓`�[]
AS
-- MWS palette ES 2019�ŁAMWS palette ES ��ĳ���ێ痿72���� ���`�[
SELECT
ES.*
,D_72.[f�󒍔ԍ�] AS �ێ�_�󒍔ԍ�
,D_72.[f�N�x] AS �ێ�_�N�x
,H_72.[f�󒍓�] AS �ێ�_�󒍓�
,H_72.[f�󒍏��F��] AS �ێ�_�󒍏��F��
,H_72.[f���㏳�F��] AS �ێ�_���㏳�F��
,H_72.[f�[��] AS �ێ�_�[��
,H_72.[f�̔����] AS �ێ�_�̔����
,D_72.[f���i�R�[�h] AS �ێ�_���i�R�[�h
,D_72.[f���i��] AS �ێ�_���i��
,convert(int, H_72.[f�󒍋��z]) AS �ێ�_�󒍋��z
,H_72.[fSV���p�J�n�N��] AS �ێ�_���p�J�n�N��
,H_72.[fSV���p�I���N��] AS �ێ�_���p�I���N��
,'10' AS ����ŗ�
FROM [JunpDB].[dbo].[tMih�󒍏ڍ�] AS D_72
LEFT JOIN [JunpDB].[dbo].[tMih�󒍃w�b�_] AS H_72 ON D_72.[f�N�x] = H_72.[f�N�x] AND D_72.[f�󒍔ԍ�] = H_72.[f�󒍔ԍ�]
INNER JOIN
(
SELECT 
H_ES.[fBshCode3] AS ���_�R�[�h
,H_ES.[f�S���x�X��] AS ���_��
,H_ES.[f�S���҃R�[�h] AS �S���҃R�[�h
,H_ES.[f�S���Җ�] AS �S���Җ�
,H_ES.[f���[�U�[�R�[�h] AS �ڋqNo
,H_ES.[f���[�U�[] AS �ڋq��
,D_ES.[f�󒍔ԍ�] AS ES_�󒍔ԍ�
,H_ES.[f�N�x] AS ES_�N�x
,H_ES.[f�󒍓�] AS ES_�󒍓�
,H_ES.[f�󒍏��F��] AS ES_�󒍏��F��
,H_ES.[f���㏳�F��] AS ES_���㏳�F��
,H_ES.[f�[��] AS ES_�[��
,H_ES.[f�̔����] AS ES_�̔����
,D_ES.[f���i�R�[�h] AS ES_���i�R�[�h
,D_ES.[f���i��] AS ES_���i��
,convert(int, H_ES.[f�󒍋��z]) AS ES_�󒍋��z
,H_ES.[fSV���p�J�n�N��] AS ES_���p�J�n�N��
,H_ES.[fSV���p�I���N��] AS ES_���p�I���N��
FROM [JunpDB].[dbo].[tMih�󒍏ڍ�] AS D_ES
LEFT JOIN [JunpDB].[dbo].[tMih�󒍃w�b�_] AS H_ES ON D_ES.[f�N�x] = H_ES.[f�N�x] AND D_ES.[f�󒍔ԍ�] = H_ES.[f�󒍔ԍ�]
WHERE D_ES.[f���i�R�[�h] = '800121' AND H_ES.[f�̔����] = 1 AND [f���㏳�F��] is not null
) AS ES ON D_72.[f�N�x] = ES_�N�x AND D_72.[f�󒍔ԍ�] = ES_�󒍔ԍ�
WHERE D_72.[f���i�R�[�h] = '800161' AND H_72.[f�̔����] = 1 AND H_72.[f�󒍏��F��] is not null

UNION ALL

-- MWS palette ES 2019�ŁAMWS palette ES ��ĳ���ێ痿72���� �ʓ`�[
SELECT
ES.*
,D_72.[f�󒍔ԍ�] AS �ێ�_�󒍔ԍ�
,D_72.[f�N�x] AS �ێ�_�N�x
,H_72.[f�󒍓�] AS �ێ�_�󒍓�
,H_72.[f�󒍏��F��] AS �ێ�_�󒍏��F��
,H_72.[f���㏳�F��] AS �ێ�_���㏳�F��
,H_72.[f�[��] AS �ێ�_�[��
,H_72.[f�̔����] AS �ێ�_�̔����
,D_72.[f���i�R�[�h] AS �ێ�_���i�R�[�h
,D_72.[f���i��] AS �ێ�_���i��
,convert(int, H_72.[f�󒍋��z]) AS �ێ�_�󒍋��z
,H_72.[fSV���p�J�n�N��] AS �ێ�_���p�J�n�N��
,H_72.[fSV���p�I���N��] AS �ێ�_���p�I���N��
,'10' AS ����ŗ�
FROM [JunpDB].[dbo].[tMih�󒍏ڍ�] AS D_72
LEFT JOIN [JunpDB].[dbo].[tMih�󒍃w�b�_] AS H_72 ON D_72.[f�N�x] = H_72.[f�N�x] AND D_72.[f�󒍔ԍ�] = H_72.[f�󒍔ԍ�]
INNER JOIN
(
SELECT 
H_ES.[fBshCode3] AS ���_�R�[�h
,H_ES.[f�S���x�X��] AS ���_��
,H_ES.[f�S���҃R�[�h] AS �S���҃R�[�h
,H_ES.[f�S���Җ�] AS �S���Җ�
,H_ES.[f���[�U�[�R�[�h] AS �ڋqNo
,H_ES.[f���[�U�[] AS �ڋq��
,D_ES.[f�󒍔ԍ�] AS ES_�󒍔ԍ�
,H_ES.[f�N�x] AS ES_�N�x
,H_ES.[f�󒍓�] AS ES_�󒍓�
,H_ES.[f�󒍏��F��] AS ES_�󒍏��F��
,H_ES.[f���㏳�F��] AS ES_���㏳�F��
,H_ES.[f�[��] AS ES_�[��
,H_ES.[f�̔����] AS ES_�̔����
,D_ES.[f���i�R�[�h] AS ES_���i�R�[�h
,D_ES.[f���i��] AS ES_���i��
,convert(int, H_ES.[f�󒍋��z]) AS ES_�󒍋��z
,H_ES.[fSV���p�J�n�N��] AS ES_���p�J�n�N��
,H_ES.[fSV���p�I���N��] AS ES_���p�I���N��
FROM [JunpDB].[dbo].[tMih�󒍏ڍ�] AS D_ES
LEFT JOIN [JunpDB].[dbo].[tMih�󒍃w�b�_] AS H_ES ON D_ES.[f�N�x] = H_ES.[f�N�x] AND D_ES.[f�󒍔ԍ�] = H_ES.[f�󒍔ԍ�]
WHERE D_ES.[f���i�R�[�h] = '800121' AND H_ES.[f�̔����] = 1 AND [f���㏳�F��] is not null
) AS ES ON H_72.[f���[�U�[�R�[�h] = ES.�ڋqNo and H_72.[fSV���p�J�n�N��] = ES.ES_���p�J�n�N�� and D_72.[f�󒍔ԍ�] <> ES_�󒍔ԍ�
WHERE D_72.[f���i�R�[�h] = '800161' AND H_72.[f�̔����] = 3 AND H_72.[f�󒍏��F��] is not null

UNION ALL

-- MWS palette ES 2019�ŁAMWS palette ES ��ĳ���ێ痿12���� �ʓ`�[
SELECT
ES.*
,D_12.[f�󒍔ԍ�] AS �ێ�_�󒍔ԍ�
,D_12.[f�N�x] AS �ێ�_�N�x
,H_12.[f�󒍓�] AS �ێ�_�󒍓�
,H_12.[f�󒍏��F��] AS �ێ�_�󒍏��F��
,H_12.[f���㏳�F��] AS �ێ�_���㏳�F��
,H_12.[f�[��] AS �ێ�_�[��
,H_12.[f�̔����] AS �ێ�_�̔����
,D_12.[f���i�R�[�h] AS �ێ�_���i�R�[�h
,D_12.[f���i��] AS �ێ�_���i��
,convert(int, H_12.[f�󒍋��z]) AS �ێ�_�󒍋��z
,H_12.[fSV���p�J�n�N��] AS �ێ�_���p�J�n�N��
,H_12.[fSV���p�I���N��] AS �ێ�_���p�I���N��
,'10' AS ����ŗ�
FROM [JunpDB].[dbo].[tMih�󒍏ڍ�] AS D_12
LEFT JOIN [JunpDB].[dbo].[tMih�󒍃w�b�_] AS H_12 ON D_12.[f�N�x] = H_12.[f�N�x] AND D_12.[f�󒍔ԍ�] = H_12.[f�󒍔ԍ�]
INNER JOIN
(
SELECT 
H_ES.[fBshCode3] AS ���_�R�[�h
,H_ES.[f�S���x�X��] AS ���_��
,H_ES.[f�S���҃R�[�h] AS �S���҃R�[�h
,H_ES.[f�S���Җ�] AS �S���Җ�
,H_ES.[f���[�U�[�R�[�h] AS �ڋqNo
,H_ES.[f���[�U�[] AS �ڋq��
,D_ES.[f�󒍔ԍ�] AS ES_�󒍔ԍ�
,H_ES.[f�N�x] AS ES_�N�x
,H_ES.[f�󒍓�] AS ES_�󒍓�
,H_ES.[f�󒍏��F��] AS ES_�󒍏��F��
,H_ES.[f���㏳�F��] AS ES_���㏳�F��
,H_ES.[f�[��] AS ES_�[��
,H_ES.[f�̔����] AS ES_�̔����
,D_ES.[f���i�R�[�h] AS ES_���i�R�[�h
,D_ES.[f���i��] AS ES_���i��
,convert(int, H_ES.[f�󒍋��z]) AS ES_�󒍋��z
,H_ES.[fSV���p�J�n�N��] AS ES_���p�J�n�N��
,H_ES.[fSV���p�I���N��] AS ES_���p�I���N��
FROM [JunpDB].[dbo].[tMih�󒍏ڍ�] AS D_ES
LEFT JOIN [JunpDB].[dbo].[tMih�󒍃w�b�_] AS H_ES ON D_ES.[f�N�x] = H_ES.[f�N�x] AND D_ES.[f�󒍔ԍ�] = H_ES.[f�󒍔ԍ�]
WHERE D_ES.[f���i�R�[�h] = '800121' AND H_ES.[f�̔����] = 1 AND [f���㏳�F��] is not null
) AS ES ON H_12.[f���[�U�[�R�[�h] = ES.�ڋqNo and H_12.[fSV���p�J�n�N��] = ES.ES_���p�J�n�N�� and D_12.[f�󒍔ԍ�] <> ES_�󒍔ԍ�
WHERE D_12.[f���i�R�[�h] = '800162' AND H_12.[f�̔����] = 3 AND H_12.[f�󒍏��F��] is not null

GO
