Imports System.Data
Imports System.Data.SqlClient


Partial Class Login
    Inherits System.Web.UI.Page


    Private Sub webCtrlFocus(ByVal webCtrl As System.Web.UI.Control)
        ' HTMLページが表示された時のフォーカス設定のプロシージャです
        Dim strScript As String
        Dim csmInstance As ClientScriptManager = Page.ClientScript

        strScript = "<SCRIPT language='javascript'>" & _
                    "document.getElementById('" & webCtrl.ID & "').focus()</SCRIPT>"
        csmInstance.RegisterStartupScript(Me.GetType(), "focus", strScript)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.Redirect("BusinessPerformance/BusinessPerformance37.aspx")
        'webCtrlFocus(txtID)
    End Sub

    'Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
    Protected Sub btnLogin0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin0.Click

        Dim blnAccess As Boolean = False

        lblError.Text = ""

        If txtID.Text.Trim = "" Or txtPass.Text.Trim = "" Then
            lblError.Text = "IDとパスワードを入力してください。"
            Exit Sub
        End If

        Dim strSQL As String
        'strSQL = "SELECT * FROM tUser dbo_tUser WHERE fUsrLoginID ='" & txtID.Text.Trim & "' AND fUsrLoginPwd ='" & txtPass.Text.Trim & "';"
        strSQL = "SELECT * FROM tUser WHERE fUsrLoginID ='" & txtID.Text.Trim & "' AND fUsrLoginPwd ='" & txtPass.Text.Trim & "';"

        'Dim cnn As SqlConnection = New SqlConnection("Data Source=aspsv\SQLEXPRESS;Initial Catalog = test; user id='sa'; password='07883510'")
        Dim cnn As SqlConnection = New SqlConnection("Data Source=dbsv;Initial Catalog = JunpDB; user id='sa'; password='07883510'")

        Dim sqlcmdSelect As SqlCommand = New SqlCommand(strSQL, cnn)
        sqlcmdSelect.CommandTimeout = 15

        Dim adpt = New SqlDataAdapter(sqlcmdSelect)
        Dim dsLogin As DataSet = New DataSet
        Try
            adpt.Fill(dsLogin)
        Catch ex As Exception
            cnn.Close()
            lblError.Text = "データベースアクセスに失敗しました。"
            GoTo LBL_EXIT
        End Try

        If dsLogin.Tables(0).Rows.Count > 0 Then
            With dsLogin.Tables(0).Rows(0)
                'Session("LOGINID") = .Item("fUsrLoginID")
                'Session("LOGINPWD") = .Item("fUsrLoginPwd")
                Session("VISITORNAME") = .Item("fUsrName")
                Session("LOGINBUSHO2") = .Item("fUsrBusho2")

                If (.Item("fUsrBusho3") = "22" Or .Item("fUsrBusho3") = "23" Or .Item("fUsrBusho3") = "24" Or .Item("fUsrBusho3") = "25" Or .Item("fUsrBusho3") = "26") Then
                    Session("LOGINBUSHO3") = "20"
                Else
                    Session("LOGINBUSHO3") = .Item("fUsrBusho3")
                End If

            End With
            blnAccess = True
        Else
            lblError.Text = "ログインに失敗しました。ログインしなおして下さい。"
            txtID.Text = ""
            txtPass.Text = ""
        End If

LBL_EXIT:
        cnn = Nothing
        sqlcmdSelect = Nothing
        adpt = Nothing
        dsLogin = Nothing
        If blnAccess = True Then
            'TOPページ選択
            'Response.Redirect("misyounin/misyounin.aspx")
            'Response.Redirect("ProspectiveCustomer.aspx")
            'Response.Redirect("online/online.aspx")
            Response.Redirect("BusinessPerformance/BusinessPerformance37.aspx")
        End If
    End Sub


End Class
