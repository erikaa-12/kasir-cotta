Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Public Class Menumakmin
    Dim id = "0"
    Dim aksi = False
    Dim nama
    Sub awal()
        'MsgBox(getCount("select * from transaksi where pembeli like '%" & TextBox1.Text & "%'"))
        DataGridView1.DataSource = getData("select * from vmenu where barang like '%" & TextBox1.Text & "%'")
        clearForm(GroupBox2)
        'MsgBox(getValue("select * from transaksi where pembeli like '%" & TextBox1.Text & "%'", "pembeli"))
        DataGridView1.Columns(0).Visible = True
        DataGridView1.Columns(1).HeaderText = "Barang"
        DataGridView1.Columns(2).HeaderText = "Stok"
        DataGridView1.Columns(3).HeaderText = "Harga"
        GroupBox1.Enabled = True
        GroupBox2.Enabled = False
        GroupBox3.Enabled = True
        id = "0"
    End Sub
    Sub buka()
        GroupBox1.Enabled = False
        GroupBox2.Enabled = True
        GroupBox3.Enabled = False
        PictureBox1.Enabled = False
    End Sub
    Sub getCombo()
        ComboBox1.DataSource = getData("select * from kategori")
        ComboBox1.DisplayMember = "kategori"
        ComboBox1.ValueMember = "idkategori"
    End Sub
    Sub comboSel()
        Dim petugas = getCount("select * from kategori")
        'MsgBox(petugas)
        If Integer.Parse(petugas) = 0 Then
            ComboBox1.Items.Add("Data Kategori Kosong")
            ComboBox1.Text = (ComboBox1.Items(0)).ToString
        Else
            getCombo()
        End If
    End Sub
    Private Sub Menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        awal()
        comboSel()
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
                Dim sql = "delete from menu where idbarang =" & id
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
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If adaKosong(GroupBox2) Then
            MsgBox("Ada data kosong")
        Else
            Dim sql
            If Not aksi Then
                sql = "insert into menu values('" & TextBox2.Text & "','" & NumericUpDown1.Value & "','" & TextBox3.Text & "','" & ComboBox1.SelectedValue.ToString & "','" & nama & "')"
                'MsgBox(sql)
                exc(sql)
                PictureBox1.Image.Save("D:\Aplikasi\vb\kasaneva\Gambar\" & nama)
                PictureBox1.Image = Nothing
                clearForm(GroupBox2)
                MsgBox("Data berhasil ditambah")
                buka()
            Else
                sql = "update menu set barang = '" & TextBox2.Text & "', stok = '" & NumericUpDown1.Text & "', harga = '" & TextBox3.Text & "', idkategori  = '" & ComboBox1.SelectedValue.ToString & "', gambar =  '" & nama & "'where idbarang = " & id
                'MsgBox(sql)
                exc(sql)
                PictureBox1.Image.Save("D:\Aplikasi\vb\kasaneva\Gambar\" & nama)
                PictureBox1.Image = Nothing
                clearForm(GroupBox2)
                MsgBox("Data berhasil diubah")
                buka()
            End If

        End If
    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            NumericUpDown1.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            ComboBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
            TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
            id = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            nama = DataGridView1.Rows(e.RowIndex).Cells(5).Value
            Dim folder = "D:\Aplikasi\vb\kasaneva\Gambar\"
            Dim fileName = System.IO.Path.Combine(folder, nama)
            TextBox4.Text = nama
            Try
                Using fs As New System.IO.FileStream(fileName, IO.FileMode.Open)
                    PictureBox1.Image = New Bitmap(Image.FromStream(fs))
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim open As OpenFileDialog = New OpenFileDialog
        open.Filter = "All files |*.jpg;*.png;*.jpeg;"
        open.Title = "Open File"
        open.FilterIndex = 1
        If open.ShowDialog = Windows.Forms.DialogResult.OK Then
            PictureBox1.Image = Image.FromFile(open.FileName)
            nama = System.IO.Path.GetFileName(open.FileName)
            TextBox4.Text = nama
            'MsgBox(nama)
        End If
    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter
        PictureBox1.Enabled = False
    End Sub

    Private Sub GroupBox3_Enter(sender As Object, e As EventArgs) Handles GroupBox3.Enter

    End Sub
End Class