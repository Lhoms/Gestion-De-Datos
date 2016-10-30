using ClinicaFrba.extras;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Abm_Afiliado
{
    public partial class MatriculaFaltante : Form
    {
        Sesion sesion;
        long matricula;

        public MatriculaFaltante(Sesion sesion)
        {
            InitializeComponent();

            this.sesion = sesion;
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.textBoxMatricula.Text))
                    throw new Exception("El campo matricula no puede estar vacio");

                this.matricula = long.Parse(this.textBoxMatricula.Text);



            //llamar al stored ||  sesion.user_id + matricula
          
                
            Form1 form = new Form1(this.sesion);
            form.Show();
            this.Close();
                
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }

        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1(this.sesion);

            form.Show();

            this.Close();
        }
    }
}
