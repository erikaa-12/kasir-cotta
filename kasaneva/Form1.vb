Imports System.Data.OleDb
Imports excel = Microsoft.Office.Interop.Excel
Public Class Form1
    Public idpetugas As Integer
    Sub awal()
        Dim bulan As Integer = DateTimePicker1.Value.Month
        Dim tahun As Integer = DateTimePicker1.Value.Year

        Dim query As String = "SELECT penjual, barang, kategori, jumlah, hargajual, subtotal, total, bayar, kembali, tglorder, idorderdetail " &
                              "FROM vdetail " &
                              "WHERE penjual LIKE '%" & TextBox1.Text & "%' " &
                              "AND MONTH(tglorder) = " & bulan & " " &
                              "AND YEAR(tglorder) = " & tahun

        DataGridView1.DataSource = getData(query)
        ''DataGridView1.Columns(0).HeaderText = "petugas"
        'DataGridView1.Columns(1).HeaderText = "barang"
        'DataGridView1.Columns(2).HeaderText = "kategori"
        'DataGridView1.Columns(3).HeaderText = "jumlah"
        'DataGridView1.Columns(4).HeaderText = "harga jual"
        'DataGridView1.Columns(5).HeaderText = "sub total"
        'DataGridView1.Columns(6).HeaderText = "total"
        'DataGridView1.Columns(7).HeaderText = "bayar"
        'DataGridView1.Columns(8).HeaderText = "kembali/kurang"
        'DataGridView1.Columns(9).HeaderText = "tanggal order"
        'DataGridView1.Columns(0).Visible = False

    End Sub
    Private Sub PetugasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PetugasToolStripMenuItem.Click
        Petugas.ShowDialog()

    End Sub
    Private Sub KategoriToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KategoriToolStripMenuItem.Click
        Kategori.ShowDialog()

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Transaksi.ShowDialog()

    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        login.ShowDialog()
        Me.Close()
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        awal()
    End Sub
    Private Sub BarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarangToolStripMenuItem.Click
        Menumakmin.ShowDialog()

    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SaveFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx"

        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim xlaapp As Microsoft.Office.Interop.Excel.Application
            Dim xlworkbook As Microsoft.Office.Interop.Excel.Workbook
            Dim xlworksheet As Microsoft.Office.Interop.Excel.Worksheet
            Dim misvalue As Object = System.Reflection.Missing.Value
            Dim i As Integer
            Dim j As Integer

            xlaapp = New Microsoft.Office.Interop.Excel.Application
            xlworkbook = xlaapp.Workbooks.Add(misvalue)
            xlworksheet = xlworkbook.Sheets.Item(1)
            For i = 0 To DataGridView1.RowCount - 2
                For j = 0 To DataGridView1.Columns.Count - 1
                    ' Set column headers
                    If i = 0 Then
                        xlworksheet.Cells(1, j + 1) = DataGridView1.Columns(j).HeaderText
                    End If
                    ' Set cell values
                    If DataGridView1(j, i).Value IsNot Nothing Then
                        xlworksheet.Cells(i + 2, j + 1) = DataGridView1(j, i).Value.ToString()
                    Else
                        xlworksheet.Cells(i + 2, j + 1) = ""
                    End If
                Next
            Next

            xlworksheet.SaveAs(SaveFileDialog1.FileName)
            xlworkbook.Close()
            xlaapp.Quit()

            relaseobject(xlaapp)
            relaseobject(xlworkbook)
            relaseobject(xlworksheet)

            MsgBox("Ekspor Berhasil")

        End If
    End Sub
    Private Sub relaseobject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        awal()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        awal()
    End Sub

    Private Sub MasterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterToolStripMenuItem.Click

    End Sub
End Class
