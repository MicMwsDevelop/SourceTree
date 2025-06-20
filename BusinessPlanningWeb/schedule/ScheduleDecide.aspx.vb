Imports System.Net.Mail


Partial Class schedule_ScheduleDecide
    Inherits System.Web.UI.Page

    Protected Sub B_Back_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles B_Back.Click

        Response.Redirect("ScheduleMenu.aspx")
    End Sub

    Protected Sub SqlDataSource1_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Selected

        L_検索結果.Text = e.AffectedRows & "件の担当未決定案件があります。"

    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand

        Dim Cname As String
        Cname = e.CommandName

        If (Cname = "Update") Then

            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(index)

            Dim SaGyo As DropDownList = row.Cells(9).FindControl("DropDownList2")


            If (SaGyo.Text = "ここマスク") Then
                Dim message As New MailMessage
                'Dim attachment As Attachment  (ファイル添付が必要な時は開放する)
                Dim client As SmtpClient

                Try
                    ' メッセージを作成する。
                    message.BodyEncoding = System.Text.Encoding.GetEncoding("iso-2022-jp")
                    message.IsBodyHtml = False
                    message.Priority = Net.Mail.MailPriority.Normal
                    message.Subject = "test"
                    message.Body = "This mail was sent from WindowsApplication1." & ControlChars.CrLf & "テストメールです"
                    message.From = New MailAddress("m-imamura@mic.jp")
                    message.To.Add(New MailAddress("m-imamura@mic.jp"))
                    message.To.Add(New MailAddress("m-imamura@mic.jp"))

                    ' ファイルを添付する。
                    'attachment = New Attachment("C:\pictures\omoide.jpg", System.Net.Mime.MediaTypeNames.Image.Jpeg)
                    'message.Attachments.Add(attachment)

                    ' SMTPサーバを指定する。
                    client = New SmtpClient("dove.mic.jp")

                    ' SMTP認証情報を設定する。(認証が必要な場合のみ)
                    '                client.UseDefaultCredentials = False
                    '                client.Timeout = 20000
                    '                Dim cred As New Net.NetworkCredential
                    '                cred.Domain = "mydomain.jp"
                    '                cred.UserName = "アカウント名"
                    '                cred.Password = "アカウントパスワード"
                    '                client.Credentials = cred

                    ' メールを送信する。
                    '*** 実働までマスクします　　　client.Send(message)

                    'attachment.Dispose()   (ファイル添付が必要な時は開放する)
                    message.Dispose()

                    'MessageBox.Show("メールを送信しました。", "Mail", MessageBoxButtons.OK, MessageBoxIcon.Information)


                Catch ex As SmtpException

                    'MessageBox.Show("メールの送信に失敗しました 。" _
                    '               & ControlChars.CrLf _
                    '               & ex.Message _
                    '                & ControlChars.CrLf _
                    '                & ex.StatusCode)

                End Try
            End If
        End If

    End Sub



End Class
