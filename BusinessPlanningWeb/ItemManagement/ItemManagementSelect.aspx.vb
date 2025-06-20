
Partial Class ItemManagement_ItemManagementSelect
    Inherits System.Web.UI.Page



    'キャンセルボタンイベント
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click

        Response.Redirect("ItemManagementRegister.aspx")

    End Sub

    '決定ボタンイベント
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim row As GridViewRow = GridView1.SelectedRow


        If (IsNothing(row) = False) Then
            Response.Redirect("ItemManagementRegister.aspx?UserNo=" & row.Cells(2).Text)
        Else
            L_Error.Text = "医院が選択されていません"
        End If


    End Sub


    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.IsPostBack Then
            Exit Sub
        End If

        L_Error.Text = ""


        If (Session("VISITORNAME") <> "") Then
        Else
            'Response.Redirect("Login.aspx")
        End If

    End Sub

    Protected Sub B_検索_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles B_検索.Click

        Dim sqlstr As String

        sqlstr = MakeSqlString()

        SqlDataSource1.ConnectionString = "Data Source=DBSV;Initial Catalog = SalesDB; user id='sa'; password='07883510'"
        SqlDataSource1.SelectCommand = sqlstr

        GridView1.PageIndex = 0

    End Sub

    Function MakeSqlString() As String

        Dim SearchItem As String = ""

        Dim sqlstr As String = "SELECT [支店名],[顧客No],[顧客名],[住所],[電話番号],[都道府県名] FROM [vMicUser]"

        If (T_顧客No.Text <> "") Then

            SearchItem = " ([顧客No] = " & T_顧客No.Text & ") AND"

        End If

        If (T_医院名.Text <> "") Then

            SearchItem = " ([顧客名] like '%" & T_医院名.Text & "%') AND"

        End If

        If (T_電話番号.Text <> "") Then

            SearchItem = " ([電話番号] like '%" & T_電話番号.Text & "%') AND"

        End If

        If (T_住所.Text <> "") Then

            SearchItem = " ([住所] like '%" & T_住所.Text & "%') AND"

        End If


        If (SearchItem <> "") Then
            SearchItem = Left(SearchItem, Len(SearchItem) - 3)
            sqlstr = sqlstr & " WHERE" & SearchItem & " ORDER BY [顧客No]"
        Else
            sqlstr = sqlstr & " ORDER BY [顧客No]"
        End If

        MakeSqlString = sqlstr
    End Function

    Protected Sub GridView1_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.PageIndexChanged

        Dim sqlstr As String

        sqlstr = MakeSqlString()
        SqlDataSource1.ConnectionString = "Data Source=DBSV;Initial Catalog = SalesDB; user id='sa'; password='07883510'"
        SqlDataSource1.SelectCommand = sqlstr

    End Sub



    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

        L_Error.Text = ""

    End Sub
End Class
