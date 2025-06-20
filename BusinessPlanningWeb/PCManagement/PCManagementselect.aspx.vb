Imports System.Data
Imports System.Data.SqlClient


Partial Class schedule_Scheduleselect
    Inherits System.Web.UI.Page


    Protected Sub B_新規登録_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles B_新規登録.Click

        Dim SQL As System.Data.SqlClient.SqlCommand
        Dim conn As New SqlConnection()


        '入力値チェック
        If (T_Code.Text = "" Or T_名称.Text = "") Then

            lblError.Text = "必要項目未入力です。"
            Exit Sub
        End If


        Dim strSQL As String

        strSQL = "SELECT * FROM PCManagement_master WHERE 名称 ='" & T_名称.Text & "';"
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
            lblError.Text = "同名のハードがすでに登録されています。"
            GoTo LBL_EXIT

        End If

        Dim kosinbi As String
        kosinbi = Now()

        conn.ConnectionString = "Data Source=DBSV;Initial Catalog = SalesDB; user id='sa'; password='07883510'"
        conn.Open()

        SQL = conn.CreateCommand
        'レコード追加
        SQL.CommandText = "INSERT INTO [PCManagement_master] ([Code],[種別],[名称],[備考],[更新日時]) VALUES ('" & T_Code.Text & "','" & D_種別.Text & "','" & T_名称.Text & "','" & T_備考.Text & "', '" & kosinbi & "');"


        'SQL実行
        SQL.ExecuteNonQuery()

        '解放
        SQL.Dispose()
        lblError.Text = "登録しました"

LBL_EXIT:
        cnn = Nothing
        sqlcmdSelect = Nothing
        adpt = Nothing


    End Sub

    Protected Sub B_キャンセル_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles B_キャンセル.Click

        Response.Redirect("PCManagement.aspx")
    End Sub

    
    Protected Sub B_クリア_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles B_クリア.Click

        '初期化
        D_種別.Text = "ＰＣ"
        D種別.Text = "すべて"


        T_名称.Text = ""
        T_Code.Text = ""
        T_備考.Text = ""

        lblError.Text = ""

    End Sub

    
End Class
