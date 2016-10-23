using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.AbmRol
{
    public partial class BusquedaRol : Form
    {
        private string accion;

        public BusquedaRol()
        {
            InitializeComponent();
        }

        public BusquedaRol(string accion)
        {
            InitializeComponent();
            // aca va a desplegar todo en funcion de si accion es baja o modificacion

            this.accion = accion;
        }
    }
}
