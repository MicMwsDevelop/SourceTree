use junpdb

SELECT
����R�[�h, �c�ƕ���, ���_�R�[�h, ���_��, �ڋqNo, �ڋq��, �󒍔ԍ�, �󒍏��F��, ���㏳�F��, �[��, ������z, �v�㌎
FROM
(
SELECT ����R�[�h, �c�ƕ���, ���_�R�[�h, ���_��, �ڋqNo, �ڋq��, �󒍔ԍ�, �󒍏��F��, ���㏳�F��, �[��, ������z, �v��1�N�� AS �v�㌎ FROM vMicES�ێ甄��\��
UNION SELECT ����R�[�h, �c�ƕ���, ���_�R�[�h, ���_��, �ڋqNo, �ڋq��, �󒍔ԍ�, �󒍏��F��, ���㏳�F��, �[��, ������z, �v��2�N�� AS �v�㌎ FROM vMicES�ێ甄��\��
UNION SELECT ����R�[�h, �c�ƕ���, ���_�R�[�h, ���_��, �ڋqNo, �ڋq��, �󒍔ԍ�, �󒍏��F��, ���㏳�F��, �[��, ������z, �v��3�N�� AS �v�㌎ FROM vMicES�ێ甄��\��
UNION SELECT ����R�[�h, �c�ƕ���, ���_�R�[�h, ���_��, �ڋqNo, �ڋq��, �󒍔ԍ�, �󒍏��F��, ���㏳�F��, �[��, ������z, �v��4�N�� AS �v�㌎ FROM vMicES�ێ甄��\��
UNION SELECT ����R�[�h, �c�ƕ���, ���_�R�[�h, ���_��, �ڋqNo, �ڋq��, �󒍔ԍ�, �󒍏��F��, ���㏳�F��, �[��, ������z, �v��5�N�� AS �v�㌎ FROM vMicES�ێ甄��\��
UNION SELECT ����R�[�h, �c�ƕ���, ���_�R�[�h, ���_��, �ڋqNo, �ڋq��, �󒍔ԍ�, �󒍏��F��, ���㏳�F��, �[��, ������z, �v��6�N�� AS �v�㌎ FROM vMicES�ێ甄��\��
) AS ES
WHERE �v�㌎ >= '2021/08' AND �v�㌎ <= '2021/08'
ORDER BY ����R�[�h, �ڋqNo, �v�㌎

--, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicES����\�z]
--, start.FirstDayOfTheMonth().ToYearMonth().ToString()
--, end.LastDayOfTheMonth().ToYearMonth().ToString()
