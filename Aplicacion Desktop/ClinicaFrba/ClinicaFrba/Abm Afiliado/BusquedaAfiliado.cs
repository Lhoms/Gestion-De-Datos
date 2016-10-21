using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Abm_Afiliado
{
    public partial class BusquedaAfiliado : Form
    {
        public BusquedaAfiliado()
        {
            InitializeComponent();

            

        }


        public static DataSet getAfiliadosSegun()
        {
            return new DataSet();

        }


    }
}
