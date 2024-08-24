Imports System.Drawing.Printing
Public Class Pembayaran
    Dim WithEvents PD As New PrintDocument
    Dim PPD As New PrintPreviewDialog
    Private Sub Pembayaran_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.Text = Transaksi.Label7.Text
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim idorderdetail = getValue("select top(1) * from orderutama order by idorder desc", "idorder")
        If adaKosong(GroupBox1) Then
            MsgBox("Data Kosong")
        Else
            Dim sql = "update orderutama set total = '" & TextBox2.Text & "', bayar = '" & TextBox1.Text & "', kembali = '" & TextBox3.Text & "', status = 1 where idorder=" & idorderdetail
            'msgBox(sql)
            exc(sql)
            MsgBox("Pembayaran Berhasil")
            Transaksi.Close()
            Form1.awal()
            Me.Close()
            Transaksi.Close()
        End If
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text = "" Then
            TextBox1.Text = "0"
            TextBox2.Text = Transaksi.Label7.Text
        Else
            TextBox3.Text = Double.Parse(TextBox1.Text) - Double.Parse(TextBox2.Text)
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        PPD.Document = PD
        PPD.ShowDialog()
        'PD.Print()
    End Sub
    Private Sub PD_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PD.PrintPage
        Dim f10 As New Font("Times New Roman", 10, FontStyle.Regular)
        Dim f10b As New Font("Times New Roman", 10, FontStyle.Bold)
        Dim f14 As New Font("Times New Roman", 14, FontStyle.Bold)

        Dim leftmargin As Integer = PD.DefaultPageSettings.Margins.Left
        Dim centermargin As Integer = PD.DefaultPageSettings.PaperSize.Width / 2
        Dim rightmargin As Integer = PD.DefaultPageSettings.PaperSize.Width

        Dim kanan As New StringFormat
        Dim tengah As New StringFormat
        kanan.Alignment = StringAlignment.Far
        tengah.Alignment = StringAlignment.Center

        Dim garis As String
        garis = "----------------------------------------------------------------------------------------------------------------------"

        e.Graphics.DrawString("Caffe Cotta", f14, Brushes.Black, centermargin, 5, tengah)
        e.Graphics.DrawString("Jl. Panglima Sudirman No.5 Tuban", f10, Brushes.Black, centermargin, 25, tengah)
        e.Graphics.DrawString("Hp: 0842-1321-5576", f10, Brushes.Black, centermargin, 40, tengah)
        Dim yPos As Integer = 80 ' Set posisi vertikal awal

        yPos += 15
        e.Graphics.DrawString("Petugas: " & Form1.Label3.Text, f10, Brushes.Black, 10, yPos)
        yPos += 20
        e.Graphics.DrawString("Tgl Order: " & Form1.DateTimePicker1.Text, f10, Brushes.Black, 10, yPos)
        yPos += 30
        e.Graphics.DrawString(garis, f10, Brushes.Black, 0, yPos)

        For Each row As DataGridViewRow In Transaksi.DataGridView3.Rows
            Dim total As Integer = Convert.ToInt32(row.Cells("subtotal").Value)
            Dim Barang As String = row.Cells("Barang").Value.ToString()
            Dim Jumlah As String = row.Cells("Jumlah").Value.ToString()

            yPos += 20 ' Tambahkan offset untuk posisi vertikal berikutnya
            e.Graphics.DrawString(Barang, f10, Brushes.Black, 10, yPos)
            e.Graphics.DrawString(Jumlah, f10, Brushes.Black, 158, yPos)
            e.Graphics.DrawString(total, f10, Brushes.Black, 260, yPos)
        Next


        yPos += 100
        e.Graphics.DrawString(garis, f10, Brushes.Black, 0, yPos)

        yPos += 10
        e.Graphics.DrawString("Tunai: " & TextBox1.Text, f10, Brushes.Black, 230, yPos)

        yPos += 15
        e.Graphics.DrawString("Total: " & TextBox2.Text, f10, Brushes.Black, 232, yPos)

        yPos += 20
        e.Graphics.DrawString("Kembalian: " & TextBox3.Text, f10, Brushes.Black, 200, yPos)


        yPos += 50
        e.Graphics.DrawString("-Terima Kasih-", f10, Brushes.Black, 112, yPos)
        yPos += 20
        e.Graphics.DrawString("atas", f10, Brushes.Black, 142, yPos)

        yPos += 20
        e.Graphics.DrawString("Kunjungan Anda", f10, Brushes.Black, 107, yPos)


    End Sub
    Private Sub pd_beginprint(sender As Object, e As PrintEventArgs) Handles PD.BeginPrint
        Dim pagesetup As New PageSettings
        pagesetup.PaperSize = New PaperSize("custom", 330, 500)
        PD.DefaultPageSettings = pagesetup
    End Sub
End Class