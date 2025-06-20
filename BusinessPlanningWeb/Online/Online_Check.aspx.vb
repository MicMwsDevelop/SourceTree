
Partial Class Online_Onlinet
    Inherits System.Web.UI.Page

    Protected Sub SqlDataSource1_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Selected

        L_検索結果.Text = e.AffectedRows & "件を検出しました。"

    End Sub


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


    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click

        T_UserNo.Text = "%"
        Me.D_拠点.Text = "%"
        T_作業担当.Text = "%"

        SqlDataSource1.DataBind()


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
