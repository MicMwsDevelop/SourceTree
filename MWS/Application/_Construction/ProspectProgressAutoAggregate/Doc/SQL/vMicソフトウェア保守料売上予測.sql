USE [JunpDB]
GO

/****** Object:  View [dbo].[vMic�\�t�g�E�F�A�ێ痿����\��]    Script Date: 2022/02/14 17:12:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vMic�\�t�g�E�F�A�ێ痿����\��]
AS
--�\�t�g�E�F�A�ێ痿�v��P�N�� ����F�`�[�x�[�X
SELECT iif(H.[f���㏳�F��] >= '2022-02-01', '0' + convert(nvarchar, BR.[fPca����R�[�h]), CASE H.[fBshCode2]
			WHEN '50' THEN '081'
			WHEN '60' THEN '083'
			WHEN '70' THEN '082'
			WHEN '75' THEN '086'
			WHEN '76' THEN '087'
			WHEN '80' THEN '085'
		END) AS ����R�[�h
		,H.[fBshCode2] AS �c�ƕ��R�[�h
		,iif(H.[f���㏳�F��] >= '2022-02-01', BU.[fBshName2], CASE H.[fBshCode2]
			WHEN '50' THEN '�����{�c�ƕ�'
			WHEN '60' THEN '�֓��c�ƕ�'
			WHEN '70' THEN '��s���c�ƕ�'
			WHEN '75' THEN '�����c�ƕ�'
			WHEN '76' THEN '�֐��c�ƕ�'
			WHEN '80' THEN '�����{�c�ƕ�'
		END) AS �c�ƕ���
       ,H.[fBshCode3] AS ���_�R�[�h
	   ,iif(H.[f���㏳�F��] >= '2022-02-01', BU.[fBshName3], CASE H.[fBshCode3]
			WHEN '11' THEN '�D�y'
			WHEN '21' THEN '���'
			WHEN '31' THEN '��������'
			WHEN '33' THEN '���l'
			WHEN '41' THEN '��s��'
			WHEN '51' THEN '���É�'
			WHEN '52' THEN '����'
			WHEN '61' THEN '���'
			WHEN '71' THEN '�L��'
			WHEN '81' THEN '����'
		END) AS ���_��
      ,H.[f���[�U�[�R�[�h] AS �ڋqNo
      ,H.[f���[�U�[] AS �ڋq��
      ,H.[f�󒍔ԍ�] AS �󒍔ԍ�
      ,H.[f�󒍏��F��] AS �󒍏��F��
      ,H.[f���㏳�F��] AS ���㏳�F��
	  ,H.[f�[��] AS �[��
      ,60000 AS ������z
      ,FORMAT(H.[f���㏳�F��], 'yyyy/MM') AS �v�㌎
FROM [JunpDB].[dbo].[tMih�󒍏ڍ�] as D
LEFT JOIN [JunpDB].[dbo].[tMih�󒍃w�b�_] AS H ON D.[f�N�x] = H.[f�N�x] AND D.[f�󒍔ԍ�] = H.[f�󒍔ԍ�]
LEFT JOIN [JunpDB].[dbo].[tBusho] AS BU ON BU.[fBshCode3] = H.[fBshCode3]
LEFT JOIN [JunpDB].[dbo].[tMih�x�X���] AS BR ON BR.[fBshCode2] = H.[fBshCode2] AND BR.[fBshCode3] = H.[fBshCode3]
WHERE H.[f�̔����] = 1 AND H.[f���㏳�F��] is Not Null AND D.[f���i�R�[�h] in ('800161', '800162')

UNION

--�\�t�g�E�F�A�ێ痿�v��Q�N�� ����F�S���G���A�x�[�X
SELECT iif(dateadd(month, 11, H.[f�[��]) >= '2022-02-01', '0' + convert(nvarchar, BR.[fPca����R�[�h]), CASE U.[�c�ƕ��R�[�h]
			WHEN '50' THEN '081'
			WHEN '60' THEN '083'
			WHEN '70' THEN '082'
			WHEN '75' THEN '086'
			WHEN '76' THEN '087'
			WHEN '80' THEN '085'
		END) AS ����R�[�h
	   ,iif(dateadd(month, 11, H.[f�[��]) >= '2022-02-01', U.[�c�ƕ��R�[�h], CASE U.[���_�R�[�h]
			WHEN '31' THEN '50'	--��������
			WHEN '33' THEN '75'	--���l
			WHEN '52' THEN '75'	--����
			ELSE U.[�c�ƕ��R�[�h]
		END) AS �c�ƕ��R�[�h
	   ,iif(dateadd(month, 11, H.[f�[��]) >= '2022-02-01', U.[�c�ƕ���], CASE U.[���_�R�[�h]
			WHEN '11' THEN '�����{�c�ƕ�'
			WHEN '21' THEN '�����{�c�ƕ�'
			WHEN '31' THEN '�֓��c�ƕ�'
			WHEN '33' THEN '�֓��c�ƕ�'
			WHEN '41' THEN '��s���c�ƕ�'
			WHEN '51' THEN '�����c�ƕ�'
			WHEN '52' THEN '�����c�ƕ�'
			WHEN '61' THEN '�֐��c�ƕ�'
			WHEN '71' THEN '�����{�c�ƕ�'
			WHEN '81' THEN '�����{�c�ƕ�'
		END) AS �c�ƕ���
	   ,U.[���_�R�[�h] AS ���_�R�[�h
	   ,iif(dateadd(month, 11, H.[f�[��]) >= '2022-02-01', U.[���_��], CASE U.[���_�R�[�h]
			WHEN '11' THEN '�D�y'
			WHEN '21' THEN '���'
			WHEN '31' THEN '��������'
			WHEN '33' THEN '���l'
			WHEN '41' THEN '��s��'
			WHEN '51' THEN '���É�'
			WHEN '52' THEN '����'
			WHEN '61' THEN '���'
			WHEN '71' THEN '�L��'
			WHEN '81' THEN '����'
		END) AS ���_��
		,H.[f���[�U�[�R�[�h] AS �ڋqNo
		,H.[f���[�U�[] AS �ڋq��
		,D.[f�󒍔ԍ�] AS �󒍔ԍ�
		,H.[f�󒍏��F��] AS �󒍏��F��
		,H.[f���㏳�F��] AS ���㏳�F��
		,H.[f�[��] AS �[��
		,60000 as ������z
		,LEFT(convert(nvarchar, dateadd(month, 11, H.[f�[��]), 111), 7) AS �v�㌎
FROM [JunpDB].[dbo].[tMih�󒍏ڍ�] AS D
LEFT JOIN [JunpDB].[dbo].[tMih�󒍃w�b�_] AS H ON D.[f�N�x] = H.[f�N�x] AND D.[f�󒍔ԍ�] = H.[f�󒍔ԍ�]
LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[�R] AS U ON U.[�ڋqNo] = H.[f���[�U�[�R�[�h]
LEFT JOIN [JunpDB].[dbo].[tMih�x�X���] AS BR ON BR.[fBshCode2] = U.[�c�ƕ��R�[�h] AND BR.[fBshCode3] = U.[���_�R�[�h]
WHERE H.[f�̔����] = 1 AND H.[f���㏳�F��] is Not Null AND D.[f���i�R�[�h] in ('800161', '800162')

UNION

--�\�t�g�E�F�A�ێ痿�v��R�N�� ����F�S���G���A�x�[�X
SELECT iif(dateadd(month, 23, H.[f�[��]) >= '2022-02-01', '0' + convert(nvarchar, BR.[fPca����R�[�h]), CASE U.[�c�ƕ��R�[�h]
			WHEN '50' THEN '081'
			WHEN '60' THEN '083'
			WHEN '70' THEN '082'
			WHEN '75' THEN '086'
			WHEN '76' THEN '087'
			WHEN '80' THEN '085'
		END) AS ����R�[�h
	   ,iif(dateadd(month, 23, H.[f�[��]) >= '2022-02-01', U.[�c�ƕ��R�[�h], CASE U.[���_�R�[�h]
			WHEN '31' THEN '50'	--��������
			WHEN '33' THEN '75'	--���l
			WHEN '52' THEN '75'	--����
			ELSE U.[�c�ƕ��R�[�h]
		END) AS �c�ƕ��R�[�h
	   ,iif(dateadd(month, 23, H.[f�[��]) >= '2022-02-01', U.[�c�ƕ���], CASE U.[���_�R�[�h]
			WHEN '11' THEN '�����{�c�ƕ�'
			WHEN '21' THEN '�����{�c�ƕ�'
			WHEN '31' THEN '�֓��c�ƕ�'
			WHEN '33' THEN '�֓��c�ƕ�'
			WHEN '41' THEN '��s���c�ƕ�'
			WHEN '51' THEN '�����c�ƕ�'
			WHEN '52' THEN '�����c�ƕ�'
			WHEN '61' THEN '�֐��c�ƕ�'
			WHEN '71' THEN '�����{�c�ƕ�'
			WHEN '81' THEN '�����{�c�ƕ�'
		END) AS �c�ƕ���
	   ,U.[���_�R�[�h] AS ���_�R�[�h
	   ,iif(dateadd(month, 23, H.[f�[��]) >= '2022-02-01', U.[���_��], CASE U.[���_�R�[�h]
			WHEN '11' THEN '�D�y'
			WHEN '21' THEN '���'
			WHEN '31' THEN '��������'
			WHEN '33' THEN '���l'
			WHEN '41' THEN '��s��'
			WHEN '51' THEN '���É�'
			WHEN '52' THEN '����'
			WHEN '61' THEN '���'
			WHEN '71' THEN '�L��'
			WHEN '81' THEN '����'
		END) AS ���_��
		,H.[f���[�U�[�R�[�h] AS �ڋqNo
		,H.[f���[�U�[] AS �ڋq��
		,D.[f�󒍔ԍ�] AS �󒍔ԍ�
		,H.[f�󒍏��F��] AS �󒍏��F��
		,H.[f���㏳�F��] AS ���㏳�F��
		,H.[f�[��] AS �[��
		,60000 as ������z
		,LEFT(convert(nvarchar, dateadd(month, 23, H.[f�[��]), 111), 7) AS �v�㌎
FROM [JunpDB].[dbo].[tMih�󒍏ڍ�] AS D
LEFT JOIN [JunpDB].[dbo].[tMih�󒍃w�b�_] AS H ON D.[f�N�x] = H.[f�N�x] AND D.[f�󒍔ԍ�] = H.[f�󒍔ԍ�]
LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[�R] AS U ON U.[�ڋqNo] = H.[f���[�U�[�R�[�h]
LEFT JOIN [JunpDB].[dbo].[tMih�x�X���] AS BR ON BR.[fBshCode2] = U.[�c�ƕ��R�[�h] AND BR.[fBshCode3] = U.[���_�R�[�h]
WHERE H.[f�̔����] = 1 AND H.[f���㏳�F��] is Not Null AND D.[f���i�R�[�h] in ('800161', '800162')

UNION

--�\�t�g�E�F�A�ێ痿�v��S�N�� ����F�S���G���A�x�[�X
SELECT iif(dateadd(month, 35, H.[f�[��]) >= '2022-02-01', '0' + convert(nvarchar, BR.[fPca����R�[�h]), CASE U.[�c�ƕ��R�[�h]
			WHEN '50' THEN '081'
			WHEN '60' THEN '083'
			WHEN '70' THEN '082'
			WHEN '75' THEN '086'
			WHEN '76' THEN '087'
			WHEN '80' THEN '085'
		END) AS ����R�[�h
	   ,iif(dateadd(month, 35, H.[f�[��]) >= '2022-02-01', U.[�c�ƕ��R�[�h], CASE U.[���_�R�[�h]
			WHEN '31' THEN '50'	--��������
			WHEN '33' THEN '75'	--���l
			WHEN '52' THEN '75'	--����
			ELSE U.[�c�ƕ��R�[�h]
		END) AS �c�ƕ��R�[�h
	   ,iif(dateadd(month, 35, H.[f�[��]) >= '2022-02-01', U.[�c�ƕ���], CASE U.[���_�R�[�h]
			WHEN '11' THEN '�����{�c�ƕ�'
			WHEN '21' THEN '�����{�c�ƕ�'
			WHEN '31' THEN '�֓��c�ƕ�'
			WHEN '33' THEN '�֓��c�ƕ�'
			WHEN '41' THEN '��s���c�ƕ�'
			WHEN '51' THEN '�����c�ƕ�'
			WHEN '52' THEN '�����c�ƕ�'
			WHEN '61' THEN '�֐��c�ƕ�'
			WHEN '71' THEN '�����{�c�ƕ�'
			WHEN '81' THEN '�����{�c�ƕ�'
		END) AS �c�ƕ���
	   ,U.[���_�R�[�h] AS ���_�R�[�h
	   ,iif(dateadd(month, 35, H.[f�[��]) >= '2022-02-01', U.[���_��], CASE U.[���_�R�[�h]
			WHEN '11' THEN '�D�y'
			WHEN '21' THEN '���'
			WHEN '31' THEN '��������'
			WHEN '33' THEN '���l'
			WHEN '41' THEN '��s��'
			WHEN '51' THEN '���É�'
			WHEN '52' THEN '����'
			WHEN '61' THEN '���'
			WHEN '71' THEN '�L��'
			WHEN '81' THEN '����'
		END) AS ���_��
		,H.[f���[�U�[�R�[�h] AS �ڋqNo
		,H.[f���[�U�[] AS �ڋq��
		,D.[f�󒍔ԍ�] AS �󒍔ԍ�
		,H.[f�󒍏��F��] AS �󒍏��F��
		,H.[f���㏳�F��] AS ���㏳�F��
		,H.[f�[��] AS �[��
		,60000 as ������z
		,LEFT(convert(nvarchar, dateadd(month, 35, H.[f�[��]), 111), 7) AS �v�㌎
FROM [JunpDB].[dbo].[tMih�󒍏ڍ�] AS D
LEFT JOIN [JunpDB].[dbo].[tMih�󒍃w�b�_] AS H ON D.[f�N�x] = H.[f�N�x] AND D.[f�󒍔ԍ�] = H.[f�󒍔ԍ�]
LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[�R] AS U ON U.[�ڋqNo] = H.[f���[�U�[�R�[�h]
LEFT JOIN [JunpDB].[dbo].[tMih�x�X���] AS BR ON BR.[fBshCode2] = U.[�c�ƕ��R�[�h] AND BR.[fBshCode3] = U.[���_�R�[�h]
WHERE H.[f�̔����] = 1 AND H.[f���㏳�F��] is Not Null AND D.[f���i�R�[�h] in ('800161', '800162')

UNION

--�\�t�g�E�F�A�ێ痿�v��T�N�� ����F�S���G���A�x�[�X
SELECT iif(dateadd(month, 47, H.[f�[��]) >= '2022-02-01', '0' + convert(nvarchar, BR.[fPca����R�[�h]), CASE U.[�c�ƕ��R�[�h]
			WHEN '50' THEN '081'
			WHEN '60' THEN '083'
			WHEN '70' THEN '082'
			WHEN '75' THEN '086'
			WHEN '76' THEN '087'
			WHEN '80' THEN '085'
		END) AS ����R�[�h
	   ,iif(dateadd(month, 47, H.[f�[��]) >= '2022-02-01', U.[�c�ƕ��R�[�h], CASE U.[���_�R�[�h]
			WHEN '31' THEN '50'	--��������
			WHEN '33' THEN '75'	--���l
			WHEN '52' THEN '75'	--����
			ELSE U.[�c�ƕ��R�[�h]
		END) AS �c�ƕ��R�[�h
	   ,iif(dateadd(month, 47, H.[f�[��]) >= '2022-02-01', U.[�c�ƕ���], CASE U.[���_�R�[�h]
			WHEN '11' THEN '�����{�c�ƕ�'
			WHEN '21' THEN '�����{�c�ƕ�'
			WHEN '31' THEN '�֓��c�ƕ�'
			WHEN '33' THEN '�֓��c�ƕ�'
			WHEN '41' THEN '��s���c�ƕ�'
			WHEN '51' THEN '�����c�ƕ�'
			WHEN '52' THEN '�����c�ƕ�'
			WHEN '61' THEN '�֐��c�ƕ�'
			WHEN '71' THEN '�����{�c�ƕ�'
			WHEN '81' THEN '�����{�c�ƕ�'
		END) AS �c�ƕ���
	   ,U.[���_�R�[�h] AS ���_�R�[�h
	   ,iif(dateadd(month, 47, H.[f�[��]) >= '2022-02-01', U.[���_��], CASE U.[���_�R�[�h]
			WHEN '11' THEN '�D�y'
			WHEN '21' THEN '���'
			WHEN '31' THEN '��������'
			WHEN '33' THEN '���l'
			WHEN '41' THEN '��s��'
			WHEN '51' THEN '���É�'
			WHEN '52' THEN '����'
			WHEN '61' THEN '���'
			WHEN '71' THEN '�L��'
			WHEN '81' THEN '����'
		END) AS ���_��
		,H.[f���[�U�[�R�[�h] AS �ڋqNo
		,H.[f���[�U�[] AS �ڋq��
		,D.[f�󒍔ԍ�] AS �󒍔ԍ�
		,H.[f�󒍏��F��] AS �󒍏��F��
		,H.[f���㏳�F��] AS ���㏳�F��
		,H.[f�[��] AS �[��
		,60000 as ������z
		,LEFT(convert(nvarchar, dateadd(month, 47, H.[f�[��]), 111), 7) AS �v�㌎
FROM [JunpDB].[dbo].[tMih�󒍏ڍ�] AS D
LEFT JOIN [JunpDB].[dbo].[tMih�󒍃w�b�_] AS H ON D.[f�N�x] = H.[f�N�x] AND D.[f�󒍔ԍ�] = H.[f�󒍔ԍ�]
LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[�R] AS U ON U.[�ڋqNo] = H.[f���[�U�[�R�[�h]
LEFT JOIN [JunpDB].[dbo].[tMih�x�X���] AS BR ON BR.[fBshCode2] = U.[�c�ƕ��R�[�h] AND BR.[fBshCode3] = U.[���_�R�[�h]
WHERE H.[f�̔����] = 1 AND H.[f���㏳�F��] is Not Null AND D.[f���i�R�[�h] in ('800161', '800162')

UNION

--�\�t�g�E�F�A�ێ痿�v��U�N�� ����F�S���G���A�x�[�X
SELECT iif(dateadd(month, 59, H.[f�[��]) >= '2022-02-01', '0' + convert(nvarchar, BR.[fPca����R�[�h]), CASE U.[�c�ƕ��R�[�h]
			WHEN '50' THEN '081'
			WHEN '60' THEN '083'
			WHEN '70' THEN '082'
			WHEN '75' THEN '086'
			WHEN '76' THEN '087'
			WHEN '80' THEN '085'
		END) AS ����R�[�h
	   ,iif(dateadd(month, 59, H.[f�[��]) >= '2022-02-01', U.[�c�ƕ��R�[�h], CASE U.[���_�R�[�h]
			WHEN '31' THEN '50'	--��������
			WHEN '33' THEN '75'	--���l
			WHEN '52' THEN '75'	--����
			ELSE U.[�c�ƕ��R�[�h]
		END) AS �c�ƕ��R�[�h
	   ,iif(dateadd(month, 59, H.[f�[��]) >= '2022-02-01', U.[�c�ƕ���], CASE U.[���_�R�[�h]
			WHEN '11' THEN '�����{�c�ƕ�'
			WHEN '21' THEN '�����{�c�ƕ�'
			WHEN '31' THEN '�֓��c�ƕ�'
			WHEN '33' THEN '�֓��c�ƕ�'
			WHEN '41' THEN '��s���c�ƕ�'
			WHEN '51' THEN '�����c�ƕ�'
			WHEN '52' THEN '�����c�ƕ�'
			WHEN '61' THEN '�֐��c�ƕ�'
			WHEN '71' THEN '�����{�c�ƕ�'
			WHEN '81' THEN '�����{�c�ƕ�'
		END) AS �c�ƕ���
	   ,U.[���_�R�[�h] AS ���_�R�[�h
	   ,iif(dateadd(month, 59, H.[f�[��]) >= '2022-02-01', U.[���_��], CASE U.[���_�R�[�h]
			WHEN '11' THEN '�D�y'
			WHEN '21' THEN '���'
			WHEN '31' THEN '��������'
			WHEN '33' THEN '���l'
			WHEN '41' THEN '��s��'
			WHEN '51' THEN '���É�'
			WHEN '52' THEN '����'
			WHEN '61' THEN '���'
			WHEN '71' THEN '�L��'
			WHEN '81' THEN '����'
		END) AS ���_��
		,H.[f���[�U�[�R�[�h] AS �ڋqNo
		,H.[f���[�U�[] AS �ڋq��
		,D.[f�󒍔ԍ�] AS �󒍔ԍ�
		,H.[f�󒍏��F��] AS �󒍏��F��
		,H.[f���㏳�F��] AS ���㏳�F��
		,H.[f�[��] AS �[��
		,60000 as ������z
		,LEFT(convert(nvarchar, dateadd(month, 59, H.[f�[��]), 111), 7) AS �v�㌎
FROM [JunpDB].[dbo].[tMih�󒍏ڍ�] AS D
LEFT JOIN [JunpDB].[dbo].[tMih�󒍃w�b�_] AS H ON D.[f�N�x] = H.[f�N�x] AND D.[f�󒍔ԍ�] = H.[f�󒍔ԍ�]
LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[�R] AS U ON U.[�ڋqNo] = H.[f���[�U�[�R�[�h]
LEFT JOIN [JunpDB].[dbo].[tMih�x�X���] AS BR ON BR.[fBshCode2] = U.[�c�ƕ��R�[�h] AND BR.[fBshCode3] = U.[���_�R�[�h]
WHERE H.[f�̔����] = 1 AND H.[f���㏳�F��] is Not Null AND D.[f���i�R�[�h] in ('800161', '800162')

GO

