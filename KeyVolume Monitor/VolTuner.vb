Imports KeyVolume_Monitor.Keyboard
Imports KeyVolume_Monitor.Sound

Public Class VolTuner
    Private line As New Line
    Private Sub OK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OK.Click
        Dim i As Integer = 0
        Try
            i = CType(TextBox1.Text, Integer)
            MessageBox.Show("ciao")
            If devinit = True Then
                line.SetVolume(i)
                MessageBox.Show(i)
            End If
        Catch ex As InvalidCastException
            MessageBox.Show("Quello che hai inserito non è un numero")
        End Try
    End Sub
End Class