Imports System.Data
Imports System.Data.SqlClient

Partial Class schedule_scheduleMenu
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.IsPostBack Then
            If (Session("VISITORNAME") <> "") Then
                L_LoginInfo.Text = Session("VISITORNAME") & "がログイン中"
            Else
                L_LoginInfo.Text = "ログイン情報なし。再ログインしてください。"
                Response.Redirect("Login.aspx")
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

    Protected Sub SqlDataSource1_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Selected

        L_検索結果.Text = e.AffectedRows & "件の予定があります。"

    End Sub


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Response.Redirect("Scheduleselect.aspx")

    End Sub

    
    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click

        Response.Redirect("ScheduleDecide.aspx")

    End Sub

    Protected Sub Calendar1_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles Calendar1.DayRender

        Dim strSQL As String

        strSQL = "SELECT COUNT(*) FROM Schedule WHERE 作業予定日 = @sdate AND 終了フラグ = 0 AND 作業担当者 = '" & Session("VISITORNAME") & "';"


        Dim cnn As SqlConnection = New SqlConnection("Data Source=dbsv;Initial Catalog = SalesDB; user id='ww_reader';")

        Dim sqlcmdSelect As New SqlCommand(strSQL, cnn)
        sqlcmdSelect.CommandTimeout = 15

        sqlcmdSelect.Parameters.Add("@sdate", SqlDbType.DateTime)
        sqlcmdSelect.Parameters("@sdate").Value = e.Day.Date


        cnn.Open()
        Dim objDr As SqlDataReader = sqlcmdSelect.ExecuteReader()

        Do While objDr.Read()

            If objDr.GetInt32(0) = 0 Then

                e.Cell.Controls.Add(New LiteralControl("<br /><br /><br />"))

            Else

                e.Cell.Controls.Add(New LiteralControl("<br /><br />" & String.Format("{0}件", objDr.GetInt32(0))))
                e.Cell.BackColor = Drawing.Color.DarkBlue
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

        If (Cname = "Update") Then

            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(index)
            Dim SyuRyoF As CheckBox = row.Cells(12).FindControl("CheckBox1")


            If (SyuRyoF.Checked = "True") Then
                'ここで、顧客情報を書き換える！！！！
                SyuRyoF.Checked = "True"



            End If


        End If

    End Sub

    
End Class
