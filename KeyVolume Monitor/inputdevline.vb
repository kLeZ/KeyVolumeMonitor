Imports KeyVolume_Monitor.Sound
Imports KeyVolume_Monitor.Keyboard
Imports KeyVolume_Monitor.Sound.Line
Imports KeyVolume_Monitor.Sound.Device

Public Class inputdevline
    Private key As New Keyboard
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        key.initTimer(Form1.CBCtrl.Checked, Form1.CBAlt.Checked, _
        Form1.CBShift.Checked, CType(UsernameTextBox.Text, Integer), _
        CType(PasswordTextBox.Text, Integer)) 'usa la funzione initTimer della classe keyboard passandole i valori dei CB del device e della line
        key.tmrCapture.Enabled = True 'Abilita il timer
        Me.Close() 'e chiude la form
    End Sub
End Class