
Partial Class test
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        '接続先設定
        Dim connectionString As String = "Data Source=aspsv\SQLEXPRESS;Initial Catalog = BusinessPlanningWeb; user id='sa'; password='07883510'"
        Dim dbConnection As System.Data.IDbConnection = New System.Data.SqlClient.SqlConnection(connectionString)
        Dim dbCommand As System.Data.IDbCommand = New System.Data.SqlClient.SqlCommand


        Dim querystring As String

        querystring = "BULK INSERT ReturnDocuments FROM '\\aspsv\Common\imamura使用\DB取り込み用.txt' with (FIELDTERMINATOR = ',',ROWTERMINATOR = '\n')"

        dbCommand.CommandText = queryString
        dbCommand.Connection = dbConnection

        dbConnection.Open()

        dbCommand.ExecuteNonQuery()
        dbCommand.Dispose()

    End Sub
End Class
