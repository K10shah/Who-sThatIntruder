Public Class TaskSelection

    Private Sub AddUserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddUserToolStripMenuItem.Click
        AddUser.Show()
        Me.Close()
    End Sub

    Private Sub DeleteUserToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteUserToolStripMenuItem1.Click
        DeleteUser.Show()
        Me.Close()
    End Sub

    Private Sub SensorControlToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SensorControlToolStripMenuItem.Click
        Monitor.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
        WelcomeLogin.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class