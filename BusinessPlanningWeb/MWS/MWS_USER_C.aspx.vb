
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

        MWS_USER_COUNT()

    End Sub


    Sub MWS_USER_COUNT()
        Dim sqlstr As String

        '抽出文字列作成

        '【営業実績順位ＳＱＬ】
        sqlstr = "SELECT JunpDB.dbo.vMic全ユーザー2.支店名, Count(JunpDB.dbo.vMic全ユーザー2.顧客No) AS [MWS総ユーザー数]"
        sqlstr = sqlstr & " FROM JunpDB.dbo.vMic全ユーザー2"
        sqlstr = sqlstr & " WHERE (((JunpDB.dbo.vMic全ユーザー2.システム名称) LIKE 'palette%') AND ((JunpDB.dbo.vMic全ユーザー2.終了フラグ)='0'))"
        sqlstr = sqlstr & " GROUP BY JunpDB.dbo.vMic全ユーザー2.支店コード,JunpDB.dbo.vMic全ユーザー2.支店名"
        sqlstr = sqlstr & " ORDER BY JunpDB.dbo.vMic全ユーザー2.支店コード"


        'データソースセット
        '実環境
        SqlDataSource1.ConnectionString = "server='DBSV'; user id='ww_reader'; password=''; database='SalesDB'"
        SqlDataSource1.SelectCommand = sqlstr


        sqlstr = "SELECT JunpDB.dbo.vMic全ユーザー2.支店名, Count(JunpDB.dbo.vMic全ユーザー2.顧客No) AS [バリューパック]"
        sqlstr = sqlstr & " FROM JunpDB.dbo.vMic全ユーザー2"
        sqlstr = sqlstr & " WHERE (((JunpDB.dbo.vMic全ユーザー2.システム名称) LIKE 'palette%') AND ((JunpDB.dbo.vMic全ユーザー2.終了フラグ)='0') AND ((JunpDB.dbo.vMic全ユーザー2.MWS_申込種別)='1'))"
        sqlstr = sqlstr & " GROUP BY JunpDB.dbo.vMic全ユーザー2.支店コード,JunpDB.dbo.vMic全ユーザー2.支店名"
        sqlstr = sqlstr & " ORDER BY JunpDB.dbo.vMic全ユーザー2.支店コード"


        SqlDataSource2.ConnectionString = "server='DBSV'; user id='ww_reader'; password=''; database='SalesDB'"
        SqlDataSource2.SelectCommand = sqlstr


        sqlstr = "SELECT JunpDB.dbo.vMic全ユーザー2.支店名, Count(JunpDB.dbo.vMic全ユーザー2.顧客No) AS [アップグレード]"
        sqlstr = sqlstr & " FROM JunpDB.dbo.vMic全ユーザー2"
        sqlstr = sqlstr & " WHERE (((JunpDB.dbo.vMic全ユーザー2.システム名称) LIKE 'palette%') AND ((JunpDB.dbo.vMic全ユーザー2.終了フラグ)='0') AND ((JunpDB.dbo.vMic全ユーザー2.MWS_申込種別)='2'))"
        sqlstr = sqlstr & " GROUP BY JunpDB.dbo.vMic全ユーザー2.支店コード,JunpDB.dbo.vMic全ユーザー2.支店名"
        sqlstr = sqlstr & " ORDER BY JunpDB.dbo.vMic全ユーザー2.支店コード"


        SqlDataSource3.ConnectionString = "server='DBSV'; user id='ww_reader'; password=''; database='SalesDB'"
        SqlDataSource3.SelectCommand = sqlstr


        sqlstr = "SELECT JunpDB.dbo.vMic全ユーザー2.支店名, Count(JunpDB.dbo.vMic全ユーザー2.顧客No) AS [月額課金]"
        sqlstr = sqlstr & " FROM JunpDB.dbo.vMic全ユーザー2"
        sqlstr = sqlstr & " WHERE (((JunpDB.dbo.vMic全ユーザー2.システム名称) LIKE 'palette%') AND ((JunpDB.dbo.vMic全ユーザー2.終了フラグ)='0') AND ((JunpDB.dbo.vMic全ユーザー2.MWS_申込種別)='3'))"
        sqlstr = sqlstr & " GROUP BY JunpDB.dbo.vMic全ユーザー2.支店コード,JunpDB.dbo.vMic全ユーザー2.支店名"
        sqlstr = sqlstr & " ORDER BY JunpDB.dbo.vMic全ユーザー2.支店コード"


        SqlDataSource4.ConnectionString = "server='DBSV'; user id='ww_reader'; password=''; database='SalesDB'"
        SqlDataSource4.SelectCommand = sqlstr


        'ページは初期値
        GridView1.PageIndex = 0

    End Sub

End Class
