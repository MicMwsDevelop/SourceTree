'################################################################
'クラウドバックアップライセンスアラート
'
'処理内容
'クラウドバックアップライセンス管理テーブル（T_USE_CLOUDDATA_LICENSE）で顧客が割り当てられていないライセンスの残数が
'100未満の時にはdensan@mic.jpに対しメールを送信する
'
'Ver1.00 2020/10/16:新規作成（勝呂）
'Ver1.01 2021/05/17:64bit対応の為、メール送信方式をbasp21からCDO方式に変更（勝呂）
'Ver1.02 2021/06/02:本社移転に伴い実行環境をxenappsvからtasksvに移動（勝呂）
'
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
	conn.Open "Driver={SQL Server}; server=SQLSV; database=charlieDB; uid=ww_reader; pwd=20150801;"
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

	Set openRecordSet = rs
End Function

'------------------------------------
'         結果出力
'------------------------------------
Function GetResult(rs)
	Dim ret : ret = 0
	If rs.EOF = False Then
		ret = rs.Fields("LicCnt").Value
	End If
	GetResult = ret
End Function

'------------------------------------
'         メイン処理
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
		'         オブジェクトの定義
		'------------------------------------
		Dim oMsg : Set oMsg = WScript.CreateObject("CDO.Message")

'------------------------------------
		'         送信元・送信先を定義
		'------------------------------------
		oMsg.From = "tasksv@mic.jp"
		oMsg.To = "densan@mic.jp"

		'------------------------------------
		'             件名・本文
		'------------------------------------
		oMsg.Subject = "クラウドバックアップ ライセンスアラート"
		oMsg.TextBody = "クラウドバックアップのライセンス残数が" & CStr(lic) & " 個になりました。" & vbCrlf & "ライセンスを追加してください。" & vbCrlf & vbCrlf & "営業管理部"

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
End Sub

'------------------------------------
'       メイン処理呼び出し
'------------------------------------
Call Main()
