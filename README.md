# Stock Ease Management App (VB.NET + MySQL)

Aplikasi ini merupakan sistem manajemen inventaris berbasis Windows Forms (VB.NET) yang memudahkan pengelolaan data karyawan, barang, transaksi barang masuk/keluar, serta pembuatan laporan.


## Fitur Utama

- **Login & Register:**
  - User diperkenankan untuk login/daftar terlebih dahulu.

- **Dashboard:**
  - Tampilan di halaman utama.

- **Informasi Tanggal & Waktu:**
  - Tanggal dan waktu real-time ditampilkan di menu utama.

- **Manajemen Karyawan:**
  - Tambah, ubah, dan hapus data karyawan.

- **Manajemen Barang:**
  - Tambah, edit, dan hapus data barang.

- **Transaksi Barang Masuk:**
  - Input barang masuk, supplier, harga partai, dan jumlah.
  - Pencarian barang.
  - Paging data transaksi masuk.

- **Transaksi Barang Keluar:**
  - Input barang keluar, jumlah, dan harga satuan.
  - Cek stok otomatis sebelum transaksi.
  - Pencarian barang.
  - Cetak nota transaksi (print/preview).
  - Data transaksi keluar otomatis tersimpan ke database.

- **Pencarian Data:**
  - Pencarian barang, karyawan dan transaksi dengan fitur filter.

- **Notifikasi Stok Minimum:**
  - Peringatan otomatis jika stok barang di bawah batas minimum.

- **Print:**
  - Print data transaksi ke printer.

- **Laporan:**
  - Lihat dan cetak laporan transaksi.

- **Tentang Aplikasi:**
  - Informasi mengenai aplikasi.

- **Logout & Exit:**
  - Fitur keluar dari aplikasi dan logout akun.


## Cara Instalasi

1. Clone atau download repository ini.
2. Buka solution/project di Visual Studio 2022.
3. Buat database terlebih dahulu
4. Jalankan aplikasi


## Database

### Users

```sql
CREATE DATABASE vbnet_managementapp;

USE vbnet_managementapp;

CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(100) NOT NULL
);
```

### Karyawan

```sql
USE vbnet_managementapp;

CREATE TABLE karyawan (
    nik VARCHAR(20) PRIMARY KEY,
    nama VARCHAR(100),
    tanggal_lahir DATE,
    asal VARCHAR(100),
    jenis_kelamin ENUM('Laki-laki', 'Perempuan'),
    alamat TEXT,
    no_hp VARCHAR(15)
);
```

### Barang
```sql
CREATE TABLE barang (
    kode_barang VARCHAR(10) PRIMARY KEY,
    nama_barang VARCHAR(100),
    satuan VARCHAR(50),
    harga_satuan INT
);
```

### Transaksi_masuk
```sql
CREATE TABLE transaksi_masuk (
    id INT AUTO_INCREMENT PRIMARY KEY,
    kode_barang VARCHAR(10) NOT NULL,
    nama_barang VARCHAR(100) NOT NULL,
    satuan VARCHAR(50) NOT NULL,
    jumlah INT NOT NULL,
    suplier VARCHAR(100) NOT NULL,
    harga_partai INT NOT NULL,
    tanggal_masuk DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (kode_barang) REFERENCES barang(kode_barang)
);
```
### Transaksi_keluar
```sql
CREATE TABLE transaksi_keluar (
    id INT AUTO_INCREMENT PRIMARY KEY,
    kode_barang VARCHAR(50),
    nama_barang VARCHAR(100),
    satuan VARCHAR(20),
    harga_satuan INT,
    qty INT,
    total INT,
    tanggal DATETIME DEFAULT CURRENT_TIMESTAMP
);
```


## Cara Penggunaan

1. Login/register terlebih dahulu.
2. Navigasi melalui menu utama untuk mengakses fitur:
   - **Karyawan:** Kelola data karyawan.
   - **Barang:** Kelola data barang.
   - **Barang Masuk/Keluar:** Catat transaksi barang.
   - **Laporan:** Lihat laporan transaksi.
   - **Tentang:** Informasi aplikasi.
3. Gunakan menu **Logout** untuk keluar dari sesi, atau **Exit** untuk menutup aplikasi.


## Struktur Menu

- **Karyawan**
- **Barang**
- **Barang Masuk**
- **Barang Keluar**
- **Laporan**
- **Tentang**
- **Logout**
- **Exit**


## Kontribusi

Silakan buat pull request atau issue untuk perbaikan dan pengembangan fitur.

---

**Catatan:**  
Aplikasi ini dikembangkan menggunakan Visual Basic .NET dan Windows Forms. Pastikan .NET Framework yang sesuai sudah terpasang di komputermu.
