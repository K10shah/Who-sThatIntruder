Imports MySql.Data.MySqlClient
Public Class DeleteUser
    Dim conn As New MySqlConnection
    Dim myCommand As New MySqlCommand
    Dim myConnString As String

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Try
            
            If TextBox1.Text <> "admin" Then
                myConnString = "Server=localhost;Uid=root;password='';database=tmote"
                conn.ConnectionString = myConnString


                conn.Open()


                myCommand.Connection = conn
                myCommand.CommandText = "SELECT * FROM userlogin WHERE username = '" & TextBox1.Text & "'"


                Dim dt = New DataTable()
                Dim ds = New MySqlDataAdapter(myCommand)

                ds.Fill(dt)

                If (dt.Rows.Count > 0) Then
                    Dim reader As MySqlDataReader
                    reader = myCommand.ExecuteReader

                    ' If reader.HasRows Then
                    reader.Read()
                    TextBox2.ReadOnly = False
                    TextBox2.Text = reader.GetString(3)
                    TextBox2.ReadOnly = True
                    TextBox3.ReadOnly = False
                    TextBox3.Text = reader.GetString(2)
                    TextBox3.ReadOnly = True
                    TextBox4.ReadOnly = False
                    TextBox4.Text = reader.GetString(4)
                    TextBox4.ReadOnly = True
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                    'Else
                    'MessageBox.Show("Username and password incorrect 1", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'End If

                Else

                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If

                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub clear()
        TextBox1.Text = ""
        TextBox2.ReadOnly = False
        TextBox2.Text = ""
        TextBox2.ReadOnly = True
        TextBox3.ReadOnly = False
        TextBox3.Text = ""
        TextBox3.ReadOnly = True
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox2.ReadOnly = False
        TextBox2.Text = ""
        TextBox2.ReadOnly = True
        TextBox3.ReadOnly = False
        TextBox3.Text = ""
        TextBox3.ReadOnly = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Len(Trim(TextBox1.Text)) = 0 Then
            MessageBox.Show("Please enter username name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox1.Clear()
            Exit Sub
        End If
        Try

            myConnString = "Server=localhost; User id=root; password=''; database=tmote"
            Dim RowsAffected As Integer = 0

            conn = New MySqlConnection(myConnString)
            conn.Open()

            Dim cq As String = "delete from userlogin where username = '" & TextBox1.Text & "'"

            myCommand = New MySqlCommand(cq)
            myCommand.Connection = conn

            RowsAffected = myCommand.ExecuteNonQuery()
            If RowsAffected > 0 Then

                If conn.State = ConnectionState.Open Then
                    conn.Close()


                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    clear()
                Else
                    MessageBox.Show("No record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Else
                MessageBox.Show("No record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information)

                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If

                conn.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TaskSelection.Show()
        Me.Close()
    End Sub
End Class