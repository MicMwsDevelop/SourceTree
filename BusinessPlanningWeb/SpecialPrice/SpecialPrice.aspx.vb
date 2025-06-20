
Partial Class SpecialPrice_SpecialPrice
    Inherits System.Web.UI.Page


    Protected Sub SqlDataSource1_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Selected

        T_検索結果.Text = e.AffectedRows & "件Hitしました。"

    End Sub

    Protected Sub GridView1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.PreRender


        For Each tr As TableRow In GridView1.Rows
            For Each tc As TableCell In tr.Cells
                For Each c As Control In tc.Controls
                    If TypeOf c Is Label Then
                        Dim lbl As Label = CType(c, Label)
                        lbl.Text = lbl.Text.Replace(vbNewLine, "<br />")

                        '検索文字列の強調表示をしたい場合はここ

                        lbl.Text = lbl.Text.Replace("L特別", "<FONT COLOR=red>L特別</Font>")
                        lbl.Text = lbl.Text.Replace("Q特別", "<FONT COLOR=red>Q特別</Font>")

                    End If
                Next
            Next
        Next

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

    Protected Sub クリア_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles クリア.Click

        T_検索結果.Text = ""
        D_LQ.Text = "%"
        D_拠点.Text = "%"
        D_承認.Text = "%"


        'SqlDataSource1.ConnectionString = Nothing
        'SqlDataSource1.Dispose()


    End Sub
 
End Class
