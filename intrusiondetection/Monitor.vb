Imports System
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.ComponentModel
Imports System.Threading
Imports System.IO.Ports
Imports System.Media

Public Class Monitor
    Dim Pos1, pos2, cpos1, cpos2 As Point
    Dim myPort As Array
    Dim dec_code As String
    Dim len As Long
    Dim receivedString As String
    Dim conn As New MySqlConnection
    Dim myCommand As New MySqlCommand
    Dim myConnString As String
    Dim inccnt As Integer
    Dim count As Integer = 0
    Dim inputData(200, 4) As String
    Dim colorCount As Integer = 0
    Dim intrusionAt As Integer
    Dim intrusioncount As Integer = 0
    Dim tempcount As Integer = 0
    Dim intrusionflag As Integer = 0
    Dim locate As String = ""
    Dim countofIntrusion As Integer = 0
    Delegate Sub SetTextCallback(ByVal receievedString1 As String)
#Region "moving the sensors"
    Private Sub renew()
        Pos1 = PictureBox2.Location
        cpos1 = Cursor.Position
    End Sub
    Private Sub renew2()
        pos2 = PictureBox3.Location
        cpos2 = Cursor.Position
    End Sub

    Private Sub PictureBox2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox2.MouseDown
        Timer1.Enabled = True
        Timer1.Start()
        renew()
    End Sub

    Private Sub PictureBox2_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox2.MouseUp
        Timer1.Stop()
        renew()
    End Sub

    Private Sub PictureBox3_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox3.MouseDown
        Timer2.Enabled = True
        Timer2.Start()
        renew2()
    End Sub

    Private Sub PictureBox3_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox3.MouseUp
        Timer2.Stop()
        renew2()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        PictureBox2.Location = Pos1 - cpos1 + Cursor.Position
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        PictureBox3.Location = pos2 - cpos2 + Cursor.Position
    End Sub
#End Region
    Private Sub Monitor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'PictureBox2.BackColor = Color.Red
        myPort = IO.Ports.SerialPort.GetPortNames()
        Try
            cmbBaud.Items.Add(115200)
            cmbBaud.Items.Add(57600)

            For i = 0 To UBound(myPort)
                cmbPort.Items.Add(myPort(i))
            Next
            cmbPort.Text = cmbPort.Items.Item(0)
            cmbBaud.Text = cmbBaud.Items.Item(0)
            btnDisconnect.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try

    End Sub
    'Connect Button Code Starts Here ….
    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        SerialPort1.PortName = cmbPort.Text
        SerialPort1.BaudRate = cmbBaud.Text
        SerialPort1.DataBits = 8
        ''SerialPort1.DiscardNull = True
        ''SerialPort1.Handshake = IO.Ports.Handshake.None
        SerialPort1.StopBits = IO.Ports.StopBits.One
        ''SerialPort1.Encoding = System.Text.Encoding.GetEncoding(1252)
        SerialPort1.Open()
        btnConnect.Enabled = False
        btnDisconnect.Enabled = True
    End Sub
    'Connect Button Code Ends Here ….

    'Disconnect Button Code Starts Here ….
    Private Sub btnDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisconnect.Click
        SerialPort1.Close()
        btnConnect.Enabled = True
        btnDisconnect.Enabled = False


    End Sub
    'disonnect ends here

    Private Sub SerialPort1_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        receivedString = SerialPort1.ReadExisting()
        'Console.WriteLine(receivedString)
        ReceivedText(receivedString)
        count = count + 1
        count = count Mod 100
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter("com.text", True)

        file.Close()

    End Sub
    'Serial Port Receiving Code(Invoke) Starts Here ….
    Private Sub ReceivedText(ByVal receivedString1 As String)
        Dim words = receivedString1.Split({". ", ", ", " "}, StringSplitOptions.None)
        Dim out As Integer


        If words.Length > 3 Then
            If Integer.TryParse(words(1), out) And Integer.TryParse(words(0), inputData(count, 0)) And Integer.TryParse(words(2), inputData(count, 2)) And Integer.TryParse(words(3), inputData(count, 3)) Then

                inputData(count, 1) = out
                If (count Mod 30) = 15 Then
                    If out > 4000 Then
                        intrusionflag = 1
                        intrusioncount = 0
                        If Me.InvokeRequired Then
                            Dim x As New SetTextCallback(AddressOf ReceivedText)
                            Me.Label4.Invoke(x, New Object() {receivedString1})
                        Else
                            colorCount = 0
                            If words(0) = "5" Then

                                intrusionAt = 5
                                If PictureBox3.Location.X <= 328 Then
                                    locate = "east"
                                Else
                                    locate = "west"
                                End If
                                If PictureBox3.Location.Y <= 345 Then
                                    locate = "north" + locate
                                Else
                                    locate = "south" + locate
                                End If
                                PictureBox3.BackColor = Color.Red
                                tempcount = count - 1

                                ' To check whether how many previous values are above 4000
                                While (tempcount > -1)
                                    If inputData(tempcount, 0) = 5 Then
                                        If inputData(tempcount, 1) > 4000 Then
                                            intrusioncount = intrusioncount + 1

                                        Else
                                            Exit While
                                        End If
                                    End If
                                    tempcount = tempcount - 1
                                End While


                            ElseIf words(0) = "3" Then
                                intrusionAt = 3
                                If PictureBox2.Location.X <= 328 Then
                                    locate = "east"
                                Else
                                    locate = "west"
                                End If
                                If PictureBox2.Location.Y <= 345 Then
                                    locate = "north" + locate
                                Else
                                    locate = "south" + locate
                                End If

                                PictureBox2.BackColor = Color.Red
                                tempcount = count - 1

                                While (tempcount > -1)
                                    If inputData(tempcount, 0) = 5 Then
                                        If inputData(tempcount, 1) > 4000 Then
                                            intrusioncount = intrusioncount + 1

                                        Else
                                            Exit While
                                        End If
                                    End If
                                    tempcount = tempcount - 1
                                End While


                            End If



                        End If
                    End If
                Else
                    If Me.InvokeRequired Then
                        Dim x As New SetTextCallback(AddressOf ReceivedText)
                        Me.Label4.Invoke(x, New Object() {receivedString1})
                        Me.Label7.Invoke(x, New Object() {receivedString1})
                    Else
                        If intrusionflag Then
                            If count < 200 Then
                                If inputData(count, 0) = intrusionAt Then
                                    If inputData(count, 1) > 4000 Then
                                        intrusioncount = intrusioncount + 1
                                    Else
                                        Dim humanOrVehicle As String = ""
                                        If intrusioncount < 500 And intrusioncount > 20 Then
                                            humanOrVehicle = "Human intrusion suspected"
                                        ElseIf intrusioncount < 20 And intrusioncount > 0 Then
                                            humanOrVehicle = " Vehicle intrusion suspected"
                                        Else
                                            humanOrVehicle = " Unknown intrusion "
                                        End If
                                        Try

                                            myConnString = "Server=localhost;Uid=root;password='';database=tmote"
                                            conn.ConnectionString = myConnString

                                            conn.Open()

                                            myCommand.Connection = conn
                                            myCommand.CommandText = "insert into intrusiondetails(typeofintrusion,moteid,timeofintrusion,location,date) values('" + humanOrVehicle + "'" + ",'" & words(0) & "','" & Date.Now.TimeOfDay.ToString & "','" + locate + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')"
                                            'myCommand.Parameters.AddWithValue("?moteid", words(0))
                                            myCommand.ExecuteNonQuery()
                                            If conn.State = ConnectionState.Open Then
                                                conn.Close()
                                            End If


                                        Catch ex As Exception
                                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        End Try
                                        Dim sp As SoundPlayer
                                        sp = New SoundPlayer(My.Resources.intrusion)
                                        sp.Play()

                                        'MsgBox("Intrusion detected at mote id: " + words(0) + " at " + Date.Now.TimeOfDay.ToString() + " Location : " + locate + " : " + humanOrVehicle, vbInformation)
                                        'intrusion.Show()
                                        Label4.Text = "Intrusion detected at mote id: " + words(0) + " at " + Date.Now.TimeOfDay.ToString() + " Location : " + locate + " : " + humanOrVehicle
                                        countofIntrusion = countofIntrusion + 1
                                        Label7.Text = "Number of intrusion unattended : " + countofIntrusion.ToString()
                                        locate = ""
                                        If intrusionAt = 5 Then
                                            PictureBox3.BackColor = Color.Yellow
                                        Else
                                            PictureBox2.BackColor = Color.Yellow
                                        End If

                                        intrusionflag = 0
                                    End If
                                End If
                                tempcount = tempcount - 1
                            Else
                                intrusionflag = 0
                            End If
                        End If
                    End If
                End If
                    If colorCount = 25 Then
                        PictureBox2.BackColor = Color.Lime
                        PictureBox3.BackColor = Color.Lime
                    End If
                    colorCount = (colorCount + 1) Mod 26
                End If
            End If
    End Sub
    'Serial Port Receiving Code(Invoke) Ends Here ….
    'Serial Port Receiving Code(Invoke) Ends Here ….
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim fdlg As OpenFileDialog = New OpenFileDialog()
        fdlg.Title = "Choose a Profile Photo"
        fdlg.InitialDirectory = "c:\"
        fdlg.Filter = "Picture Files(*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
        fdlg.FilterIndex = 2
        fdlg.RestoreDirectory = True
        If fdlg.ShowDialog() = DialogResult.OK Then
            If File.Exists(fdlg.FileName) = False Then
                MessageBox.Show("Sorry, The File You Specified Does Not Exist.")
            Else
                PictureBox1.ImageLocation = fdlg.FileName
            End If

        End If
    End Sub

    'Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
    '    Thread.Sleep(5000)
    '    PictureBox2.BackColor = Color.Lime
    '    PictureBox3.BackColor = Color.Lime
    'End Sub
    Private Sub cmbPort_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPort.SelectedIndexChanged
        If SerialPort1.IsOpen = False Then
            SerialPort1.PortName = cmbPort.Text
        Else
            MsgBox("Valid only if port is Closed", vbCritical)
        End If
    End Sub
    'Com Port Change Warning Code Ends Here ….

    'Baud Rate Change Warning Code Starts Here ….
    Private Sub cmbBaud_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBaud.SelectedIndexChanged
        If SerialPort1.IsOpen = False Then
            SerialPort1.BaudRate = cmbBaud.Text
        Else
            MsgBox("Valid only if port is Closed", vbCritical)
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Login.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        countofIntrusion = 0
        Label7.Text = "Number of intrusion unattended : " + countofIntrusion.ToString()
    End Sub
End Class