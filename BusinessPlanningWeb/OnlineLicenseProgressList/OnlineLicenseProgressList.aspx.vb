Partial Class OnlineLicenseProgressList_OnlineLicenseProgressList
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

	End Sub

	Protected Sub SqlDataSourceProgressList_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceProgressList.Selected

		labelResult.Text = e.AffectedRows & "件見つかりました。"

	End Sub

	Protected Sub buttonClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonClear.Click

		Me.comboBoxSale.Text = "%"
		Me.comboBoxSC.Text = "%"
		Me.comboBoxOffice.Text = "%"
		Me.textBoxCustomerNo.Text = ""
		GridViewProgressList.DataSource = Nothing
		GridViewProgressList.DataBind()

	End Sub

	Protected Sub buttonSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonSearch.Click

		If textBoxCustomerNo.Text.Length > 0 Then
			Dim sqlStr As String
			sqlStr = "SELECT [顧客No],[得意先No],[顧客名],[オン資担当],[導入意思],[工事種別],[ステータス],[現調完了月],[導入月],[価格帯],[部署],[都道府県],[営業部],[SC名],[拠点名],[進捗管理ステータス] FROM [vオンライン資格確認進捗管理情報]"
			sqlStr = sqlStr & " WHERE [顧客No] = " & textBoxCustomerNo.Text
			SqlDataSourceProgressList.SelectCommand = sqlStr

			'ページは初期値
			GridViewProgressList.PageIndex = 0
		End If

	End Sub

	Protected Sub buttonExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonExcel.Click

		GridViewUnvisible.Visible = True

		Dim sqlStr As String
		sqlStr = "SELECT * FROM [vオンライン資格確認進捗管理情報] ORDER BY [営業部コード],[SCコード],[拠点コード],[顧客No]"
		SqlDataSourceUnvisible.SelectCommand = sqlStr

		'ページは初期値
		GridViewUnvisible.PageIndex = 0

		'Excel出力
		Dim tw As New System.IO.StringWriter()
		Dim hw As New System.Web.UI.HtmlTextWriter(tw)
		Dim frm As HtmlForm = New HtmlForm()
		Dim fName As String
		fName = "オン資進捗管理顧客一覧.xls"
		Response.ClearContent()
		Response.ContentType = "application/vnd.ms-excel"
		Response.AddHeader("content-disposition", "attachment;filename=" & HttpUtility.UrlEncode(fName))
		Response.Charset = ""
		Response.ContentEncoding = System.Text.Encoding.GetEncoding("Shift_JIS")  '指定しないと文字化けする場合有Page.EnableViewState = False
		Controls.Add(frm)
		frm.Controls.Add(GridViewUnvisible)
		frm.RenderControl(hw)
		Response.Write(tw.ToString())

		Response.End()

		GridViewUnvisible.Visible = False

	End Sub

	'EXCEL出力時に下記のエラーが発生するのでおまじない
	'RegisterForEventValidation は Render(); の実行中にのみ呼び出されることができます。
	Public Overrides Property EnableEventValidation() As Boolean

		Get
			' 「RegisterForEventValidation は Render(); の実行中にのみ呼び出されることができます。」
			' を出ないようにする
			Return False
		End Get
		Set(ByVal value As Boolean)
		End Set

	End Property
End Class
