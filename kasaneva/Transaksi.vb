Imports System.Drawing.Printing
Imports System.Reflection.Emit
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.Office.Interop.Excel
Public Class Transaksi
    Dim idmenu = "0"
    Dim id = "0"
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
        idmenu = "0"
    End Sub
    Sub bayarorder()
        Dim idorder = getValue("Select top(1) * from orderutama order by idorder desc", "idorder")
        DataGridView3.DataSource = getData("select penjual, barang, kategori, jumlah, hargajual, subtotal, idorderdetail from vdetail where idorder =" & idorder)
        DataGridView3.Columns(0).HeaderText = "Petugas"
        DataGridView3.Columns(1).HeaderText = "Barang"
        DataGridView3.Columns(2).HeaderText = "Kategori"
        DataGridView3.Columns(3).HeaderText = "Jumlah"
        DataGridView3.Columns(4).HeaderText = "Harga jual"

        DataGridView3.Columns(6).Visible = False

        Dim subtotal = getValue("select SUM(subtotal) as sub FROM orderdetail where idorder =" & idorder, "sub")
        Label7.Text = subtotal
        id = "0"
    End Sub
    Private Sub Transaksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        awal()
        insertTransaksi()
    End Sub
    Sub insertTransaksi()
        exc("insert into orderutama values ('" & Form1.idpetugas.ToString & "','" & Date.Now.ToString("yyy/MM/dd") & "',0,0,0,0)")
        'MsgBox("insert into orderutama values ('" & pilih_petugas.id.ToString"','"Date.Now.ToString("yyy/MM/dd") & "',0,0,0,0)")
    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            idmenu = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
            GroupBox2.Enabled = True
            nama = DataGridView1.Rows(e.RowIndex).Cells(5).Value
            Dim folder = "D:\Aplikasi\vb\kasaneva\Gambar\"
            Dim fileName = System.IO.Path.Combine(folder, nama)
            Try
                Using fs As New System.IO.FileStream(fileName, IO.FileMode.Open)
                    PictureBox1.Image = New Bitmap(Image.FromStream(fs))
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        TextBox5.Text = Double.Parse(TextBox4.Text) * NumericUpDown1.Value
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        awal()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If idmenu = "0" Then
            MsgBox("Pilih data dulu")
        Else
            Dim idorder = getValue("select top (1) * from orderutama order by idorder desc", "idorder")
            Dim sql = "insert into orderdetail values ('" & idorder & "', '" & idmenu & "','" & NumericUpDown1.Value & "', '" & TextBox4.Text & "','" & TextBox5.Text & "')"
            'MsgBox(sql)
            exc(sql)
            MsgBox("Berhasil ditambahkan")
            bayarorder()
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If id = "0" Then
            MsgBox("Pilih data dulu")
        Else
            If dialog("Apakah anda yakin ingin menghapus?") Then
                Dim sql = "delete from orderdetail where idorderdetail =" & id
                MsgBox(sql)
                Label7.Text = "0"
                'MsgBox(sql)
                exc(sql)
                clearForm(GroupBox2)
                bayarorder()
            End If
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Label7.Text = "0" Then
            MsgBox("Data Kosong")
        Else
            Pembayaran.ShowDialog()
        End If
    End Sub
    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            TextBox2.Text = DataGridView3.Rows(e.RowIndex).Cells(1).Value
            TextBox4.Text = DataGridView3.Rows(e.RowIndex).Cells(3).Value
            TextBox5.Text = DataGridView3.Rows(e.RowIndex).Cells(4).Value
            NumericUpDown1.Text = DataGridView3.Rows(e.RowIndex).Cells(5).Value
            id = DataGridView3.Rows(e.RowIndex).Cells(6).Value
        End If
    End Sub

    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub
End Class