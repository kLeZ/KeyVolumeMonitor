<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla nell'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.NI = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.CBShift = New System.Windows.Forms.CheckBox
        Me.CBAlt = New System.Windows.Forms.CheckBox
        Me.CBCtrl = New System.Windows.Forms.CheckBox
        Me.OKButton = New System.Windows.Forms.Button
        Me.AnnullaButton = New System.Windows.Forms.Button
        Me.ApplicaButton = New System.Windows.Forms.Button
        Me.CBALoad = New System.Windows.Forms.CheckBox
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'NI
        '
        Me.NI.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.NI.BalloonTipText = "Benvenuto in KeyVolume Monitor"
        Me.NI.BalloonTipTitle = "KeyVolume Monitor 1.0"
        Me.NI.Icon = CType(resources.GetObject("NI.Icon"), System.Drawing.Icon)
        Me.NI.Text = "KeyVolume Monitor"
        Me.NI.Visible = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.CBShift)
        Me.GroupBox1.Controls.Add(Me.CBAlt)
        Me.GroupBox1.Controls.Add(Me.CBCtrl)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(268, 90)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Seleziona i tasti"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(60, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(202, 71)
        Me.Label1.TabIndex = 3
        '
        'CBShift
        '
        Me.CBShift.AutoSize = True
        Me.CBShift.Location = New System.Drawing.Point(7, 67)
        Me.CBShift.Name = "CBShift"
        Me.CBShift.Size = New System.Drawing.Size(47, 17)
        Me.CBShift.TabIndex = 2
        Me.CBShift.Text = "Shift"
        Me.CBShift.UseVisualStyleBackColor = True
        '
        'CBAlt
        '
        Me.CBAlt.AutoSize = True
        Me.CBAlt.Location = New System.Drawing.Point(7, 44)
        Me.CBAlt.Name = "CBAlt"
        Me.CBAlt.Size = New System.Drawing.Size(38, 17)
        Me.CBAlt.TabIndex = 1
        Me.CBAlt.Text = "Alt"
        Me.CBAlt.UseVisualStyleBackColor = True
        '
        'CBCtrl
        '
        Me.CBCtrl.AutoSize = True
        Me.CBCtrl.Location = New System.Drawing.Point(7, 20)
        Me.CBCtrl.Name = "CBCtrl"
        Me.CBCtrl.Size = New System.Drawing.Size(41, 17)
        Me.CBCtrl.TabIndex = 0
        Me.CBCtrl.Text = "Ctrl"
        Me.CBCtrl.UseVisualStyleBackColor = True
        '
        'OKButton
        '
        Me.OKButton.Location = New System.Drawing.Point(12, 132)
        Me.OKButton.Name = "OKButton"
        Me.OKButton.Size = New System.Drawing.Size(85, 23)
        Me.OKButton.TabIndex = 1
        Me.OKButton.Text = "OK"
        Me.OKButton.UseVisualStyleBackColor = True
        '
        'AnnullaButton
        '
        Me.AnnullaButton.Location = New System.Drawing.Point(103, 132)
        Me.AnnullaButton.Name = "AnnullaButton"
        Me.AnnullaButton.Size = New System.Drawing.Size(86, 23)
        Me.AnnullaButton.TabIndex = 2
        Me.AnnullaButton.Text = "Annulla"
        Me.AnnullaButton.UseVisualStyleBackColor = True
        '
        'ApplicaButton
        '
        Me.ApplicaButton.Location = New System.Drawing.Point(195, 132)
        Me.ApplicaButton.Name = "ApplicaButton"
        Me.ApplicaButton.Size = New System.Drawing.Size(85, 23)
        Me.ApplicaButton.TabIndex = 3
        Me.ApplicaButton.Text = "Applica"
        Me.ApplicaButton.UseVisualStyleBackColor = True
        '
        'CBALoad
        '
        Me.CBALoad.AutoSize = True
        Me.CBALoad.Location = New System.Drawing.Point(13, 109)
        Me.CBALoad.Name = "CBALoad"
        Me.CBALoad.Size = New System.Drawing.Size(229, 17)
        Me.CBALoad.TabIndex = 4
        Me.CBALoad.Text = "Avvia automaticamente il servizio di cattura"
        Me.CBALoad.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(292, 167)
        Me.ControlBox = False
        Me.Controls.Add(Me.CBALoad)
        Me.Controls.Add(Me.ApplicaButton)
        Me.Controls.Add(Me.AnnullaButton)
        Me.Controls.Add(Me.OKButton)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Form1"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "KeyVolume Monitor"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents NI As System.Windows.Forms.NotifyIcon
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CBShift As System.Windows.Forms.CheckBox
    Friend WithEvents CBAlt As System.Windows.Forms.CheckBox
    Friend WithEvents CBCtrl As System.Windows.Forms.CheckBox
    Friend WithEvents OKButton As System.Windows.Forms.Button
    Friend WithEvents AnnullaButton As System.Windows.Forms.Button
    Friend WithEvents ApplicaButton As System.Windows.Forms.Button
    Friend WithEvents CBALoad As System.Windows.Forms.CheckBox

End Class
