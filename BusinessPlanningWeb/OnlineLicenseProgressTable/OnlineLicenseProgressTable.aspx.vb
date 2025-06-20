
Partial Class OnlineLicenseProgressTable_OnlineLicenseProgressTable
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


	End Sub

    Dim 集計対象外件数 As Integer = 0
    Dim 営業活動中件数 As Integer = 0
    Dim 現調予定件数 As Integer = 0
    Dim 現調済件数 As Integer = 0
    Dim 導入済件数 As Integer = 0
    Dim 全体件数 As Integer = 0

    Protected Sub GridViewTable_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewTable.RowDataBound

        Dim 比率 as Double

        If e.Row.RowType = DataControlRowType.DataRow Then
            集計対象外件数 += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "集計対象外件数"))
            営業活動中件数  += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "営業活動中件数"))
            現調予定件数  += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "現調予定件数"))
            現調済件数  += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "現調済件数"))
            導入済件数  += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "導入済件数"))
            全体件数  += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "全体の件数"))

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "総計"
            e.Row.Cells(2).Text = 集計対象外件数.ToString("N0")
            e.Row.Cells(4).Text = 営業活動中件数.ToString("N0")
            e.Row.Cells(6).Text = 現調予定件数.ToString("N0")
            e.Row.Cells(8).Text = 現調済件数.ToString("N0")
            e.Row.Cells(10).Text = 導入済件数.ToString("N0")
            e.Row.Cells(12).Text = 全体件数.ToString("N0")

            If 集計対象外件数 = 0 Then
               e.Row.Cells(3).Text = "0.0%"
            Else
               比率 = 集計対象外件数 / 全体件数
               e.Row.Cells(3).Text = 比率.ToString("#0.#%")
            End If
            If 営業活動中件数 = 0 Then
               e.Row.Cells(5).Text = "0.0%"
            Else
               比率 = 営業活動中件数 / 全体件数
               e.Row.Cells(5).Text = 比率.ToString("#0.#%")
            End If
            If 現調予定件数 = 0 Then
               e.Row.Cells(7).Text = "0.0%"
            Else
               比率 = 現調予定件数 / 全体件数
               e.Row.Cells(7).Text = 比率.ToString("#0.#%")
            End If
            If 現調済件数 = 0 Then
               e.Row.Cells(9).Text = "0.0%"
            Else
               比率 = 現調済件数 / 全体件数
               e.Row.Cells(9).Text = 比率.ToString("#0.#%")
            End If
            If 導入済件数 = 0 Then
               e.Row.Cells(11).Text = "0.0%"
            Else
               比率 = 導入済件数 / 全体件数
               e.Row.Cells(11).Text = 比率.ToString("#0.#%")
            End If
            If 全体件数 = 0 Then
               e.Row.Cells(13).Text = "0.0%"
            Else
               比率 = 全体件数 / 全体件数
               e.Row.Cells(13).Text = 比率.ToString("#0.#%")
            End If

            'e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            'e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Font.Bold = True
        End If

    End Sub

	Protected Sub buttonExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonExcel.Click

		Dim tw As New System.IO.StringWriter()
		Dim hw As New System.Web.UI.HtmlTextWriter(tw)
		Dim frm As HtmlForm = New HtmlForm()
		Dim fName As String
		fName = "オン資進捗管理表.xls"
		Response.ContentType = "application/vnd.ms-excel"
		Response.AddHeader("content-disposition", "attachment;filename=" & HttpUtility.UrlEncode(fName))
		Response.Charset = ""
		Response.ContentEncoding = System.Text.Encoding.GetEncoding("Shift_JIS")  '指定しないと文字化けする場合有Page.EnableViewState = False
		Controls.Add(frm)
		frm.Controls.Add(GridViewTable)
		frm.RenderControl(hw)
		Response.Write(tw.ToString())

		Response.End()

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
