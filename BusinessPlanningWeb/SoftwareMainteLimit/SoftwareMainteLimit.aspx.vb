
Partial Class SoftwareMainteLimit_SoftwareMainteLimit
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

    'Me.D_Tanto.Text = "%"

    If Me.IsPostBack = False Then
      '最初のロードの時に表示

      '利用終了ドロップダウンリストに当月から18ヵ月の期間を設定する
      D_利用終了.Items.Add(New ListItem("すべて", "%"))
      Dim index As Integer : index = 0
      Dim ym As New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
      Dim i As Integer
      For i = 1 To 18
        D_利用終了.Items.Add(New ListItem(Format(ym, "yyyy/MM"), Format(ym, "yyyy/MM")))
        If DateTime.Now.Year = ym.Year And DateTime.Now.Month = ym.Month Then
          index = i
        End If
        ym = ym.AddMonths(1)
      Next
      D_利用終了.SelectedIndex = index
    End If

  End Sub

  Protected Sub SqlDataSource1_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Selected

        L_検索結果.Text = e.AffectedRows & "件見つかりました。"

    End Sub


    
    Protected Sub B_クリア_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles B_クリア.Click

		Me.D_拠点.Text = "%"
		Me.D_利用終了.Text = "%"

    End Sub

    Protected Sub B_エクスポート_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles B_エクスポート.Click

        'GridView1.AllowPaging = False
        'GridView1.AllowSorting = False
        'GridView1.DataBind()

        Dim tw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        Dim frm As HtmlForm = New HtmlForm()


        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("content-disposition", "attachment;filename=SoftwareMainteLimitList.xls")
        Response.Charset = ""
        Page.EnableViewState = False
        Controls.Add(frm)
        frm.Controls.Add(GridView1)
        frm.RenderControl(hw)
        Response.Write(tw.ToString())

        Response.End()

        'GridView1.AllowPaging = True
        'GridView1.AllowSorting = True
        'GridView1.DataBind()

    End Sub

	'EXCEL出力時に下記のエラーが発生するのでおまじない
	'RegisterForEventValidation は Render(); の実行中にのみ呼び出されることができます。
	public Overrides Property EnableEventValidation() As Boolean

		Get
			' 「RegisterForEventValidation は Render(); の実行中にのみ呼び出されることができます。」
			' を出ないようにする
			Return False
		End Get
		Set(ByVal value As Boolean)
		End Set

	End Property

End Class
