Imports System.Data
Imports System.Data.SqlClient

Partial Class Yosan
    Inherits System.Web.UI.Page


    Sub JoinCells(ByVal gv As GridView, ByVal icol As Integer)

        Dim numRow As Integer = gv.Rows.Count
        Dim baseIndex As Integer = 0

        Do While (baseIndex < numRow)
            Dim nextIndex As Integer = baseIndex + 1
            Dim baseCell As TableCell = gv.Rows(baseIndex).Cells(icol)

            Do While (nextIndex < numRow)

                Dim nextCell As TableCell = gv.Rows(nextIndex).Cells(icol)
                If baseCell.Text = nextCell.Text Then
                    If baseCell.RowSpan = 0 Then
                        baseCell.RowSpan = 2
                    Else
                        baseCell.RowSpan = baseCell.RowSpan + 1
                    End If
                    nextCell.Visible = False
                    nextIndex = nextIndex + 1
                Else
                    Exit Do
                End If
            Loop
            baseIndex = nextIndex
        Loop
    End Sub



    Protected Sub GridView1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.PreRender

        JoinCells(GridView1, 2)
        JoinCells(GridView1, 3)


    End Sub

    Private January As Integer = 0
    Private February As Integer = 0
    Private March As Integer = 0
    Private April As Integer = 0
    Private May As Integer = 0
    Private June As Integer = 0
    Private July As Integer = 0
    Private August As Integer = 0
    Private September As Integer = 0
    Private October As Integer = 0
    Private November As Integer = 0
    Private December As Integer = 0



    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If D_kind.Text <> "%" Then

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim drv As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                Dim dr As Data.DataRow = CType(drv.Row, Data.DataRow)

                If Not IsDBNull(dr("column6")) Then
                    January += dr("column6")
                End If
                If Not IsDBNull(dr("column7")) Then
                    February += dr("column7")
                End If
                If Not IsDBNull(dr("column8")) Then
                    March += dr("column8")
                End If
                If Not IsDBNull(dr("column9")) Then
                    April += dr("column9")
                End If
                If Not IsDBNull(dr("column10")) Then
                    May += dr("column10")
                End If
                If Not IsDBNull(dr("column11")) Then
                    June += dr("column11")
                End If
                If Not IsDBNull(dr("column12")) Then
                    July += dr("column12")
                End If
                If Not IsDBNull(dr("column1")) Then
                    August += dr("column1")
                End If
                If Not IsDBNull(dr("column2")) Then
                    September += dr("column2")
                End If
                If Not IsDBNull(dr("column3")) Then
                    October += dr("column3")
                End If
                If Not IsDBNull(dr("column4")) Then
                    November += dr("column4")
                End If
                If Not IsDBNull(dr("column5")) Then
                    December += dr("column5")
                End If

            ElseIf e.Row.RowType = DataControlRowType.Footer Then

                e.Row.Cells(0).Text = "合計"
                e.Row.Cells(5).Text = August.ToString("n0")
                e.Row.Cells(6).Text = September.ToString("n0")
                e.Row.Cells(7).Text = October.ToString("n0")
                e.Row.Cells(8).Text = November.ToString("n0")
                e.Row.Cells(9).Text = December.ToString("n0")
                e.Row.Cells(10).Text = January.ToString("n0")
                e.Row.Cells(11).Text = February.ToString("n0")
                e.Row.Cells(12).Text = March.ToString("n0")
                e.Row.Cells(13).Text = April.ToString("n0")
                e.Row.Cells(14).Text = May.ToString("n0")
                e.Row.Cells(15).Text = June.ToString("n0")
                e.Row.Cells(16).Text = July.ToString("n0")
                e.Row.Cells(17).Text = August + September + October + November + December + January + February + March + April + May + June + July

            End If
        Else
            If e.Row.RowType = DataControlRowType.Footer Then

                e.Row.Cells(0).Text = "合計"
                e.Row.Cells(5).Text = "-"
                e.Row.Cells(6).Text = "-"
                e.Row.Cells(7).Text = "-"
                e.Row.Cells(8).Text = "-"
                e.Row.Cells(9).Text = "-"
                e.Row.Cells(10).Text = "-"
                e.Row.Cells(11).Text = "-"
                e.Row.Cells(12).Text = "-"
                e.Row.Cells(13).Text = "-"
                e.Row.Cells(14).Text = "-"
                e.Row.Cells(15).Text = "-"
                e.Row.Cells(16).Text = "-"
                e.Row.Cells(17).Text = "-"

            End If

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim cb As CheckBox = CType(e.Row.FindControl("checkbox1"), CheckBox)

            If (cb.Checked = True) Then
                e.Row.Cells(0).Controls(0).Visible = False
            End If

        End If

    End Sub


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        If (IsPostBack = True) Then
            Exit Sub
        End If


        Dim blnAccess As Boolean = False
        Dim strSQL As String
        Dim Login As String

        Login = Session("VISITORNAME")

        strSQL = "SELECT * FROM Yosan_tUser WHERE fUsrName ='" & Login & "';"
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


            If (dsUser.Tables(0).Rows(0).Item("fUsrTop") = True) Then

                'もし拠点長だったら
                '自分の拠点メンバーをすべて表示
                With dsUser.Tables(0).Rows(0)

                    T_Area.Text = .Item("fUsrArea")

                    D_Busho.Items.Add(.Item("fUsrBusho"))
                    D_Busho.Text = "%"


                    '秋支店長だったら
                    '金沢営業所と広島営業所を追加
                    If (.Item("fUsrName") = "秋 相文") Then

                        D_Busho.Items.Add("金沢営業所")
                        D_Busho.Items.Add("広島営業所")

                    End If

                End With

                '*** Member確定ループ ***
                strSQL = "SELECT * FROM Yosan_tUser WHERE fUsrArea ='" & T_Area.Text & "';"

                sqlcmdSelect = New SqlCommand(strSQL, cnn)
                sqlcmdSelect.CommandTimeout = 15

                adpt = New SqlDataAdapter(sqlcmdSelect)
                dsUser = New DataSet
                Try
                    adpt.Fill(dsUser)
                Catch ex As Exception
                    cnn.Close()
                    'lblError.Text = "データベースアクセスに失敗しました。"
                    GoTo LBL_EXIT
                End Try

                'レコードにHIT　よって予算編成メンバーである

                With dsUser.Tables(0)

                    For i = 0 To dsUser.Tables(0).Rows.Count - 1

                        D_User.Items.Add(.Rows(i).Item("fUsrName"))

                    Next

                End With
                '*** Member確定ここまで ***



            Else
                'もし予算編成メンバーだったら Session("VISITORNAME")
                '自分のリストのみを表示
                With dsUser.Tables(0).Rows(0)

                    T_Area.Text = .Item("fUsrArea")

                    D_User.Items.Add(.Item("fUsrName"))
                    D_User.Text = .Item("fUsrName")

                End With

                D_User.Visible = False
                D_Busho.Visible = False
                L_Busho.Visible = False
                L_Member.Visible = False

            End If

            blnAccess = True

        End If


        If (Login = "川越 浩一" Or Login = "佐々木 誠" Or Login = "小杉 秀徳" Or Login = "後藤 進一郎" Or Login = "今村 元樹") Then

            T_Area.Text = "%"

            D_Busho.Items.Add("札幌支店")
            D_Busho.Items.Add("仙台支店")
            D_Busho.Items.Add("名古屋支店")
            D_Busho.Items.Add("大阪支店")
            D_Busho.Items.Add("金沢営業所")
            D_Busho.Items.Add("広島営業所")
            D_Busho.Items.Add("福岡支店")
            D_Busho.Items.Add("首都圏営業部 営業１課")
            D_Busho.Items.Add("首都圏営業部 営業２課")
            D_Busho.Items.Add("首都圏営業部 営業３課")


            D_User.Visible = True
            L_Member.Visible = True

            '*** Member確定ループ ***
            strSQL = "SELECT * FROM Yosan_tUser;"

            sqlcmdSelect = New SqlCommand(strSQL, cnn)
            sqlcmdSelect.CommandTimeout = 15

            adpt = New SqlDataAdapter(sqlcmdSelect)
            dsUser = New DataSet
            Try
                adpt.Fill(dsUser)
            Catch ex As Exception
                cnn.Close()
                'lblError.Text = "データベースアクセスに失敗しました。"
                GoTo LBL_EXIT
            End Try

            'レコードにHIT　よって予算編成メンバーである

            With dsUser.Tables(0)

                For i = 0 To dsUser.Tables(0).Rows.Count - 1

                    D_User.Items.Add(.Rows(i).Item("fUsrName"))

                Next

            End With
            '*** Member確定ここまで ***

            blnAccess = True
        End If


LBL_EXIT:
        cnn = Nothing
        sqlcmdSelect = Nothing
        adpt = Nothing
        dsUser = Nothing

        If blnAccess = False Then
            Response.Redirect("AuthorityError.aspx")
        End If

    End Sub


End Class
