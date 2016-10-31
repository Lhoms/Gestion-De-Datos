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
    public partial class ModificarAfiliado : Form
    {
        private Sesion sesion;
        private DataGridViewRow afiliado;

        public ModificarAfiliado(extras.Sesion sesion, DataGridViewRow dataGridViewRow)
        {
            this.sesion = sesion;
            this.afiliado = dataGridViewRow;


        }
    }
}
