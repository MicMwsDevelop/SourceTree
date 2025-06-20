Imports System.Data
Imports System.Data.SqlClient


Partial Class schedule_Scheduleselect
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim sqlstr As String


        '抽出文字列作成
        '【販売促進店】

        'sqlstr2 = "SELECT 案件No,案件名,ハード種別,ハード名 FROM [PCManagement_Details] WHERE 案件No = '" & 案件No & "';"
        sqlstr = "SELECT 種別,Count(名称) AS 総数 FROM [PCManagement_master] GROUP BY PCManagement_master.種別;"


        'データソースセット
        '実環境
        SqlDataSource1.ConnectionString = "server='DBSV'; user id='ww_reader'; password=''; database='SalesDB'"
        SqlDataSource1.SelectCommand = sqlstr

        'ページは初期値
        GridView1.PageIndex = 0


    End Sub


    Protected Sub B_キャンセル_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles B_キャンセル.Click

        Response.Redirect("PCManagement.aspx")
    End Sub

    
    Protected Sub Calendar1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Calendar1.SelectionChanged

        If (C_Target.Checked = False) Then
            T_作業予定日.Text = Calendar1.SelectedDate.ToString("yyyy/MM/dd")
        ElseIf (C_Target.Checked = True) Then
            T_作業終了日.Text = Calendar1.SelectedDate.ToString("yyyy/MM/dd")
        End If

    End Sub

    
    Protected Sub B_クリア_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles B_クリア.Click

        '初期化
        T_案件名.Text = ""
        T_作業予定日.Text = ""
        T_作業終了日.Text = ""
        T_備考.Text = ""
        D_登録者.Text = "若杉 昇"

        C_Target.Checked = False

        lblError.Text = ""

        'カレンダークリア
        Calendar1.SelectedDate = Format(Now(), "yyyy/MM/dd")
        Calendar1.VisibleDate = Format(Now(), "yyyy/MM/dd")

        '選択ハードクリア
        GridView1.DataBind()

    End Sub

    
    Protected Sub B_新規登録_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles B_新規登録.Click


        Dim SQL As System.Data.SqlClient.SqlCommand
        Dim conn As New SqlConnection()
        Dim Suryo As TextBox
        Dim Checkflg As Boolean = False


        '入力値チェック
        If (T_案件名.Text = "" Or T_作業予定日.Text = "") Then

            lblError.Text = "必要項目未入力です。"
            Exit Sub
        End If

        '期間チェック
        If (T_作業終了日.Text <> "" And T_作業予定日.Text > T_作業終了日.Text) Then

            lblError.Text = "期間入力が不正です。"
            Exit Sub
        End If

        'ハード登録チェック
        For i = 0 To GridView1.Rows.Count - 1

            Suryo = GridView1.Rows(i).FindControl("T_使用数量")

            If Suryo.Text >= "1" Then
                Checkflg = True
                Exit For
            End If
        Next
        If (Checkflg = False) Then
            lblError.Text = "ハードが登録されていません。"
            Exit Sub
        End If


        Dim shiyou As Integer = 0
        Dim zettai As Integer = 0
        '数量チェック
        For i = 0 To GridView1.Rows.Count - 1

            Suryo = GridView1.Rows(i).FindControl("T_使用数量")

            If (Suryo.Text <> "") Then
                shiyou = CInt(Suryo.Text)
            Else
                shiyou = 0
            End If

            zettai = CInt(GridView1.Rows(i).Cells(1).Text)

            'If Suryo.Text > GridView1.Rows(i).Cells(1).Text Then
            If shiyou > zettai Then

                Checkflg = False
                Exit For
            End If
        Next
        If (Checkflg = False) Then
            lblError.Text = "ハード数が不正です。"
            Exit Sub
        End If

        'ハード重複チェック
        If (checkHard() = True) Then
            lblError.Text = "ハード数量が足りていません。" & lblError.Text
            Exit Sub
        End If

        'ここから登録


        Dim kosinbi As String
        kosinbi = Now()

        conn.ConnectionString = "Data Source=DBSV;Initial Catalog = SalesDB; user id='sa'; password=''"
        conn.Open()
        SQL = conn.CreateCommand

        '案件登録

        If (IsDBNull(T_作業終了日.Text) = True Or T_作業終了日.Text = "" Or T_作業予定日.Text = T_作業終了日.Text) Then

            SQL.CommandText = "INSERT INTO [PCManagement_Header] ([出力フラグ],[案件名],[開始日],[終了日],[備考],[登録者],[更新日]) VALUES (1,'" & T_案件名.Text & "','" & T_作業予定日.Text & "','" & T_作業予定日.Text & "','" & T_備考.Text & "', '" & D_登録者.Text & "', '" & kosinbi & "');"
            'SQL実行
            SQL.ExecuteNonQuery()
        Else
            SQL.CommandText = "INSERT INTO [PCManagement_Header] ([出力フラグ],[案件名],[開始日],[終了日],[備考],[登録者],[更新日]) VALUES (1,'" & T_案件名.Text & "','" & T_作業予定日.Text & "','" & T_作業終了日.Text & "','" & T_備考.Text & "', '" & D_登録者.Text & "', '" & kosinbi & "');"
            'SQL実行
            SQL.ExecuteNonQuery()


            Dim startDate As Date = T_作業予定日.Text
            Dim EndDate As Date = T_作業終了日.Text

            While (startDate <> EndDate)

                startDate = DateAdd(DateInterval.Day, 1, startDate)

                SQL.CommandText = "INSERT INTO [PCManagement_Header] ([出力フラグ],[案件名],[開始日],[終了日],[備考],[登録者],[更新日]) VALUES (0,'" & T_案件名.Text & "','" & startDate & "','" & T_作業終了日.Text & "','" & T_備考.Text & "', '" & D_登録者.Text & "', '" & kosinbi & "');"
                'SQL実行
                SQL.ExecuteNonQuery()

            End While
        End If


        '登録情報取得
        Dim strSQL As String
        strSQL = "SELECT * FROM PCManagement_Header WHERE 案件名 ='" & T_案件名.Text & "' AND 開始日 = '" & T_作業予定日.Text & "';"
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
        If dsUser.Tables(0).Rows.Count = 0 Then
            cnn.Close()
            lblError.Text = "想定外エラー発生"
            GoTo LBL_EXIT

        End If

        Dim 案件No As Integer
        Dim H種別 As String


        案件No = dsUser.Tables(0).Rows(0).Item(0)

        'ハード登録
        For i = 0 To GridView1.Rows.Count - 1

            Suryo = GridView1.Rows(i).FindControl("T_使用数量")

            If Suryo.Text >= "1" Then

                H種別 = GridView1.Rows(i).Cells(0).Text

                'レコード追加
                SQL.CommandText = "INSERT INTO [PCManagement_Details] ([案件No],[案件名],[ハード種別],[数量]) VALUES ('" & 案件No & "','" & T_案件名.Text & "','" & H種別 & "','" & Suryo.Text & "');"

                'SQL実行
                SQL.ExecuteNonQuery()

            End If
        Next

        '解放
        SQL.Dispose()

        lblError.Text = "登録しました"
        '*** 初期化 *****************************************
        T_案件名.Text = ""
        T_作業予定日.Text = ""
        T_作業終了日.Text = ""
        T_備考.Text = ""
        D_登録者.Text = "若杉 昇"

        C_Target.Checked = False

        'カレンダークリア
        Calendar1.SelectedDate = Format(Now(), "yyyy/MM/dd")
        Calendar1.VisibleDate = Format(Now(), "yyyy/MM/dd")

        '選択ハードクリア
        GridView1.DataBind()
        '*****************************************************

LBL_EXIT:


    End Sub


    Function checkHard() As Boolean

        Dim startDate As Date = T_作業予定日.Text
        Dim EndDate As Date

        Dim strSQL As String
        Dim 案件No As String = ""
        Dim Suryo As TextBox
        Dim meisyo As String

        lblError.Text = ""


        'Falseで初期化
        checkHard = False

        Dim cnn As SqlConnection = New SqlConnection("Data Source=DBSV;Initial Catalog = SalesDB; user id='sa'; password='07883510'")


        'ループをちゃんとまわす為の回避策…最悪…
        If (T_作業終了日.Text = "") Then
            EndDate = DateAdd(DateInterval.Day, 1, startDate)
        Else
            EndDate = T_作業終了日.Text
            EndDate = DateAdd(DateInterval.Day, 1, EndDate)
        End If

        '予定日～終了日までの期間を検索
        While (startDate <> EndDate)

            'strSQL = "SELECT * FROM PCManagement_Header WHERE 開始日 ='" & startDate & "' AND 出力フラグ = 1 ;"
            strSQL = "SELECT * FROM PCManagement_Header WHERE 開始日 <='" & startDate & "' AND 終了日 >='" & startDate & "' AND 出力フラグ = 1 ;"

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

                案件No = ""

                'その日付の案件分ループ
                For i = 0 To dsUser.Tables(0).Rows.Count - 1

                    案件No = 案件No & " (案件No = " & dsUser.Tables(0).Rows(i).Item(0) & ") Or "

                Next
                案件No = Left(案件No, Len(案件No) - 3)

                'strSQL = "SELECT * FROM PCManagement_Details WHERE 案件No ='" & 案件No & "';"
                'strSQL = "SELECT ハード種別,Sum(数量) AS 総数 FROM [PCManagement_Details] WHERE 案件No = '" & 案件No & "' GROUP BY PCManagement_Details.ハード種別;"
                strSQL = "SELECT ハード種別,Sum(数量) AS 総数 FROM [PCManagement_Details] WHERE" & 案件No & "GROUP BY PCManagement_Details.ハード種別;"


                Dim sqlcmdSelect2 As SqlCommand = New SqlCommand(strSQL, cnn)
                sqlcmdSelect2.CommandTimeout = 15

                Dim adpt2 = New SqlDataAdapter(sqlcmdSelect2)
                Dim dsUser2 As DataSet = New DataSet
                Try
                    adpt2.Fill(dsUser2)
                Catch ex As Exception
                    cnn.Close()
                    lblError.Text = "データベースアクセスに失敗しました。"
                    GoTo LBL_EXIT
                End Try

                For m = 0 To GridView1.Rows.Count - 1

                    Suryo = GridView1.Rows(m).FindControl("T_使用数量")

                    If Suryo.Text >= "1" Then

                        meisyo = GridView1.Rows(m).Cells(0).Text

                        For n = 0 To dsUser2.Tables(0).Rows.Count - 1

                            If meisyo = dsUser2.Tables(0).Rows(n).Item(0) Then

                                Dim fafa As Integer
                                fafa = CInt(Suryo.Text) + dsUser2.Tables(0).Rows(n).Item(1)

                                If (fafa > GridView1.Rows(m).Cells(1).Text) Then
                                    checkHard = True
                                    lblError.Text = "【" & startDate & " " & meisyo & "】"
                                    GoTo LBL_EXIT

                                End If
                            End If

                        Next

                    End If
                Next


            End If

            startDate = DateAdd(DateInterval.Day, 1, startDate)


        End While


LBL_EXIT:


    End Function

End Class
