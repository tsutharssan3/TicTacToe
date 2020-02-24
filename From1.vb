Public Class Form1
    Private InitialiseClicked As Boolean
    Private XUsed(2, 2) As Boolean
    Private OUsed(2, 2) As Boolean
    Private XUsedCount As Integer
    Private OUsedCount As Integer
    Private XBtnClicked As Boolean
    Private OBtnClicked As Boolean
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' creating a game space i.e. horizontal and vertical lineees separating each field
        Dim paper As Graphics = PictureBox1.CreateGraphics
        Dim myPen As New Pen(Color.Black)
        ' calculations will need to use the current picturebox dimensions
        Dim Xwidth As Integer = PictureBox1.Size.Width
        Dim Yheight As Integer = PictureBox1.Size.Height
        ' the lines are drawn, at 1/3 and 2/3 of the X and Y distances to ensure correct appearance
        paper.Clear(Color.White)
        paper.DrawLine(myPen, CInt(Xwidth * 0.33), 0, CInt(0.33 * Xwidth), Yheight) ' first vertical separator
        paper.DrawLine(myPen, CInt(Xwidth * 0.66), 0, CInt(Xwidth * 0.66), Yheight) ' second vertical separator
        paper.DrawLine(myPen, 0, CInt(Yheight * 0.33), Xwidth, CInt(Yheight * 0.33)) ' first horizontal separator
        paper.DrawLine(myPen, 0, CInt(Yheight * 0.66), Xwidth, CInt(Yheight * 0.66)) ' second horizontal separator
        InitialiseClicked = True
        For i As Integer = 0 To 2
            For j As Integer = 0 To 2
                OUsed(i, j) = False
                XUsed(i, j) = False
            Next
        Next
        XUsedCount = 0
        OUsedCount = 0
        XBtnClicked = False
        OBtnClicked = False
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Dim paper As Graphics = PictureBox1.CreateGraphics
        Dim myPen As New Pen(Color.Black)
        ' calculations will need to use the current picturebox dimensions
        Dim Xwidth As Integer = PictureBox1.Size.Width
        Dim Yheight As Integer = PictureBox1.Size.Height
        Dim WindowWidth = Me.Size.Width
        Dim WindowHeight = Me.Size.Height
        If InitialiseClicked Then
            paper.Clear(Color.White)
            ' the lines are drawn, at 1/3 and 2/3 of the X and Y distances to ensure correct appearance
            paper.DrawLine(myPen, CInt(Xwidth * 0.33), 0, CInt(0.33 * Xwidth), Yheight) ' first vertical separator
            paper.DrawLine(myPen, CInt(Xwidth * 0.66), 0, CInt(Xwidth * 0.66), Yheight) ' second vertical separator
            paper.DrawLine(myPen, 0, CInt(Yheight * 0.33), Xwidth, CInt(Yheight * 0.33)) ' first horizontal separator
            paper.DrawLine(myPen, 0, CInt(Yheight * 0.66), Xwidth, CInt(Yheight * 0.66)) ' second horizontal separator
            Button1.Location = New Point(WindowWidth - 120, 20)
            XBtn.Location = New Point(WindowWidth - 120, 70)
            OBtn.Location = New Point(WindowWidth - 120, 120)
            Button2.Location = New Point(WindowWidth - 120, 170)
            InitialiseClicked = True
            For i As Integer = 0 To 2
                For j As Integer = 0 To 2
                    OUsed(i, j) = False
                    XUsed(i, j) = False
                Next
            Next
            XUsedCount = 0
            OUsedCount = 0
            XBtnClicked = False
            OBtnClicked = False
        Else
            Button1.Location = New Point(WindowWidth - 120, 20)
            XBtn.Location = New Point(WindowWidth - 120, 70)
            OBtn.Location = New Point(WindowWidth - 120, 120)
            Button2.Location = New Point(WindowWidth - 120, 170)
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim paper As Graphics = PictureBox1.CreateGraphics
        paper.Clear(Color.White) ' clear the paper with speciffied color
        InitialiseClicked = False
        For i As Integer = 0 To 2
            For j As Integer = 0 To 2
                OUsed(i, j) = False
                XUsed(i, j) = False
            Next
        Next
        XUsedCount = 0
        OUsedCount = 0
        XBtnClicked = False
        OBtnClicked = False
    End Sub

    Private Sub XBtn_Click(sender As Object, e As EventArgs) Handles XBtn.Click
        ' retrieve input  from players
        If XBtnClicked Then
            MessageBox.Show("Wait for other player to make a move ...")
        Else
            If InitialiseClicked And XUsedCount < 3 Then
                Dim XCoord As Integer = CInt(InputBox("Please enter X coordinate [0,1,2]:"))
                If XCoord > 2 Then
                    MessageBox.Show("Please enter a value between 0 and 2")
                    Return
                End If
                Dim YCoord As Integer = CInt(InputBox("Please enter Y coordinate [0,1,2]:"))
                If YCoord > 2 Then
                    MessageBox.Show("Please enter a value between 0 and 2")
                    Return

                End If
                If XUsed(XCoord, YCoord) Or OUsed(XCoord, YCoord) Then
                    MessageBox.Show("Place is already taken. Try a new position")
                Else
                    ' the diagonal dimension of the X (cross) drawn onto the game space
                    Dim l As Integer = CInt(PictureBox1.Size.Height / 4)
                    Dim m As Integer = CInt(PictureBox1.Size.Width / 4)
                    Dim Xdiagonal As Integer

                    If l < m Then
                        Xdiagonal = l
                    Else
                        Xdiagonal = m
                    End If
                    ' based on the picturebox size, the distances are determined for X and Y
                    Dim xsquare As Integer = CInt(PictureBox1.Size.Width / 3)
                    Dim ysquare As Integer = CInt(PictureBox1.Size.Height / 3)
                    ' offset from the corners of each field on the game space
                    Dim offset As Integer = 20

                    ' creating paper and draw the X
                    Dim paper As Graphics = PictureBox1.CreateGraphics
                    Dim myPen As New Pen(Color.Black)
                    paper.DrawLine(myPen, offset + XCoord * xsquare, offset + YCoord * ysquare, offset + XCoord * xsquare + Xdiagonal, offset + YCoord * ysquare + Xdiagonal)
                    paper.DrawLine(myPen, offset + XCoord * xsquare, offset + YCoord * ysquare + Xdiagonal, offset + XCoord * xsquare + Xdiagonal, offset + YCoord * ysquare)
                    XUsed(XCoord, YCoord) = True
                    XBtnClicked = True
                    OBtnClicked = False
                    XUsedCount = XUsedCount + 1
                    If XUsedCount = 3 Then
                        If (XUsed(0, 0) And XUsed(1, 1) And XUsed(2, 2)) Or (XUsed(0, 2) And XUsed(1, 1) And XUsed(2, 0)) Or (XUsed(0, 0) And XUsed(0, 1) And XUsed(0, 2)) Or (XUsed(1, 0) And XUsed(1, 1) And XUsed(1, 2)) Or (XUsed(2, 0) And XUsed(2, 1) And XUsed(2, 2)) Or (XUsed(0, 0) And XUsed(1, 0) And XUsed(2, 0)) Or (XUsed(0, 1) And XUsed(1, 1) And XUsed(2, 1)) Or (XUsed(0, 2) And XUsed(1, 2) And XUsed(2, 2)) Then
                            MessageBox.Show("You are a WINNER! Please initialise the game to play again!")
                        End If
                    End If
                End If

            Else
                MessageBox.Show("Please Initialise the Game")
            End If
        End If

    End Sub

    Private Sub OBtn_Click(sender As Object, e As EventArgs) Handles OBtn.Click
        If OBtnClicked Then
            MessageBox.Show("Wait for other player to make a move")
        Else
            If InitialiseClicked And OUsedCount < 3 Then
                Dim XCoord As Integer = CInt(InputBox("Please enter X coordinate [0,1,2]:"))
                If XCoord > 2 Then
                    MessageBox.Show("Please enter a value between 0 and 2")
                    Return
                End If
                Dim YCoord As Integer = CInt(InputBox("Please enter Y coordinate [0,1,2]:"))
                If YCoord > 2 Then
                    MessageBox.Show("Please enter a value between 0 and 2")
                    Return
                End If
                If XUsed(XCoord, YCoord) Or OUsed(XCoord, YCoord) Then
                    MessageBox.Show("Place is already taken. Try a new position")
                Else
                    Dim Odiameter As Integer

                    Dim l As Integer = CInt(PictureBox1.Size.Height / 4)
                    Dim m As Integer = CInt(PictureBox1.Size.Width / 4)

                    If l < m Then
                        Odiameter = l
                    Else
                        Odiameter = m
                    End If

                    Dim xsquare As Integer = CInt(PictureBox1.Size.Width / 3)
                    Dim ysquare As Integer = CInt(PictureBox1.Size.Height / 3)

                    Dim offset As Integer = 20

                    Dim paper As Graphics = PictureBox1.CreateGraphics
                    Dim myPen As New Pen(Color.Black)
                    ' the difference to the X is, that the first set offfff   X/Y coordinates is
                    ' the top left of a box surrounding the circle. The second set of X/Y is then the diameter
                    paper.DrawEllipse(myPen, offset + XCoord * xsquare, offset + YCoord * ysquare, Odiameter, Odiameter)
                    OUsed(XCoord, YCoord) = True
                    XBtnClicked = False
                    OBtnClicked = True
                    OUsedCount = OUsedCount + 1
                    If OUsedCount = 3 Then
                        If (OUsed(0, 0) And OUsed(1, 1) And OUsed(2, 2)) Or (OUsed(0, 2) And OUsed(1, 1) And OUsed(2, 0)) Or (OUsed(0, 0) And OUsed(0, 1) And OUsed(0, 2)) Or (OUsed(1, 0) And OUsed(1, 1) And OUsed(1, 2)) Or (OUsed(2, 0) And OUsed(2, 1) And OUsed(2, 2)) Or (OUsed(0, 0) And OUsed(1, 0) And OUsed(2, 0)) Or (OUsed(0, 1) And OUsed(1, 1) And OUsed(2, 1)) Or (OUsed(0, 2) And OUsed(1, 2) And OUsed(2, 2)) Then
                            MessageBox.Show("You are a WINNER! Please initialise the game to play again!")
                        End If
                    End If


                End If
            Else
                MessageBox.Show("Please Initialise the Game")
            End If
        End If

    End Sub


End Class
