'################################################################
'ハード情報追加-ブラザーカラーレーザー複合機
'
'製作背景
'サービスセンターからの依頼 「Brotherスキャナのハード情報登録について」 2021/07/15 (木) 12:49受信
'800511 MWS 保険証OCR読込ｾｯﾄ(ﾄﾞﾝｸﾞﾙ&複合機) の受注の際に、本来は売上承認時にWWのハード情報のプリンタに
'017752 ﾌﾞﾗｻﾞｰ(ｶﾗｰﾚｰｻﾞｰ複合機) MFC-L8610CDWを追加したいが、800511がセット商品でソフト付属品となっているため
'追加されない。
'
'処理内容
'(1) 800511 MWS 保険証OCR読込ｾｯﾄ(ﾄﾞﾝｸﾞﾙ&複合機) で当日分の売上承認されている受注伝票をピックアップ
'      ※赤黒伝票を考慮して数量の合計が正かどうか判断する
'(2) tMikハード構成で既にプリンタが登録済みでないか判断する
'(3) tMikハード構成の最大ハードNoの抽出
'(4) tMikハード構成にﾌﾞﾗｻﾞｰ(ｶﾗｰﾚｰｻﾞｰ複合機) を登録
'※本処理は当日追加分だけでなく、過去分も検索対象としてるが、処理は軽いし重複して追加されないのでこのままとする。
'   ただし、赤伝分も追加されることで、ハード情報から削除しても、本処理でまた追加されてしまう。
'   問題になるなら当日以降に処理を見直す。
'
'Ver1.00 2021/08/18:新規作成（勝呂）
'
'################################################################
Option Explicit

'------------------------------------
'	変数宣言
'------------------------------------
Dim conn

'------------------------------------
'	ＤＢ接続
'------------------------------------
Sub OpenDB()
	Set conn = WScript.CreateObject("ADODB.Connection")
	conn.Open "Driver={SQL Server}; server=SQLSV; database=junpDB; uid=web; pwd=02035612;"
End Sub

'------------------------------------
'	ＤＢ切断
'------------------------------------
Sub CloseDB()
	conn.Close
	Set conn = Nothing
End Sub

'------------------------------------
'	SQL 実行
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
'	メイン処理
'------------------------------------
Sub Main()
	If WScript.Arguments.Count = 0  Then
		'コマンド引数がない場合には処理終了
		Exit Sub	
	End If

	OpenDB
	Dim sql1 : sql1 = "SELECT fユーザーコード, sum(f数量) as 数量 FROM tMih受注詳細 as D" + _
			" INNER JOIN tMih受注ヘッダ as H ON H.f受注番号 = D.f受注番号 AND H.f年度 = D.f年度" + _
			" WHERE D.f商品コード = '800511' AND convert(int, convert(nvarchar, H.f売上承認日, 112)) >= " & WScript.Arguments(0) + _
			" GROUP BY f商品コード, fユーザーコード ORDER BY fユーザーコード"
	Dim rs1 : Set rs1 = OpenRecordSet(sql1)
	Do While rs1.EOF = False
		Dim clinicCode : clinicCode = rs1.Fields("fユーザーコード").Value
		Dim cnt : cnt = rs1.Fields("数量").Value
		If clinicCode <> 0 And cnt > 0 Then
			Dim sql2 : sql2 = "SELECT COUNT(*) as RecCnt FROM tMikハード構成 WHERE fhaCliMicID = " & CStr(clinicCode) & " AND fha商品コード = '017752'"
			Dim rs2 : Set rs2 = OpenRecordSet(sql2)
			If rs2.EOF = False Then
				Dim rec_cnt : rec_cnt = rs2.Fields("RecCnt").Value
				If rec_cnt = 0 Then
					Dim sql3 : sql3 = "SELECT TOP 1 fhaハードNo as hardNo FROM tMikハード構成 WHERE fhaCliMicID = " & CStr(clinicCode) & "ORDER BY hardNo DESC"
					Dim rs3 : Set rs3 = OpenRecordSet(sql3)
					If rs3.EOF = False Then
						Dim hardNo : hardNo = rs3.Fields("hardNo").Value
						Dim sql4 : sql4 = "INSERT INTO tMikハード構成 VALUES (" & CStr(clinicCode) & ", " & CStr(hardNo + 1) & ", 14, 'ﾌﾞﾗｻﾞｰ(ｶﾗｰﾚｰｻﾞｰ複合機) MFC-L8610CDW', '', CONVERT(nvarchar, getdate(), 11), getdate(), '営業管理部', CONVERT(nvarchar, getdate(), 11), '017752')"
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
'       メイン処理呼び出し
'------------------------------------
Call Main()
