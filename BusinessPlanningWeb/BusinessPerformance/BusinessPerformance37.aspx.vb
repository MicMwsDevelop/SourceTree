
Partial Class BusinessPerformance_BusinessPerformance
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '        If Me.IsPostBack Then
        'If (Session("VISITORNAME") <> "") Then
        'L_LoginInfo.Text = Session("VISITORNAME") & "がログイン中"
        'Else
        'L_LoginInfo.Text = "ログイン情報なし。再ログインしてください。"
        'Response.Redirect("../Login.aspx")
        'End If

        'Exit Sub
        'End If


        'If (Session("VISITORNAME") <> "") Then
        'L_LoginInfo.Text = Session("VISITORNAME") & "がログイン中"
        'Else
        'L_LoginInfo.Text = "ログイン情報なし。再ログインしてください。"
        'Response.Redirect("../Login.aspx")
        'End If

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim sqlstr As String

        If (D_kikan.Text = "37期通期") Then
            T_開始.Text = "2011/08/01"
            T_終了.Text = "2012/07/31"

        ElseIf (D_kikan.Text = "37期上半期") Then
            T_開始.Text = "2011/08/01"
            T_終了.Text = "2012/01/31"

        ElseIf (D_kikan.Text = "37期下半期") Then
            T_開始.Text = "2012/02/01"
            T_終了.Text = "2012/07/31"

        ElseIf (D_kikan.Text = "38期通期") Then
            T_開始.Text = "2012/08/01"
            T_終了.Text = "2013/07/31"

        ElseIf (D_kikan.Text = "38期上半期") Then
            T_開始.Text = "2012/08/01"
            T_終了.Text = "2013/01/31"

        ElseIf (D_kikan.Text = "38期下半期") Then
            T_開始.Text = "2013/02/01"
            T_終了.Text = "2013/07/31"

        ElseIf (D_kikan.Text = "39期通期") Then
            T_開始.Text = "2013/08/01"
            T_終了.Text = "2014/07/31"

        ElseIf (D_kikan.Text = "39期上半期") Then
            T_開始.Text = "2013/08/01"
            T_終了.Text = "2014/01/31"

        ElseIf (D_kikan.Text = "39期下半期") Then
            T_開始.Text = "2014/02/01"
            T_終了.Text = "2014/07/31"

        ElseIf (D_kikan.Text = "40期通期") Then
            T_開始.Text = "2014/08/01"
            T_終了.Text = "2015/07/31"

        ElseIf (D_kikan.Text = "40期上半期") Then
            T_開始.Text = "2014/08/01"
            T_終了.Text = "2015/01/31"

        ElseIf (D_kikan.Text = "40期下半期") Then
            T_開始.Text = "2015/02/01"
            T_終了.Text = "2015/07/31"

        ElseIf (D_kikan.Text = "41期通期") Then
            T_開始.Text = "2015/08/01"
            T_終了.Text = "2016/07/31"

        ElseIf (D_kikan.Text = "41期上半期") Then
            T_開始.Text = "2015/08/01"
            T_終了.Text = "2016/01/31"

        ElseIf (D_kikan.Text = "41期下半期") Then
            T_開始.Text = "2016/02/01"
            T_終了.Text = "2016/07/31"

            '        ElseIf (D_kikan.Text = "35期第１四半期") Then
            '            T_開始.Text = "2009/08/01"
            '            T_終了.Text = "2009/10/31"

            '        ElseIf (D_kikan.Text = "35期第２四半期") Then
            '            T_開始.Text = "2009/11/01"
            '            T_終了.Text = "2010/01/31"

            '        ElseIf (D_kikan.Text = "35期第３四半期") Then
            '            T_開始.Text = "2010/02/01"
            '            T_終了.Text = "2010/04/30"

            '        ElseIf (D_kikan.Text = "35期第４四半期") Then
            '            T_開始.Text = "2010/05/01"
            '            T_終了.Text = "2010/07/31"



            '        ElseIf (D_kikan.Text = "36期通期") Then
            '            T_開始.Text = "2010/08/01"
            '            T_終了.Text = "2011/07/31"

            '        ElseIf (D_kikan.Text = "36期上半期") Then
            '            T_開始.Text = "2010/08/01"
            '            T_終了.Text = "2011/01/31"

            '        ElseIf (D_kikan.Text = "36期下半期") Then
            '            T_開始.Text = "2011/02/01"
            '            T_終了.Text = "2011/07/31"
        ElseIf (D_kikan.Text = "42期通期") Then
            T_開始.Text = "2016/08/01"
            T_終了.Text = "2017/07/31"
        ElseIf (D_kikan.Text = "43期通期") Then
            T_開始.Text = "2017/08/01"
            T_終了.Text = "2018/07/31"
        ElseIf (D_kikan.Text = "44期通期") Then
            T_開始.Text = "2018/08/01"
            T_終了.Text = "2019/07/31"
        ElseIf (D_kikan.Text = "45期通期") Then
            T_開始.Text = "2019/08/01"
            T_終了.Text = "2020/07/31"
        End If

        '抽出文字列作成

        '【営業実績順位ＳＱＬ】
        sqlstr = "SELECT t.現在支店名,t.担当者名, SUM(t.実績金額) AS 実績金額 FROM("
        sqlstr = sqlstr & " SELECT JunpDB.dbo.vMic個人営業実績.現在支店名 AS 現在支店名, JunpDB.dbo.vMic個人営業実績.担当者名 AS 担当者名, JunpDB.dbo.vMic個人営業実績.受注金額3 AS 実績金額,"
        sqlstr = sqlstr & " JunpDB.dbo.vMic担当者.fUsrYaku AS fUsrYaku, JunpDB.dbo.vMic個人営業実績.売上承認日"
        sqlstr = sqlstr & " FROM JunpDB.dbo.vMic個人営業実績 INNER JOIN JunpDB.dbo.vMic担当者 ON JunpDB.dbo.vMic個人営業実績.[担当者コード] = JunpDB.dbo.vMic担当者.fUsrID"
        sqlstr = sqlstr & " UNION ALL"
        sqlstr = sqlstr & " SELECT JunpDB.dbo.vMic個人MWS営業実績.現在支店名 AS 現在支店名,JunpDB.dbo.vMic個人MWS営業実績.担当者名 AS 担当者名, JunpDB.dbo.vMic個人MWS営業実績.月課金金額 AS 実績金額,"
        sqlstr = sqlstr & " JunpDB.dbo.vMic担当者.fUsrYaku AS fUsrYaku, JunpDB.dbo.vMic個人MWS営業実績.売上承認日"
        sqlstr = sqlstr & " FROM JunpDB.dbo.vMic個人MWS営業実績 INNER JOIN JunpDB.dbo.vMic担当者 ON JunpDB.dbo.vMic個人MWS営業実績.[担当者コード] = JunpDB.dbo.vMic担当者.fUsrID"
        sqlstr = sqlstr & " WHERE (((JunpDB.dbo.vMic個人MWS営業実績.ＷＷ伝票)='1'))"
        sqlstr = sqlstr & ") t"

        sqlstr = sqlstr & " WHERE (売上承認日 Between '" & T_開始.Text & "' And '" & T_終了.Text & "')"
        sqlstr = sqlstr & " GROUP BY 現在支店名, 担当者名, fUsrYaku"
        sqlstr = sqlstr & " HAVING (SUM(実績金額)"
        sqlstr = sqlstr & " > 0) AND (fUsrYaku = '31' OR"
        sqlstr = sqlstr & " fUsrYaku = '32' OR"
        sqlstr = sqlstr & " fUsrYaku = '33' OR"
        sqlstr = sqlstr & " fUsrYaku = '34' OR"
        sqlstr = sqlstr & " fUsrYaku = '35' OR"
        sqlstr = sqlstr & " fUsrYaku = '36' OR"
        sqlstr = sqlstr & " fUsrYaku = '41' OR"
        sqlstr = sqlstr & " fUsrYaku = '54' OR"
        sqlstr = sqlstr & " fUsrYaku = '61' OR"
        sqlstr = sqlstr & " fUsrYaku = '62' OR"
        sqlstr = sqlstr & " fUsrYaku = '63' OR"
        sqlstr = sqlstr & " fUsrYaku = '64' OR"
        sqlstr = sqlstr & " fUsrYaku = '73' OR"
        sqlstr = sqlstr & " fUsrYaku = '74' OR"
        sqlstr = sqlstr & " fUsrYaku = '75' OR"
        sqlstr = sqlstr & " fUsrYaku = '76')"
        sqlstr = sqlstr & " ORDER BY SUM(実績金額) DESC"





        'データソースセット
        '実環境
        '        SqlDataSource1.ConnectionString = "server='DBSV'; user id='ww_reader'; password=''; database='SalesDB'"

        '新WonderWeb環境
        SqlDataSource1.ConnectionString = "server='SQLSV'; user id='ww_reader'; password='20150801'; database='SalesDB'"


        SqlDataSource1.SelectCommand = sqlstr

        'ページは初期値
        GridView1.PageIndex = 0

    End Sub


    Protected Sub B_エクスポート_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles B_エクスポート.Click


        Dim tw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        Dim frm As HtmlForm = New HtmlForm()

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("content-disposition", "attachment;filename=BusinessPerformance.xls")
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
