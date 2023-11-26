Imports System.Drawing
Imports System.IO
Imports System.IO.Compression
Module Module_Main

    Private oFrames As New List(Of st_FrameInfo)
    Friend EmptyColor As Color = Color.FromArgb(0, 0, 0)
    Private G As Graphics

    Friend Structure st_Matrix
        Friend RGBA(,) As Color
        Friend fName As String
    End Structure

    Private Structure st_FrameInfo
        Friend fName As String
        Friend b() As Byte
        Friend fType As en_fType
        Friend DataOffset As Long
        Friend HavePalete As Boolean
        Friend ColorType As en_TGAImgType
        Friend Compressed As Boolean
        Friend FirstPaleteIndex As Integer
        Friend PaleteLen As Integer
        Friend PaleteBPP As Byte
        Friend X As Integer
        Friend Y As Integer
        Friend ImgBPP As Byte
        Friend LeftToRight As Boolean
        Friend TopToBottom As Boolean
        Friend WidthActual As Integer
        Friend Width As Integer
        Friend Height As Integer
    End Structure
    Private Enum en_TGAImgType As Byte
        None = 0
        Palete = 1
        NoPalete = 2
        BW = 3
        'по мануалу
        'UnCompressedPalete = 1
        'UnCompressedNoPalete = 2
        'UnCompressedBW = 3
        'технически, это то-же что выше, просто с 3-м битом (от нуля). вынес в отдельную переменную
        'RLEPalete = 9
        'RLENoPalete = 10
        'RLEBW = 11
    End Enum
    Private Enum en_fType As Integer
        TGA = 0
        PNG = 1
    End Enum

    Friend Sub LoadFramesPNG(ByRef fName As String, ByRef Matrix() As st_Matrix, ByRef ok As Boolean)
        If IO.File.Exists(fName) Then
            If GetPNGBytes(fName) Then
                Dim n, tn As Integer
                Try
                    tn = oFrames.Count - 1
                    ReDim Matrix(tn)
                    For n = 0 To tn
                        If Not ReadMatrix(Matrix(n), n) Then
                            ReDim Preserve Matrix(n - 1)
                            Exit Sub
                        End If
                        Matrix(n).fName = oFrames(n).fName
                    Next n
                    Exit Sub
                Catch ex As Exception
                    GoTo Wrong
                End Try
            Else
                GoTo Wrong
            End If
        End If
Wrong:
        ReDim Matrix(-1)
        ok = False
    End Sub

    Private Function GetPNGBytes(ByRef fName As String) As Boolean
        If IO.File.Exists(fName) Then

            Try
                Dim ms As New IO.MemoryStream
                Dim fi As st_FrameInfo
                Dim pName As String = ""
                oFrames.Clear()

                If fName.EndsWith(".png", StringComparison.OrdinalIgnoreCase) Then
                    fi = New st_FrameInfo
                    With fi
                        .fName = fName.ToLower
                        ms = New MemoryStream(File.ReadAllBytes(fName))
                        'fName.Open.CopyTo(ms)
                        .b = ms.ToArray
                        ms.Dispose()
                        If CompareByteChain(.b, {&H89, &H50, &H4E, &H47}, 0) Then ' нормальный пнг - конвертация не нужна
                            .fType = en_fType.PNG
                            '.Width = .b(&H11) * &H10000 + .b(&H12) * &H100 + .b(&H13) 'если надо
                            '.Height = .b(&H15) * &H10000 + .b(&H16) * &H100 + .b(&H17) 'если надо
                        Else
                            .fType = en_fType.TGA
                            .DataOffset = &H12 + .b(0)
                            Select Case (.b(1) And 127)
                                Case 0
                                    .HavePalete = False
                                Case 1
                                    .HavePalete = True
                                Case Else
                                    GoTo Err
                            End Select
                            .ColorType = (.b(2) And 7)
                            .Compressed = (.b(2) > 7)
                            .FirstPaleteIndex = Get2bValue(.b, 3) '>0 не попадались, не знаю как обрабатывать, игнор
                            .PaleteLen = Get2bValue(.b, 5)
                            .PaleteBPP = Get2bValue(.b, 7)
                            .X = Get2bValue(.b, 8) '>0 не попадались, не знаю как обрабатывать, игнор
                            .Y = Get2bValue(.b, 10) '>0 не попадались, не знаю как обрабатывать, игнор
                            .Width = Get2bValue(.b, 12)
                            .Height = Get2bValue(.b, 14)
                            .ImgBPP = .b(16)
                            .LeftToRight = ((.b(17) And 8) = 8) '0 не попадались, не знаю как обрабатывать, игнор
                            .TopToBottom = ((.b(17) And 16) = 0) '1 не попадались, не знаю как обрабатывать, игнор
                            .WidthActual = Get2bValue(.b, 22)
                        End If
                    End With
                    oFrames.Add(fi)
                End If
                Return True
            Catch ex As Exception
                GoTo Err
            End Try
        Else
Err:
            Return False
        End If
    End Function

    Private Function ReadMatrix(ByRef Matrix As st_Matrix, ByRef fIndex As Integer) As Boolean
        Dim w, h, x, y, n, tn As Integer
        Dim c() As Color
        Dim pos, skip As Long
        Try
            With oFrames(fIndex)
                Select Case .fType
                    Case en_fType.PNG
                        BMPToMatrix(Matrix, New Bitmap(New IO.MemoryStream(.b)))
                        Return True
                    Case en_fType.TGA
                        w = .WidthActual - 1
                        h = .Height - 1
                        ReDim Matrix.RGBA(w, h)
                        If .HavePalete Then
                            If .FirstPaleteIndex > 0 Then n = n
                            If .Compressed Then
                                GoTo Err
                            End If
                            tn = .PaleteLen - 1
                            ReDim c(tn)
                            pos = .DataOffset 'палитра
                            For n = 0 To tn
                                Select Case .PaleteBPP
                                    Case 32
                                        c(n) = Color.FromArgb(.b(pos + 3), .b(pos), .b(pos + 1), .b(pos + 2))
                                        pos += 4
                                    Case 24 'теория. не попадались
                                        c(n) = Color.FromArgb(255, .b(pos), .b(pos + 1), .b(pos + 2))
                                        pos += 3
                                    Case 16
                                        'тга
                                        'c(n) = Color.FromArgb(255, ((.b(pos + 1) And &H7C) * 2), (((.b(pos + 1) And 3) * &H40) Or ((.b(pos) \ &H20)) * 8), (.b(pos) And &H1F) * 8)
                                        'ргб565
                                        c(n) = RGB565ToRGB(.b(pos + 1), .b(pos))
                                        pos += 2
                                    Case 15 'теория. не попадались
                                        c(n) = Color.FromArgb(255, ((.b(pos + 1) And &H7C) * 2), (((.b(pos + 1) And 3) * &H40) Or ((.b(pos) \ &H20) * 8)), ((.b(pos) And &H1F) * 8))
                                        pos += 2
                                    Case Else
                                        GoTo Err
                                End Select
                            Next n
                            'картинка
                            pos = .DataOffset + .PaleteLen * (.PaleteBPP \ 8 + IIf(((.PaleteBPP Mod 8) = 0), 0, 1))
                            skip = .Width - .WidthActual
                            For y = 0 To h
                                For x = 0 To w
                                    Matrix.RGBA(x, y) = c(.b(pos))
                                    pos += 1
                                Next x
                                pos += skip
                            Next y
                        Else 'без палитры
                            pos = .DataOffset
                            Select Case .ImgBPP
                                Case 32
                                    skip = (.Width - .WidthActual) * 4
                                    For y = 0 To h
                                        For x = 0 To w
                                            Matrix.RGBA(x, y) = Color.FromArgb(.b(pos + 3), .b(pos + 2), .b(pos + 1), .b(pos))
                                            pos += 4
                                        Next x
                                        pos += skip
                                    Next y
                                Case 24 'теория. не попадались
                                    skip = (.Width - .WidthActual) * 3
                                    For y = 0 To h
                                        For x = 0 To w
                                            Matrix.RGBA(x, y) = Color.FromArgb(255, .b(pos), .b(pos + 1), .b(pos + 2))
                                            pos += 3
                                        Next x
                                        pos += skip
                                    Next y
                                Case 16
                                    skip = (.Width - .WidthActual) * 2
                                    For y = 0 To h
                                        For x = 0 To w
                                            'тга
                                            'Matrix.RGBA(x, y) = Color.FromArgb(255, ((.b(pos + 1) And &H7C) * 2), (((.b(pos + 1) And 3) * &H40) Or ((.b(pos) \ &H20)) * 8), (.b(pos) And &H1F) * 8)
                                            'ргб565
                                            Matrix.RGBA(x, y) = RGB565ToRGB(.b(pos + 1), .b(pos))
                                            pos += 2
                                        Next x
                                        pos += skip
                                    Next y
                                Case 15 'теория. не попадались
                                    skip = (.Width - .WidthActual) * 2
                                    For y = 0 To h
                                        For x = 0 To w
                                            Matrix.RGBA(x, y) = Color.FromArgb(255, ((.b(pos + 1) And &H7C) * 2), (((.b(pos + 1) And 3) * &H40) Or ((.b(pos) \ &H20) * 8)), ((.b(pos) And &H1F) * 8))
                                            pos += 2
                                        Next x
                                        pos += skip
                                    Next y
                                Case Else
                                    GoTo Err
                            End Select
                        End If
                End Select
            End With
            Return True
        Catch ex As Exception
        End Try
Err:
        Return False
    End Function




#Region "Tools"
    Friend Function RGB565ToRGB(ByRef b1 As Byte, ByRef b2 As Byte, Optional a As Byte = 255) As Color
        Return Color.FromArgb(a, b1 And 248&,
                                 ((b2 \ 32) Or ((b1 And 7&) * 8)) * 4,
                                 (b2 And 31&) * 8)
    End Function
    Friend Function Get2bValue(ByRef b() As Byte, ByRef pos As Long) As Integer
        If (pos + 1) > UBound(b) OrElse pos < 0 Then Return -1
        Return b(pos) + b(pos + 1) * 256
    End Function
    Friend Function CompareByteChain(ByRef b() As Byte, ByRef bT() As Byte, ByRef pos As Long) As Boolean
        Try
            Dim n, tn As Integer
            tn = UBound(bT)
            For n = 0 To tn
                If Not (b(pos + n) = bT(n)) Then Return False
            Next n
        Catch ex As Exception
        End Try
        Return True
    End Function
    Friend Sub BMPToMatrix(ByRef Matrix As st_Matrix, ByRef BMP As Bitmap)
        Dim x, y, w, h, sl, pos As Integer
        Dim BMPData As Imaging.BitmapData
        Dim RawBMP() As Byte
        Dim tBMP As New Bitmap(BMP.Width, BMP.Height, Imaging.PixelFormat.Format32bppArgb)
        G = Graphics.FromImage(tBMP)
        G.DrawImage(BMP, 0, 0, BMP.Width, BMP.Height)
        G.Flush()
        G.Dispose()
        BMPData = tBMP.LockBits(New Rectangle(Point.Empty, tBMP.Size), Imaging.ImageLockMode.ReadOnly, tBMP.PixelFormat)
        w = tBMP.Width - 1
        h = tBMP.Height - 1
        If IsNothing(Matrix.RGBA) OrElse (Not UBound(Matrix.RGBA, 1) = w OrElse Not UBound(Matrix.RGBA, 2) = h) Then ReDim Matrix.RGBA(w, h)
        sl = BMPData.Stride
        ReDim RawBMP(sl * BMPData.Height)
        Runtime.InteropServices.Marshal.Copy(BMPData.Scan0, RawBMP, 0, UBound(RawBMP))
        For y = 0 To h
            For x = 0 To w
                pos = y * sl + x * 4
                Matrix.RGBA(x, y) = Color.FromArgb((RawBMP(pos + 3) \ 4) * 4, (RawBMP(pos + 2) \ 8) * 8, (RawBMP(pos + 1) \ 4) * 4, (RawBMP(pos) \ 8) * 8)
            Next x
        Next y
        Runtime.InteropServices.Marshal.Copy(RawBMP, 0, BMPData.Scan0, UBound(RawBMP))
        tBMP.UnlockBits(BMPData)
    End Sub

    Friend Sub Matrix2BMP(ByRef Matrix As st_Matrix, ByRef BMP As Bitmap, Optional ByRef zFactor As Integer = 1)
        Dim x, y, w, h, sl As Integer
        Dim BMPData As Imaging.BitmapData
        Dim RawBMP() As Byte
        If Not IsNothing(BMP) Then BMP.Dispose()
        w = UBound(Matrix.RGBA, 1)
        h = UBound(Matrix.RGBA, 2)
        BMP = New Bitmap((w + 1) * zFactor, (h + 1) * zFactor, Imaging.PixelFormat.Format32bppArgb)
        G = Graphics.FromImage(BMP)
        G.Clear(EmptyColor)
        G.Flush()
        G.Dispose()
        BMPData = BMP.LockBits(New Rectangle(Point.Empty, BMP.Size), Imaging.ImageLockMode.ReadWrite, BMP.PixelFormat)
        sl = BMPData.Stride
        ReDim RawBMP(BMPData.Stride * BMPData.Height)
        Runtime.InteropServices.Marshal.Copy(BMPData.Scan0, RawBMP, 0, UBound(RawBMP))
        For y = 0 To h
            For x = 0 To w
                DrawRawPixel(RawBMP, x, y, sl, zFactor, Matrix.RGBA(x, y).A, Matrix.RGBA(x, y).R, Matrix.RGBA(x, y).G, Matrix.RGBA(x, y).B)
            Next x
        Next y
        Runtime.InteropServices.Marshal.Copy(RawBMP, 0, BMPData.Scan0, UBound(RawBMP))
        BMP.UnlockBits(BMPData)
    End Sub
    Private Sub DrawRawPixel(ByRef RawData() As Byte, ByRef pX As Integer, ByRef pY As Integer, ByRef sl As Integer, ByVal zFactor As Integer, ByRef a As Byte, ByRef r As Byte, ByRef g As Byte, ByRef b As Byte)
        Dim x, y, pos, zpos, dpos As Integer
        zpos = (sl * pY + pX * 4) * zFactor
        zFactor -= 1
        For y = 0 To zFactor
            dpos = zpos + y * sl
            For x = 0 To zFactor
                pos = dpos + x * 4
                RawData(pos) = b
                RawData(pos + 1) = g
                RawData(pos + 2) = r
                RawData(pos + 3) = a
            Next x
        Next y
    End Sub
#End Region '"Tools"


End Module

