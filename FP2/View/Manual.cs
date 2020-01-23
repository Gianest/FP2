using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;
using FP2.Control;
using FP2.Model.Entity;
using FP2.Model.Repository;

namespace FP2.View
{
    public partial class Manual : Form
    {
        public Manual()
        {
            InitializeComponent();
            InisialisasiListView();
        }
        Controller controller = new Controller();
        private Repo rep;
        private List<Pegawai> listOfPegawai = new List<Pegawai>();

        private void Manual_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void Readjabatan()
        {
            label8.Text = controller.Readjab(comboBox1.Text);
        }
        private void InisialisasiListView()
        {
            listView1.View = System.Windows.Forms.View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;

            listView1.Columns.Add("No.", 35, HorizontalAlignment.Center);
            listView1.Columns.Add("Nip", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Tanggal", 107, HorizontalAlignment.Center);
            listView1.Columns.Add("Nama", 100, HorizontalAlignment.Center);
        }

        private void LoadData()
        {
            // kosongkan listview
            listView1.Items.Clear();

            // panggil method ReadAll dan tampung datanya ke dalam collection
            listOfPegawai = controller.ReadAllAbs();

            // ekstrak objek mhs dari collection
            foreach (var pg in listOfPegawai)
            {
                var noUrut = listView1.Items.Count + 1;

                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(pg.Nip);
                item.SubItems.Add(pg.Tanggal);
                item.SubItems.Add(pg.Nama);

                // tampilkan data mhs ke listview
                listView1.Items.Add(item);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Pegawai pg = new Pegawai();
            label1.Text = controller.Read(textBox1.Text);
            pg.Nip = label1.Text;
            pg.Tanggal = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            controller.Create(pg);

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var konfirmasi = MessageBox.Show("Apakah data Pegawai ingin dihapus?", "Konfirmasi",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (konfirmasi == DialogResult.Yes)
                {
                    // ambil objek mhs yang mau dihapus dari collection
                    Pegawai pg = listOfPegawai[listView1.SelectedIndices[0]];

                    // panggil operasi CRUD
                    var result = controller.Delete(pg);
                    if (result > 0) LoadData();
                }
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data Pegawai belum dipilih !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Readjabatan();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Pegawai pg = new Pegawai();
            pg.Nip = textBox2.Text;
            pg.Nama = textBox6.Text;
            pg.Golongan = comboBox1.Text;
            controller.CreatePeg(pg);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            controller.DeletePeg(textBox3.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label12.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            label12.Text = controller.ReadNama(textBox4.Text);
            label13.Text = controller.JmlAbs(textBox4.Text);
            label14.Text = controller.Readjablapo(textBox4.Text);
        }
    }
}
