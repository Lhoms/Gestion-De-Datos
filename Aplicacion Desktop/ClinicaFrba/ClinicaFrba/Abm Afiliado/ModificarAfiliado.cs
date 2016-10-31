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
    public partial class ModificarAfiliado : Form
    {
        public Sesion sesion;
        public DataGridViewRow afiliado;

        public ModificarAfiliado(extras.Sesion sesion, DataGridViewRow dataGridViewRow)
        {
            InitializeComponent();

            this.sesion = sesion;
            this.afiliado = dataGridViewRow;

            this.labelUsuario.Text = this.afiliado.Cells[2].Value.ToString() + " - " +
                                     this.afiliado.Cells[4].Value.ToString() + " " +
                                     this.afiliado.Cells[5].Value.ToString(); ;
            this.labelNroAfil.Text = this.afiliado.Cells[13].Value.ToString(); 


        }
    }
}
