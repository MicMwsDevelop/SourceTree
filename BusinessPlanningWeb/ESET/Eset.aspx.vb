Imports System.Data
Imports System.Data.SqlClient

Partial Class Eset_Eset
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

	End Sub

	Protected Sub button検索_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button検索.Click

		If textBox顧客No.Text.Length > 0 Then
			'ESET月額版
			Dim sqlStr1 As String
			sqlStr1 = "SELECT CASE E.LICENSE_CNT WHEN 1 THEN '1ライセンス' WHEN 3 THEN '3ライセンス' ELSE '5ライセンス' END as サービス名"
			sqlStr1 = sqlStr1 & ", E.SERIAL as シリアル番号, E.LICENSE_USER_NAME as ユーザー名, E.LICENSE_PASSWORD as パスワード, E.LICENSE_KEY as 製品認証キー"
			sqlStr1 = sqlStr1 & ", convert(nvarchar, E.START_DATE, 111) as 利用開始日時, iif(E.END_DATE is null, '', convert(nvarchar, E.END_DATE, 111)) as 利用終了日時"
			sqlStr1 = sqlStr1 & ", iif(E.APPLY_STATUS = '0', '利用中', iif(E.APPLY_STATUS = '1' AND convert(nvarchar, E.END_DATE, 112) >= convert(nvarchar, getdate(), 112), '解約申込受付中', '解約済み')) as 状態"
			sqlStr1 = sqlStr1 & " FROM charlieDB.dbo.T_LICENSE_PRODUCT_CONTRACT as E"
			sqlStr1 = sqlStr1 & " LEFT JOIN JunpDB.dbo.vMicユーザー基本2 as U on E.CUSTOMER_ID = U.顧客No"
			sqlStr1 = sqlStr1 & " WHERE U.終了フラグ = 0 AND E.RIYO_CANCEL_FLG = 0 AND E.KAIYAKU_CANCEL_FLG = 0"
			sqlStr1 = sqlStr1 & " AND 顧客No = " & textBox顧客No.Text
			sqlStr1 = sqlStr1 & " ORDER BY E.LICENSE_CNT ASC, E.REQUEST_NO ASC"

			SqlDataSourceMonthly.ConnectionString = "server='SQLSV'; user id='ww_reader'; password='20150801'; database='charlieDB'"
			SqlDataSourceMonthly.SelectCommand = sqlStr1

			'ページは初期値
			GridViewMonthly.PageIndex = 0

			'ESET6年パック
			Dim sqlStr2 As String
			sqlStr2 = "SELECT D.f受注番号 as 受注番号, CASE H.f販売種別 WHEN 1 THEN 'VP' WHEN 2 THEN 'UG' WHEN 3 THEN '月額' WHEN 4 THEN 'まとめ' WHEN 5 THEN 'PC安心' ELSE 'その他' END as 販売種別"
			sqlStr2 = sqlStr2 & ", convert(int, H.f受注金額) as 金額, D.f数量 as 数量, convert(nvarchar, H.f売上承認日, 111) as 売上承認日, H.fSV利用開始年月 as サービス利用開始"
			sqlStr2 = sqlStr2 & ", H.fSV利用終了年月 as サービス利用終了, H.f備考 as 備考, H.f件名 as 件名"
			sqlStr2 = sqlStr2 & " FROM JunpDB.dbo.tMih受注詳細 as D"
			sqlStr2 = sqlStr2 & " LEFT JOIN JunpDB.dbo.tMih受注ヘッダ as H ON D.f受注番号 = H.f受注番号"
			sqlStr2 = sqlStr2 & " WHERE D.f商品コード = '014031' AND H.fユーザーコード = " & textBox顧客No.Text & " ORDER BY D.f受注番号"

			SqlDataSourcePack.ConnectionString = "server='SQLSV'; user id='ww_reader'; password='20150801'; database='junpDB'"
			SqlDataSourcePack.SelectCommand = sqlStr2

			'ページは初期値
			GridViewPack.PageIndex = 0

			'顧客名の設定
			Dim sqlStr3 As String
			sqlStr3 = "SELECT TOP 1 顧客名１ + 顧客名２ as 顧客名 FROM JunpDB.dbo.vMicユーザー基本2 WHERE 顧客No = " & textBox顧客No.Text
			Dim con As New SqlConnection("server='SQLSV'; user id='ww_reader'; password='20150801'; database='junpDB'")
			Dim cmd As New SqlCommand(sqlStr3, con)
			cmd.CommandType = CommandType.Text
			con.Open()
			Dim i As Object = cmd.ExecuteScalar()
			textBox顧客名.Text = If(i IsNot Nothing, i.ToString(), String.Empty)
		Else
			'ESET月額版のクリア
			GridViewMonthly.DataSource = Nothing
			GridViewMonthly.DataBind()

			'ESET6年パックのクリア
			GridViewPack.DataSource = Nothing
			GridViewPack.DataBind()

			'顧客名のクリア
			textBox顧客名.Text = String.Empty
		End If

	End Sub

End Class
