Imports MySql.Data.MySqlClient
Public Class AdminLogin
    Dim conn As New MySqlConnection
    Dim myCommand As New MySqlCommand
    Dim myConnString As String
    Private Sub AdminLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try



            myConnString = "Server=localhost;Uid=root;password='';database=tmote"
            conn.ConnectionString = myConnString

            Try
                conn.Open()

            Catch ex As Exception
            End Try
            myCommand.Connection = conn
            myCommand.CommandText = "SELECT * FROM userlogin"
            Dim dt = New DataTable()
            Dim ds = New MySqlDataAdapter(myCommand)
            ds.Fill(dt)
            If (dt.Rows.Count > 0) Then
                Button1.Visible = True


            Else
                Button1.Visible = False
                Button3.Visible = True
                Label1.Visible = True
                Label4.Visible = True
                TextBox1.Visible = True

            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        myCommand.CommandText = "insert into userlogin(username,password,name,role) values('" & TextBox2.Text & "','" & TextBox1.Text & "','" + TextBox3.Text + "','admin')"
        myCommand.ExecuteNonQuery()
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        MessageBox.Show("You are now the admin of this software", "Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox1.Focus()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        WelcomeLogin.Show()
        Me.Close()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Try
            myCommand.CommandText = "SELECT * FROM userlogin WHERE username = ?Username and password = ?Password and role = 'admin'"
            myCommand.Parameters.AddWithValue("?Username", TextBox2.Text)
            myCommand.Parameters.AddWithValue("?Password", TextBox3.Text)
            Dim dt = New DataTable()
            Dim ds = New MySqlDataAdapter(myCommand)

            ds.Fill(dt)

            If (dt.Rows.Count > 0) Then
                Dim reader As MySqlDataReader
                reader = myCommand.ExecuteReader



                ProgressBar1.Visible = True
                ProgressBar1.Maximum = 5000
                ProgressBar1.Minimum = 0
                ProgressBar1.Value = 4
                ProgressBar1.Step = 1
                For i = 0 To 5000
                    ProgressBar1.PerformStep()
                Next
                ' If reader.HasRows Then

                TaskSelection.Show()
                Me.Close()
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

                MessageBox.Show("Username and password incorrect", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox2.Clear()
                TextBox3.Clear()
                TextBox1.Focus()


            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class