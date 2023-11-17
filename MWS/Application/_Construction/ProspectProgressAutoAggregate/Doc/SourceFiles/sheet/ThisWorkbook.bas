VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "ThisWorkbook"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = True
'******************************************************************
'
' 46��_�����i�������W�v.xlms
'
' �����i�������W�v
'
'
'******************************************************************
' Ver1.00(2020/07/09):�V�K�쐬 ���C
' Ver1.01(2020/11/18):�����ς���Ă��v���O�����I�ɂ͕ύX���Ȃ���ԂɏC�� ���C
'******************************************************************
' �W�v���̕ύX���@
' gToday �ɏW�v��������ݒ�
'******************************************************************
'
Const sJunpODBC As String = "DSN=WW_DS;UID=ww_reader;PWD=20150801;DATABASE=JunpDB"
Const sCharlieODBC As String = "DSN=WW_DS;UID=ww_reader;PWD=20150801;DATABASE=CharlieDB"
Const adOpenKeyset = 1
Const adLockReadOnly = 1

Const ����Ȗږ�_palette As String = "palette"
Const ����Ȗږ�_paletteES As String = "paletteES"
Const ����Ȗږ�_���̑��\�t�g As String = "���̑����"
Const ����Ȗږ�_�n�[�h As String = "�n�[�h"
Const ����Ȗږ�_�Z�p�w������ As String = "�Z�p�w��"
Const ����Ȗږ�_�n�[�h�ێ� As String = "�n�[�h�ێ�"
Const ����Ȗږ�_�\�t�g�ێ� As String = "�\�t�g�ێ�"
Const ����Ȗږ�_���Ӌ@�� As String = "���Ӌ@��"
Const ����Ȗږ�_���̑� As String = "���̑�����"
Const ����Ȗږ�_Curline�{�� As String = "Curline�{��"
Const ����Ȗږ�_Curline�փu���V As String = "Curline����׼"

Const sheet�\���A���p As String = "�\���A���p"          '�u�\���A���p�v�V�[�g��
Const sheet������� As String = "�������"              '�u������сv�V�[�g��
Const sheet�����i���ڍ� As String = "�����i��_�ڍ�"     '�u�����i��_�ڍׁv�V�[�g��
Const sheet�\���A���pES As String = "�\���A���p-ES"     '�u�\���A���p-ES�v�V�[�g��
Const sheet�\���A���p�܂Ƃ� As String = "�\���A���p-�܂Ƃ�"     '�u�\���A���p-�܂Ƃ߁v�V�[�g��
Const sheet��������\�z As String = "vMic��������\�z"  '�uvMic��������\�z�v�V�[�g��
Const sheet��������\�z As String = "vMic��������\�z"  '�uvMic��������\�z�v�V�[�g��
Const sheetES����\�z As String = "vMicES����\�z"      '�uvMicES����\�z�v�V�[�g��
Const sheet�\�Z�\�����ђl As String = "�\�Z�\�����ђl"  '�u�\�Z�\�����ђl�v�V�[�g��

Private dic����R�[�h As Object
Private dic���i�敪�R�[�h As Object
Private dicPCA����R�[�h As Object
Private gToday As Date    '�W�v��
Private gThisYM As String
Private gNextYM As String
Private gMatomeList As PredictionList    '�u�\���A���p-�܂Ƃ߁v�W�v����

' ----------------------------------------------------------
'����������
' ----------------------------------------------------------
Private Sub Workbook_Open()
    
    Set dic����R�[�h = CreateObject("Scripting.Dictionary")
    dic����R�[�h.Add "�����{�c�ƕ�", "081"
    dic����R�[�h.Add "��s���c�ƕ�", "082"
    dic����R�[�h.Add "�֓��c�ƕ�", "083"
    dic����R�[�h.Add "�����c�ƕ�", "086"
    dic����R�[�h.Add "�֐��c�ƕ�", "087"
    dic����R�[�h.Add "�����{�c�ƕ�", "085"
    dic����R�[�h.Add "�w���X�P�A�c�ƕ�", "075"
    dic����R�[�h.Add "�c�ƊǗ���", "011"
    
    Set dic���i�敪�R�[�h = CreateObject("Scripting.Dictionary")
    dic���i�敪�R�[�h.Add "palette", "28"
    dic���i�敪�R�[�h.Add "paletteES", "27"
    dic���i�敪�R�[�h.Add "���̑��\�t�g", "1"
    dic���i�敪�R�[�h.Add "�n�[�h", "2"
    dic���i�敪�R�[�h.Add "�Z�p�w������", "40"
    dic���i�敪�R�[�h.Add "�n�[�h�ێ�", "7"
    dic���i�敪�R�[�h.Add "�\�t�g�ێ�", "3"
    dic���i�敪�R�[�h.Add "���Ӌ@��", "97"
    dic���i�敪�R�[�h.Add "���̑�", "99"
    dic���i�敪�R�[�h.Add "Curline�{��", "201"
    dic���i�敪�R�[�h.Add "Curline�փu���V��", "202"
    
    Set dicPCA����R�[�h = CreateObject("Scripting.Dictionary")
    dicPCA����R�[�h.Add "�����{�c�ƕ�", "310"
    dicPCA����R�[�h.Add "��s���c�ƕ�", "320"
    dicPCA����R�[�h.Add "�֓��c�ƕ�", "330"
    dicPCA����R�[�h.Add "�����c�ƕ�", "341"
    dicPCA����R�[�h.Add "�֐��c�ƕ�", "342"
    dicPCA����R�[�h.Add "�����{�c�ƕ�", "350"
    dicPCA����R�[�h.Add "�w���X�P�A�c�ƕ�", "910"
    dicPCA����R�[�h.Add "�c�ƊǗ���", "390"

    Set gMatomeList = New PredictionList
    Set gMatomeList.Items = New Collection

End Sub

' ----------------------------------------------------------
'�u�X�V�v�{�^������
' �����i���W�v�y�ь��ʊi�[
' ----------------------------------------------------------
Public Sub �X�V_Click()
    Dim backupFile As String
    Dim objFileSys As Object
    Dim resultList As SaleResultList

    'Worksheets(sheet�\���A���p).Range("Y1").Value = "�J�n " & Now
    
    '�\���A���p-�܂ƂߏW�v���ʂ̃N���A
    gMatomeList.ItemClear
        
    '�W�v���̐ݒ�
    gToday = Date
    'gToday = "2020/7/1"
    gThisYM = Format(gToday, "yyyy/MM")  '����
    gNextYM = Format(DateAdd("M", 1, gToday), "yyyy/MM") '����
    
    '�o�b�N�A�b�v�t�@�C���̍쐬 ex. 46��_�����i�������W�v_20200713.xlsm
    Set objFileSys = CreateObject("Scripting.FileSystemObject")
    backupFile = ThisWorkbook.Path & "\" & objFileSys.GetBaseName(ThisWorkbook.Name) & "_" & Format(Now, "yyyymmdd") & ".xlsm"
    ActiveWorkbook.SaveCopyAs backupFile
    Set objFileSys = Nothing
    
    '��Ɨp�V�[�g�̍폜
    'Call DeleteWorkSheet
    
    '��Ɨp�V�[�g�̒ǉ�
    Call AddWorkSheet
    
    '����W�v
    'vMic��������\�z���uvMic��������\�z�v�Ɍ��ʂ��i�[
    'vMic��������\�z���uvMic��������\�z�v�Ɍ��ʂ��i�[
    'vMicES����\�z���uvMicES����\�z�v�Ɍ��ʂ��i�[
    '������с��u�\�Z�\�����ђl�v�Ɍ��ʂ��i�[
    'ES�󒍏�񁨁u�\���A���p-ES�v�Ɍ��ʂ��i�[
    '�܂Ƃߎ󒍏��{�܂Ƃߌ_���񁨁u�\���A���p-�܂Ƃ߁v�Ɍ��ʂ��i�[
    Call ����W�v
    
    '�u�\�Z�\�����ђl�v�V�[�g��ǂݍ��݁A���X�g�ɏW�v
    Set resultList = New SaleResultList
    Set resultList.Items = New Collection
    With Worksheets(sheet�\�Z�\�����ђl)
        Dim i As Long: i = 2
        Do While .Cells(i, 1).Value <> ""
            Dim p As SaleResult: Set p = New SaleResult
            Call p.Initialize(.Range(.Cells(i, 1), .Cells(i, 17)))
            resultList.Items.Add p
            i = i + 1
        Loop
    End With
    
    '�u������сv�V�[�g�ɗ\�Z��\������ђl��ݒ�i2020/08�`2021/07�j
    Call �������_�\�Z�\�����ђl�ݒ�(resultList)
    
    '�u�\���A���p�v�V�[�g�ɗ\�Z��\���l��ݒ�i�����A�����j
    Call �\���A��_�\�Z�\���l�ݒ�(resultList)
    
    Dim jissekiThis(6) As Long  '��������v
    Dim jissekiNext(6) As Long  '��������v
    Call �����i���ڍ�_�i�������W�v(jissekiThis(), jissekiNext())
    
    Call �\���A���p_�i�������W�v(jissekiThis(), jissekiNext())

    '��Ɨp�V�[�g�̍폜
    'Call DeleteWorkSheet
    
    Worksheets(sheet�\���A���p).Activate
    
    MsgBox "�X�V���܂����B"

End Sub

' ----------------------------------------------------------
' ����W�v
' ----------------------------------------------------------
Private Sub ����W�v()
    Dim strSQL As String
    Dim qt As QueryTable
    Dim cn As Object
    Dim rs As Object

    '��������\�z�̓ǂݍ���
    With Worksheets(sheet��������\�z)
        .Cells.Clear
        strSQL = "SELECT ����敪, ����R�[�h, ���喼, ���i�敪�R�[�h, ���i�敪��, ���z" & _
                    " FROM JunpDB.dbo.vMic��������\�z"
        Set qt = .QueryTables.Add(Connection:="ODBC;" & sJunpODBC, Destination:=.Range("A1"), Sql:=strSQL)
        qt.Name = "��������\�z"
        qt.SavePassword = True
        qt.Refresh BackgroundQuery:=False
        Set qt = Nothing
    End With

    '��������\�z�̓ǂݍ���
    With Worksheets(sheet��������\�z)
        .Cells.Clear
        strSQL = "SELECT ����敪, ����R�[�h, ���喼, ���i�敪�R�[�h, ���i�敪��, ���z" & _
                    " FROM JunpDB.dbo.vMic��������\�z"
        Set qt = .QueryTables.Add(Connection:="ODBC;" & sJunpODBC, Destination:=.Range("A1"), Sql:=strSQL)
        qt.Name = "��������\�z"
        qt.SavePassword = True
        qt.Refresh BackgroundQuery:=False
        Set qt = Nothing
    End With

    'ES����\�z�̓ǂݍ���
    With Worksheets(sheetES����\�z)
        .Cells.Clear
        strSQL = "SELECT ES.[����R�[�h],ES.[�c�ƕ���],ES.[���_�R�[�h],ES.[���_��],ES.[�ڋqNo],ES.[�ڋq��],ES.[�󒍔ԍ�],ES.[�󒍏��F��],ES.[���㏳�F��],ES.[�[��],ES.[������z],ES.[�v�㌎]" & _
                    " FROM" & _
                    " (" & _
                    " SELECT [����R�[�h],[�c�ƕ���],[���_�R�[�h],[���_��],[�ڋqNo],[�ڋq��],[�󒍔ԍ�],[�󒍏��F��],[���㏳�F��],[�[��],[������z],[�v��1�N��] AS [�v�㌎] FROM [JunpDB].[dbo].[vMicES����\�z]" & _
                    " UNION SELECT [����R�[�h],[�c�ƕ���],[���_�R�[�h],[���_��],[�ڋqNo],[�ڋq��],[�󒍔ԍ�],[�󒍏��F��],[���㏳�F��],[�[��],[������z],[�v��2�N��] AS [�v�㌎] FROM [JunpDB].[dbo].[vMicES����\�z]" & _
                    " UNION SELECT [����R�[�h],[�c�ƕ���],[���_�R�[�h],[���_��],[�ڋqNo],[�ڋq��],[�󒍔ԍ�],[�󒍏��F��],[���㏳�F��],[�[��],[������z],[�v��3�N��] AS [�v�㌎] FROM [JunpDB].[dbo].[vMicES����\�z]" & _
                    " UNION SELECT [����R�[�h],[�c�ƕ���],[���_�R�[�h],[���_��],[�ڋqNo],[�ڋq��],[�󒍔ԍ�],[�󒍏��F��],[���㏳�F��],[�[��],[������z],[�v��4�N��] AS [�v�㌎] FROM [JunpDB].[dbo].[vMicES����\�z]" & _
                    " UNION SELECT [����R�[�h],[�c�ƕ���],[���_�R�[�h],[���_��],[�ڋqNo],[�ڋq��],[�󒍔ԍ�],[�󒍏��F��],[���㏳�F��],[�[��],[������z],[�v��5�N��] AS [�v�㌎] FROM [JunpDB].[dbo].[vMicES����\�z]" & _
                    " UNION SELECT [����R�[�h],[�c�ƕ���],[���_�R�[�h],[���_��],[�ڋqNo],[�ڋq��],[�󒍔ԍ�],[�󒍏��F��],[���㏳�F��],[�[��],[������z],[�v��6�N��] AS [�v�㌎] FROM [JunpDB].[dbo].[vMicES����\�z]" & _
                    ") AS ES" & _
                    " ORDER BY [����R�[�h], [�ڋqNo], [�v�㌎]"
        Set qt = .QueryTables.Add(Connection:="ODBC;" & sJunpODBC, Destination:=.Range("A1"), Sql:=strSQL)
        qt.Name = "ES����\�z"
        qt.SavePassword = True
        qt.Refresh BackgroundQuery:=False
        Set qt = Nothing
    End With

    '�\�Z�\�����ђl�̓ǂݍ���
    With Worksheets(sheet�\�Z�\�����ђl)
        .Cells.Clear
        strSQL = "SELECT * FROM charlieDB.dbo.������� ORDER BY ���ѓ�, �c�ƕ��R�[�h"
        Set qt = .QueryTables.Add(Connection:="ODBC;" & sCharlieODBC, Destination:=.Range("A1"), Sql:=strSQL)
        qt.Name = "�\�Z�\�����ђl�W�v"
        qt.SavePassword = True
        qt.Refresh BackgroundQuery:=False
        Set qt = Nothing
    End With

    Set cn = CreateObject("ADODB.Connection")
    Set rs = CreateObject("ADODB.Recordset")
    cn.ConnectionString = sJunpODBC
    cn.Open
    
    '�u�\���A���p-ES�v�Ɍ��ʂ��o��
    With Worksheets(sheet�\���A���pES)
        '�^�C�g���s�o��
        .Range("A1").Value = "���㌎"
        .Range("B1").Value = "f�󒍔ԍ�"
        .Range("C1").Value = "f�󒍓�"
        .Range("D1").Value = "f�̔���R�[�h"
        .Range("E1").Value = "f���[�U�[�R�[�h"
        .Range("F1").Value = "f�̔���"
        .Range("G1").Value = "f���[�U�["
        .Range("H1").Value = "f�󒍋��z"
        .Range("I1").Value = "f����"
        .Range("J1").Value = "f�[��"
        .Range("K1").Value = "f���v���[�X�敪"
        .Range("L1").Value = "f���v���[�X"
        .Range("M1").Value = "f�S���҃R�[�h"
        .Range("N1").Value = "f�S���Җ�"
        .Range("O1").Value = "fBshCode2"
        .Range("P1").Value = "fBshCode3"
        .Range("Q1").Value = "f�S���x�X��"
        .Range("R1").Value = "f�󒍏��F��"
        .Range("S1").Value = "f���㏳�F��"
        .Range("T1").Value = "f�����敪"
        .Range("U1").Value = "f�̔��X�R�[�h"
        .Range("V1").Value = "f�̔��X"
        .Range("W1").Value = "f�̔����"
        .Range("X1").Value = "fSV���p�J�n�N��"
        .Range("Y1").Value = "fSV���p�I���N��"
        
        strSQL = "SELECT iif([f���㏳�F��] is null, left([f�[��], 7), LEFT(CONVERT(nvarchar, [f���㏳�F��], 111), 7)) as ���㌎" & _
                ", H.[f�󒍔ԍ�], convert(nvarchar, [f�󒍓�], 111) as f�󒍓�, [f�̔���R�[�h], [f���[�U�[�R�[�h], [f�̔���]" & _
                ", [f���[�U�[], convert(int, [f�󒍋��z]) as f�󒍋��z, [f����], [f�[��]" & _
                ", [f���v���[�X�敪], [f���v���[�X], [f�S���҃R�[�h], [f�S���Җ�], [fBshCode2], [fBshCode3], [f�S���x�X��]" & _
                ", convert(nvarchar, [f�󒍏��F��], 111) as f�󒍏��F��, convert(nvarchar, [f���㏳�F��], 111) as f���㏳�F��" & _
                ", [f�����敪], [f�̔��X�R�[�h], [f�̔��X], [f�̔����], [fSV���p�J�n�N��], [fSV���p�I���N��]" & _
                " FROM [JunpDB].[dbo].[tMih�󒍃w�b�_] AS H" & _
                " LEFT JOIN [JunpDB].[dbo].[tMih�󒍏ڍ�] AS D ON H.[f�󒍔ԍ�] = D.[f�󒍔ԍ�]" & _
                " LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[4] AS U ON H.[f���[�U�[�R�[�h] = U.[�ڋqNo]" & _
                " WHERE [f���i�R�[�h] = '800121' AND [f�̔����] = 1" & _
                " ORDER BY ���㌎, H.[f�󒍔ԍ�]"
        
        '[�\���A���p-ES]�V�[�g�ɏo��
        rs.Open strSQL, cn, adOpenKeyset, adLockReadOnly
        .Range("A2").CopyFromRecordset Data:=rs
        rs.Close
    End With

    '�u�\���A���p-�܂Ƃ߁v�Ɍ��ʂ��o��
    '�܂Ƃ�WonderWeb�N�[��
    strSQL = "SELECT iif([f���㏳�F��] Is Null, Left([f�[��], 7), Left(Convert(NVARCHAR, [f���㏳�F��], 111), 7)) AS ���㌎,U.[�c�ƕ��R�[�h] AS �c�ƕ��R�[�h,U.[�c�ƕ���] AS �c�ƕ���" & _
            ",U.[���_�R�[�h] AS ���_�R�[�h,U.[���_��] AS ���_��,[f�S���҃R�[�h] AS �S���҃R�[�h,[f�S���Җ�] AS �S����,H.[f�󒍔ԍ�] AS �󒍔ԍ�,[f���[�U�[�R�[�h] AS �ڋqNo,[f���[�U�[] AS �ڋq��" & _
            ",[f���i�R�[�h] AS ���i�R�[�h,[f����] AS ����,[fSV���p�J�n�N��] AS �ۋ��J�n��,[fSV���p�I���N��] AS �ۋ��I����, CONVERT(int, [f�W�����i]) AS ���z" & _
            " FROM [JunpDB].[dbo].[tMih�󒍃w�b�_] AS H" & _
            " LEFT JOIN [JunpDB].[dbo].[tMih�󒍏ڍ�] AS D ON H.[f�󒍔ԍ�] = D.[f�󒍔ԍ�]" & _
            " LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[4] AS U ON H.[f���[�U�[�R�[�h] = U.[�ڋqNo]" & _
            " WHERE ([f���i�R�[�h] = '800155' OR [f���i�R�[�h] = '800156' OR [f���i�R�[�h] = '800157' OR [f���i�R�[�h] = '800158' OR [f���i�R�[�h] = '800159') AND [f�̔����] = 4"
    
    rs.Open strSQL, cn, adOpenKeyset, adLockReadOnly
    Do While rs.EOF = False
        Dim p As Prediction: Set p = New Prediction
        p.���㌎ = rs.Fields("���㌎")
        p.�c�ƕ��R�[�h = rs.Fields("�c�ƕ��R�[�h")
        p.�c�ƕ��� = rs.Fields("�c�ƕ���")
        p.���_�R�[�h = rs.Fields("���_�R�[�h")
        p.���_�� = rs.Fields("���_��")
        p.�S���҃R�[�h = rs.Fields("�S���҃R�[�h")
        p.�S���� = rs.Fields("�S����")
        p.�󒍔ԍ� = rs.Fields("�󒍔ԍ�")
        p.�ڋqNo = rs.Fields("�ڋqNo")
        p.�ڋq�� = rs.Fields("�ڋq��")
        p.���i�R�[�h = rs.Fields("���i�R�[�h")
        p.���� = rs.Fields("����")
        p.�ۋ��J�n�� = rs.Fields("�ۋ��J�n��")
        p.�ۋ��I���� = rs.Fields("�ۋ��I����")
        p.���z = rs.Fields("���z")
        gMatomeList.Items.Add p
        
        rs.MoveNext
    Loop
    rs.Close
    
    '�܂Ƃߌ_����
    strSQL = "SELECT LEFT(Convert(NVARCHAR, EoMonth(H.fContractStartDate, -1), 111), 7) As ���㌎,U.[�c�ƕ��R�[�h] AS �c�ƕ��R�[�h,U.[�c�ƕ���] AS �c�ƕ���,U.[���_�R�[�h] AS ���_�R�[�h,U.[���_��] AS ���_��" & _
            ",iif(U.[�c�ƒS���҃R�[�h] is null, '', U.[�c�ƒS���҃R�[�h]) AS �S���҃R�[�h,iif(U.[�c�ƒS���Җ�] is null, '', U.[�c�ƒS���Җ�]) AS �S����,'' AS �󒍔ԍ�,H.fCustomerID AS �ڋqNo,U.�ڋq�� AS �ڋq��,H.fGoodsID as ���i�R�[�h,1 AS ����" & _
            ",LEFT(CONVERT(NVARCHAR, H.fContractStartDate, 111), 7) AS �ۋ��J�n��,iif(H.fBillingEndDate is null, '', LEFT(CONVERT(NVARCHAR, H.fBillingEndDate, 111), 7)) AS �ۋ��I����, [fTotalAmount] AS ���z" & _
            " FROM [charlieDB].[dbo].[T_USE_CONTRACT_HEADER] AS H" & _
            " LEFT JOIN [JunpDB].[dbo].[vMic�S���[�U�[4] AS U ON H.fCustomerID = U.[�ڋqNo]" & _
            " WHERE H.fContractType = '�܂Ƃ�'"

    rs.Open strSQL, cn, adOpenKeyset, adLockReadOnly
    Do While rs.EOF = False
        Dim p2 As Prediction: Set p2 = New Prediction
        p2.���㌎ = rs.Fields("���㌎")
        p2.�c�ƕ��R�[�h = rs.Fields("�c�ƕ��R�[�h")
        p2.�c�ƕ��� = rs.Fields("�c�ƕ���")
        p2.���_�R�[�h = rs.Fields("���_�R�[�h")
        p2.���_�� = rs.Fields("���_��")
        p2.�S���҃R�[�h = rs.Fields("�S���҃R�[�h")
        p2.�S���� = rs.Fields("�S����")
        p2.�󒍔ԍ� = rs.Fields("�󒍔ԍ�")
        p2.�ڋqNo = rs.Fields("�ڋqNo")
        p2.�ڋq�� = rs.Fields("�ڋq��")
        p2.���i�R�[�h = rs.Fields("���i�R�[�h")
        p2.���� = rs.Fields("����")
        p2.�ۋ��J�n�� = rs.Fields("�ۋ��J�n��")
        p2.�ۋ��I���� = rs.Fields("�ۋ��I����")
        p2.���z = rs.Fields("���z")
                
        If gMatomeList.Items.Count > 0 Then
            If gMatomeList.IsExists(p2) = False Then
                gMatomeList.Items.Add p2
            End If
        Else
            gMatomeList.Items.Add p2
        End If
        
        rs.MoveNext
    Loop
    rs.Close
    
    Set rs = Nothing
    cn.Close
    Set cn = Nothing
    
    '[�\���A���p-�܂Ƃ�]�V�[�g�ɏo��
    With Worksheets(sheet�\���A���p�܂Ƃ�)
        .Range("A1").Value = "���㌎"
        .Range("B1").Value = "�c�ƕ��R�[�h"
        .Range("C1").Value = "�c�ƕ���"
        .Range("D1").Value = "���_�R�[�h"
        .Range("E1").Value = "���_��"
        .Range("F1").Value = "�S���҃R�[�h"
        .Range("G1").Value = "�S����"
        .Range("H1").Value = "�󒍔ԍ�"
        .Range("I1").Value = "�ڋqNo"
        .Range("J1").Value = "�ڋq��"
        .Range("K1").Value = "���i�R�[�h"
        .Range("L1").Value = "����"
        .Range("M1").Value = "�ۋ��J�n��"
        .Range("N1").Value = "�ۋ��I����"
        .Range("O1").Value = "���z"
        
        Dim i As Integer
        For i = 1 To gMatomeList.Items.Count
            .Cells(i + 1, 1).Value = gMatomeList.Items(i).���㌎
            .Cells(i + 1, 2).Value = gMatomeList.Items(i).�c�ƕ��R�[�h
            .Cells(i + 1, 3).Value = gMatomeList.Items(i).�c�ƕ���
            .Cells(i + 1, 4).Value = gMatomeList.Items(i).���_�R�[�h
            .Cells(i + 1, 5).Value = gMatomeList.Items(i).���_��
            .Cells(i + 1, 6).Value = gMatomeList.Items(i).�S���҃R�[�h
            .Cells(i + 1, 7).Value = gMatomeList.Items(i).�S����
            .Cells(i + 1, 8).Value = gMatomeList.Items(i).�󒍔ԍ�
            .Cells(i + 1, 9).Value = gMatomeList.Items(i).�ڋqNo
            .Cells(i + 1, 10).Value = gMatomeList.Items(i).�ڋq��
            .Cells(i + 1, 11).Value = gMatomeList.Items(i).���i�R�[�h
            .Cells(i + 1, 12).Value = gMatomeList.Items(i).����
            .Cells(i + 1, 13).Value = gMatomeList.Items(i).�ۋ��J�n��
            .Cells(i + 1, 14).Value = gMatomeList.Items(i).�ۋ��I����
            .Cells(i + 1, 15).Value = gMatomeList.Items(i).���z
        Next i
    End With
End Sub

' ----------------------------------------------------------
' �����i��_�ڍׂ̐i�������W�v
' ----------------------------------------------------------
' [����]
' jissekiThis():�������e�c�ƕ��ʐi��������z
' jissekiNext():�������e�c�ƕ��ʐi��������z
' ----------------------------------------------------------
' ��Ǝ菇
'(1) �������ї���ԍ��̎擾
'(2)�uvMic��������\�z�v��ǂݍ��݁AlistSaleThis�ɏW�v
'(3)�uvMicES����\�z�v��ǂݍ��݁AlistES�ɏW�v
'(4) �������̏W�v
'(5) �������̏W�v
'    ����������palette����ɂ͂܂Ƃߕ����܂߂�
'(6) �X�V���̍X�V
' ----------------------------------------------------------
Private Sub �����i���ڍ�_�i�������W�v(jissekiThis() As Long, jissekiNext() As Long)
    Dim searchRng, rng As Range
    Dim searchTarget As String
    Dim thisCol As Integer
    Dim nextCol As Integer
    Dim price As Long
    
    With Worksheets(sheet�����i���ڍ�)
        '(1) �������ї���ԍ��̎擾
        searchTarget = Month(gToday) & "���x (����)"
        Set searchRng = .Range("E4:AZ4")
        Set rng = searchRng.Find(searchTarget, LookAt:=xlWhole)
        thisCol = rng.column
        nextCol = thisCol + 4
    
        '(2)�uvMic��������\�z�v��ǂݍ��݁AlistSaleThis�ɏW�v
        Dim listSaleThis As SaleExpectationList: Set listSaleThis = New SaleExpectationList
        Set listSaleThis.Items = New Collection
        Dim i As Long: i = 2
        With Worksheets(sheet��������\�z)
            Do While .Cells(i, 1).Value <> ""
                Dim p1 As SaleExpectation: Set p1 = New SaleExpectation
                p1.Initialize (.Range(.Cells(i, 1), .Cells(i, 6)))
                listSaleThis.Items.Add p1
                i = i + 1
            Loop
        End With
    
        '(3)�uvMicES����\�z�v��ǂݍ��݁AlistES�ɏW�v
        Dim listES As EsExpectationList: Set listES = New EsExpectationList
        Set listES.Items = New Collection
        i = 2
        With Worksheets(sheetES����\�z)
            Do While .Cells(i, 1).Value <> ""
                Dim p3 As EsExpectation: Set p3 = New EsExpectation
                p3.Initialize (.Range(.Cells(i, 1), .Cells(i, 12)))
                listES.Items.Add p3
                i = i + 1
            Loop
        End With
    
        
        '(4) �������̏W�v
        
        'palette����
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("palette"))
        ���ђl�i�[ thisCol, ����Ȗږ�_palette, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(0) = price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), dic���i�敪�R�[�h.Item("palette"))
        ���ђl�i�[ thisCol, ����Ȗږ�_palette, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiThis(1) = price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), dic���i�敪�R�[�h.Item("palette"))
        ���ђl�i�[ thisCol, ����Ȗږ�_palette, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiThis(2) = price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), dic���i�敪�R�[�h.Item("palette"))
        ���ђl�i�[ thisCol, ����Ȗږ�_palette, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiThis(3) = price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), dic���i�敪�R�[�h.Item("palette"))
        ���ђl�i�[ thisCol, ����Ȗږ�_palette, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiThis(4) = price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("palette"))
        ���ђl�i�[ thisCol, ����Ȗږ�_palette, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(5) = price
    
        'paletteES����
         price = listSaleThis.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("paletteES"))
        ���ђl�i�[ thisCol, ����Ȗږ�_paletteES, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(0) = jissekiThis(0) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), dic���i�敪�R�[�h.Item("paletteES"))
        ���ђl�i�[ thisCol, ����Ȗږ�_paletteES, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiThis(1) = jissekiThis(1) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), dic���i�敪�R�[�h.Item("paletteES"))
        ���ђl�i�[ thisCol, ����Ȗږ�_paletteES, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiThis(2) = jissekiThis(2) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), dic���i�敪�R�[�h.Item("paletteES"))
        ���ђl�i�[ thisCol, ����Ȗږ�_paletteES, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiThis(3) = jissekiThis(3) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), dic���i�敪�R�[�h.Item("paletteES"))
        ���ђl�i�[ thisCol, ����Ȗږ�_paletteES, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiThis(4) = jissekiThis(4) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("paletteES"))
        ���ђl�i�[ thisCol, ����Ȗږ�_paletteES, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(5) = jissekiThis(5) + price
    
        '���̑���Ĕ���
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("���̑��\�t�g"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���̑��\�t�g, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(0) = jissekiThis(0) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), dic���i�敪�R�[�h.Item("���̑��\�t�g"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���̑��\�t�g, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiThis(1) = jissekiThis(1) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), dic���i�敪�R�[�h.Item("���̑��\�t�g"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���̑��\�t�g, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiThis(2) = jissekiThis(2) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), dic���i�敪�R�[�h.Item("���̑��\�t�g"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���̑��\�t�g, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiThis(3) = jissekiThis(3) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), dic���i�敪�R�[�h.Item("���̑��\�t�g"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���̑��\�t�g, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiThis(4) = jissekiThis(4) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("���̑��\�t�g"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���̑��\�t�g, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(5) = jissekiThis(5) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�w���X�P�A�c�ƕ�"), dic���i�敪�R�[�h.Item("���̑��\�t�g"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���̑��\�t�g, dicPCA����R�[�h.Item("�w���X�P�A�c�ƕ�"), price
    
        '�n�[�h���㍂
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h"))
        ���ђl�i�[ thisCol, ����Ȗږ�_�n�[�h, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(0) = jissekiThis(0) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h"))
        ���ђl�i�[ thisCol, ����Ȗږ�_�n�[�h, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiThis(1) = jissekiThis(1) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h"))
        ���ђl�i�[ thisCol, ����Ȗږ�_�n�[�h, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiThis(2) = jissekiThis(2) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h"))
        ���ђl�i�[ thisCol, ����Ȗږ�_�n�[�h, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiThis(3) = jissekiThis(3) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h"))
        ���ђl�i�[ thisCol, ����Ȗږ�_�n�[�h, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiThis(4) = jissekiThis(4) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h"))
        ���ђl�i�[ thisCol, ����Ȗږ�_�n�[�h, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(5) = jissekiThis(5) + price

        '�Z�p�w������
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("�Z�p�w������"))
        ���ђl�i�[ thisCol, ����Ȗږ�_�Z�p�w������, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(0) = jissekiThis(0) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), dic���i�敪�R�[�h.Item("�Z�p�w������"))
        ���ђl�i�[ thisCol, ����Ȗږ�_�Z�p�w������, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiThis(1) = jissekiThis(1) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), dic���i�敪�R�[�h.Item("�Z�p�w������"))
        ���ђl�i�[ thisCol, ����Ȗږ�_�Z�p�w������, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiThis(2) = jissekiThis(2) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), dic���i�敪�R�[�h.Item("�Z�p�w������"))
        ���ђl�i�[ thisCol, ����Ȗږ�_�Z�p�w������, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiThis(3) = jissekiThis(3) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), dic���i�敪�R�[�h.Item("�Z�p�w������"))
        ���ђl�i�[ thisCol, ����Ȗږ�_�Z�p�w������, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiThis(4) = jissekiThis(4) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("�Z�p�w������"))
        ���ђl�i�[ thisCol, ����Ȗږ�_�Z�p�w������, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(5) = jissekiThis(5) + price
        
        '�n�[�h�ێ�
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h�ێ�"))
        ���ђl�i�[ thisCol, ����Ȗږ�_�n�[�h�ێ�, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(0) = jissekiThis(0) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h�ێ�"))
        ���ђl�i�[ thisCol, ����Ȗږ�_�n�[�h�ێ�, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiThis(1) = jissekiThis(1) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h�ێ�"))
        ���ђl�i�[ thisCol, ����Ȗږ�_�n�[�h�ێ�, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiThis(2) = jissekiThis(2) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h�ێ�"))
        ���ђl�i�[ thisCol, ����Ȗږ�_�n�[�h�ێ�, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiThis(3) = jissekiThis(3) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h�ێ�"))
        ���ђl�i�[ thisCol, ����Ȗږ�_�n�[�h�ێ�, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiThis(4) = jissekiThis(4) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h�ێ�"))
        ���ђl�i�[ thisCol, ����Ȗږ�_�n�[�h�ێ�, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(5) = jissekiThis(5) + price
    
        '�\�t�g�ێ�
        price = listES.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), gThisYM)
        ���ђl�i�[ thisCol, ����Ȗږ�_�\�t�g�ێ�, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(0) = jissekiThis(0) + price
        price = listES.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), gThisYM)
        ���ђl�i�[ thisCol, ����Ȗږ�_�\�t�g�ێ�, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiThis(1) = jissekiThis(1) + price
        price = listES.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), gThisYM)
        ���ђl�i�[ thisCol, ����Ȗږ�_�\�t�g�ێ�, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiThis(2) = jissekiThis(2) + price
        price = listES.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), gThisYM)
        ���ђl�i�[ thisCol, ����Ȗږ�_�\�t�g�ێ�, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiThis(3) = jissekiThis(3) + price
        price = listES.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), gThisYM)
        ���ђl�i�[ thisCol, ����Ȗږ�_�\�t�g�ێ�, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiThis(4) = jissekiThis(4) + price
        price = listES.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), gThisYM)
        ���ђl�i�[ thisCol, ����Ȗږ�_�\�t�g�ێ�, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(5) = jissekiThis(5) + price

        '���Ӌ@�프�㍂
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("���Ӌ@��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���Ӌ@��, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(0) = jissekiThis(0) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), dic���i�敪�R�[�h.Item("���Ӌ@��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���Ӌ@��, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiThis(1) = jissekiThis(1) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), dic���i�敪�R�[�h.Item("���Ӌ@��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���Ӌ@��, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiThis(2) = jissekiThis(2) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), dic���i�敪�R�[�h.Item("���Ӌ@��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���Ӌ@��, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiThis(3) = jissekiThis(3) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), dic���i�敪�R�[�h.Item("���Ӌ@��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���Ӌ@��, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiThis(4) = jissekiThis(4) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("���Ӌ@��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���Ӌ@��, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(5) = jissekiThis(5) + price
    
        '���̑����㍂
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("���̑�"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���̑�, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(0) = jissekiThis(0) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), dic���i�敪�R�[�h.Item("���̑�"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���̑�, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiThis(1) = jissekiThis(1) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), dic���i�敪�R�[�h.Item("���̑�"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���̑�, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiThis(2) = jissekiThis(2) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), dic���i�敪�R�[�h.Item("���̑�"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���̑�, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiThis(3) = jissekiThis(3) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), dic���i�敪�R�[�h.Item("���̑�"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���̑�, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiThis(4) = jissekiThis(4) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("���̑�"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���̑�, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(5) = jissekiThis(5) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�c�ƊǗ���"), dic���i�敪�R�[�h.Item("���̑�"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���̑�, dicPCA����R�[�h.Item("�c�ƊǗ���"), price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�w���X�P�A�c�ƕ�"), dic���i�敪�R�[�h.Item("���̑�"))
        ���ђl�i�[ thisCol, ����Ȗږ�_���̑�, dicPCA����R�[�h.Item("�w���X�P�A�c�ƕ�"), price
    
        'Curline�{��
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�{��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_Curline�{��, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(0) = jissekiThis(0) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�{��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_Curline�{��, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiThis(1) = jissekiThis(1) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�{��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_Curline�{��, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiThis(2) = jissekiThis(2) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�{��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_Curline�{��, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiThis(3) = jissekiThis(3) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�{��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_Curline�{��, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiThis(4) = jissekiThis(4) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�{��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_Curline�{��, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(5) = jissekiThis(5) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�c�ƊǗ���"), dic���i�敪�R�[�h.Item("Curline�{��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_Curline�{��, dicPCA����R�[�h.Item("�c�ƊǗ���"), price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�w���X�P�A�c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�{��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_Curline�{��, dicPCA����R�[�h.Item("�w���X�P�A�c�ƕ�"), price
        
        'Curline�փu���V��
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�փu���V��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_Curline�փu���V, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(0) = jissekiThis(0) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�փu���V��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_Curline�փu���V, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiThis(1) = jissekiThis(1) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�փu���V��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_Curline�փu���V, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiThis(2) = jissekiThis(2) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�փu���V��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_Curline�փu���V, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiThis(3) = jissekiThis(3) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�փu���V��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_Curline�փu���V, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiThis(4) = jissekiThis(4) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�փu���V��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_Curline�փu���V, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiThis(5) = jissekiThis(5) + price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�c�ƊǗ���"), dic���i�敪�R�[�h.Item("Curline�փu���V��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_Curline�փu���V, dicPCA����R�[�h.Item("�c�ƊǗ���"), price
        price = listSaleThis.GetPrice(dic����R�[�h.Item("�w���X�P�A�c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�փu���V��"))
        ���ђl�i�[ thisCol, ����Ȗږ�_Curline�փu���V, dicPCA����R�[�h.Item("�w���X�P�A�c�ƕ�"), price
    
        '(5) �������̏W�v

        '�uvMic��������\�z�v��ǂݍ��݁AlistSaleThis�ɏW�v
        Dim listSaleNext As SaleExpectationList: Set listSaleNext = New SaleExpectationList
        Set listSaleNext.Items = New Collection
        i = 2
        With Worksheets(sheet��������\�z)
            Do While .Cells(i, 1).Value <> ""
                Dim p2 As SaleExpectation: Set p2 = New SaleExpectation
                p2.Initialize (.Range(.Cells(i, 1), .Cells(i, 6)))
                listSaleNext.Items.Add p2
                i = i + 1
            Loop
        End With
                
        'palette����
        '��������palette���㕪�ɂ́A�܂Ƃߕ����܂܂�Ă��Ȃ��̂ŁA�u�\���A���p-�܂Ƃ߁v�̋��z�����Z����
        price = gMatomeList.GetPalettePrice(gNextYM, "50")
        price = price + listSaleNext.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("palette"))
        ���ђl�i�[ nextCol, ����Ȗږ�_palette, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(0) = price

        price = gMatomeList.GetPalettePrice(gNextYM, "70")
        price = price + listSaleNext.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), dic���i�敪�R�[�h.Item("palette"))
        ���ђl�i�[ nextCol, ����Ȗږ�_palette, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiNext(1) = price

        price = gMatomeList.GetPalettePrice(gNextYM, "60")
        price = price + listSaleNext.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), dic���i�敪�R�[�h.Item("palette"))
        ���ђl�i�[ nextCol, ����Ȗږ�_palette, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiNext(2) = price

        price = gMatomeList.GetPalettePrice(gNextYM, "75")
        price = price + listSaleNext.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), dic���i�敪�R�[�h.Item("palette"))
        ���ђl�i�[ nextCol, ����Ȗږ�_palette, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiNext(3) = price

        price = gMatomeList.GetPalettePrice(gNextYM, "76")
        price = price + listSaleNext.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), dic���i�敪�R�[�h.Item("palette"))
        ���ђl�i�[ nextCol, ����Ȗږ�_palette, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiNext(4) = price

        price = gMatomeList.GetPalettePrice(gNextYM, "80")
        price = price + listSaleNext.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("palette"))
        ���ђl�i�[ nextCol, ����Ȗږ�_palette, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(5) = price
        
        'paletteES����
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("paletteES"))
        ���ђl�i�[ nextCol, ����Ȗږ�_paletteES, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(0) = jissekiNext(0) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), dic���i�敪�R�[�h.Item("paletteES"))
        ���ђl�i�[ nextCol, ����Ȗږ�_paletteES, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiNext(1) = jissekiNext(1) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), dic���i�敪�R�[�h.Item("paletteES"))
        ���ђl�i�[ nextCol, ����Ȗږ�_paletteES, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiNext(2) = jissekiNext(2) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), dic���i�敪�R�[�h.Item("paletteES"))
        ���ђl�i�[ nextCol, ����Ȗږ�_paletteES, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiNext(3) = jissekiNext(3) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), dic���i�敪�R�[�h.Item("paletteES"))
        ���ђl�i�[ nextCol, ����Ȗږ�_paletteES, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiNext(4) = jissekiNext(4) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("paletteES"))
        ���ђl�i�[ nextCol, ����Ȗږ�_paletteES, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(5) = jissekiNext(5) + price
        
        '���̑���Ĕ���
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("���̑��\�t�g"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���̑��\�t�g, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(0) = jissekiNext(0) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), dic���i�敪�R�[�h.Item("���̑��\�t�g"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���̑��\�t�g, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiNext(1) = jissekiNext(1) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), dic���i�敪�R�[�h.Item("���̑��\�t�g"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���̑��\�t�g, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiNext(2) = jissekiNext(2) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), dic���i�敪�R�[�h.Item("���̑��\�t�g"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���̑��\�t�g, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiNext(3) = jissekiNext(3) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), dic���i�敪�R�[�h.Item("���̑��\�t�g"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���̑��\�t�g, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiNext(4) = jissekiNext(4) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("���̑��\�t�g"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���̑��\�t�g, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(5) = jissekiNext(5) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�w���X�P�A�c�ƕ�"), dic���i�敪�R�[�h.Item("���̑��\�t�g"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���̑��\�t�g, dicPCA����R�[�h.Item("�w���X�P�A�c�ƕ�"), price
        
        '�n�[�h���㍂
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h"))
        ���ђl�i�[ nextCol, ����Ȗږ�_�n�[�h, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(0) = jissekiNext(0) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h"))
        ���ђl�i�[ nextCol, ����Ȗږ�_�n�[�h, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiNext(1) = jissekiNext(1) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h"))
        ���ђl�i�[ nextCol, ����Ȗږ�_�n�[�h, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiNext(2) = jissekiNext(2) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h"))
        ���ђl�i�[ nextCol, ����Ȗږ�_�n�[�h, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiNext(3) = jissekiNext(3) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h"))
        ���ђl�i�[ nextCol, ����Ȗږ�_�n�[�h, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiNext(4) = jissekiNext(4) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h"))
        ���ђl�i�[ nextCol, ����Ȗږ�_�n�[�h, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(5) = jissekiNext(5) + price

        '�Z�p�w������
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("�Z�p�w������"))
        ���ђl�i�[ nextCol, ����Ȗږ�_�Z�p�w������, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(0) = jissekiNext(0) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), dic���i�敪�R�[�h.Item("�Z�p�w������"))
        ���ђl�i�[ nextCol, ����Ȗږ�_�Z�p�w������, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiNext(1) = jissekiNext(1) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), dic���i�敪�R�[�h.Item("�Z�p�w������"))
        ���ђl�i�[ nextCol, ����Ȗږ�_�Z�p�w������, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiNext(2) = jissekiNext(2) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), dic���i�敪�R�[�h.Item("�Z�p�w������"))
        ���ђl�i�[ nextCol, ����Ȗږ�_�Z�p�w������, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiNext(3) = jissekiNext(3) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), dic���i�敪�R�[�h.Item("�Z�p�w������"))
        ���ђl�i�[ nextCol, ����Ȗږ�_�Z�p�w������, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiNext(4) = jissekiNext(4) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("�Z�p�w������"))
        ���ђl�i�[ nextCol, ����Ȗږ�_�Z�p�w������, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(5) = jissekiNext(5) + price
        
        '�n�[�h�ێ�
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h�ێ�"))
        ���ђl�i�[ nextCol, ����Ȗږ�_�n�[�h�ێ�, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(0) = jissekiNext(0) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h�ێ�"))
        ���ђl�i�[ nextCol, ����Ȗږ�_�n�[�h�ێ�, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiNext(1) = jissekiNext(1) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h�ێ�"))
        ���ђl�i�[ nextCol, ����Ȗږ�_�n�[�h�ێ�, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiNext(2) = jissekiNext(2) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h�ێ�"))
        ���ђl�i�[ nextCol, ����Ȗږ�_�n�[�h�ێ�, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiNext(3) = jissekiNext(3) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h�ێ�"))
        ���ђl�i�[ nextCol, ����Ȗږ�_�n�[�h�ێ�, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiNext(4) = jissekiNext(4) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("�n�[�h�ێ�"))
        ���ђl�i�[ nextCol, ����Ȗږ�_�n�[�h�ێ�, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(5) = jissekiNext(5) + price
        
        '�\�t�g�ێ�
        price = listES.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), gNextYM)
        ���ђl�i�[ nextCol, ����Ȗږ�_�\�t�g�ێ�, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(0) = jissekiNext(0) + price
        price = listES.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), gNextYM)
        ���ђl�i�[ nextCol, ����Ȗږ�_�\�t�g�ێ�, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiNext(1) = jissekiNext(1) + price
        price = listES.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), gNextYM)
        ���ђl�i�[ nextCol, ����Ȗږ�_�\�t�g�ێ�, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiNext(2) = jissekiNext(2) + price
        price = listES.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), gNextYM)
        ���ђl�i�[ nextCol, ����Ȗږ�_�\�t�g�ێ�, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiNext(3) = jissekiNext(3) + price
        price = listES.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), gNextYM)
        ���ђl�i�[ nextCol, ����Ȗږ�_�\�t�g�ێ�, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiNext(4) = jissekiNext(4) + price
        price = listES.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), gNextYM)
        ���ђl�i�[ nextCol, ����Ȗږ�_�\�t�g�ێ�, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(5) = jissekiNext(5) + price
        
        '���Ӌ@�프�㍂
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("���Ӌ@��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���Ӌ@��, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(0) = jissekiNext(0) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), dic���i�敪�R�[�h.Item("���Ӌ@��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���Ӌ@��, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiNext(1) = jissekiNext(1) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), dic���i�敪�R�[�h.Item("���Ӌ@��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���Ӌ@��, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiNext(2) = jissekiNext(2) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), dic���i�敪�R�[�h.Item("���Ӌ@��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���Ӌ@��, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiNext(3) = jissekiNext(3) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), dic���i�敪�R�[�h.Item("���Ӌ@��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���Ӌ@��, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiNext(4) = jissekiNext(4) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("���Ӌ@��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���Ӌ@��, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(5) = jissekiNext(5) + price
        
        '���̑����㍂
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("���̑�"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���̑�, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(0) = jissekiNext(0) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), dic���i�敪�R�[�h.Item("���̑�"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���̑�, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiNext(1) = jissekiNext(1) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), dic���i�敪�R�[�h.Item("���̑�"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���̑�, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiNext(2) = jissekiNext(2) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), dic���i�敪�R�[�h.Item("���̑�"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���̑�, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiNext(3) = jissekiNext(3) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), dic���i�敪�R�[�h.Item("���̑�"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���̑�, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiNext(4) = jissekiNext(4) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("���̑�"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���̑�, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(5) = jissekiNext(5) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�w���X�P�A�c�ƕ�"), dic���i�敪�R�[�h.Item("���̑�"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���̑�, dicPCA����R�[�h.Item("�w���X�P�A�c�ƕ�"), price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�c�ƊǗ���"), dic���i�敪�R�[�h.Item("���̑�"))
        ���ђl�i�[ nextCol, ����Ȗږ�_���̑�, dicPCA����R�[�h.Item("�c�ƊǗ���"), price
        
        'Curline�{��
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�{��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_Curline�{��, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(0) = jissekiNext(0) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�{��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_Curline�{��, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiNext(1) = jissekiNext(1) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�{��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_Curline�{��, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiNext(2) = jissekiNext(2) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�{��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_Curline�{��, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiNext(3) = jissekiNext(3) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�{��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_Curline�{��, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiNext(4) = jissekiNext(4) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�{��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_Curline�{��, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(5) = jissekiNext(5) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�c�ƊǗ���"), dic���i�敪�R�[�h.Item("Curline�{��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_Curline�{��, dicPCA����R�[�h.Item("�c�ƊǗ���"), price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�w���X�P�A�c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�{��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_Curline�{��, dicPCA����R�[�h.Item("�w���X�P�A�c�ƕ�"), price
        
        'Curline�փu���V��
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�փu���V��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_Curline�փu���V, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(0) = jissekiNext(0) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("��s���c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�փu���V��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_Curline�փu���V, dicPCA����R�[�h.Item("��s���c�ƕ�"), price
        jissekiNext(1) = jissekiNext(1) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�֓��c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�փu���V��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_Curline�փu���V, dicPCA����R�[�h.Item("�֓��c�ƕ�"), price
        jissekiNext(2) = jissekiNext(2) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�փu���V��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_Curline�փu���V, dicPCA����R�[�h.Item("�����c�ƕ�"), price
        jissekiNext(3) = jissekiNext(3) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�֐��c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�փu���V��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_Curline�փu���V, dicPCA����R�[�h.Item("�֐��c�ƕ�"), price
        jissekiNext(4) = jissekiNext(4) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�����{�c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�փu���V��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_Curline�փu���V, dicPCA����R�[�h.Item("�����{�c�ƕ�"), price
        jissekiNext(5) = jissekiNext(5) + price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�c�ƊǗ���"), dic���i�敪�R�[�h.Item("Curline�փu���V��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_Curline�փu���V, dicPCA����R�[�h.Item("�c�ƊǗ���"), price
        price = listSaleNext.GetPrice(dic����R�[�h.Item("�w���X�P�A�c�ƕ�"), dic���i�敪�R�[�h.Item("Curline�փu���V��"))
        ���ђl�i�[ nextCol, ����Ȗږ�_Curline�փu���V, dicPCA����R�[�h.Item("�w���X�P�A�c�ƕ�"), price
    
        '(6) �X�V���̍X�V
        .Range("A2").Value = "�X�V�� " & Now
    
    End With
End Sub

' ----------------------------------------------------------
'�u�\���A���p�v�V�[�g �i���W�v�y�ь��ʊi�[
' ----------------------------------------------------------
' [����]
' jissekiThis():�������e�c�ƕ��ʐi��������z
' jissekiNext():�������e�c�ƕ��ʐi��������z
' ----------------------------------------------------------
Private Sub �\���A���p_�i�������W�v(jissekiThis() As Long, jissekiNext() As Long)
    Dim esList As PredictionList
    Dim i As Integer
    Dim thisRow As Integer
    Dim nextRow As Integer
    
    With Worksheets(sheet�\���A���pES)
        Set esList = New PredictionList
        Set esList.Items = New Collection
        i = 2
        Do While .Cells(i, 1).Value <> ""
            Dim p As Prediction: Set p = New Prediction
            Call p.InitializeES(.Range(.Cells(i, 1), .Cells(i, 25)))
            esList.Items.Add p
            i = i + 1
        Loop
    End With
    
    With Worksheets(sheet�\���A���p)
        thisRow = 8
        nextRow = 16
        
        '===================================
        'ES�����i�[ - ������
        '===================================
        
        '�����{�c�ƕ�
        .Cells(thisRow, 5).Value = esList.GetCount(gThisYM, "50")
    
        '��s���c�ƕ�
        .Cells(thisRow, 8).Value = esList.GetCount(gThisYM, "70")
        
        '�֓��c�ƕ�
        .Cells(thisRow, 11).Value = esList.GetCount(gThisYM, "60")
        
        '�����c�ƕ�
        .Cells(thisRow, 14).Value = esList.GetCount(gThisYM, "75")
        
        '�֐��c�ƕ�
        .Cells(thisRow, 17).Value = esList.GetCount(gThisYM, "76")
        
        '�����{�c�ƕ�
        .Cells(thisRow, 20).Value = esList.GetCount(gThisYM, "80")
        
        '===================================
        '�܂Ƃߌ����i�[ - ������
        '===================================
        
        '�����{�c�ƕ�
        .Cells(thisRow, 6).Value = gMatomeList.GetCount(gThisYM, "50")
        
        '��s���c�ƕ�
        .Cells(thisRow, 9).Value = gMatomeList.GetCount(gThisYM, "70")
        
        '�֓��c�ƕ�
        .Cells(thisRow, 12).Value = gMatomeList.GetCount(gThisYM, "60")
        
        '�����c�ƕ�
        .Cells(thisRow, 15).Value = gMatomeList.GetCount(gThisYM, "75")
        
        '�֐��c�ƕ�
        .Cells(thisRow, 18).Value = gMatomeList.GetCount(gThisYM, "76")
        
        '�����{�c�ƕ�
        .Cells(thisRow, 21).Value = gMatomeList.GetCount(gThisYM, "80")
        
        '===================================
        '����i�[ - ������
        '===================================
        
        '�����{�c�ƕ�
        .Cells(thisRow, 7).Value = RoundPrice(jissekiThis(0))
        
        '��s���c�ƕ�
        .Cells(thisRow, 10).Value = RoundPrice(jissekiThis(1))
        
        '�֓��c�ƕ�
        .Cells(thisRow, 13).Value = RoundPrice(jissekiThis(2))
        
        '�����c�ƕ�
        .Cells(thisRow, 16).Value = RoundPrice(jissekiThis(3))
        
        '�֐��c�ƕ�
        .Cells(thisRow, 19).Value = RoundPrice(jissekiThis(4))
        
        '�����{�c�ƕ�
        .Cells(thisRow, 22).Value = RoundPrice(jissekiThis(5))
        
    
        '===================================
        'ES�����i�[ - ������
        '===================================
        
        '�����{�c�ƕ�
        .Cells(nextRow, 5).Value = esList.GetCount(gNextYM, "50")
    
        '��s���c�ƕ�
        .Cells(nextRow, 8).Value = esList.GetCount(gNextYM, "70")
        
        '�֓��c�ƕ�
        .Cells(nextRow, 11).Value = esList.GetCount(gNextYM, "60")
        
        '�����c�ƕ�
        .Cells(nextRow, 14).Value = esList.GetCount(gNextYM, "75")
        
        '�֐��c�ƕ�
        .Cells(nextRow, 17).Value = esList.GetCount(gNextYM, "76")
        
        '�����{�c�ƕ�
        .Cells(nextRow, 20).Value = esList.GetCount(gNextYM, "80")
    
    
        '===================================
        '�܂Ƃߌ����i�[ - ������
        '===================================
        
        '�����{�c�ƕ�
        .Cells(nextRow, 6).Value = gMatomeList.GetCount(gNextYM, "50")
        
        '��s���c�ƕ�
        .Cells(nextRow, 9).Value = gMatomeList.GetCount(gNextYM, "70")
        
        '�֓��c�ƕ�
        .Cells(nextRow, 12).Value = gMatomeList.GetCount(gNextYM, "60")
        
        '�����c�ƕ�
        .Cells(nextRow, 15).Value = gMatomeList.GetCount(gNextYM, "75")
        
        '�֐��c�ƕ�
        .Cells(nextRow, 18).Value = gMatomeList.GetCount(gNextYM, "76")
        
        '�����{�c�ƕ�
        .Cells(nextRow, 21).Value = gMatomeList.GetCount(gNextYM, "80")
    
    
        '===================================
        '����i�[ - ������
        '===================================
        
        '�����{�c�ƕ�
        .Cells(nextRow, 7).Value = RoundPrice(jissekiNext(0))
        
        '��s���c�ƕ�
        .Cells(nextRow, 10).Value = RoundPrice(jissekiNext(1))
        
        '�֓��c�ƕ�
        .Cells(nextRow, 13).Value = RoundPrice(jissekiNext(2))
        
        '�����c�ƕ�
        .Cells(nextRow, 16).Value = RoundPrice(jissekiNext(3))
        
        '�֐��c�ƕ�
        .Cells(nextRow, 19).Value = RoundPrice(jissekiNext(4))
        
        '�����{�c�ƕ�
        .Cells(nextRow, 22).Value = RoundPrice(jissekiNext(5))
    
        
        '�X�V���X�V
        .Range("Y2").Value = "�X�V�� " & Now
    
    End With
End Sub

' ----------------------------------------------------------
'�u������сv�V�[�g�ɗ\�Z��\������ђl��ݒ�
' ----------------------------------------------------------
Private Sub �������_�\�Z�\�����ђl�ݒ�(resultList As SaleResultList)
    Dim index As Integer
    Dim ymdNum As Long
    Dim ymdDate As Date
    
    With Worksheets(sheet�������)
        If Month(gToday) > 7 Then
            ymdNum = (Year(gToday) * 10000) + 801
        Else
            ymdNum = ((Year(gToday) - 1) * 10000) + 801
        End If
        ymdDate = CDate(Format(Str(ymdNum), "####/##/##"))
        
        '���8������
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(6, 5).Value = resultList.Items(index).�\�ZES
        .Cells(7, 5).Value = resultList.Items(index).�\��ES
        .Cells(8, 5).Value = resultList.Items(index).����ES
        .Cells(6, 6).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(7, 6).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(8, 6).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(6, 7).Value = resultList.Items(index).�\�Z����
        .Cells(7, 7).Value = resultList.Items(index).�\������
        .Cells(8, 7).Value = resultList.Items(index).���є���
        .Cells(11, 5).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(12, 5).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(13, 5).Value = resultList.Items(index).���щc�Ƒ��v
   
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(6, 8).Value = resultList.Items(index).�\�ZES
        .Cells(7, 8).Value = resultList.Items(index).�\��ES
        .Cells(8, 8).Value = resultList.Items(index).����ES
        .Cells(6, 9).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(7, 9).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(8, 9).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(6, 10).Value = resultList.Items(index).�\�Z����
        .Cells(7, 10).Value = resultList.Items(index).�\������
        .Cells(8, 10).Value = resultList.Items(index).���є���
        .Cells(11, 8).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(12, 8).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(13, 8).Value = resultList.Items(index).���щc�Ƒ��v

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(6, 11).Value = resultList.Items(index).�\�ZES
        .Cells(7, 11).Value = resultList.Items(index).�\��ES
        .Cells(8, 11).Value = resultList.Items(index).����ES
        .Cells(6, 12).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(7, 12).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(8, 12).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(6, 13).Value = resultList.Items(index).�\�Z����
        .Cells(7, 13).Value = resultList.Items(index).�\������
        .Cells(8, 13).Value = resultList.Items(index).���є���
        .Cells(11, 11).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(12, 11).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(13, 11).Value = resultList.Items(index).���щc�Ƒ��v

        index = resultList.GetIndex(ymdNum, "75")
        .Cells(6, 14).Value = resultList.Items(index).�\�ZES
        .Cells(7, 14).Value = resultList.Items(index).�\��ES
        .Cells(8, 14).Value = resultList.Items(index).����ES
        .Cells(6, 15).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(7, 15).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(8, 15).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(6, 16).Value = resultList.Items(index).�\�Z����
        .Cells(7, 16).Value = resultList.Items(index).�\������
        .Cells(8, 16).Value = resultList.Items(index).���є���
        .Cells(11, 14).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(12, 14).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(13, 14).Value = resultList.Items(index).���щc�Ƒ��v

        index = resultList.GetIndex(ymdNum, "76")
        .Cells(6, 17).Value = resultList.Items(index).�\�ZES
        .Cells(7, 17).Value = resultList.Items(index).�\��ES
        .Cells(8, 17).Value = resultList.Items(index).����ES
        .Cells(6, 18).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(7, 18).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(8, 18).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(6, 19).Value = resultList.Items(index).�\�Z����
        .Cells(7, 19).Value = resultList.Items(index).�\������
        .Cells(8, 19).Value = resultList.Items(index).���є���
        .Cells(11, 17).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(12, 17).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(13, 17).Value = resultList.Items(index).���щc�Ƒ��v

        index = resultList.GetIndex(ymdNum, "80")
        .Cells(6, 20).Value = resultList.Items(index).�\�ZES
        .Cells(7, 20).Value = resultList.Items(index).�\��ES
        .Cells(8, 20).Value = resultList.Items(index).����ES
        .Cells(6, 21).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(7, 21).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(8, 21).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(6, 22).Value = resultList.Items(index).�\�Z����
        .Cells(7, 22).Value = resultList.Items(index).�\������
        .Cells(8, 22).Value = resultList.Items(index).���є���
        .Cells(11, 20).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(12, 20).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(13, 20).Value = resultList.Items(index).���щc�Ƒ��v

        '���9������
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(16, 5).Value = resultList.Items(index).�\�ZES
        .Cells(17, 5).Value = resultList.Items(index).�\��ES
        .Cells(18, 5).Value = resultList.Items(index).����ES
        .Cells(16, 6).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(17, 6).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(18, 6).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(16, 7).Value = resultList.Items(index).�\�Z����
        .Cells(17, 7).Value = resultList.Items(index).�\������
        .Cells(18, 7).Value = resultList.Items(index).���є���
        .Cells(21, 5).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(22, 5).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(23, 5).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(16, 8).Value = resultList.Items(index).�\�ZES
        .Cells(17, 8).Value = resultList.Items(index).�\��ES
        .Cells(18, 8).Value = resultList.Items(index).����ES
        .Cells(16, 9).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(17, 9).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(18, 9).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(16, 10).Value = resultList.Items(index).�\�Z����
        .Cells(17, 10).Value = resultList.Items(index).�\������
        .Cells(18, 10).Value = resultList.Items(index).���є���
        .Cells(21, 8).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(22, 8).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(23, 8).Value = resultList.Items(index).���щc�Ƒ��v

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(16, 11).Value = resultList.Items(index).�\�ZES
        .Cells(17, 11).Value = resultList.Items(index).�\��ES
        .Cells(18, 11).Value = resultList.Items(index).����ES
        .Cells(16, 12).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(17, 12).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(18, 12).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(16, 13).Value = resultList.Items(index).�\�Z����
        .Cells(17, 13).Value = resultList.Items(index).�\������
        .Cells(18, 13).Value = resultList.Items(index).���є���
        .Cells(21, 11).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(22, 11).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(23, 11).Value = resultList.Items(index).���щc�Ƒ��v

        index = resultList.GetIndex(ymdNum, "75")
        .Cells(16, 14).Value = resultList.Items(index).�\�ZES
        .Cells(17, 14).Value = resultList.Items(index).�\��ES
        .Cells(18, 14).Value = resultList.Items(index).����ES
        .Cells(16, 15).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(17, 15).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(18, 15).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(16, 16).Value = resultList.Items(index).�\�Z����
        .Cells(17, 16).Value = resultList.Items(index).�\������
        .Cells(18, 16).Value = resultList.Items(index).���є���
        .Cells(21, 14).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(22, 14).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(23, 14).Value = resultList.Items(index).���щc�Ƒ��v

        index = resultList.GetIndex(ymdNum, "76")
        .Cells(16, 17).Value = resultList.Items(index).�\�ZES
        .Cells(17, 17).Value = resultList.Items(index).�\��ES
        .Cells(18, 17).Value = resultList.Items(index).����ES
        .Cells(16, 18).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(17, 18).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(18, 18).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(16, 19).Value = resultList.Items(index).�\�Z����
        .Cells(17, 19).Value = resultList.Items(index).�\������
        .Cells(18, 19).Value = resultList.Items(index).���є���
        .Cells(21, 17).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(22, 17).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(23, 17).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "80")
        .Cells(16, 20).Value = resultList.Items(index).�\�ZES
        .Cells(17, 20).Value = resultList.Items(index).�\��ES
        .Cells(18, 20).Value = resultList.Items(index).����ES
        .Cells(16, 21).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(17, 21).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(18, 21).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(16, 22).Value = resultList.Items(index).�\�Z����
        .Cells(17, 22).Value = resultList.Items(index).�\������
        .Cells(18, 22).Value = resultList.Items(index).���є���
        .Cells(21, 20).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(22, 20).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(23, 20).Value = resultList.Items(index).���щc�Ƒ��v
    
        '���10������
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(26, 5).Value = resultList.Items(index).�\�ZES
        .Cells(27, 5).Value = resultList.Items(index).�\��ES
        .Cells(28, 5).Value = resultList.Items(index).����ES
        .Cells(26, 6).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(27, 6).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(28, 6).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(26, 7).Value = resultList.Items(index).�\�Z����
        .Cells(27, 7).Value = resultList.Items(index).�\������
        .Cells(28, 7).Value = resultList.Items(index).���є���
        .Cells(31, 5).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(32, 5).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(33, 5).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(26, 8).Value = resultList.Items(index).�\�ZES
        .Cells(27, 8).Value = resultList.Items(index).�\��ES
        .Cells(28, 8).Value = resultList.Items(index).����ES
        .Cells(26, 9).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(27, 9).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(28, 9).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(26, 10).Value = resultList.Items(index).�\�Z����
        .Cells(27, 10).Value = resultList.Items(index).�\������
        .Cells(28, 10).Value = resultList.Items(index).���є���
        .Cells(31, 8).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(32, 8).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(33, 8).Value = resultList.Items(index).���щc�Ƒ��v

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(26, 11).Value = resultList.Items(index).�\�ZES
        .Cells(27, 11).Value = resultList.Items(index).�\��ES
        .Cells(28, 11).Value = resultList.Items(index).����ES
        .Cells(26, 12).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(27, 12).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(28, 12).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(26, 13).Value = resultList.Items(index).�\�Z����
        .Cells(27, 13).Value = resultList.Items(index).�\������
        .Cells(28, 13).Value = resultList.Items(index).���є���
        .Cells(31, 11).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(32, 11).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(33, 11).Value = resultList.Items(index).���щc�Ƒ��v

        index = resultList.GetIndex(ymdNum, "75")
        .Cells(26, 14).Value = resultList.Items(index).�\�ZES
        .Cells(27, 14).Value = resultList.Items(index).�\��ES
        .Cells(28, 14).Value = resultList.Items(index).����ES
        .Cells(26, 15).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(27, 15).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(28, 15).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(26, 16).Value = resultList.Items(index).�\�Z����
        .Cells(27, 16).Value = resultList.Items(index).�\������
        .Cells(28, 16).Value = resultList.Items(index).���є���
        .Cells(31, 14).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(32, 14).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(33, 14).Value = resultList.Items(index).���щc�Ƒ��v

        index = resultList.GetIndex(ymdNum, "76")
        .Cells(26, 17).Value = resultList.Items(index).�\�ZES
        .Cells(27, 17).Value = resultList.Items(index).�\��ES
        .Cells(28, 17).Value = resultList.Items(index).����ES
        .Cells(26, 18).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(27, 18).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(28, 18).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(26, 19).Value = resultList.Items(index).�\�Z����
        .Cells(27, 19).Value = resultList.Items(index).�\������
        .Cells(28, 19).Value = resultList.Items(index).���є���
        .Cells(31, 17).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(32, 17).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(33, 17).Value = resultList.Items(index).���щc�Ƒ��v

        index = resultList.GetIndex(ymdNum, "80")
        .Cells(26, 20).Value = resultList.Items(index).�\�ZES
        .Cells(27, 20).Value = resultList.Items(index).�\��ES
        .Cells(28, 20).Value = resultList.Items(index).����ES
        .Cells(26, 21).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(27, 21).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(28, 21).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(26, 22).Value = resultList.Items(index).�\�Z����
        .Cells(27, 22).Value = resultList.Items(index).�\������
        .Cells(28, 22).Value = resultList.Items(index).���є���
        .Cells(31, 20).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(32, 20).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(33, 20).Value = resultList.Items(index).���щc�Ƒ��v
        
        '���11������
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(44, 5).Value = resultList.Items(index).�\�ZES
        .Cells(45, 5).Value = resultList.Items(index).�\��ES
        .Cells(46, 5).Value = resultList.Items(index).����ES
        .Cells(44, 6).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(45, 6).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(46, 6).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(44, 7).Value = resultList.Items(index).�\�Z����
        .Cells(45, 7).Value = resultList.Items(index).�\������
        .Cells(46, 7).Value = resultList.Items(index).���є���
        .Cells(49, 5).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(50, 5).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(51, 5).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(44, 8).Value = resultList.Items(index).�\�ZES
        .Cells(45, 8).Value = resultList.Items(index).�\��ES
        .Cells(46, 8).Value = resultList.Items(index).����ES
        .Cells(44, 9).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(45, 9).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(46, 9).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(44, 10).Value = resultList.Items(index).�\�Z����
        .Cells(45, 10).Value = resultList.Items(index).�\������
        .Cells(46, 10).Value = resultList.Items(index).���є���
        .Cells(49, 8).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(50, 8).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(51, 8).Value = resultList.Items(index).���щc�Ƒ��v

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(44, 11).Value = resultList.Items(index).�\�ZES
        .Cells(45, 11).Value = resultList.Items(index).�\��ES
        .Cells(46, 11).Value = resultList.Items(index).����ES
        .Cells(44, 12).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(45, 12).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(46, 12).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(44, 13).Value = resultList.Items(index).�\�Z����
        .Cells(45, 13).Value = resultList.Items(index).�\������
        .Cells(46, 13).Value = resultList.Items(index).���є���
        .Cells(49, 11).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(50, 11).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(51, 11).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "75")
        .Cells(44, 14).Value = resultList.Items(index).�\�ZES
        .Cells(45, 14).Value = resultList.Items(index).�\��ES
        .Cells(46, 14).Value = resultList.Items(index).����ES
        .Cells(44, 15).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(45, 15).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(46, 15).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(44, 16).Value = resultList.Items(index).�\�Z����
        .Cells(45, 16).Value = resultList.Items(index).�\������
        .Cells(46, 16).Value = resultList.Items(index).���є���
        .Cells(49, 14).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(50, 14).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(51, 14).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "76")
        .Cells(44, 17).Value = resultList.Items(index).�\�ZES
        .Cells(45, 17).Value = resultList.Items(index).�\��ES
        .Cells(46, 17).Value = resultList.Items(index).����ES
        .Cells(44, 18).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(45, 18).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(46, 18).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(44, 19).Value = resultList.Items(index).�\�Z����
        .Cells(45, 19).Value = resultList.Items(index).�\������
        .Cells(46, 19).Value = resultList.Items(index).���є���
        .Cells(49, 17).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(50, 17).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(51, 17).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "80")
        .Cells(44, 20).Value = resultList.Items(index).�\�ZES
        .Cells(45, 20).Value = resultList.Items(index).�\��ES
        .Cells(46, 20).Value = resultList.Items(index).����ES
        .Cells(44, 21).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(45, 21).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(46, 21).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(44, 22).Value = resultList.Items(index).�\�Z����
        .Cells(45, 22).Value = resultList.Items(index).�\������
        .Cells(46, 22).Value = resultList.Items(index).���є���
        .Cells(49, 20).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(50, 20).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(51, 20).Value = resultList.Items(index).���щc�Ƒ��v
        
        '���12������
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(54, 5).Value = resultList.Items(index).�\�ZES
        .Cells(55, 5).Value = resultList.Items(index).�\��ES
        .Cells(56, 5).Value = resultList.Items(index).����ES
        .Cells(54, 6).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(55, 6).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(56, 6).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(54, 7).Value = resultList.Items(index).�\�Z����
        .Cells(55, 7).Value = resultList.Items(index).�\������
        .Cells(56, 7).Value = resultList.Items(index).���є���
        .Cells(59, 5).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(60, 5).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(61, 5).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(54, 8).Value = resultList.Items(index).�\�ZES
        .Cells(55, 8).Value = resultList.Items(index).�\��ES
        .Cells(56, 8).Value = resultList.Items(index).����ES
        .Cells(54, 9).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(55, 9).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(56, 9).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(54, 10).Value = resultList.Items(index).�\�Z����
        .Cells(55, 10).Value = resultList.Items(index).�\������
        .Cells(56, 10).Value = resultList.Items(index).���є���
        .Cells(59, 8).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(60, 8).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(61, 8).Value = resultList.Items(index).���щc�Ƒ��v

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(54, 11).Value = resultList.Items(index).�\�ZES
        .Cells(55, 11).Value = resultList.Items(index).�\��ES
        .Cells(56, 11).Value = resultList.Items(index).����ES
        .Cells(54, 12).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(55, 12).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(56, 12).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(54, 13).Value = resultList.Items(index).�\�Z����
        .Cells(55, 13).Value = resultList.Items(index).�\������
        .Cells(56, 13).Value = resultList.Items(index).���є���
        .Cells(59, 11).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(60, 11).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(61, 11).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "75")
        .Cells(54, 14).Value = resultList.Items(index).�\�ZES
        .Cells(55, 14).Value = resultList.Items(index).�\��ES
        .Cells(56, 14).Value = resultList.Items(index).����ES
        .Cells(54, 15).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(55, 15).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(56, 15).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(54, 16).Value = resultList.Items(index).�\�Z����
        .Cells(55, 16).Value = resultList.Items(index).�\������
        .Cells(56, 16).Value = resultList.Items(index).���є���
        .Cells(59, 14).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(60, 14).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(61, 14).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "76")
        .Cells(54, 17).Value = resultList.Items(index).�\�ZES
        .Cells(55, 17).Value = resultList.Items(index).�\��ES
        .Cells(56, 17).Value = resultList.Items(index).����ES
        .Cells(54, 18).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(55, 18).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(56, 18).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(54, 19).Value = resultList.Items(index).�\�Z����
        .Cells(55, 19).Value = resultList.Items(index).�\������
        .Cells(56, 19).Value = resultList.Items(index).���є���
        .Cells(59, 17).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(60, 17).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(61, 17).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "80")
        .Cells(54, 20).Value = resultList.Items(index).�\�ZES
        .Cells(55, 20).Value = resultList.Items(index).�\��ES
        .Cells(56, 20).Value = resultList.Items(index).����ES
        .Cells(54, 21).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(55, 21).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(56, 21).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(54, 22).Value = resultList.Items(index).�\�Z����
        .Cells(55, 22).Value = resultList.Items(index).�\������
        .Cells(56, 22).Value = resultList.Items(index).���є���
        .Cells(59, 20).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(60, 20).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(61, 20).Value = resultList.Items(index).���щc�Ƒ��v
        
        '���1������
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(64, 5).Value = resultList.Items(index).�\�ZES
        .Cells(65, 5).Value = resultList.Items(index).�\��ES
        .Cells(66, 5).Value = resultList.Items(index).����ES
        .Cells(64, 6).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(65, 6).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(66, 6).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(64, 7).Value = resultList.Items(index).�\�Z����
        .Cells(65, 7).Value = resultList.Items(index).�\������
        .Cells(66, 7).Value = resultList.Items(index).���є���
        .Cells(69, 5).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(70, 5).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(71, 5).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(64, 8).Value = resultList.Items(index).�\�ZES
        .Cells(65, 8).Value = resultList.Items(index).�\��ES
        .Cells(66, 8).Value = resultList.Items(index).����ES
        .Cells(64, 9).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(65, 9).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(66, 9).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(64, 10).Value = resultList.Items(index).�\�Z����
        .Cells(65, 10).Value = resultList.Items(index).�\������
        .Cells(66, 10).Value = resultList.Items(index).���є���
        .Cells(69, 8).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(70, 8).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(71, 8).Value = resultList.Items(index).���щc�Ƒ��v

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(64, 11).Value = resultList.Items(index).�\�ZES
        .Cells(65, 11).Value = resultList.Items(index).�\��ES
        .Cells(66, 11).Value = resultList.Items(index).����ES
        .Cells(64, 12).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(65, 12).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(66, 12).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(64, 13).Value = resultList.Items(index).�\�Z����
        .Cells(65, 13).Value = resultList.Items(index).�\������
        .Cells(66, 13).Value = resultList.Items(index).���є���
        .Cells(69, 11).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(70, 11).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(71, 11).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "75")
        .Cells(64, 14).Value = resultList.Items(index).�\�ZES
        .Cells(65, 14).Value = resultList.Items(index).�\��ES
        .Cells(66, 14).Value = resultList.Items(index).����ES
        .Cells(64, 15).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(65, 15).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(66, 15).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(64, 16).Value = resultList.Items(index).�\�Z����
        .Cells(65, 16).Value = resultList.Items(index).�\������
        .Cells(66, 16).Value = resultList.Items(index).���є���
        .Cells(69, 14).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(70, 14).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(71, 14).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "76")
        .Cells(64, 17).Value = resultList.Items(index).�\�ZES
        .Cells(65, 17).Value = resultList.Items(index).�\��ES
        .Cells(66, 17).Value = resultList.Items(index).����ES
        .Cells(64, 18).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(65, 18).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(66, 18).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(64, 19).Value = resultList.Items(index).�\�Z����
        .Cells(65, 19).Value = resultList.Items(index).�\������
        .Cells(66, 19).Value = resultList.Items(index).���є���
        .Cells(69, 17).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(70, 17).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(71, 17).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "80")
        .Cells(64, 20).Value = resultList.Items(index).�\�ZES
        .Cells(65, 20).Value = resultList.Items(index).�\��ES
        .Cells(66, 20).Value = resultList.Items(index).����ES
        .Cells(64, 21).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(65, 21).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(66, 21).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(64, 22).Value = resultList.Items(index).�\�Z����
        .Cells(65, 22).Value = resultList.Items(index).�\������
        .Cells(66, 22).Value = resultList.Items(index).���є���
        .Cells(69, 20).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(70, 20).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(71, 20).Value = resultList.Items(index).���щc�Ƒ��v
        
        '����2������
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(90, 5).Value = resultList.Items(index).�\�ZES
        .Cells(91, 5).Value = resultList.Items(index).�\��ES
        .Cells(92, 5).Value = resultList.Items(index).����ES
        .Cells(90, 6).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(91, 6).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(92, 6).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(90, 7).Value = resultList.Items(index).�\�Z����
        .Cells(91, 7).Value = resultList.Items(index).�\������
        .Cells(92, 7).Value = resultList.Items(index).���є���
        .Cells(95, 5).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(96, 5).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(97, 5).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(90, 8).Value = resultList.Items(index).�\�ZES
        .Cells(91, 8).Value = resultList.Items(index).�\��ES
        .Cells(92, 8).Value = resultList.Items(index).����ES
        .Cells(90, 9).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(91, 9).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(92, 9).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(90, 10).Value = resultList.Items(index).�\�Z����
        .Cells(91, 10).Value = resultList.Items(index).�\������
        .Cells(92, 10).Value = resultList.Items(index).���є���
        .Cells(95, 8).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(96, 8).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(97, 8).Value = resultList.Items(index).���щc�Ƒ��v

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(90, 11).Value = resultList.Items(index).�\�ZES
        .Cells(91, 11).Value = resultList.Items(index).�\��ES
        .Cells(92, 11).Value = resultList.Items(index).����ES
        .Cells(90, 12).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(91, 12).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(92, 12).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(90, 13).Value = resultList.Items(index).�\�Z����
        .Cells(91, 13).Value = resultList.Items(index).�\������
        .Cells(92, 13).Value = resultList.Items(index).���є���
        .Cells(95, 11).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(96, 11).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(97, 11).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "75")
        .Cells(90, 14).Value = resultList.Items(index).�\�ZES
        .Cells(91, 14).Value = resultList.Items(index).�\��ES
        .Cells(92, 14).Value = resultList.Items(index).����ES
        .Cells(90, 15).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(91, 15).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(92, 15).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(90, 16).Value = resultList.Items(index).�\�Z����
        .Cells(91, 16).Value = resultList.Items(index).�\������
        .Cells(92, 16).Value = resultList.Items(index).���є���
        .Cells(95, 14).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(96, 14).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(97, 14).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "76")
        .Cells(90, 17).Value = resultList.Items(index).�\�ZES
        .Cells(91, 17).Value = resultList.Items(index).�\��ES
        .Cells(92, 17).Value = resultList.Items(index).����ES
        .Cells(90, 18).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(91, 18).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(92, 18).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(90, 19).Value = resultList.Items(index).�\�Z����
        .Cells(91, 19).Value = resultList.Items(index).�\������
        .Cells(92, 19).Value = resultList.Items(index).���є���
        .Cells(95, 17).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(96, 17).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(97, 17).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "80")
        .Cells(90, 20).Value = resultList.Items(index).�\�ZES
        .Cells(91, 20).Value = resultList.Items(index).�\��ES
        .Cells(92, 20).Value = resultList.Items(index).����ES
        .Cells(90, 21).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(91, 21).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(92, 21).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(90, 22).Value = resultList.Items(index).�\�Z����
        .Cells(91, 22).Value = resultList.Items(index).�\������
        .Cells(92, 22).Value = resultList.Items(index).���є���
        .Cells(95, 20).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(96, 20).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(97, 20).Value = resultList.Items(index).���щc�Ƒ��v
        
        '����3������
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(100, 5).Value = resultList.Items(index).�\�ZES
        .Cells(101, 5).Value = resultList.Items(index).�\��ES
        .Cells(102, 5).Value = resultList.Items(index).����ES
        .Cells(100, 6).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(101, 6).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(102, 6).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(100, 7).Value = resultList.Items(index).�\�Z����
        .Cells(101, 7).Value = resultList.Items(index).�\������
        .Cells(102, 7).Value = resultList.Items(index).���є���
        .Cells(105, 5).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(106, 5).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(107, 5).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(100, 8).Value = resultList.Items(index).�\�ZES
        .Cells(101, 8).Value = resultList.Items(index).�\��ES
        .Cells(102, 8).Value = resultList.Items(index).����ES
        .Cells(100, 9).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(101, 9).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(102, 9).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(100, 10).Value = resultList.Items(index).�\�Z����
        .Cells(101, 10).Value = resultList.Items(index).�\������
        .Cells(102, 10).Value = resultList.Items(index).���є���
        .Cells(105, 8).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(106, 8).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(107, 8).Value = resultList.Items(index).���щc�Ƒ��v

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(100, 11).Value = resultList.Items(index).�\�ZES
        .Cells(101, 11).Value = resultList.Items(index).�\��ES
        .Cells(102, 11).Value = resultList.Items(index).����ES
        .Cells(100, 12).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(101, 12).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(102, 12).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(100, 13).Value = resultList.Items(index).�\�Z����
        .Cells(101, 13).Value = resultList.Items(index).�\������
        .Cells(102, 13).Value = resultList.Items(index).���є���
        .Cells(105, 11).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(106, 11).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(107, 11).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "75")
        .Cells(100, 14).Value = resultList.Items(index).�\�ZES
        .Cells(101, 14).Value = resultList.Items(index).�\��ES
        .Cells(102, 14).Value = resultList.Items(index).����ES
        .Cells(100, 15).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(101, 15).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(102, 15).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(100, 16).Value = resultList.Items(index).�\�Z����
        .Cells(101, 16).Value = resultList.Items(index).�\������
        .Cells(102, 16).Value = resultList.Items(index).���є���
        .Cells(105, 14).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(106, 14).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(107, 14).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "76")
        .Cells(100, 17).Value = resultList.Items(index).�\�ZES
        .Cells(101, 17).Value = resultList.Items(index).�\��ES
        .Cells(102, 17).Value = resultList.Items(index).����ES
        .Cells(100, 18).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(101, 18).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(102, 18).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(100, 19).Value = resultList.Items(index).�\�Z����
        .Cells(101, 19).Value = resultList.Items(index).�\������
        .Cells(102, 19).Value = resultList.Items(index).���є���
        .Cells(105, 17).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(106, 17).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(107, 17).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "80")
        .Cells(100, 20).Value = resultList.Items(index).�\�ZES
        .Cells(101, 20).Value = resultList.Items(index).�\��ES
        .Cells(102, 20).Value = resultList.Items(index).����ES
        .Cells(100, 21).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(101, 21).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(102, 21).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(100, 22).Value = resultList.Items(index).�\�Z����
        .Cells(101, 22).Value = resultList.Items(index).�\������
        .Cells(102, 22).Value = resultList.Items(index).���є���
        .Cells(105, 20).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(106, 20).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(107, 20).Value = resultList.Items(index).���щc�Ƒ��v
        
        '����4������
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(110, 5).Value = resultList.Items(index).�\�ZES
        .Cells(111, 5).Value = resultList.Items(index).�\��ES
        .Cells(112, 5).Value = resultList.Items(index).����ES
        .Cells(110, 6).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(111, 6).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(112, 6).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(110, 7).Value = resultList.Items(index).�\�Z����
        .Cells(111, 7).Value = resultList.Items(index).�\������
        .Cells(112, 7).Value = resultList.Items(index).���є���
        .Cells(115, 5).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(116, 5).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(117, 5).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(110, 8).Value = resultList.Items(index).�\�ZES
        .Cells(111, 8).Value = resultList.Items(index).�\��ES
        .Cells(112, 8).Value = resultList.Items(index).����ES
        .Cells(110, 9).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(111, 9).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(112, 9).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(110, 10).Value = resultList.Items(index).�\�Z����
        .Cells(111, 10).Value = resultList.Items(index).�\������
        .Cells(112, 10).Value = resultList.Items(index).���є���
        .Cells(115, 8).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(116, 8).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(117, 8).Value = resultList.Items(index).���щc�Ƒ��v

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(110, 11).Value = resultList.Items(index).�\�ZES
        .Cells(111, 11).Value = resultList.Items(index).�\��ES
        .Cells(112, 11).Value = resultList.Items(index).����ES
        .Cells(110, 12).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(111, 12).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(112, 12).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(110, 13).Value = resultList.Items(index).�\�Z����
        .Cells(111, 13).Value = resultList.Items(index).�\������
        .Cells(112, 13).Value = resultList.Items(index).���є���
        .Cells(115, 11).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(116, 11).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(117, 11).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "75")
        .Cells(110, 14).Value = resultList.Items(index).�\�ZES
        .Cells(111, 14).Value = resultList.Items(index).�\��ES
        .Cells(112, 14).Value = resultList.Items(index).����ES
        .Cells(110, 15).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(111, 15).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(112, 15).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(110, 16).Value = resultList.Items(index).�\�Z����
        .Cells(111, 16).Value = resultList.Items(index).�\������
        .Cells(112, 16).Value = resultList.Items(index).���є���
        .Cells(115, 14).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(116, 14).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(117, 14).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "76")
        .Cells(110, 17).Value = resultList.Items(index).�\�ZES
        .Cells(111, 17).Value = resultList.Items(index).�\��ES
        .Cells(112, 17).Value = resultList.Items(index).����ES
        .Cells(110, 18).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(111, 18).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(112, 18).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(110, 19).Value = resultList.Items(index).�\�Z����
        .Cells(111, 19).Value = resultList.Items(index).�\������
        .Cells(112, 19).Value = resultList.Items(index).���є���
        .Cells(115, 17).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(116, 17).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(117, 17).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "80")
        .Cells(110, 20).Value = resultList.Items(index).�\�ZES
        .Cells(111, 20).Value = resultList.Items(index).�\��ES
        .Cells(112, 20).Value = resultList.Items(index).����ES
        .Cells(110, 21).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(111, 21).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(112, 21).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(110, 22).Value = resultList.Items(index).�\�Z����
        .Cells(111, 22).Value = resultList.Items(index).�\������
        .Cells(112, 22).Value = resultList.Items(index).���є���
        .Cells(115, 20).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(116, 20).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(117, 20).Value = resultList.Items(index).���щc�Ƒ��v
        
        '����5������
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(128, 5).Value = resultList.Items(index).�\�ZES
        .Cells(129, 5).Value = resultList.Items(index).�\��ES
        .Cells(130, 5).Value = resultList.Items(index).����ES
        .Cells(128, 6).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(129, 6).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(130, 6).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(128, 7).Value = resultList.Items(index).�\�Z����
        .Cells(129, 7).Value = resultList.Items(index).�\������
        .Cells(130, 7).Value = resultList.Items(index).���є���
        .Cells(133, 5).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(134, 5).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(135, 5).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(128, 8).Value = resultList.Items(index).�\�ZES
        .Cells(129, 8).Value = resultList.Items(index).�\��ES
        .Cells(130, 8).Value = resultList.Items(index).����ES
        .Cells(128, 9).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(129, 9).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(130, 9).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(128, 10).Value = resultList.Items(index).�\�Z����
        .Cells(129, 10).Value = resultList.Items(index).�\������
        .Cells(130, 10).Value = resultList.Items(index).���є���
        .Cells(133, 8).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(134, 8).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(135, 8).Value = resultList.Items(index).���щc�Ƒ��v

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(128, 11).Value = resultList.Items(index).�\�ZES
        .Cells(129, 11).Value = resultList.Items(index).�\��ES
        .Cells(130, 11).Value = resultList.Items(index).����ES
        .Cells(128, 12).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(129, 12).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(130, 12).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(128, 13).Value = resultList.Items(index).�\�Z����
        .Cells(129, 13).Value = resultList.Items(index).�\������
        .Cells(130, 13).Value = resultList.Items(index).���є���
        .Cells(133, 11).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(134, 11).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(135, 11).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "75")
        .Cells(128, 14).Value = resultList.Items(index).�\�ZES
        .Cells(129, 14).Value = resultList.Items(index).�\��ES
        .Cells(130, 14).Value = resultList.Items(index).����ES
        .Cells(128, 15).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(129, 15).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(130, 15).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(128, 16).Value = resultList.Items(index).�\�Z����
        .Cells(129, 16).Value = resultList.Items(index).�\������
        .Cells(130, 16).Value = resultList.Items(index).���є���
        .Cells(133, 14).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(134, 14).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(135, 14).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "76")
        .Cells(128, 17).Value = resultList.Items(index).�\�ZES
        .Cells(129, 17).Value = resultList.Items(index).�\��ES
        .Cells(130, 17).Value = resultList.Items(index).����ES
        .Cells(128, 18).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(129, 18).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(130, 18).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(128, 19).Value = resultList.Items(index).�\�Z����
        .Cells(129, 19).Value = resultList.Items(index).�\������
        .Cells(130, 19).Value = resultList.Items(index).���є���
        .Cells(133, 17).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(134, 17).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(135, 17).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "80")
        .Cells(128, 20).Value = resultList.Items(index).�\�ZES
        .Cells(129, 20).Value = resultList.Items(index).�\��ES
        .Cells(130, 20).Value = resultList.Items(index).����ES
        .Cells(128, 21).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(129, 21).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(130, 21).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(128, 22).Value = resultList.Items(index).�\�Z����
        .Cells(129, 22).Value = resultList.Items(index).�\������
        .Cells(130, 22).Value = resultList.Items(index).���є���
        .Cells(133, 20).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(134, 20).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(135, 20).Value = resultList.Items(index).���щc�Ƒ��v
        
        '����6������
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(138, 5).Value = resultList.Items(index).�\�ZES
        .Cells(139, 5).Value = resultList.Items(index).�\��ES
        .Cells(140, 5).Value = resultList.Items(index).����ES
        .Cells(138, 6).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(139, 6).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(140, 6).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(138, 7).Value = resultList.Items(index).�\�Z����
        .Cells(139, 7).Value = resultList.Items(index).�\������
        .Cells(140, 7).Value = resultList.Items(index).���є���
        .Cells(143, 5).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(144, 5).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(145, 5).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(138, 8).Value = resultList.Items(index).�\�ZES
        .Cells(139, 8).Value = resultList.Items(index).�\��ES
        .Cells(140, 8).Value = resultList.Items(index).����ES
        .Cells(138, 9).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(139, 9).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(140, 9).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(138, 10).Value = resultList.Items(index).�\�Z����
        .Cells(139, 10).Value = resultList.Items(index).�\������
        .Cells(140, 10).Value = resultList.Items(index).���є���
        .Cells(143, 8).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(144, 8).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(145, 8).Value = resultList.Items(index).���щc�Ƒ��v

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(138, 11).Value = resultList.Items(index).�\�ZES
        .Cells(139, 11).Value = resultList.Items(index).�\��ES
        .Cells(140, 11).Value = resultList.Items(index).����ES
        .Cells(138, 12).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(139, 12).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(140, 12).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(138, 13).Value = resultList.Items(index).�\�Z����
        .Cells(139, 13).Value = resultList.Items(index).�\������
        .Cells(140, 13).Value = resultList.Items(index).���є���
        .Cells(143, 11).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(144, 11).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(145, 11).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "75")
        .Cells(138, 14).Value = resultList.Items(index).�\�ZES
        .Cells(139, 14).Value = resultList.Items(index).�\��ES
        .Cells(140, 14).Value = resultList.Items(index).����ES
        .Cells(138, 15).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(139, 15).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(140, 15).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(138, 16).Value = resultList.Items(index).�\�Z����
        .Cells(139, 16).Value = resultList.Items(index).�\������
        .Cells(140, 16).Value = resultList.Items(index).���є���
        .Cells(143, 14).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(144, 14).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(145, 14).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "76")
        .Cells(138, 17).Value = resultList.Items(index).�\�ZES
        .Cells(139, 17).Value = resultList.Items(index).�\��ES
        .Cells(140, 17).Value = resultList.Items(index).����ES
        .Cells(138, 18).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(139, 18).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(140, 18).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(138, 19).Value = resultList.Items(index).�\�Z����
        .Cells(139, 19).Value = resultList.Items(index).�\������
        .Cells(140, 19).Value = resultList.Items(index).���є���
        .Cells(143, 17).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(144, 17).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(145, 17).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "80")
        .Cells(138, 20).Value = resultList.Items(index).�\�ZES
        .Cells(139, 20).Value = resultList.Items(index).�\��ES
        .Cells(140, 20).Value = resultList.Items(index).����ES
        .Cells(138, 21).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(139, 21).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(140, 21).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(138, 22).Value = resultList.Items(index).�\�Z����
        .Cells(139, 22).Value = resultList.Items(index).�\������
        .Cells(140, 22).Value = resultList.Items(index).���є���
        .Cells(143, 20).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(144, 20).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(145, 20).Value = resultList.Items(index).���щc�Ƒ��v
        
        '����7������
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(148, 5).Value = resultList.Items(index).�\�ZES
        .Cells(149, 5).Value = resultList.Items(index).�\��ES
        .Cells(150, 5).Value = resultList.Items(index).����ES
        .Cells(148, 6).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(149, 6).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(150, 5).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(148, 7).Value = resultList.Items(index).�\�Z����
        .Cells(149, 7).Value = resultList.Items(index).�\������
        .Cells(150, 7).Value = resultList.Items(index).���є���
        .Cells(153, 5).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(154, 5).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(155, 5).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(148, 8).Value = resultList.Items(index).�\�ZES
        .Cells(149, 8).Value = resultList.Items(index).�\��ES
        .Cells(150, 8).Value = resultList.Items(index).����ES
        .Cells(148, 9).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(149, 9).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(150, 9).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(148, 10).Value = resultList.Items(index).�\�Z����
        .Cells(149, 10).Value = resultList.Items(index).�\������
        .Cells(150, 10).Value = resultList.Items(index).���є���
        .Cells(153, 8).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(154, 8).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(155, 8).Value = resultList.Items(index).���щc�Ƒ��v

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(148, 11).Value = resultList.Items(index).�\�ZES
        .Cells(149, 11).Value = resultList.Items(index).�\��ES
        .Cells(150, 11).Value = resultList.Items(index).����ES
        .Cells(148, 12).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(149, 12).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(150, 12).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(148, 13).Value = resultList.Items(index).�\�Z����
        .Cells(149, 13).Value = resultList.Items(index).�\������
        .Cells(150, 13).Value = resultList.Items(index).���є���
        .Cells(153, 11).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(154, 11).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(155, 11).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "75")
        .Cells(148, 14).Value = resultList.Items(index).�\�ZES
        .Cells(149, 14).Value = resultList.Items(index).�\��ES
        .Cells(150, 14).Value = resultList.Items(index).����ES
        .Cells(148, 15).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(149, 15).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(150, 15).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(148, 16).Value = resultList.Items(index).�\�Z����
        .Cells(149, 16).Value = resultList.Items(index).�\������
        .Cells(150, 16).Value = resultList.Items(index).���є���
        .Cells(153, 14).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(154, 14).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(155, 14).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "76")
        .Cells(148, 17).Value = resultList.Items(index).�\�ZES
        .Cells(149, 17).Value = resultList.Items(index).�\��ES
        .Cells(150, 17).Value = resultList.Items(index).����ES
        .Cells(148, 18).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(149, 18).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(150, 18).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(148, 19).Value = resultList.Items(index).�\�Z����
        .Cells(149, 19).Value = resultList.Items(index).�\������
        .Cells(150, 19).Value = resultList.Items(index).���є���
        .Cells(153, 17).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(154, 17).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(155, 17).Value = resultList.Items(index).���щc�Ƒ��v
        
        index = resultList.GetIndex(ymdNum, "80")
        .Cells(148, 20).Value = resultList.Items(index).�\�ZES
        .Cells(149, 20).Value = resultList.Items(index).�\��ES
        .Cells(150, 20).Value = resultList.Items(index).����ES
        .Cells(148, 21).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(149, 21).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(150, 21).Value = resultList.Items(index).���т܂Ƃ�
        .Cells(148, 22).Value = resultList.Items(index).�\�Z����
        .Cells(149, 22).Value = resultList.Items(index).�\������
        .Cells(150, 22).Value = resultList.Items(index).���є���
        .Cells(153, 20).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(154, 20).Value = resultList.Items(index).�\���c�Ƒ��v
        .Cells(155, 20).Value = resultList.Items(index).���щc�Ƒ��v
        
        '�X�V���̍X�V
        .Range("Y2").Value = "�X�V�� " & Now
    
    End With
End Sub

' ----------------------------------------------------------
'�u�\���A���p�v�V�[�g�ɗ\�Z��\���l��ݒ�
' ----------------------------------------------------------
Private Sub �\���A��_�\�Z�\���l�ݒ�(resultList As SaleResultList)
    Dim thisYMD As Long
    Dim nextYMD As Long
    Dim nextMonth As Date
    Dim index As Integer

    nextMonth = DateAdd("M", 1, gToday)
    thisYMD = Year(gToday) * 10000 + Month(gToday) * 100 + 1
    nextYMD = Year(nextMonth) * 10000 + Month(nextMonth) * 100 + 1
    
    With Worksheets(sheet�\���A���p)
        '����-�\�Z�A�\��
        index = resultList.GetIndex(thisYMD, "50")
        .Cells(6, 5).Value = resultList.Items(index).�\�ZES
        .Cells(7, 5).Value = resultList.Items(index).�\��ES
        .Cells(6, 6).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(7, 6).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(6, 7).Value = resultList.Items(index).�\�Z����
        .Cells(7, 7).Value = resultList.Items(index).�\������
        .Cells(11, 5).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(12, 5).Value = resultList.Items(index).�\���c�Ƒ��v
        
        index = resultList.GetIndex(thisYMD, "70")
        .Cells(6, 8).Value = resultList.Items(index).�\�ZES
        .Cells(7, 8).Value = resultList.Items(index).�\��ES
        .Cells(6, 9).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(7, 9).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(6, 10).Value = resultList.Items(index).�\�Z����
        .Cells(7, 10).Value = resultList.Items(index).�\������
        .Cells(11, 8).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(12, 8).Value = resultList.Items(index).�\���c�Ƒ��v

        index = resultList.GetIndex(thisYMD, "60")
        .Cells(6, 11).Value = resultList.Items(index).�\�ZES
        .Cells(7, 11).Value = resultList.Items(index).�\��ES
        .Cells(6, 12).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(7, 12).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(6, 13).Value = resultList.Items(index).�\�Z����
        .Cells(7, 13).Value = resultList.Items(index).�\������
        .Cells(11, 11).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(12, 11).Value = resultList.Items(index).�\���c�Ƒ��v
        
        index = resultList.GetIndex(thisYMD, "75")
        .Cells(6, 14).Value = resultList.Items(index).�\�ZES
        .Cells(7, 14).Value = resultList.Items(index).�\��ES
        .Cells(6, 15).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(7, 15).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(6, 16).Value = resultList.Items(index).�\�Z����
        .Cells(7, 16).Value = resultList.Items(index).�\������
        .Cells(11, 14).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(12, 14).Value = resultList.Items(index).�\���c�Ƒ��v
        
        index = resultList.GetIndex(thisYMD, "76")
        .Cells(6, 17).Value = resultList.Items(index).�\�ZES
        .Cells(7, 17).Value = resultList.Items(index).�\��ES
        .Cells(6, 18).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(7, 18).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(6, 19).Value = resultList.Items(index).�\�Z����
        .Cells(7, 19).Value = resultList.Items(index).�\������
        .Cells(11, 17).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(12, 17).Value = resultList.Items(index).�\���c�Ƒ��v
        
        index = resultList.GetIndex(thisYMD, "80")
        .Cells(6, 20).Value = resultList.Items(index).�\�ZES
        .Cells(7, 20).Value = resultList.Items(index).�\��ES
        .Cells(6, 21).Value = resultList.Items(index).�\�Z�܂Ƃ�
        .Cells(7, 21).Value = resultList.Items(index).�\���܂Ƃ�
        .Cells(6, 22).Value = resultList.Items(index).�\�Z����
        .Cells(7, 22).Value = resultList.Items(index).�\������
        .Cells(11, 20).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        .Cells(12, 20).Value = resultList.Items(index).�\���c�Ƒ��v

        '����-�\�Z�A�\��
        index = resultList.GetIndex(nextYMD, "50")
        If index <> -1 Then
            .Cells(15, 5).Value = resultList.Items(index).�\�ZES
            .Cells(15, 6).Value = resultList.Items(index).�\�Z�܂Ƃ�
            .Cells(15, 7).Value = resultList.Items(index).�\�Z����
            .Cells(19, 5).Value = resultList.Items(index).�\�Z�c�Ƒ��v
            
            index = resultList.GetIndex(nextYMD, "70")
            .Cells(15, 8).Value = resultList.Items(index).�\�ZES
            .Cells(15, 9).Value = resultList.Items(index).�\�Z�܂Ƃ�
            .Cells(15, 10).Value = resultList.Items(index).�\�Z����
            .Cells(19, 8).Value = resultList.Items(index).�\�Z�c�Ƒ��v
    
            index = resultList.GetIndex(nextYMD, "60")
            .Cells(15, 11).Value = resultList.Items(index).�\�ZES
            .Cells(15, 12).Value = resultList.Items(index).�\�Z�܂Ƃ�
            .Cells(15, 13).Value = resultList.Items(index).�\�Z����
            .Cells(19, 11).Value = resultList.Items(index).�\�Z�c�Ƒ��v
            
            index = resultList.GetIndex(nextYMD, "75")
            .Cells(15, 14).Value = resultList.Items(index).�\�ZES
            .Cells(15, 15).Value = resultList.Items(index).�\�Z�܂Ƃ�
            .Cells(15, 16).Value = resultList.Items(index).�\�Z����
            .Cells(19, 14).Value = resultList.Items(index).�\�Z�c�Ƒ��v
            
            index = resultList.GetIndex(nextYMD, "76")
            .Cells(15, 17).Value = resultList.Items(index).�\�ZES
            .Cells(15, 18).Value = resultList.Items(index).�\�Z�܂Ƃ�
            .Cells(15, 19).Value = resultList.Items(index).�\�Z����
            .Cells(19, 17).Value = resultList.Items(index).�\�Z�c�Ƒ��v
            
            index = resultList.GetIndex(nextYMD, "80")
            .Cells(15, 20).Value = resultList.Items(index).�\�ZES
            .Cells(15, 21).Value = resultList.Items(index).�\�Z�܂Ƃ�
            .Cells(15, 22).Value = resultList.Items(index).�\�Z����
            .Cells(19, 20).Value = resultList.Items(index).�\�Z�c�Ƒ��v
        End If
    End With
End Sub

' ----------------------------------------------------------
'�u�����i��_�ڍׁv���ђl�i�[
' ----------------------------------------------------------
' [����]
' column:���ђl��ԍ�
' ����Ȗږ�:����Ȗږ�
' ����R�[�h:����R�[�h
' ���z:�������ђl
' ----------------------------------------------------------
Private Sub ���ђl�i�[(column As Integer, ����Ȗږ� As String, ����R�[�h As String, ���z As Long)
    Dim searchRng, rng, tempRng As Range

    With Worksheets(sheet�����i���ڍ�)
        Set searchRng = .Range("B:B")
        Set rng = searchRng.Find(����Ȗږ�, LookAt:=xlWhole)
        Set tempRng = rng
        Do While Not rng Is Nothing
            If .Cells(rng.Row, 3).Value = ����R�[�h Then
                .Cells(rng.Row, column).Value = RoundPrice(���z)
                Exit Sub
            End If
            Set rng = searchRng.FindNext(rng)
            If rng.Address = tempRng.Address Then
                Exit Do
            End If
        Loop
    End With
End Sub

' ----------------------------------------------------------
' ���z���~�P�ʂŎ擾�i�����_�ȉ���P�ʂŎl�̌ܓ��j
' ----------------------------------------------------------
Private Function RoundPrice(price As Long) As Long
    If price > 0 Then
        Dim ret As Double
        ret = price / 1000
        
        'VBA��Round�֐��ł�0.5��1�ɂȂ炸�A0�ƂȂ�
        RoundPrice = WorksheetFunction.Round(ret, 0)
        Exit Function
    End If
    RoundPrice = 0
End Function

' ----------------------------------------------------------
' �V�[�g�̑��݊m�F
' ----------------------------------------------------------
Private Function ExistSheet(shName As String) As Boolean
    For Each ws In Worksheets
        If ws.Name = shName Then
            ExistSheet = True
            Exit Function
        End If
    Next ws
    ExistSheet = False
End Function

' ----------------------------------------------------------
' �V�[�g�̍폜
' ----------------------------------------------------------
Private Sub DeleteSheet(shName As String)
    If ExistSheet(shName) Then
        '���b�Z�[�W�}�~
        Application.DisplayAlerts = False
        Worksheets(shName).Delete
        Application.DisplayAlerts = True
    End If
End Sub

' ----------------------------------------------------------
' ��Ɨp�V�[�g�̍폜
' ----------------------------------------------------------
Private Sub DeleteWorkSheet()
    DeleteSheet (sheet��������\�z)
    DeleteSheet (sheet��������\�z)
    DeleteSheet (sheetES����\�z)
    DeleteSheet (sheet�\�Z�\�����ђl)
    DeleteSheet (sheet�\���A���pES)
    DeleteSheet (sheet�\���A���p�܂Ƃ�)
End Sub

' ----------------------------------------------------------
' ��Ɨp�V�[�g�̒ǉ�
' ----------------------------------------------------------
Private Sub AddWorkSheet()
    Worksheets.Add.Name = sheet��������\�z
    Worksheets.Add.Name = sheet��������\�z
    Worksheets.Add.Name = sheetES����\�z
    Worksheets.Add.Name = sheet�\�Z�\�����ђl
    Worksheets.Add.Name = sheet�\���A���pES
    Worksheets.Add.Name = sheet�\���A���p�܂Ƃ�
End Sub

