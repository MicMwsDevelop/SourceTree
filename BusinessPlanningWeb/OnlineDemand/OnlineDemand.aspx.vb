
Partial Class OnlineDemand_OnlineDemand
    Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

  End Sub

  Protected Sub SqlDataSourceOnlineDemand_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceOnlineDemand.Selected

        labelResult.Text = e.AffectedRows & "件見つかりました。"

  End Sub


    
  Protected Sub buttonClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonClear.Click

	Me.comboBoxSC.Text = "%"
	Me.comboBoxOffice.Text = "%"
	Me.textBoxStartDate.Text = ""
	Me.textBoxEndDate.Text = ""
	Me.textBoxCustomerNo.Text = ""
	GridViewOnlineDemand.DataSource = Nothing
	GridViewOnlineDemand.DataBind()

  End Sub

  Protected Sub buttonSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonSearch.Click
	
	If textBoxStartDate.Text.Length > 0 AND textBoxEndDate.Text.Length > 0 Then
		Dim sqlStr As String
		sqlStr = "SELECT [顧客No],[顧客名],[得意先コード],[商品コード],[商品名],[売上金額],[受付No],[申請日時],[売上日時],[営業部コード],[営業部名],[拠点コード],[拠点名] FROM [vBPW_各種作業料顧客一覧]"
		sqlStr = sqlStr & " WHERE CONVERT(int, CONVERT(nvarchar, [申請日時], 112)) BETWEEN CONVERT(int, REPLACE('" & textBoxStartDate.Text & "', '/', '')) AND CONVERT(int, REPLACE('" & textBoxEndDate.Text & "', '/', ''))"
		SqlDataSourceOnlineDemand.SelectCommand = sqlStr

		'ページは初期値
		GridViewOnlineDemand.PageIndex = 0
	Else If textBoxCustomerNo.Text.Length > 0 Then
		Dim sqlStr As String
		sqlStr = "SELECT [顧客No],[顧客名],[得意先コード],[商品コード],[商品名],[売上金額],[受付No],[申請日時],[売上日時],[営業部コード],[営業部名],[拠点コード],[拠点名] FROM [vBPW_各種作業料顧客一覧]"
		sqlStr = sqlStr & " WHERE [顧客No] = " & textBoxCustomerNo.Text
		SqlDataSourceOnlineDemand.SelectCommand = sqlStr

		'ページは初期値
		GridViewOnlineDemand.PageIndex = 0
	End If
  End Sub

  Protected Sub buttonExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonExport.Click

	GridViewUnvisible.Visible = True

	Dim sqlStr As String
	sqlStr = "SELECT * FROM [vBPW_各種作業料顧客一覧] ORDER BY [営業部コード],[拠点コード],[申請日時],[顧客No]"
	SqlDataSourceUnvisible.SelectCommand = sqlStr

	'ページは初期値
	GridViewUnvisible.PageIndex = 0

	'Excel出力
	Dim tw As New System.IO.StringWriter()
	Dim hw As New System.Web.UI.HtmlTextWriter(tw)
	Dim frm As HtmlForm = New HtmlForm()
	Dim fName As String
	fName = "各種作業料顧客一覧.xls"
	Response.ClearContent()
	'Response.ContentType = "application/vnd.ms-excel"
	Response.ContentType = "vnd.openxmlformats-officedocument.spreadsheetml.sheet"
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
