Partial Class SaleOnlineQualifyList_SaleOnlineQualifyList
	Inherits System.Web.UI.Page

	'*****************************************************************
	' オンライン資格確認売上一覧
	'
	'Ver1.01(2023/02/07 勝呂):オンライン資格確認売上一覧 集計単位が部単位で集計できていない
	'*****************************************************************
	Dim totalPrice東日本営業部 As Decimal = 0
	Dim totalCount東日本営業部 As Integer = 0
	Dim totalPrice西日本営業部 As Decimal = 0
	Dim totalCount西日本営業部 As Integer = 0
	Dim totalPrice東日本サポートセンター As Decimal = 0
	Dim totalCount東日本サポートセンター As Integer = 0
	Dim totalPrice首都圏サポートセンター As Decimal = 0
	Dim totalCount首都圏サポートセンター As Integer = 0
	Dim totalPrice中日本サポートセンター As Decimal = 0
	Dim totalCount中日本サポートセンター As Integer = 0
	Dim totalPrice関西サポートセンター As Decimal = 0
	Dim totalCount関西サポートセンター As Integer = 0
	Dim totalPrice西日本サポートセンター As Decimal = 0
	Dim totalCount西日本サポートセンター As Integer = 0

	Dim totalPrice(46) As Decimal
	Dim totalCount(46) As Integer

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If False = Me.IsPostBack Then
			'最初の１回のみ
			'集計期間 今期初日～今期末日
			Dim dtStart As DateTime
			Dim dtEnd As DateTime
			If DateTime.Today.Month < 8 Then
				dtStart = New DateTime(DateTime.Today.Year - 1, 8, 1)
				dtEnd = New DateTime(DateTime.Today.Year, 7, 31)
			Else
				dtStart = New DateTime(DateTime.Today.Year, 8, 1)
				dtEnd = New DateTime(DateTime.Today.Year + 1, 7, 31)
			End If
			textBox開始日.Text = dtStart.ToString("yyyy/MM/dd")
			textBox終了日.Text = dtEnd.ToString("yyyy/MM/dd")

			'検索商品リストボックスの設定
			Dim connStr As String = "server='SQLSV'; user id='ww_reader'; password='20150801'; database='JunpDB'"
			Dim sqlStr As String = "SELECT RTRIM([sms_scd]) as sms_scd, RTRIM([sms_scd]) + '   ' + [sms_mei] as 商品名 FROM [JunpDB].[dbo].[vMicPCA商品マスタ] WHERE sms_skbn2 = '50' ORDER BY sms_scd ASC"
			SqlDataSourceGoods.ConnectionString = connStr
			SqlDataSourceGoods.SelectCommand = sqlStr
		End If
	End Sub

	Protected Sub VisibleControl(ken As Boolean)
		If ken = True Then
			'都道府県
			label01.Visible = True
			label02.Visible = True
			label03.Visible = True
			label04.Visible = True
			label05.Visible = True
			label06.Visible = True
			label07.Visible = True
			label08.Visible = True
			label09.Visible = True
			label10.Visible = True
			label11.Visible = True
			label12.Visible = True
			label13.Visible = True
			label14.Visible = True
			label15.Visible = True
			label16.Visible = True
			label17.Visible = True
			label18.Visible = True
			label19.Visible = True
			label20.Visible = True
			label21.Visible = True
			label22.Visible = True
			label23.Visible = True
			label24.Visible = True
			label25.Visible = True
			label26.Visible = True
			label27.Visible = True
			label28.Visible = True
			label29.Visible = True
			label30.Visible = True
			label31.Visible = True
			label32.Visible = True
			label33.Visible = True
			label34.Visible = True
			label35.Visible = True
			label36.Visible = True
			label37.Visible = True
			label38.Visible = True
			label39.Visible = True
			label40.Visible = True
			label41.Visible = True
			label42.Visible = True
			label43.Visible = True
			label44.Visible = True
			label45.Visible = True
			label46.Visible = True
			label47.Visible = True
			GridView01.Visible = True
			GridView02.Visible = True
			GridView03.Visible = True
			GridView04.Visible = True
			GridView05.Visible = True
			GridView06.Visible = True
			GridView07.Visible = True
			GridView08.Visible = True
			GridView09.Visible = True
			GridView10.Visible = True
			GridView11.Visible = True
			GridView12.Visible = True
			GridView13.Visible = True
			GridView14.Visible = True
			GridView15.Visible = True
			GridView16.Visible = True
			GridView17.Visible = True
			GridView18.Visible = True
			GridView19.Visible = True
			GridView20.Visible = True
			GridView21.Visible = True
			GridView22.Visible = True
			GridView23.Visible = True
			GridView24.Visible = True
			GridView25.Visible = True
			GridView26.Visible = True
			GridView27.Visible = True
			GridView28.Visible = True
			GridView29.Visible = True
			GridView30.Visible = True
			GridView31.Visible = True
			GridView32.Visible = True
			GridView33.Visible = True
			GridView34.Visible = True
			GridView35.Visible = True
			GridView36.Visible = True
			GridView37.Visible = True
			GridView38.Visible = True
			GridView39.Visible = True
			GridView40.Visible = True
			GridView41.Visible = True
			GridView42.Visible = True
			GridView43.Visible = True
			GridView44.Visible = True
			GridView45.Visible = True
			GridView46.Visible = True
			GridView47.Visible = True

			GridView東日本営業部.Visible = False
			GridView西日本営業部.Visible = False
			GridView東日本サポートセンター.Visible = False
			GridView首都圏サポートセンター.Visible = False
			GridView中日本サポートセンター.Visible = False
			GridView関西サポートセンター.Visible = False
			GridView西日本サポートセンター.Visible = False
			label東日本営業部.Visible = False
			label西日本営業部.Visible = False
			label東日本サポートセンター.Visible = False
			label首都圏サポートセンター.Visible = False
			label中日本サポートセンター.Visible = False
			label関西サポートセンター.Visible = False
			label西日本サポートセンター.Visible = False
		Else
			'部
			label01.Visible = False
			label02.Visible = False
			label03.Visible = False
			label04.Visible = False
			label05.Visible = False
			label06.Visible = False
			label07.Visible = False
			label08.Visible = False
			label09.Visible = False
			label10.Visible = False
			label11.Visible = False
			label12.Visible = False
			label13.Visible = False
			label14.Visible = False
			label15.Visible = False
			label16.Visible = False
			label17.Visible = False
			label18.Visible = False
			label19.Visible = False
			label20.Visible = False
			label21.Visible = False
			label22.Visible = False
			label23.Visible = False
			label24.Visible = False
			label25.Visible = False
			label26.Visible = False
			label27.Visible = False
			label28.Visible = False
			label29.Visible = False
			label30.Visible = False
			label31.Visible = False
			label32.Visible = False
			label33.Visible = False
			label34.Visible = False
			label35.Visible = False
			label36.Visible = False
			label37.Visible = False
			label38.Visible = False
			label39.Visible = False
			label40.Visible = False
			label41.Visible = False
			label42.Visible = False
			label43.Visible = False
			label44.Visible = False
			label45.Visible = False
			label46.Visible = False
			label47.Visible = False
			GridView01.Visible = False
			GridView02.Visible = False
			GridView03.Visible = False
			GridView04.Visible = False
			GridView05.Visible = False
			GridView06.Visible = False
			GridView07.Visible = False
			GridView08.Visible = False
			GridView09.Visible = False
			GridView10.Visible = False
			GridView11.Visible = False
			GridView12.Visible = False
			GridView13.Visible = False
			GridView14.Visible = False
			GridView15.Visible = False
			GridView16.Visible = False
			GridView17.Visible = False
			GridView18.Visible = False
			GridView19.Visible = False
			GridView20.Visible = False
			GridView21.Visible = False
			GridView22.Visible = False
			GridView23.Visible = False
			GridView24.Visible = False
			GridView25.Visible = False
			GridView26.Visible = False
			GridView27.Visible = False
			GridView28.Visible = False
			GridView29.Visible = False
			GridView30.Visible = False
			GridView31.Visible = False
			GridView32.Visible = False
			GridView33.Visible = False
			GridView34.Visible = False
			GridView35.Visible = False
			GridView36.Visible = False
			GridView37.Visible = False
			GridView38.Visible = False
			GridView39.Visible = False
			GridView40.Visible = False
			GridView41.Visible = False
			GridView42.Visible = False
			GridView43.Visible = False
			GridView44.Visible = False
			GridView45.Visible = False
			GridView46.Visible = False
			GridView47.Visible = False

			GridView東日本営業部.Visible = True
			GridView西日本営業部.Visible = True
			GridView東日本サポートセンター.Visible = True
			GridView首都圏サポートセンター.Visible = True
			GridView中日本サポートセンター.Visible = True
			GridView関西サポートセンター.Visible = True
			GridView西日本サポートセンター.Visible = True
			label東日本営業部.Visible = True
			label西日本営業部.Visible = True
			label東日本サポートセンター.Visible = True
			label首都圏サポートセンター.Visible = True
			label中日本サポートセンター.Visible = True
			label関西サポートセンター.Visible = True
			label西日本サポートセンター.Visible = True
		End If
	End Sub

	Protected Sub button集計実行_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button集計実行.Click

		Dim connStr As String = "server='SQLSV'; user id='ww_reader'; password='20150801'; database='JunpDB'"
		Dim selectStr As String = "SELECT [売上日],[伝票No],[営業部コード],[営業部名],[県番号],[都道府県名],[顧客No],[得意先No],[顧客名],[商品コード],[商品名],[数量],[金額],[売上日]/100 as 売上月 FROM [JunpDB].[dbo].[vMicオンライン資格確認売上集計]"
		Dim whereStr As String = " WHERE "
		Dim termStr As String = "[売上日] BETWEEN CONVERT(int, REPLACE('" & textBox開始日.Text & "', '/', '')) AND CONVERT(int, REPLACE('" & textBox終了日.Text & "', '/', ''))"
		Dim i As Integer
		Dim sqlStr As String

		'検索商品リストで選択済みの商品から条件式を作成
		For i = 0 To GoodsListBox.Items.Count - 1
			If GoodsListBox.Items(i).Selected Then
				If whereStr = " WHERE " Then
					whereStr = whereStr & "[商品コード] In ('" & GoodsListBox.Items(i).Value & "'"
				Else
					whereStr = whereStr & ",'" & GoodsListBox.Items(i).Value & "'"
				End If
			End If
		Next
		If whereStr <> " WHERE " Then
			whereStr = whereStr & ") AND "
		End If

		'Ver1.01(2023/02/07 勝呂):オンライン資格確認売上一覧 集計単位が部単位で集計できていない
		'If dropList集計単位.Text = "部" Then
		If dropList集計単位.SelectedIndex = 0 Then
			'部単位
			VisibleControl(False)

			totalPrice東日本営業部 = 0
			totalCount東日本営業部 = 0
			totalPrice西日本営業部 = 0
			totalCount西日本営業部 = 0
			totalPrice東日本サポートセンター = 0
			totalCount東日本サポートセンター = 0
			totalPrice首都圏サポートセンター = 0
			totalCount首都圏サポートセンター = 0
			totalPrice中日本サポートセンター = 0
			totalCount中日本サポートセンター = 0
			totalPrice関西サポートセンター = 0
			totalCount関西サポートセンター = 0
			totalPrice西日本サポートセンター = 0
			totalCount西日本サポートセンター = 0

			sqlStr = selectStr & whereStr & termStr & " AND [営業部コード] = '045' ORDER BY [県番号] ASC, [顧客No] ASC"
			SqlDataSource東日本営業部.ConnectionString = connStr
			SqlDataSource東日本営業部.SelectCommand = sqlStr
			GridView東日本営業部.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [営業部コード] = '046' ORDER BY [県番号] ASC, [顧客No] ASC"
			SqlDataSource西日本営業部.ConnectionString = connStr
			SqlDataSource西日本営業部.SelectCommand = sqlStr
			GridView西日本営業部.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [営業部コード] = '053' ORDER BY [県番号] ASC, [顧客No] ASC"
			SqlDataSource東日本サポートセンター.ConnectionString = connStr
			SqlDataSource東日本サポートセンター.SelectCommand = sqlStr
			GridView東日本サポートセンター.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [営業部コード] = '054' ORDER BY [県番号] ASC, [顧客No] ASC"
			SqlDataSource首都圏サポートセンター.ConnectionString = connStr
			SqlDataSource首都圏サポートセンター.SelectCommand = sqlStr
			GridView首都圏サポートセンター.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [営業部コード] = '055' ORDER BY [県番号] ASC, [顧客No] ASC"
			SqlDataSource中日本サポートセンター.ConnectionString = connStr
			SqlDataSource中日本サポートセンター.SelectCommand = sqlStr
			GridView中日本サポートセンター.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [営業部コード] = '057' ORDER BY [県番号] ASC, [顧客No] ASC"
			SqlDataSource関西サポートセンター.ConnectionString = connStr
			SqlDataSource関西サポートセンター.SelectCommand = sqlStr
			GridView関西サポートセンター.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [営業部コード] = '058' ORDER BY [県番号] ASC, [顧客No] ASC"
			SqlDataSource西日本サポートセンター.ConnectionString = connStr
			SqlDataSource西日本サポートセンター.SelectCommand = sqlStr
			GridView西日本サポートセンター.PageIndex = 0
		Else
			VisibleControl(True)

			For i = 0 To 46
				totalPrice(i) = 0
				totalCount(i) = 0
			Next

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '01' ORDER BY [顧客No] ASC"
			SqlDataSource01.ConnectionString = connStr
			SqlDataSource01.SelectCommand = sqlStr
			GridView01.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '02' ORDER BY [顧客No] ASC"
			SqlDataSource02.ConnectionString = connStr
			SqlDataSource02.SelectCommand = sqlStr
			GridView02.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '03' ORDER BY [顧客No] ASC"
			SqlDataSource03.ConnectionString = connStr
			SqlDataSource03.SelectCommand = sqlStr
			GridView03.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '04' ORDER BY [顧客No] ASC"
			SqlDataSource04.ConnectionString = connStr
			SqlDataSource04.SelectCommand = sqlStr
			GridView04.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '05' ORDER BY [顧客No] ASC"
			SqlDataSource05.ConnectionString = connStr
			SqlDataSource05.SelectCommand = sqlStr
			GridView05.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '06' ORDER BY [顧客No] ASC"
			SqlDataSource06.ConnectionString = connStr
			SqlDataSource06.SelectCommand = sqlStr
			GridView06.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '07' ORDER BY [顧客No] ASC"
			SqlDataSource07.ConnectionString = connStr
			SqlDataSource07.SelectCommand = sqlStr
			GridView07.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '08' ORDER BY [顧客No] ASC"
			SqlDataSource08.ConnectionString = connStr
			SqlDataSource08.SelectCommand = sqlStr
			GridView08.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '09' ORDER BY [顧客No] ASC"
			SqlDataSource09.ConnectionString = connStr
			SqlDataSource09.SelectCommand = sqlStr
			GridView09.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '10' ORDER BY [顧客No] ASC"
			SqlDataSource10.ConnectionString = connStr
			SqlDataSource10.SelectCommand = sqlStr
			GridView10.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '11' ORDER BY [顧客No] ASC"
			SqlDataSource11.ConnectionString = connStr
			SqlDataSource11.SelectCommand = sqlStr
			GridView11.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '12' ORDER BY [顧客No] ASC"
			SqlDataSource12.ConnectionString = connStr
			SqlDataSource12.SelectCommand = sqlStr
			GridView12.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '13' ORDER BY [顧客No] ASC"
			SqlDataSource13.ConnectionString = connStr
			SqlDataSource13.SelectCommand = sqlStr
			GridView13.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '14' ORDER BY [顧客No] ASC"
			SqlDataSource14.ConnectionString = connStr
			SqlDataSource14.SelectCommand = sqlStr
			GridView14.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '15' ORDER BY [顧客No] ASC"
			SqlDataSource15.ConnectionString = connStr
			SqlDataSource15.SelectCommand = sqlStr
			GridView15.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '16' ORDER BY [顧客No] ASC"
			SqlDataSource16.ConnectionString = connStr
			SqlDataSource16.SelectCommand = sqlStr
			GridView16.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '17' ORDER BY [顧客No] ASC"
			SqlDataSource17.ConnectionString = connStr
			SqlDataSource17.SelectCommand = sqlStr
			GridView17.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '18' ORDER BY [顧客No] ASC"
			SqlDataSource18.ConnectionString = connStr
			SqlDataSource18.SelectCommand = sqlStr
			GridView18.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '19' ORDER BY [顧客No] ASC"
			SqlDataSource19.ConnectionString = connStr
			SqlDataSource19.SelectCommand = sqlStr
			GridView19.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '20' ORDER BY [顧客No] ASC"
			SqlDataSource20.ConnectionString = connStr
			SqlDataSource20.SelectCommand = sqlStr
			GridView20.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '21' ORDER BY [顧客No] ASC"
			SqlDataSource21.ConnectionString = connStr
			SqlDataSource21.SelectCommand = sqlStr
			GridView21.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '22' ORDER BY [顧客No] ASC"
			SqlDataSource22.ConnectionString = connStr
			SqlDataSource22.SelectCommand = sqlStr
			GridView22.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '23' ORDER BY [顧客No] ASC"
			SqlDataSource23.ConnectionString = connStr
			SqlDataSource23.SelectCommand = sqlStr
			GridView23.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '24' ORDER BY [顧客No] ASC"
			SqlDataSource24.ConnectionString = connStr
			SqlDataSource24.SelectCommand = sqlStr
			GridView24.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '25' ORDER BY [顧客No] ASC"
			SqlDataSource25.ConnectionString = connStr
			SqlDataSource25.SelectCommand = sqlStr
			GridView25.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '26' ORDER BY [顧客No] ASC"
			SqlDataSource26.ConnectionString = connStr
			SqlDataSource26.SelectCommand = sqlStr
			GridView26.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '27' ORDER BY [顧客No] ASC"
			SqlDataSource27.ConnectionString = connStr
			SqlDataSource27.SelectCommand = sqlStr
			GridView27.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '28' ORDER BY [顧客No] ASC"
			SqlDataSource28.ConnectionString = connStr
			SqlDataSource28.SelectCommand = sqlStr
			GridView28.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '29' ORDER BY [顧客No] ASC"
			SqlDataSource29.ConnectionString = connStr
			SqlDataSource29.SelectCommand = sqlStr
			GridView29.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '30' ORDER BY [顧客No] ASC"
			SqlDataSource30.ConnectionString = connStr
			SqlDataSource30.SelectCommand = sqlStr
			GridView30.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '31' ORDER BY [顧客No] ASC"
			SqlDataSource31.ConnectionString = connStr
			SqlDataSource31.SelectCommand = sqlStr
			GridView31.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '32' ORDER BY [顧客No] ASC"
			SqlDataSource32.ConnectionString = connStr
			SqlDataSource32.SelectCommand = sqlStr
			GridView32.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '33' ORDER BY [顧客No] ASC"
			SqlDataSource33.ConnectionString = connStr
			SqlDataSource33.SelectCommand = sqlStr
			GridView33.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '34' ORDER BY [顧客No] ASC"
			SqlDataSource34.ConnectionString = connStr
			SqlDataSource34.SelectCommand = sqlStr
			GridView34.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '35' ORDER BY [顧客No] ASC"
			SqlDataSource35.ConnectionString = connStr
			SqlDataSource35.SelectCommand = sqlStr
			GridView35.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '36' ORDER BY [顧客No] ASC"
			SqlDataSource36.ConnectionString = connStr
			SqlDataSource36.SelectCommand = sqlStr
			GridView36.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '37' ORDER BY [顧客No] ASC"
			SqlDataSource37.ConnectionString = connStr
			SqlDataSource37.SelectCommand = sqlStr
			GridView37.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '38' ORDER BY [顧客No] ASC"
			SqlDataSource38.ConnectionString = connStr
			SqlDataSource38.SelectCommand = sqlStr
			GridView38.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '39' ORDER BY [顧客No] ASC"
			SqlDataSource39.ConnectionString = connStr
			SqlDataSource39.SelectCommand = sqlStr
			GridView39.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '40' ORDER BY [顧客No] ASC"
			SqlDataSource40.ConnectionString = connStr
			SqlDataSource40.SelectCommand = sqlStr
			GridView40.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '41' ORDER BY [顧客No] ASC"
			SqlDataSource41.ConnectionString = connStr
			SqlDataSource41.SelectCommand = sqlStr
			GridView41.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '42' ORDER BY [顧客No] ASC"
			SqlDataSource42.ConnectionString = connStr
			SqlDataSource42.SelectCommand = sqlStr
			GridView42.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '43' ORDER BY [顧客No] ASC"
			SqlDataSource43.ConnectionString = connStr
			SqlDataSource43.SelectCommand = sqlStr
			GridView43.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '44' ORDER BY [顧客No] ASC"
			SqlDataSource44.ConnectionString = connStr
			SqlDataSource44.SelectCommand = sqlStr
			GridView44.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '45' ORDER BY [顧客No] ASC"
			SqlDataSource45.ConnectionString = connStr
			SqlDataSource45.SelectCommand = sqlStr
			GridView45.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '46' ORDER BY [顧客No] ASC"
			SqlDataSource46.ConnectionString = connStr
			SqlDataSource46.SelectCommand = sqlStr
			GridView46.PageIndex = 0

			sqlStr = selectStr & whereStr & termStr & " AND [県番号] = '47' ORDER BY [顧客No] ASC"
			SqlDataSource47.ConnectionString = connStr
			SqlDataSource47.SelectCommand = sqlStr
			GridView47.PageIndex = 0
		End If
	End Sub

	Protected Sub buttonOutputExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonOutputExcel.Click
		Dim tw As New System.IO.StringWriter()
		Dim hw As New System.Web.UI.HtmlTextWriter(tw)
		Dim frm As HtmlForm = New HtmlForm()

		Response.ContentType = "application/vnd.ms-excel"
		Response.AddHeader("content-disposition", "attachment;filename=SaleOnlineQualifyList.xls")
		Response.Charset = ""
		Page.EnableViewState = False
		Controls.Add(frm)

		frm.Controls.Add(label東日本営業部)
		frm.Controls.Add(GridView東日本営業部)
		frm.Controls.Add(label西日本営業部)
		frm.Controls.Add(GridView西日本営業部)
		frm.Controls.Add(label東日本サポートセンター)
		frm.Controls.Add(GridView東日本サポートセンター)
		frm.Controls.Add(label首都圏サポートセンター)
		frm.Controls.Add(GridView首都圏サポートセンター)
		frm.Controls.Add(label中日本サポートセンター)
		frm.Controls.Add(GridView中日本サポートセンター)
		frm.Controls.Add(label関西サポートセンター)
		frm.Controls.Add(GridView関西サポートセンター)
		frm.Controls.Add(label西日本サポートセンター)
		frm.Controls.Add(GridView西日本サポートセンター)

		frm.Controls.Add(label01)
		frm.Controls.Add(GridView01)
		frm.Controls.Add(label02)
		frm.Controls.Add(GridView02)
		frm.Controls.Add(label03)
		frm.Controls.Add(GridView03)
		frm.Controls.Add(label04)
		frm.Controls.Add(GridView04)
		frm.Controls.Add(label05)
		frm.Controls.Add(GridView05)
		frm.Controls.Add(label06)
		frm.Controls.Add(GridView06)
		frm.Controls.Add(label07)
		frm.Controls.Add(GridView07)
		frm.Controls.Add(label08)
		frm.Controls.Add(GridView08)
		frm.Controls.Add(label09)
		frm.Controls.Add(GridView09)
		frm.Controls.Add(label10)
		frm.Controls.Add(GridView10)
		frm.Controls.Add(label11)
		frm.Controls.Add(GridView11)
		frm.Controls.Add(label12)
		frm.Controls.Add(GridView12)
		frm.Controls.Add(label13)
		frm.Controls.Add(GridView13)
		frm.Controls.Add(label14)
		frm.Controls.Add(GridView14)
		frm.Controls.Add(label15)
		frm.Controls.Add(GridView15)
		frm.Controls.Add(label16)
		frm.Controls.Add(GridView16)
		frm.Controls.Add(label17)
		frm.Controls.Add(GridView17)
		frm.Controls.Add(label18)
		frm.Controls.Add(GridView18)
		frm.Controls.Add(label19)
		frm.Controls.Add(GridView19)
		frm.Controls.Add(label20)
		frm.Controls.Add(GridView20)
		frm.Controls.Add(label21)
		frm.Controls.Add(GridView21)
		frm.Controls.Add(label22)
		frm.Controls.Add(GridView22)
		frm.Controls.Add(label23)
		frm.Controls.Add(GridView23)
		frm.Controls.Add(label24)
		frm.Controls.Add(GridView24)
		frm.Controls.Add(label25)
		frm.Controls.Add(GridView25)
		frm.Controls.Add(label26)
		frm.Controls.Add(GridView26)
		frm.Controls.Add(label27)
		frm.Controls.Add(GridView27)
		frm.Controls.Add(label28)
		frm.Controls.Add(GridView28)
		frm.Controls.Add(label29)
		frm.Controls.Add(GridView29)
		frm.Controls.Add(label30)
		frm.Controls.Add(GridView30)
		frm.Controls.Add(label31)
		frm.Controls.Add(GridView31)
		frm.Controls.Add(label32)
		frm.Controls.Add(GridView32)
		frm.Controls.Add(label33)
		frm.Controls.Add(GridView33)
		frm.Controls.Add(label34)
		frm.Controls.Add(GridView34)
		frm.Controls.Add(label35)
		frm.Controls.Add(GridView35)
		frm.Controls.Add(label36)
		frm.Controls.Add(GridView36)
		frm.Controls.Add(label37)
		frm.Controls.Add(GridView37)
		frm.Controls.Add(label38)
		frm.Controls.Add(GridView38)
		frm.Controls.Add(label39)
		frm.Controls.Add(GridView39)
		frm.Controls.Add(label40)
		frm.Controls.Add(GridView40)
		frm.Controls.Add(label41)
		frm.Controls.Add(GridView41)
		frm.Controls.Add(label42)
		frm.Controls.Add(GridView42)
		frm.Controls.Add(label43)
		frm.Controls.Add(GridView43)
		frm.Controls.Add(label44)
		frm.Controls.Add(GridView44)
		frm.Controls.Add(label45)
		frm.Controls.Add(GridView45)
		frm.Controls.Add(label46)
		frm.Controls.Add(GridView46)
		frm.Controls.Add(label47)
		frm.Controls.Add(GridView47)
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

	Protected Sub GridView東日本営業部_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView東日本営業部.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount東日本営業部 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice東日本営業部 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount東日本営業部.ToString("G")
			e.Row.Cells(6).Text = totalPrice東日本営業部.ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView西日本営業部_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView西日本営業部.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount西日本営業部 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice西日本営業部 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount西日本営業部.ToString("G")
			e.Row.Cells(6).Text = totalPrice西日本営業部.ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView東日本サポートセンター_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView東日本サポートセンター.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount東日本サポートセンター += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice東日本サポートセンター += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount東日本サポートセンター.ToString("G")
			e.Row.Cells(6).Text = totalPrice東日本サポートセンター.ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView首都圏サポートセンター_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView首都圏サポートセンター.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount首都圏サポートセンター += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice首都圏サポートセンター += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount首都圏サポートセンター.ToString("G")
			e.Row.Cells(6).Text = totalPrice首都圏サポートセンター.ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView中日本サポートセンター_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView中日本サポートセンター.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount中日本サポートセンター += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice中日本サポートセンター += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount中日本サポートセンター.ToString("G")
			e.Row.Cells(6).Text = totalPrice中日本サポートセンター.ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView関西サポートセンター_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView関西サポートセンター.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount関西サポートセンター += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice関西サポートセンター += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount関西サポートセンター.ToString("G")
			e.Row.Cells(6).Text = totalPrice関西サポートセンター.ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView西日本サポートセンター_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView西日本サポートセンター.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount西日本サポートセンター += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice西日本サポートセンター += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount西日本サポートセンター.ToString("G")
			e.Row.Cells(6).Text = totalPrice西日本サポートセンター.ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView01_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView01.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(0) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(0) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(0).ToString("G")
			e.Row.Cells(6).Text = totalPrice(0).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView02_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView02.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(1) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(1) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(1).ToString("G")
			e.Row.Cells(6).Text = totalPrice(1).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView03_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView03.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(2) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(2) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(2).ToString("G")
			e.Row.Cells(6).Text = totalPrice(2).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView04_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView04.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(3) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(3) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(3).ToString("G")
			e.Row.Cells(6).Text = totalPrice(3).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView05_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView05.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(4) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(4) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(4).ToString("G")
			e.Row.Cells(6).Text = totalPrice(4).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView06_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView06.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(5) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(5) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(5).ToString("G")
			e.Row.Cells(6).Text = totalPrice(5).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView07_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView07.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(6) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(6) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(6).ToString("G")
			e.Row.Cells(6).Text = totalPrice(6).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView08_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView08.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(7) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(7) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(7).ToString("G")
			e.Row.Cells(6).Text = totalPrice(7).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView09_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView09.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(8) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(8) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(8).ToString("G")
			e.Row.Cells(6).Text = totalPrice(8).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView10_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView10.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(9) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(9) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(9).ToString("G")
			e.Row.Cells(6).Text = totalPrice(9).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView11_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView11.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(10) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(10) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(10).ToString("G")
			e.Row.Cells(6).Text = totalPrice(10).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView12_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView12.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(11) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(11) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(11).ToString("G")
			e.Row.Cells(6).Text = totalPrice(11).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView13_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView13.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(12) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(12) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(12).ToString("G")
			e.Row.Cells(6).Text = totalPrice(12).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView14_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView14.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(13) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(13) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(13).ToString("G")
			e.Row.Cells(6).Text = totalPrice(13).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView15_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView15.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(14) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(14) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(14).ToString("G")
			e.Row.Cells(6).Text = totalPrice(14).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView16_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView16.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(15) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(15) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(15).ToString("G")
			e.Row.Cells(6).Text = totalPrice(15).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView17_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView17.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(16) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(16) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(16).ToString("G")
			e.Row.Cells(6).Text = totalPrice(16).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView18_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView18.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(17) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(17) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(17).ToString("G")
			e.Row.Cells(6).Text = totalPrice(17).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView19_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView19.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(18) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(18) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(18).ToString("G")
			e.Row.Cells(6).Text = totalPrice(18).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView20_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView20.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(19) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(19) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(19).ToString("G")
			e.Row.Cells(6).Text = totalPrice(19).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView21_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView21.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(20) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(20) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(20).ToString("G")
			e.Row.Cells(6).Text = totalPrice(20).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView22_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView22.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(21) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(21) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(21).ToString("G")
			e.Row.Cells(6).Text = totalPrice(21).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView23_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView23.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(22) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(22) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(22).ToString("G")
			e.Row.Cells(6).Text = totalPrice(22).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView24_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView24.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(23) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(23) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(23).ToString("G")
			e.Row.Cells(6).Text = totalPrice(23).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView25_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView25.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(24) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(24) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(24).ToString("G")
			e.Row.Cells(6).Text = totalPrice(24).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView26_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView26.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(25) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(25) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(25).ToString("G")
			e.Row.Cells(6).Text = totalPrice(25).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView27_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView27.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(26) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(26) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(26).ToString("G")
			e.Row.Cells(6).Text = totalPrice(26).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView28_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView28.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(27) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(27) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(27).ToString("G")
			e.Row.Cells(6).Text = totalPrice(27).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView29_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView29.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(28) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(28) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(28).ToString("G")
			e.Row.Cells(6).Text = totalPrice(28).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView30_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView30.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(29) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(29) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(29).ToString("G")
			e.Row.Cells(6).Text = totalPrice(29).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView31_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView31.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(30) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(30) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(30).ToString("G")
			e.Row.Cells(6).Text = totalPrice(30).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView32_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView32.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(31) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(31) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(31).ToString("G")
			e.Row.Cells(6).Text = totalPrice(31).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView33_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView33.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(32) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(32) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(32).ToString("G")
			e.Row.Cells(6).Text = totalPrice(32).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView34_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView34.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(33) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(33) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(33).ToString("G")
			e.Row.Cells(6).Text = totalPrice(33).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView35_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView35.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(34) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(34) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(34).ToString("G")
			e.Row.Cells(6).Text = totalPrice(34).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView36_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView36.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(35) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(35) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(35).ToString("G")
			e.Row.Cells(6).Text = totalPrice(35).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView37_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView37.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(36) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(36) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(36).ToString("G")
			e.Row.Cells(6).Text = totalPrice(36).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView38_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView38.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(37) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(37) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(37).ToString("G")
			e.Row.Cells(6).Text = totalPrice(37).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView39_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView39.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(38) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(38) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(38).ToString("G")
			e.Row.Cells(6).Text = totalPrice(38).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView40_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView40.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(39) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(39) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(39).ToString("G")
			e.Row.Cells(6).Text = totalPrice(39).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView41_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView41.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(40) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(40) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(40).ToString("G")
			e.Row.Cells(6).Text = totalPrice(40).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView42_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView42.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(41) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(41) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(41).ToString("G")
			e.Row.Cells(6).Text = totalPrice(41).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView43_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView43.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(42) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(42) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(42).ToString("G")
			e.Row.Cells(6).Text = totalPrice(42).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView44_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView44.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(43) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(43) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(43).ToString("G")
			e.Row.Cells(6).Text = totalPrice(43).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView45_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView45.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(44) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(44) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(44).ToString("G")
			e.Row.Cells(6).Text = totalPrice(44).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView46_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView46.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(45) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(45) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(45).ToString("G")
			e.Row.Cells(6).Text = totalPrice(45).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub

	Protected Sub GridView47_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView47.RowDataBound
		If e.Row.RowType = DataControlRowType.DataRow Then
			' totalCount および totalPrice をそれぞれの累計用変数に加算します
			totalCount(46) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "数量"))
			totalPrice(46) += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "金額"))
		ElseIf e.Row.RowType = DataControlRowType.Footer Then
			e.Row.Cells(0).Text = "合計："
			' フッターに、累計を表示します。
			e.Row.Cells(5).Text = totalCount(46).ToString("G")
			e.Row.Cells(6).Text = totalPrice(46).ToString("c")
			e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
			e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
			e.Row.Font.Bold = True
		End If
	End Sub
End Class
