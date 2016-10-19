using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Login
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {

                if (string.IsNullOrEmpty(textBoxUser.Text))
                    throw new Exception("El campo usuario no puede estar vacio");


                if (string.IsNullOrEmpty(textBoxPass.Text))
                    throw new Exception("El campo contraseña no puede estar vacio");

                Form1 form = new Form1(textBoxUser.Text);

                form.Show();

                this.Hide();
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }
    }
}
