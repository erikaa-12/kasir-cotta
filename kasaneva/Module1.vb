
Imports System.Data.SqlClient
Imports System.Net.Mail
Module Module1

        Public conn As SqlConnection
        Public da As SqlDataAdapter
        Public dr As SqlDataReader
    Public dt As DataTable
    Public cmd As SqlCommand

    Sub koneksi()
        conn = New SqlConnection("Data Source=ERIKA\SQLEXPRESS; Initial Catalog=db_kasir ;Integrated Security=True")
        If conn.State = conn.State.Closed Then
            conn.Open()
        End If
    End Sub

    Sub closeKoneksi()
        conn.Close()
        cmd.Dispose()
    End Sub

    'getdata digunakan untuk menampilkan data 
    Function getData(s As String)
        koneksi()
        Try
            cmd = New SqlCommand(s, conn)
            da = New SqlDataAdapter
            dt = New DataTable
            da.SelectCommand = cmd
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        Finally
            closeKoneksi()
        End Try
    End Function

    'getcount untuk menampilkan jumlah data 
    Function getCount(s As String)
            koneksi()
            Try
                cmd = New SqlCommand(s, conn)
                da = New SqlDataAdapter
                dt = New DataTable
                da.SelectCommand = cmd
                da.Fill(dt)
                Return dt.Rows.Count
            Catch ex As Exception
                MsgBox(ex.Message)
                Return 0
            Finally
                closeKoneksi()
            End Try
        End Function
    'getvalue untuk menampilkan isi berdasarkan colum
    Function getValue(s As String, col As String)
            koneksi()
            Try
                cmd = New SqlCommand(s, conn)
                dr = cmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    If IsDBNull(dr.Item(col)) Then
                        Return ""
                    Else
                        Return dr.Item(col)
                    End If
                Else
                    Return ""
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
                Return ""
            Finally
                closeKoneksi()
            End Try
        End Function
    'exc untuk mengeksekusi sintax atau queri
    Function exc(s As String)
            koneksi()
            Try
                cmd = New SqlCommand(s, conn)
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            Finally
                closeKoneksi()
            End Try
        End Function
    'isemail untuk validasi email
    Function isEmail(s As String)
            Try
                Dim a = New MailAddress(s)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
    'adakosong untuk mengecek apakah textbox didalam groupbox kosong atau tidak 
    Function adaKosong(gb As GroupBox)
            Dim status = False
            For Each ct As Control In gb.Controls
                If TypeOf ct Is TextBox Then
                    If CType(ct, TextBox).Text = "" Then
                        status = True
                        Exit For
                    End If
                End If
            'If TypeOf ct Is PictureBox Then
            'If CType(ct, PictureBox).Image = Nothing Then
            '    status = True
            '    Exit For
            'End If
        Next
            Return status
        End Function
    'clearform untuk menghapus text box yang ada di groupbox
    Function clearForm(gb As GroupBox)
        For Each ct As Control In gb.Controls
            If TypeOf ct Is TextBox Then
                CType(ct, TextBox).Text = ""
            End If
            If TypeOf ct Is PictureBox Then
                CType(ct, PictureBox).ImageLocation = Nothing
            End If
            If TypeOf ct Is NumericUpDown Then
                CType(ct, NumericUpDown).Value = 0
            End If
        Next
    End Function
    'onlynumber agar textbox cuma diisi angka
    Function onlyNumber(e As KeyPressEventArgs)
            If Asc(e.KeyChar) <> 8 Then
                If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                    e.Handled = True
                End If
            End If
        End Function
    'dialog untuk menampilkan pesan dialog
    Function dialog(s As String)
        Dim a As DialogResult = MessageBox.Show(s, "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If a = DialogResult.Yes Then
            Return True
        End If
    End Function
End Module


