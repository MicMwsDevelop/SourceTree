Imports System.Data
Imports System.Data.SqlClient

Partial Class schedule_scheduleMenu
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If Me.IsPostBack Then
        'If (Session("VISITORNAME") <> "") Then
        'L_LoginInfo.Text = Session("VISITORNAME") & "がログイン中"
        'Else
        'L_LoginInfo.Text = "ログイン情報なし。再ログインしてください。"
        'Response.Redirect("Login.aspx")
        'End If

        'Exit Sub
        'End If

        'If (Session("VISITORNAME") <> "") Then
        'L_LoginInfo.Text = Session("VISITORNAME") & "がログイン中"
        'Else
        'L_LoginInfo.Text = "ログイン情報なし。再ログインしてください。"
        'Response.Redirect("../Login.aspx")
        'End If

        L_基準.Text = Format(DateAdd(DateInterval.Month, -2, Now()), "yyyy/MM/dd")

    End Sub

    Protected Sub SqlDataSource1_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Selected

        L_検索結果.Text = e.AffectedRows & "件の予定があります。"

    End Sub


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Response.Redirect("PCManagementSche.aspx")

    End Sub

    
    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click

        Response.Redirect("PCManagementselect.aspx")

    End Sub

    Protected Sub Calendar1_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles Calendar1.DayRender

        Dim strSQL As String

        'strSQL = "SELECT COUNT(*) FROM PCManagement WHERE 開始日 = @sdate ;"
        strSQL = "SELECT 案件名 FROM PCManagement_Header WHERE 開始日 = @sdate ;"


        Dim cnn As SqlConnection = New SqlConnection("Data Source=dbsv;Initial Catalog = SalesDB; user id='ww_reader';")

        Dim sqlcmdSelect As New SqlCommand(strSQL, cnn)
        sqlcmdSelect.CommandTimeout = 15

        sqlcmdSelect.Parameters.Add("@sdate", SqlDbType.DateTime)
        sqlcmdSelect.Parameters("@sdate").Value = e.Day.Date


        cnn.Open()
        Dim objDr As SqlDataReader = sqlcmdSelect.ExecuteReader()

        Do While objDr.Read()

            'If objDr.GetInt32(0) = 0 Then
            If objDr.GetString(0) = "" Then

                e.Cell.Controls.Add(New LiteralControl("<br /><br /><br />"))

            Else

                'e.Cell.Controls.Add(New LiteralControl("<br /><br />" & String.Format("{0}件", objDr.GetInt32(0))))
                e.Cell.Controls.Add(New LiteralControl("<br /><br />" & String.Format("{0}", objDr.GetString(0))))
                e.Cell.BackColor = Drawing.Color.DarkOrange
                e.Cell.ForeColor = Drawing.Color.White
            End If

        Loop

LBL_EXIT:
        cnn.Close()
        cnn = Nothing

        sqlcmdSelect = Nothing

    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand

        Dim Cname As String
        Cname = e.CommandName

        Dim sqlstr2 As String


        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim row As GridViewRow = GridView1.Rows(index)
        Dim 案件No As Integer = row.Cells(1).Text
        Dim 案件名 As String = row.Cells(2).Text
        Dim 終了日 As String = row.Cells(4).Text


        '選択イベント
        If (Cname = "Select") Then

            '抽出文字列作成
            '【販売促進店】
            'sqlstr2 = "SELECT 案件No,案件名,ハード種別,ハード名 FROM [PCManagement_Details] WHERE 案件名 = '" & SyuRyoF & "';"
            sqlstr2 = "SELECT 案件No,案件名,ハード種別,数量 FROM [PCManagement_Details] WHERE 案件No = '" & 案件No & "';"

            
            'データソースセット
            '実環境
            SqlDataSource2.ConnectionString = "server='DBSV'; user id='ww_reader'; password=''; database='SalesDB'"
            SqlDataSource2.SelectCommand = sqlstr2

            'ページは初期値
            GridView2.PageIndex = 0

        ElseIf (Cname = "削除") Then

            Dim SQL As System.Data.SqlClient.SqlCommand
            Dim conn As New SqlConnection()

            conn.ConnectionString = "Data Source=DBSV;Initial Catalog = SalesDB; user id='sa'; password='07883510'"
            conn.Open()

            Sql = conn.CreateCommand
            'レコード追加

            '詳細テーブル削除
            SQL.CommandText = "DELETE FROM [PCManagement_Details] WHERE 案件No = '" & 案件No & "';"
            'SQL実行
            SQL.ExecuteNonQuery()

            'ヘッダーテーブル削除
            SQL.CommandText = "DELETE FROM [PCManagement_Header] WHERE 案件名 = '" & 案件名 & "' AND 終了日 = '" & 終了日 & "';"
            'SQL実行
            SQL.ExecuteNonQuery()


            '解放
            SQL.Dispose()

            GridView1.DataBind()
            GridView2.DataBind()

        End If

    End Sub

    
    Protected Sub GridView1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.PreRender

        For Each tr As TableRow In GridView1.Rows

            For Each tc As TableCell In tr.Cells
                For Each c As Control In tc.Controls
                    If TypeOf c Is Label Then
                        Dim lbl As Label = CType(c, Label)
                        lbl.Text = lbl.Text.Replace(vbNewLine, "<br />")

                    End If
                Next
            Next
        Next


    End Sub

   
End Class
