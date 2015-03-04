'Imports EITitor.My
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.IO

Namespace eit_file
    Public Class eit
        Private ReadOnly Desc_Short_Event As Byte

        Private ReadOnly Desc_Extended_Event As Byte

        Private ReadOnly Desc_Componente As Byte

        Private ReadOnly Desc_Content As Byte

        Private ReadOnly Desc_HDVideo As Byte

        Private ReadOnly Desc_HDAudio As Byte

        Private ReadOnly Desc_Untertitel As Byte

        Private ReadOnly Desc_Audio As Byte

        Private ReadOnly Desc_Video As Byte

        Private Event_Startzeit As String

        Private Event_Duration As String

        Private Event_Language As String

        Private Event_Name As String

        Private Event_Description As String

        Private RunningStatus As Byte

        Private CA_Modus As Integer

        Private DLoop_Length_Hi As Byte

        Private DLoop_Lenght_Lo As Byte

        Private DLoop_Length As UShort

        Private Event_Type As String

        Private Event_Audio As String(,)

        Private Event_Picture As String

        Private FileLenght As Integer

        Private Event_ID_Hi As Byte

        Private Event_ID_Lo As Byte

        Private Event_VPID As String

        Public parseEventData As String(,)

        Public streamdata As Byte()

        Private Audio_Count As Integer

        Private HDAudio As Boolean

        Private HDVideo As Boolean

        Public BigEndian As Boolean = True
        Public AnzahlByte As Integer = 4096

        Public Property Audio() As Array
            Get
                Return Me.Event_Audio
            End Get
            Set(ByVal value As Array)
                Me.Event_Audio = DirectCast(value, String(,))
            End Set
        End Property

        Public Property Beschreibung() As String
            Get
                Return Me.Event_Description
            End Get
            Set(ByVal value As String)
                Me.Event_Description = eit.STrim(value)
            End Set
        End Property

        Public Property Kurzbeschreibung() As String
            Get
                Return Me.Event_Description
            End Get
            Set(ByVal value As String)
                Me.Event_Description = eit.STrim(value)
            End Set
        End Property

        Public Property Dauer() As String
            Get
                Return Me.Event_Duration
            End Get
            Set(ByVal value As String)
                Me.Event_Duration = eit.STrim(value)
                If (Me.Event_Duration.Length > 8) Then
                    Me.Event_Duration = Strings.Right(Me.Event_Duration, 8)
                End If
            End Set
        End Property

        Public Property eventName() As String
            Get
                Return Me.Event_Name
            End Get
            Set(ByVal value As String)
                Me.Event_Name = eit.STrim(value)
            End Set
        End Property

        Public Property FileSize() As Integer
            Get
                Return Me.FileLenght
            End Get
            Set(ByVal value As Integer)
                Me.FileLenght = value
            End Set
        End Property

        Public Property Picture() As String
            Get
                Return Me.Event_Picture
            End Get
            Set(ByVal value As String)
                Me.Event_Picture = eit.STrim(value)
            End Set
        End Property

        Public Property Sprache() As String
            Get
                Return Me.Event_Language
            End Get
            Set(ByVal value As String)
                Me.Event_Language = eit.STrim(value)
            End Set
        End Property

        Public Property Typ() As String
            Get
                Return Me.Event_Type
            End Get
            Set(ByVal value As String)
                Me.Event_Type = eit.STrim(value)
            End Set
        End Property

        Public Property Verschluesselung() As Integer
            Get
                Return Me.CA_Modus
            End Get
            Set(ByVal value As Integer)
                Me.CA_Modus = value
            End Set
        End Property

        Public Property VPid() As String
            Get
                Return Me.Event_VPID
            End Get
            Set(ByVal value As String)
                Me.Event_VPID = value
            End Set
        End Property

        Public Property Zeit() As String
            Get
                Return Me.Event_Startzeit
            End Get
            Set(ByVal value As String)
                Me.Event_Startzeit = eit.STrim(value)
            End Set
        End Property
        Private _dateiname As String
        Public ReadOnly Property Dateiname() As String
            Get
                Return _dateiname
            End Get
        End Property
        Public Sub New()
            MyBase.New()
            Me.Desc_Short_Event = 77
            Me.Desc_Extended_Event = 78
            Me.Desc_Componente = 80
            Me.Desc_Content = 84
            Me.Desc_HDVideo = 245
            Me.Desc_HDAudio = 244
            Me.Desc_Untertitel = 243
            Me.Desc_Audio = 242
            Me.Desc_Video = 241
            Me.Event_Startzeit = ""
            Me.Event_Duration = ""
            Me.Event_Language = ""
            Me.Event_Name = ""
            Me.Event_Description = ""
            Me.RunningStatus = 0
            Me.DLoop_Length_Hi = 0
            Me.DLoop_Lenght_Lo = 0
            Me.DLoop_Length = 0
            Me.Event_Type = ""
            ReDim Me.Event_Audio(4, 4)
            Me.Event_Picture = ""
            Me.Event_ID_Hi = 0
            Me.Event_ID_Lo = 0
            ReDim Me.parseEventData(21, 2)
            ReDim Me.streamdata(4097)
        End Sub

        Public Sub Clear()
            Me.Event_Startzeit = ""
            Me.Event_Duration = ""
            Me.Event_Language = ""
            Me.Event_Name = ""
            Me.Event_Description = ""
            Me.RunningStatus = 0
            Me.CA_Modus = 0
            Me.DLoop_Length_Hi = 0
            Me.DLoop_Lenght_Lo = 0
            Me.DLoop_Length = 0
            Me.Event_Type = ""
            ReDim Me.Event_Audio(4, 4)
            Me.Event_Picture = ""
            ReDim Me.streamdata(6021)
            Dim num As Integer = 0
            Do
                Me.streamdata(num) = 0
                num = num + 1
            Loop While num <= 6020
            Dim i As Integer = 0
            Do
                Me.parseEventData(i, 0) = ""
                Me.parseEventData(i, 1) = ""
                i = i + 1
            Loop While i <= 20
        End Sub

        Public Sub GetAudio(ByVal Zeiger As Integer)
            Me.Event_Audio(Me.Audio_Count, 1) = Conversions.ToString(Me.GetString(Zeiger + 5, 3))
            Me.Event_Audio(Me.Audio_Count, 1) = Strings.StrConv(Me.Event_Audio(Me.Audio_Count, 1), VbStrConv.Lowercase, 0)
            Me.Event_Audio(Me.Audio_Count, 2) = Conversions.ToString(Me.GetString(Zeiger + 8, Me.streamdata(Zeiger + 1) - 6))
            Me.Event_Audio(Me.Audio_Count, 3) = Me.getPID(Zeiger + 3)
            Me.Audio_Count = Me.Audio_Count + 1
            Me.HDAudio = False
        End Sub


        Public Sub GetHDAudio(ByVal Zeiger As Integer)
            Me.Event_Audio(Me.Audio_Count, 1) = Conversions.ToString(Me.GetString(Zeiger + 5, 3))
            Me.Event_Audio(Me.Audio_Count, 1) = Strings.StrConv(Me.Event_Audio(Me.Audio_Count, 1), VbStrConv.Lowercase, 0)
            Me.Event_Audio(Me.Audio_Count, 2) = Conversions.ToString(Me.GetString(Zeiger + 8, Me.streamdata(Zeiger + 1) - 6))
            Me.Event_Audio(Me.Audio_Count, 3) = Me.getPID(Zeiger + 3)
            Me.Audio_Count = Me.Audio_Count + 1
            Me.HDAudio = True
        End Sub

        Public Sub GetHDVideo(ByVal Zeiger As Integer)
            Me.Event_Picture = Conversions.ToString(Me.GetString(Zeiger + 8, Me.streamdata(Zeiger + 1) - 6))
            Me.Event_VPID = Me.getPID(Zeiger + 3)
            Me.HDVideo = True
        End Sub

        Private Function getPID(ByVal start As Integer) As String
            Dim part1 As String = Conversion.Hex(Me.streamdata(start))
            Dim part2 As String = Conversion.Hex(Me.streamdata(start + 1))
            If (part1.Length = 1) Then
                part1 = String.Format("0{0}", part1)
            End If
            If (part2.Length = 1) Then
                part2 = String.Format("0{0}", part2)
            End If
            Return String.Concat(part1, part2)
        End Function

        Public Sub GetDescExtended(ByVal Zeiger As Integer)
            Me.Event_Description = Conversions.ToString(Operators.ConcatenateObject(Me.Event_Description, Me.GetString(Zeiger + 8, CInt(Me.streamdata(Zeiger + 7)))))
            Me.Event_Description = eit.STrim(Me.Event_Description)
        End Sub

        Public Sub GetShortDescription(ByVal Zeiger As Integer)
            Me.Event_Language = Conversions.ToString(Me.GetString(Zeiger + 2, 3))
            Me.Event_Name = Conversions.ToString(Me.GetString(Zeiger + 6, CInt(Me.streamdata(Zeiger + 5))))
            Dim Pointer As Integer = Zeiger + 7 + Me.streamdata(Zeiger + 5)
            Me.Event_Type = Conversions.ToString(Me.GetString(Pointer, CInt(Me.streamdata(Pointer - 1))))
        End Sub

        Private Function GetString(ByVal Start As Integer, ByVal Anzahl As Integer) As Object
            Dim TestString As String = ""
            Dim num As Integer = Start + Anzahl - 1
            Dim Zeiger As Integer = Start
            Do
                If (Me.streamdata(Zeiger) = 13) Then
                    TestString = String.Concat(TestString, "" & vbCrLf & "")
                ElseIf (Me.streamdata(Zeiger) >= 32) Then
                    TestString = If(Me.streamdata(Zeiger) <> 138, String.Concat(TestString, Conversions.ToString(Strings.Chr(CInt(Me.streamdata(Zeiger))))), String.Concat(TestString, "" & vbCrLf & ""))
                Else
                    TestString = String.Format("{0} ", TestString)
                End If
                Zeiger = Zeiger + 1
            Loop While Zeiger <= num
            Return eit.STrim(TestString)
        End Function

        Private Function GetTime(ByVal StartByte As Object) As String
            Dim mhour As String = Conversion.Hex(Me.streamdata(Conversions.ToInteger(StartByte)))
            Dim mMinute As String = Conversion.Hex(Me.streamdata(Conversions.ToInteger(Operators.AddObject(StartByte, 1))))
            Dim mSecunde As String = Conversion.Hex(Me.streamdata(Conversions.ToInteger(Operators.AddObject(StartByte, 2))))
            If (Strings.Len(mhour) = 1) Then
                mhour = String.Format("0{0}", mhour)
            End If
            If (Strings.Len(mMinute) = 1) Then
                mMinute = String.Format("0{0}", mMinute)
            End If
            If (Strings.Len(mSecunde) = 1) Then
                mSecunde = String.Format("0{0}", mSecunde)
            End If
            Return String.Format("{0}:{1}:{2}", mhour, mMinute, mSecunde)
        End Function

        Public Sub GetVideo(ByVal Zeiger As Integer)
            Me.Event_Picture = Conversions.ToString(Me.GetString(Zeiger + 8, Me.streamdata(Zeiger + 1) - 6))
            Me.Event_VPID = Me.getPID(Zeiger + 3)
        End Sub

        Public Sub parseEvent(ByVal description As String)
            Dim length As Integer = description.Length / 249 + 1
            For count As Integer = 0 To length
                If (description.Length <= 249) Then
                    Me.parseEventData(count, 0) = description
                    Me.parseEventData(count, 1) = Conversions.ToString(description.Length)
                    Return
                End If
                Me.parseEventData(count, 0) = Strings.Left(description, 249)
                Me.parseEventData(count, 1) = "249"
                description = Strings.Right(description, description.Length - 249)
            Next

        End Sub

        Private Shared Function ParseTime(ByVal T1 As Byte, ByVal T2 As Byte, ByVal T3 As Byte, ByVal T4 As Byte, ByVal T5 As Byte) As String
            Dim Stunde As String = Conversion.Hex(T3)
            Dim Minute As String = Conversion.Hex(T4)
            Dim Sekunde As String = Conversion.Hex(T5)
            If (Strings.Len(Stunde) = 1) Then
                Stunde = String.Format("0{0}", Stunde)
            End If
            If (Strings.Len(Minute) = 1) Then
                Minute = String.Format("0{0}", Minute)
            End If
            If (Strings.Len(Sekunde) = 1) Then
                Sekunde = String.Format("0{0}", Sekunde)
            End If
            Dim mjd As Integer = T1 * 256 + T2
            Dim datum As DateTime = DateAndTime.DateAdd("d", CDbl(mjd), "17.11.1858")
            Dim result As String = Strings.Left(datum.ToString(), 10)
            Dim objArray() As Object = {result, Stunde, Minute, Sekunde}
            Return String.Format("{0} {1}:{2}:{3}", objArray)
        End Function

        Private Shared Function ParseToDVBTime(ByVal MJD As String) As Integer
            Dim num As Integer
            Dim ReferenzDatum As DateTime = New DateTime(1858, 11, 17)
            If (Operators.CompareString(MJD, Nothing, False) = 0) Then
                num = 0
            Else
                num = CInt(DateAndTime.DateDiff("d", ReferenzDatum, MJD, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1))
                If (num < 0) Then
                    num = 0
                End If
            End If
            Return num
        End Function

        Public Sub readStreamData(ByVal filename As String)
            Dim rs As FileStream
            Dim flag As Boolean = False
            Dim path As String = filename
            _dateiname = filename
            Dim mode As FileMode = FileMode.Open
            Dim access As FileAccess = FileAccess.Read
            Dim secondTry As Boolean = False
            Try
                rs = New FileStream(path, mode, access)
            Catch exception As System.Exception
                ProjectData.SetProjectError(exception)
                rs = Nothing
                ProjectData.ClearProjectError()
                flag = True
            End Try
            If (Not flag) Then
                Dim Zeiger As Integer = 12
                Me.Audio_Count = 0
                rs = File.OpenRead(path)
                Me.Clear()
                rs.Read(Me.streamdata, 0, CInt(rs.Length))
                Me.FileLenght = CInt(rs.Length)
                Me.Event_ID_Hi = Me.streamdata(0)
                Me.Event_ID_Lo = Me.streamdata(1)
                Me.Event_Duration = Me.GetTime(7)
                Me.Event_Startzeit = eit.ParseTime(Me.streamdata(2), Me.streamdata(3), Me.streamdata(4), Me.streamdata(5), Me.streamdata(6))
                Me.DLoop_Lenght_Lo = Me.streamdata(11)

                If (Not Me.BigEndian) Then
                    Me.DLoop_Length_Hi = CByte((CByte((Me.streamdata(10) >> 4)) << 4))
                    Me.CA_Modus = CByte((CByte((Me.streamdata(10) << 4)) >> 7))
                    Me.RunningStatus = CByte((Me.streamdata(10) << 5))
                Else
                    Me.RunningStatus = CByte((Me.streamdata(10) >> 5))
                    Me.CA_Modus = CByte((CByte((Me.streamdata(10) >> 4)) << 7))
                    Me.DLoop_Length_Hi = CByte((CByte((Me.streamdata(10) << 4)) >> 4))
                End If
                Me.DLoop_Length = Convert.ToUInt16(Me.DLoop_Length_Hi * 256 + Me.DLoop_Lenght_Lo)
                If (Me.CA_Modus = 128) Then
                    Me.CA_Modus = 1
                End If
                While Convert.ToUInt64(Me.streamdata(Zeiger + 1)) < rs.Length
                    Dim num As Byte = Me.streamdata(Zeiger)
                    If (num = Me.Desc_Short_Event) Then
                        Me.GetShortDescription(Zeiger)
                        secondTry = False
                    ElseIf (num = Me.Desc_Extended_Event) Then
                        Me.GetDescExtended(Zeiger)
                        secondTry = False
                    ElseIf (num = Me.Desc_Content) Then
                        secondTry = False
                    ElseIf (num = Me.Desc_Componente) Then
                        If (Me.Audio_Count <= 3) Then
                            Dim num1 As Byte = Me.streamdata(Zeiger + 2)
                            If (num1 = Me.Desc_HDVideo) Then
                                Me.GetHDVideo(Zeiger)
                            ElseIf (num1 = Me.Desc_HDAudio) Then
                                Me.GetHDAudio(Zeiger)
                            ElseIf (num1 <> Me.Desc_Untertitel) Then
                                If (num1 = Me.Desc_Audio) Then
                                    Me.GetAudio(Zeiger)
                                ElseIf (num1 = Me.Desc_Video) Then
                                    Me.GetVideo(Zeiger)
                                End If
                            End If
                        End If
                        secondTry = False
                    ElseIf (num = 95) Then
                        secondTry = False
                    ElseIf (num = 101) Then
                        secondTry = False
                    ElseIf (num = 105) Then
                        secondTry = False
                    ElseIf (num <> 130) Then
                        If (Not (Not secondTry And CLng(Zeiger) < rs.Length) OrElse Interaction.MsgBox(String.Format("Fehlerhaftes eit-File{0}Nochmal versuchen ?", "" & vbCrLf & ""), MsgBoxStyle.YesNo Or MsgBoxStyle.Critical, "Lesefehler") <> MsgBoxResult.Yes) Then
                            Exit While
                        End If
                        secondTry = True
                        Zeiger = Zeiger + 1
                    Else
                        secondTry = False
                    End If
                    If (secondTry) Then
                        Continue While
                    End If
                    Zeiger = Zeiger + Me.streamdata(Zeiger + 1) + 2
                End While
            End If
            rs.Close()
            rs = Nothing
            flag = False
        End Sub

        Private Shared Function STrim(ByVal TestString As String) As String
            If (Operators.CompareString(Strings.Left(TestString, 1), " ", False) = 0) Then
                TestString = Strings.Right(TestString, TestString.Length - 1)
            End If
            If (Operators.CompareString(Strings.Left(TestString, 1), "", False) = 0) Then
                TestString = Strings.Right(TestString, TestString.Length - 1)
            End If
            If (Operators.CompareString(Strings.Right(TestString, 1), "", False) = 0) Then
                TestString = Strings.Left(TestString, TestString.Length - 1)
            End If
            Return TestString
        End Function

        Private Shared Function toBCD(ByVal dec As Integer) As Integer
            If (dec > 100) Then
            End If
            Return dec / 10 * 16 + dec Mod 10
        End Function

        Public Sub writeStreamData(ByVal filename As String, ByVal mExist As Boolean)
            Dim offset As Integer
            Dim rs As FileStream
            Dim w As BinaryWriter
            Dim flag As Boolean = False
            Dim path As String = filename
            Dim access As FileAccess = FileAccess.Write
            Dim Zeiger As Integer = 0
            If (Me.AnzahlByte >= 4096) Then
                Interaction.MsgBox("Die maximale Dateigröße wurde erreicht. Bitte die Beschreibung verkürzen.", MsgBoxStyle.Critical, "Dateifehler")
                Return
            End If
            ReDim Me.streamdata(4097)
            Zeiger = 0
            Do
                Me.streamdata(Zeiger) = 0
                Zeiger = Zeiger + 1
            Loop While Zeiger <= 4096
            If (Me.Event_ID_Hi = 0) Then
                Me.streamdata(0) = 123
            Else
                Me.streamdata(0) = Me.Event_ID_Hi
            End If
            If (Me.Event_ID_Lo = 0) Then
                Me.streamdata(1) = 123
            Else
                Me.streamdata(1) = Me.Event_ID_Lo
            End If
            Dim Puffer As Integer = eit.ParseToDVBTime(Strings.Left(Me.Event_Startzeit, 10))
            Me.streamdata(2) = CByte((Puffer >> 8))
            Me.streamdata(3) = CByte((Puffer - Me.streamdata(2) * 256))
            If (Operators.CompareString(Me.Event_Startzeit, Nothing, False) <> 0) Then
                Me.streamdata(4) = CByte(eit.toBCD(Conversions.ToInteger(Strings.Mid(Me.Event_Startzeit, 12, 2))))
                Me.streamdata(5) = CByte(eit.toBCD(Conversions.ToInteger(Strings.Mid(Me.Event_Startzeit, 15, 2))))
                Me.streamdata(6) = CByte(eit.toBCD(Conversions.ToInteger(Strings.Right(Me.Event_Startzeit, 2))))
                Me.streamdata(7) = CByte(eit.toBCD(Conversions.ToInteger(Strings.Left(Me.Event_Duration, 2))))
                Me.streamdata(8) = CByte(eit.toBCD(Conversions.ToInteger(Strings.Mid(Me.Event_Duration, 4, 2))))
                Me.streamdata(9) = CByte(eit.toBCD(Conversions.ToInteger(Strings.Right(Me.Event_Duration, 2))))
            Else
                Me.streamdata(4) = 0
                Me.streamdata(5) = 0
                Me.streamdata(6) = 0
                Me.streamdata(7) = 0
                Me.streamdata(8) = 0
                Me.streamdata(9) = 0
            End If
            Me.streamdata(12) = 77
            Me.streamdata(13) = CByte((Me.Event_Name.Length + 5 + Me.Event_Type.Length))
            Me.streamdata(14) = CByte(Strings.Asc(Strings.Left(Me.Event_Language, 1)))
            Me.streamdata(15) = CByte(Strings.Asc(Strings.Mid(Me.Event_Language, 2, 1)))
            Me.streamdata(16) = CByte(Strings.Asc(Strings.Right(Me.Event_Language, 1)))
            Me.streamdata(17) = CByte(Me.Event_Name.Length)
            Zeiger = 18
            Dim length As Integer = Zeiger + Me.Event_Name.Length - 1
            Zeiger = 18
            Do
                Me.streamdata(Zeiger) = CByte(Strings.Asc(Strings.Mid(Me.Event_Name, Zeiger - 17, 1)))
                Zeiger = Zeiger + 1
            Loop While Zeiger <= length
            Me.streamdata(Zeiger) = CByte(Me.Event_Type.Length)
            Zeiger = Zeiger + 1
            If (Operators.CompareString(Me.Event_Type, Nothing, False) <> 0) Then
                Dim num As Integer = Me.Event_Type.Length
                For offset = 1 To num
                    Me.streamdata(Zeiger) = CByte(Strings.Asc(Strings.Mid(Me.Event_Type, offset, 1)))
                    Zeiger = Zeiger + 1
                Next

            End If
            offset = 0
            Do
                If (Operators.CompareString(Me.Event_Audio(offset, 1), Nothing, False) <> 0) Then
                    Me.streamdata(Zeiger) = 80
                    Zeiger = Zeiger + 1
                    Me.streamdata(Zeiger) = CByte((Me.Event_Audio(offset, 1).Length + 6))
                    Zeiger = Zeiger + 1
                    If (Not Me.HDAudio) Then
                        Me.streamdata(Zeiger) = 242
                    Else
                        Me.streamdata(Zeiger) = 244
                    End If
                    Zeiger = Zeiger + 1
                    Me.streamdata(Zeiger) = CByte(Convert.ToInt32(Strings.Left(Me.Event_Audio(offset, 2), 2), 16))
                    Zeiger = Zeiger + 1
                    Me.streamdata(Zeiger) = CByte(Convert.ToInt32(Strings.Right(Me.Event_Audio(offset, 2), 2), 16))
                    Zeiger = Zeiger + 1
                    Me.streamdata(Zeiger) = CByte(Strings.Asc(Strings.Left(Me.Event_Audio(offset, 0), 1)))
                    Zeiger = Zeiger + 1
                    Me.streamdata(Zeiger) = CByte(Strings.Asc(Strings.Mid(Me.Event_Audio(offset, 0), 2, 1)))
                    Zeiger = Zeiger + 1
                    Me.streamdata(Zeiger) = CByte(Strings.Asc(Strings.Right(Me.Event_Audio(offset, 0), 1)))
                    Zeiger = Zeiger + 1
                    Dim length1 As Integer = Me.Event_Audio(offset, 1).Length
                    For Count As Integer = 1 To length1
                        Me.streamdata(Zeiger) = CByte(Strings.Asc(Strings.Mid(Me.Event_Audio(offset, 1), Count, 1)))
                        Zeiger = Zeiger + 1
                    Next

                End If
                offset = offset + 1
            Loop While offset <= 3
            If (Operators.CompareString(Me.Event_Picture, Nothing, False) <> 0) Then
                Me.streamdata(Zeiger) = 80
                Zeiger = Zeiger + 1
                Me.streamdata(Zeiger) = CByte((Me.Picture.Length + 6))
                Zeiger = Zeiger + 1
                If (Not Me.HDVideo) Then
                    Me.streamdata(Zeiger) = 241
                Else
                    Me.streamdata(Zeiger) = 245
                End If
                Zeiger = Zeiger + 1
                Me.streamdata(Zeiger) = CByte(Convert.ToInt32(Strings.Left(Me.VPid, 2), 16))
                Zeiger = Zeiger + 1
                Me.streamdata(Zeiger) = CByte(Convert.ToInt32(Strings.Right(Me.VPid, 2), 16))
                Zeiger = Zeiger + 1
                Me.streamdata(Zeiger) = CByte(Strings.Asc(Strings.Left(Me.Event_Language, 1)))
                Zeiger = Zeiger + 1
                Me.streamdata(Zeiger) = CByte(Strings.Asc(Strings.Mid(Me.Event_Language, 2, 1)))
                Zeiger = Zeiger + 1
                Me.streamdata(Zeiger) = CByte(Strings.Asc(Strings.Right(Me.Event_Language, 1)))
                Zeiger = Zeiger + 1
                Dim num1 As Integer = Me.Event_Picture.Length
                For offset = 1 To num1
                    Me.streamdata(Zeiger) = CByte(Strings.Asc(Strings.Mid(Me.Event_Picture, offset, 1)))
                    Zeiger = Zeiger + 1
                Next

            End If
            If (Operators.CompareString(Me.Event_Description, Nothing, False) <> 0) Then
                Me.parseEvent(Me.Event_Description)
                Dim Anzahl As Integer = 0
                Dim maxAnzahl As Integer = 0
                While Operators.CompareString(Me.parseEventData(maxAnzahl, 1), "", False) <> 0
                    maxAnzahl = maxAnzahl + 1
                End While
                For Anzahl = 0 To Anzahl
                    Me.streamdata(Zeiger) = 78
                    Zeiger = Zeiger + 1
                    Me.streamdata(Zeiger) = CByte((Conversions.ToInteger(Me.parseEventData(Anzahl, 1)) + 6))
                    Zeiger = Zeiger + 1
                    Me.streamdata(Zeiger) = CByte(((Anzahl << 4) + maxAnzahl - 1))
                    Zeiger = Zeiger + 1
                    Me.streamdata(Zeiger) = CByte(Strings.Asc(Strings.Left(Me.Event_Language, 1)))
                    Zeiger = Zeiger + 1
                    Me.streamdata(Zeiger) = CByte(Strings.Asc(Strings.Mid(Me.Event_Language, 2, 1)))
                    Zeiger = Zeiger + 1
                    Me.streamdata(Zeiger) = CByte(Strings.Asc(Strings.Right(Me.Event_Language, 1)))
                    Zeiger = Zeiger + 1
                    Me.streamdata(Zeiger) = 0
                    Zeiger = Zeiger + 1
                    Me.streamdata(Zeiger) = CByte(Me.parseEventData(Anzahl, 0).Length)
                    Zeiger = Zeiger + 1
                    Dim length2 As Integer = Me.parseEventData(Anzahl, 0).Length
                    For offset = 1 To length2
                        If (Operators.CompareString(Strings.Mid(Me.parseEventData(Anzahl, 0), offset, 1), Conversions.ToString(Strings.Chr(255)), False) <> 0) Then
                            Me.streamdata(Zeiger) = CByte(Strings.Asc(Strings.Mid(Me.parseEventData(Anzahl, 0), offset, 1)))
                            Zeiger = Zeiger + 1
                        Else
                            Me.streamdata(Zeiger) = 138
                            Zeiger = Zeiger + 1
                        End If
                    Next

                Next

            End If
            If (Me.BigEndian) Then
                Me.DLoop_Length_Hi = CByte(Math.Round(Conversion.Fix(CDbl((Zeiger - 13)) / 255)))
                Me.DLoop_Lenght_Lo = CByte((Zeiger - 13 - Me.DLoop_Length_Hi * 255))
                Me.streamdata(10) = Me.RunningStatus
                If (Me.CA_Modus = 1) Then
                    Me.CA_Modus = 128
                End If
                Me.streamdata(10) = CByte((CByte((Me.streamdata(10) << 1)) + Me.CA_Modus))
                Me.streamdata(10) = DirectCast((CByte((CByte((Me.streamdata(10) << 4)) + Me.DLoop_Length_Hi))), Byte)
                Me.streamdata(11) = Me.DLoop_Lenght_Lo
            End If
            Dim mode As FileMode = FileMode.OpenOrCreate
            Try
                rs = New FileStream(path, mode, access)
                w = New BinaryWriter(rs)
            Catch exception As System.Exception
                ProjectData.SetProjectError(exception)
                Interaction.MsgBox(exception.Message, MsgBoxStyle.Critical, Nothing)
                rs = Nothing
                w = Nothing
                ProjectData.ClearProjectError()
                flag = True
            End Try
            If (Not flag) Then
                w.Write(Me.streamdata, 0, Zeiger)
                w.Close()
                rs.Close()
                rs = Nothing
                w = Nothing
            End If
            flag = False
        End Sub
    End Class
End Namespace