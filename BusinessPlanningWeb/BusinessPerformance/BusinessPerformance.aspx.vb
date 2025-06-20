
Partial Class BusinessPerformance_BusinessPerformance
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.IsPostBack Then
            If (Session("VISITORNAME") <> "") Then
                L_LoginInfo.Text = Session("VISITORNAME") & "がログイン中"
            Else
                L_LoginInfo.Text = "ログイン情報なし。再ログインしてください。"
                Response.Redirect("../Login.aspx")
            End If

            Exit Sub
        End If


        If (Session("VISITORNAME") <> "") Then
            L_LoginInfo.Text = Session("VISITORNAME") & "がログイン中"
        Else
            L_LoginInfo.Text = "ログイン情報なし。再ログインしてください。"
            Response.Redirect("../Login.aspx")
        End If

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim sqlstr As String

        If (D_kikan.Text = "35期通期") Then
            T_開始.Text = "2009/08/01"
            T_終了.Text = "2010/07/31"

        ElseIf (D_kikan.Text = "35期上半期") Then
            T_開始.Text = "2009/08/01"
            T_終了.Text = "2010/01/31"

        ElseIf (D_kikan.Text = "35期下半期") Then
            T_開始.Text = "2010/02/01"
            T_終了.Text = "2010/07/31"

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



        ElseIf (D_kikan.Text = "36期通期") Then
            T_開始.Text = "2010/08/01"
            T_終了.Text = "2011/07/31"

        ElseIf (D_kikan.Text = "36期上半期") Then
            T_開始.Text = "2010/08/01"
            T_終了.Text = "2011/01/31"

        ElseIf (D_kikan.Text = "36期下半期") Then
            T_開始.Text = "2011/02/01"
            T_終了.Text = "2011/07/31"




        End If

        '抽出文字列作成

        '【代理店/特約店】
        sqlstr = "SELECT JunpDB.dbo.vMic個人営業実績.現在支店名,"
        sqlstr = sqlstr & " JunpDB.dbo.vMic個人営業実績.担当者名,"
        sqlstr = sqlstr & " SUM(JunpDB.dbo.vMic個人営業実績.受注金額 - JunpDB.dbo.vMic個人営業実績.支払額)"
        sqlstr = sqlstr & " AS 実績金額"
        sqlstr = sqlstr & " FROM JunpDB.dbo.vMic個人営業実績 INNER JOIN"
        sqlstr = sqlstr & " JunpDB.dbo.vMic担当者 ON"
        sqlstr = sqlstr & " JunpDB.dbo.vMic個人営業実績.[担当者コード] = JunpDB.dbo.vMic担当者.fUsrID"
        sqlstr = sqlstr & " WHERE (JunpDB.dbo.vMic個人営業実績.売上承認日 Between '" & T_開始.Text & "' And '" & T_終了.Text & "')"
        sqlstr = sqlstr & " GROUP BY JunpDB.dbo.vMic個人営業実績.現在支店名,"
        sqlstr = sqlstr & " JunpDB.dbo.vMic個人営業実績.担当者名, "
        sqlstr = sqlstr & " JunpDB.dbo.vMic担当者.fUsrYaku"


        sqlstr = sqlstr & " HAVING (SUM(JunpDB.dbo.vMic個人営業実績.受注金額 - JunpDB.dbo.vMic個人営業実績.支払額)"
        sqlstr = sqlstr & " > 0) AND (JunpDB.dbo.vMic担当者.fUsrYaku = '61' OR"
        sqlstr = sqlstr & " JunpDB.dbo.vMic担当者.fUsrYaku = '62' OR"
        sqlstr = sqlstr & " JunpDB.dbo.vMic担当者.fUsrYaku = '63' OR"
        sqlstr = sqlstr & " JunpDB.dbo.vMic担当者.fUsrYaku = '64' OR"
        sqlstr = sqlstr & " JunpDB.dbo.vMic担当者.fUsrYaku = '73' OR"
        sqlstr = sqlstr & " JunpDB.dbo.vMic担当者.fUsrYaku = '74' OR"
        sqlstr = sqlstr & " JunpDB.dbo.vMic担当者.fUsrYaku = '75' OR"
        sqlstr = sqlstr & " JunpDB.dbo.vMic担当者.fUsrYaku = '76')"


        sqlstr = sqlstr & " ORDER BY SUM(JunpDB.dbo.vMic個人営業実績.受注金額 - JunpDB.dbo.vMic個人営業実績.支払額) DESC"


        'データソースセット
        '実環境
        'SqlDataSource1.ConnectionString = "server='DBSV'; user id='ww_reader'; password=''; database='SalesDB'"

        '新WWテスト環境
        SqlDataSource1.ConnectionString = "server='SQLSV'; user id='ww_reader'; password=''; database='SalesDB'"

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
