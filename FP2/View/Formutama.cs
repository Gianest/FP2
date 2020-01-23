using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FP2.Control;
using FP2.Model.Entity;

namespace FP2
{
    public partial class Formutama : Form
    {
        private Controller controller;
        private List<Pegawai> listOfPegawai = new List<Pegawai>();
        public Formutama()
        {
            InitializeComponent();
            controller = new Controller();
            InisialisasiListView();

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

        private void tambahAbsenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View.FormPresensi f = new View.FormPresensi();
            f.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Formutama_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
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

        private void Formutama_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }

        private void modeAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View.Formlogin f = new View.Formlogin();
            f.Show();
            /*View.Manual manual = new View.Manual();
            manual.Show();*/

        }
    }
}
