Imports MySql.Data.MySqlClient
Imports System
Imports System.ComponentModel
Public Class WelcomeLogin
    Dim conn As New MySqlConnection
    Dim myCommand As New MySqlCommand
    Dim myConnString As String

    Private Sub WelcomeLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        ToolTip1.AutoPopDelay = 5000
        ToolTip1.InitialDelay = 1000
        ToolTip1.ReshowDelay = 500
        ToolTip1.SetToolTip(Button4, "Contact us")
        Try



            myConnString = "Server=localhost;Uid=root;password='';database=tmote"
            conn.ConnectionString = myConnString


            conn.Open()


            myCommand.Connection = conn
            myCommand.CommandText = "SELECT * FROM userlogin"
            Dim dt = New DataTable()
            Dim ds = New MySqlDataAdapter(myCommand)
            ds.Fill(dt)
            If (dt.Rows.Count > 0) Then
                Button2.Visible = True
            Else
                Button2.Visible = False

            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        AdminLogin.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Login.Show()
        Me.Close()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        ContactUs.Show()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'MsgBox("Contact UsDatabyte Services and Systems.4/31 Old Anand Nagar, \n Bses Road,Santacruz East Mumbai, Maharashtra 400055022 2618 12888 ", vbInformation)
        Help.Show()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        ' MsgBox("Intrusion Detection System using WSN Version 1.0 \n This system  provides a Proof of Concept for detecting and classifying Intrusions such as Human and Vehicle. It will prove to be an efficient low power  system for areas of high security such as Banks,DefenceReserves,Government Buildings.It is based on Tmote Sky’s  Wireless sensor Module Moteiv with WiEye PIR Plugin Sensor Module for Intrusion detection.", vbInformation)
        AboutUS.Show()
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub
End Class
