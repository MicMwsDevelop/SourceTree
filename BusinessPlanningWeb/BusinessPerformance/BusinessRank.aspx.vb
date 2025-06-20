
Partial Class BusinessPerformance_BusinessPerformance
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '        If Me.IsPostBack Then
        '        If (Session("VISITORNAME") <> "") Then
        '        L_LoginInfo.Text = Session("VISITORNAME") & "がログイン中"
        '        Else
        '        L_LoginInfo.Text = "ログイン情報なし。再ログインしてください。"
        '        Response.Redirect("../Login.aspx")
        '        End If

        '       Exit Sub
        '       End If
        '

        '       If (Session("VISITORNAME") <> "") Then
        ' L_LoginInfo.Text = Session("VISITORNAME") & "がログイン中"
        ' Else
        'L_LoginInfo.Text = "ログイン情報なし。再ログインしてください。"
        'Response.Redirect("../Login.aspx")
        'End If

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim sqlstr As String

        If (D_kikan.Text = "43期通期") Then
            T_開始.Text = "2017/08/01"
            T_終了.Text = "2018/07/31"

        ElseIf (D_kikan.Text = "44期上半期") Then
            T_開始.Text = "2018/08/01"
            T_終了.Text = "2019/01/31"

        ElseIf (D_kikan.Text = "44期下半期") Then
            T_開始.Text = "2019/02/01"
            T_終了.Text = "2018/07/31"

        ElseIf (D_kikan.Text = "44期通期") Then
            T_開始.Text = "2018/08/01"
            T_終了.Text = "2019/07/31"

        ElseIf (D_kikan.Text = "45期上半期") Then
            T_開始.Text = "2019/08/01"
            T_終了.Text = "2020/01/31"

        ElseIf (D_kikan.Text = "45期下半期") Then
            T_開始.Text = "2019/02/01"
            T_終了.Text = "2020/07/31"


        End If

        '抽出文字列作成
        '        sqlstr = "SELECT dbo.JunpMih受注ヘッダ.f担当支店名,"
        '        sqlstr = sqlstr & " dbo.JunpMih受注ヘッダ.f担当者コード,"
        '        sqlstr = sqlstr & " dbo.JunpMih受注ヘッダ.f担当者名,"
        '        sqlstr = sqlstr & " SUM(CASE WHEN dbo.JunpMih受注ヘッダ.fリプレース区分 = 0 THEN dbo.JunpMih受注詳細.f数量 ELSE 0 END) AS リプレースなし,"
        '        sqlstr = sqlstr & " SUM(CASE WHEN dbo.JunpMih受注ヘッダ.fリプレース区分 = 1 THEN dbo.JunpMih受注詳細.f数量 ELSE 0 END) AS 自社R,"
        '        sqlstr = sqlstr & " SUM(CASE WHEN dbo.JunpMih受注ヘッダ.fリプレース区分 = 2 THEN dbo.JunpMih受注詳細.f数量 ELSE 0 END) AS 新規,"
        '        sqlstr = sqlstr & " SUM(CASE WHEN dbo.JunpMih受注ヘッダ.fリプレース区分 = 3 THEN dbo.JunpMih受注詳細.f数量 ELSE 0 END) AS 新開,"
        '        sqlstr = sqlstr & " SUM(CASE WHEN dbo.JunpMih受注ヘッダ.fリプレース区分 BETWEEN 4 AND 99 THEN dbo.JunpMih受注詳細.f数量 ELSE 0 END) AS 他社R,"
        '        sqlstr = sqlstr & " (SUM(CASE WHEN dbo.JunpMih受注ヘッダ.fリプレース区分 = 2 THEN dbo.JunpMih受注詳細.f数量 ELSE 0 END) "
        '        sqlstr = sqlstr & " + SUM(CASE WHEN dbo.JunpMih受注ヘッダ.fリプレース区分 = 3 THEN dbo.JunpMih受注詳細.f数量 ELSE 0 END)"
        '        sqlstr = sqlstr & " + SUM(CASE WHEN dbo.JunpMih受注ヘッダ.fリプレース区分 BETWEEN 4 AND 99 THEN dbo.JunpMih受注詳細.f数量 ELSE 0 END)) AS 総計"
        '        sqlstr = sqlstr & " FROM dbo.JunpMih受注ヘッダ INNER JOIN dbo.JunpMih受注詳細 ON dbo.JunpMih受注ヘッダ.f受注番号 = dbo.JunpMih受注詳細.f受注番号"
        '        sqlstr = sqlstr & " WHERE (dbo.JunpMih受注ヘッダ.f売上承認日 Between '" & T_開始.Text & "' And '" & T_終了.Text & "') AND (dbo.JunpMih受注詳細.f区分 = 1)"
        '        sqlstr = sqlstr & " GROUP BY dbo.JunpMih受注ヘッダ.f担当支店名, dbo.JunpMih受注ヘッダ.f担当者コード, dbo.JunpMih受注ヘッダ.f担当者名"
        '        sqlstr = sqlstr & " ORDER BY 総計 DESC , 他社R DESC"


        '抽出文字列作成
        sqlstr = "SELECT JunpDB.dbo.vMic販売実績情報.担当支店名,"
        sqlstr = sqlstr & " JunpDB.dbo.vMic販売実績情報.担当者コード,"
        sqlstr = sqlstr & " JunpDB.dbo.vMic販売実績情報.担当者名,"
        sqlstr = sqlstr & " SUM(CASE WHEN JunpDB.dbo.vMic販売実績情報.リプレース区分 = '新規' THEN JunpDB.dbo.vMic販売実績情報.数量 ELSE 0 END) AS 新規,"
        sqlstr = sqlstr & " SUM(CASE WHEN JunpDB.dbo.vMic販売実績情報.リプレース区分 = '新開' THEN JunpDB.dbo.vMic販売実績情報.数量 ELSE 0 END) AS 新開,"
        sqlstr = sqlstr & " SUM(CASE WHEN JunpDB.dbo.vMic販売実績情報.リプレース区分 = '他社R' THEN JunpDB.dbo.vMic販売実績情報.数量 ELSE 0 END) AS 他社R,"
        sqlstr = sqlstr & " (SUM(CASE WHEN JunpDB.dbo.vMic販売実績情報.リプレース区分 = '新規' THEN JunpDB.dbo.vMic販売実績情報.数量 ELSE 0 END) "
        sqlstr = sqlstr & " + SUM(CASE WHEN JunpDB.dbo.vMic販売実績情報.リプレース区分 = '新開' THEN JunpDB.dbo.vMic販売実績情報.数量 ELSE 0 END)"
        sqlstr = sqlstr & " + SUM(CASE WHEN JunpDB.dbo.vMic販売実績情報.リプレース区分 = '他社R' THEN JunpDB.dbo.vMic販売実績情報.数量 ELSE 0 END)) AS 総計,"
        sqlstr = sqlstr & " (Sum(CASE WHEN JunpDB.dbo.vMic販売実績情報.受注金額 Is Null THEN 0 ELSE JunpDB.dbo.vMic販売実績情報.受注金額 END)"
        sqlstr = sqlstr & " - Sum(CASE WHEN JunpDB.dbo.vMic販売実績情報.支払金額税抜 Is Null THEN 0 ELSE JunpDB.dbo.vMic販売実績情報.支払金額税抜 END)) AS 売上"

        sqlstr = sqlstr & " FROM JunpDB.dbo.vMic販売実績情報"
        sqlstr = sqlstr & " WHERE (((JunpDB.dbo.vMic販売実績情報.リプレース区分)='新開' Or (JunpDB.dbo.vMic販売実績情報.リプレース区分)='新規' Or (JunpDB.dbo.vMic販売実績情報.リプレース区分)='他社R') AND ((JunpDB.dbo.vMic販売実績情報.数量) Is Not Null) AND ((JunpDB.dbo.vMic販売実績情報.売上承認日) Between '" & T_開始.Text & "' And '" & T_終了.Text & "'))"
        sqlstr = sqlstr & " GROUP BY JunpDB.dbo.vMic販売実績情報.担当支店名, JunpDB.dbo.vMic販売実績情報.担当者コード, JunpDB.dbo.vMic販売実績情報.担当者名"
        sqlstr = sqlstr & " ORDER BY 総計 DESC , 他社R DESC ,売上 DESC"


        'データソースセット
        '実環境
        SqlDataSource1.ConnectionString = "server='sqlsv'; user id='ww_reader'; password='20150801'; database='SalesDB'"
        SqlDataSource1.SelectCommand = sqlstr

        'ページは初期値
        GridView1.PageIndex = 0

    End Sub

    
    Protected Sub B_エクスポート_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles B_エクスポート.Click


        Dim tw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        Dim frm As HtmlForm = New HtmlForm()

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("content-disposition", "attachment;filename=BusinessRank.xls")
        Response.Charset = ""
        Page.EnableViewState = False
        Controls.Add(frm)
        frm.Controls.Add(GridView1)
        frm.RenderControl(hw)
        Response.Write(tw.ToString())

        Response.End()

        'GridView1.AllowPaging = True
        'GridView1.AllowSorting = True
        'GridView1.DataBind()

    End Sub
End Class
