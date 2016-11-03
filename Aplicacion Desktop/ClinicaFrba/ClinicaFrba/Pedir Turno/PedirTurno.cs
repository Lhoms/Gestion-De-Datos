using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Pedir_Turno
{
    public partial class PedirTurno : Form
    {
        public PedirTurno()
        {
            InitializeComponent();

            probarSP();

        }

        private void probarSP()
        {
            //NUL.sp_get_disp_profesional(@id_prof numeric(18,0), @esp_id numeric(18,0), @fec_inicio datetime, @fec_fin datetime)

            SqlParameter[] dbParams = new SqlParameter[]
            {
                DAL.Classes.DBHelper.MakeParam("@id_prof", SqlDbType.Decimal, 250, 5576),
                DAL.Classes.DBHelper.MakeParam("@esp_id", SqlDbType.Decimal, 250, 10012),
                DAL.Classes.DBHelper.MakeParam("@fec_inicio", SqlDbType.DateTime, 250, DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema").ToString()).AddHours(-1)),
                DAL.Classes.DBHelper.MakeParam("@fec_fin", SqlDbType.DateTime, 250, DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema").ToString()).AddHours(10)),
            };

            this.dataGridView1.DataSource = DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_get_disp_profesional", dbParams).Tables[0];
        }
    }
}
