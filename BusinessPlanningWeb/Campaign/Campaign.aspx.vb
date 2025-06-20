
Partial Class Campaign_Campaign
    Inherits System.Web.UI.Page


    Public findstr(2) As String



    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click

        Dim sqlstr As String

        If (D_キャンペーンコード.Text = "%" And (T_検索文字列.Text = "" Or IsDBNull(T_検索文字列.Text) = True)) Then

            L_エラー.Text = "どちらかの抽出条件入力は必須です"
        Else
            L_エラー.Text = ""
            '抽出文字列作成
            sqlstr = MakeSqlString()

            'データソースセット
            '実環境
            SqlDataSource1.ConnectionString = "server='SQLSV'; user id='ww_reader'; password='20150801'; database='SalesDB'"


            SqlDataSource1.SelectCommand = sqlstr

            'ページは初期値
            GridView1.PageIndex = 0
        End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '        If Me.IsPostBack Then
        'If (Session("VISITORNAME") <> "") Then
        'L_LoginInfo.Text = Session("VISITORNAME") & "がログイン中"
        'Else
        'L_LoginInfo.Text = "ログイン情報なし。再ログインしてください。"
        'Response.Redirect("../Login.aspx")
        'End If
        '
        'Exit Sub
        'End If


        'If (Session("VISITORNAME") <> "") Then
        'L_LoginInfo.Text = Session("VISITORNAME") & "がログイン中"
        'Else
        'L_LoginInfo.Text = "ログイン情報なし。再ログインしてください。"
        'Response.Redirect("../Login.aspx")
        'End If

    End Sub

    Protected Sub クリア_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles クリア.Click

        T_検索結果.Text = ""
        T_検索文字列.Text = ""
        D_キャンペーンコード.Text = "%"
        L_エラー.Text = ""

        SqlDataSource1.ConnectionString = Nothing
        SqlDataSource1.Dispose()


    End Sub


    Protected Sub GridView1_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.PageIndexChanged

        Dim sqlstr As String

        sqlstr = MakeSqlString()
        SqlDataSource1.ConnectionString = "server='SQLSV'; user id='ww_reader'; password='20150801'; database='SalesDB'"
        SqlDataSource1.SelectCommand = sqlstr

        'GridView1.DataBind()

    End Sub

    Protected Sub GridView1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.PreRender

        Dim cnt As Integer
        Dim findstrcnt As Integer

        If (findstr(0) = "") Then
            findstrcnt = -1
        ElseIf (findstr(1) = "") Then
            findstrcnt = 0
        ElseIf (findstr(2) = "") Then
            findstrcnt = 1
        Else
            findstrcnt = 2
        End If



        For Each tr As TableRow In GridView1.Rows
            For Each tc As TableCell In tr.Cells
                For Each c As Control In tc.Controls
                    If TypeOf c Is Label Then
                        Dim lbl As Label = CType(c, Label)
                        lbl.Text = lbl.Text.Replace(vbNewLine, "<br />")

                        '検索文字列の強調表示をしたい場合はここ
                        For cnt = 0 To findstrcnt
                            lbl.Text = lbl.Text.Replace(findstr(cnt), "<FONT COLOR=red>" & findstr(cnt) & "</Font>")
                        Next

                    End If
                Next
            Next
        Next




    End Sub

    Protected Sub SqlDataSource1_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Selected

        T_検索結果.Text = e.AffectedRows & "件Hitしました。"

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub


    Function MakeSqlString() As String

        Dim SearchItem As String
        Dim SystemItem As String
        Dim choose As String
        Dim strbuf As String

        Dim Midcnt As Integer
        Dim cnt As Integer
        Dim chooseStrcnt As Integer
        Dim chooseStr As String

        Dim findstrcnt As String
        Dim sqlstr As String = ""

        MakeSqlString = sqlstr


        SearchItem = ""
        SystemItem = ""

        For cnt = 0 To 2
            findstr(cnt) = ""
        Next

        sqlstr = "SELECT [f受注番号] AS 受注No,[f販売先] AS 販売先,[fユーザー] AS ユーザー名,[f件名] AS 件名,[f受注金額] AS 金額,[f受注日] AS 受注日,[f売上承認日] AS 売上承認日,[f担当者名] AS 担当,[f担当支店名] AS 担当支店,[f備考] AS 備考 FROM [JunpMih受注ヘッダ]"

        If (D_キャンペーンコード.Text <> "%") Then
            SearchItem = " ([f備考] like '%" & D_キャンペーンコード.Text & "%') OR"
        End If


        If (T_検索文字列.Text <> "") Then

            choose = "AND"

            T_検索文字列.Text = T_検索文字列.Text.Trim
            T_検索文字列.Text = Replace(T_検索文字列.Text, "　", " ")

            '初期化の数々
            strbuf = Replace(T_検索文字列.Text, "'", "''")
            cnt = 0
            Midcnt = 0
            chooseStrcnt = 0
            findstrcnt = 0


            For cnt = 1 To Len(strbuf)

                If (Mid(strbuf, cnt, 1) = " ") Then
                    If (chooseStrcnt > 0) Then
                        chooseStr = Mid(strbuf, Midcnt + 1, chooseStrcnt)
                        SearchItem = SearchItem & " ([f備考] like '%" & chooseStr & "%') " & choose & ""

                        '強調文字列格納領域へ（３キーワードまで）
                        If (findstrcnt < 3) Then
                            findstr(findstrcnt) = chooseStr
                            findstrcnt = findstrcnt + 1
                        End If


                        Midcnt = cnt
                        chooseStrcnt = 0
                    Else
                        Midcnt = cnt
                        chooseStrcnt = 0
                    End If
                Else
                    chooseStrcnt = chooseStrcnt + 1
                End If
            Next

            chooseStr = Mid(strbuf, Midcnt + 1, chooseStrcnt)
            SearchItem = SearchItem & " ([f備考] like '%" & chooseStr & "%')"
            SearchItem = "(" & SearchItem & ") AND"

            '強調文字列格納領域へ（３キーワードまで）
            If (findstrcnt < 3) Then
                findstr(findstrcnt) = chooseStr
            End If

        End If

        If (SearchItem <> "") Then
            SearchItem = Left(SearchItem, Len(SearchItem) - 3)
            sqlstr = sqlstr & " WHERE" & SearchItem & " ORDER BY [f受注番号] DESC"
        Else
            sqlstr = sqlstr & " ORDER BY [f受注番号] DESC"
        End If

        MakeSqlString = sqlstr

    End Function


End Class
