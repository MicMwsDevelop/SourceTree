Imports System.Data
Imports System.Data.SqlClient

Partial Class ItemManagement_Default
    Inherits System.Web.UI.Page

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click


        Dim SQL As System.Data.SqlClient.SqlCommand
        Dim conn As New SqlConnection()


        conn.ConnectionString = "Data Source=DBSV;Initial Catalog = SalesDB; user id='sa'; password='07883510'"
        conn.Open()

        '現編集ロックがONの場合⇒編集ロックOFF

        '<<テーブル削除処理>>
        SQL = conn.CreateCommand
        'レコード追加
        SQL.CommandText = "INSERT INTO [ItemManagement] ([登録月],[顧客No],[医院名],[担当部署], [担当者], [ステータス], [地域], [販売店], [商材], [金額], [備考]) VALUES ('" & D_登録月.Text & "','" & T_顧客No.Text & "','" & T_医院名.Text & "', '" & D_担当者.Text & "', '" & D_担当者.Text & "', '" & D_ステータス.Text & "', '" & T_地域.Text & "', '" & T_販売店.Text & "', '" & D_商材.Text & "', '" & CInt(T_金額.Text) & "', '" & T_備考.Text & "');"


        'SQL実行
        SQL.ExecuteNonQuery()

        '解放
        SQL.Dispose()

        Response.Redirect("ItemManagement.aspx")

    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click

        Response.Redirect("ItemManagement.aspx")

    End Sub

    Protected Sub B_顧客呼出_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles B_顧客呼出.Click

        Response.Redirect("ItemManagementSelect.aspx")

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        T_顧客No.Text = Request.QueryString.Item("UserNo")

        If (T_顧客No.Text <> "") Then

            Dim strSQL As String

            strSQL = "SELECT * FROM vMicUser WHERE 顧客No ='" & T_顧客No.Text & "';"
            Dim cnn As SqlConnection = New SqlConnection("Data Source=DBSV;Initial Catalog = SalesDB; user id='sa'; password='07883510'")

            Dim sqlcmdSelect As SqlCommand = New SqlCommand(strSQL, cnn)
            sqlcmdSelect.CommandTimeout = 15

            Dim adpt = New SqlDataAdapter(sqlcmdSelect)
            Dim dsUser As DataSet = New DataSet
            Try
                adpt.Fill(dsUser)
            Catch ex As Exception
                cnn.Close()
                'lblError.Text = "データベースアクセスに失敗しました。"
                GoTo LBL_EXIT
            End Try

            'レコードにHIT　よって予算編成メンバーである
            If dsUser.Tables(0).Rows.Count > 0 Then


                T_医院名.Text = dsUser.Tables(0).Rows(0).Item("顧客名")
                T_地域.Text = dsUser.Tables(0).Rows(0).Item("都道府県名")

            End If

LBL_EXIT:
            cnn = Nothing
            sqlcmdSelect = Nothing
            adpt = Nothing
            dsUser = Nothing

        End If

    End Sub

End Class
