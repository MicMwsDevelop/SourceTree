
Partial Class Hosyu_Hosyu_Info
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        If Me.IsPostBack Then
            If (Session("VISITORNAME") <> "") Then
                L_LoginInfo.Text = Session("VISITORNAME") & "がログイン中"
            Else
                L_LoginInfo.Text = "ログイン情報なし。再ログインしてください。"
                Response.Redirect("../Login.aspx")
                Exit Sub
            End If

        End If


        If (Session("VISITORNAME") <> "") Then
            L_LoginInfo.Text = Session("VISITORNAME") & "がログイン中"
        Else
            L_LoginInfo.Text = "ログイン情報なし。再ログインしてください。"
            Response.Redirect("../Login.aspx")
        End If





        Dim sqlstr As String

        '抽出文字列作成
        sqlstr = "SELECT 種別,Count(名称) AS 総数 FROM [PCManagement_master] GROUP BY PCManagement_master.種別;"

        sqlstr = "SELECT [Hosyu_保守リプレース調査].顧客No,"
        sqlstr = sqlstr & "[Hosyu_保守リプレース調査].支店名,"
        sqlstr = sqlstr & "[Hosyu_保守リプレース調査].顧客名,"
        sqlstr = sqlstr & "[Hosyu_保守リプレース調査].システム名称,"
        sqlstr = sqlstr & "[Hosyu_保守リプレース調査].前システム名称,"
        sqlstr = sqlstr & "[Hosyu_保守リプレース調査].Sメンテ料金 AS 保守金額,"
        sqlstr = sqlstr & "[Hosyu_保守リプレース調査].Sメンテ契約終了 AS 保守契約終了年月,"
        sqlstr = sqlstr & "[Hosyu_保守リプレース調査].売上月 AS システム売上月,"
        sqlstr = sqlstr & "[Hosyu_保守リプレース調査].改正時情報"
        sqlstr = sqlstr & " FROM [Hosyu_保守リプレース調査] LEFT JOIN "
        sqlstr = sqlstr & "[Hosyu_保守伝票抽出] ON [Hosyu_保守リプレース調査].顧客No = [Hosyu_保守伝票抽出].顧客 "
        sqlstr = sqlstr & "WHERE ((([Hosyu_保守伝票抽出].f商品コード) Is Null) AND ([Hosyu_保守リプレース調査].支店名 like '%" & D_拠点.Text & "%') AND ([Hosyu_保守リプレース調査].前システム名称 like '%" & D_前システム.Text & "%') "
        sqlstr = sqlstr & "AND ([Hosyu_保守リプレース調査].Sメンテ契約開始 < [Hosyu_保守リプレース調査].売上月) "
        sqlstr = sqlstr & "AND ([Hosyu_保守リプレース調査].S契約書回収年月 < [Hosyu_保守リプレース調査].売上月) "
        sqlstr = sqlstr & "AND ([Hosyu_保守リプレース調査].売上月 >= '2008/10')) "
        sqlstr = sqlstr & "ORDER BY [Hosyu_保守リプレース調査].売上月 DESC"



        'データソースセット
        '実環境
        SqlDataSource1.ConnectionString = "server='DBSV'; user id='ww_reader'; password=''; database='SalesDB'"
        SqlDataSource1.SelectCommand = sqlstr

        'ページは初期値
        GridView1.PageIndex = 0
        GridView1.DataBind()

    End Sub

    Protected Sub SqlDataSource1_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Selected

        L_検索結果.Text = e.AffectedRows & "件の予定があります。"

    End Sub


End Class
