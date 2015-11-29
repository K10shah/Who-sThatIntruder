Imports MySql.Data.MySqlClient
Imports System.Media
Public Class Login
    Dim conn As New MySqlConnection
    Dim myCommand As New MySqlCommand
    Dim myConnString As String

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        TextBox1.Clear()
        TextBox2.Clear()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If Len(Trim(TextBox1.Text)) = 0 Then
            MessageBox.Show("Please enter user name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox1.Focus()
            Exit Sub
        End If
        If Len(Trim(TextBox2.Text)) = 0 Then
            MessageBox.Show("Please enter password", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox2.Focus()
            Exit Sub
        End If

        Try



            myConnString = "Server=localhost;Uid=root;password='';database=tmote"
            conn.ConnectionString = myConnString


            conn.Open()


            myCommand.Connection = conn
            myCommand.CommandText = "SELECT * FROM userlogin WHERE username = ?Username and password = ?Password"
            myCommand.Parameters.AddWithValue("?Username", TextBox1.Text)
            myCommand.Parameters.AddWithValue("?Password", TextBox2.Text)
            Dim likea As String = myCommand.CommandText
            Dim dt = New DataTable()
            Dim ds = New MySqlDataAdapter(myCommand)

            ds.Fill(dt)

            If (dt.Rows.Count > 0) Then
                Dim reader As MySqlDataReader
                reader = myCommand.ExecuteReader
                reader.Read()
                If reader.GetString(4) = "user" Then


                    ProgressBar1.Visible = True
                    ProgressBar1.Maximum = 5000
                    ProgressBar1.Minimum = 0
                    ProgressBar1.Value = 4
                    ProgressBar1.Step = 1
                    For i = 0 To 5000
                        ProgressBar1.PerformStep()
                    Next
                    ' If reader.HasRows Then

                    Monitor.Show()
                    Me.Close()
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If

                Else
                    If reader.GetString(4) = "database-a" Then

                        ProgressBar1.Visible = True
                        ProgressBar1.Maximum = 5000
                        ProgressBar1.Minimum = 0
                        ProgressBar1.Value = 4
                        ProgressBar1.Step = 1
                        For i = 0 To 5000
                            ProgressBar1.PerformStep()
                        Next
                        Databaseadmin.Show()
                        Me.Close()
                        If conn.State = ConnectionState.Open Then
                            conn.Close()
                        End If
                    End If
                End If


            Else

                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If

                MessageBox.Show("Username and password incorrect", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox1.Focus()


            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class