
Partial Class AchievementES_AchievementES
    Inherits System.Web.UI.Page

    'Dim totalPrice As Decimal = 0
    Dim totalES As Integer = 0
    Dim totalKakin As Integer = 0
    Dim totalMatome As Integer = 0
    Dim totalAll As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

		If False = Me.IsPostBack Then
			'最初の１回のみ
			'集計期間 前月初日～当月末日
			Dim dtStart As DateTime
			Dim dtEnd As DateTime
			dtStart = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1)
			dtEnd = New DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month))
			textBox開始日.Text = dtStart.ToString("yyyy/MM/dd")
			textBox終了日.Text = dtEnd.ToString("yyyy/MM/dd")
		End If
    End Sub

    Protected Sub button集計実行_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button集計実行.Click

        Dim sqlstr As String

		'totalPrice = 0
		totalES = 0
		totalKakin = 0
		totalMatome = 0
		totalAll = 0

		'sqlstr = "SELECT"
		'sqlstr = sqlstr & " T.fBshCode2 AS 営業部コード"
		'sqlstr = sqlstr & " , T.fBshName2 AS 営業部名"
		'sqlstr = sqlstr & " , ES.拠点コード AS 拠点コード"
		'sqlstr = sqlstr & " , ES.拠点名 AS 拠点名"
		'sqlstr = sqlstr & " , ES.担当者コード AS 担当者コード"
		'sqlstr = sqlstr & " , ES.担当者名 AS 担当者名"
		'sqlstr = sqlstr & " , ES.売上金額 AS 売上金額"
		'sqlstr = sqlstr & " , ES.数量 as 数量"
		'sqlstr = sqlstr & " FROM JunpDB.dbo.vMih担当者 AS T"
		'sqlstr = sqlstr & " INNER JOIN ("
		'sqlstr = sqlstr & "  SELECT"
		'sqlstr = sqlstr & "    H.fBshCode3 AS 拠点コード"
		'sqlstr = sqlstr & "  , H.f担当支店名 AS 拠点名"
		'sqlstr = sqlstr & "  , H.f担当者コード AS 担当者コード"
		'sqlstr = sqlstr & "  , H.f担当者名 AS 担当者名"
		'sqlstr = sqlstr & "  , SUM(CONVERT(int, D.f提供価格)) AS 売上金額"
		'sqlstr = sqlstr & "  , SUM(D.f数量) as 数量"
		'sqlstr = sqlstr & "  FROM JunpDB.dbo.tMih受注詳細 AS D"
		'sqlstr = sqlstr & "  LEFT JOIN JunpDB.dbo.tMih受注ヘッダ AS H ON D.f年度 = H.f年度 AND D.f受注番号 = H.f受注番号"
		'sqlstr = sqlstr & "  WHERE H.f売上承認日 is not null AND D.f商品コード = '800121' AND CONVERT(nvarchar, H.f売上承認日, 111) BETWEEN '" & textBox開始日.Text & "' AND '" & textBox終了日.Text & "'"
		'sqlstr = sqlstr & "  GROUP BY H.fBshCode3, H.f担当支店名, H.f担当者コード, H.f担当者名"
		'sqlstr = sqlstr & " ) AS ES ON T.fUsrID = ES.担当者コード"
		'sqlstr = sqlstr & " WHERE T.fBshName2 LIKE '%" & dropListエリア.Text & "%'"
		'sqlstr = sqlstr & " ORDER BY 売上金額 DESC, 担当者コード ASC"
		
		'2022/02/01の組織変更に伴いSQLの修正(勝呂)
		'sqlstr = "SELECT"
		'sqlstr = sqlstr & " 営業部コード,営業部名,拠点コード,拠点名,担当者コード,担当者名,SUM(売上金額) AS 売上金額,SUM(数量) AS 数量"
		'sqlstr = sqlstr & " FROM JunpDB.dbo.vBPW_AchievementES"
		'sqlstr = sqlstr & " WHERE (営業部名 LIKE '%" & dropListエリア.Text & "%') AND (CONVERT(nvarchar, 売上承認日, 111) BETWEEN '" & textBox開始日.Text & "' AND '" & textBox終了日.Text & "')"
		'sqlstr = sqlstr & " GROUP BY 営業部コード,営業部名,拠点コード,拠点名,担当者コード,担当者名"
		'sqlstr = sqlstr & " ORDER BY 売上金額 DESC,担当者コード ASC"

		'2023/04/04 勝呂:営業部からの要望対応 paletteESの他に課金本数、まとめ本数の列の追加、売上金額の列の削除に対応
		sqlstr = "SELECT"
		sqlstr = sqlstr & " 営業部コード,営業部名,拠点コード,拠点名,担当者コード,担当者名,SUM(ES本数) as ES本数,SUM(課金本数) as 課金本数,SUM(まとめ本数) as まとめ本数,SUM(ES本数)+SUM(課金本数)+SUM(まとめ本数) as 合計本数"
		sqlstr = sqlstr & " FROM JunpDB.dbo.vBPW_AchievementES"
		sqlstr = sqlstr & " WHERE (営業部名 LIKE '%" & dropListエリア.Text & "%') AND (CONVERT(nvarchar, 売上承認日, 111) BETWEEN '" & textBox開始日.Text & "' AND '" & textBox終了日.Text & "')"
		sqlstr = sqlstr & " GROUP BY 営業部コード, 営業部名, 拠点コード, 拠点名, 担当者コード, 担当者名"
		sqlstr = sqlstr & " HAVING SUM(ES本数) > 0 OR SUM(課金本数) > 0 OR SUM(まとめ本数) > 0"
		sqlstr = sqlstr & " ORDER BY 合計本数 DESC, 拠点コード ASC, 担当者コード ASC"

        'データソースセット
        '実環境
        SqlDataSource.ConnectionString = "server='SQLSV'; user id='ww_reader'; password='20150801'; database='JunpDB'"
        SqlDataSource.SelectCommand = sqlstr

        'ページは初期値
        GridViewES.PageIndex = 0

    End Sub

    Protected Sub buttonOutputExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonOutputExcel.Click

        Dim tw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        Dim frm As HtmlForm = New HtmlForm()

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("content-disposition", "attachment;filename=paletteESList.xls")
        Response.Charset = ""
        Page.EnableViewState = False
        Controls.Add(frm)
        frm.Controls.Add(GridViewES)
        frm.RenderControl(hw)
        Response.Write(tw.ToString())

        Response.End()

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

    Protected Sub GridViewES_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewES.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            ' totalES および totalPrice をそれぞれの累計用変数に加算します
            '2023/04/04 勝呂:営業部からの要望対応 paletteESの他に課金本数、まとめ本数の列の追加、売上金額の列の削除に対応
            totalES += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ES本数"))
            totalKakin += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "課金本数"))
            totalMatome += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "まとめ本数"))
            totalAll += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "合計本数"))

            'totalPrice += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "売上金額"))

        ElseIf e.Row.RowType = DataControlRowType.Footer Then

            e.Row.Cells(0).Text = "合計："
            ' フッターに、累計を表示します。
            '2023/04/04 勝呂:営業部からの要望対応 paletteESの他に課金本数、まとめ本数の列の追加、売上金額の列の削除に対応
            e.Row.Cells(3).Text = totalES.ToString("G")
            e.Row.Cells(4).Text = totalKakin.ToString("G")
            e.Row.Cells(5).Text = totalMatome.ToString("G")
            e.Row.Cells(6).Text = totalAll.ToString("G")

            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Font.Bold = True
        End If

    End Sub
End Class
