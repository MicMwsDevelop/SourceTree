
Partial Class misyounin_misyounin
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

      '納期ドロップダウンリストに現在日時から算出した期間を設定する
      D_納期.Items.Add(New ListItem("すべて", "%"))

      Dim year As Integer
      year = DateTime.Now.Year
      If DateTime.Now.Month < 8 Then
        year = year - 1
      End If

      Dim nouki As New DateTime(year, 8, 1)
      Dim i As Integer
      For i = 1 To 18
        D_納期.Items.Add(New ListItem(Format(nouki, "yyyy/MM"), Format(nouki, "yyyy/MM")))
        nouki = nouki.AddMonths(1)
      Next
    End If

  End Sub

  Protected Sub SqlDataSource1_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Selected

        L_検索結果.Text = e.AffectedRows & "件見つかりました。"

    End Sub


    
    Protected Sub B_クリア_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles B_クリア.Click

		Me.D_Tanto.Text = "%"
		Me.D_承認.Text = "%"
		Me.D_システム.Text = "%"
		Me.D_Area.Text = "%"
		Me.D_納期.Text = "%"
		Me.textBoxKenmei.Text = "%"
		Me.comboBoxReplace.Text = "%"

    End Sub

    Dim priceTotal As Decimal = 0
    Dim VPTotal As Integer = 0


    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            ' UnitPrice および QuantityTotal をそれぞれの累計用変数に加算します。
            priceTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "受注金額"))
            VPTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ES本数"))

        ElseIf e.Row.RowType = DataControlRowType.Footer Then

            e.Row.Cells(0).Text = "Totals:"
            ' フッターに、累計を表示します。
            e.Row.Cells(5).Text = priceTotal.ToString("c")
            e.Row.Cells(13).Text = VPTotal.ToString("G")

            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Font.Bold = True
        End If

    End Sub

    Protected Sub B_エクスポート_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles B_エクスポート.Click

        'GridView1.AllowPaging = False
        'GridView1.AllowSorting = False
        'GridView1.DataBind()

        Dim tw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        Dim frm As HtmlForm = New HtmlForm()


        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("content-disposition", "attachment;filename=MisyouninList.xls")
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
