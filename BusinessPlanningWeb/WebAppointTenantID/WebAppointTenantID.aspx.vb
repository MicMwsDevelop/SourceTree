Imports System.Data
Imports Microsoft.VisualBasic

Partial Class WebAppointTenantID_WebAppointTenantID
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Protected Sub button検索_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button検索.Click

        Dim sqlStr As String

		sqlStr = "SELECT 拠点コード, 拠点名, 顧客No, 顧客名, テナントID"
		sqlStr = sqlStr & " FROM charlieDB.dbo.V_TENANT_ID"
		If textBox顧客No.Text.Length > 0 Then
			sqlStr = sqlStr & " WHERE 顧客No = " & textBox顧客No.Text
		End If
		sqlStr = sqlStr & " ORDER BY 拠点コード ASC, 顧客No ASC"

        'データソースセット
        '実環境
        SqlDataSource.ConnectionString = "server='SQLSV'; user id='ww_reader'; password='20150801'; database='charlieDB'"
        SqlDataSource.SelectCommand = sqlStr

        'ページは初期値
        GridViewTenantID.PageIndex = 0

    End Sub

    'EXCEL出力
    Protected Sub buttonOutputExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonOutputExcel.Click

        Dim xlApp As Object = Nothing
        Dim xlBooks As Object = Nothing
        Dim xlBook As Object = Nothing
        Dim xlSheet As Object = Nothing
        Dim xlCells As Object = Nothing
        Dim xlRange As Object = Nothing
        Dim xlCellStart As Object = Nothing
        Dim xlCellEnd As Object = Nothing

        Try
            xlApp = CreateObject("Excel.Application")
            xlBooks = xlApp.Workbooks
            xlBook = xlApp.Workbooks.Add
            xlSheet = xlBook.WorkSheets(1)
            xlCells = xlSheet.Cells

            xlCells(1, 1).Value = "拠点名"
            xlCells(1, 2).Value = "顧客No"
            xlCells(1, 3).Value = "顧客名"
            xlCells(1, 4).Value = "テナントID"

            Dim dv As DataView
            dv = CType(SqlDataSource.Select(DataSourceSelectArguments.Empty), DataView)
            Dim row As DataRowView
            Dim i As Integer
            i = 2
            For Each row In dv
                xlCells(i, 1).Value = row(1).ToString
                xlCells(i, 2).Value = row(2).ToString
                xlCells(i, 3).Value = row(3).ToString
                xlCells(i, 4).Value = row(4).ToString
                i = i + 1
            Next
        Catch
            xlApp.DisplayAlerts = False
            xlApp.Quit()
            Throw
        Finally
            If xlCellStart IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCellStart)
            If xlCellEnd IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCellEnd)
            If xlRange IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(xlRange)
            If xlCells IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCells)
            If xlSheet IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet)
            If xlBooks IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBooks)
            If xlBook IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook)
            If xlApp IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp)

            GC.Collect()
        End Try





        'Dim tw As New System.IO.StringWriter()
        'Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        'Dim frm As HtmlForm = New HtmlForm()

        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        'Response.AddHeader("content-disposition", "attachment;filename=WebAppointTenantID_List.xlsx")
        'Response.Charset = ""
        'Page.EnableViewState = False
        'Controls.Add(frm)
        'frm.Controls.Add(GridViewTenantID)
        'frm.RenderControl(hw)
        'Response.Write(tw.ToString())

        'Response.End()

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
