
Partial Class OnlineLicenseHomon_OnlineLicenseHomon
    Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

  End Sub

  Protected Sub SqlDataSourceOnlineLicenseHomon_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceOnlineLicenseHomon.Selected

        labelResult.Text = e.AffectedRows & "件見つかりました。"

  End Sub


    
  Protected Sub buttonClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonClear.Click

	Me.comboBoxSC.Text = "%"
	Me.comboBoxOffice.Text = "%"
	Me.textBoxAcceptMonth.Text = ""
	Me.textBoxSaleMonth.Text = ""
	Me.textBoxCustomerNo.Text = ""
	GridViewOnlineLicenseHomon.DataSource = Nothing
	GridViewOnlineLicenseHomon.DataBind()

  End Sub

  Protected Sub buttonSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonSearch.Click
	
	Dim sqlStr As String
	sqlStr = "SELECT [営業部コード],[営業部名],[拠点コード],[拠点名],[顧客No],[顧客名],[商品コード],[商品名],iif([カードリーダー申込No] is null,'無','有') as カードリーダー,[売上金額],CONVERT(Date, [申込日時]) as 申込日,CONVERT(Date, [売上日時]) as 売上日 FROM [vBPW_オン資訪問診療連携費顧客一覧]"
	If textBoxAcceptMonth.Text.Length > 0 AND textBoxSaleMonth.Text.Length > 0 Then
		sqlStr = sqlStr & " WHERE CONVERT(int, LEFT(CONVERT(nvarchar, [申込日時], 112), 6)) = CONVERT(int, REPLACE('" & textBoxAcceptMonth.Text & "', '/', ''))"
		sqlStr = sqlStr & " AND CONVERT(int, LEFT(CONVERT(nvarchar, [売上日時], 112), 6)) = CONVERT(int, REPLACE('" & textBoxSaleMonth.Text & "', '/', ''))"
		sqlStr = sqlStr & " ORDER BY [営業部コード],[拠点コード],[申込日],[顧客No]"
		SqlDataSourceOnlineLicenseHomon.SelectCommand = sqlStr

		'ページは初期値
		GridViewOnlineLicenseHomon.PageIndex = 0
	Else If textBoxAcceptMonth.Text.Length > 0 AND textBoxSaleMonth.Text.Length = 0 Then
		sqlStr = sqlStr & " WHERE CONVERT(int, LEFT(CONVERT(nvarchar, [申込日時], 112), 6)) = CONVERT(int, REPLACE('" & textBoxAcceptMonth.Text & "', '/', ''))"
		sqlStr = sqlStr & " ORDER BY [営業部コード],[拠点コード],[申込日],[顧客No]"
		SqlDataSourceOnlineLicenseHomon.SelectCommand = sqlStr

		'ページは初期値
		GridViewOnlineLicenseHomon.PageIndex = 0
	Else If textBoxAcceptMonth.Text.Length = 0 AND textBoxSaleMonth.Text.Length > 0 Then
		sqlStr = sqlStr & " WHERE CONVERT(int, LEFT(CONVERT(nvarchar, [売上日時], 112), 6)) = CONVERT(int, REPLACE('" & textBoxSaleMonth.Text & "', '/', ''))"
		sqlStr = sqlStr & " ORDER BY [営業部コード],[拠点コード],[申込日],[顧客No]"
		SqlDataSourceOnlineLicenseHomon.SelectCommand = sqlStr

		'ページは初期値
		GridViewOnlineLicenseHomon.PageIndex = 0
	Else If textBoxCustomerNo.Text.Length > 0 Then
		sqlStr = sqlStr & " WHERE [顧客No] = " & textBoxCustomerNo.Text
		SqlDataSourceOnlineLicenseHomon.SelectCommand = sqlStr

		'ページは初期値
		GridViewOnlineLicenseHomon.PageIndex = 0
	Else
		sqlStr = sqlStr & " ORDER BY [営業部コード],[拠点コード],[申込日],[顧客No]"
		SqlDataSourceOnlineLicenseHomon.SelectCommand = sqlStr

		'ページは初期値
		GridViewOnlineLicenseHomon.PageIndex = 0
	End If
  End Sub

  Protected Sub buttonExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonExport.Click

	GridViewUnvisible.Visible = True

	Dim sqlStr As String
	sqlStr = "SELECT [営業部コード],[営業部名],[拠点コード],[拠点名],[顧客No],[顧客名],[得意先コード] as 得意先No,[商品コード],[商品名],iif([カードリーダー申込No] is null,'無','有') as カードリーダー,[売上金額],CONVERT(Date, [申込日時]) as 申込日,CONVERT(Date, [売上日時]) as 売上日 FROM [vBPW_オン資訪問診療連携費顧客一覧] ORDER BY [営業部コード],[拠点コード],[申込日],[顧客No]"
	SqlDataSourceUnvisible.SelectCommand = sqlStr

	'ページは初期値
	GridViewUnvisible.PageIndex = 0

	'Excel出力
	Dim tw As New System.IO.StringWriter()
	Dim hw As New System.Web.UI.HtmlTextWriter(tw)
	Dim frm As HtmlForm = New HtmlForm()
	Dim fName As String
	fName = "オン資訪問診療連携費顧客一覧.xls"
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
