Imports System.Data
Imports System.Data.SqlClient


Partial Class schedule_Scheduleselect
    Inherits System.Web.UI.Page



    Protected Sub B_検索_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles B_検索.Click

        lblError.Text = ""

        If T_伝票No.Text.Trim = "" Or T_伝票No.Text.Trim = "" Then
            lblError.Text = "伝票Noを入力してください。"
            Exit Sub
        End If

        Dim strSQL As String

        strSQL = "SELECT * FROM tMih受注ヘッダ WHERE f受注番号 ='" & T_伝票No.Text.Trim & "';"

        Dim cnn As SqlConnection = New SqlConnection("Data Source=dbsv;Initial Catalog = JunpDB; user id='ww_reader';")

        Dim sqlcmdSelect As SqlCommand = New SqlCommand(strSQL, cnn)
        sqlcmdSelect.CommandTimeout = 15

        Dim adpt = New SqlDataAdapter(sqlcmdSelect)
        Dim dsLogin As DataSet = New DataSet
        Try
            adpt.fill(dsLogin)
        Catch ex As Exception
            cnn.Close()
            lblError.Text = "データベースアクセスに失敗しました。"
            GoTo LBL_EXIT
        End Try

        If dsLogin.Tables(0).Rows.Count > 0 Then
            With dsLogin.Tables(0).Rows(0)

                T_顧客No.Text = .Item("fユーザーコード")
                T_顧客名.Text = .Item("fユーザー")
                T_担当部署名.Text = .Item("f担当支店名")
                T_担当部署コード.Text = .Item("fBshCode3")

            End With
        Else
            lblError.Text = "ログインに失敗しました。ログインしなおして下さい。"
            T_伝票No.Text = ""

        End If

LBL_EXIT:
        cnn = Nothing
        sqlcmdSelect = Nothing
        adpt = Nothing
        dsLogin = Nothing

    End Sub


    Protected Sub B_新規登録_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles B_新規登録.Click

        Dim SQL As System.Data.SqlClient.SqlCommand
        Dim conn As New SqlConnection()

        lblError.Text = ""

        '入力値チェック
        '(顧客No確定チェック)
        If (IsDBNull(T_顧客No.Text) = True Or T_顧客No.Text = "") Then

            lblError.Text = "対象ユーザーが確定していません。"
            Exit Sub
        End If

        '(作業予定日入力チェック)
        If (IsDBNull(T_作業予定日.Text) = True Or T_作業予定日.Text = "") Then

            lblError.Text = "作業予定日の入力は必須です。"
            Exit Sub
        End If


        Dim strSQL As String


        strSQL = "SELECT * FROM schedule WHERE 顧客No ='" & T_顧客No.Text & "';"
        Dim cnn As SqlConnection = New SqlConnection("Data Source=DBSV;Initial Catalog = SalesDB; user id='sa'; password='07883510'")

        Dim sqlcmdSelect As SqlCommand = New SqlCommand(strSQL, cnn)
        sqlcmdSelect.CommandTimeout = 15

        Dim adpt = New SqlDataAdapter(sqlcmdSelect)
        Dim dsUser As DataSet = New DataSet
        Try
            adpt.Fill(dsUser)
        Catch ex As Exception
            cnn.Close()
            lblError.Text = "データベースアクセスに失敗しました。"
            GoTo LBL_EXIT
        End Try

        'レコードにHIT　よって登録済みである
        If dsUser.Tables(0).Rows.Count > 0 Then
            cnn.Close()
            lblError.Text = "対象のユーザーはすでに作業登録済みです。"
            GoTo LBL_EXIT

        End If



        '担当部署コード 担当部署名調整 調整
        If (T_担当部署コード.Text = "22" Or T_担当部署コード.Text = "23" Or T_担当部署コード.Text = "24" Or T_担当部署コード.Text = "25" Or T_担当部署コード.Text = "26") Then
            T_担当部署コード.Text = "20"
            T_担当部署名.Text = "首都圏営業部"
        End If


        conn.ConnectionString = "Data Source=DBSV;Initial Catalog = SalesDB; user id='sa'; password='07883510'"
        conn.Open()

        SQL = conn.CreateCommand
        'レコード追加
        SQL.CommandText = "INSERT INTO [schedule] ([顧客No],[顧客名],[担当部署コード],[担当部署名],[登録担当ID],[登録担当者],[作業担当者], [登録日], [作業予定日], [作業方法],[備考],[終了フラグ]) VALUES (" & T_顧客No.Text & ",'" & T_顧客名.Text & "','" & T_担当部署コード.Text & "','" & T_担当部署名.Text & "', '','" & Session("VISITORNAME") & "','未決定','" & Now() & "', '" & T_作業予定日.Text & "', '" & D_作業方法.Text & "', '" & T_備考.Text & "',0 );"


        'SQL実行
        SQL.ExecuteNonQuery()

        '解放
        SQL.Dispose()

        Response.Redirect("ScheduleMenu.aspx")

LBL_EXIT:
        cnn = Nothing
        sqlcmdSelect = Nothing
        adpt = Nothing


    End Sub

    Protected Sub B_キャンセル_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles B_キャンセル.Click

        Response.Redirect("ScheduleMenu.aspx")
    End Sub

    
    Protected Sub Calendar1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Calendar1.SelectionChanged

        T_作業予定日.Text = Calendar1.SelectedDate.ToString("yyyy/MM/dd")

    End Sub

    
    Protected Sub B_クリア_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles B_クリア.Click

        '初期化
        T_伝票No.Text = ""
        T_顧客No.Text = ""
        T_顧客名.Text = ""
        T_担当部署名.Text = ""
        T_担当部署コード.Text = ""
        D_作業方法.Text = "訪問対応"
        T_作業予定日.Text = ""
        T_備考.Text = ""
        lblError.Text = ""

        'カレンダークリア
        Calendar1.SelectedDate = Format(Now(), "yyyy/MM/dd")
        Calendar1.VisibleDate = Format(Now(), "yyyy/MM/dd")


    End Sub
End Class
