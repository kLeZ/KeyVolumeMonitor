Imports System
Imports System.Runtime.InteropServices

Public Class Sound

#Region "MIXERLINE_COMPONENTTYPE"
    Public Enum MIXERLINE_COMPONENTTYPE
        DST_FIRST = &H0
        DST_SPEAKERS = (DST_FIRST + 4)

        SRC_FIRST = &H1000
        SRC_UNDEFINED = (SRC_FIRST + 0)
        SRC_DIGITAL = (SRC_FIRST + 1)
        SRC_LINE = (SRC_FIRST + 2)
        SRC_MICROPHONE = (SRC_FIRST + 3)
        SRC_SYNTHESIZER = (SRC_FIRST + 4)
        SRC_COMPACTDISC = (SRC_FIRST + 5)
        SRC_TELEPHONE = (SRC_FIRST + 6)
        SRC_PCSPEAKER = (SRC_FIRST + 7)
        SRC_WAVEOUT = (SRC_FIRST + 8)
        SRC_AUXILIARY = (SRC_FIRST + 9)
        SRC_ANALOG = (SRC_FIRST + 10)
        SRC_LAST = (SRC_FIRST + 10)
    End Enum
#End Region

#Region "Constants"
    Friend Const MMSYSERR_NOERROR As Integer = 0
    Friend Const MMSYSERR_BASE = 0
    Friend Const MMSYSERR_BADDEVICEID = (MMSYSERR_BASE + 2)
    Friend Const MMSYSERR_INVALFLAG = (MMSYSERR_BASE + 10)
    Friend Const MMSYSERR_INVALHANDLE = (MMSYSERR_BASE + 5)
    Friend Const MMSYSERR_INVALPARAM = (MMSYSERR_BASE + 11)
    Friend Const MMSYSERR_NODRIVER = (MMSYSERR_BASE + 6)
    Friend Const MAXPNAMELEN As Integer = 32
    Friend Const MIXER_LONG_NAME_CHARS As Integer = 64
    Friend Const MIXER_SHORT_NAME_CHARS As Integer = 16
    Friend Const MIXERCONTROL_CT_CLASS_FADER As Integer = &H50000000
    Friend Const MIXERCONTROL_CT_UNITS_UNSIGNED As Integer = &H30000
    Friend Const MIXERCONTROL_CT_UNITS_BOOLEAN As Integer = &H10000
    Friend Const MIXERCONTROL_CT_CLASS_SWITCH As Integer = &H20000000
    Friend Const MIXERCONTROL_CONTROLTYPE_FADER As Integer = (MIXERCONTROL_CT_CLASS_FADER Or MIXERCONTROL_CT_UNITS_UNSIGNED)
    Friend Const MIXERCONTROL_CONTROLTYPE_VOLUME As Integer = (MIXERCONTROL_CONTROLTYPE_FADER + 1)
    Friend Const MIXER_GETLINEINFOF_COMPONENTTYPE As Integer = &H3&
    Friend Const MIXER_GETLINECONTROLSF_ONEBYTYPE As Integer = &H2
    Friend Const MIXERCONTROL_CONTROLTYPE_BASS As Integer = (MIXERCONTROL_CONTROLTYPE_FADER + 2)
    Friend Const MIXERCONTROL_CONTROLTYPE_TREBLE As Integer = (MIXERCONTROL_CONTROLTYPE_FADER + 3)
    Friend Const MIXERCONTROL_CONTROLTYPE_EQUALIZER As Integer = (MIXERCONTROL_CONTROLTYPE_FADER + 4)
    Friend Const MIXERCONTROL_CONTROLTYPE_BOOLEAN As Integer = (MIXERCONTROL_CT_CLASS_SWITCH Or MIXERCONTROL_CT_UNITS_BOOLEAN)
    Friend Const MIXERCONTROL_CONTROLTYPE_MUTE As Integer = (MIXERCONTROL_CONTROLTYPE_BOOLEAN + 2)
#End Region

#Region "Structs"

    Friend Structure MIXERCAPS
        Dim wMid As Integer
        Dim wPid As Integer
        Dim vDriverVersion As Long
        Dim szPname As String
        Dim fdwSupport As Long
        Dim cDestinations As Long
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure MIXERCONTROL
        <FieldOffset(0)> Public cbStruct As Integer           '  size in Byte of MIXERCONTROL
        <FieldOffset(4)> Public dwControlID As Integer        '  unique control id for mixer device
        <FieldOffset(8)> Public dwControlType As Integer      '  MIXERCONTROL_CONTROLTYPE_xxx
        <FieldOffset(12)> Public fdwControl As Integer         '  MIXERCONTROL_CONTROLF_xxx
        <FieldOffset(16)> Public cMultipleItems As Integer     '  if MIXERCONTROL_CONTROLF_MULTIPLE set
        <FieldOffset(20), MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=MIXER_SHORT_NAME_CHARS)> Public szShortName As String ' * MIXER_SHORT_NAME_CHARS  ' short name of control
        <FieldOffset(36), MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=MIXER_LONG_NAME_CHARS)> Public szName As String '  * MIXER_LONG_NAME_CHARS ' Integer name of control
        <FieldOffset(100)> Public lMinimum As Integer           '  Minimum value
        <FieldOffset(104)> Public lMaximum As Integer           '  Maximum value
        <FieldOffset(108), MarshalAs(UnmanagedType.ByValArray, SizeConst:=11, ArraySubType:=UnmanagedType.AsAny)> Public reserved() As Integer      '  reserved structure space
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure MIXERCONTROLDETAILS
        <FieldOffset(0)> Public cbStruct As Integer       '  size in Byte of MIXERCONTROLDETAILS
        <FieldOffset(4)> Public dwControlID As Integer    '  control id to get/set details on
        <FieldOffset(8)> Public cChannels As Integer      '  number of channels in paDetails array
        <FieldOffset(12)> Public item As Integer           '  hwndOwner or cMultipleItems
        <FieldOffset(16)> Public cbDetails As Integer      '  size of _one_ details_XX struct
        <FieldOffset(20)> Public paDetails As IntPtr       '  pointer to array of details_XX structs
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure MIXERCONTROLDETAILS_UNSIGNED
        <FieldOffset(0)> Public dwValue As Integer        '  value of the control
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure MIXERLINECONTROLS
        <FieldOffset(0)> Public cbStruct As Integer       '  size in Byte of MIXERLINECONTROLS
        <FieldOffset(4)> Public dwLineID As Integer       '  line id (from MIXERLINE.dwLineID)
        <FieldOffset(8)> Public dwControl As Integer      '  MIXER_GETLINECONTROLSF_ONEBYTYPE
        <FieldOffset(12)> Public cControls As Integer      '  count of controls pmxctrl points to
        <FieldOffset(16)> Public cbmxctrl As Integer       '  size in Byte of _one_ MIXERCONTROL
        <FieldOffset(20)> Public pamxctrl As IntPtr       '  pointer to first MIXERCONTROL array
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure MIXERLINE
        <FieldOffset(0)> Public cbStruct As Integer                '  size of MIXERLINE structure
        <FieldOffset(4)> Public dwDestination As Integer          '  zero based destination index
        <FieldOffset(8)> Public dwSource As Integer               '  zero based source index (if source)
        <FieldOffset(12)> Public dwLineID As Integer               '  unique line id for mixer device
        <FieldOffset(16)> Public fdwLine As Integer                '  state/information about line
        <FieldOffset(20)> Public dwUser As Integer                 '  driver specific information
        <FieldOffset(24)> Public dwComponentType As Integer        '  component type line connects to
        <FieldOffset(28)> Public cChannels As Integer              '  number of channels line supports
        <FieldOffset(32)> Public cConnections As Integer           '  number of connections (possible)
        <FieldOffset(36)> Public cControls As Integer              '  number of controls at this line
        <FieldOffset(40), MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=MIXER_SHORT_NAME_CHARS)> Public szShortName As String  ' * MIXER_SHORT_NAME_CHARS
        <FieldOffset(56), MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=MIXER_LONG_NAME_CHARS)> Public szName As String ' * MIXER_LONG_NAME_CHARS
        <FieldOffset(120)> Public dwType As Integer
        <FieldOffset(124)> Public dwDeviceID As Integer
        <FieldOffset(128)> Public wMid As Integer
        <FieldOffset(132)> Public wPid As Integer
        <FieldOffset(136)> Public vDriverVersion As Integer
        <FieldOffset(135), MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=MAXPNAMELEN)> Public szPname As String ' * MAXPNAMELEN
    End Structure

#End Region

#Region "API Declarations"
    Friend Declare Function mixerGetNumDevs Lib "winmm.dll" () As Integer
    Friend Declare Function mixerGetLineControls Lib "winmm.dll" Alias "mixerGetLineControlsA" (<MarshalAs(UnmanagedType.I4)> ByVal hmxobj As Integer, ByRef pmxlc As MIXERLINECONTROLS, ByVal fdwControls As Integer) As Integer
    Friend Declare Function mixerSetControlDetails Lib "winmm.dll" (<MarshalAs(UnmanagedType.I4)> ByVal hmxobj As Integer, ByRef pmxcd As MIXERCONTROLDETAILS, ByVal fdwDetails As Integer) As Integer
    Friend Declare Function mixerGetLineInfo Lib "winmm.dll" Alias "mixerGetLineInfoA" (<MarshalAs(UnmanagedType.I4)> ByVal hmxobj As Integer, ByRef pmxl As MIXERLINE, ByVal fdwInfo As Integer) As Integer
    Friend Declare Function mixerOpen Lib "winmm.dll" (ByRef phmx As Integer, <MarshalAs(UnmanagedType.U4)> ByVal uMxId As Integer, ByVal dwCallback As Integer, ByVal dwInstance As Integer, ByVal fdwOpen As Integer) As Integer
    Friend Declare Function mixerClose Lib "winmm.dll" (ByVal hm As Integer) As Integer
    Friend Declare Function mixerGetControlDetails Lib "winmm.dll" Alias "mixerGetControlDetailsA" (<MarshalAs(UnmanagedType.I4)> ByVal hmxobj As Integer, ByRef pmxcd As MIXERCONTROLDETAILS, ByVal fdwDetails As Integer) As Integer
#End Region

    Public Class Device
        Public Handle As Integer
        Public ID As Integer
        Public Name As String


        Private Sub CreateDevice(ByVal Identity As String)

            Dim lngReturn As Integer
            Dim i As Integer
            Dim rc As Integer
            Dim DeviceID As Integer
            Dim DeviceName As String = ""
            Dim MixerLine As MIXERLINE

            Try
                If CInt(Identity) >= 0 Then
                    DeviceID = CInt(Identity) + 1
                Else
                    Exit Sub
                End If
            Catch
                DeviceName = Identity
            Finally
            End Try

            Do
                mixerClose(Handle)
                MixerLine = New MIXERLINE

                ' Obtain the Handle struct
                lngReturn = mixerOpen(Handle, i, 0, 0, 0)
                i = i + 1

                ' Error check

                If lngReturn <> 0 And i > mixerGetNumDevs() + 7 Then
                    mixerClose(Handle)
                    Dim ex As New Exception("No one SoundDevice corresponds to DeviceID/DeviceName indicated!")
                    Throw ex
                    Exit Sub
                End If

                MixerLine.cbStruct = Marshal.SizeOf(MixerLine)
                MixerLine.dwComponentType = MIXERLINE_COMPONENTTYPE.DST_SPEAKERS

                ' Obtain a line corresponding to the component type
                rc = mixerGetLineInfo(Handle, MixerLine, MIXER_GETLINEINFOF_COMPONENTTYPE)
            Loop Until MMSYSERR_NOERROR = rc And (DeviceID = MixerLine.dwDeviceID + 1 Or DeviceName Like "*" & MixerLine.szPname & "*")
            ID = MixerLine.dwDeviceID
            Name = MixerLine.szPname

        End Sub

        Public Sub New(ByVal DeviceID As Integer)
            Me.CreateDevice(DeviceID)
        End Sub

        Public Sub New(ByVal DeviceName As String)
            Me.CreateDevice(DeviceName)
        End Sub


        Private Function GetControl(ByVal MixerLine As MIXERLINE, ByVal ControlType As Integer) As MIXERCONTROL
            Dim rc As Integer
            Dim mxlc As New MIXERLINECONTROLS
            Dim Control As New MIXERCONTROL
            Dim pmem As IntPtr


            mxlc.cbStruct = Marshal.SizeOf(mxlc)
            mxlc.dwLineID = MixerLine.dwLineID
            mxlc.dwControl = ControlType
            mxlc.cControls = 0
            mxlc.cbmxctrl = Marshal.SizeOf(Control)

            ' Allocate a buffer for the control
            pmem = Marshal.AllocHGlobal(Marshal.SizeOf(Control))
            mxlc.pamxctrl = pmem

            Control.cbStruct = Marshal.SizeOf(Control)

            ' Get the control
            rc = mixerGetLineControls(Handle, mxlc, MIXER_GETLINECONTROLSF_ONEBYTYPE)

            If (MMSYSERR_NOERROR = rc) Then
                Control = CType(Marshal.PtrToStructure(mxlc.pamxctrl, GetType(MIXERCONTROL)), MIXERCONTROL)
            Else
                Dim ex As New Exception("No one Control corresponds to ControlIndex indicated!")
                Throw ex
                Exit Function
            End If

            Marshal.FreeHGlobal(pmem)
            Return Control
        End Function

        Public Function GetLine(ByVal iComponentType As Integer) As Line
            Dim MixerLine As New MIXERLINE
            Dim rc As Integer

            MixerLine.cbStruct = Marshal.SizeOf(MixerLine)
            MixerLine.dwComponentType = iComponentType

            ' Obtain a line corresponding to the component type
            rc = mixerGetLineInfo(Handle, MixerLine, MIXER_GETLINEINFOF_COMPONENTTYPE)

            If (MMSYSERR_NOERROR = rc) Then
                Dim LineMx As New Line
                LineMx.VolCtrl = GetControl(MixerLine, MIXERCONTROL_CONTROLTYPE_VOLUME)
                LineMx.MuteCtrl = GetControl(MixerLine, MIXERCONTROL_CONTROLTYPE_MUTE)
                LineMx.DeviceHandle = Handle
                LineMx.Name = MixerLine.szName
                Return LineMx
            Else
                Dim ex As New Exception("No one Line corresponds with Component Type indicated!")
                Throw ex
            End If
        End Function
    End Class

    Public Class Line
        Public DeviceHandle As Integer
        Public Name As String
        Friend MuteCtrl As MIXERCONTROL
        Friend VolCtrl As MIXERCONTROL

        Public Sub SetVolume(ByVal Level As Integer)
            If Not (Level >= 0 And Level <= 100) Then
                MessageBox.Show("Molla quel pulsante!")
                Exit Sub
            End If

            ' Then determine the value of the volume and Set the volume!
            Call SetParameter(VolCtrl, CType(VolCtrl.lMaximum * (Level / 100), Integer))
        End Sub

        Public Sub SetMute(ByVal boolMute As Boolean)
            ' Then determine the value of the volume
            If boolMute Then
                ' Mute
                Call SetParameter(MuteCtrl, 1)
            Else
                ' Turn the sound on
                Call SetParameter(MuteCtrl, 0)
            End If
        End Sub

        Private Sub SetParameter(ByVal Control As MIXERCONTROL, ByVal Parameter As Integer)
            Dim rc As Integer

            Dim mxcd As MIXERCONTROLDETAILS
            Dim vol As MIXERCONTROLDETAILS_UNSIGNED
            Dim hptr As IntPtr

            mxcd.item = 0
            mxcd.dwControlID = Control.dwControlID
            mxcd.cbStruct = Marshal.SizeOf(mxcd)
            mxcd.cbDetails = Marshal.SizeOf(vol)

            hptr = Marshal.AllocHGlobal(Marshal.SizeOf(vol))

            ' Allocate a buffer for the control value buffer
            mxcd.paDetails = hptr
            mxcd.cChannels = 1
            vol.dwValue = Parameter

            Marshal.StructureToPtr(vol, hptr, False)

            ' Set the control value
            rc = mixerSetControlDetails(DeviceHandle, mxcd, 0)

            Marshal.FreeHGlobal(hptr)
        End Sub

        Private Function GetParameter(ByVal Control As MIXERCONTROL) As Integer
            Dim rc As Integer

            Dim mxcd As MIXERCONTROLDETAILS
            Dim vol As MIXERCONTROLDETAILS_UNSIGNED
            Dim hptr As IntPtr

            mxcd.item = 0
            mxcd.dwControlID = Control.dwControlID
            mxcd.cbStruct = Marshal.SizeOf(mxcd)
            mxcd.cbDetails = Marshal.SizeOf(vol)

            hptr = Marshal.AllocHGlobal(Marshal.SizeOf(vol))

            ' Allocate a buffer for the control value buffer
            mxcd.paDetails = hptr
            mxcd.cChannels = 1

            Marshal.StructureToPtr(vol, hptr, False)

            ' Obtain the details of the control
            rc = mixerGetControlDetails(DeviceHandle, mxcd, 0)

            vol = CType(Marshal.PtrToStructure(mxcd.paDetails, GetType(MIXERCONTROLDETAILS_UNSIGNED)), MIXERCONTROLDETAILS_UNSIGNED)

            Marshal.FreeHGlobal(hptr)

            ' Return of the parameter!
            Return vol.dwValue
        End Function

        Public Function GetVolume() As Integer
            ' Return of the current volume!
            Return CType((GetParameter(VolCtrl) * 100) / VolCtrl.lMaximum, Integer)
        End Function

        Public Function GetMute() As Boolean
            ' Return of the Value of current Mute!
            If GetParameter(MuteCtrl) = 1 Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Class