using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();

        }

        public Form1(String usuario)
        {

            InitializeComponent();


            //comboBoxRol.DataSource = Recibe DataRow;

            ID_Usuario.Text = usuario;

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void buttonCerrarSesion_Click(object sender, EventArgs e)
        {
            Login.Login login = new Login.Login();
            login.Show();

            this.Hide();


        }

        private void comboBoxRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            //aca calcularia que puede hacer en cada rol
        }

        private void buttonModificarAfiliado_Click(object sender, EventArgs e)
        {

        }

        private void buttonModificarRol_Click(object sender, EventArgs e)
        {

        }
    }
}
