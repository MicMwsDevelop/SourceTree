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
' 46期_見込進捗自動集計.xlms
'
' 見込進捗自動集計
'
'
'******************************************************************
' Ver1.00(2020/07/09):新規作成 勝呂
' Ver1.01(2020/11/18):期が変わってもプログラム的には変更がない状態に修正 勝呂
'******************************************************************
' 集計月の変更方法
' gToday に集計月初日を設定
'******************************************************************
'
Const sJunpODBC As String = "DSN=WW_DS;UID=ww_reader;PWD=20150801;DATABASE=JunpDB"
Const sCharlieODBC As String = "DSN=WW_DS;UID=ww_reader;PWD=20150801;DATABASE=CharlieDB"
Const adOpenKeyset = 1
Const adLockReadOnly = 1

Const 勘定科目名_palette As String = "palette"
Const 勘定科目名_paletteES As String = "paletteES"
Const 勘定科目名_その他ソフト As String = "その他ｿﾌﾄ"
Const 勘定科目名_ハード As String = "ハード"
Const 勘定科目名_技術指導売上 As String = "技術指導"
Const 勘定科目名_ハード保守 As String = "ハード保守"
Const 勘定科目名_ソフト保守 As String = "ソフト保守"
Const 勘定科目名_周辺機器 As String = "周辺機器"
Const 勘定科目名_その他 As String = "その他売上"
Const 勘定科目名_Curline本体 As String = "Curline本体"
Const 勘定科目名_Curline替ブラシ As String = "Curline替ﾌﾞﾗｼ"

Const sheet予測連絡用 As String = "予測連絡用"          '「予測連絡用」シート名
Const sheet売上実績 As String = "売上実績"              '「売上実績」シート名
Const sheet見込進捗詳細 As String = "見込進捗_詳細"     '「見込進捗_詳細」シート名
Const sheet予測連絡用ES As String = "予測連絡用-ES"     '「予測連絡用-ES」シート名
Const sheet予測連絡用まとめ As String = "予測連絡用-まとめ"     '「予測連絡用-まとめ」シート名
Const sheet当月売上予想 As String = "vMic当月売上予想"  '「vMic当月売上予想」シート名
Const sheet翌月売上予想 As String = "vMic翌月売上予想"  '「vMic翌月売上予想」シート名
Const sheetES売上予想 As String = "vMicES売上予想"      '「vMicES売上予想」シート名
Const sheet予算予測実績値 As String = "予算予測実績値"  '「予算予測実績値」シート名

Private dic部門コード As Object
Private dic商品区分コード As Object
Private dicPCA部門コード As Object
Private gToday As Date    '集計月
Private gThisYM As String
Private gNextYM As String
Private gMatomeList As PredictionList    '「予測連絡用-まとめ」集計結果

' ----------------------------------------------------------
'初期化処理
' ----------------------------------------------------------
Private Sub Workbook_Open()
    
    Set dic部門コード = CreateObject("Scripting.Dictionary")
    dic部門コード.Add "東日本営業部", "081"
    dic部門コード.Add "首都圏営業部", "082"
    dic部門コード.Add "関東営業部", "083"
    dic部門コード.Add "中部営業部", "086"
    dic部門コード.Add "関西営業部", "087"
    dic部門コード.Add "西日本営業部", "085"
    dic部門コード.Add "ヘルスケア営業部", "075"
    dic部門コード.Add "営業管理部", "011"
    
    Set dic商品区分コード = CreateObject("Scripting.Dictionary")
    dic商品区分コード.Add "palette", "28"
    dic商品区分コード.Add "paletteES", "27"
    dic商品区分コード.Add "その他ソフト", "1"
    dic商品区分コード.Add "ハード", "2"
    dic商品区分コード.Add "技術指導売上", "40"
    dic商品区分コード.Add "ハード保守", "7"
    dic商品区分コード.Add "ソフト保守", "3"
    dic商品区分コード.Add "周辺機器", "97"
    dic商品区分コード.Add "その他", "99"
    dic商品区分コード.Add "Curline本体", "201"
    dic商品区分コード.Add "Curline替ブラシ等", "202"
    
    Set dicPCA部門コード = CreateObject("Scripting.Dictionary")
    dicPCA部門コード.Add "東日本営業部", "310"
    dicPCA部門コード.Add "首都圏営業部", "320"
    dicPCA部門コード.Add "関東営業部", "330"
    dicPCA部門コード.Add "中部営業部", "341"
    dicPCA部門コード.Add "関西営業部", "342"
    dicPCA部門コード.Add "西日本営業部", "350"
    dicPCA部門コード.Add "ヘルスケア営業部", "910"
    dicPCA部門コード.Add "営業管理部", "390"

    Set gMatomeList = New PredictionList
    Set gMatomeList.Items = New Collection

End Sub

' ----------------------------------------------------------
'「更新」ボタン押下
' 当月進捗集計及び結果格納
' ----------------------------------------------------------
Public Sub 更新_Click()
    Dim backupFile As String
    Dim objFileSys As Object
    Dim resultList As SaleResultList

    'Worksheets(sheet予測連絡用).Range("Y1").Value = "開始 " & Now
    
    '予測連絡用-まとめ集計結果のクリア
    gMatomeList.ItemClear
        
    '集計月の設定
    gToday = Date
    'gToday = "2020/7/1"
    gThisYM = Format(gToday, "yyyy/MM")  '今月
    gNextYM = Format(DateAdd("M", 1, gToday), "yyyy/MM") '翌月
    
    'バックアップファイルの作成 ex. 46期_見込進捗自動集計_20200713.xlsm
    Set objFileSys = CreateObject("Scripting.FileSystemObject")
    backupFile = ThisWorkbook.Path & "\" & objFileSys.GetBaseName(ThisWorkbook.Name) & "_" & Format(Now, "yyyymmdd") & ".xlsm"
    ActiveWorkbook.SaveCopyAs backupFile
    Set objFileSys = Nothing
    
    '作業用シートの削除
    'Call DeleteWorkSheet
    
    '作業用シートの追加
    Call AddWorkSheet
    
    '売上集計
    'vMic当月売上予想→「vMic当月売上予想」に結果を格納
    'vMic翌月売上予想→「vMic翌月売上予想」に結果を格納
    'vMicES売上予想→「vMicES売上予想」に結果を格納
    '売上実績→「予算予測実績値」に結果を格納
    'ES受注情報→「予測連絡用-ES」に結果を格納
    'まとめ受注情報＋まとめ契約情報→「予測連絡用-まとめ」に結果を格納
    Call 売上集計
    
    '「予算予測実績値」シートを読み込み、リストに集計
    Set resultList = New SaleResultList
    Set resultList.Items = New Collection
    With Worksheets(sheet予算予測実績値)
        Dim i As Long: i = 2
        Do While .Cells(i, 1).Value <> ""
            Dim p As SaleResult: Set p = New SaleResult
            Call p.Initialize(.Range(.Cells(i, 1), .Cells(i, 17)))
            resultList.Items.Add p
            i = i + 1
        Loop
    End With
    
    '「売上実績」シートに予算､予測､実績値を設定（2020/08～2021/07）
    Call 売上実績_予算予測実績値設定(resultList)
    
    '「予測連絡用」シートに予算､予測値を設定（当月、翌月）
    Call 予測連絡_予算予測値設定(resultList)
    
    Dim jissekiThis(6) As Long  '当月売上計
    Dim jissekiNext(6) As Long  '翌月売上計
    Call 見込進捗詳細_進捗自動集計(jissekiThis(), jissekiNext())
    
    Call 予測連絡用_進捗自動集計(jissekiThis(), jissekiNext())

    '作業用シートの削除
    'Call DeleteWorkSheet
    
    Worksheets(sheet予測連絡用).Activate
    
    MsgBox "更新しました。"

End Sub

' ----------------------------------------------------------
' 売上集計
' ----------------------------------------------------------
Private Sub 売上集計()
    Dim strSQL As String
    Dim qt As QueryTable
    Dim cn As Object
    Dim rs As Object

    '当月売上予想の読み込み
    With Worksheets(sheet当月売上予想)
        .Cells.Clear
        strSQL = "SELECT 売上区分, 部門コード, 部門名, 商品区分コード, 商品区分名, 金額" & _
                    " FROM JunpDB.dbo.vMic当月売上予想"
        Set qt = .QueryTables.Add(Connection:="ODBC;" & sJunpODBC, Destination:=.Range("A1"), Sql:=strSQL)
        qt.Name = "当月売上予想"
        qt.SavePassword = True
        qt.Refresh BackgroundQuery:=False
        Set qt = Nothing
    End With

    '翌月売上予想の読み込み
    With Worksheets(sheet翌月売上予想)
        .Cells.Clear
        strSQL = "SELECT 売上区分, 部門コード, 部門名, 商品区分コード, 商品区分名, 金額" & _
                    " FROM JunpDB.dbo.vMic翌月売上予想"
        Set qt = .QueryTables.Add(Connection:="ODBC;" & sJunpODBC, Destination:=.Range("A1"), Sql:=strSQL)
        qt.Name = "翌月売上予想"
        qt.SavePassword = True
        qt.Refresh BackgroundQuery:=False
        Set qt = Nothing
    End With

    'ES売上予想の読み込み
    With Worksheets(sheetES売上予想)
        .Cells.Clear
        strSQL = "SELECT ES.[部門コード],ES.[営業部名],ES.[拠点コード],ES.[拠点名],ES.[顧客No],ES.[顧客名],ES.[受注番号],ES.[受注承認日],ES.[売上承認日],ES.[納期],ES.[売上金額],ES.[計上月]" & _
                    " FROM" & _
                    " (" & _
                    " SELECT [部門コード],[営業部名],[拠点コード],[拠点名],[顧客No],[顧客名],[受注番号],[受注承認日],[売上承認日],[納期],[売上金額],[計上1年目] AS [計上月] FROM [JunpDB].[dbo].[vMicES売上予想]" & _
                    " UNION SELECT [部門コード],[営業部名],[拠点コード],[拠点名],[顧客No],[顧客名],[受注番号],[受注承認日],[売上承認日],[納期],[売上金額],[計上2年目] AS [計上月] FROM [JunpDB].[dbo].[vMicES売上予想]" & _
                    " UNION SELECT [部門コード],[営業部名],[拠点コード],[拠点名],[顧客No],[顧客名],[受注番号],[受注承認日],[売上承認日],[納期],[売上金額],[計上3年目] AS [計上月] FROM [JunpDB].[dbo].[vMicES売上予想]" & _
                    " UNION SELECT [部門コード],[営業部名],[拠点コード],[拠点名],[顧客No],[顧客名],[受注番号],[受注承認日],[売上承認日],[納期],[売上金額],[計上4年目] AS [計上月] FROM [JunpDB].[dbo].[vMicES売上予想]" & _
                    " UNION SELECT [部門コード],[営業部名],[拠点コード],[拠点名],[顧客No],[顧客名],[受注番号],[受注承認日],[売上承認日],[納期],[売上金額],[計上5年目] AS [計上月] FROM [JunpDB].[dbo].[vMicES売上予想]" & _
                    " UNION SELECT [部門コード],[営業部名],[拠点コード],[拠点名],[顧客No],[顧客名],[受注番号],[受注承認日],[売上承認日],[納期],[売上金額],[計上6年目] AS [計上月] FROM [JunpDB].[dbo].[vMicES売上予想]" & _
                    ") AS ES" & _
                    " ORDER BY [部門コード], [顧客No], [計上月]"
        Set qt = .QueryTables.Add(Connection:="ODBC;" & sJunpODBC, Destination:=.Range("A1"), Sql:=strSQL)
        qt.Name = "ES売上予想"
        qt.SavePassword = True
        qt.Refresh BackgroundQuery:=False
        Set qt = Nothing
    End With

    '予算予測実績値の読み込み
    With Worksheets(sheet予算予測実績値)
        .Cells.Clear
        strSQL = "SELECT * FROM charlieDB.dbo.売上実績 ORDER BY 実績日, 営業部コード"
        Set qt = .QueryTables.Add(Connection:="ODBC;" & sCharlieODBC, Destination:=.Range("A1"), Sql:=strSQL)
        qt.Name = "予算予測実績値集計"
        qt.SavePassword = True
        qt.Refresh BackgroundQuery:=False
        Set qt = Nothing
    End With

    Set cn = CreateObject("ADODB.Connection")
    Set rs = CreateObject("ADODB.Recordset")
    cn.ConnectionString = sJunpODBC
    cn.Open
    
    '「予測連絡用-ES」に結果を出力
    With Worksheets(sheet予測連絡用ES)
        'タイトル行出力
        .Range("A1").Value = "売上月"
        .Range("B1").Value = "f受注番号"
        .Range("C1").Value = "f受注日"
        .Range("D1").Value = "f販売先コード"
        .Range("E1").Value = "fユーザーコード"
        .Range("F1").Value = "f販売先"
        .Range("G1").Value = "fユーザー"
        .Range("H1").Value = "f受注金額"
        .Range("I1").Value = "f件名"
        .Range("J1").Value = "f納期"
        .Range("K1").Value = "fリプレース区分"
        .Range("L1").Value = "fリプレース"
        .Range("M1").Value = "f担当者コード"
        .Range("N1").Value = "f担当者名"
        .Range("O1").Value = "fBshCode2"
        .Range("P1").Value = "fBshCode3"
        .Range("Q1").Value = "f担当支店名"
        .Range("R1").Value = "f受注承認日"
        .Range("S1").Value = "f売上承認日"
        .Range("T1").Value = "f請求区分"
        .Range("U1").Value = "f販売店コード"
        .Range("V1").Value = "f販売店"
        .Range("W1").Value = "f販売種別"
        .Range("X1").Value = "fSV利用開始年月"
        .Range("Y1").Value = "fSV利用終了年月"
        
        strSQL = "SELECT iif([f売上承認日] is null, left([f納期], 7), LEFT(CONVERT(nvarchar, [f売上承認日], 111), 7)) as 売上月" & _
                ", H.[f受注番号], convert(nvarchar, [f受注日], 111) as f受注日, [f販売先コード], [fユーザーコード], [f販売先]" & _
                ", [fユーザー], convert(int, [f受注金額]) as f受注金額, [f件名], [f納期]" & _
                ", [fリプレース区分], [fリプレース], [f担当者コード], [f担当者名], [fBshCode2], [fBshCode3], [f担当支店名]" & _
                ", convert(nvarchar, [f受注承認日], 111) as f受注承認日, convert(nvarchar, [f売上承認日], 111) as f売上承認日" & _
                ", [f請求区分], [f販売店コード], [f販売店], [f販売種別], [fSV利用開始年月], [fSV利用終了年月]" & _
                " FROM [JunpDB].[dbo].[tMih受注ヘッダ] AS H" & _
                " LEFT JOIN [JunpDB].[dbo].[tMih受注詳細] AS D ON H.[f受注番号] = D.[f受注番号]" & _
                " LEFT JOIN [JunpDB].[dbo].[vMic全ユーザー4] AS U ON H.[fユーザーコード] = U.[顧客No]" & _
                " WHERE [f商品コード] = '800121' AND [f販売種別] = 1" & _
                " ORDER BY 売上月, H.[f受注番号]"
        
        '[予測連絡用-ES]シートに出力
        rs.Open strSQL, cn, adOpenKeyset, adLockReadOnly
        .Range("A2").CopyFromRecordset Data:=rs
        rs.Close
    End With

    '「予測連絡用-まとめ」に結果を出力
    'まとめWonderWeb起票分
    strSQL = "SELECT iif([f売上承認日] Is Null, Left([f納期], 7), Left(Convert(NVARCHAR, [f売上承認日], 111), 7)) AS 売上月,U.[営業部コード] AS 営業部コード,U.[営業部名] AS 営業部名" & _
            ",U.[拠点コード] AS 拠点コード,U.[拠点名] AS 拠点名,[f担当者コード] AS 担当者コード,[f担当者名] AS 担当者,H.[f受注番号] AS 受注番号,[fユーザーコード] AS 顧客No,[fユーザー] AS 顧客名" & _
            ",[f商品コード] AS 商品コード,[f数量] AS 数量,[fSV利用開始年月] AS 課金開始日,[fSV利用終了年月] AS 課金終了日, CONVERT(int, [f標準価格]) AS 金額" & _
            " FROM [JunpDB].[dbo].[tMih受注ヘッダ] AS H" & _
            " LEFT JOIN [JunpDB].[dbo].[tMih受注詳細] AS D ON H.[f受注番号] = D.[f受注番号]" & _
            " LEFT JOIN [JunpDB].[dbo].[vMic全ユーザー4] AS U ON H.[fユーザーコード] = U.[顧客No]" & _
            " WHERE ([f商品コード] = '800155' OR [f商品コード] = '800156' OR [f商品コード] = '800157' OR [f商品コード] = '800158' OR [f商品コード] = '800159') AND [f販売種別] = 4"
    
    rs.Open strSQL, cn, adOpenKeyset, adLockReadOnly
    Do While rs.EOF = False
        Dim p As Prediction: Set p = New Prediction
        p.売上月 = rs.Fields("売上月")
        p.営業部コード = rs.Fields("営業部コード")
        p.営業部名 = rs.Fields("営業部名")
        p.拠点コード = rs.Fields("拠点コード")
        p.拠点名 = rs.Fields("拠点名")
        p.担当者コード = rs.Fields("担当者コード")
        p.担当者 = rs.Fields("担当者")
        p.受注番号 = rs.Fields("受注番号")
        p.顧客No = rs.Fields("顧客No")
        p.顧客名 = rs.Fields("顧客名")
        p.商品コード = rs.Fields("商品コード")
        p.数量 = rs.Fields("数量")
        p.課金開始日 = rs.Fields("課金開始日")
        p.課金終了日 = rs.Fields("課金終了日")
        p.金額 = rs.Fields("金額")
        gMatomeList.Items.Add p
        
        rs.MoveNext
    Loop
    rs.Close
    
    'まとめ契約情報
    strSQL = "SELECT LEFT(Convert(NVARCHAR, EoMonth(H.fContractStartDate, -1), 111), 7) As 売上月,U.[営業部コード] AS 営業部コード,U.[営業部名] AS 営業部名,U.[拠点コード] AS 拠点コード,U.[拠点名] AS 拠点名" & _
            ",iif(U.[営業担当者コード] is null, '', U.[営業担当者コード]) AS 担当者コード,iif(U.[営業担当者名] is null, '', U.[営業担当者名]) AS 担当者,'' AS 受注番号,H.fCustomerID AS 顧客No,U.顧客名 AS 顧客名,H.fGoodsID as 商品コード,1 AS 数量" & _
            ",LEFT(CONVERT(NVARCHAR, H.fContractStartDate, 111), 7) AS 課金開始日,iif(H.fBillingEndDate is null, '', LEFT(CONVERT(NVARCHAR, H.fBillingEndDate, 111), 7)) AS 課金終了日, [fTotalAmount] AS 金額" & _
            " FROM [charlieDB].[dbo].[T_USE_CONTRACT_HEADER] AS H" & _
            " LEFT JOIN [JunpDB].[dbo].[vMic全ユーザー4] AS U ON H.fCustomerID = U.[顧客No]" & _
            " WHERE H.fContractType = 'まとめ'"

    rs.Open strSQL, cn, adOpenKeyset, adLockReadOnly
    Do While rs.EOF = False
        Dim p2 As Prediction: Set p2 = New Prediction
        p2.売上月 = rs.Fields("売上月")
        p2.営業部コード = rs.Fields("営業部コード")
        p2.営業部名 = rs.Fields("営業部名")
        p2.拠点コード = rs.Fields("拠点コード")
        p2.拠点名 = rs.Fields("拠点名")
        p2.担当者コード = rs.Fields("担当者コード")
        p2.担当者 = rs.Fields("担当者")
        p2.受注番号 = rs.Fields("受注番号")
        p2.顧客No = rs.Fields("顧客No")
        p2.顧客名 = rs.Fields("顧客名")
        p2.商品コード = rs.Fields("商品コード")
        p2.数量 = rs.Fields("数量")
        p2.課金開始日 = rs.Fields("課金開始日")
        p2.課金終了日 = rs.Fields("課金終了日")
        p2.金額 = rs.Fields("金額")
                
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
    
    '[予測連絡用-まとめ]シートに出力
    With Worksheets(sheet予測連絡用まとめ)
        .Range("A1").Value = "売上月"
        .Range("B1").Value = "営業部コード"
        .Range("C1").Value = "営業部名"
        .Range("D1").Value = "拠点コード"
        .Range("E1").Value = "拠点名"
        .Range("F1").Value = "担当者コード"
        .Range("G1").Value = "担当者"
        .Range("H1").Value = "受注番号"
        .Range("I1").Value = "顧客No"
        .Range("J1").Value = "顧客名"
        .Range("K1").Value = "商品コード"
        .Range("L1").Value = "数量"
        .Range("M1").Value = "課金開始日"
        .Range("N1").Value = "課金終了日"
        .Range("O1").Value = "金額"
        
        Dim i As Integer
        For i = 1 To gMatomeList.Items.Count
            .Cells(i + 1, 1).Value = gMatomeList.Items(i).売上月
            .Cells(i + 1, 2).Value = gMatomeList.Items(i).営業部コード
            .Cells(i + 1, 3).Value = gMatomeList.Items(i).営業部名
            .Cells(i + 1, 4).Value = gMatomeList.Items(i).拠点コード
            .Cells(i + 1, 5).Value = gMatomeList.Items(i).拠点名
            .Cells(i + 1, 6).Value = gMatomeList.Items(i).担当者コード
            .Cells(i + 1, 7).Value = gMatomeList.Items(i).担当者
            .Cells(i + 1, 8).Value = gMatomeList.Items(i).受注番号
            .Cells(i + 1, 9).Value = gMatomeList.Items(i).顧客No
            .Cells(i + 1, 10).Value = gMatomeList.Items(i).顧客名
            .Cells(i + 1, 11).Value = gMatomeList.Items(i).商品コード
            .Cells(i + 1, 12).Value = gMatomeList.Items(i).数量
            .Cells(i + 1, 13).Value = gMatomeList.Items(i).課金開始日
            .Cells(i + 1, 14).Value = gMatomeList.Items(i).課金終了日
            .Cells(i + 1, 15).Value = gMatomeList.Items(i).金額
        Next i
    End With
End Sub

' ----------------------------------------------------------
' 見込進捗_詳細の進捗自動集計
' ----------------------------------------------------------
' [引数]
' jissekiThis():今月分各営業部別進捗売上金額
' jissekiNext():翌月分各営業部別進捗売上金額
' ----------------------------------------------------------
' 作業手順
'(1) 当月実績欄列番号の取得
'(2)「vMic当月売上予想」を読み込み、listSaleThisに集計
'(3)「vMicES売上予想」を読み込み、listESに集計
'(4) 今月分の集計
'(5) 翌月分の集計
'    ※翌月分のpalette売上にはまとめ分を含める
'(6) 更新日の更新
' ----------------------------------------------------------
Private Sub 見込進捗詳細_進捗自動集計(jissekiThis() As Long, jissekiNext() As Long)
    Dim searchRng, rng As Range
    Dim searchTarget As String
    Dim thisCol As Integer
    Dim nextCol As Integer
    Dim price As Long
    
    With Worksheets(sheet見込進捗詳細)
        '(1) 当月実績欄列番号の取得
        searchTarget = Month(gToday) & "月度 (実績)"
        Set searchRng = .Range("E4:AZ4")
        Set rng = searchRng.Find(searchTarget, LookAt:=xlWhole)
        thisCol = rng.column
        nextCol = thisCol + 4
    
        '(2)「vMic当月売上予想」を読み込み、listSaleThisに集計
        Dim listSaleThis As SaleExpectationList: Set listSaleThis = New SaleExpectationList
        Set listSaleThis.Items = New Collection
        Dim i As Long: i = 2
        With Worksheets(sheet当月売上予想)
            Do While .Cells(i, 1).Value <> ""
                Dim p1 As SaleExpectation: Set p1 = New SaleExpectation
                p1.Initialize (.Range(.Cells(i, 1), .Cells(i, 6)))
                listSaleThis.Items.Add p1
                i = i + 1
            Loop
        End With
    
        '(3)「vMicES売上予想」を読み込み、listESに集計
        Dim listES As EsExpectationList: Set listES = New EsExpectationList
        Set listES.Items = New Collection
        i = 2
        With Worksheets(sheetES売上予想)
            Do While .Cells(i, 1).Value <> ""
                Dim p3 As EsExpectation: Set p3 = New EsExpectation
                p3.Initialize (.Range(.Cells(i, 1), .Cells(i, 12)))
                listES.Items.Add p3
                i = i + 1
            Loop
        End With
    
        
        '(4) 今月分の集計
        
        'palette売上
        price = listSaleThis.GetPrice(dic部門コード.Item("東日本営業部"), dic商品区分コード.Item("palette"))
        実績値格納 thisCol, 勘定科目名_palette, dicPCA部門コード.Item("東日本営業部"), price
        jissekiThis(0) = price
        price = listSaleThis.GetPrice(dic部門コード.Item("首都圏営業部"), dic商品区分コード.Item("palette"))
        実績値格納 thisCol, 勘定科目名_palette, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiThis(1) = price
        price = listSaleThis.GetPrice(dic部門コード.Item("関東営業部"), dic商品区分コード.Item("palette"))
        実績値格納 thisCol, 勘定科目名_palette, dicPCA部門コード.Item("関東営業部"), price
        jissekiThis(2) = price
        price = listSaleThis.GetPrice(dic部門コード.Item("中部営業部"), dic商品区分コード.Item("palette"))
        実績値格納 thisCol, 勘定科目名_palette, dicPCA部門コード.Item("中部営業部"), price
        jissekiThis(3) = price
        price = listSaleThis.GetPrice(dic部門コード.Item("関西営業部"), dic商品区分コード.Item("palette"))
        実績値格納 thisCol, 勘定科目名_palette, dicPCA部門コード.Item("関西営業部"), price
        jissekiThis(4) = price
        price = listSaleThis.GetPrice(dic部門コード.Item("西日本営業部"), dic商品区分コード.Item("palette"))
        実績値格納 thisCol, 勘定科目名_palette, dicPCA部門コード.Item("西日本営業部"), price
        jissekiThis(5) = price
    
        'paletteES売上
         price = listSaleThis.GetPrice(dic部門コード.Item("東日本営業部"), dic商品区分コード.Item("paletteES"))
        実績値格納 thisCol, 勘定科目名_paletteES, dicPCA部門コード.Item("東日本営業部"), price
        jissekiThis(0) = jissekiThis(0) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("首都圏営業部"), dic商品区分コード.Item("paletteES"))
        実績値格納 thisCol, 勘定科目名_paletteES, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiThis(1) = jissekiThis(1) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("関東営業部"), dic商品区分コード.Item("paletteES"))
        実績値格納 thisCol, 勘定科目名_paletteES, dicPCA部門コード.Item("関東営業部"), price
        jissekiThis(2) = jissekiThis(2) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("中部営業部"), dic商品区分コード.Item("paletteES"))
        実績値格納 thisCol, 勘定科目名_paletteES, dicPCA部門コード.Item("中部営業部"), price
        jissekiThis(3) = jissekiThis(3) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("関西営業部"), dic商品区分コード.Item("paletteES"))
        実績値格納 thisCol, 勘定科目名_paletteES, dicPCA部門コード.Item("関西営業部"), price
        jissekiThis(4) = jissekiThis(4) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("西日本営業部"), dic商品区分コード.Item("paletteES"))
        実績値格納 thisCol, 勘定科目名_paletteES, dicPCA部門コード.Item("西日本営業部"), price
        jissekiThis(5) = jissekiThis(5) + price
    
        'その他ｿﾌﾄ売上
        price = listSaleThis.GetPrice(dic部門コード.Item("東日本営業部"), dic商品区分コード.Item("その他ソフト"))
        実績値格納 thisCol, 勘定科目名_その他ソフト, dicPCA部門コード.Item("東日本営業部"), price
        jissekiThis(0) = jissekiThis(0) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("首都圏営業部"), dic商品区分コード.Item("その他ソフト"))
        実績値格納 thisCol, 勘定科目名_その他ソフト, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiThis(1) = jissekiThis(1) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("関東営業部"), dic商品区分コード.Item("その他ソフト"))
        実績値格納 thisCol, 勘定科目名_その他ソフト, dicPCA部門コード.Item("関東営業部"), price
        jissekiThis(2) = jissekiThis(2) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("中部営業部"), dic商品区分コード.Item("その他ソフト"))
        実績値格納 thisCol, 勘定科目名_その他ソフト, dicPCA部門コード.Item("中部営業部"), price
        jissekiThis(3) = jissekiThis(3) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("関西営業部"), dic商品区分コード.Item("その他ソフト"))
        実績値格納 thisCol, 勘定科目名_その他ソフト, dicPCA部門コード.Item("関西営業部"), price
        jissekiThis(4) = jissekiThis(4) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("西日本営業部"), dic商品区分コード.Item("その他ソフト"))
        実績値格納 thisCol, 勘定科目名_その他ソフト, dicPCA部門コード.Item("西日本営業部"), price
        jissekiThis(5) = jissekiThis(5) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("ヘルスケア営業部"), dic商品区分コード.Item("その他ソフト"))
        実績値格納 thisCol, 勘定科目名_その他ソフト, dicPCA部門コード.Item("ヘルスケア営業部"), price
    
        'ハード売上高
        price = listSaleThis.GetPrice(dic部門コード.Item("東日本営業部"), dic商品区分コード.Item("ハード"))
        実績値格納 thisCol, 勘定科目名_ハード, dicPCA部門コード.Item("東日本営業部"), price
        jissekiThis(0) = jissekiThis(0) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("首都圏営業部"), dic商品区分コード.Item("ハード"))
        実績値格納 thisCol, 勘定科目名_ハード, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiThis(1) = jissekiThis(1) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("関東営業部"), dic商品区分コード.Item("ハード"))
        実績値格納 thisCol, 勘定科目名_ハード, dicPCA部門コード.Item("関東営業部"), price
        jissekiThis(2) = jissekiThis(2) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("中部営業部"), dic商品区分コード.Item("ハード"))
        実績値格納 thisCol, 勘定科目名_ハード, dicPCA部門コード.Item("中部営業部"), price
        jissekiThis(3) = jissekiThis(3) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("関西営業部"), dic商品区分コード.Item("ハード"))
        実績値格納 thisCol, 勘定科目名_ハード, dicPCA部門コード.Item("関西営業部"), price
        jissekiThis(4) = jissekiThis(4) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("西日本営業部"), dic商品区分コード.Item("ハード"))
        実績値格納 thisCol, 勘定科目名_ハード, dicPCA部門コード.Item("西日本営業部"), price
        jissekiThis(5) = jissekiThis(5) + price

        '技術指導売上
        price = listSaleThis.GetPrice(dic部門コード.Item("東日本営業部"), dic商品区分コード.Item("技術指導売上"))
        実績値格納 thisCol, 勘定科目名_技術指導売上, dicPCA部門コード.Item("東日本営業部"), price
        jissekiThis(0) = jissekiThis(0) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("首都圏営業部"), dic商品区分コード.Item("技術指導売上"))
        実績値格納 thisCol, 勘定科目名_技術指導売上, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiThis(1) = jissekiThis(1) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("関東営業部"), dic商品区分コード.Item("技術指導売上"))
        実績値格納 thisCol, 勘定科目名_技術指導売上, dicPCA部門コード.Item("関東営業部"), price
        jissekiThis(2) = jissekiThis(2) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("中部営業部"), dic商品区分コード.Item("技術指導売上"))
        実績値格納 thisCol, 勘定科目名_技術指導売上, dicPCA部門コード.Item("中部営業部"), price
        jissekiThis(3) = jissekiThis(3) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("関西営業部"), dic商品区分コード.Item("技術指導売上"))
        実績値格納 thisCol, 勘定科目名_技術指導売上, dicPCA部門コード.Item("関西営業部"), price
        jissekiThis(4) = jissekiThis(4) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("西日本営業部"), dic商品区分コード.Item("技術指導売上"))
        実績値格納 thisCol, 勘定科目名_技術指導売上, dicPCA部門コード.Item("西日本営業部"), price
        jissekiThis(5) = jissekiThis(5) + price
        
        'ハード保守
        price = listSaleThis.GetPrice(dic部門コード.Item("東日本営業部"), dic商品区分コード.Item("ハード保守"))
        実績値格納 thisCol, 勘定科目名_ハード保守, dicPCA部門コード.Item("東日本営業部"), price
        jissekiThis(0) = jissekiThis(0) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("首都圏営業部"), dic商品区分コード.Item("ハード保守"))
        実績値格納 thisCol, 勘定科目名_ハード保守, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiThis(1) = jissekiThis(1) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("関東営業部"), dic商品区分コード.Item("ハード保守"))
        実績値格納 thisCol, 勘定科目名_ハード保守, dicPCA部門コード.Item("関東営業部"), price
        jissekiThis(2) = jissekiThis(2) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("中部営業部"), dic商品区分コード.Item("ハード保守"))
        実績値格納 thisCol, 勘定科目名_ハード保守, dicPCA部門コード.Item("中部営業部"), price
        jissekiThis(3) = jissekiThis(3) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("関西営業部"), dic商品区分コード.Item("ハード保守"))
        実績値格納 thisCol, 勘定科目名_ハード保守, dicPCA部門コード.Item("関西営業部"), price
        jissekiThis(4) = jissekiThis(4) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("西日本営業部"), dic商品区分コード.Item("ハード保守"))
        実績値格納 thisCol, 勘定科目名_ハード保守, dicPCA部門コード.Item("西日本営業部"), price
        jissekiThis(5) = jissekiThis(5) + price
    
        'ソフト保守
        price = listES.GetPrice(dic部門コード.Item("東日本営業部"), gThisYM)
        実績値格納 thisCol, 勘定科目名_ソフト保守, dicPCA部門コード.Item("東日本営業部"), price
        jissekiThis(0) = jissekiThis(0) + price
        price = listES.GetPrice(dic部門コード.Item("首都圏営業部"), gThisYM)
        実績値格納 thisCol, 勘定科目名_ソフト保守, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiThis(1) = jissekiThis(1) + price
        price = listES.GetPrice(dic部門コード.Item("関東営業部"), gThisYM)
        実績値格納 thisCol, 勘定科目名_ソフト保守, dicPCA部門コード.Item("関東営業部"), price
        jissekiThis(2) = jissekiThis(2) + price
        price = listES.GetPrice(dic部門コード.Item("中部営業部"), gThisYM)
        実績値格納 thisCol, 勘定科目名_ソフト保守, dicPCA部門コード.Item("中部営業部"), price
        jissekiThis(3) = jissekiThis(3) + price
        price = listES.GetPrice(dic部門コード.Item("関西営業部"), gThisYM)
        実績値格納 thisCol, 勘定科目名_ソフト保守, dicPCA部門コード.Item("関西営業部"), price
        jissekiThis(4) = jissekiThis(4) + price
        price = listES.GetPrice(dic部門コード.Item("西日本営業部"), gThisYM)
        実績値格納 thisCol, 勘定科目名_ソフト保守, dicPCA部門コード.Item("西日本営業部"), price
        jissekiThis(5) = jissekiThis(5) + price

        '周辺機器売上高
        price = listSaleThis.GetPrice(dic部門コード.Item("東日本営業部"), dic商品区分コード.Item("周辺機器"))
        実績値格納 thisCol, 勘定科目名_周辺機器, dicPCA部門コード.Item("東日本営業部"), price
        jissekiThis(0) = jissekiThis(0) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("首都圏営業部"), dic商品区分コード.Item("周辺機器"))
        実績値格納 thisCol, 勘定科目名_周辺機器, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiThis(1) = jissekiThis(1) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("関東営業部"), dic商品区分コード.Item("周辺機器"))
        実績値格納 thisCol, 勘定科目名_周辺機器, dicPCA部門コード.Item("関東営業部"), price
        jissekiThis(2) = jissekiThis(2) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("中部営業部"), dic商品区分コード.Item("周辺機器"))
        実績値格納 thisCol, 勘定科目名_周辺機器, dicPCA部門コード.Item("中部営業部"), price
        jissekiThis(3) = jissekiThis(3) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("関西営業部"), dic商品区分コード.Item("周辺機器"))
        実績値格納 thisCol, 勘定科目名_周辺機器, dicPCA部門コード.Item("関西営業部"), price
        jissekiThis(4) = jissekiThis(4) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("西日本営業部"), dic商品区分コード.Item("周辺機器"))
        実績値格納 thisCol, 勘定科目名_周辺機器, dicPCA部門コード.Item("西日本営業部"), price
        jissekiThis(5) = jissekiThis(5) + price
    
        'その他売上高
        price = listSaleThis.GetPrice(dic部門コード.Item("東日本営業部"), dic商品区分コード.Item("その他"))
        実績値格納 thisCol, 勘定科目名_その他, dicPCA部門コード.Item("東日本営業部"), price
        jissekiThis(0) = jissekiThis(0) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("首都圏営業部"), dic商品区分コード.Item("その他"))
        実績値格納 thisCol, 勘定科目名_その他, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiThis(1) = jissekiThis(1) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("関東営業部"), dic商品区分コード.Item("その他"))
        実績値格納 thisCol, 勘定科目名_その他, dicPCA部門コード.Item("関東営業部"), price
        jissekiThis(2) = jissekiThis(2) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("中部営業部"), dic商品区分コード.Item("その他"))
        実績値格納 thisCol, 勘定科目名_その他, dicPCA部門コード.Item("中部営業部"), price
        jissekiThis(3) = jissekiThis(3) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("関西営業部"), dic商品区分コード.Item("その他"))
        実績値格納 thisCol, 勘定科目名_その他, dicPCA部門コード.Item("関西営業部"), price
        jissekiThis(4) = jissekiThis(4) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("西日本営業部"), dic商品区分コード.Item("その他"))
        実績値格納 thisCol, 勘定科目名_その他, dicPCA部門コード.Item("西日本営業部"), price
        jissekiThis(5) = jissekiThis(5) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("営業管理部"), dic商品区分コード.Item("その他"))
        実績値格納 thisCol, 勘定科目名_その他, dicPCA部門コード.Item("営業管理部"), price
        price = listSaleThis.GetPrice(dic部門コード.Item("ヘルスケア営業部"), dic商品区分コード.Item("その他"))
        実績値格納 thisCol, 勘定科目名_その他, dicPCA部門コード.Item("ヘルスケア営業部"), price
    
        'Curline本体
        price = listSaleThis.GetPrice(dic部門コード.Item("東日本営業部"), dic商品区分コード.Item("Curline本体"))
        実績値格納 thisCol, 勘定科目名_Curline本体, dicPCA部門コード.Item("東日本営業部"), price
        jissekiThis(0) = jissekiThis(0) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("首都圏営業部"), dic商品区分コード.Item("Curline本体"))
        実績値格納 thisCol, 勘定科目名_Curline本体, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiThis(1) = jissekiThis(1) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("関東営業部"), dic商品区分コード.Item("Curline本体"))
        実績値格納 thisCol, 勘定科目名_Curline本体, dicPCA部門コード.Item("関東営業部"), price
        jissekiThis(2) = jissekiThis(2) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("中部営業部"), dic商品区分コード.Item("Curline本体"))
        実績値格納 thisCol, 勘定科目名_Curline本体, dicPCA部門コード.Item("中部営業部"), price
        jissekiThis(3) = jissekiThis(3) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("関西営業部"), dic商品区分コード.Item("Curline本体"))
        実績値格納 thisCol, 勘定科目名_Curline本体, dicPCA部門コード.Item("関西営業部"), price
        jissekiThis(4) = jissekiThis(4) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("西日本営業部"), dic商品区分コード.Item("Curline本体"))
        実績値格納 thisCol, 勘定科目名_Curline本体, dicPCA部門コード.Item("西日本営業部"), price
        jissekiThis(5) = jissekiThis(5) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("営業管理部"), dic商品区分コード.Item("Curline本体"))
        実績値格納 thisCol, 勘定科目名_Curline本体, dicPCA部門コード.Item("営業管理部"), price
        price = listSaleThis.GetPrice(dic部門コード.Item("ヘルスケア営業部"), dic商品区分コード.Item("Curline本体"))
        実績値格納 thisCol, 勘定科目名_Curline本体, dicPCA部門コード.Item("ヘルスケア営業部"), price
        
        'Curline替ブラシ等
        price = listSaleThis.GetPrice(dic部門コード.Item("東日本営業部"), dic商品区分コード.Item("Curline替ブラシ等"))
        実績値格納 thisCol, 勘定科目名_Curline替ブラシ, dicPCA部門コード.Item("東日本営業部"), price
        jissekiThis(0) = jissekiThis(0) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("首都圏営業部"), dic商品区分コード.Item("Curline替ブラシ等"))
        実績値格納 thisCol, 勘定科目名_Curline替ブラシ, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiThis(1) = jissekiThis(1) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("関東営業部"), dic商品区分コード.Item("Curline替ブラシ等"))
        実績値格納 thisCol, 勘定科目名_Curline替ブラシ, dicPCA部門コード.Item("関東営業部"), price
        jissekiThis(2) = jissekiThis(2) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("中部営業部"), dic商品区分コード.Item("Curline替ブラシ等"))
        実績値格納 thisCol, 勘定科目名_Curline替ブラシ, dicPCA部門コード.Item("中部営業部"), price
        jissekiThis(3) = jissekiThis(3) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("関西営業部"), dic商品区分コード.Item("Curline替ブラシ等"))
        実績値格納 thisCol, 勘定科目名_Curline替ブラシ, dicPCA部門コード.Item("関西営業部"), price
        jissekiThis(4) = jissekiThis(4) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("西日本営業部"), dic商品区分コード.Item("Curline替ブラシ等"))
        実績値格納 thisCol, 勘定科目名_Curline替ブラシ, dicPCA部門コード.Item("西日本営業部"), price
        jissekiThis(5) = jissekiThis(5) + price
        price = listSaleThis.GetPrice(dic部門コード.Item("営業管理部"), dic商品区分コード.Item("Curline替ブラシ等"))
        実績値格納 thisCol, 勘定科目名_Curline替ブラシ, dicPCA部門コード.Item("営業管理部"), price
        price = listSaleThis.GetPrice(dic部門コード.Item("ヘルスケア営業部"), dic商品区分コード.Item("Curline替ブラシ等"))
        実績値格納 thisCol, 勘定科目名_Curline替ブラシ, dicPCA部門コード.Item("ヘルスケア営業部"), price
    
        '(5) 翌月分の集計

        '「vMic翌月売上予想」を読み込み、listSaleThisに集計
        Dim listSaleNext As SaleExpectationList: Set listSaleNext = New SaleExpectationList
        Set listSaleNext.Items = New Collection
        i = 2
        With Worksheets(sheet翌月売上予想)
            Do While .Cells(i, 1).Value <> ""
                Dim p2 As SaleExpectation: Set p2 = New SaleExpectation
                p2.Initialize (.Range(.Cells(i, 1), .Cells(i, 6)))
                listSaleNext.Items.Add p2
                i = i + 1
            Loop
        End With
                
        'palette売上
        '翌月分のpalette売上分には、まとめ分が含まれていないので、「予測連絡用-まとめ」の金額を加算する
        price = gMatomeList.GetPalettePrice(gNextYM, "50")
        price = price + listSaleNext.GetPrice(dic部門コード.Item("東日本営業部"), dic商品区分コード.Item("palette"))
        実績値格納 nextCol, 勘定科目名_palette, dicPCA部門コード.Item("東日本営業部"), price
        jissekiNext(0) = price

        price = gMatomeList.GetPalettePrice(gNextYM, "70")
        price = price + listSaleNext.GetPrice(dic部門コード.Item("首都圏営業部"), dic商品区分コード.Item("palette"))
        実績値格納 nextCol, 勘定科目名_palette, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiNext(1) = price

        price = gMatomeList.GetPalettePrice(gNextYM, "60")
        price = price + listSaleNext.GetPrice(dic部門コード.Item("関東営業部"), dic商品区分コード.Item("palette"))
        実績値格納 nextCol, 勘定科目名_palette, dicPCA部門コード.Item("関東営業部"), price
        jissekiNext(2) = price

        price = gMatomeList.GetPalettePrice(gNextYM, "75")
        price = price + listSaleNext.GetPrice(dic部門コード.Item("中部営業部"), dic商品区分コード.Item("palette"))
        実績値格納 nextCol, 勘定科目名_palette, dicPCA部門コード.Item("中部営業部"), price
        jissekiNext(3) = price

        price = gMatomeList.GetPalettePrice(gNextYM, "76")
        price = price + listSaleNext.GetPrice(dic部門コード.Item("関西営業部"), dic商品区分コード.Item("palette"))
        実績値格納 nextCol, 勘定科目名_palette, dicPCA部門コード.Item("関西営業部"), price
        jissekiNext(4) = price

        price = gMatomeList.GetPalettePrice(gNextYM, "80")
        price = price + listSaleNext.GetPrice(dic部門コード.Item("西日本営業部"), dic商品区分コード.Item("palette"))
        実績値格納 nextCol, 勘定科目名_palette, dicPCA部門コード.Item("西日本営業部"), price
        jissekiNext(5) = price
        
        'paletteES売上
        price = listSaleNext.GetPrice(dic部門コード.Item("東日本営業部"), dic商品区分コード.Item("paletteES"))
        実績値格納 nextCol, 勘定科目名_paletteES, dicPCA部門コード.Item("東日本営業部"), price
        jissekiNext(0) = jissekiNext(0) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("首都圏営業部"), dic商品区分コード.Item("paletteES"))
        実績値格納 nextCol, 勘定科目名_paletteES, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiNext(1) = jissekiNext(1) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("関東営業部"), dic商品区分コード.Item("paletteES"))
        実績値格納 nextCol, 勘定科目名_paletteES, dicPCA部門コード.Item("関東営業部"), price
        jissekiNext(2) = jissekiNext(2) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("中部営業部"), dic商品区分コード.Item("paletteES"))
        実績値格納 nextCol, 勘定科目名_paletteES, dicPCA部門コード.Item("中部営業部"), price
        jissekiNext(3) = jissekiNext(3) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("関西営業部"), dic商品区分コード.Item("paletteES"))
        実績値格納 nextCol, 勘定科目名_paletteES, dicPCA部門コード.Item("関西営業部"), price
        jissekiNext(4) = jissekiNext(4) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("西日本営業部"), dic商品区分コード.Item("paletteES"))
        実績値格納 nextCol, 勘定科目名_paletteES, dicPCA部門コード.Item("西日本営業部"), price
        jissekiNext(5) = jissekiNext(5) + price
        
        'その他ｿﾌﾄ売上
        price = listSaleNext.GetPrice(dic部門コード.Item("東日本営業部"), dic商品区分コード.Item("その他ソフト"))
        実績値格納 nextCol, 勘定科目名_その他ソフト, dicPCA部門コード.Item("東日本営業部"), price
        jissekiNext(0) = jissekiNext(0) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("首都圏営業部"), dic商品区分コード.Item("その他ソフト"))
        実績値格納 nextCol, 勘定科目名_その他ソフト, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiNext(1) = jissekiNext(1) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("関東営業部"), dic商品区分コード.Item("その他ソフト"))
        実績値格納 nextCol, 勘定科目名_その他ソフト, dicPCA部門コード.Item("関東営業部"), price
        jissekiNext(2) = jissekiNext(2) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("中部営業部"), dic商品区分コード.Item("その他ソフト"))
        実績値格納 nextCol, 勘定科目名_その他ソフト, dicPCA部門コード.Item("中部営業部"), price
        jissekiNext(3) = jissekiNext(3) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("関西営業部"), dic商品区分コード.Item("その他ソフト"))
        実績値格納 nextCol, 勘定科目名_その他ソフト, dicPCA部門コード.Item("関西営業部"), price
        jissekiNext(4) = jissekiNext(4) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("西日本営業部"), dic商品区分コード.Item("その他ソフト"))
        実績値格納 nextCol, 勘定科目名_その他ソフト, dicPCA部門コード.Item("西日本営業部"), price
        jissekiNext(5) = jissekiNext(5) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("ヘルスケア営業部"), dic商品区分コード.Item("その他ソフト"))
        実績値格納 nextCol, 勘定科目名_その他ソフト, dicPCA部門コード.Item("ヘルスケア営業部"), price
        
        'ハード売上高
        price = listSaleNext.GetPrice(dic部門コード.Item("東日本営業部"), dic商品区分コード.Item("ハード"))
        実績値格納 nextCol, 勘定科目名_ハード, dicPCA部門コード.Item("東日本営業部"), price
        jissekiNext(0) = jissekiNext(0) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("首都圏営業部"), dic商品区分コード.Item("ハード"))
        実績値格納 nextCol, 勘定科目名_ハード, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiNext(1) = jissekiNext(1) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("関東営業部"), dic商品区分コード.Item("ハード"))
        実績値格納 nextCol, 勘定科目名_ハード, dicPCA部門コード.Item("関東営業部"), price
        jissekiNext(2) = jissekiNext(2) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("中部営業部"), dic商品区分コード.Item("ハード"))
        実績値格納 nextCol, 勘定科目名_ハード, dicPCA部門コード.Item("中部営業部"), price
        jissekiNext(3) = jissekiNext(3) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("関西営業部"), dic商品区分コード.Item("ハード"))
        実績値格納 nextCol, 勘定科目名_ハード, dicPCA部門コード.Item("関西営業部"), price
        jissekiNext(4) = jissekiNext(4) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("西日本営業部"), dic商品区分コード.Item("ハード"))
        実績値格納 nextCol, 勘定科目名_ハード, dicPCA部門コード.Item("西日本営業部"), price
        jissekiNext(5) = jissekiNext(5) + price

        '技術指導売上
        price = listSaleNext.GetPrice(dic部門コード.Item("東日本営業部"), dic商品区分コード.Item("技術指導売上"))
        実績値格納 nextCol, 勘定科目名_技術指導売上, dicPCA部門コード.Item("東日本営業部"), price
        jissekiNext(0) = jissekiNext(0) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("首都圏営業部"), dic商品区分コード.Item("技術指導売上"))
        実績値格納 nextCol, 勘定科目名_技術指導売上, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiNext(1) = jissekiNext(1) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("関東営業部"), dic商品区分コード.Item("技術指導売上"))
        実績値格納 nextCol, 勘定科目名_技術指導売上, dicPCA部門コード.Item("関東営業部"), price
        jissekiNext(2) = jissekiNext(2) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("中部営業部"), dic商品区分コード.Item("技術指導売上"))
        実績値格納 nextCol, 勘定科目名_技術指導売上, dicPCA部門コード.Item("中部営業部"), price
        jissekiNext(3) = jissekiNext(3) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("関西営業部"), dic商品区分コード.Item("技術指導売上"))
        実績値格納 nextCol, 勘定科目名_技術指導売上, dicPCA部門コード.Item("関西営業部"), price
        jissekiNext(4) = jissekiNext(4) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("西日本営業部"), dic商品区分コード.Item("技術指導売上"))
        実績値格納 nextCol, 勘定科目名_技術指導売上, dicPCA部門コード.Item("西日本営業部"), price
        jissekiNext(5) = jissekiNext(5) + price
        
        'ハード保守
        price = listSaleNext.GetPrice(dic部門コード.Item("東日本営業部"), dic商品区分コード.Item("ハード保守"))
        実績値格納 nextCol, 勘定科目名_ハード保守, dicPCA部門コード.Item("東日本営業部"), price
        jissekiNext(0) = jissekiNext(0) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("首都圏営業部"), dic商品区分コード.Item("ハード保守"))
        実績値格納 nextCol, 勘定科目名_ハード保守, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiNext(1) = jissekiNext(1) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("関東営業部"), dic商品区分コード.Item("ハード保守"))
        実績値格納 nextCol, 勘定科目名_ハード保守, dicPCA部門コード.Item("関東営業部"), price
        jissekiNext(2) = jissekiNext(2) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("中部営業部"), dic商品区分コード.Item("ハード保守"))
        実績値格納 nextCol, 勘定科目名_ハード保守, dicPCA部門コード.Item("中部営業部"), price
        jissekiNext(3) = jissekiNext(3) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("関西営業部"), dic商品区分コード.Item("ハード保守"))
        実績値格納 nextCol, 勘定科目名_ハード保守, dicPCA部門コード.Item("関西営業部"), price
        jissekiNext(4) = jissekiNext(4) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("西日本営業部"), dic商品区分コード.Item("ハード保守"))
        実績値格納 nextCol, 勘定科目名_ハード保守, dicPCA部門コード.Item("西日本営業部"), price
        jissekiNext(5) = jissekiNext(5) + price
        
        'ソフト保守
        price = listES.GetPrice(dic部門コード.Item("東日本営業部"), gNextYM)
        実績値格納 nextCol, 勘定科目名_ソフト保守, dicPCA部門コード.Item("東日本営業部"), price
        jissekiNext(0) = jissekiNext(0) + price
        price = listES.GetPrice(dic部門コード.Item("首都圏営業部"), gNextYM)
        実績値格納 nextCol, 勘定科目名_ソフト保守, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiNext(1) = jissekiNext(1) + price
        price = listES.GetPrice(dic部門コード.Item("関東営業部"), gNextYM)
        実績値格納 nextCol, 勘定科目名_ソフト保守, dicPCA部門コード.Item("関東営業部"), price
        jissekiNext(2) = jissekiNext(2) + price
        price = listES.GetPrice(dic部門コード.Item("中部営業部"), gNextYM)
        実績値格納 nextCol, 勘定科目名_ソフト保守, dicPCA部門コード.Item("中部営業部"), price
        jissekiNext(3) = jissekiNext(3) + price
        price = listES.GetPrice(dic部門コード.Item("関西営業部"), gNextYM)
        実績値格納 nextCol, 勘定科目名_ソフト保守, dicPCA部門コード.Item("関西営業部"), price
        jissekiNext(4) = jissekiNext(4) + price
        price = listES.GetPrice(dic部門コード.Item("西日本営業部"), gNextYM)
        実績値格納 nextCol, 勘定科目名_ソフト保守, dicPCA部門コード.Item("西日本営業部"), price
        jissekiNext(5) = jissekiNext(5) + price
        
        '周辺機器売上高
        price = listSaleNext.GetPrice(dic部門コード.Item("東日本営業部"), dic商品区分コード.Item("周辺機器"))
        実績値格納 nextCol, 勘定科目名_周辺機器, dicPCA部門コード.Item("東日本営業部"), price
        jissekiNext(0) = jissekiNext(0) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("首都圏営業部"), dic商品区分コード.Item("周辺機器"))
        実績値格納 nextCol, 勘定科目名_周辺機器, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiNext(1) = jissekiNext(1) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("関東営業部"), dic商品区分コード.Item("周辺機器"))
        実績値格納 nextCol, 勘定科目名_周辺機器, dicPCA部門コード.Item("関東営業部"), price
        jissekiNext(2) = jissekiNext(2) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("中部営業部"), dic商品区分コード.Item("周辺機器"))
        実績値格納 nextCol, 勘定科目名_周辺機器, dicPCA部門コード.Item("中部営業部"), price
        jissekiNext(3) = jissekiNext(3) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("関西営業部"), dic商品区分コード.Item("周辺機器"))
        実績値格納 nextCol, 勘定科目名_周辺機器, dicPCA部門コード.Item("関西営業部"), price
        jissekiNext(4) = jissekiNext(4) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("西日本営業部"), dic商品区分コード.Item("周辺機器"))
        実績値格納 nextCol, 勘定科目名_周辺機器, dicPCA部門コード.Item("西日本営業部"), price
        jissekiNext(5) = jissekiNext(5) + price
        
        'その他売上高
        price = listSaleNext.GetPrice(dic部門コード.Item("東日本営業部"), dic商品区分コード.Item("その他"))
        実績値格納 nextCol, 勘定科目名_その他, dicPCA部門コード.Item("東日本営業部"), price
        jissekiNext(0) = jissekiNext(0) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("首都圏営業部"), dic商品区分コード.Item("その他"))
        実績値格納 nextCol, 勘定科目名_その他, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiNext(1) = jissekiNext(1) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("関東営業部"), dic商品区分コード.Item("その他"))
        実績値格納 nextCol, 勘定科目名_その他, dicPCA部門コード.Item("関東営業部"), price
        jissekiNext(2) = jissekiNext(2) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("中部営業部"), dic商品区分コード.Item("その他"))
        実績値格納 nextCol, 勘定科目名_その他, dicPCA部門コード.Item("中部営業部"), price
        jissekiNext(3) = jissekiNext(3) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("関西営業部"), dic商品区分コード.Item("その他"))
        実績値格納 nextCol, 勘定科目名_その他, dicPCA部門コード.Item("関西営業部"), price
        jissekiNext(4) = jissekiNext(4) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("西日本営業部"), dic商品区分コード.Item("その他"))
        実績値格納 nextCol, 勘定科目名_その他, dicPCA部門コード.Item("西日本営業部"), price
        jissekiNext(5) = jissekiNext(5) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("ヘルスケア営業部"), dic商品区分コード.Item("その他"))
        実績値格納 nextCol, 勘定科目名_その他, dicPCA部門コード.Item("ヘルスケア営業部"), price
        price = listSaleNext.GetPrice(dic部門コード.Item("営業管理部"), dic商品区分コード.Item("その他"))
        実績値格納 nextCol, 勘定科目名_その他, dicPCA部門コード.Item("営業管理部"), price
        
        'Curline本体
        price = listSaleNext.GetPrice(dic部門コード.Item("東日本営業部"), dic商品区分コード.Item("Curline本体"))
        実績値格納 nextCol, 勘定科目名_Curline本体, dicPCA部門コード.Item("東日本営業部"), price
        jissekiNext(0) = jissekiNext(0) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("首都圏営業部"), dic商品区分コード.Item("Curline本体"))
        実績値格納 nextCol, 勘定科目名_Curline本体, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiNext(1) = jissekiNext(1) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("関東営業部"), dic商品区分コード.Item("Curline本体"))
        実績値格納 nextCol, 勘定科目名_Curline本体, dicPCA部門コード.Item("関東営業部"), price
        jissekiNext(2) = jissekiNext(2) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("中部営業部"), dic商品区分コード.Item("Curline本体"))
        実績値格納 nextCol, 勘定科目名_Curline本体, dicPCA部門コード.Item("中部営業部"), price
        jissekiNext(3) = jissekiNext(3) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("関西営業部"), dic商品区分コード.Item("Curline本体"))
        実績値格納 nextCol, 勘定科目名_Curline本体, dicPCA部門コード.Item("関西営業部"), price
        jissekiNext(4) = jissekiNext(4) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("西日本営業部"), dic商品区分コード.Item("Curline本体"))
        実績値格納 nextCol, 勘定科目名_Curline本体, dicPCA部門コード.Item("西日本営業部"), price
        jissekiNext(5) = jissekiNext(5) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("営業管理部"), dic商品区分コード.Item("Curline本体"))
        実績値格納 nextCol, 勘定科目名_Curline本体, dicPCA部門コード.Item("営業管理部"), price
        price = listSaleNext.GetPrice(dic部門コード.Item("ヘルスケア営業部"), dic商品区分コード.Item("Curline本体"))
        実績値格納 nextCol, 勘定科目名_Curline本体, dicPCA部門コード.Item("ヘルスケア営業部"), price
        
        'Curline替ブラシ等
        price = listSaleNext.GetPrice(dic部門コード.Item("東日本営業部"), dic商品区分コード.Item("Curline替ブラシ等"))
        実績値格納 nextCol, 勘定科目名_Curline替ブラシ, dicPCA部門コード.Item("東日本営業部"), price
        jissekiNext(0) = jissekiNext(0) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("首都圏営業部"), dic商品区分コード.Item("Curline替ブラシ等"))
        実績値格納 nextCol, 勘定科目名_Curline替ブラシ, dicPCA部門コード.Item("首都圏営業部"), price
        jissekiNext(1) = jissekiNext(1) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("関東営業部"), dic商品区分コード.Item("Curline替ブラシ等"))
        実績値格納 nextCol, 勘定科目名_Curline替ブラシ, dicPCA部門コード.Item("関東営業部"), price
        jissekiNext(2) = jissekiNext(2) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("中部営業部"), dic商品区分コード.Item("Curline替ブラシ等"))
        実績値格納 nextCol, 勘定科目名_Curline替ブラシ, dicPCA部門コード.Item("中部営業部"), price
        jissekiNext(3) = jissekiNext(3) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("関西営業部"), dic商品区分コード.Item("Curline替ブラシ等"))
        実績値格納 nextCol, 勘定科目名_Curline替ブラシ, dicPCA部門コード.Item("関西営業部"), price
        jissekiNext(4) = jissekiNext(4) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("西日本営業部"), dic商品区分コード.Item("Curline替ブラシ等"))
        実績値格納 nextCol, 勘定科目名_Curline替ブラシ, dicPCA部門コード.Item("西日本営業部"), price
        jissekiNext(5) = jissekiNext(5) + price
        price = listSaleNext.GetPrice(dic部門コード.Item("営業管理部"), dic商品区分コード.Item("Curline替ブラシ等"))
        実績値格納 nextCol, 勘定科目名_Curline替ブラシ, dicPCA部門コード.Item("営業管理部"), price
        price = listSaleNext.GetPrice(dic部門コード.Item("ヘルスケア営業部"), dic商品区分コード.Item("Curline替ブラシ等"))
        実績値格納 nextCol, 勘定科目名_Curline替ブラシ, dicPCA部門コード.Item("ヘルスケア営業部"), price
    
        '(6) 更新日の更新
        .Range("A2").Value = "更新日 " & Now
    
    End With
End Sub

' ----------------------------------------------------------
'「予測連絡用」シート 進捗集計及び結果格納
' ----------------------------------------------------------
' [引数]
' jissekiThis():今月分各営業部別進捗売上金額
' jissekiNext():翌月分各営業部別進捗売上金額
' ----------------------------------------------------------
Private Sub 予測連絡用_進捗自動集計(jissekiThis() As Long, jissekiNext() As Long)
    Dim esList As PredictionList
    Dim i As Integer
    Dim thisRow As Integer
    Dim nextRow As Integer
    
    With Worksheets(sheet予測連絡用ES)
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
    
    With Worksheets(sheet予測連絡用)
        thisRow = 8
        nextRow = 16
        
        '===================================
        'ES件数格納 - 今月分
        '===================================
        
        '東日本営業部
        .Cells(thisRow, 5).Value = esList.GetCount(gThisYM, "50")
    
        '首都圏営業部
        .Cells(thisRow, 8).Value = esList.GetCount(gThisYM, "70")
        
        '関東営業部
        .Cells(thisRow, 11).Value = esList.GetCount(gThisYM, "60")
        
        '中部営業部
        .Cells(thisRow, 14).Value = esList.GetCount(gThisYM, "75")
        
        '関西営業部
        .Cells(thisRow, 17).Value = esList.GetCount(gThisYM, "76")
        
        '西日本営業部
        .Cells(thisRow, 20).Value = esList.GetCount(gThisYM, "80")
        
        '===================================
        'まとめ件数格納 - 今月分
        '===================================
        
        '東日本営業部
        .Cells(thisRow, 6).Value = gMatomeList.GetCount(gThisYM, "50")
        
        '首都圏営業部
        .Cells(thisRow, 9).Value = gMatomeList.GetCount(gThisYM, "70")
        
        '関東営業部
        .Cells(thisRow, 12).Value = gMatomeList.GetCount(gThisYM, "60")
        
        '中部営業部
        .Cells(thisRow, 15).Value = gMatomeList.GetCount(gThisYM, "75")
        
        '関西営業部
        .Cells(thisRow, 18).Value = gMatomeList.GetCount(gThisYM, "76")
        
        '西日本営業部
        .Cells(thisRow, 21).Value = gMatomeList.GetCount(gThisYM, "80")
        
        '===================================
        '売上格納 - 今月分
        '===================================
        
        '東日本営業部
        .Cells(thisRow, 7).Value = RoundPrice(jissekiThis(0))
        
        '首都圏営業部
        .Cells(thisRow, 10).Value = RoundPrice(jissekiThis(1))
        
        '関東営業部
        .Cells(thisRow, 13).Value = RoundPrice(jissekiThis(2))
        
        '中部営業部
        .Cells(thisRow, 16).Value = RoundPrice(jissekiThis(3))
        
        '関西営業部
        .Cells(thisRow, 19).Value = RoundPrice(jissekiThis(4))
        
        '西日本営業部
        .Cells(thisRow, 22).Value = RoundPrice(jissekiThis(5))
        
    
        '===================================
        'ES件数格納 - 来月分
        '===================================
        
        '東日本営業部
        .Cells(nextRow, 5).Value = esList.GetCount(gNextYM, "50")
    
        '首都圏営業部
        .Cells(nextRow, 8).Value = esList.GetCount(gNextYM, "70")
        
        '関東営業部
        .Cells(nextRow, 11).Value = esList.GetCount(gNextYM, "60")
        
        '中部営業部
        .Cells(nextRow, 14).Value = esList.GetCount(gNextYM, "75")
        
        '関西営業部
        .Cells(nextRow, 17).Value = esList.GetCount(gNextYM, "76")
        
        '西日本営業部
        .Cells(nextRow, 20).Value = esList.GetCount(gNextYM, "80")
    
    
        '===================================
        'まとめ件数格納 - 来月分
        '===================================
        
        '東日本営業部
        .Cells(nextRow, 6).Value = gMatomeList.GetCount(gNextYM, "50")
        
        '首都圏営業部
        .Cells(nextRow, 9).Value = gMatomeList.GetCount(gNextYM, "70")
        
        '関東営業部
        .Cells(nextRow, 12).Value = gMatomeList.GetCount(gNextYM, "60")
        
        '中部営業部
        .Cells(nextRow, 15).Value = gMatomeList.GetCount(gNextYM, "75")
        
        '関西営業部
        .Cells(nextRow, 18).Value = gMatomeList.GetCount(gNextYM, "76")
        
        '西日本営業部
        .Cells(nextRow, 21).Value = gMatomeList.GetCount(gNextYM, "80")
    
    
        '===================================
        '売上格納 - 来月分
        '===================================
        
        '東日本営業部
        .Cells(nextRow, 7).Value = RoundPrice(jissekiNext(0))
        
        '首都圏営業部
        .Cells(nextRow, 10).Value = RoundPrice(jissekiNext(1))
        
        '関東営業部
        .Cells(nextRow, 13).Value = RoundPrice(jissekiNext(2))
        
        '中部営業部
        .Cells(nextRow, 16).Value = RoundPrice(jissekiNext(3))
        
        '関西営業部
        .Cells(nextRow, 19).Value = RoundPrice(jissekiNext(4))
        
        '西日本営業部
        .Cells(nextRow, 22).Value = RoundPrice(jissekiNext(5))
    
        
        '更新日更新
        .Range("Y2").Value = "更新日 " & Now
    
    End With
End Sub

' ----------------------------------------------------------
'「売上実績」シートに予算､予測､実績値を設定
' ----------------------------------------------------------
Private Sub 売上実績_予算予測実績値設定(resultList As SaleResultList)
    Dim index As Integer
    Dim ymdNum As Long
    Dim ymdDate As Date
    
    With Worksheets(sheet売上実績)
        If Month(gToday) > 7 Then
            ymdNum = (Year(gToday) * 10000) + 801
        Else
            ymdNum = ((Year(gToday) - 1) * 10000) + 801
        End If
        ymdDate = CDate(Format(Str(ymdNum), "####/##/##"))
        
        '上期8月実績
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(6, 5).Value = resultList.Items(index).予算ES
        .Cells(7, 5).Value = resultList.Items(index).予測ES
        .Cells(8, 5).Value = resultList.Items(index).実績ES
        .Cells(6, 6).Value = resultList.Items(index).予算まとめ
        .Cells(7, 6).Value = resultList.Items(index).予測まとめ
        .Cells(8, 6).Value = resultList.Items(index).実績まとめ
        .Cells(6, 7).Value = resultList.Items(index).予算売上
        .Cells(7, 7).Value = resultList.Items(index).予測売上
        .Cells(8, 7).Value = resultList.Items(index).実績売上
        .Cells(11, 5).Value = resultList.Items(index).予算営業損益
        .Cells(12, 5).Value = resultList.Items(index).予測営業損益
        .Cells(13, 5).Value = resultList.Items(index).実績営業損益
   
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(6, 8).Value = resultList.Items(index).予算ES
        .Cells(7, 8).Value = resultList.Items(index).予測ES
        .Cells(8, 8).Value = resultList.Items(index).実績ES
        .Cells(6, 9).Value = resultList.Items(index).予算まとめ
        .Cells(7, 9).Value = resultList.Items(index).予測まとめ
        .Cells(8, 9).Value = resultList.Items(index).実績まとめ
        .Cells(6, 10).Value = resultList.Items(index).予算売上
        .Cells(7, 10).Value = resultList.Items(index).予測売上
        .Cells(8, 10).Value = resultList.Items(index).実績売上
        .Cells(11, 8).Value = resultList.Items(index).予算営業損益
        .Cells(12, 8).Value = resultList.Items(index).予測営業損益
        .Cells(13, 8).Value = resultList.Items(index).実績営業損益

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(6, 11).Value = resultList.Items(index).予算ES
        .Cells(7, 11).Value = resultList.Items(index).予測ES
        .Cells(8, 11).Value = resultList.Items(index).実績ES
        .Cells(6, 12).Value = resultList.Items(index).予算まとめ
        .Cells(7, 12).Value = resultList.Items(index).予測まとめ
        .Cells(8, 12).Value = resultList.Items(index).実績まとめ
        .Cells(6, 13).Value = resultList.Items(index).予算売上
        .Cells(7, 13).Value = resultList.Items(index).予測売上
        .Cells(8, 13).Value = resultList.Items(index).実績売上
        .Cells(11, 11).Value = resultList.Items(index).予算営業損益
        .Cells(12, 11).Value = resultList.Items(index).予測営業損益
        .Cells(13, 11).Value = resultList.Items(index).実績営業損益

        index = resultList.GetIndex(ymdNum, "75")
        .Cells(6, 14).Value = resultList.Items(index).予算ES
        .Cells(7, 14).Value = resultList.Items(index).予測ES
        .Cells(8, 14).Value = resultList.Items(index).実績ES
        .Cells(6, 15).Value = resultList.Items(index).予算まとめ
        .Cells(7, 15).Value = resultList.Items(index).予測まとめ
        .Cells(8, 15).Value = resultList.Items(index).実績まとめ
        .Cells(6, 16).Value = resultList.Items(index).予算売上
        .Cells(7, 16).Value = resultList.Items(index).予測売上
        .Cells(8, 16).Value = resultList.Items(index).実績売上
        .Cells(11, 14).Value = resultList.Items(index).予算営業損益
        .Cells(12, 14).Value = resultList.Items(index).予測営業損益
        .Cells(13, 14).Value = resultList.Items(index).実績営業損益

        index = resultList.GetIndex(ymdNum, "76")
        .Cells(6, 17).Value = resultList.Items(index).予算ES
        .Cells(7, 17).Value = resultList.Items(index).予測ES
        .Cells(8, 17).Value = resultList.Items(index).実績ES
        .Cells(6, 18).Value = resultList.Items(index).予算まとめ
        .Cells(7, 18).Value = resultList.Items(index).予測まとめ
        .Cells(8, 18).Value = resultList.Items(index).実績まとめ
        .Cells(6, 19).Value = resultList.Items(index).予算売上
        .Cells(7, 19).Value = resultList.Items(index).予測売上
        .Cells(8, 19).Value = resultList.Items(index).実績売上
        .Cells(11, 17).Value = resultList.Items(index).予算営業損益
        .Cells(12, 17).Value = resultList.Items(index).予測営業損益
        .Cells(13, 17).Value = resultList.Items(index).実績営業損益

        index = resultList.GetIndex(ymdNum, "80")
        .Cells(6, 20).Value = resultList.Items(index).予算ES
        .Cells(7, 20).Value = resultList.Items(index).予測ES
        .Cells(8, 20).Value = resultList.Items(index).実績ES
        .Cells(6, 21).Value = resultList.Items(index).予算まとめ
        .Cells(7, 21).Value = resultList.Items(index).予測まとめ
        .Cells(8, 21).Value = resultList.Items(index).実績まとめ
        .Cells(6, 22).Value = resultList.Items(index).予算売上
        .Cells(7, 22).Value = resultList.Items(index).予測売上
        .Cells(8, 22).Value = resultList.Items(index).実績売上
        .Cells(11, 20).Value = resultList.Items(index).予算営業損益
        .Cells(12, 20).Value = resultList.Items(index).予測営業損益
        .Cells(13, 20).Value = resultList.Items(index).実績営業損益

        '上期9月実績
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(16, 5).Value = resultList.Items(index).予算ES
        .Cells(17, 5).Value = resultList.Items(index).予測ES
        .Cells(18, 5).Value = resultList.Items(index).実績ES
        .Cells(16, 6).Value = resultList.Items(index).予算まとめ
        .Cells(17, 6).Value = resultList.Items(index).予測まとめ
        .Cells(18, 6).Value = resultList.Items(index).実績まとめ
        .Cells(16, 7).Value = resultList.Items(index).予算売上
        .Cells(17, 7).Value = resultList.Items(index).予測売上
        .Cells(18, 7).Value = resultList.Items(index).実績売上
        .Cells(21, 5).Value = resultList.Items(index).予算営業損益
        .Cells(22, 5).Value = resultList.Items(index).予測営業損益
        .Cells(23, 5).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(16, 8).Value = resultList.Items(index).予算ES
        .Cells(17, 8).Value = resultList.Items(index).予測ES
        .Cells(18, 8).Value = resultList.Items(index).実績ES
        .Cells(16, 9).Value = resultList.Items(index).予算まとめ
        .Cells(17, 9).Value = resultList.Items(index).予測まとめ
        .Cells(18, 9).Value = resultList.Items(index).実績まとめ
        .Cells(16, 10).Value = resultList.Items(index).予算売上
        .Cells(17, 10).Value = resultList.Items(index).予測売上
        .Cells(18, 10).Value = resultList.Items(index).実績売上
        .Cells(21, 8).Value = resultList.Items(index).予算営業損益
        .Cells(22, 8).Value = resultList.Items(index).予測営業損益
        .Cells(23, 8).Value = resultList.Items(index).実績営業損益

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(16, 11).Value = resultList.Items(index).予算ES
        .Cells(17, 11).Value = resultList.Items(index).予測ES
        .Cells(18, 11).Value = resultList.Items(index).実績ES
        .Cells(16, 12).Value = resultList.Items(index).予算まとめ
        .Cells(17, 12).Value = resultList.Items(index).予測まとめ
        .Cells(18, 12).Value = resultList.Items(index).実績まとめ
        .Cells(16, 13).Value = resultList.Items(index).予算売上
        .Cells(17, 13).Value = resultList.Items(index).予測売上
        .Cells(18, 13).Value = resultList.Items(index).実績売上
        .Cells(21, 11).Value = resultList.Items(index).予算営業損益
        .Cells(22, 11).Value = resultList.Items(index).予測営業損益
        .Cells(23, 11).Value = resultList.Items(index).実績営業損益

        index = resultList.GetIndex(ymdNum, "75")
        .Cells(16, 14).Value = resultList.Items(index).予算ES
        .Cells(17, 14).Value = resultList.Items(index).予測ES
        .Cells(18, 14).Value = resultList.Items(index).実績ES
        .Cells(16, 15).Value = resultList.Items(index).予算まとめ
        .Cells(17, 15).Value = resultList.Items(index).予測まとめ
        .Cells(18, 15).Value = resultList.Items(index).実績まとめ
        .Cells(16, 16).Value = resultList.Items(index).予算売上
        .Cells(17, 16).Value = resultList.Items(index).予測売上
        .Cells(18, 16).Value = resultList.Items(index).実績売上
        .Cells(21, 14).Value = resultList.Items(index).予算営業損益
        .Cells(22, 14).Value = resultList.Items(index).予測営業損益
        .Cells(23, 14).Value = resultList.Items(index).実績営業損益

        index = resultList.GetIndex(ymdNum, "76")
        .Cells(16, 17).Value = resultList.Items(index).予算ES
        .Cells(17, 17).Value = resultList.Items(index).予測ES
        .Cells(18, 17).Value = resultList.Items(index).実績ES
        .Cells(16, 18).Value = resultList.Items(index).予算まとめ
        .Cells(17, 18).Value = resultList.Items(index).予測まとめ
        .Cells(18, 18).Value = resultList.Items(index).実績まとめ
        .Cells(16, 19).Value = resultList.Items(index).予算売上
        .Cells(17, 19).Value = resultList.Items(index).予測売上
        .Cells(18, 19).Value = resultList.Items(index).実績売上
        .Cells(21, 17).Value = resultList.Items(index).予算営業損益
        .Cells(22, 17).Value = resultList.Items(index).予測営業損益
        .Cells(23, 17).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "80")
        .Cells(16, 20).Value = resultList.Items(index).予算ES
        .Cells(17, 20).Value = resultList.Items(index).予測ES
        .Cells(18, 20).Value = resultList.Items(index).実績ES
        .Cells(16, 21).Value = resultList.Items(index).予算まとめ
        .Cells(17, 21).Value = resultList.Items(index).予測まとめ
        .Cells(18, 21).Value = resultList.Items(index).実績まとめ
        .Cells(16, 22).Value = resultList.Items(index).予算売上
        .Cells(17, 22).Value = resultList.Items(index).予測売上
        .Cells(18, 22).Value = resultList.Items(index).実績売上
        .Cells(21, 20).Value = resultList.Items(index).予算営業損益
        .Cells(22, 20).Value = resultList.Items(index).予測営業損益
        .Cells(23, 20).Value = resultList.Items(index).実績営業損益
    
        '上期10月実績
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(26, 5).Value = resultList.Items(index).予算ES
        .Cells(27, 5).Value = resultList.Items(index).予測ES
        .Cells(28, 5).Value = resultList.Items(index).実績ES
        .Cells(26, 6).Value = resultList.Items(index).予算まとめ
        .Cells(27, 6).Value = resultList.Items(index).予測まとめ
        .Cells(28, 6).Value = resultList.Items(index).実績まとめ
        .Cells(26, 7).Value = resultList.Items(index).予算売上
        .Cells(27, 7).Value = resultList.Items(index).予測売上
        .Cells(28, 7).Value = resultList.Items(index).実績売上
        .Cells(31, 5).Value = resultList.Items(index).予算営業損益
        .Cells(32, 5).Value = resultList.Items(index).予測営業損益
        .Cells(33, 5).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(26, 8).Value = resultList.Items(index).予算ES
        .Cells(27, 8).Value = resultList.Items(index).予測ES
        .Cells(28, 8).Value = resultList.Items(index).実績ES
        .Cells(26, 9).Value = resultList.Items(index).予算まとめ
        .Cells(27, 9).Value = resultList.Items(index).予測まとめ
        .Cells(28, 9).Value = resultList.Items(index).実績まとめ
        .Cells(26, 10).Value = resultList.Items(index).予算売上
        .Cells(27, 10).Value = resultList.Items(index).予測売上
        .Cells(28, 10).Value = resultList.Items(index).実績売上
        .Cells(31, 8).Value = resultList.Items(index).予算営業損益
        .Cells(32, 8).Value = resultList.Items(index).予測営業損益
        .Cells(33, 8).Value = resultList.Items(index).実績営業損益

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(26, 11).Value = resultList.Items(index).予算ES
        .Cells(27, 11).Value = resultList.Items(index).予測ES
        .Cells(28, 11).Value = resultList.Items(index).実績ES
        .Cells(26, 12).Value = resultList.Items(index).予算まとめ
        .Cells(27, 12).Value = resultList.Items(index).予測まとめ
        .Cells(28, 12).Value = resultList.Items(index).実績まとめ
        .Cells(26, 13).Value = resultList.Items(index).予算売上
        .Cells(27, 13).Value = resultList.Items(index).予測売上
        .Cells(28, 13).Value = resultList.Items(index).実績売上
        .Cells(31, 11).Value = resultList.Items(index).予算営業損益
        .Cells(32, 11).Value = resultList.Items(index).予測営業損益
        .Cells(33, 11).Value = resultList.Items(index).実績営業損益

        index = resultList.GetIndex(ymdNum, "75")
        .Cells(26, 14).Value = resultList.Items(index).予算ES
        .Cells(27, 14).Value = resultList.Items(index).予測ES
        .Cells(28, 14).Value = resultList.Items(index).実績ES
        .Cells(26, 15).Value = resultList.Items(index).予算まとめ
        .Cells(27, 15).Value = resultList.Items(index).予測まとめ
        .Cells(28, 15).Value = resultList.Items(index).実績まとめ
        .Cells(26, 16).Value = resultList.Items(index).予算売上
        .Cells(27, 16).Value = resultList.Items(index).予測売上
        .Cells(28, 16).Value = resultList.Items(index).実績売上
        .Cells(31, 14).Value = resultList.Items(index).予算営業損益
        .Cells(32, 14).Value = resultList.Items(index).予測営業損益
        .Cells(33, 14).Value = resultList.Items(index).実績営業損益

        index = resultList.GetIndex(ymdNum, "76")
        .Cells(26, 17).Value = resultList.Items(index).予算ES
        .Cells(27, 17).Value = resultList.Items(index).予測ES
        .Cells(28, 17).Value = resultList.Items(index).実績ES
        .Cells(26, 18).Value = resultList.Items(index).予算まとめ
        .Cells(27, 18).Value = resultList.Items(index).予測まとめ
        .Cells(28, 18).Value = resultList.Items(index).実績まとめ
        .Cells(26, 19).Value = resultList.Items(index).予算売上
        .Cells(27, 19).Value = resultList.Items(index).予測売上
        .Cells(28, 19).Value = resultList.Items(index).実績売上
        .Cells(31, 17).Value = resultList.Items(index).予算営業損益
        .Cells(32, 17).Value = resultList.Items(index).予測営業損益
        .Cells(33, 17).Value = resultList.Items(index).実績営業損益

        index = resultList.GetIndex(ymdNum, "80")
        .Cells(26, 20).Value = resultList.Items(index).予算ES
        .Cells(27, 20).Value = resultList.Items(index).予測ES
        .Cells(28, 20).Value = resultList.Items(index).実績ES
        .Cells(26, 21).Value = resultList.Items(index).予算まとめ
        .Cells(27, 21).Value = resultList.Items(index).予測まとめ
        .Cells(28, 21).Value = resultList.Items(index).実績まとめ
        .Cells(26, 22).Value = resultList.Items(index).予算売上
        .Cells(27, 22).Value = resultList.Items(index).予測売上
        .Cells(28, 22).Value = resultList.Items(index).実績売上
        .Cells(31, 20).Value = resultList.Items(index).予算営業損益
        .Cells(32, 20).Value = resultList.Items(index).予測営業損益
        .Cells(33, 20).Value = resultList.Items(index).実績営業損益
        
        '上期11月実績
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(44, 5).Value = resultList.Items(index).予算ES
        .Cells(45, 5).Value = resultList.Items(index).予測ES
        .Cells(46, 5).Value = resultList.Items(index).実績ES
        .Cells(44, 6).Value = resultList.Items(index).予算まとめ
        .Cells(45, 6).Value = resultList.Items(index).予測まとめ
        .Cells(46, 6).Value = resultList.Items(index).実績まとめ
        .Cells(44, 7).Value = resultList.Items(index).予算売上
        .Cells(45, 7).Value = resultList.Items(index).予測売上
        .Cells(46, 7).Value = resultList.Items(index).実績売上
        .Cells(49, 5).Value = resultList.Items(index).予算営業損益
        .Cells(50, 5).Value = resultList.Items(index).予測営業損益
        .Cells(51, 5).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(44, 8).Value = resultList.Items(index).予算ES
        .Cells(45, 8).Value = resultList.Items(index).予測ES
        .Cells(46, 8).Value = resultList.Items(index).実績ES
        .Cells(44, 9).Value = resultList.Items(index).予算まとめ
        .Cells(45, 9).Value = resultList.Items(index).予測まとめ
        .Cells(46, 9).Value = resultList.Items(index).実績まとめ
        .Cells(44, 10).Value = resultList.Items(index).予算売上
        .Cells(45, 10).Value = resultList.Items(index).予測売上
        .Cells(46, 10).Value = resultList.Items(index).実績売上
        .Cells(49, 8).Value = resultList.Items(index).予算営業損益
        .Cells(50, 8).Value = resultList.Items(index).予測営業損益
        .Cells(51, 8).Value = resultList.Items(index).実績営業損益

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(44, 11).Value = resultList.Items(index).予算ES
        .Cells(45, 11).Value = resultList.Items(index).予測ES
        .Cells(46, 11).Value = resultList.Items(index).実績ES
        .Cells(44, 12).Value = resultList.Items(index).予算まとめ
        .Cells(45, 12).Value = resultList.Items(index).予測まとめ
        .Cells(46, 12).Value = resultList.Items(index).実績まとめ
        .Cells(44, 13).Value = resultList.Items(index).予算売上
        .Cells(45, 13).Value = resultList.Items(index).予測売上
        .Cells(46, 13).Value = resultList.Items(index).実績売上
        .Cells(49, 11).Value = resultList.Items(index).予算営業損益
        .Cells(50, 11).Value = resultList.Items(index).予測営業損益
        .Cells(51, 11).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "75")
        .Cells(44, 14).Value = resultList.Items(index).予算ES
        .Cells(45, 14).Value = resultList.Items(index).予測ES
        .Cells(46, 14).Value = resultList.Items(index).実績ES
        .Cells(44, 15).Value = resultList.Items(index).予算まとめ
        .Cells(45, 15).Value = resultList.Items(index).予測まとめ
        .Cells(46, 15).Value = resultList.Items(index).実績まとめ
        .Cells(44, 16).Value = resultList.Items(index).予算売上
        .Cells(45, 16).Value = resultList.Items(index).予測売上
        .Cells(46, 16).Value = resultList.Items(index).実績売上
        .Cells(49, 14).Value = resultList.Items(index).予算営業損益
        .Cells(50, 14).Value = resultList.Items(index).予測営業損益
        .Cells(51, 14).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "76")
        .Cells(44, 17).Value = resultList.Items(index).予算ES
        .Cells(45, 17).Value = resultList.Items(index).予測ES
        .Cells(46, 17).Value = resultList.Items(index).実績ES
        .Cells(44, 18).Value = resultList.Items(index).予算まとめ
        .Cells(45, 18).Value = resultList.Items(index).予測まとめ
        .Cells(46, 18).Value = resultList.Items(index).実績まとめ
        .Cells(44, 19).Value = resultList.Items(index).予算売上
        .Cells(45, 19).Value = resultList.Items(index).予測売上
        .Cells(46, 19).Value = resultList.Items(index).実績売上
        .Cells(49, 17).Value = resultList.Items(index).予算営業損益
        .Cells(50, 17).Value = resultList.Items(index).予測営業損益
        .Cells(51, 17).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "80")
        .Cells(44, 20).Value = resultList.Items(index).予算ES
        .Cells(45, 20).Value = resultList.Items(index).予測ES
        .Cells(46, 20).Value = resultList.Items(index).実績ES
        .Cells(44, 21).Value = resultList.Items(index).予算まとめ
        .Cells(45, 21).Value = resultList.Items(index).予測まとめ
        .Cells(46, 21).Value = resultList.Items(index).実績まとめ
        .Cells(44, 22).Value = resultList.Items(index).予算売上
        .Cells(45, 22).Value = resultList.Items(index).予測売上
        .Cells(46, 22).Value = resultList.Items(index).実績売上
        .Cells(49, 20).Value = resultList.Items(index).予算営業損益
        .Cells(50, 20).Value = resultList.Items(index).予測営業損益
        .Cells(51, 20).Value = resultList.Items(index).実績営業損益
        
        '上期12月実績
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(54, 5).Value = resultList.Items(index).予算ES
        .Cells(55, 5).Value = resultList.Items(index).予測ES
        .Cells(56, 5).Value = resultList.Items(index).実績ES
        .Cells(54, 6).Value = resultList.Items(index).予算まとめ
        .Cells(55, 6).Value = resultList.Items(index).予測まとめ
        .Cells(56, 6).Value = resultList.Items(index).実績まとめ
        .Cells(54, 7).Value = resultList.Items(index).予算売上
        .Cells(55, 7).Value = resultList.Items(index).予測売上
        .Cells(56, 7).Value = resultList.Items(index).実績売上
        .Cells(59, 5).Value = resultList.Items(index).予算営業損益
        .Cells(60, 5).Value = resultList.Items(index).予測営業損益
        .Cells(61, 5).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(54, 8).Value = resultList.Items(index).予算ES
        .Cells(55, 8).Value = resultList.Items(index).予測ES
        .Cells(56, 8).Value = resultList.Items(index).実績ES
        .Cells(54, 9).Value = resultList.Items(index).予算まとめ
        .Cells(55, 9).Value = resultList.Items(index).予測まとめ
        .Cells(56, 9).Value = resultList.Items(index).実績まとめ
        .Cells(54, 10).Value = resultList.Items(index).予算売上
        .Cells(55, 10).Value = resultList.Items(index).予測売上
        .Cells(56, 10).Value = resultList.Items(index).実績売上
        .Cells(59, 8).Value = resultList.Items(index).予算営業損益
        .Cells(60, 8).Value = resultList.Items(index).予測営業損益
        .Cells(61, 8).Value = resultList.Items(index).実績営業損益

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(54, 11).Value = resultList.Items(index).予算ES
        .Cells(55, 11).Value = resultList.Items(index).予測ES
        .Cells(56, 11).Value = resultList.Items(index).実績ES
        .Cells(54, 12).Value = resultList.Items(index).予算まとめ
        .Cells(55, 12).Value = resultList.Items(index).予測まとめ
        .Cells(56, 12).Value = resultList.Items(index).実績まとめ
        .Cells(54, 13).Value = resultList.Items(index).予算売上
        .Cells(55, 13).Value = resultList.Items(index).予測売上
        .Cells(56, 13).Value = resultList.Items(index).実績売上
        .Cells(59, 11).Value = resultList.Items(index).予算営業損益
        .Cells(60, 11).Value = resultList.Items(index).予測営業損益
        .Cells(61, 11).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "75")
        .Cells(54, 14).Value = resultList.Items(index).予算ES
        .Cells(55, 14).Value = resultList.Items(index).予測ES
        .Cells(56, 14).Value = resultList.Items(index).実績ES
        .Cells(54, 15).Value = resultList.Items(index).予算まとめ
        .Cells(55, 15).Value = resultList.Items(index).予測まとめ
        .Cells(56, 15).Value = resultList.Items(index).実績まとめ
        .Cells(54, 16).Value = resultList.Items(index).予算売上
        .Cells(55, 16).Value = resultList.Items(index).予測売上
        .Cells(56, 16).Value = resultList.Items(index).実績売上
        .Cells(59, 14).Value = resultList.Items(index).予算営業損益
        .Cells(60, 14).Value = resultList.Items(index).予測営業損益
        .Cells(61, 14).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "76")
        .Cells(54, 17).Value = resultList.Items(index).予算ES
        .Cells(55, 17).Value = resultList.Items(index).予測ES
        .Cells(56, 17).Value = resultList.Items(index).実績ES
        .Cells(54, 18).Value = resultList.Items(index).予算まとめ
        .Cells(55, 18).Value = resultList.Items(index).予測まとめ
        .Cells(56, 18).Value = resultList.Items(index).実績まとめ
        .Cells(54, 19).Value = resultList.Items(index).予算売上
        .Cells(55, 19).Value = resultList.Items(index).予測売上
        .Cells(56, 19).Value = resultList.Items(index).実績売上
        .Cells(59, 17).Value = resultList.Items(index).予算営業損益
        .Cells(60, 17).Value = resultList.Items(index).予測営業損益
        .Cells(61, 17).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "80")
        .Cells(54, 20).Value = resultList.Items(index).予算ES
        .Cells(55, 20).Value = resultList.Items(index).予測ES
        .Cells(56, 20).Value = resultList.Items(index).実績ES
        .Cells(54, 21).Value = resultList.Items(index).予算まとめ
        .Cells(55, 21).Value = resultList.Items(index).予測まとめ
        .Cells(56, 21).Value = resultList.Items(index).実績まとめ
        .Cells(54, 22).Value = resultList.Items(index).予算売上
        .Cells(55, 22).Value = resultList.Items(index).予測売上
        .Cells(56, 22).Value = resultList.Items(index).実績売上
        .Cells(59, 20).Value = resultList.Items(index).予算営業損益
        .Cells(60, 20).Value = resultList.Items(index).予測営業損益
        .Cells(61, 20).Value = resultList.Items(index).実績営業損益
        
        '上期1月実績
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(64, 5).Value = resultList.Items(index).予算ES
        .Cells(65, 5).Value = resultList.Items(index).予測ES
        .Cells(66, 5).Value = resultList.Items(index).実績ES
        .Cells(64, 6).Value = resultList.Items(index).予算まとめ
        .Cells(65, 6).Value = resultList.Items(index).予測まとめ
        .Cells(66, 6).Value = resultList.Items(index).実績まとめ
        .Cells(64, 7).Value = resultList.Items(index).予算売上
        .Cells(65, 7).Value = resultList.Items(index).予測売上
        .Cells(66, 7).Value = resultList.Items(index).実績売上
        .Cells(69, 5).Value = resultList.Items(index).予算営業損益
        .Cells(70, 5).Value = resultList.Items(index).予測営業損益
        .Cells(71, 5).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(64, 8).Value = resultList.Items(index).予算ES
        .Cells(65, 8).Value = resultList.Items(index).予測ES
        .Cells(66, 8).Value = resultList.Items(index).実績ES
        .Cells(64, 9).Value = resultList.Items(index).予算まとめ
        .Cells(65, 9).Value = resultList.Items(index).予測まとめ
        .Cells(66, 9).Value = resultList.Items(index).実績まとめ
        .Cells(64, 10).Value = resultList.Items(index).予算売上
        .Cells(65, 10).Value = resultList.Items(index).予測売上
        .Cells(66, 10).Value = resultList.Items(index).実績売上
        .Cells(69, 8).Value = resultList.Items(index).予算営業損益
        .Cells(70, 8).Value = resultList.Items(index).予測営業損益
        .Cells(71, 8).Value = resultList.Items(index).実績営業損益

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(64, 11).Value = resultList.Items(index).予算ES
        .Cells(65, 11).Value = resultList.Items(index).予測ES
        .Cells(66, 11).Value = resultList.Items(index).実績ES
        .Cells(64, 12).Value = resultList.Items(index).予算まとめ
        .Cells(65, 12).Value = resultList.Items(index).予測まとめ
        .Cells(66, 12).Value = resultList.Items(index).実績まとめ
        .Cells(64, 13).Value = resultList.Items(index).予算売上
        .Cells(65, 13).Value = resultList.Items(index).予測売上
        .Cells(66, 13).Value = resultList.Items(index).実績売上
        .Cells(69, 11).Value = resultList.Items(index).予算営業損益
        .Cells(70, 11).Value = resultList.Items(index).予測営業損益
        .Cells(71, 11).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "75")
        .Cells(64, 14).Value = resultList.Items(index).予算ES
        .Cells(65, 14).Value = resultList.Items(index).予測ES
        .Cells(66, 14).Value = resultList.Items(index).実績ES
        .Cells(64, 15).Value = resultList.Items(index).予算まとめ
        .Cells(65, 15).Value = resultList.Items(index).予測まとめ
        .Cells(66, 15).Value = resultList.Items(index).実績まとめ
        .Cells(64, 16).Value = resultList.Items(index).予算売上
        .Cells(65, 16).Value = resultList.Items(index).予測売上
        .Cells(66, 16).Value = resultList.Items(index).実績売上
        .Cells(69, 14).Value = resultList.Items(index).予算営業損益
        .Cells(70, 14).Value = resultList.Items(index).予測営業損益
        .Cells(71, 14).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "76")
        .Cells(64, 17).Value = resultList.Items(index).予算ES
        .Cells(65, 17).Value = resultList.Items(index).予測ES
        .Cells(66, 17).Value = resultList.Items(index).実績ES
        .Cells(64, 18).Value = resultList.Items(index).予算まとめ
        .Cells(65, 18).Value = resultList.Items(index).予測まとめ
        .Cells(66, 18).Value = resultList.Items(index).実績まとめ
        .Cells(64, 19).Value = resultList.Items(index).予算売上
        .Cells(65, 19).Value = resultList.Items(index).予測売上
        .Cells(66, 19).Value = resultList.Items(index).実績売上
        .Cells(69, 17).Value = resultList.Items(index).予算営業損益
        .Cells(70, 17).Value = resultList.Items(index).予測営業損益
        .Cells(71, 17).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "80")
        .Cells(64, 20).Value = resultList.Items(index).予算ES
        .Cells(65, 20).Value = resultList.Items(index).予測ES
        .Cells(66, 20).Value = resultList.Items(index).実績ES
        .Cells(64, 21).Value = resultList.Items(index).予算まとめ
        .Cells(65, 21).Value = resultList.Items(index).予測まとめ
        .Cells(66, 21).Value = resultList.Items(index).実績まとめ
        .Cells(64, 22).Value = resultList.Items(index).予算売上
        .Cells(65, 22).Value = resultList.Items(index).予測売上
        .Cells(66, 22).Value = resultList.Items(index).実績売上
        .Cells(69, 20).Value = resultList.Items(index).予算営業損益
        .Cells(70, 20).Value = resultList.Items(index).予測営業損益
        .Cells(71, 20).Value = resultList.Items(index).実績営業損益
        
        '下期2月実績
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(90, 5).Value = resultList.Items(index).予算ES
        .Cells(91, 5).Value = resultList.Items(index).予測ES
        .Cells(92, 5).Value = resultList.Items(index).実績ES
        .Cells(90, 6).Value = resultList.Items(index).予算まとめ
        .Cells(91, 6).Value = resultList.Items(index).予測まとめ
        .Cells(92, 6).Value = resultList.Items(index).実績まとめ
        .Cells(90, 7).Value = resultList.Items(index).予算売上
        .Cells(91, 7).Value = resultList.Items(index).予測売上
        .Cells(92, 7).Value = resultList.Items(index).実績売上
        .Cells(95, 5).Value = resultList.Items(index).予算営業損益
        .Cells(96, 5).Value = resultList.Items(index).予測営業損益
        .Cells(97, 5).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(90, 8).Value = resultList.Items(index).予算ES
        .Cells(91, 8).Value = resultList.Items(index).予測ES
        .Cells(92, 8).Value = resultList.Items(index).実績ES
        .Cells(90, 9).Value = resultList.Items(index).予算まとめ
        .Cells(91, 9).Value = resultList.Items(index).予測まとめ
        .Cells(92, 9).Value = resultList.Items(index).実績まとめ
        .Cells(90, 10).Value = resultList.Items(index).予算売上
        .Cells(91, 10).Value = resultList.Items(index).予測売上
        .Cells(92, 10).Value = resultList.Items(index).実績売上
        .Cells(95, 8).Value = resultList.Items(index).予算営業損益
        .Cells(96, 8).Value = resultList.Items(index).予測営業損益
        .Cells(97, 8).Value = resultList.Items(index).実績営業損益

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(90, 11).Value = resultList.Items(index).予算ES
        .Cells(91, 11).Value = resultList.Items(index).予測ES
        .Cells(92, 11).Value = resultList.Items(index).実績ES
        .Cells(90, 12).Value = resultList.Items(index).予算まとめ
        .Cells(91, 12).Value = resultList.Items(index).予測まとめ
        .Cells(92, 12).Value = resultList.Items(index).実績まとめ
        .Cells(90, 13).Value = resultList.Items(index).予算売上
        .Cells(91, 13).Value = resultList.Items(index).予測売上
        .Cells(92, 13).Value = resultList.Items(index).実績売上
        .Cells(95, 11).Value = resultList.Items(index).予算営業損益
        .Cells(96, 11).Value = resultList.Items(index).予測営業損益
        .Cells(97, 11).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "75")
        .Cells(90, 14).Value = resultList.Items(index).予算ES
        .Cells(91, 14).Value = resultList.Items(index).予測ES
        .Cells(92, 14).Value = resultList.Items(index).実績ES
        .Cells(90, 15).Value = resultList.Items(index).予算まとめ
        .Cells(91, 15).Value = resultList.Items(index).予測まとめ
        .Cells(92, 15).Value = resultList.Items(index).実績まとめ
        .Cells(90, 16).Value = resultList.Items(index).予算売上
        .Cells(91, 16).Value = resultList.Items(index).予測売上
        .Cells(92, 16).Value = resultList.Items(index).実績売上
        .Cells(95, 14).Value = resultList.Items(index).予算営業損益
        .Cells(96, 14).Value = resultList.Items(index).予測営業損益
        .Cells(97, 14).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "76")
        .Cells(90, 17).Value = resultList.Items(index).予算ES
        .Cells(91, 17).Value = resultList.Items(index).予測ES
        .Cells(92, 17).Value = resultList.Items(index).実績ES
        .Cells(90, 18).Value = resultList.Items(index).予算まとめ
        .Cells(91, 18).Value = resultList.Items(index).予測まとめ
        .Cells(92, 18).Value = resultList.Items(index).実績まとめ
        .Cells(90, 19).Value = resultList.Items(index).予算売上
        .Cells(91, 19).Value = resultList.Items(index).予測売上
        .Cells(92, 19).Value = resultList.Items(index).実績売上
        .Cells(95, 17).Value = resultList.Items(index).予算営業損益
        .Cells(96, 17).Value = resultList.Items(index).予測営業損益
        .Cells(97, 17).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "80")
        .Cells(90, 20).Value = resultList.Items(index).予算ES
        .Cells(91, 20).Value = resultList.Items(index).予測ES
        .Cells(92, 20).Value = resultList.Items(index).実績ES
        .Cells(90, 21).Value = resultList.Items(index).予算まとめ
        .Cells(91, 21).Value = resultList.Items(index).予測まとめ
        .Cells(92, 21).Value = resultList.Items(index).実績まとめ
        .Cells(90, 22).Value = resultList.Items(index).予算売上
        .Cells(91, 22).Value = resultList.Items(index).予測売上
        .Cells(92, 22).Value = resultList.Items(index).実績売上
        .Cells(95, 20).Value = resultList.Items(index).予算営業損益
        .Cells(96, 20).Value = resultList.Items(index).予測営業損益
        .Cells(97, 20).Value = resultList.Items(index).実績営業損益
        
        '下期3月実績
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(100, 5).Value = resultList.Items(index).予算ES
        .Cells(101, 5).Value = resultList.Items(index).予測ES
        .Cells(102, 5).Value = resultList.Items(index).実績ES
        .Cells(100, 6).Value = resultList.Items(index).予算まとめ
        .Cells(101, 6).Value = resultList.Items(index).予測まとめ
        .Cells(102, 6).Value = resultList.Items(index).実績まとめ
        .Cells(100, 7).Value = resultList.Items(index).予算売上
        .Cells(101, 7).Value = resultList.Items(index).予測売上
        .Cells(102, 7).Value = resultList.Items(index).実績売上
        .Cells(105, 5).Value = resultList.Items(index).予算営業損益
        .Cells(106, 5).Value = resultList.Items(index).予測営業損益
        .Cells(107, 5).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(100, 8).Value = resultList.Items(index).予算ES
        .Cells(101, 8).Value = resultList.Items(index).予測ES
        .Cells(102, 8).Value = resultList.Items(index).実績ES
        .Cells(100, 9).Value = resultList.Items(index).予算まとめ
        .Cells(101, 9).Value = resultList.Items(index).予測まとめ
        .Cells(102, 9).Value = resultList.Items(index).実績まとめ
        .Cells(100, 10).Value = resultList.Items(index).予算売上
        .Cells(101, 10).Value = resultList.Items(index).予測売上
        .Cells(102, 10).Value = resultList.Items(index).実績売上
        .Cells(105, 8).Value = resultList.Items(index).予算営業損益
        .Cells(106, 8).Value = resultList.Items(index).予測営業損益
        .Cells(107, 8).Value = resultList.Items(index).実績営業損益

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(100, 11).Value = resultList.Items(index).予算ES
        .Cells(101, 11).Value = resultList.Items(index).予測ES
        .Cells(102, 11).Value = resultList.Items(index).実績ES
        .Cells(100, 12).Value = resultList.Items(index).予算まとめ
        .Cells(101, 12).Value = resultList.Items(index).予測まとめ
        .Cells(102, 12).Value = resultList.Items(index).実績まとめ
        .Cells(100, 13).Value = resultList.Items(index).予算売上
        .Cells(101, 13).Value = resultList.Items(index).予測売上
        .Cells(102, 13).Value = resultList.Items(index).実績売上
        .Cells(105, 11).Value = resultList.Items(index).予算営業損益
        .Cells(106, 11).Value = resultList.Items(index).予測営業損益
        .Cells(107, 11).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "75")
        .Cells(100, 14).Value = resultList.Items(index).予算ES
        .Cells(101, 14).Value = resultList.Items(index).予測ES
        .Cells(102, 14).Value = resultList.Items(index).実績ES
        .Cells(100, 15).Value = resultList.Items(index).予算まとめ
        .Cells(101, 15).Value = resultList.Items(index).予測まとめ
        .Cells(102, 15).Value = resultList.Items(index).実績まとめ
        .Cells(100, 16).Value = resultList.Items(index).予算売上
        .Cells(101, 16).Value = resultList.Items(index).予測売上
        .Cells(102, 16).Value = resultList.Items(index).実績売上
        .Cells(105, 14).Value = resultList.Items(index).予算営業損益
        .Cells(106, 14).Value = resultList.Items(index).予測営業損益
        .Cells(107, 14).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "76")
        .Cells(100, 17).Value = resultList.Items(index).予算ES
        .Cells(101, 17).Value = resultList.Items(index).予測ES
        .Cells(102, 17).Value = resultList.Items(index).実績ES
        .Cells(100, 18).Value = resultList.Items(index).予算まとめ
        .Cells(101, 18).Value = resultList.Items(index).予測まとめ
        .Cells(102, 18).Value = resultList.Items(index).実績まとめ
        .Cells(100, 19).Value = resultList.Items(index).予算売上
        .Cells(101, 19).Value = resultList.Items(index).予測売上
        .Cells(102, 19).Value = resultList.Items(index).実績売上
        .Cells(105, 17).Value = resultList.Items(index).予算営業損益
        .Cells(106, 17).Value = resultList.Items(index).予測営業損益
        .Cells(107, 17).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "80")
        .Cells(100, 20).Value = resultList.Items(index).予算ES
        .Cells(101, 20).Value = resultList.Items(index).予測ES
        .Cells(102, 20).Value = resultList.Items(index).実績ES
        .Cells(100, 21).Value = resultList.Items(index).予算まとめ
        .Cells(101, 21).Value = resultList.Items(index).予測まとめ
        .Cells(102, 21).Value = resultList.Items(index).実績まとめ
        .Cells(100, 22).Value = resultList.Items(index).予算売上
        .Cells(101, 22).Value = resultList.Items(index).予測売上
        .Cells(102, 22).Value = resultList.Items(index).実績売上
        .Cells(105, 20).Value = resultList.Items(index).予算営業損益
        .Cells(106, 20).Value = resultList.Items(index).予測営業損益
        .Cells(107, 20).Value = resultList.Items(index).実績営業損益
        
        '下期4月実績
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(110, 5).Value = resultList.Items(index).予算ES
        .Cells(111, 5).Value = resultList.Items(index).予測ES
        .Cells(112, 5).Value = resultList.Items(index).実績ES
        .Cells(110, 6).Value = resultList.Items(index).予算まとめ
        .Cells(111, 6).Value = resultList.Items(index).予測まとめ
        .Cells(112, 6).Value = resultList.Items(index).実績まとめ
        .Cells(110, 7).Value = resultList.Items(index).予算売上
        .Cells(111, 7).Value = resultList.Items(index).予測売上
        .Cells(112, 7).Value = resultList.Items(index).実績売上
        .Cells(115, 5).Value = resultList.Items(index).予算営業損益
        .Cells(116, 5).Value = resultList.Items(index).予測営業損益
        .Cells(117, 5).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(110, 8).Value = resultList.Items(index).予算ES
        .Cells(111, 8).Value = resultList.Items(index).予測ES
        .Cells(112, 8).Value = resultList.Items(index).実績ES
        .Cells(110, 9).Value = resultList.Items(index).予算まとめ
        .Cells(111, 9).Value = resultList.Items(index).予測まとめ
        .Cells(112, 9).Value = resultList.Items(index).実績まとめ
        .Cells(110, 10).Value = resultList.Items(index).予算売上
        .Cells(111, 10).Value = resultList.Items(index).予測売上
        .Cells(112, 10).Value = resultList.Items(index).実績売上
        .Cells(115, 8).Value = resultList.Items(index).予算営業損益
        .Cells(116, 8).Value = resultList.Items(index).予測営業損益
        .Cells(117, 8).Value = resultList.Items(index).実績営業損益

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(110, 11).Value = resultList.Items(index).予算ES
        .Cells(111, 11).Value = resultList.Items(index).予測ES
        .Cells(112, 11).Value = resultList.Items(index).実績ES
        .Cells(110, 12).Value = resultList.Items(index).予算まとめ
        .Cells(111, 12).Value = resultList.Items(index).予測まとめ
        .Cells(112, 12).Value = resultList.Items(index).実績まとめ
        .Cells(110, 13).Value = resultList.Items(index).予算売上
        .Cells(111, 13).Value = resultList.Items(index).予測売上
        .Cells(112, 13).Value = resultList.Items(index).実績売上
        .Cells(115, 11).Value = resultList.Items(index).予算営業損益
        .Cells(116, 11).Value = resultList.Items(index).予測営業損益
        .Cells(117, 11).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "75")
        .Cells(110, 14).Value = resultList.Items(index).予算ES
        .Cells(111, 14).Value = resultList.Items(index).予測ES
        .Cells(112, 14).Value = resultList.Items(index).実績ES
        .Cells(110, 15).Value = resultList.Items(index).予算まとめ
        .Cells(111, 15).Value = resultList.Items(index).予測まとめ
        .Cells(112, 15).Value = resultList.Items(index).実績まとめ
        .Cells(110, 16).Value = resultList.Items(index).予算売上
        .Cells(111, 16).Value = resultList.Items(index).予測売上
        .Cells(112, 16).Value = resultList.Items(index).実績売上
        .Cells(115, 14).Value = resultList.Items(index).予算営業損益
        .Cells(116, 14).Value = resultList.Items(index).予測営業損益
        .Cells(117, 14).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "76")
        .Cells(110, 17).Value = resultList.Items(index).予算ES
        .Cells(111, 17).Value = resultList.Items(index).予測ES
        .Cells(112, 17).Value = resultList.Items(index).実績ES
        .Cells(110, 18).Value = resultList.Items(index).予算まとめ
        .Cells(111, 18).Value = resultList.Items(index).予測まとめ
        .Cells(112, 18).Value = resultList.Items(index).実績まとめ
        .Cells(110, 19).Value = resultList.Items(index).予算売上
        .Cells(111, 19).Value = resultList.Items(index).予測売上
        .Cells(112, 19).Value = resultList.Items(index).実績売上
        .Cells(115, 17).Value = resultList.Items(index).予算営業損益
        .Cells(116, 17).Value = resultList.Items(index).予測営業損益
        .Cells(117, 17).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "80")
        .Cells(110, 20).Value = resultList.Items(index).予算ES
        .Cells(111, 20).Value = resultList.Items(index).予測ES
        .Cells(112, 20).Value = resultList.Items(index).実績ES
        .Cells(110, 21).Value = resultList.Items(index).予算まとめ
        .Cells(111, 21).Value = resultList.Items(index).予測まとめ
        .Cells(112, 21).Value = resultList.Items(index).実績まとめ
        .Cells(110, 22).Value = resultList.Items(index).予算売上
        .Cells(111, 22).Value = resultList.Items(index).予測売上
        .Cells(112, 22).Value = resultList.Items(index).実績売上
        .Cells(115, 20).Value = resultList.Items(index).予算営業損益
        .Cells(116, 20).Value = resultList.Items(index).予測営業損益
        .Cells(117, 20).Value = resultList.Items(index).実績営業損益
        
        '下期5月実績
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(128, 5).Value = resultList.Items(index).予算ES
        .Cells(129, 5).Value = resultList.Items(index).予測ES
        .Cells(130, 5).Value = resultList.Items(index).実績ES
        .Cells(128, 6).Value = resultList.Items(index).予算まとめ
        .Cells(129, 6).Value = resultList.Items(index).予測まとめ
        .Cells(130, 6).Value = resultList.Items(index).実績まとめ
        .Cells(128, 7).Value = resultList.Items(index).予算売上
        .Cells(129, 7).Value = resultList.Items(index).予測売上
        .Cells(130, 7).Value = resultList.Items(index).実績売上
        .Cells(133, 5).Value = resultList.Items(index).予算営業損益
        .Cells(134, 5).Value = resultList.Items(index).予測営業損益
        .Cells(135, 5).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(128, 8).Value = resultList.Items(index).予算ES
        .Cells(129, 8).Value = resultList.Items(index).予測ES
        .Cells(130, 8).Value = resultList.Items(index).実績ES
        .Cells(128, 9).Value = resultList.Items(index).予算まとめ
        .Cells(129, 9).Value = resultList.Items(index).予測まとめ
        .Cells(130, 9).Value = resultList.Items(index).実績まとめ
        .Cells(128, 10).Value = resultList.Items(index).予算売上
        .Cells(129, 10).Value = resultList.Items(index).予測売上
        .Cells(130, 10).Value = resultList.Items(index).実績売上
        .Cells(133, 8).Value = resultList.Items(index).予算営業損益
        .Cells(134, 8).Value = resultList.Items(index).予測営業損益
        .Cells(135, 8).Value = resultList.Items(index).実績営業損益

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(128, 11).Value = resultList.Items(index).予算ES
        .Cells(129, 11).Value = resultList.Items(index).予測ES
        .Cells(130, 11).Value = resultList.Items(index).実績ES
        .Cells(128, 12).Value = resultList.Items(index).予算まとめ
        .Cells(129, 12).Value = resultList.Items(index).予測まとめ
        .Cells(130, 12).Value = resultList.Items(index).実績まとめ
        .Cells(128, 13).Value = resultList.Items(index).予算売上
        .Cells(129, 13).Value = resultList.Items(index).予測売上
        .Cells(130, 13).Value = resultList.Items(index).実績売上
        .Cells(133, 11).Value = resultList.Items(index).予算営業損益
        .Cells(134, 11).Value = resultList.Items(index).予測営業損益
        .Cells(135, 11).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "75")
        .Cells(128, 14).Value = resultList.Items(index).予算ES
        .Cells(129, 14).Value = resultList.Items(index).予測ES
        .Cells(130, 14).Value = resultList.Items(index).実績ES
        .Cells(128, 15).Value = resultList.Items(index).予算まとめ
        .Cells(129, 15).Value = resultList.Items(index).予測まとめ
        .Cells(130, 15).Value = resultList.Items(index).実績まとめ
        .Cells(128, 16).Value = resultList.Items(index).予算売上
        .Cells(129, 16).Value = resultList.Items(index).予測売上
        .Cells(130, 16).Value = resultList.Items(index).実績売上
        .Cells(133, 14).Value = resultList.Items(index).予算営業損益
        .Cells(134, 14).Value = resultList.Items(index).予測営業損益
        .Cells(135, 14).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "76")
        .Cells(128, 17).Value = resultList.Items(index).予算ES
        .Cells(129, 17).Value = resultList.Items(index).予測ES
        .Cells(130, 17).Value = resultList.Items(index).実績ES
        .Cells(128, 18).Value = resultList.Items(index).予算まとめ
        .Cells(129, 18).Value = resultList.Items(index).予測まとめ
        .Cells(130, 18).Value = resultList.Items(index).実績まとめ
        .Cells(128, 19).Value = resultList.Items(index).予算売上
        .Cells(129, 19).Value = resultList.Items(index).予測売上
        .Cells(130, 19).Value = resultList.Items(index).実績売上
        .Cells(133, 17).Value = resultList.Items(index).予算営業損益
        .Cells(134, 17).Value = resultList.Items(index).予測営業損益
        .Cells(135, 17).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "80")
        .Cells(128, 20).Value = resultList.Items(index).予算ES
        .Cells(129, 20).Value = resultList.Items(index).予測ES
        .Cells(130, 20).Value = resultList.Items(index).実績ES
        .Cells(128, 21).Value = resultList.Items(index).予算まとめ
        .Cells(129, 21).Value = resultList.Items(index).予測まとめ
        .Cells(130, 21).Value = resultList.Items(index).実績まとめ
        .Cells(128, 22).Value = resultList.Items(index).予算売上
        .Cells(129, 22).Value = resultList.Items(index).予測売上
        .Cells(130, 22).Value = resultList.Items(index).実績売上
        .Cells(133, 20).Value = resultList.Items(index).予算営業損益
        .Cells(134, 20).Value = resultList.Items(index).予測営業損益
        .Cells(135, 20).Value = resultList.Items(index).実績営業損益
        
        '下期6月実績
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(138, 5).Value = resultList.Items(index).予算ES
        .Cells(139, 5).Value = resultList.Items(index).予測ES
        .Cells(140, 5).Value = resultList.Items(index).実績ES
        .Cells(138, 6).Value = resultList.Items(index).予算まとめ
        .Cells(139, 6).Value = resultList.Items(index).予測まとめ
        .Cells(140, 6).Value = resultList.Items(index).実績まとめ
        .Cells(138, 7).Value = resultList.Items(index).予算売上
        .Cells(139, 7).Value = resultList.Items(index).予測売上
        .Cells(140, 7).Value = resultList.Items(index).実績売上
        .Cells(143, 5).Value = resultList.Items(index).予算営業損益
        .Cells(144, 5).Value = resultList.Items(index).予測営業損益
        .Cells(145, 5).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(138, 8).Value = resultList.Items(index).予算ES
        .Cells(139, 8).Value = resultList.Items(index).予測ES
        .Cells(140, 8).Value = resultList.Items(index).実績ES
        .Cells(138, 9).Value = resultList.Items(index).予算まとめ
        .Cells(139, 9).Value = resultList.Items(index).予測まとめ
        .Cells(140, 9).Value = resultList.Items(index).実績まとめ
        .Cells(138, 10).Value = resultList.Items(index).予算売上
        .Cells(139, 10).Value = resultList.Items(index).予測売上
        .Cells(140, 10).Value = resultList.Items(index).実績売上
        .Cells(143, 8).Value = resultList.Items(index).予算営業損益
        .Cells(144, 8).Value = resultList.Items(index).予測営業損益
        .Cells(145, 8).Value = resultList.Items(index).実績営業損益

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(138, 11).Value = resultList.Items(index).予算ES
        .Cells(139, 11).Value = resultList.Items(index).予測ES
        .Cells(140, 11).Value = resultList.Items(index).実績ES
        .Cells(138, 12).Value = resultList.Items(index).予算まとめ
        .Cells(139, 12).Value = resultList.Items(index).予測まとめ
        .Cells(140, 12).Value = resultList.Items(index).実績まとめ
        .Cells(138, 13).Value = resultList.Items(index).予算売上
        .Cells(139, 13).Value = resultList.Items(index).予測売上
        .Cells(140, 13).Value = resultList.Items(index).実績売上
        .Cells(143, 11).Value = resultList.Items(index).予算営業損益
        .Cells(144, 11).Value = resultList.Items(index).予測営業損益
        .Cells(145, 11).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "75")
        .Cells(138, 14).Value = resultList.Items(index).予算ES
        .Cells(139, 14).Value = resultList.Items(index).予測ES
        .Cells(140, 14).Value = resultList.Items(index).実績ES
        .Cells(138, 15).Value = resultList.Items(index).予算まとめ
        .Cells(139, 15).Value = resultList.Items(index).予測まとめ
        .Cells(140, 15).Value = resultList.Items(index).実績まとめ
        .Cells(138, 16).Value = resultList.Items(index).予算売上
        .Cells(139, 16).Value = resultList.Items(index).予測売上
        .Cells(140, 16).Value = resultList.Items(index).実績売上
        .Cells(143, 14).Value = resultList.Items(index).予算営業損益
        .Cells(144, 14).Value = resultList.Items(index).予測営業損益
        .Cells(145, 14).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "76")
        .Cells(138, 17).Value = resultList.Items(index).予算ES
        .Cells(139, 17).Value = resultList.Items(index).予測ES
        .Cells(140, 17).Value = resultList.Items(index).実績ES
        .Cells(138, 18).Value = resultList.Items(index).予算まとめ
        .Cells(139, 18).Value = resultList.Items(index).予測まとめ
        .Cells(140, 18).Value = resultList.Items(index).実績まとめ
        .Cells(138, 19).Value = resultList.Items(index).予算売上
        .Cells(139, 19).Value = resultList.Items(index).予測売上
        .Cells(140, 19).Value = resultList.Items(index).実績売上
        .Cells(143, 17).Value = resultList.Items(index).予算営業損益
        .Cells(144, 17).Value = resultList.Items(index).予測営業損益
        .Cells(145, 17).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "80")
        .Cells(138, 20).Value = resultList.Items(index).予算ES
        .Cells(139, 20).Value = resultList.Items(index).予測ES
        .Cells(140, 20).Value = resultList.Items(index).実績ES
        .Cells(138, 21).Value = resultList.Items(index).予算まとめ
        .Cells(139, 21).Value = resultList.Items(index).予測まとめ
        .Cells(140, 21).Value = resultList.Items(index).実績まとめ
        .Cells(138, 22).Value = resultList.Items(index).予算売上
        .Cells(139, 22).Value = resultList.Items(index).予測売上
        .Cells(140, 22).Value = resultList.Items(index).実績売上
        .Cells(143, 20).Value = resultList.Items(index).予算営業損益
        .Cells(144, 20).Value = resultList.Items(index).予測営業損益
        .Cells(145, 20).Value = resultList.Items(index).実績営業損益
        
        '下期7月実績
        ymdDate = DateAdd("m", 1, ymdDate)
        ymdNum = Val(Format(ymdDate, "yyyymmdd"))
        
        index = resultList.GetIndex(ymdNum, "50")
        .Cells(148, 5).Value = resultList.Items(index).予算ES
        .Cells(149, 5).Value = resultList.Items(index).予測ES
        .Cells(150, 5).Value = resultList.Items(index).実績ES
        .Cells(148, 6).Value = resultList.Items(index).予算まとめ
        .Cells(149, 6).Value = resultList.Items(index).予測まとめ
        .Cells(150, 5).Value = resultList.Items(index).実績まとめ
        .Cells(148, 7).Value = resultList.Items(index).予算売上
        .Cells(149, 7).Value = resultList.Items(index).予測売上
        .Cells(150, 7).Value = resultList.Items(index).実績売上
        .Cells(153, 5).Value = resultList.Items(index).予算営業損益
        .Cells(154, 5).Value = resultList.Items(index).予測営業損益
        .Cells(155, 5).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "70")
        .Cells(148, 8).Value = resultList.Items(index).予算ES
        .Cells(149, 8).Value = resultList.Items(index).予測ES
        .Cells(150, 8).Value = resultList.Items(index).実績ES
        .Cells(148, 9).Value = resultList.Items(index).予算まとめ
        .Cells(149, 9).Value = resultList.Items(index).予測まとめ
        .Cells(150, 9).Value = resultList.Items(index).実績まとめ
        .Cells(148, 10).Value = resultList.Items(index).予算売上
        .Cells(149, 10).Value = resultList.Items(index).予測売上
        .Cells(150, 10).Value = resultList.Items(index).実績売上
        .Cells(153, 8).Value = resultList.Items(index).予算営業損益
        .Cells(154, 8).Value = resultList.Items(index).予測営業損益
        .Cells(155, 8).Value = resultList.Items(index).実績営業損益

        index = resultList.GetIndex(ymdNum, "60")
        .Cells(148, 11).Value = resultList.Items(index).予算ES
        .Cells(149, 11).Value = resultList.Items(index).予測ES
        .Cells(150, 11).Value = resultList.Items(index).実績ES
        .Cells(148, 12).Value = resultList.Items(index).予算まとめ
        .Cells(149, 12).Value = resultList.Items(index).予測まとめ
        .Cells(150, 12).Value = resultList.Items(index).実績まとめ
        .Cells(148, 13).Value = resultList.Items(index).予算売上
        .Cells(149, 13).Value = resultList.Items(index).予測売上
        .Cells(150, 13).Value = resultList.Items(index).実績売上
        .Cells(153, 11).Value = resultList.Items(index).予算営業損益
        .Cells(154, 11).Value = resultList.Items(index).予測営業損益
        .Cells(155, 11).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "75")
        .Cells(148, 14).Value = resultList.Items(index).予算ES
        .Cells(149, 14).Value = resultList.Items(index).予測ES
        .Cells(150, 14).Value = resultList.Items(index).実績ES
        .Cells(148, 15).Value = resultList.Items(index).予算まとめ
        .Cells(149, 15).Value = resultList.Items(index).予測まとめ
        .Cells(150, 15).Value = resultList.Items(index).実績まとめ
        .Cells(148, 16).Value = resultList.Items(index).予算売上
        .Cells(149, 16).Value = resultList.Items(index).予測売上
        .Cells(150, 16).Value = resultList.Items(index).実績売上
        .Cells(153, 14).Value = resultList.Items(index).予算営業損益
        .Cells(154, 14).Value = resultList.Items(index).予測営業損益
        .Cells(155, 14).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "76")
        .Cells(148, 17).Value = resultList.Items(index).予算ES
        .Cells(149, 17).Value = resultList.Items(index).予測ES
        .Cells(150, 17).Value = resultList.Items(index).実績ES
        .Cells(148, 18).Value = resultList.Items(index).予算まとめ
        .Cells(149, 18).Value = resultList.Items(index).予測まとめ
        .Cells(150, 18).Value = resultList.Items(index).実績まとめ
        .Cells(148, 19).Value = resultList.Items(index).予算売上
        .Cells(149, 19).Value = resultList.Items(index).予測売上
        .Cells(150, 19).Value = resultList.Items(index).実績売上
        .Cells(153, 17).Value = resultList.Items(index).予算営業損益
        .Cells(154, 17).Value = resultList.Items(index).予測営業損益
        .Cells(155, 17).Value = resultList.Items(index).実績営業損益
        
        index = resultList.GetIndex(ymdNum, "80")
        .Cells(148, 20).Value = resultList.Items(index).予算ES
        .Cells(149, 20).Value = resultList.Items(index).予測ES
        .Cells(150, 20).Value = resultList.Items(index).実績ES
        .Cells(148, 21).Value = resultList.Items(index).予算まとめ
        .Cells(149, 21).Value = resultList.Items(index).予測まとめ
        .Cells(150, 21).Value = resultList.Items(index).実績まとめ
        .Cells(148, 22).Value = resultList.Items(index).予算売上
        .Cells(149, 22).Value = resultList.Items(index).予測売上
        .Cells(150, 22).Value = resultList.Items(index).実績売上
        .Cells(153, 20).Value = resultList.Items(index).予算営業損益
        .Cells(154, 20).Value = resultList.Items(index).予測営業損益
        .Cells(155, 20).Value = resultList.Items(index).実績営業損益
        
        '更新日の更新
        .Range("Y2").Value = "更新日 " & Now
    
    End With
End Sub

' ----------------------------------------------------------
'「予測連絡用」シートに予算､予測値を設定
' ----------------------------------------------------------
Private Sub 予測連絡_予算予測値設定(resultList As SaleResultList)
    Dim thisYMD As Long
    Dim nextYMD As Long
    Dim nextMonth As Date
    Dim index As Integer

    nextMonth = DateAdd("M", 1, gToday)
    thisYMD = Year(gToday) * 10000 + Month(gToday) * 100 + 1
    nextYMD = Year(nextMonth) * 10000 + Month(nextMonth) * 100 + 1
    
    With Worksheets(sheet予測連絡用)
        '今月-予算、予測
        index = resultList.GetIndex(thisYMD, "50")
        .Cells(6, 5).Value = resultList.Items(index).予算ES
        .Cells(7, 5).Value = resultList.Items(index).予測ES
        .Cells(6, 6).Value = resultList.Items(index).予算まとめ
        .Cells(7, 6).Value = resultList.Items(index).予測まとめ
        .Cells(6, 7).Value = resultList.Items(index).予算売上
        .Cells(7, 7).Value = resultList.Items(index).予測売上
        .Cells(11, 5).Value = resultList.Items(index).予算営業損益
        .Cells(12, 5).Value = resultList.Items(index).予測営業損益
        
        index = resultList.GetIndex(thisYMD, "70")
        .Cells(6, 8).Value = resultList.Items(index).予算ES
        .Cells(7, 8).Value = resultList.Items(index).予測ES
        .Cells(6, 9).Value = resultList.Items(index).予算まとめ
        .Cells(7, 9).Value = resultList.Items(index).予測まとめ
        .Cells(6, 10).Value = resultList.Items(index).予算売上
        .Cells(7, 10).Value = resultList.Items(index).予測売上
        .Cells(11, 8).Value = resultList.Items(index).予算営業損益
        .Cells(12, 8).Value = resultList.Items(index).予測営業損益

        index = resultList.GetIndex(thisYMD, "60")
        .Cells(6, 11).Value = resultList.Items(index).予算ES
        .Cells(7, 11).Value = resultList.Items(index).予測ES
        .Cells(6, 12).Value = resultList.Items(index).予算まとめ
        .Cells(7, 12).Value = resultList.Items(index).予測まとめ
        .Cells(6, 13).Value = resultList.Items(index).予算売上
        .Cells(7, 13).Value = resultList.Items(index).予測売上
        .Cells(11, 11).Value = resultList.Items(index).予算営業損益
        .Cells(12, 11).Value = resultList.Items(index).予測営業損益
        
        index = resultList.GetIndex(thisYMD, "75")
        .Cells(6, 14).Value = resultList.Items(index).予算ES
        .Cells(7, 14).Value = resultList.Items(index).予測ES
        .Cells(6, 15).Value = resultList.Items(index).予算まとめ
        .Cells(7, 15).Value = resultList.Items(index).予測まとめ
        .Cells(6, 16).Value = resultList.Items(index).予算売上
        .Cells(7, 16).Value = resultList.Items(index).予測売上
        .Cells(11, 14).Value = resultList.Items(index).予算営業損益
        .Cells(12, 14).Value = resultList.Items(index).予測営業損益
        
        index = resultList.GetIndex(thisYMD, "76")
        .Cells(6, 17).Value = resultList.Items(index).予算ES
        .Cells(7, 17).Value = resultList.Items(index).予測ES
        .Cells(6, 18).Value = resultList.Items(index).予算まとめ
        .Cells(7, 18).Value = resultList.Items(index).予測まとめ
        .Cells(6, 19).Value = resultList.Items(index).予算売上
        .Cells(7, 19).Value = resultList.Items(index).予測売上
        .Cells(11, 17).Value = resultList.Items(index).予算営業損益
        .Cells(12, 17).Value = resultList.Items(index).予測営業損益
        
        index = resultList.GetIndex(thisYMD, "80")
        .Cells(6, 20).Value = resultList.Items(index).予算ES
        .Cells(7, 20).Value = resultList.Items(index).予測ES
        .Cells(6, 21).Value = resultList.Items(index).予算まとめ
        .Cells(7, 21).Value = resultList.Items(index).予測まとめ
        .Cells(6, 22).Value = resultList.Items(index).予算売上
        .Cells(7, 22).Value = resultList.Items(index).予測売上
        .Cells(11, 20).Value = resultList.Items(index).予算営業損益
        .Cells(12, 20).Value = resultList.Items(index).予測営業損益

        '来月-予算、予測
        index = resultList.GetIndex(nextYMD, "50")
        If index <> -1 Then
            .Cells(15, 5).Value = resultList.Items(index).予算ES
            .Cells(15, 6).Value = resultList.Items(index).予算まとめ
            .Cells(15, 7).Value = resultList.Items(index).予算売上
            .Cells(19, 5).Value = resultList.Items(index).予算営業損益
            
            index = resultList.GetIndex(nextYMD, "70")
            .Cells(15, 8).Value = resultList.Items(index).予算ES
            .Cells(15, 9).Value = resultList.Items(index).予算まとめ
            .Cells(15, 10).Value = resultList.Items(index).予算売上
            .Cells(19, 8).Value = resultList.Items(index).予算営業損益
    
            index = resultList.GetIndex(nextYMD, "60")
            .Cells(15, 11).Value = resultList.Items(index).予算ES
            .Cells(15, 12).Value = resultList.Items(index).予算まとめ
            .Cells(15, 13).Value = resultList.Items(index).予算売上
            .Cells(19, 11).Value = resultList.Items(index).予算営業損益
            
            index = resultList.GetIndex(nextYMD, "75")
            .Cells(15, 14).Value = resultList.Items(index).予算ES
            .Cells(15, 15).Value = resultList.Items(index).予算まとめ
            .Cells(15, 16).Value = resultList.Items(index).予算売上
            .Cells(19, 14).Value = resultList.Items(index).予算営業損益
            
            index = resultList.GetIndex(nextYMD, "76")
            .Cells(15, 17).Value = resultList.Items(index).予算ES
            .Cells(15, 18).Value = resultList.Items(index).予算まとめ
            .Cells(15, 19).Value = resultList.Items(index).予算売上
            .Cells(19, 17).Value = resultList.Items(index).予算営業損益
            
            index = resultList.GetIndex(nextYMD, "80")
            .Cells(15, 20).Value = resultList.Items(index).予算ES
            .Cells(15, 21).Value = resultList.Items(index).予算まとめ
            .Cells(15, 22).Value = resultList.Items(index).予算売上
            .Cells(19, 20).Value = resultList.Items(index).予算営業損益
        End If
    End With
End Sub

' ----------------------------------------------------------
'「見込進捗_詳細」実績値格納
' ----------------------------------------------------------
' [引数]
' column:実績値列番号
' 勘定科目名:勘定科目名
' 部門コード:部門コード
' 金額:当月実績値
' ----------------------------------------------------------
Private Sub 実績値格納(column As Integer, 勘定科目名 As String, 部門コード As String, 金額 As Long)
    Dim searchRng, rng, tempRng As Range

    With Worksheets(sheet見込進捗詳細)
        Set searchRng = .Range("B:B")
        Set rng = searchRng.Find(勘定科目名, LookAt:=xlWhole)
        Set tempRng = rng
        Do While Not rng Is Nothing
            If .Cells(rng.Row, 3).Value = 部門コード Then
                .Cells(rng.Row, column).Value = RoundPrice(金額)
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
' 金額を千円単位で取得（小数点以下第１位で四捨五入）
' ----------------------------------------------------------
Private Function RoundPrice(price As Long) As Long
    If price > 0 Then
        Dim ret As Double
        ret = price / 1000
        
        'VBAのRound関数では0.5が1にならず、0となる
        RoundPrice = WorksheetFunction.Round(ret, 0)
        Exit Function
    End If
    RoundPrice = 0
End Function

' ----------------------------------------------------------
' シートの存在確認
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
' シートの削除
' ----------------------------------------------------------
Private Sub DeleteSheet(shName As String)
    If ExistSheet(shName) Then
        'メッセージ抑止
        Application.DisplayAlerts = False
        Worksheets(shName).Delete
        Application.DisplayAlerts = True
    End If
End Sub

' ----------------------------------------------------------
' 作業用シートの削除
' ----------------------------------------------------------
Private Sub DeleteWorkSheet()
    DeleteSheet (sheet当月売上予想)
    DeleteSheet (sheet翌月売上予想)
    DeleteSheet (sheetES売上予想)
    DeleteSheet (sheet予算予測実績値)
    DeleteSheet (sheet予測連絡用ES)
    DeleteSheet (sheet予測連絡用まとめ)
End Sub

' ----------------------------------------------------------
' 作業用シートの追加
' ----------------------------------------------------------
Private Sub AddWorkSheet()
    Worksheets.Add.Name = sheet当月売上予想
    Worksheets.Add.Name = sheet翌月売上予想
    Worksheets.Add.Name = sheetES売上予想
    Worksheets.Add.Name = sheet予算予測実績値
    Worksheets.Add.Name = sheet予測連絡用ES
    Worksheets.Add.Name = sheet予測連絡用まとめ
End Sub

