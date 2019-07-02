Imports KeyVolume_Monitor.Sound
Imports KeyVolume_Monitor.Keyboard
Imports KeyVolume_Monitor.Sound.Line
Imports KeyVolume_Monitor.Sound.Device

Public Class Form1

    Private key As New Keyboard 'Istanzia una nuova classe keyboard
    Private CMenu As New System.Windows.Forms.ContextMenu 'Dichiarazione del ContextMenu
    Dim ds As New Data.DataSet 'Dichiarazione del DataSet per l'XML
    Dim CTRL As String 'Dichiarazione della variabile che conterrà il valore del tasto CTRL registrato sull'XML
    Dim ALT As String '(Come sopra) tast ALT
    Dim SHIFT As String '(Come sopra) tasto SHIFT
    Dim ALoad As String '(Come sopra) proprietà auto-load del servizio di cattura dei tasti

    Dim logitem As MenuItem 'Oggetto del ContextMenu: Start with Login
    Dim startitem As MenuItem 'Oggetto del ContextMenu: start service
    Dim stopitem As MenuItem 'Oggetto del ContextMenu: stop service
    Dim aboutitem As MenuItem 'Oggetto del ContextMenu: about
    Dim exititem As MenuItem 'Oggetto del ContextMenu: exit
    Dim optitem As MenuItem 'Oggetto del ContextMenu: options

    Dim runkey As Microsoft.Win32.RegistryKey = _
    My.Computer.Registry.CurrentUser.OpenSubKey( _
    "Software\Microsoft\Windows\CurrentVersion\Run", _
    True) 'Dichiarazione della variabile che conterrà il percorso del registro per l'esecuzione automatica

    Dim XMLPath As String = My.Computer.FileSystem.CurrentDirectory() & "\" & _
    My.Application.Info.ProductName() & "_options.xml" 'Percorso dell'XML

    Dim XML As String = "<?xml version=""1.0"" encoding=""utf-8"" ?>" & vbCrLf & _
    "<KeyVolumeMonitor_options>" & vbCrLf & _
    "   <property>" & vbCrLf & _
    "       <property-CTRL>" & vbCrLf & _
    "           1" & vbCrLf & _
    "       </property-CTRL>" & vbCrLf & _
    "       <property-ALT>" & vbCrLf & _
    "           0" & vbCrLf & _
    "       </property-ALT>" & vbCrLf & _
    "       <property-SHIFT>" & vbCrLf & _
    "           0" & vbCrLf & _
    "       </property-SHIFT>" & vbCrLf & _
    "       <auto-load>" & vbCrLf & _
    "           0" & vbCrLf & _
    "       </auto-load>" & vbCrLf & _
    "   </property>" & vbCrLf & _
    "</KeyVolumeMonitor_options>" 'Testo dell'XML

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        logitem = New MenuItem 'istanziazione di un nuovo item nel ContextMenu
        startitem = New MenuItem 'istanziazione di un nuovo item nel ContextMenu
        stopitem = New MenuItem 'istanziazione di un nuovo item nel ContextMenu
        aboutitem = New MenuItem 'istanziazione di un nuovo item nel ContextMenu
        exititem = New MenuItem 'istanziazione di un nuovo item nel ContextMenu
        optitem = New MenuItem 'istanziazione di un nuovo item nel ContextMenu

        logitem.Text = "Start with login" 'testo dell'oggetto appena istanziato del ContextMenu
        startitem.Text = "Start service" 'testo dell'oggetto appena istanziato del ContextMenu
        stopitem.Text = "Stop service" 'testo dell'oggetto appena istanziato del ContextMenu
        aboutitem.Text = "About" 'testo dell'oggetto appena istanziato del ContextMenu
        exititem.Text = "Exit" 'testo dell'oggetto appena istanziato del ContextMenu
        optitem.Text = "Options" 'testo dell'oggetto appena istanziato del ContextMenu

        Label1.Text = "Seleziona i tasti di controllo per aumentare e diminuire" & _
        " il volume e premili insieme a PagSu o PagGiù." & _
        vbCrLf & "Premendo i tasti di controllo con ESC si disattiva il volume." 'Testo della label del form opzioni

        If runkey.GetValue(My.Application.Info.ProductName()) = "" Then 'Se il valore non è scritto nel registro allora
            logitem.Checked = False 'metto a false la proprietà checked dell'oggetto logitem (Start with Login)
        Else 'Altrimenti
            logitem.Checked = True 'metto a true la proprietà checked dell'oggetto logitem (Start with Login)
        End If

        optitem.DefaultItem = True 'pongo l'oggetto optitem (options) del ContextMenu come default per il doppioclick sull'icona

        AddHandler logitem.Click, AddressOf Me.logitem_Click 'Creo l'handle per l'evento click dell'oggetto e gli assegno un indirizzo collegato
        AddHandler optitem.Click, AddressOf Me.optitem_Click 'Creo l'handle per l'evento click dell'oggetto e gli assegno un indirizzo collegato
        AddHandler startitem.Click, AddressOf Me.startitem_Click 'Creo l'handle per l'evento click dell'oggetto e gli assegno un indirizzo collegato
        AddHandler stopitem.Click, AddressOf Me.stopitem_Click 'Creo l'handle per l'evento click dell'oggetto e gli assegno un indirizzo collegato
        AddHandler aboutitem.Click, AddressOf Me.aboutitem_Click 'Creo l'handle per l'evento click dell'oggetto e gli assegno un indirizzo collegato
        AddHandler exititem.Click, AddressOf Me.exititem_Click 'Creo l'handle per l'evento click dell'oggetto e gli assegno un indirizzo collegato

        With CMenu.MenuItems 'In un blocco With aggiungo gli items al menu
            .Add(startitem)
            .Add(stopitem)
            .Add(logitem)
            .Add("-")
            .Add(optitem)
            .Add(aboutitem)
            .Add("-")
            .Add(exititem)
        End With

        NI.ContextMenu = CMenu 'Assegno all'icona nella traybar il menu creato, istanziato, popolato e valorizzato

        Me.Hide() 'Nascondo il form delle opzioni alla vista, verrà richiamato con un click su Options nel menu
        Me.Opacity = 0 'Per lo stesso motivo imposto anche l'opacità a 0
        stopitem.Enabled = False 'pongo a false l'oggetto stopitem del ContextMenu

        If My.Computer.FileSystem.FileExists(XMLPath) = False Then 'Se il file XML puntato da XMLPath non esiste allora
            My.Computer.FileSystem.WriteAllText(XMLPath, XML, True) 'Crealo
        End If

        ds.ReadXml(XMLPath) 'Legge il file puntato da XMLPath

        CTRL = ds.Tables("property").Rows(0).Item("property-CTRL") 'Inserisce nella variabile il valore della proprietà specificata
        ALT = ds.Tables("property").Rows(0).Item("property-ALT") 'Inserisce nella variabile il valore della proprietà specificata
        SHIFT = ds.Tables("property").Rows(0).Item("property-SHIFT") 'Inserisce nella variabile il valore della proprietà specificata
        ALoad = ds.Tables("property").Rows(0).Item("auto-load") 'Inserisce nella variabile il valore della proprietà specificata

        If CTRL And ALT And SHIFT = 0 Then 'Se le tre variabili sono a 0 allora
            CTRL = 1 'Imposta CTRL a 1
        End If

        If CTRL = 1 Then 'Se CTRL vale 1 allora
            CBCtrl.Checked = True 'Imposta la proprietà checked del CheckBox a true
        Else 'Altrimenti
            CBCtrl.Checked = False 'Imposta la proprietà checked del CheckBox a false
        End If
        If ALT = 1 Then 'Se ALT vale 1 allora
            CBAlt.Checked = True 'Imposta la proprietà checked del CheckBox a true
        Else 'Altrimenti
            CBAlt.Checked = False 'Imposta la proprietà checked del CheckBox a false
        End If
        If SHIFT = 1 Then 'Se SHIFT vale 1 allora
            CBShift.Checked = True 'Imposta la proprietà checked del CheckBox a true
        Else 'Altrimenti
            CBShift.Checked = False 'Imposta la proprietà checked del CheckBox a false
        End If
        If ALoad = 1 Then 'Se ALoad vale 1 allora
            CBALoad.Checked = True 'Imposta la proprietà checked del CheckBox a true
        Else 'Altrimenti
            CBALoad.Checked = False 'Imposta la proprietà checked del CheckBox a false
        End If

        If CBALoad.Checked = True And stopitem.Enabled = False Then 'Se il CheckBox è selezionato e se stopitem non è selezionato allora
            stopitem.Enabled = True 'stopitem va a true
            startitem.Enabled = False 'startitem va a false
            key.initTimer(CBCtrl.Checked, CBAlt.Checked, CBShift.Checked, 0, 4)
            key.tmrCapture.Enabled = True 'e abilita il timer
        End If

        ApplicaButton.Enabled = False 'Imposta il bottone applica come non selezionabile
    End Sub

    Private Sub logitem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If logitem.Checked = False Then 'Se logitem non è selezionato allora
            logitem.Checked = True 'mandalo a true
            runkey.SetValue(My.Application.Info.ProductName(), My.Computer.FileSystem.CurrentDirectory()) 'e scrivi la chiave nel registro
        Else 'Altrimenti
            logitem.Checked = False 'mandalo a false
            runkey.DeleteValue(My.Application.Info.ProductName()) 'e cancella la chiave dal registro
        End If
    End Sub

    Private Sub startitem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If stopitem.Enabled = False Then 'Se stopitem è disabilitato allora
            stopitem.Enabled = True 'abilitalo
            startitem.Enabled = False 'disabilita startitem
            inputdevline.Show() 'e visualizza la finestra per l'input dei parametri audio
        End If
    End Sub

    Private Sub stopitem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If startitem.Enabled = False Then 'Se startitem è disabilitato allora
            startitem.Enabled = True 'abilitalo
            stopitem.Enabled = False 'disabilita stopitem
            key.tmrCapture.Enabled = False 'e disabilita il timer
        End If
    End Sub

    Private Sub aboutitem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If Me.Visible = True Then 'Se il form è visibile allora
            Me.Hide() 'nascondilo
        End If
        AboutBox1.Show() 'e visualizza l'about
    End Sub

    Private Sub exititem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Close() 'chiudi l'applicazione
    End Sub

    Private Sub optitem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AboutBox1.Visible = True Then 'Se l'about è visibile allora
            AboutBox1.Hide() 'nascondilo
        End If
        Me.Show() 'visualizza il form principale
        Me.Opacity = 100 'e opacizzalo a 100
    End Sub
    Private Sub NI_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NI.MouseDoubleClick
        optitem_Click(sender, e) 'chiama l'evento optitem al doppio click sull'icona
    End Sub

    Private Sub AnnullaButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnnullaButton.Click
        Me.Hide() 'nascondi il form
        Me.Opacity = 0 'e deopacizzalo a 0
    End Sub

    Private Sub ApplicaButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApplicaButton.Click
        ds.Tables("property").Rows(0).Item("property-CTRL") = boolToInt(CBCtrl.Checked) 'assegna al nodo specificato nel DataSet un valore intero
        ds.Tables("property").Rows(0).Item("property-ALT") = boolToInt(CBAlt.Checked) 'assegna al nodo specificato nel DataSet un valore intero
        ds.Tables("property").Rows(0).Item("property-SHIFT") = boolToInt(CBShift.Checked) 'assegna al nodo specificato nel DataSet un valore intero
        ds.Tables("property").Rows(0).Item("auto-load") = boolToInt(CBALoad.Checked) 'assegna al nodo specificato nel DataSet un valore intero
        ds.WriteXml(XMLPath) 'e scrivi il DataSet popolato nel file XML
        ApplicaButton.Enabled = False 'rendi il bottone non cliccabile
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        ApplicaButton_Click(sender, e) 'richiama l'evento ApplicaButton
        AnnullaButton_Click(sender, e) 'richiama l'evento AnnullaButton
    End Sub

    Private Sub CBCtrl_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBCtrl.CheckedChanged
        ApplicaButton.Enabled = True 'Al cambio di stato rendi cliccabile il bottone
    End Sub

    Private Sub CBAlt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBAlt.CheckedChanged
        ApplicaButton.Enabled = True 'Al cambio di stato rendi cliccabile il bottone
    End Sub

    Private Sub CBShift_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBShift.CheckedChanged
        ApplicaButton.Enabled = True 'Al cambio di stato rendi cliccabile il bottone
    End Sub

    Private Sub CBALoad_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBALoad.CheckedChanged
        ApplicaButton.Enabled = True 'Al cambio di stato rendi cliccabile il bottone
    End Sub

    Private Function boolToInt(ByVal bool As Boolean) 'Funzione che converte da boolean a integer
        Dim int As Integer = 0 'dichiaro un intero e lo inizializzo a 0
        If bool = True Then 'Se il boolean è true allora
            int = 1 'l'intero vale 1
        Else 'altrimenti
            int = 0 'l'intero vale 0
        End If
        Return int 'ritorna l'intero calcolato
    End Function
End Class