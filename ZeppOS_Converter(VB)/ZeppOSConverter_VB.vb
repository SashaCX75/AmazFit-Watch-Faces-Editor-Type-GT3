Imports System.Drawing
Imports System.IO
Imports System.Runtime.InteropServices.ComTypes
Imports System.Security.Cryptography
Public Class ZeppOSConverter_VB
    Private Matrix(-1) As st_Matrix
    Private PvBMP As New Bitmap(1, 1)
    Public Function MyMethod(sourceFileName As String, targetFileName As String) As Boolean
        Dim result = False
        Console.WriteLine("sourceFileName: {0}  targetFileName: {1}", sourceFileName, targetFileName)
        Try
            LoadFramesPNG(sourceFileName, Me.Matrix, True)
            Matrix2BMP(Me.Matrix(0), Me.PvBMP, 1)
            Dim img As New Bitmap(Me.PvBMP.Size.Width, Me.PvBMP.Size.Height)
            img = Me.PvBMP

            img.Save(targetFileName, Imaging.ImageFormat.Png)
            result = True
        Catch ex As Exception
            result = False
        End Try

        Return result
    End Function

End Class
