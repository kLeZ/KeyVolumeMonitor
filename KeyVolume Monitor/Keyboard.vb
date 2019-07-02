Option Strict Off
Option Explicit On

Imports KeyVolume_Monitor.Sound
Imports KeyVolume_Monitor.Sound.Line
Imports KeyVolume_Monitor.Sound.Device

Public Class Keyboard

    Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Integer) As Short

    Public WithEvents tmrCapture As New System.Windows.Forms.Timer()
    Public controlk As Boolean
    Public altk As Boolean
    Public shiftk As Boolean
    Public line As New Line
    Public Shared devinit As Boolean

    Public Sub initTimer(ByVal Ctrl As Boolean, ByVal Alt As Boolean, ByVal Shift As Boolean, ByVal deviceid As Integer, ByVal lineid As Integer)
        'Serve a inizializzare i tasti scelti per la cattura e ad attivare la device audio e la linea da catturare
        Dim device As New Device(deviceid)
        line = device.GetLine(lineid)
        tmrCapture.Interval = 100
        tmrCapture.Enabled = False
        controlk = Ctrl
        altk = Alt
        shiftk = Shift
        devinit = True
    End Sub

    Public Sub tmrCapture_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrCapture.Tick
        '-----------------------------------------------------------------------------------------------------------
        'Modifica il volume con PagSu e PagGiù
        If controlk = True And altk = True And shiftk = True Then 'tutti true
            If GetAsyncKeyState(System.Windows.Forms.Keys.LControlKey) And GetAsyncKeyState(System.Windows.Forms.Keys.LMenu) And GetAsyncKeyState(System.Windows.Forms.Keys.LShiftKey) And GetAsyncKeyState(System.Windows.Forms.Keys.PageUp) Then
                'alza il volume
                line.SetVolume(line.GetVolume() + 1)
            ElseIf GetAsyncKeyState(System.Windows.Forms.Keys.LControlKey) And GetAsyncKeyState(System.Windows.Forms.Keys.LMenu) And GetAsyncKeyState(System.Windows.Forms.Keys.LShiftKey) And GetAsyncKeyState(System.Windows.Forms.Keys.PageDown) Then
                'abbassa il volume
                line.SetVolume(line.GetVolume() - 1)
            End If
        ElseIf controlk = True And altk = True And shiftk = False Then 'shift in false
            If GetAsyncKeyState(System.Windows.Forms.Keys.LControlKey) And GetAsyncKeyState(System.Windows.Forms.Keys.LMenu) And GetAsyncKeyState(System.Windows.Forms.Keys.PageUp) Then
                'alza il volume
                line.SetVolume(line.GetVolume() + 1)
            ElseIf GetAsyncKeyState(System.Windows.Forms.Keys.LControlKey) And GetAsyncKeyState(System.Windows.Forms.Keys.LMenu) And GetAsyncKeyState(System.Windows.Forms.Keys.PageDown) Then
                'abbassa il volume
                line.SetVolume(line.GetVolume() - 1)
            End If
        ElseIf controlk = True And altk = False And shiftk = True Then 'alt in false
            If GetAsyncKeyState(System.Windows.Forms.Keys.LControlKey) And GetAsyncKeyState(System.Windows.Forms.Keys.LShiftKey) And GetAsyncKeyState(System.Windows.Forms.Keys.PageUp) Then
                'alza il volume
                line.SetVolume(line.GetVolume() + 1)
            ElseIf GetAsyncKeyState(System.Windows.Forms.Keys.LControlKey) And GetAsyncKeyState(System.Windows.Forms.Keys.LShiftKey) And GetAsyncKeyState(System.Windows.Forms.Keys.PageDown) Then
                'abbassa il volume
                line.SetVolume(line.GetVolume() - 1)
            End If
        ElseIf controlk = False And altk = True And shiftk = True Then 'ctrl in false
            If GetAsyncKeyState(System.Windows.Forms.Keys.LMenu) And GetAsyncKeyState(System.Windows.Forms.Keys.LShiftKey) And GetAsyncKeyState(System.Windows.Forms.Keys.PageUp) Then
                'alza il volume
                line.SetVolume(line.GetVolume() + 1)
            ElseIf GetAsyncKeyState(System.Windows.Forms.Keys.LMenu) And GetAsyncKeyState(System.Windows.Forms.Keys.LShiftKey) And GetAsyncKeyState(System.Windows.Forms.Keys.PageDown) Then
                'abbassa il volume
                line.SetVolume(line.GetVolume() - 1)
            End If
        ElseIf controlk = True And altk = False And shiftk = False Then 'ctrl in true
            If GetAsyncKeyState(System.Windows.Forms.Keys.LControlKey) And GetAsyncKeyState(System.Windows.Forms.Keys.PageUp) Then
                'alza il volume
                line.SetVolume(line.GetVolume() + 1)
            ElseIf GetAsyncKeyState(System.Windows.Forms.Keys.LControlKey) And GetAsyncKeyState(System.Windows.Forms.Keys.PageDown) Then
                'abbassa il volume
                line.SetVolume(line.GetVolume() - 1)
            End If
        ElseIf controlk = False And altk = True And shiftk = False Then 'alt in true
            If GetAsyncKeyState(System.Windows.Forms.Keys.LMenu) And GetAsyncKeyState(System.Windows.Forms.Keys.PageUp) Then
                'alza il volume
                line.SetVolume(line.GetVolume() + 1)
            ElseIf GetAsyncKeyState(System.Windows.Forms.Keys.LMenu) And GetAsyncKeyState(System.Windows.Forms.Keys.PageDown) Then
                'abbassa il volume
                line.SetVolume(line.GetVolume() - 1)
            End If
        ElseIf controlk = False And altk = False And shiftk = True Then 'shift in true
            If GetAsyncKeyState(System.Windows.Forms.Keys.LShiftKey) And GetAsyncKeyState(System.Windows.Forms.Keys.PageUp) Then
                'alza il volume
                line.SetVolume(line.GetVolume() + 1)
            ElseIf GetAsyncKeyState(System.Windows.Forms.Keys.LShiftKey) And GetAsyncKeyState(System.Windows.Forms.Keys.PageDown) Then
                'abbassa il volume
                line.SetVolume(line.GetVolume() - 1)
            End If
        End If

        '-----------------------------------------------------------------------------------------------------------------
        'Setta il muto con ESC
        If controlk = True And altk = True And shiftk = True Then 'tutti true
            If GetAsyncKeyState(System.Windows.Forms.Keys.LControlKey) And GetAsyncKeyState(System.Windows.Forms.Keys.LMenu) And GetAsyncKeyState(System.Windows.Forms.Keys.LShiftKey) And GetAsyncKeyState(System.Windows.Forms.Keys.Escape) Then
                'Muto
                If line.GetMute = True Then
                    line.SetMute(False)
                Else
                    line.SetMute(True)
                End If
            End If
        ElseIf controlk = True And altk = True And shiftk = False Then 'shift in false
            If GetAsyncKeyState(System.Windows.Forms.Keys.LControlKey) And GetAsyncKeyState(System.Windows.Forms.Keys.LMenu) And GetAsyncKeyState(System.Windows.Forms.Keys.Escape) Then
                'Muto
                If line.GetMute = True Then
                    line.SetMute(False)
                Else
                    line.SetMute(True)
                End If
            End If
        ElseIf controlk = True And altk = False And shiftk = True Then 'alt in false
            If GetAsyncKeyState(System.Windows.Forms.Keys.LControlKey) And GetAsyncKeyState(System.Windows.Forms.Keys.LShiftKey) And GetAsyncKeyState(System.Windows.Forms.Keys.Escape) Then
                'Muto
                If line.GetMute = True Then
                    line.SetMute(False)
                Else
                    line.SetMute(True)
                End If
            End If
        ElseIf controlk = False And altk = True And shiftk = True Then 'ctrl in false
            If GetAsyncKeyState(System.Windows.Forms.Keys.LMenu) And GetAsyncKeyState(System.Windows.Forms.Keys.LShiftKey) And GetAsyncKeyState(System.Windows.Forms.Keys.Escape) Then
                'Muto
                If line.GetMute = True Then
                    line.SetMute(False)
                Else
                    line.SetMute(True)
                End If
            End If
        ElseIf controlk = True And altk = False And shiftk = False Then 'ctrl in true
            If GetAsyncKeyState(System.Windows.Forms.Keys.LControlKey) And GetAsyncKeyState(System.Windows.Forms.Keys.Escape) Then
                'Muto
                If line.GetMute = True Then
                    line.SetMute(False)
                Else
                    line.SetMute(True)
                End If
            End If
        ElseIf controlk = False And altk = True And shiftk = False Then 'alt in true
            If GetAsyncKeyState(System.Windows.Forms.Keys.LMenu) And GetAsyncKeyState(System.Windows.Forms.Keys.Escape) Then
                'Muto
                If line.GetMute = True Then
                    line.SetMute(False)
                Else
                    line.SetMute(True)
                End If
            End If
        ElseIf controlk = False And altk = False And shiftk = True Then 'shift in true
            If GetAsyncKeyState(System.Windows.Forms.Keys.LShiftKey) And GetAsyncKeyState(System.Windows.Forms.Keys.Escape) Then
                'Muto
                If line.GetMute = True Then
                    line.SetMute(False)
                Else
                    line.SetMute(True)
                End If
            End If
        End If

        '-------------------------------------------------------------------------------
        If GetAsyncKeyState(System.Windows.Forms.Keys.LWin) And GetAsyncKeyState(System.Windows.Forms.Keys.V) Then
            'Chiama il form VolTuner
            VolTuner.Show()
        End If
    End Sub
End Class