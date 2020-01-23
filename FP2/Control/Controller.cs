using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using FP2.Model.Entity;
using FP2.Model.Repository;
using FP2.Model.Context;


namespace FP2.Control
{
    public class Controller
    {
        private Repo _repository;

        public List<Pegawai> ReadAll()
        {
            // membuat objek collection
            List<Pegawai> list = new List<Pegawai>();

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new Repo(context);

                // panggil method GetAll yang ada di dalam class repository
                list = _repository.ReadAll();
            }

            return list;
        }
        public List<Pegawai> ReadAllAbs()
        {
            // membuat objek collection
            List<Pegawai> list = new List<Pegawai>();

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new Repo(context);

                // panggil method GetAll yang ada di dalam class repository
                list = _repository.ReadAllAbs();
            }

            return list;
        }
        public List<Pegawai> Auth()
        {
            // membuat objek collection
            List<Pegawai> list = new List<Pegawai>();

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new Repo(context);

                // panggil method GetAll yang ada di dalam class repository
                list = _repository.Auth();
            }

            return list;
        }
        public string ReadNama(string nip)
        {
            string result;
            using (DbContext context = new DbContext())
            {
                // membuat objek class repository
                _repository = new Repo(context);

                // panggil method Create class repository untuk menambahkan data
                result = _repository.ReadNama(nip);
            }
            return result;
        }
        public string JmlAbs(string nip)
        {
            string result;
            using (DbContext context = new DbContext())
            {
                // membuat objek class repository
                _repository = new Repo(context);

                // panggil method Create class repository untuk menambahkan data
                result = _repository.JmlAbs(nip);
            }
            return result;
        }
        public int Create(Pegawai pg)
        {
            int result = 0;
            using (DbContext context = new DbContext())
            {
                // membuat objek class repository
                _repository = new Repo(context);

                // panggil method Create class repository untuk menambahkan data
                result = _repository.Create(pg);
            }
            if (result > 0)
            { 
               MessageBox.Show("Data Pegawai berhasil disimpan !", "Informasi",MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Data Pegawai Gagal disimpan !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return result;
        }
        public int CreatePeg(Pegawai pg)
        {
            int result = 0;
            using (DbContext context = new DbContext())
            {
                // membuat objek class repository
                _repository = new Repo(context);

                // panggil method Create class repository untuk menambahkan data
                result = _repository.CreatePeg(pg);
            }
            if (result > 0)
            {
                MessageBox.Show("Data Pegawai berhasil disimpan !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Data Pegawai Gagal disimpan !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return result;
        }
        public string Read(string nip)
        {
            string result;
            using (DbContext context = new DbContext())
            {
                // membuat objek class repository
                _repository = new Repo(context);

                // panggil method Create class repository untuk menambahkan data
                result = _repository.Read(nip);
            }
            return result;
        }
        public string Readjab(string gol)
        {
            string result;
            using (DbContext context = new DbContext())
            {
                // membuat objek class repository
                _repository = new Repo(context);

                // panggil method Create class repository untuk menambahkan data
                result = _repository.Readjab(gol);
            }
            return result;
        }
        public string Readjablapo(string gol)
        {
            string result;
            using (DbContext context = new DbContext())
            {
                // membuat objek class repository
                _repository = new Repo(context);

                // panggil method Create class repository untuk menambahkan data
                result = _repository.Readjablapo(gol);
            }
            return result;
        }
        public int DeletePeg(string nip)
        {
            int result = 0;
            if (string.IsNullOrEmpty(nip))
            {
                MessageBox.Show("Nip harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            using (DbContext context = new DbContext())
            {
                // membuat objek class repository
                _repository = new Repo(context);

                // panggil method Create class repository untuk menambahkan data
                result = _repository.DeletePeg(nip);
            }
            if (result > 0)
            {
                MessageBox.Show("Data Pegawai berhasil dihapus !", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data Pegawai gagal dihapus !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }
        public int Delete(Pegawai pg)
        {
            int result = 0;

            // cek nilai npm yang diinputkan tidak boleh kosong
            if (string.IsNullOrEmpty(pg.Nip))
            {
                MessageBox.Show("Nip harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new Repo(context);

                // panggil method Delete class repository untuk menghapus data
                result = _repository.Delete(pg);
            }

            if (result > 0)
            {
                MessageBox.Show("Data Pegawai berhasil dihapus !", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data Pegawai gagal dihapus !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }
    }
}
