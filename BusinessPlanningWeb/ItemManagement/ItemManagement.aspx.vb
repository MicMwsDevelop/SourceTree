
Partial Class ItemManagement
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Response.Redirect("ItemManagementRegister.aspx")

    End Sub


    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim drv As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
            Dim dr As Data.DataRow = CType(drv.Row, Data.DataRow)

            '*** 12月 ***
            If Not (IsDBNull(dr("12月"))) Then
                If (dr("12月") = "×") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.DarkGray
                End If

                If (dr("12月") = "★") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.Pink
                End If
            End If

            '*** 12月 ***
            If Not (IsDBNull(dr("12月"))) Then
                If (dr("12月") = "×") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.DarkGray
                End If

                If (dr("12月") = "★") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.Pink
                End If
            End If

            '*** 1月 ***
            If Not (IsDBNull(dr("1月"))) Then
                If (dr("1月") = "×") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.DarkGray
                End If

                If (dr("1月") = "★") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.Pink
                End If
            End If

            '*** 2月 ***
            If Not (IsDBNull(dr("2月"))) Then
                If (dr("2月") = "×") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.DarkGray
                End If

                If (dr("2月") = "★") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.Pink
                End If
            End If

            '*** 3月 ***
            If Not (IsDBNull(dr("3月"))) Then
                If (dr("3月") = "×") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.DarkGray
                End If

                If (dr("3月") = "★") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.Pink
                End If
            End If

            '*** 4月 ***
            If Not (IsDBNull(dr("4月"))) Then
                If (dr("4月") = "×") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.DarkGray
                End If

                If (dr("4月") = "★") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.Pink
                End If
            End If

            '*** 5月 ***
            If Not (IsDBNull(dr("5月"))) Then
                If (dr("5月") = "×") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.DarkGray
                End If

                If (dr("5月") = "★") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.Pink
                End If
            End If

            '*** 6月 ***
            If Not (IsDBNull(dr("6月"))) Then
                If (dr("6月") = "×") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.DarkGray
                End If

                If (dr("6月") = "★") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.Pink
                End If
            End If

            '*** 7月 ***
            If Not (IsDBNull(dr("7月"))) Then
                If (dr("7月") = "×") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.DarkGray
                End If

                If (dr("7月") = "★") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.Pink
                End If
            End If

            '*** 8月 ***
            If Not (IsDBNull(dr("8月"))) Then
                If (dr("8月") = "×") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.DarkGray
                End If

                If (dr("8月") = "★") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.Pink
                End If
            End If

            '*** 9月 ***
            If Not (IsDBNull(dr("9月"))) Then
                If (dr("9月") = "×") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.DarkGray
                End If

                If (dr("9月") = "★") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.Pink
                End If
            End If

            '*** 10月 ***
            If Not (IsDBNull(dr("10月"))) Then
                If (dr("10月") = "×") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.DarkGray
                End If

                If (dr("10月") = "★") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.Pink
                End If
            End If

            '*** 11月 ***
            If Not (IsDBNull(dr("11月"))) Then
                If (dr("11月") = "×") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.DarkGray
                End If

                If (dr("11月") = "★") Then
                    e.Row.ControlStyle.BackColor = Drawing.Color.Pink
                End If
            End If


        End If
    End Sub







End Class
