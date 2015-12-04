Public Class addEditRoom

    Public Bdisplay As Boolean

    Private Sub addEditRoom_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'HotelDBDataSet.roomList' table. You can move, or remove it, as needed.
        'Me.RoomListTableAdapter.Fill(Me.HotelDBDataSet.roomList)


        If Form1.edit = True Then
            Me.RoomListTableAdapter.FillBy(Me.HotelDBDataSet.roomList, Form1.RoomListDataGridView.CurrentRow.Cells(0).Value)
        Else
            Me.RoomListBindingSource.AddNew()
        End If
        Form1.edit = False

    End Sub

    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage1.Click




    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Me.Validate()
        Me.RoomListBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.HotelDBDataSet)

        Me.Close()

        Bdisplay = True
    End Sub

  
    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            TextBox5.Visible = True
        Else
            TextBox5.Visible = False
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            Panel2.Visible = True
        Else
            Panel2.Visible = False

        End If
    End Sub

End Class