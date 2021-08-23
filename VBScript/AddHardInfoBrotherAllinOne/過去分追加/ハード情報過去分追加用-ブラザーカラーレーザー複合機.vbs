'################################################################
'ハード情報過去分追加用-ブラザーカラーレーザー複合機
'
'本プログラムはこれまで追加されていなかったブラザーカラーレーザー複合機をハード情報に追加するプログラムである。
'本プログラム実行後は日々、ハード情報追加用-ブラザーカラーレーザー複合機.vbsを実行する
'
'処理内容
'(1) 800511 MWS 保険証OCR読込ｾｯﾄ(ﾄﾞﾝｸﾞﾙ&複合機) で売上承認されている受注伝票をピックアップ
'      ※赤黒伝票を考慮して数量の合計が正かどうか判断する
'(2) tMikハード構成で既にプリンタが登録済みでないか判断する
'(3) tMikハード構成の最大ハードNoの抽出
'(4) tMikハード構成にﾌﾞﾗｻﾞｰ(ｶﾗｰﾚｰｻﾞｰ複合機) を登録
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
	OpenDB
	Dim sql1 : sql1 = "SELECT fユーザーコード, sum(f数量) as 数量 FROM tMih受注詳細 as D " + _
						"INNER JOIN tMih受注ヘッダ as H ON H.f受注番号 = D.f受注番号 AND H.f年度 = D.f年度 WHERE D.f商品コード = '800511' AND H.f売上承認日 is not null " + _
						"GROUP BY f商品コード, fユーザーコード ORDER BY fユーザーコード"
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
