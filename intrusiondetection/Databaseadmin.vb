Imports MySql.Data.MySqlClient



Public Class Databaseadmin

    Dim conn As New MySqlConnection
    Dim myCommand As New MySqlCommand
    Dim myConnString As String = "Server=localhost;Uid=root;password='';database=tmote"

    Dim Myadapter As New MySql.Data.MySqlClient.MySqlDataAdapter
    Dim dbDataSet As DataTable
    Dim bSource As New BindingSource
    Dim MyDataset As DataSet
    Dim reader As MySqlDataReader
    Dim seldate As String = ""
    Dim selid As String = ""
    Dim selloc As String = ""
    
    
    Private Sub Databaseadmin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        conn.ConnectionString = myConnString
        Dim Queryy As String
        Dim mCommand As MySqlCommand
        Try
            

            ComboBox1.Items.Add("")
            ComboBox2.Items.Add("")
            ComboBox3.Items.Add("")
            conn.Open()

            Queryy = "Select Distinct date from tmote.intrusiondetails"
            mCommand = New MySqlCommand(Queryy, conn)
            reader = mCommand.ExecuteReader

            While reader.Read
                Dim sdate = reader.GetString("date")

                ComboBox1.Items.Add(sdate)

            End While
            reader.Close()


            Queryy = "Select Distinct location from tmote.intrusiondetails"
            mCommand = New MySqlCommand(Queryy, conn)
            reader = mCommand.ExecuteReader

            While reader.Read
                Dim sloc = reader.GetString("location")

                ComboBox2.Items.Add(sloc)
            End While
            reader.Close()


            Queryy = "Select Distinct moteid from tmote.intrusiondetails"
            mCommand = New MySqlCommand(Queryy, conn)
            reader = mCommand.ExecuteReader

            While reader.Read
                Dim sid = reader.GetString("moteid")

                ComboBox3.Items.Add(sid)
            End While

            reader.Close()

            Dim dt = New DataTable()
            Dim ds = New MySqlDataAdapter
            Queryy = "select * from intrusiondetails"
            mCommand = New MySqlCommand(Queryy, conn)
            ds.SelectCommand = mCommand
            ds.Fill(dt)
            bSource.DataSource = dt
            DataGridView1.DataSource = bSource
            ds.Update(dt)


            conn.Close()



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            conn.Dispose()
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        WelcomeLogin.Show()
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim Queryy As String
        Dim mCommand As MySqlCommand
        Try
            If ComboBox1.Text = " Then" Then
                seldate = ""
            Else
                seldate = ComboBox1.SelectedItem.ToString()
            End If
            If ComboBox2.Text = "" Then
                selloc = ""
            Else
                selloc = ComboBox2.SelectedItem.ToString()
            End If
            If ComboBox3.Text = "" Then
                selid = ""
            Else
                selid = ComboBox3.SelectedItem.ToString()
            End If
            conn.Open()
            Dim dt = New DataTable()
            Dim ds = New MySqlDataAdapter
            Queryy = "select * from tmote.intrusiondetails"

            If (seldate <> "" Or selloc <> "" Or selid <> "") Then
                Queryy = Queryy + " where "
                If (seldate <> "") Then
                    Queryy = Queryy + "date = " + "'" + seldate + "'"
                    If (selloc <> "") Then
                        Queryy = Queryy + " and location = " + "'" + selloc + "'"
                    End If
                    If (selid <> "") Then
                        Queryy = Queryy + " and moteid = " + "'" + selid + "'"
                    End If
                Else
                    If (selloc <> "") Then
                        Queryy = Queryy + "location = " + "'" + selloc + "'"
                        If (selid <> "") Then
                            Queryy = Queryy + " and moteid = " + "'" + selid + "'"
                        End If
                    Else
                        If (selid <> "") Then
                            Queryy = Queryy + "moteid = " + "'" + selid + "'"
                        End If
                    End If


                End If
            End If

            mCommand = New MySqlCommand(Queryy, conn)
            ds.SelectCommand = mCommand
            ds.Fill(dt)
            bSource.DataSource = dt
            DataGridView1.DataSource = bSource
            ds.Update(dt)

            conn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            conn.Dispose()
        End Try
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        Dim Queryy As String
        Dim mCommand As MySqlCommand
        Try
            If ComboBox1.Text = "" Then
                seldate = ""
            Else
                seldate = ComboBox1.SelectedItem.ToString()
            End If
            If ComboBox2.Text = "" Then
                selloc = ""
            Else
                selloc = ComboBox2.SelectedItem.ToString()
            End If
            If ComboBox3.Text = "" Then
                selid = ""
            Else
                selid = ComboBox3.SelectedItem.ToString()
            End If
            conn.Open()
            Dim dt = New DataTable()
            Dim ds = New MySqlDataAdapter
            Queryy = "select * from tmote.intrusiondetails"

            If (seldate <> "" Or selloc <> "" Or selid <> "") Then
                Queryy = Queryy + " where "
                If (seldate <> "") Then
                    Queryy = Queryy + "date = " + "'" + seldate + "'"
                    If (selloc <> "") Then
                        Queryy = Queryy + " and location = " + "'" + selloc + "'"
                    End If
                    If (selid <> "") Then
                        Queryy = Queryy + " and moteid = " + "'" + selid + "'"
                    End If
                Else
                    If (selloc <> "") Then
                        Queryy = Queryy + "location = " + "'" + selloc + "'"
                        If (selid <> "") Then
                            Queryy = Queryy + " and moteid = " + "'" + selid + "'"
                        End If
                    Else
                        If (selid <> "") Then
                            Queryy = Queryy + "moteid = " + "'" + selid + "'"
                        End If
                    End If


                End If
            End If

            mCommand = New MySqlCommand(Queryy, conn)
            ds.SelectCommand = mCommand
            ds.Fill(dt)
            bSource.DataSource = dt
            DataGridView1.DataSource = bSource
            ds.Update(dt)

            conn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            conn.Dispose()
        End Try
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim Queryy As String
        Dim mCommand As MySqlCommand
        Try
            If ComboBox1.Text = "" Then
                seldate = ""
            Else
                seldate = ComboBox1.SelectedItem.ToString()
            End If
            If ComboBox2.Text = "" Then
                selloc = ""
            Else
                selloc = ComboBox2.SelectedItem.ToString()
            End If
            If ComboBox3.Text = "" Then
                selid = ""
            Else
                selid = ComboBox3.SelectedItem.ToString()
            End If
            conn.Open()
            Dim dt = New DataTable()
            Dim ds = New MySqlDataAdapter
            Queryy = "select * from tmote.intrusiondetails"

            If (seldate <> "" Or selloc <> "" Or selid <> "") Then
                Queryy = Queryy + " where "
                If (seldate <> "") Then
                    Queryy = Queryy + "date = " + "'" + seldate + "'"
                    If (selloc <> "") Then
                        Queryy = Queryy + " and location = " + "'" + selloc + "'"
                    End If
                    If (selid <> "") Then
                        Queryy = Queryy + " and moteid = " + "'" + selid + "'"
                    End If
                Else
                    If (selloc <> "") Then
                        Queryy = Queryy + "location = " + "'" + selloc + "'"
                        If (selid <> "") Then
                            Queryy = Queryy + " and moteid = " + "'" + selid + "'"
                        End If
                    Else
                        If (selid <> "") Then
                            Queryy = Queryy + "moteid = " + "'" + selid + "'"
                        End If
                    End If


                End If
            End If

            mCommand = New MySqlCommand(Queryy, conn)
            ds.SelectCommand = mCommand
            ds.Fill(dt)
            bSource.DataSource = dt
            DataGridView1.DataSource = bSource
            ds.Update(dt)

            conn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            conn.Dispose()
        End Try
    End Sub
End Class