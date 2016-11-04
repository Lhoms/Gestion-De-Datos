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
    public partial class GrupoFamiliar : Form
    {
        private extras.Sesion sesion;
        private DataGridViewRow afiliado;
 

        public GrupoFamiliar(extras.Sesion sesion, DataGridViewRow dataGridViewRow)
        {
            
            this.sesion = sesion;
            this.afiliado = dataGridViewRow;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            salir(e);
        }

        private void salir(EventArgs e)
        {
            ModificarAfiliado form = new ModificarAfiliado(this.sesion, this.afiliado);
            form.Show();
            this.Hide();
        }
    }
}
