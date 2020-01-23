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
using FP2.Model.Repository;

namespace FP2.View
{
    public partial class Formlogin : Form
    {
        public Formlogin()
        {
            InitializeComponent();
        }
        private List<Pegawai> listOfPegawai = new List<Pegawai>();
        Controller controller = new Controller();
        string usr;
        string psw;

        private void Formlogin_Load(object sender, EventArgs e)
        {
            listOfPegawai = controller.Auth();
            foreach (var pg in listOfPegawai)
            {
               usr  = pg.Admin;
               psw = pg.Password;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == usr && textBox2.Text == psw)
            {
                View.Manual manual = new View.Manual();
                manual.Show();
                this.Close();
            }
            else
                MessageBox.Show("Password/User Salah !!!", "Peringatan",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
