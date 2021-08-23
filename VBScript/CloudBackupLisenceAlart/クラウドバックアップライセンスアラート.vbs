'################################################################
'�N���E�h�o�b�N�A�b�v���C�Z���X�A���[�g
'
'�������e
'�N���E�h�o�b�N�A�b�v���C�Z���X�Ǘ��e�[�u���iT_USE_CLOUDDATA_LICENSE�j�Ōڋq�����蓖�Ă��Ă��Ȃ����C�Z���X�̎c����
'100�����̎��ɂ�densan@mic.jp�ɑ΂����[���𑗐M����
'
'Ver1.00 2020/10/16:�V�K�쐬�i���C�j
'Ver1.01 2021/05/17:64bit�Ή��ׁ̈A���[�����M������basp21����CDO�����ɕύX�i���C�j
'Ver1.02 2021/06/02:�{�Јړ]�ɔ������s����xenappsv����tasksv�Ɉړ��i���C�j
'
'################################################################
Option Explicit

'------------------------------------
'         �ϐ��錾
'------------------------------------
Dim conn

'------------------------------------
'         �c�a�ڑ�
'------------------------------------
Sub OpenDB()
	Set conn = WScript.CreateObject("ADODB.Connection")
	conn.Open "Driver={SQL Server}; server=SQLSV; database=charlieDB; uid=ww_reader; pwd=20150801;"
End Sub

'------------------------------------
'         �c�a�ؒf
'------------------------------------
Sub CloseDB()
	conn.Close
	Set conn = Nothing
End Sub

'------------------------------------
'         SQL ���s
'------------------------------------
Function OpenRecordSet(sql)
	Dim rs : Set rs = WScript.CreateObject("ADODB.Recordset")
	With rs
		.ActiveConnection = conn
		.CursorType = 0		'adOpenForwardOnly
		.LockType = 1		'adLockReadOnly
		.Source = sql
		.Open
	End With

	Set openRecordSet = rs
End Function

'------------------------------------
'         ���ʏo��
'------------------------------------
Function GetResult(rs)
	Dim ret : ret = 0
	If rs.EOF = False Then
		ret = rs.Fields("LicCnt").Value
	End If
	GetResult = ret
End Function

'------------------------------------
'         ���C������
'------------------------------------
Sub Main()
	OpenDB
	
	Dim sql : sql = "SELECT LC.LicCnt FROM (SELECT Count(*) as LicCnt FROM dbo.T_USE_CLOUDDATA_LICENSE WHERE fCustomerID is null) as LC WHERE LC.LicCnt < 100"
	Dim rs : Set rs = OpenRecordSet(sql)
	Dim lic : lic = GetResult(rs) 

	rs.Close
	Set rs = Nothing

	CloseDB

	If lic > 0 Then
		'------------------------------------
		'         �I�u�W�F�N�g�̒�`
		'------------------------------------
		Dim oMsg : Set oMsg = WScript.CreateObject("CDO.Message")

'------------------------------------
		'         ���M���E���M����`
		'------------------------------------
		oMsg.From = "tasksv@mic.jp"
		oMsg.To = "densan@mic.jp"

		'------------------------------------
		'             �����E�{��
		'------------------------------------
		oMsg.Subject = "�N���E�h�o�b�N�A�b�v ���C�Z���X�A���[�g"
		oMsg.TextBody = "�N���E�h�o�b�N�A�b�v�̃��C�Z���X�c����" & CStr(lic) & " �ɂȂ�܂����B" & vbCrlf & "���C�Z���X��ǉ����Ă��������B" & vbCrlf & vbCrlf & "�c�ƊǗ���"

		'------------------------------------
		'            �T�[�o�[�ݒ�
		'------------------------------------
		Dim strConfigurationField : strConfigurationField = "http://schemas.microsoft.com/cdo/configuration/"
		With oMsg.Configuration.Fields
		   .Item(strConfigurationField & "sendusing") = 2                     '�ݒ�l�̐����͈ȉ��ɋL��
		   .Item(strConfigurationField & "smtpserver") = "dove.mic.jp"
		   .Item(strConfigurationField & "smtpserverport") = 25
		   .Item(strConfigurationField & "smtpusessl") = false           'use ssl�̐ݒ�
		   '------------------- smtp�F�؂�ݒ肷��ꍇ�ȉ���ݒ� ------------
		   '.Item(strConfigurationField & "smtpauthenticate") = 2              '1(Basic�F��)/2(NTLM�F��)
		   '.Item(strConfigurationField & "sendusername") = "���M���[�U�[��"     'smtp-auth�𗘗p����ꍇ�K�v
		   '.Item(strConfigurationField & "sendpassword") = "���M�p�X���[�h"     'smtp-auth�𗘗p����ꍇ�K�v
		   '.Item(strConfigurationField & "smtpconnectiontimeout") = 60
		   '--------------------------------------------------------------
		   .Update
		End With

		'------------------------------------
		'          �Y�t�t�@�C���̐ݒ�         (�t�@�C����Y�t���Ȃ��ꍇ�͐ݒ肵�Ȃ�)
		'------------------------------------
		'oMsg.AddAttachment(filePath)          '�Y�t�t�@�C���̐ݒ�

		'------------------------------------
		'               ���M
		'------------------------------------
		oMsg.Send
		Set oMsg = Nothing
	End If
End Sub

'------------------------------------
'       ���C�������Ăяo��
'------------------------------------
Call Main()
