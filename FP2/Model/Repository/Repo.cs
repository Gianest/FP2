using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
using FP2.Model.Entity;
using FP2.Model.Context;

namespace FP2.Model.Repository
{
   public class Repo
    {
        private OleDbConnection _conn;
        public Repo(DbContext context)
        {
            _conn = context.Conn;
        }
        public int Create(Pegawai pg)
        {
            int res = 0;

            // deklarasi perintah SQL
            string sql = @"insert into Absensi (id_kar,tanggal)
                           values (@id_kar, @tanggal)";

            // membuat objek command menggunakan blok using
            using (OleDbCommand cmd = new OleDbCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@id_kar", pg.Nip);
                cmd.Parameters.AddWithValue("#'"+"@tanggal"+"'#", pg.Tanggal);

                try
                {
                    // jalankan perintah INSERT dan tampung hasilnya ke dalam variabel result
                    res = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                }
            }

            return res;
        }
        public int CreatePeg(Pegawai pg)
        {
            int res = 0;

            // deklarasi perintah SQL
            string sql = @"insert into Karyawan (id_kar,nama,id_jabatan)
                           values (@id_kar, @nama, @id_jabatan)";

            // membuat objek command menggunakan blok using
            using (OleDbCommand cmd = new OleDbCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@id_kar", pg.Nip);
                cmd.Parameters.AddWithValue("@nama", pg.Nama);
                cmd.Parameters.AddWithValue("@id_jabatan", pg.Golongan);

                try
                {
                    // jalankan perintah INSERT dan tampung hasilnya ke dalam variabel result
                    res = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                }
            }

            return res;
        }
        public List<Pegawai> Auth()
        {

            List<Pegawai> list = new List<Pegawai>();

            try
            {
                // deklarasi perintah SQL
                string sql = @"select user,password
                               from Admin";

                // membuat objek command menggunakan blok using
                using (OleDbCommand cmd = new OleDbCommand(sql, _conn))
                {
                    // membuat objek dtr (data reader) untuk menampung result set (hasil perintah SELECT)
                    using (OleDbDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                        while (dtr.Read())
                        {
                            // proses konversi dari row result set ke object
                            Pegawai pg = new Pegawai();
                            pg.Admin = dtr["user"].ToString();
                            pg.Password = dtr["password"].ToString();

                            // tambahkan objek mahasiswa ke dalam collection
                            list.Add(pg);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadAll error: {0}", ex.Message);
            }
            return list;

        }
        public string Read(string nip)
        {

            string res = null ;

            // deklarasi perintah SQL
            string sql = @"Select id_kar from Karyawan
                           where id_kar = @id_kar";
            try
            {
                // membuat objek command menggunakan blok using
                using (OleDbCommand cmd = new OleDbCommand(sql, _conn))
                {
                    // mendaftarkan parameter dan mengeset nilainya
                    cmd.Parameters.AddWithValue("@id_kar", nip);
                    using (OleDbDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                        while (dtr.Read())
                        {
                            // proses konversi dari row result set ke object
                            Pegawai pg = new Pegawai();
                            pg.Nip = dtr["id_kar"].ToString();
                            res = pg.Nip;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByNama error: {0}", ex.Message);
            }
            return res;
        }
        public string Readjab(string gol)
        {

            string res = null;

            // deklarasi perintah SQL
            string sql = @"Select jabatan from Jabatan
                           where id_jabatan = @id_jabatan";
            try
            {
                // membuat objek command menggunakan blok using
                using (OleDbCommand cmd = new OleDbCommand(sql, _conn))
                {
                    // mendaftarkan parameter dan mengeset nilainya
                    cmd.Parameters.AddWithValue("@id_jabatan", gol);
                    using (OleDbDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                        while (dtr.Read())
                        {
                            // proses konversi dari row result set ke object
                            res = dtr["jabatan"].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByNama error: {0}", ex.Message);
            }
            return res;
        }
        public string Readjablapo(string gol)
        {

            string res = null;

            // deklarasi perintah SQL
            string sql = @"select jabatan from (Karyawan inner join Jabatan on Karyawan.id_jabatan = Jabatan.id_jabatan) where id_kar= @id_kar";
            try
            {
                // membuat objek command menggunakan blok using
                using (OleDbCommand cmd = new OleDbCommand(sql, _conn))
                {
                    // mendaftarkan parameter dan mengeset nilainya
                    cmd.Parameters.AddWithValue("@id_jabatan", gol);
                    using (OleDbDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                        while (dtr.Read())
                        {
                            // proses konversi dari row result set ke object
                            res = dtr["jabatan"].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByNama error: {0}", ex.Message);
            }
            return res;
        }
        public string ReadNama(string nip)
        {

            string res = null;

            // deklarasi perintah SQL
            string sql = @"Select nama from Karyawan
                           where id_kar = @id_kar";
            try
            {
                // membuat objek command menggunakan blok using
                using (OleDbCommand cmd = new OleDbCommand(sql, _conn))
                {
                    // mendaftarkan parameter dan mengeset nilainya
                    cmd.Parameters.AddWithValue("@id_kar", nip);
                    using (OleDbDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                        while (dtr.Read())
                        {
                            // proses konversi dari row result set ke object
                            res = dtr["nama"].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByNama error: {0}", ex.Message);
            }
            return res;
        }
        public string JmlAbs(string nip)
        {

            string res = null;

            // deklarasi perintah SQL
            string sql = @"select count(id_kar) from Absensi as jumlah
                           where id_kar = @id_kar";
            try
            {
                // membuat objek command menggunakan blok using
                using (OleDbCommand cmd = new OleDbCommand(sql, _conn))
                {
                    // mendaftarkan parameter dan mengeset nilainya
                    cmd.Parameters.AddWithValue("@id_kar", nip);
                    using (OleDbDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                        while (dtr.Read())
                        {
                            // proses konversi dari row result set ke object
                            res = dtr["Expr1000"].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByNama error: {0}", ex.Message);
            }
            return res;
        }
        /*public int Check(Pegawai pg)
        {

            int result = 0;

            // deklarasi perintah SQL
            string sql = @"Select id_kar,tanggal from Absensi
                           where id_kar = @id_kar and tanggal = @tanggal";
            try
            {
                // membuat objek command menggunakan blok using
                using (OleDbCommand cmd = new OleDbCommand(sql, _conn))
                {
                    // mendaftarkan parameter dan mengeset nilainya
                    var time = DateTime.Now.ToString("yyyy-MM-dd");
                    cmd.Parameters.AddWithValue("@id_kar", pg.Nip);
                    cmd.Parameters.AddWithValue("#'" + "@tanggal" + "'#", time);
                    using (OleDbDataReader dtr = cmd.ExecuteReader())
                        try
                        {
                            // jalankan perintah INSERT dan tampung hasilnya ke dalam variabel result
                            result = cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                        }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByNama error: {0}", ex.Message);
            }
            return result ;
        }*/
        public List<Pegawai> ReadAll()
        {
            // membuat objek collection untuk menampung objek Pegawai
            List<Pegawai> list = new List<Pegawai>();

            try
            {
                // deklarasi perintah SQL
                string sql = @"select id_kar, nama, id_jabatan
                               from Karyawan
                               order by nama";

                // membuat objek command menggunakan blok using
                using (OleDbCommand cmd = new OleDbCommand(sql, _conn))
                {
                    // membuat objek dtr (data reader) untuk menampung result set (hasil perintah SELECT)
                    using (OleDbDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                        while (dtr.Read())
                        {
                            // proses konversi dari row result set ke object
                            Pegawai pg = new Pegawai();
                            pg.Nip = dtr["id_kar"].ToString();
                            pg.Nama = dtr["nama"].ToString();
                            pg.Golongan = dtr["id_jabatan"].ToString();

                            // tambahkan objek mahasiswa ke dalam collection
                            list.Add(pg);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadAll error: {0}", ex.Message);
            }
            return list;
        }
        public List<Pegawai> ReadAllAbs()
        {
            int result = 0;
            // membuat objek collection untuk menampung objek Pegawai
            List<Pegawai> list = new List<Pegawai>();

            try
            {
                // deklarasi perintah SQL
                string sql = @"select Absensi.id_kar, tanggal, nama from (Absensi inner join Karyawan on Absensi.id_kar = Karyawan.id_kar ) where tanggal between Date() and Date()+1";

                // membuat objek command menggunakan blok using
                using (OleDbCommand cmd = new OleDbCommand(sql, _conn))
                {
                    //var time = DateTime.Now.ToString("yyyy-MM-dd");
                    //cmd.Parameters.AddWithValue("@tanggal", "#'" + time + "'#");
                    // membuat objek dtr (data reader) untuk menampung result set (hasil perintah SELECT)
                    using (OleDbDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                        while (dtr.Read())
                        {
                            // proses konversi dari row result set ke object
                            Pegawai pg = new Pegawai();
                            pg.Nip = dtr["id_kar"].ToString();
                            pg.Tanggal = dtr["tanggal"].ToString();
                            pg.Nama = dtr["nama"].ToString();

                            // tambahkan objek mahasiswa ke dalam collection
                            list.Add(pg);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadAll error: {0}", ex.Message);
            }
            return list;
        }
        public int DeletePeg(string nip)
        {

            int result = 0;

            // deklarasi perintah SQL
            string sql = @"delete from Karyawan where id_kar = @id_kar";
            using (OleDbCommand cmd = new OleDbCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@id_kar", nip);

                try
                {
                    // jalankan perintah DELETE dan tampung hasilnya ke dalam variabel result
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Delete error: {0}", ex.Message);
                }
            }

            return result;
        }
        public int Delete(Pegawai pg)
        {
            int result = 0;

            // deklarasi perintah SQL
            string sql = @"delete from Absensi where tanggal = @tanggal";

            // membuat objek command menggunakan blok using
            using (OleDbCommand cmd = new OleDbCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@tanggal", pg.Tanggal);

                try
                {
                    // jalankan perintah DELETE dan tampung hasilnya ke dalam variabel result
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Delete error: {0}", ex.Message);
                }
            }

            return result;
        }

    }
}
