Imports MySql.Data.MySqlClient

Public Class FR_LAPORAN
    Dim conn As MySqlConnection
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim cmd As MySqlCommand
    Dim dr As MySqlDataReader
    Dim strConn As String = "server=localhost;user id=root;password=;database=vbnet_projectuas;"

    Private Sub FR_LAPORAN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = New MySqlConnection(strConn)
        IsiComboBoxLaporan()
        AturRangeTanggal()
        AddHandler dgvtampil.CellFormatting, AddressOf dgvtampil_CellFormatting
    End Sub

    ' ===============================================
    '             ISI PILIHAN LAPORAN
    ' ===============================================
    Private Sub IsiComboBoxLaporan()
        cbLaporan.Items.Clear()
        cbLaporan.Items.Add("Barang Masuk")
        cbLaporan.Items.Add("Barang Keluar")
        cbLaporan.Items.Add("Semua Transaksi")
        cbLaporan.SelectedIndex = 0
    End Sub

    '================================================
    '      NO URUTAN UNTUK MENAMPILKAN LAPORAN
    '================================================
    Private Sub TambahKolomNoUrut()
        ' Hapus kolom No jika sudah ada
        If dgvtampil.Columns.Contains("No") Then
            dgvtampil.Columns.Remove("No")
        End If

        Dim colNo As New DataGridViewTextBoxColumn()
        colNo.Name = "No"
        colNo.HeaderText = "No"
        colNo.Width = 50
        colNo.ReadOnly = True
        dgvtampil.Columns.Insert(0, colNo)

        For i As Integer = 0 To dgvtampil.Rows.Count - 1
            If Not dgvtampil.Rows(i).IsNewRow Then
                dgvtampil.Rows(i).Cells("No").Value = i + 1
            End If
        Next
    End Sub

    '=================================================
    '       FORMAT SELURUH ANGKA DATA GRID VIEW
    '=================================================
    Private Sub dgvtampil_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If dgvtampil.Columns(e.ColumnIndex).HeaderText.ToLower().Contains("jumlah") OrElse
       dgvtampil.Columns(e.ColumnIndex).HeaderText.ToLower().Contains("qty") OrElse
       dgvtampil.Columns(e.ColumnIndex).HeaderText.ToLower().Contains("harga") OrElse
       dgvtampil.Columns(e.ColumnIndex).HeaderText.ToLower().Contains("total") Then

            If e.Value IsNot Nothing AndAlso IsNumeric(e.Value) Then
                e.Value = FormatNumber(Convert.ToDouble(e.Value), 0, TriState.False, TriState.False, TriState.True)
                e.FormattingApplied = True
            End If
        End If
    End Sub

    ' ===============================================
    ' BUTTON TAMPIL - MENAMPILKAN DATA SESUAI PILIHAN
    ' ===============================================
    Private Sub btnTampil_Click(sender As Object, e As EventArgs) Handles btnTampil.Click
        If cbLaporan.SelectedIndex = -1 Then
            MessageBox.Show("Pilih jenis laporan terlebih dahulu!")
            Return
        End If

        If tglMulai.Value > tglSampai.Value Then
            MessageBox.Show("Tanggal mulai tidak boleh lebih besar dari tanggal sampai!")
            Return
        End If

        Select Case cbLaporan.SelectedItem.ToString()
            Case "Barang Masuk"
                TampilkanBarangMasuk()
            Case "Barang Keluar"
                TampilkanBarangKeluar()
            Case "Semua Transaksi"
                TampilkanSemuaTransaksi()
        End Select
    End Sub

    ' ===============================================
    '           TAMPILKAN DATA BARANG MASUK
    ' ===============================================
    Private Sub TampilkanBarangMasuk()
        Try
            If conn.State = ConnectionState.Open Then conn.Close()
            conn.Open()

            Dim query As String = "SELECT kode_barang, nama_barang, satuan, jumlah, suplier, harga_partai, tanggal_masuk " &
                                "FROM transaksi_masuk " &
                                "WHERE tanggal_masuk BETWEEN @tgl1 AND @tgl2 " &
                                "ORDER BY tanggal_masuk ASC"

            da = New MySqlDataAdapter(query, conn)
            da.SelectCommand.Parameters.AddWithValue("@tgl1", tglMulai.Value.Date)
            da.SelectCommand.Parameters.AddWithValue("@tgl2", tglSampai.Value.Date.AddDays(1).AddSeconds(-1))

            ds = New DataSet()
            da.Fill(ds, "data")

            If ds.Tables("data").Rows.Count > 0 Then
                dgvtampil.DataSource = ds.Tables("data")
                AturHeaderBarangMasuk()
                TambahKolomNoUrut()
            Else
                dgvtampil.DataSource = Nothing
                MessageBox.Show("Tidak ada data barang masuk pada rentang tanggal tersebut.")
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal menampilkan data barang masuk: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' ===============================================
    '           TAMPILKAN DATA BARANG KELUAR
    ' ===============================================
    Private Sub TampilkanBarangKeluar()
        Try
            If conn.State = ConnectionState.Open Then conn.Close()
            conn.Open()

            Dim query As String = "SELECT kode_barang, nama_barang, satuan, harga_satuan, qty, total, tanggal " &
                                "FROM transaksi_keluar " &
                                "WHERE tanggal BETWEEN @tgl1 AND @tgl2 " &
                                "ORDER BY tanggal ASC"

            da = New MySqlDataAdapter(query, conn)
            da.SelectCommand.Parameters.AddWithValue("@tgl1", tglMulai.Value.Date)
            da.SelectCommand.Parameters.AddWithValue("@tgl2", tglSampai.Value.Date.AddDays(1).AddSeconds(-1))

            ds = New DataSet()
            da.Fill(ds, "data")

            If ds.Tables("data").Rows.Count > 0 Then
                dgvtampil.DataSource = ds.Tables("data")
                AturHeaderBarangKeluar()
                TambahKolomNoUrut()
            Else
                dgvtampil.DataSource = Nothing
                MessageBox.Show("Tidak ada data barang keluar pada rentang tanggal tersebut.")
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal menampilkan data barang keluar: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' ===============================================
    '       TAMPILKAN SEMUA TRANSAKSI (GABUNGAN)
    ' ===============================================
    Private Sub TampilkanSemuaTransaksi()
        Try
            If conn.State = ConnectionState.Open Then conn.Close()
            conn.Open()

            Dim query As String = "SELECT kode_barang, nama_barang, satuan, jumlah AS qty, suplier AS keterangan, harga_partai AS harga, tanggal_masuk AS tanggal, 'MASUK' AS jenis " &
                                 "FROM transaksi_masuk WHERE tanggal_masuk BETWEEN @tgl1 AND @tgl2 " &
                                 "UNION ALL " &
                                 "SELECT kode_barang, nama_barang, satuan, qty, CONCAT('Customer') AS keterangan, total AS harga, tanggal, 'KELUAR' AS jenis " &
                                 "FROM transaksi_keluar WHERE tanggal BETWEEN @tgl1 AND @tgl2 " &
                                 "ORDER BY tanggal ASC"

            da = New MySqlDataAdapter(query, conn)
            da.SelectCommand.Parameters.AddWithValue("@tgl1", tglMulai.Value.Date)
            da.SelectCommand.Parameters.AddWithValue("@tgl2", tglSampai.Value.Date.AddDays(1).AddSeconds(-1))

            ds = New DataSet()
            da.Fill(ds, "data")

            If ds.Tables("data").Rows.Count > 0 Then
                dgvtampil.DataSource = ds.Tables("data")
                AturHeaderSemuaTransaksi()
                TambahKolomNoUrut()
            Else
                dgvtampil.DataSource = Nothing
                MessageBox.Show("Tidak ada data transaksi pada rentang tanggal tersebut.")
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal menampilkan semua transaksi: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' ===============================================
    '           ATUR HEADER DATAGRIDVIEW
    ' ===============================================
    Private Sub AturHeaderBarangMasuk()
        If dgvtampil.Columns.Count >= 8 Then
            dgvtampil.Columns(1).HeaderText = "Kode Barang"
            dgvtampil.Columns(2).HeaderText = "Nama Barang"
            dgvtampil.Columns(3).HeaderText = "Satuan"
            dgvtampil.Columns(4).HeaderText = "Jumlah"
            dgvtampil.Columns(5).HeaderText = "Supplier"
            dgvtampil.Columns(6).HeaderText = "Harga Partai"
            dgvtampil.Columns(7).HeaderText = "Tanggal Masuk"

            ' Atur lebar kolom
            dgvtampil.Columns(1).Width = 40
            dgvtampil.Columns(2).Width = 150
            dgvtampil.Columns(3).Width = 60
            dgvtampil.Columns(4).Width = 80
            dgvtampil.Columns(5).Width = 120
            dgvtampil.Columns(6).Width = 100
            dgvtampil.Columns(7).Width = 120
        End If
    End Sub

    Private Sub AturHeaderBarangKeluar()
        If dgvtampil.Columns.Count >= 8 Then
            dgvtampil.Columns(1).HeaderText = "Kode Barang"
            dgvtampil.Columns(2).HeaderText = "Nama Barang"
            dgvtampil.Columns(3).HeaderText = "Satuan"
            dgvtampil.Columns(4).HeaderText = "Harga Satuan"
            dgvtampil.Columns(5).HeaderText = "Qty"
            dgvtampil.Columns(6).HeaderText = "Total"
            dgvtampil.Columns(7).HeaderText = "Tanggal Keluar"

            ' Atur lebar kolom
            dgvtampil.Columns(1).Width = 40
            dgvtampil.Columns(2).Width = 150
            dgvtampil.Columns(3).Width = 60
            dgvtampil.Columns(4).Width = 100
            dgvtampil.Columns(5).Width = 40
            dgvtampil.Columns(6).Width = 100
            dgvtampil.Columns(7).Width = 120
        End If
    End Sub

    Private Sub AturHeaderSemuaTransaksi()
        If dgvtampil.Columns.Count >= 9 Then
            dgvtampil.Columns(1).HeaderText = "Kode Barang"
            dgvtampil.Columns(2).HeaderText = "Nama Barang"
            dgvtampil.Columns(3).HeaderText = "Satuan"
            dgvtampil.Columns(4).HeaderText = "Qty"
            dgvtampil.Columns(5).HeaderText = "Keterangan"
            dgvtampil.Columns(6).HeaderText = "Harga"
            dgvtampil.Columns(7).HeaderText = "Tanggal"
            dgvtampil.Columns(8).HeaderText = "Jenis"

            ' Atur lebar kolom
            dgvtampil.Columns(1).Width = 40
            dgvtampil.Columns(2).Width = 150
            dgvtampil.Columns(3).Width = 60
            dgvtampil.Columns(4).Width = 40
            dgvtampil.Columns(5).Width = 120
            dgvtampil.Columns(6).Width = 100
            dgvtampil.Columns(7).Width = 120
            dgvtampil.Columns(8).Width = 80
        End If
    End Sub

    ' ===============================================
    '               ATUR RANGE TANGGAL
    ' ===============================================
    Sub AturRangeTanggal()
        Try
            If conn.State = ConnectionState.Open Then conn.Close()
            conn.Open()

            Dim minDate As Date = Date.Today.AddYears(-1)
            Dim maxDate As Date = Date.Today

            cmd = New MySqlCommand("SELECT MIN(tanggal_masuk), MAX(tanggal_masuk) FROM transaksi_masuk", conn)
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                If Not IsDBNull(dr(0)) Then minDate = dr.GetDateTime(0)
                If Not IsDBNull(dr(1)) Then maxDate = dr.GetDateTime(1)
            End If
            dr.Close()

            cmd = New MySqlCommand("SELECT MIN(tanggal), MAX(tanggal) FROM transaksi_keluar", conn)
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                If Not IsDBNull(dr(0)) AndAlso dr.GetDateTime(0) < minDate Then
                    minDate = dr.GetDateTime(0)
                End If
                If Not IsDBNull(dr(1)) AndAlso dr.GetDateTime(1) > maxDate Then
                    maxDate = dr.GetDateTime(1)
                End If
            End If
            dr.Close()

            tglMulai.MinDate = minDate
            tglMulai.MaxDate = maxDate
            tglSampai.MinDate = minDate
            tglSampai.MaxDate = maxDate

            tglMulai.Value = New Date(Date.Today.Year, Date.Today.Month, 1)
            tglSampai.Value = Date.Today

        Catch ex As Exception
            MessageBox.Show("Gagal mengatur range tanggal: " & ex.Message)
            tglMulai.Value = Date.Today.AddMonths(-1)
            tglSampai.Value = Date.Today
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' ===============================================
    '               EVENT CETAK
    ' ===============================================
    Private Sub btnCetak_Click(sender As Object, e As EventArgs) Handles btnCetak.Click
        If dgvtampil.DataSource Is Nothing OrElse dgvtampil.Rows.Count = 0 Then
            MessageBox.Show("Tidak ada data untuk dicetak!")
            Return
        End If

        MessageBox.Show("Fitur cetak akan dikembangkan. Data siap untuk dicetak: " & dgvtampil.Rows.Count & " record")
    End Sub

End Class