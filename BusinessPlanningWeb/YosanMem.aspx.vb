Imports System.Data
Imports System.Data.SqlClient

Partial Class YosanMem
    Inherits System.Web.UI.Page

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

        Dim row As GridViewRow = GridView1.SelectedRow
        Dim User As String = row.Cells(6).Text
        Dim cb As CheckBox = CType(row.FindControl("CheckBox1"), CheckBox)


        Dim setfig As Integer

        Dim SQL As System.Data.SqlClient.SqlCommand
        Dim conn As New SqlConnection()

        conn.ConnectionString = "Data Source=DBSV;Initial Catalog = SalesDB; user id='sa'; password='07883510'"
        conn.Open()

        '現編集ロックがONの場合⇒編集ロックOFF
        If (cb.Checked = "True") Then
            setfig = 0
        Else
            '現編集ロックがOFFの場合⇒編集ロックON
            setfig = 1
        End If

        '<<テーブル削除処理>>
        SQL = conn.CreateCommand
        'レコード追加
        SQL.CommandText = "UPDATE Yosan_tUser SET fUsrLockFlg = '" & setfig & "' WHERE fUsrName ='" & User & "';"

        'SQL実行
        SQL.ExecuteNonQuery()

        SQL.CommandText = "UPDATE YosanTable SET 編集ロック = '" & setfig & "' WHERE 氏名 ='" & User & "';"

        'SQL実行
        SQL.ExecuteNonQuery()


        '解放
        SQL.Dispose()

        Response.Redirect("YosanMem.aspx")

    End Sub



End Class
