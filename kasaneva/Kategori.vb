Public Class Kategori
    Dim id = "0"
    Dim aksi = False
    Sub awal()
        DataGridView1.DataSource = getData("select * from kategori where kategori like '%" & TextBox1.Text & "%'")
        clearForm(GroupBox3)
        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(1).HeaderText = "kategori"
        GroupBox2.Enabled = True
        GroupBox3.Enabled = False
        GroupBox4.Enabled = True
        id = "0"
    End Sub
    Sub buka()
        GroupBox2.Enabled = False
        GroupBox3.Enabled = True
        GroupBox4.Enabled = False
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        clearForm(GroupBox3)
        awal()
    End Sub
    Private Sub Kategori_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        awal()
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        awal()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        buka()
        clearForm(GroupBox3)
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If id = "0" Then
            MsgBox("Pilih data dulu")
        Else
            aksi = True
            buka()
        End If
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        MsgBox(id)
        If id = "0" Then
            MsgBox("Pilih data dulu")
        Else
            If dialog("Apakah anda yakin ingin menghapus?") Then
                Dim sql = "delete from kategori where idkategori =" & id
                'MsgBox(sql)
                exc(sql)
                clearForm(GroupBox3)
                awal()
            End If
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If adaKosong(GroupBox3) Then
            MsgBox("Ada data kosong")
        Else
            Dim sql
            If Not aksi Then
                sql = "insert into kategori values('" & TextBox2.Text & "')"
                'MsgBox(sql)
                exc(sql)
                clearForm(GroupBox3)
                MsgBox("Data berhasil ditambah")
                awal()
            Else
                sql = "update kategori set kategori = '" & TextBox2.Text & "' where idkategori= " & id
                'MsgBox(sql)
                exc(sql)
                clearForm(GroupBox3)
                MsgBox("Data berhasil diubah")
                awal()
            End If

        End If

    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value

            id = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        End If
    End Sub
End Class