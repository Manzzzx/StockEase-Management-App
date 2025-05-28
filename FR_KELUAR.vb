Imports MySql.Data.MySqlClient
Public Class FR_KELUAR
    Private Sub btnMenu_Click(sender As Object, e As EventArgs) Handles btnMenu.Click
        Dim fr As New FR_MENU
        fr.Show()
        Me.Close()
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub txtKode_TextChanged(sender As Object, e As EventArgs) Handles txtKode.TextChanged
        If txtKode.Text.Trim = "" Then
            txtBarang.Clear()
            txtSatuan.Clear()
            txtHarga.Clear()
            Return
        End If

        Try
            BukaKoneksi()
            Dim cmd As New MySqlCommand("SELECT nama_barang, satuan, harga_satuan FROM barang WHERE kode_barang = @kode", conn)
            cmd.Parameters.AddWithValue("@kode", txtKode.Text.Trim)

            Using rd As MySqlDataReader = cmd.ExecuteReader()
                If rd.Read() Then
                    txtBarang.Text = rd("nama_barang").ToString()
                    txtSatuan.Text = rd("satuan").ToString()
                    txtHarga.Text = rd("harga_satuan").ToString()
                    rd.Close()
                    If False Then
                        MessageBox.Show("Stok barang tidak mencukupi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        txtKode.Clear()
                        txtBarang.Clear()
                        txtSatuan.Clear()
                        txtHarga.Clear()
                        txtKode.Focus()
                    End If
                Else
                    txtBarang.Clear()
                    txtSatuan.Clear()
                    txtHarga.Clear()
                End If
            End Using
        Catch ex As Exception
            MsgBox("Terjadi kesalahan saat mengambil data barang: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub FR_KELUAR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler txtJumlah.KeyDown, AddressOf txtJumlah_KeyDown
        AddHandler dgvTampil.CellEndEdit, AddressOf dgvTampil_CellEndEdit
        AddHandler dgvTampil.EditingControlShowing, AddressOf dgvTampil_EditingControlShowing

        txtKode.Text = "BRG0"
        txtKode.SelectionStart = txtKode.Text.Length
        txtKode.SelectionLength = 0
    End Sub

    Private Sub txtJumlah_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            ' Validasi input
            If txtKode.Text.Trim = "" OrElse txtBarang.Text.Trim = "" OrElse txtSatuan.Text.Trim = "" OrElse txtHarga.Text.Trim = "" Then
                MessageBox.Show("Lengkapi data barang terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Ambil dan validasi jumlah
            Dim qty As Integer = 1
            If Not String.IsNullOrWhiteSpace(txtJumlah.Text) Then
                Integer.TryParse(txtJumlah.Text, qty)
                If qty <= 0 Then qty = 1
            End If

            ' Cek stok
            Dim stok_tersedia As Integer = cari_stok(txtKode.Text.Trim)
            If qty > stok_tersedia Then
                MessageBox.Show("Melebihi stok! Stok tersedia: " & stok_tersedia, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtJumlah.Focus()
                txtJumlah.SelectAll()
                Return
            End If

            ' Hitung total harga
            Dim harga As Decimal
            Decimal.TryParse(txtHarga.Text, harga)
            Dim total As Decimal = harga * qty

            ' Tambahkan atau update baris di DataGridView
            Dim found As Boolean = False
            For Each row As DataGridViewRow In dgvTampil.Rows
                If row.Cells("Kode").Value IsNot Nothing AndAlso row.Cells("Kode").Value.ToString() = txtKode.Text.Trim Then
                    Dim currentQty As Integer = Convert.ToInt32(row.Cells("Qty").Value)
                    currentQty += qty
                    row.Cells("Qty").Value = currentQty
                    row.Cells("Total").Value = harga * currentQty
                    found = True
                    Exit For
                End If
            Next

            If Not found Then
                dgvTampil.Rows.Add(txtKode.Text, txtBarang.Text, txtSatuan.Text, harga, qty, total)
            End If

            ' Update total harga semua barang
            UpdateTotalHarga()

            ' Reset input
            txtKode.Text = "BRG0"
            txtKode.SelectionStart = txtKode.Text.Length
            txtBarang.Clear()
            txtSatuan.Clear()
            txtHarga.Clear()
            txtJumlah.Clear()
            txtKode.Focus()
        End If
    End Sub


    Private Sub UpdateTotalHarga()
        Dim sum As Decimal = 0
        For Each row As DataGridViewRow In dgvTampil.Rows
            If row.Cells("Total").Value IsNot Nothing Then
                Dim val As Decimal
                If Decimal.TryParse(row.Cells("Total").Value.ToString(), val) Then
                    sum += val
                End If
            End If
        Next
        lblHarga.Text = sum.ToString("N0")
    End Sub


    Private Sub HapusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HapusToolStripMenuItem.Click
        If dgvTampil.SelectedRows.Count > 0 Then
            Dim result As DialogResult = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                dgvTampil.Rows.Remove(dgvTampil.SelectedRows(0))
                UpdateTotalHarga() ' Supaya label total harga di sebelah kanan ikut berubah
            End If
        Else
            MessageBox.Show("Pilih baris yang ingin dihapus terlebih dahulu.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub


    Private Sub dgvTampil_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
        ' Jika kolom Qty yang diedit
        If dgvTampil.Columns(e.ColumnIndex).Name = "Qty" Then
            Dim row As DataGridViewRow = dgvTampil.Rows(e.RowIndex)
            Dim harga As Decimal
            Dim qty As Integer

            Decimal.TryParse(row.Cells("Harga").Value.ToString(), harga)
            Integer.TryParse(row.Cells("Qty").Value.ToString(), qty)

            ' Ambil kode barang dari baris yang diedit
            Dim kodeBarang As String = row.Cells("Kode").Value.ToString()

            ' Hitung stok yang tersedia dengan mengurangi qty baris yang sedang diedit
            Dim stok_tersedia As Integer = cari_stok(kodeBarang) + qty

            If qty > stok_tersedia Then
                MessageBox.Show("Melebihi stok! Stok tersedia: " & stok_tersedia, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                row.Cells("Qty").Value = stok_tersedia
                qty = stok_tersedia
            End If

            row.Cells("Total").Value = harga * qty
            UpdateTotalHarga()
        End If
    End Sub

    Private Sub dgvTampil_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgvTampil.EditingControlShowing
        If dgvTampil.CurrentCell.ColumnIndex = dgvTampil.Columns("Qty").Index Then
            Dim tb As TextBox = TryCast(e.Control, TextBox)
            If tb IsNot Nothing Then
                RemoveHandler tb.KeyPress, AddressOf QtyColumn_KeyPress
                AddHandler tb.KeyPress, AddressOf QtyColumn_KeyPress
            End If
        End If
    End Sub

    Private Sub dgvTampil_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvTampil.CellMouseDown
        If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 Then
            dgvTampil.ClearSelection()
            dgvTampil.Rows(e.RowIndex).Selected = True
        End If
    End Sub

    Private Sub QtyColumn_KeyPress(sender As Object, e As KeyPressEventArgs)
        ' Hanya izinkan angka dan tombol kontrol (backspace, delete, dll)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Function cari_stok(ByVal kode As String) As Integer
        Dim stok_masuk As Integer = 0
        Dim stok_keluar As Integer = 0

        Try
            BukaKoneksi()
            ' Hitung jumlah masuk
            Dim cmdMasuk As New MySqlCommand("SELECT COALESCE(SUM(jumlah),0) FROM transaksi_masuk WHERE kode_barang = @kode", conn)
            cmdMasuk.Parameters.AddWithValue("@kode", kode)
            stok_masuk = Convert.ToInt32(cmdMasuk.ExecuteScalar())

            ' Cek apakah tabel transaksi_keluar ada dan kolom jumlah ada
            ' Jika tidak ada, stok_keluar tetap 0
            Try
                Dim cmdKeluar As New MySqlCommand("SELECT COALESCE(SUM(jumlah),0) FROM transaksi_keluar WHERE kode_barang = @kode", conn)
                cmdKeluar.Parameters.AddWithValue("@kode", kode)
                stok_keluar = Convert.ToInt32(cmdKeluar.ExecuteScalar())
            Catch ex2 As Exception
                stok_keluar = 0
            End Try
        Catch ex As Exception
            MessageBox.Show("Gagal mengambil stok: " & ex.Message)
        Finally
            conn.Close()
        End Try

        ' Hitung qty yang sudah diinput user di dgvTampil
        Dim stok_order As Integer = 0
        For Each row As DataGridViewRow In dgvTampil.Rows
            If row.Cells("Kode").Value IsNot Nothing AndAlso row.Cells("Kode").Value.ToString() = kode Then
                Dim qty As Integer = 0
                Integer.TryParse(row.Cells("Qty").Value.ToString(), qty)
                stok_order += qty
            End If
        Next

        Return (stok_masuk - stok_keluar) - stok_order
    End Function
End Class