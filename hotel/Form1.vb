Public Class Form1

    Public edit As Boolean
    Public deleteIndex As Integer
    Public Bdelete As Boolean

    Dim rowTotal As Integer

    Private Sub Form1_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.RoomListTableAdapter.Fill(Me.HotelDBDataSet.roomList)
        If RoomListDataGridView.RowCount <> 0 Then
            Button3.Enabled = True
            Button2.Enabled = True
        End If

       
        
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        
    End Sub

    Private Sub RoomListBindingNavigatorSaveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RoomListBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.RoomListBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.HotelDBDataSet)

    End Sub

    
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        addEditRoom.ShowDialog()
        
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        edit = True
        addEditRoom.ShowDialog()


    End Sub

    Private Sub RoomListDataGridView_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles RoomListDataGridView.CellValueChanged
       
    End Sub

    Private Sub RoomListDataGridView_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RoomListDataGridView.SelectionChanged


    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        deleteIndex = RoomListDataGridView.CurrentRow.Index
        Bdelete = True
        Me.RoomListBindingSource.RemoveCurrent()
        Me.Validate()
        Me.RoomListBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.HotelDBDataSet)

        If RoomListDataGridView.RowCount = 0 Then
            Button3.Enabled = False
            Button2.Enabled = False
        End If

        'Form2.index = RoomListDataGridView.RowCount

        MsgBox(deleteIndex)
    End Sub

    
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'MsgBox(Form2.index)
        'MsgBox(Form2.MyList.Item(0))

        Me.RoomListTableAdapter.Fill(Me.HotelDBDataSet.roomList)
        
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Form2.index = RoomListDataGridView.RowCount
        For i As Integer = 0 To Form2.index - 1
            'MsgBox(RoomListDataGridView.Rows(i).Cells(1).Value)
            Form2.MyList.Add(RoomListDataGridView.Rows(i).Cells(1).Value)
            'Form2.CalendarView1.DisplayedOwners.Add(Form2.MyList.Item(i))
            '  MsgBox(Form2.MyList.Item(i)) 
        Next

        Form2.Show()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        MsgBox(rowTotal)
        ' 
    End Sub

  
    Private Sub RoomListDataGridView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles RoomListDataGridView.CellContentClick

    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If addEditRoom.Bdisplay = True Then
            rowTotal = RoomListDataGridView.RowCount - 1
            Form2.CalendarView1.DisplayedOwners.Add(RoomListDataGridView.Rows(rowTotal).Cells(1).Value)
            addEditRoom.Bdisplay = False
        End If


    End Sub
End Class
