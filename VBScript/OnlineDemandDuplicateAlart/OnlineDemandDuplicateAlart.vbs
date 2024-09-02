'################################################################
'OnlineDemandDuplicateAlart APPID646 各種作業料同月内重複申請アラート
'
'処理内容
'各種作業料作業済申請情報管理テーブル（T_USE_ONLINE_DEMAND）で同月内に２回以上作業料を申請している顧客が
'存在する場合にmainte_info_sys@mic.jpに対しアラートメールを送信する
'
'Ver1.00 2024/08/21:新規作成（勝呂）
'################################################################
Option Explicit

'------------------------------------
'         変数宣言
'------------------------------------
Dim conn

'------------------------------------
'         ＤＢ接続
'------------------------------------
Sub OpenDB()
	Set conn = WScript.CreateObject("ADODB.Connection")
	conn.Open "Driver={SQL Server}; server=SQLSV; database=charlieDB; uid=web; pwd=02035612;"
End Sub

'------------------------------------
'         ＤＢ切断
'------------------------------------
Sub CloseDB()
	conn.Close
	Set conn = Nothing
End Sub

'------------------------------------
'         SQL 実行
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
'         メイン処理
'------------------------------------
Sub Main()
	OpenDB
	
	Dim sql : sql = "SELECT [ApplyNo],D.[CustomerID] as 顧客No,[fCliName],[GoodsID],[sms_mei],CONVERT(date, [ApplyDate]) as 受付日,CONVERT(date, [SalesDate]) as 売上日" + _
							" FROM [charlieDB].[dbo].[T_USE_ONLINE_DEMAND] as D" + _
							" INNER JOIN [JunpDB].[dbo].[tClient] as CL on CL.[fCliID] = D.[CustomerID]" + _
							" LEFT JOIN [JunpDB].[dbo].[vMicPCA商品マスタ] as M on M.[sms_scd] = D.[GoodsID]" + _
							" INNER JOIN" + _
							" (" + _
							"SELECT [CustomerID], DATEADD(dd, 1, EOMONTH([ApplyDate] , -1)) as 受付月" + _
							" FROM [charlieDB].[dbo].[T_USE_ONLINE_DEMAND]" + _
							" WHERE [CustomerID] < 30000000" + _
							" GROUP BY [CustomerID], DATEADD(dd, 1, EOMONTH([ApplyDate] , -1))" + _
							" HAVING count([CustomerID]) > 1" + _
							") as DU on DU.CustomerID = D.CustomerID" + _
							" ORDER BY D.[CustomerID], [ApplyNo]"
	Dim rs : Set rs = OpenRecordSet(sql)

	If rs.EOF = False Then
		'------------------------------------
		'         オブジェクトの定義
		'------------------------------------
		Dim oMsg : Set oMsg = WScript.CreateObject("CDO.Message")

		'------------------------------------
		'         送信元・送信先を定義
		'------------------------------------
		oMsg.From = "tasksv@mic.jp"
		oMsg.To = "mainte_info_sys@mic.jp"
		'oMsg.To = "suguro@mic.jp"

		'------------------------------------
		'             件名・本文
		'------------------------------------
		oMsg.Subject = "各種作業料同月内重複申請アラート"
		oMsg.HtmlBody = "<html>"
		'oMsg.HtmlBody = oMsg.HtmlBody + "<head><meta http-equiv=Content-Type content=""text/html; charset=iso-2022-jp""></head>"
		oMsg.HtmlBody = oMsg.HtmlBody + "<body>"
		oMsg.HtmlBody = oMsg.HtmlBody + "<font face=""MS UI Gothic"" size=""2"">"
		oMsg.HtmlBody = oMsg.HtmlBody + "<p>システム管理部各位<br><br>各種作業料が同月内に２回以上申請されています。拠点へのご確認をお願います。</p>"

		oMsg.HtmlBody = oMsg.HtmlBody + "<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
		oMsg.HtmlBody = oMsg.HtmlBody + "<tr>"
		oMsg.HtmlBody = oMsg.HtmlBody + "<th style=""BACKGROUND-COLOR: silver""><font size=2>受付番号</font></th>"
		oMsg.HtmlBody = oMsg.HtmlBody + "<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
		oMsg.HtmlBody = oMsg.HtmlBody + "<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客名</font></th>"
		oMsg.HtmlBody = oMsg.HtmlBody + "<th style=""BACKGROUND-COLOR: silver""><font size=2>商品コード</font></th>"
		oMsg.HtmlBody = oMsg.HtmlBody + "<th style=""BACKGROUND-COLOR: silver""><font size=2>商品名</font></th>"
		oMsg.HtmlBody = oMsg.HtmlBody + "<th style=""BACKGROUND-COLOR: silver""><font size=2>受付日</font></th>"
		oMsg.HtmlBody = oMsg.HtmlBody + "<th style=""BACKGROUND-COLOR: silver""><font size=2>売上日</font></th>"
		oMsg.HtmlBody = oMsg.HtmlBody + "</tr>"

		Do While rs.EOF = False
			Dim acceptNo : acceptNo = rs.Fields("ApplyNo").Value
			Dim customerID : customerID = rs.Fields("顧客No").Value
			Dim customerName : customerName = rs.Fields("fCliName").Value
			Dim goodsID : goodsID = rs.Fields("GoodsID").Value
			Dim goodsName : goodsName = rs.Fields("sms_mei").Value
			Dim acceptDate : acceptDate = rs.Fields("受付日").Value
			Dim saleDate : saleDate = rs.Fields("売上日").Value

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
		oMsg.HtmlBody = oMsg.HtmlBody  + "<p>以上、よろしくお願いいたします。</p>"
		oMsg.HtmlBody = oMsg.HtmlBody  + "</font>"
		oMsg.HtmlBody = oMsg.HtmlBody  + "</body>"
		oMsg.HtmlBody = oMsg.HtmlBody  + "</html>"

		'------------------------------------
		'            サーバー設定
		'------------------------------------
		Dim strConfigurationField : strConfigurationField = "http://schemas.microsoft.com/cdo/configuration/"
		With oMsg.Configuration.Fields
		   .Item(strConfigurationField & "sendusing") = 2                     '設定値の説明は以下に記載
		   .Item(strConfigurationField & "smtpserver") = "dove.mic.jp"
		   .Item(strConfigurationField & "smtpserverport") = 25
		   .Item(strConfigurationField & "smtpusessl") = false           'use sslの設定
		   '------------------- smtp認証を設定する場合以下を設定 ------------
		   '.Item(strConfigurationField & "smtpauthenticate") = 2              '1(Basic認証)/2(NTLM認証)
		   '.Item(strConfigurationField & "sendusername") = "送信ユーザー名"     'smtp-authを利用する場合必要
		   '.Item(strConfigurationField & "sendpassword") = "送信パスワード"     'smtp-authを利用する場合必要
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
		'          添付ファイルの設定         (ファイルを添付しない場合は設定しない)
		'------------------------------------
		'oMsg.AddAttachment(filePath)          '添付ファイルの設定

		'------------------------------------
		'               送信
		'------------------------------------
		oMsg.Send
		Set oMsg = Nothing
	End If

	rs.Close
	Set rs = Nothing

	CloseDB

End Sub

'------------------------------------
'       メイン処理呼び出し
'------------------------------------
Call Main()
