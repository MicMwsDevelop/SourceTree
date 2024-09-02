'################################################################
'OnlineDemandDuplicateAlart APPID646 �e���Ɨ��������d���\���A���[�g
'
'�������e
'�e���Ɨ���ƍϐ\�����Ǘ��e�[�u���iT_USE_ONLINE_DEMAND�j�œ������ɂQ��ȏ��Ɨ���\�����Ă���ڋq��
'���݂���ꍇ��mainte_info_sys@mic.jp�ɑ΂��A���[�g���[���𑗐M����
'
'Ver1.00 2024/08/21:�V�K�쐬�i���C�j
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
	conn.Open "Driver={SQL Server}; server=SQLSV; database=charlieDB; uid=web; pwd=02035612;"
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

	Set OpenRecordSet = rs
End Function

'------------------------------------
'         ���C������
'------------------------------------
Sub Main()
	OpenDB
	
	Dim sql : sql = "SELECT [ApplyNo],D.[CustomerID] as �ڋqNo,[fCliName],[GoodsID],[sms_mei],CONVERT(date, [ApplyDate]) as ��t��,CONVERT(date, [SalesDate]) as �����" + _
							" FROM [charlieDB].[dbo].[T_USE_ONLINE_DEMAND] as D" + _
							" INNER JOIN [JunpDB].[dbo].[tClient] as CL on CL.[fCliID] = D.[CustomerID]" + _
							" LEFT JOIN [JunpDB].[dbo].[vMicPCA���i�}�X�^] as M on M.[sms_scd] = D.[GoodsID]" + _
							" INNER JOIN" + _
							" (" + _
							"SELECT [CustomerID], DATEADD(dd, 1, EOMONTH([ApplyDate] , -1)) as ��t��" + _
							" FROM [charlieDB].[dbo].[T_USE_ONLINE_DEMAND]" + _
							" WHERE [CustomerID] < 30000000" + _
							" GROUP BY [CustomerID], DATEADD(dd, 1, EOMONTH([ApplyDate] , -1))" + _
							" HAVING count([CustomerID]) > 1" + _
							") as DU on DU.CustomerID = D.CustomerID" + _
							" ORDER BY D.[CustomerID], [ApplyNo]"
	Dim rs : Set rs = OpenRecordSet(sql)

	If rs.EOF = False Then
		'------------------------------------
		'         �I�u�W�F�N�g�̒�`
		'------------------------------------
		Dim oMsg : Set oMsg = WScript.CreateObject("CDO.Message")

		'------------------------------------
		'         ���M���E���M����`
		'------------------------------------
		oMsg.From = "tasksv@mic.jp"
		oMsg.To = "mainte_info_sys@mic.jp"
		'oMsg.To = "suguro@mic.jp"

		'------------------------------------
		'             �����E�{��
		'------------------------------------
		oMsg.Subject = "�e���Ɨ��������d���\���A���[�g"
		oMsg.HtmlBody = "<html>"
		'oMsg.HtmlBody = oMsg.HtmlBody + "<head><meta http-equiv=Content-Type content=""text/html; charset=iso-2022-jp""></head>"
		oMsg.HtmlBody = oMsg.HtmlBody + "<body>"
		oMsg.HtmlBody = oMsg.HtmlBody + "<font face=""MS UI Gothic"" size=""2"">"
		oMsg.HtmlBody = oMsg.HtmlBody + "<p>�V�X�e���Ǘ����e��<br><br>�e���Ɨ����������ɂQ��ȏ�\������Ă��܂��B���_�ւ̂��m�F�����肢�܂��B</p>"

		oMsg.HtmlBody = oMsg.HtmlBody + "<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
		oMsg.HtmlBody = oMsg.HtmlBody + "<tr>"
		oMsg.HtmlBody = oMsg.HtmlBody + "<th style=""BACKGROUND-COLOR: silver""><font size=2>��t�ԍ�</font></th>"
		oMsg.HtmlBody = oMsg.HtmlBody + "<th style=""BACKGROUND-COLOR: silver""><font size=2>�ڋqNo</font></th>"
		oMsg.HtmlBody = oMsg.HtmlBody + "<th style=""BACKGROUND-COLOR: silver""><font size=2>�ڋq��</font></th>"
		oMsg.HtmlBody = oMsg.HtmlBody + "<th style=""BACKGROUND-COLOR: silver""><font size=2>���i�R�[�h</font></th>"
		oMsg.HtmlBody = oMsg.HtmlBody + "<th style=""BACKGROUND-COLOR: silver""><font size=2>���i��</font></th>"
		oMsg.HtmlBody = oMsg.HtmlBody + "<th style=""BACKGROUND-COLOR: silver""><font size=2>��t��</font></th>"
		oMsg.HtmlBody = oMsg.HtmlBody + "<th style=""BACKGROUND-COLOR: silver""><font size=2>�����</font></th>"
		oMsg.HtmlBody = oMsg.HtmlBody + "</tr>"

		Do While rs.EOF = False
			Dim acceptNo : acceptNo = rs.Fields("ApplyNo").Value
			Dim customerID : customerID = rs.Fields("�ڋqNo").Value
			Dim customerName : customerName = rs.Fields("fCliName").Value
			Dim goodsID : goodsID = rs.Fields("GoodsID").Value
			Dim goodsName : goodsName = rs.Fields("sms_mei").Value
			Dim acceptDate : acceptDate = rs.Fields("��t��").Value
			Dim saleDate : saleDate = rs.Fields("�����").Value

			oMsg.HtmlBody = oMsg.HtmlBody + "<tr>"
			oMsg.HtmlBody = oMsg.HtmlBody + "<td><font size=2>" + CStr(acceptNo) + "</font></td>"
			oMsg.HtmlBody = oMsg.HtmlBody + "<td><font size=2>" + CStr(customerID) + "</font></td>"
			oMsg.HtmlBody = oMsg.HtmlBody + "<td><font size=2>" + customerName + "</font></td>"
			oMsg.HtmlBody = oMsg.HtmlBody + "<td><font size=2>" + goodsID + "</font></td>"
			oMsg.HtmlBody = oMsg.HtmlBody + "<td><font size=2>" + goodsName + "</font></td>"
			If IsNull(acceptDate) = False Then
				oMsg.HtmlBody = oMsg.HtmlBody + "<td><font size=2>" + acceptDate + "</font></td>"
			Else
				oMsg.HtmlBody = oMsg.HtmlBody + "<td><font size=2></font></td>"
			End If
			If IsNull(saleDate) = False Then
				oMsg.HtmlBody = oMsg.HtmlBody + "<td><font size=2>" + saleDate + "</font></td>"
			Else
				oMsg.HtmlBody = oMsg.HtmlBody + "<td><font size=2></font></td>"
			End If
			oMsg.HtmlBody = oMsg.HtmlBody + "</tr>"
			rs.MoveNext
		Loop

		oMsg.HtmlBody = oMsg.HtmlBody  + "</table>"
		oMsg.HtmlBody = oMsg.HtmlBody  + "<p>�ȏ�A��낵�����肢�������܂��B</p>"
		oMsg.HtmlBody = oMsg.HtmlBody  + "</font>"
		oMsg.HtmlBody = oMsg.HtmlBody  + "</body>"
		oMsg.HtmlBody = oMsg.HtmlBody  + "</html>"

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

		oMsg.Fields("urn:schemas:mailheader:X-Mailer") = "vbscript mail"
		oMsg.Fields("urn:schemas:mailheader:Importance") = "High"
		oMsg.Fields("urn:schemas:mailheader:Priority") = 1
		oMsg.Fields("urn:schemas:mailheader:X-Priority") = 1
		oMsg.Fields("urn:schemas:mailheader:X-MsMail-Priority") = "High"
		oMsg.Fields.update

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

	rs.Close
	Set rs = Nothing

	CloseDB

End Sub

'------------------------------------
'       ���C�������Ăяo��
'------------------------------------
Call Main()
