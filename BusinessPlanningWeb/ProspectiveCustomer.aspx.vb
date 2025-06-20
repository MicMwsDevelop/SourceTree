
Partial Class _Default
    Inherits System.Web.UI.Page

    Public sqlstr As String


    Protected Sub SqlDataSource1_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Selected

        L_検索結果.Text = e.AffectedRows & "件の登録があります。"

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

    'Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    sqlstr = ""
    '    If Me.T_UserName.Text <> "" Then
    '        sqlstr = "([UserName] like '%" & T_UserName.Text & "%')"
    '    End If
    '    If Me.D_Check.Text <> "" Then
    '        sqlstr = sqlstr & "([Check] like '%" & D_Check.Text & "%')"
    '    End If
    '    If sqlstr <> "" Then
    '        sqlstr = "WHERE " & sqlstr
    '    End If
    '    SqlDataSource1.ConnectionString = "Data Source=aspsv\SQLEXPRESS;Initial Catalog = BusinessPlanningWeb; user id='sa'; password='07883510'"
    '    SqlDataSource1.SelectCommand = "SELECT * FROM [ProspectiveCustomer] " & sqlstr & "ORDER BY [ID]"
    'End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click


        GridView1.PageIndex = 0

        Me.T_UserName.Text = "%"
        Me.T_UserAdress.Text = "%"
        Me.T_UserNo.Text = "%"
        Me.T_UserTel.Text = "%"
        Me.D_Check.Text = "%"
        Me.D_Tanto.Text = "%"

        'L_検索結果.Text = ""
        lblResult.Text = ""

    End Sub

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
            Response.Redirect("Login.aspx")
        End If

        Me.T_UserName.Text = "%"
        Me.T_UserAdress.Text = "%"
        Me.T_UserNo.Text = "%"
        Me.T_UserTel.Text = "%"
        Me.D_Check.Text = "%"
        Me.D_Tanto.Text = "%"


    End Sub

    Protected Sub SqlDataSource1_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Updated

        If e.AffectedRows = 0 Then

            lblResult.Text = "同時実行エラーのため更新できませんでした。再試行してください"
        Else
            lblResult.Text = ""

        End If

    End Sub


    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        On Error GoTo Err_RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Lbl_Str As Label = e.Row.Cells(1).FindControl("Label1")

          
            If (Lbl_Str.Text = "済") Then
                e.Row.Cells(0).Controls(0).Visible = False
            End If

        End If

Exit_RowDataBound:
        Exit Sub

Err_RowDataBound:
        Resume Exit_RowDataBound

    End Sub

End Class
