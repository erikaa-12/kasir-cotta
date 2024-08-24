Public Class Petugas
    Dim id = "0"
    Dim aksi = False
    Sub awal()
        'MsgBox(getCount("select * from transaksi where pembeli like '%" & TextBox1.Text & "%'"))
        DataGridView1.DataSource = getData("select * from penjual where penjual like '%" & TextBox1.Text & "%'")
        clearForm(GroupBox2)
        'MsgBox(getValue("select * from transaksi where pembeli like '%" & TextBox1.Text & "%'", "pembeli"))
        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(1).HeaderText = "Nama"
        DataGridView1.Columns(2).HeaderText = "Telp"
        DataGridView1.Columns(3).HeaderText = "Alamat"
        GroupBox1.Enabled = True
        GroupBox2.Enabled = False
        GroupBox3.Enabled = True
        id = "0"
    End Sub

    Sub buka()
        GroupBox1.Enabled = False
        GroupBox2.Enabled = True
        GroupBox3.Enabled = False
    End Sub
    Private Sub Petugas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        awal()
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        awal()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        buka()
        clearForm(GroupBox2)
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
        If id = "0" Then
            MsgBox("Pilih data dulu")
        Else
            If dialog("Apakah anda yakin ingin menghapus?") Then
                Dim sql = "delete from penjual where idpenjual =" & id
                'MsgBox(sql)
                exc(sql)
                clearForm(GroupBox2)
                awal()
            End If
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        clearForm(GroupBox2)
        awal()
    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
            TextBox5.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
            TextBox6.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value
            id = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If adaKosong(GroupBox2) Then
            MsgBox("Ada data kosong")
        Else
            Dim sql
            If Not aksi Then
                sql = "insert into penjual values('" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "')"
                'MsgBox(sql)
                exc(sql)
                clearForm(GroupBox2)
                MsgBox("Data berhasil ditambah")
                awal()
            Else
                sql = "update penjual set penjual = '" & TextBox2.Text & "', telp = '" & TextBox3.Text & "', alamat = '" & TextBox4.Text & "', username = '" & TextBox5.Text & "', password = '" & TextBox6.Text & "' where idpenjual = " & id
                'MsgBox(sql)
                exc(sql)
                clearForm(GroupBox2)
                MsgBox("Data berhasil diubah")
                awal()
            End If
        End If
    End Sub
End Class