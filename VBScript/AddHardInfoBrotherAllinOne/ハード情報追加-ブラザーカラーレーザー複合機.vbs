'################################################################
'�n�[�h���ǉ�-�u���U�[�J���[���[�U�[�����@
'
'����w�i
'�T�[�r�X�Z���^�[����̈˗� �uBrother�X�L���i�̃n�[�h���o�^�ɂ��āv 2021/07/15 (��) 12:49��M
'800511 MWS �ی���OCR�Ǎ����(��ݸ��&�����@) �̎󒍂̍ۂɁA�{���͔��㏳�F����WW�̃n�[�h���̃v�����^��
'017752 ��׻ް(�װڰ�ް�����@) MFC-L8610CDW��ǉ����������A800511���Z�b�g���i�Ń\�t�g�t���i�ƂȂ��Ă��邽��
'�ǉ�����Ȃ��B
'
'�������e
'(1) 800511 MWS �ی���OCR�Ǎ����(��ݸ��&�����@) �œ������̔��㏳�F����Ă���󒍓`�[���s�b�N�A�b�v
'      ���ԍ��`�[���l�����Đ��ʂ̍��v�������ǂ������f����
'(2) tMik�n�[�h�\���Ŋ��Ƀv�����^���o�^�ς݂łȂ������f����
'(3) tMik�n�[�h�\���̍ő�n�[�hNo�̒��o
'(4) tMik�n�[�h�\������׻ް(�װڰ�ް�����@) ��o�^
'���{�����͓����ǉ��������łȂ��A�ߋ����������ΏۂƂ��Ă邪�A�����͌y�����d�����Ēǉ�����Ȃ��̂ł��̂܂܂Ƃ���B
'   �������A�ԓ`�����ǉ�����邱�ƂŁA�n�[�h��񂩂�폜���Ă��A�{�����ł܂��ǉ�����Ă��܂��B
'   ���ɂȂ�Ȃ瓖���ȍ~�ɏ������������B
'
'Ver1.00 2021/08/18:�V�K�쐬�i���C�j
'
'################################################################
Option Explicit

'------------------------------------
'	�ϐ��錾
'------------------------------------
Dim conn

'------------------------------------
'	�c�a�ڑ�
'------------------------------------
Sub OpenDB()
	Set conn = WScript.CreateObject("ADODB.Connection")
	conn.Open "Driver={SQL Server}; server=SQLSV; database=junpDB; uid=web; pwd=02035612;"
End Sub

'------------------------------------
'	�c�a�ؒf
'------------------------------------
Sub CloseDB()
	conn.Close
	Set conn = Nothing
End Sub

'------------------------------------
'	SQL ���s
'------------------------------------
Function OpenRecordSet(sql)
	Dim rs : Set rs = WScript.CreateObject("ADODB.Recordset")
	With rs
		.ActiveConnection = conn
		.CursorType = 0
		.LockType = 1
		.Source = sql
		.Open
	End With

	Set openRecordSet = rs
End Function

'------------------------------------
'	���C������
'------------------------------------
Sub Main()
	If WScript.Arguments.Count = 0  Then
		'�R�}���h�������Ȃ��ꍇ�ɂ͏����I��
		Exit Sub	
	End If

	OpenDB
	Dim sql1 : sql1 = "SELECT f���[�U�[�R�[�h, sum(f����) as ���� FROM tMih�󒍏ڍ� as D" + _
			" INNER JOIN tMih�󒍃w�b�_ as H ON H.f�󒍔ԍ� = D.f�󒍔ԍ� AND H.f�N�x = D.f�N�x" + _
			" WHERE D.f���i�R�[�h = '800511' AND convert(int, convert(nvarchar, H.f���㏳�F��, 112)) >= " & WScript.Arguments(0) + _
			" GROUP BY f���i�R�[�h, f���[�U�[�R�[�h ORDER BY f���[�U�[�R�[�h"
	Dim rs1 : Set rs1 = OpenRecordSet(sql1)
	Do While rs1.EOF = False
		Dim clinicCode : clinicCode = rs1.Fields("f���[�U�[�R�[�h").Value
		Dim cnt : cnt = rs1.Fields("����").Value
		If clinicCode <> 0 And cnt > 0 Then
			Dim sql2 : sql2 = "SELECT COUNT(*) as RecCnt FROM tMik�n�[�h�\�� WHERE fhaCliMicID = " & CStr(clinicCode) & " AND fha���i�R�[�h = '017752'"
			Dim rs2 : Set rs2 = OpenRecordSet(sql2)
			If rs2.EOF = False Then
				Dim rec_cnt : rec_cnt = rs2.Fields("RecCnt").Value
				If rec_cnt = 0 Then
					Dim sql3 : sql3 = "SELECT TOP 1 fha�n�[�hNo as hardNo FROM tMik�n�[�h�\�� WHERE fhaCliMicID = " & CStr(clinicCode) & "ORDER BY hardNo DESC"
					Dim rs3 : Set rs3 = OpenRecordSet(sql3)
					If rs3.EOF = False Then
						Dim hardNo : hardNo = rs3.Fields("hardNo").Value
						Dim sql4 : sql4 = "INSERT INTO tMik�n�[�h�\�� VALUES (" & CStr(clinicCode) & ", " & CStr(hardNo + 1) & ", 14, '��׻ް(�װڰ�ް�����@) MFC-L8610CDW', '', CONVERT(nvarchar, getdate(), 11), getdate(), '�c�ƊǗ���', CONVERT(nvarchar, getdate(), 11), '017752')"
						conn.Execute(sql4)
					End If
					rs3.Close
					Set rs3 = Nothing
				End If
			End If
			rs2.Close
			Set rs2 = Nothing
		End If
		rs1.MoveNext
	Loop
	rs1.Close
	Set rs1 = Nothing

	CloseDB

End Sub

'------------------------------------
'       ���C�������Ăяo��
'------------------------------------
Call Main()
