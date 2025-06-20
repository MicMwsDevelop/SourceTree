
Partial Class OnlineInquiry_OnlineInquiry
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If Me.IsPostBack Then
        'If (Session("VISITORNAME") <> "") Then
        'L_LoginInfo.Text = Session("VISITORNAME") & "がログイン中"
        'Else
        'L_LoginInfo.Text = "ログイン情報なし。再ログインしてください。"
        'Response.Redirect("../Login.aspx")
        'End If

        'Exit Sub
        'End If


        'If (Session("VISITORNAME") <> "") Then
        'L_LoginInfo.Text = Session("VISITORNAME") & "がログイン中"
        'Else
        'L_LoginInfo.Text = "ログイン情報なし。再ログインしてください。"
        'Response.Redirect("../Login.aspx")
        'End If

        'Me.dropDownList拠点名.Text = "%"

    End Sub

    Protected Sub SqlDataSourceOnlineInquiry_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceOnlineInquiry.Selected

        label検索結果.Text = e.AffectedRows & "件見つかりました。"

    End Sub


    
    Protected Sub buttonClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonClear.Click

		Me.dropDownList拠点名.Text = "%"

    End Sub

End Class
