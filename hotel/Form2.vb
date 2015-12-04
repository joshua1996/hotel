Imports DevComponents.DotNetBar.Schedule
Imports DevComponents.Schedule.Model
Imports DevComponents.DotNetBar

Public Class Form2

    Dim startD As New List(Of DateTime)()
    Dim endD As New List(Of DateTime)()

    'Private defUsers As String() = New String() {"Fred", "Sue", "Robert"}
    Dim defUsers() As String
    Public MyList As New List(Of String)()
    Public index As Integer

    Private Sub Form2_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If Form1.Bdelete = True Then
            CalendarView1.DisplayedOwners.RemoveAt(Form1.deleteIndex)
            Form1.Bdelete = False
        End If
    End Sub

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'HotelDBDataSet.roomList' table. You can move, or remove it, as needed.
        Me.RoomListTableAdapter.Fill(Me.HotelDBDataSet.roomList)
        index = RoomListDataGridView.RowCount
        'MsgBox(index)
        For i As Integer = 0 To index - 1
            'MsgBox(RoomListDataGridView.Rows(i).Cells(1).Value)
            MyList.Add(RoomListDataGridView.Rows(i).Cells(1).Value)
            'Form2.CalendarView1.DisplayedOwners.Add(Form2.MyList.Item(i))
            '  MsgBox(Form2.MyList.Item(i)) 
        Next
        CalendarView1.TimeLinePeriod = eTimeLinePeriod.Days
        'CalendarView1.DisplayedOwners.Add("GER")

        Dim _Model As New CalendarModel()
        Dim ap As New Appointment()


        _Model.Appointments.Add(ap)
        CalendarView1.CalendarModel = _Model

        ap.Subject = "Create Demo Application"
        ap.OwnerKey = CalendarView1.SelectedOwner
        ap.StartTime = DateTime.Now
        ap.EndTime = ap.StartTime.AddDays(3)
        displayOwner()


    End Sub

    Private Sub displayOwner()
        For i As Integer = 0 To index - 1
            CalendarView1.DisplayedOwners.Add(MyList.Item(i))

        Next
    End Sub

   

    Private Sub CalendarView1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles CalendarView1.MouseUp
        If e.Button = MouseButtons.Right Then
            If TypeOf sender Is BaseView Then
                BaseViewMouseUp(sender, e)
            End If
        End If
    End Sub

    Private Sub BaseViewMouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
        Dim view As BaseView = TryCast(sender, BaseView)
        Dim area As eViewArea = view.GetViewAreaFromPoint(e.Location)

        If area = eViewArea.InContent Then
            InContentMouseUp(view, e)
        End If
    End Sub

    Private Sub InContentMouseUp(ByVal view As BaseView, ByVal e As MouseEventArgs)
        ' Get the DateSelection start and end
        ' from the current mouse location

        Dim startDate As DateTime, endDate As DateTime

        If CalendarView1.GetDateSelectionFromPoint(e.Location, startDate, endDate) = True Then
            ' If this date already falls outside the currently
            ' selected range (DateSelectionStart and DateSelectionEnd)
            ' then select the new range

            If IsDateSelected(startDate, endDate) = False Then
                CalendarView1.DateSelectionStart = startDate
                CalendarView1.DateSelectionEnd = endDate
            End If
        End If

        ' Remove any previously added view specific items

        For i As Integer = InContentContextMenu.SubItems.Count - 1 To 1 Step -1
            InContentContextMenu.SubItems.RemoveAt(i)
        Next

        ' If this is a TimeLine view, then add a couple
        ' of extra items

        If TypeOf view Is TimeLineView Then
            Dim bi As New ButtonItem()

            ' Page Navigator

            If CalendarView1.TimeLineShowPageNavigation = True Then
                bi.Text = "Hide Page Navigator"
            Else
                bi.Text = "Show Page Navigator"
            End If

            bi.BeginGroup = True

            AddHandler bi.Click, AddressOf bi_PageNavigatorClick

            InContentContextMenu.SubItems.Add(bi)

            ' Condensed Visibility

            If CalendarView1.TimeLineCondensedViewVisibility = eCondensedViewVisibility.Hidden Then
                bi = New ButtonItem("", "Show Condensed View")
                AddHandler bi.Click, AddressOf bi_CondensedClick

                InContentContextMenu.SubItems.Add(bi)
            End If
        End If

        ShowContextMenu(InContentContextMenu)
    End Sub

    Private Function IsDateSelected(ByVal startDate As DateTime, ByVal endDate As DateTime) As Boolean
        If CalendarView1.DateSelectionStart.HasValue AndAlso CalendarView1.DateSelectionEnd.HasValue Then
            If CalendarView1.DateSelectionStart.Value <= startDate AndAlso CalendarView1.DateSelectionEnd.Value >= endDate Then
                Return (True)
            End If
        End If

        Return (False)
    End Function

    Private Sub ShowContextMenu(ByVal cm As ButtonItem)
        Dim pt As Point = Control.MousePosition
        cm.Popup(pt)
    End Sub

    Private Sub bi_PageNavigatorClick(ByVal sender As Object, ByVal e As EventArgs)
        CalendarView1.TimeLineShowPageNavigation = (CalendarView1.TimeLineShowPageNavigation = False)
    End Sub

    Private Sub bi_CondensedClick(ByVal sender As Object, ByVal e As EventArgs)
        CalendarView1.TimeLineCondensedViewVisibility = eCondensedViewVisibility.AllResources
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        MsgBox(owner(0))
    End Sub

    Dim owner As New List(Of String)()

    Private Sub miAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles InContentAddAppContextItem.Click


        Dim startDate As DateTime = CalendarView1.DateSelectionStart.GetValueOrDefault()
        Dim endDate As DateTime = CalendarView1.DateSelectionEnd.GetValueOrDefault()

        startD.Add(CalendarView1.DateSelectionStart.GetValueOrDefault())
        endD.Add(CalendarView1.DateSelectionEnd.GetValueOrDefault())
        owner.Add(CalendarView1.SelectedOwner)

        AddNewAppointment(startDate, endDate)
    End Sub


    Private Function AddNewAppointment(ByVal startDate As DateTime, ByVal endDate As DateTime) As Appointment
        ' Create new appointment and add it to the model
        ' Appointment will show up in the view automatically

        Dim appointment As New Appointment()

        appointment.StartTime = startDate
        appointment.EndTime = endDate
        appointment.OwnerKey = CalendarView1.SelectedOwner

        appointment.Subject = "New " & appointment.CategoryColor & " appointment!"

        appointment.Description = "This is a new appointment"
        appointment.Tooltip = "This is a Custom tooltip for this new appointment"

        ' Add appointment to the model

        CalendarView1.CalendarModel.Appointments.Add(appointment)

        Return (appointment)
    End Function



    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Me.Hide()
        Form1.Show()
    End Sub

    Private Sub RoomListBindingNavigatorSaveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RoomListBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.RoomListBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.HotelDBDataSet)

    End Sub

End Class