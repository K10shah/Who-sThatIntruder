Imports MySql.Data.MySqlClient
Public Class AddUser
    Dim conn As New MySqlConnection
    Dim myCommand As New MySqlCommand
    Dim myConnString As String

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox1.Focus()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try


            Dim role As String
            myConnString = "Server=localhost;Uid=root;password='';database=tmote"
            conn.ConnectionString = myConnString


            conn.Open()

            role = ComboBox1.SelectedItem
            myCommand.Connection = conn
            myCommand.CommandText = "insert into userlogin(username,password,name,role) values('" & TextBox2.Text & "','" & TextBox3.Text & "','" + TextBox1.Text + "','" + role + "')"
            'myCommand.Parameters.AddWithValue("?moteid", words(0))
            myCommand.ExecuteNonQuery()
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            MessageBox.Show("New user added to database", "User details", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox1.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TaskSelection.Show()
        Me.Close()
    End Sub


    Private Sub AddUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.Items.Add("admin")
        ComboBox1.Items.Add("user")
        ComboBox1.Items.Add("database-admin")
        ComboBox1.Text = ComboBox1.Items.Item(0)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        ComboBox1.Text = ComboBox1.SelectedItem
    End Sub

    Private Sub luname_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles luname.Click

    End Sub
End Class