Public Class login
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Form1.Showdialog
        Dim sql = "select * from penjual where username = '" & TextBox1.Text & "' and password = '" & TextBox2.Text & "'"
        'MsgBox(getCount(sql))
        If TextBox1.Text = "" And TextBox2.Text = "" Then
            MsgBox("Data Masih Kosong")
        Else
            If getCount(sql) > 0 Then
                Form1.Label3.Text = getValue(sql, "penjual")
                Form1.idpetugas = getValue(sql, "idpenjual")
                Form1.Show()
                Me.Hide()
                TextBox1.Clear()
                TextBox2.Clear()
                'MsgBox(getValue(sql, "penjual"))
            Else
                MsgBox("Username atau Password salah")
            End If
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Clear()
        TextBox2.Clear()
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox2.UseSystemPasswordChar = False
        Else
            TextBox2.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub
End Class