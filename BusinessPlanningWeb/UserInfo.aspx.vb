Imports System.Data
Imports System.Data.SqlClient

Partial Class UserInfo

    Inherits System.Web.UI.Page

    'Public UserNo As Integer
    Public UserNo As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim reader As System.Data.IDataReader
        Dim sqlstr As String


        'パラメータの顧客Noを取得
        UserNo = Request.QueryString("UserID")


        reader = MyQueryMethod(UserNo)

        If (reader.Read = True) Then

            L_顧客No.Text = reader("顧客No")

            L_顧客名.Text = reader("顧客名1") & reader("顧客名2")
            L_郵便番号.Text = reader("郵便番号")
            L_フリガナ.Text = Nz(reader("フリガナ"))

            L_住所フリガナ.Text = Nz(reader("住所フリガナ"))
            L_住所.Text = reader("住所1") & reader("住所2")

            L_電話番号.Text = reader("電話番号")
            L_FAX番号.Text = Nz(reader("FAX番号"))

            'メモ情報
            sqlstr = "SELECT [fMemType],[fMemMemo] FROM [tMemo] WHERE ([fMemKey] = " & UserNo & ") ORDER BY [fMemUpdate] DESC"

            SqlDataSource1.ConnectionString = "server='DBSV'; user id='ww_reader'; password=''; database='JunpDB'"
            SqlDataSource1.SelectCommand = sqlstr

            '訪問履歴
            sqlstr = "SELECT [fTrnType],[fTrnCliID],[fTrnFrDate],[fTrnFrTime],[fUsrName],[fTrnToTime],[fTrnResult],[fTrnMemo] FROM [vMic訪問予定と実績] WHERE (([fTrnCliID] = " & UserNo & ") AND ([fTrnType] = 'ｺｰﾙ'))  ORDER BY [fTrnFrDate] DESC"

            SqlDataSource2.ConnectionString = "server='DBSV'; user id='ww_reader'; password=''; database='JunpDB'"
            SqlDataSource2.SelectCommand = sqlstr

            'ページは初期値
            GridView1.PageIndex = 0

        End If
        reader = Nothing


    End Sub

    Function MyQueryMethod(ByVal 顧客No As String) As System.Data.IDataReader

        Dim connectionString As String = "server='DBSV'; user id='ww_reader'; password=''; database='JunpDB'"
        Dim dbConnection As System.Data.IDbConnection = New System.Data.SqlClient.SqlConnection(connectionString)

        Dim queryString As String = "SELECT * FROM [vMic全見込み客] WHERE ([vMic全見込み客].[顧客No] = " & 顧客No & ")"
        Dim dbCommand As System.Data.IDbCommand = New System.Data.SqlClient.SqlCommand
        dbCommand.CommandText = queryString
        dbCommand.Connection = dbConnection

        
        dbConnection.Open()
        Dim dataReader As System.Data.IDataReader = dbCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection)

        Return dataReader

        dataReader = Nothing
        dbConnection.Close()

    End Function

    Protected Sub GridView1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.PreRender


        For Each tr As TableRow In GridView1.Rows

            For Each tc As TableCell In tr.Cells
                For Each c As Control In tc.Controls
                    If TypeOf c Is Label Then
                        Dim lbl As Label = CType(c, Label)
                        lbl.Text = lbl.Text.Replace(vbNewLine, "<br />")

                    End If
                Next
            Next
        Next

    End Sub

    Protected Sub GridView2_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.PreRender

        For Each tr As TableRow In GridView2.Rows

            For Each tc As TableCell In tr.Cells
                For Each c As Control In tc.Controls
                    If TypeOf c Is Label Then
                        Dim lbl As Label = CType(c, Label)
                        lbl.Text = lbl.Text.Replace(vbNewLine, "<br />")

                    End If
                Next
            Next
        Next

    End Sub


    Public Function Nz(ByVal Value As Object) As Object
        If IsDBNull(Value) Then
            Nz = ""
        Else
            Nz = Value
        End If
    End Function

    Protected Sub SqlDataSource1_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Selected

        L_検索結果.Text = e.AffectedRows & "件のメモ登録があります。"

    End Sub


    Protected Sub SqlDataSource2_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSource2.Selected

        L_訪問結果.Text = e.AffectedRows & "件の訪問履歴があります。"

    End Sub

  
End Class
