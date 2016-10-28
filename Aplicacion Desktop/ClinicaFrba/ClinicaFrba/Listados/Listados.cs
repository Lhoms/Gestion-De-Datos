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

namespace ClinicaFrba.Listados
{
    public partial class Listados : Form
    {
        string tipo_doc;
        string username;
        int user_id;

        Dictionary<string, int> planes;
        List<string> plan_descrip;

        Dictionary<string, int> especialidades;
        List<string> esp_descrip;

        DataTable dt;

        public Listados(string tipo_doc, string username, int user_id)
        {
            InitializeComponent();

            this.tipo_doc = tipo_doc;
            this.username = username;
            this.user_id = user_id;

            plan_descrip = new List<string>();
            planes = new Dictionary<string, int>();

            esp_descrip = new List<string>();
            especialidades = new Dictionary<string, int>();

            get_planes();
            get_especialidades();

            this.comboBoxPlan.DataSource = plan_descrip;
            this.comboBoxEspecialidad.DataSource = esp_descrip;

            ocultarPlanEspecialidad();

            

        }

        private void ocultarPlanEspecialidad()
        {
            this.labelPlan.Visible = false;
            this.comboBoxPlan.Visible = false;
            this.labelEspecialidad.Visible = false;
            this.comboBoxEspecialidad.Visible = false;
        }


        private void comboBoxListado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try{

                switch (this.comboBoxListado.Text)
                {

                    case "Top 5 de las especialidades que mas registrarion cancelaciones.":

                        ocultarPlanEspecialidad();
                        break;

                    case "Top 5 de los profesionales mas consultados por plan.":

                        ocultarPlanEspecialidad();
                        this.labelPlan.Visible = true;
                        this.comboBoxPlan.Visible = true;
                        break;

                    case "Top 5 de los profesionales con menos horas trabajadas por plan y especialidad.":

                        this.labelPlan.Visible = true;
                        this.comboBoxPlan.Visible = true;
                        this.labelEspecialidad.Visible = true;
                        this.comboBoxEspecialidad.Visible = true;
                        break;

                    case "Top 5 de los afiliados con mayor cantidad de bonos comprados.":

                        ocultarPlanEspecialidad();
                        break;

                    case "Top 5 de las especialidades de medicos con mas bonos de consultas utilizados.":

                        ocultarPlanEspecialidad();
                        break;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        private void get_top5_afil_bonos()
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                //DAL.Classes.DBHelper.MakeParam("@id", SqlDbType.Decimal, 0, rol_id),
                //DAL.Classes.DBHelper.MakeParam("@descrip", SqlDbType.VarChar, 0, rol_nombre),
                //DAL.Classes.DBHelper.MakeParam("@habilitado", SqlDbType.Bit, 0, habilitado),
                //result,
            };

            this.dt = DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_get_top5_afil_bonos", dbParams).Tables[0];
        }

        private void get_top5_prof_horas(string plan, string especialidad)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                //DAL.Classes.DBHelper.MakeParam("@id", SqlDbType.Decimal, 0, rol_id),
                //DAL.Classes.DBHelper.MakeParam("@descrip", SqlDbType.VarChar, 0, rol_nombre),
                //DAL.Classes.DBHelper.MakeParam("@habilitado", SqlDbType.Bit, 0, habilitado),
                //result,
            };

            this.dt = DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_get_top5_afil_bonos", dbParams).Tables[0];
        }

        private void get_top5_prof_consultados(string plan)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                //DAL.Classes.DBHelper.MakeParam("@id", SqlDbType.Decimal, 0, rol_id),
                //DAL.Classes.DBHelper.MakeParam("@descrip", SqlDbType.VarChar, 0, rol_nombre),
                //DAL.Classes.DBHelper.MakeParam("@habilitado", SqlDbType.Bit, 0, habilitado),
                //result,
            };

            this.dt = DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_get_top5_afil_bonos", dbParams).Tables[0];
        }

        private void get_top5_esp_cancel()
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                //DAL.Classes.DBHelper.MakeParam("@id", SqlDbType.Decimal, 0, rol_id),
                //DAL.Classes.DBHelper.MakeParam("@descrip", SqlDbType.VarChar, 0, rol_nombre),
                //DAL.Classes.DBHelper.MakeParam("@habilitado", SqlDbType.Bit, 0, habilitado),
                //result,
            };

            this.dt = DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_get_top5_afil_bonos", dbParams).Tables[0];
        }

        private void get_top5_esp_bonos()
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                //DAL.Classes.DBHelper.MakeParam("@id", SqlDbType.Decimal, 0, rol_id),
                //DAL.Classes.DBHelper.MakeParam("@descrip", SqlDbType.VarChar, 0, rol_nombre),
                //DAL.Classes.DBHelper.MakeParam("@habilitado", SqlDbType.Bit, 0, habilitado),
                //result,
            };

            this.dt = DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_get_top5_afil_bonos", dbParams).Tables[0];
        }

        private void get_planes()
        {
            string expresion = "SELECT plan_id, plan_descrip FROM NUL.Plan_medico";

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            if (lector.HasRows)
            {
                planes.Add((string)lector["plan_descrip"].ToString(), int.Parse(lector["plan_id"].ToString()));
                plan_descrip.Add((string)lector["plan_descrip"].ToString());

                while (lector.Read())
                {
                    planes.Add((string)lector["plan_descrip"].ToString(), int.Parse(lector["plan_id"].ToString()));
                    plan_descrip.Add((string)lector["plan_descrip"].ToString());
                }
            }

        }

        private void get_especialidades()
        {
            string expresion = "SELECT esp_id, esp_descrip FROM NUL.Especialidad";

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            if (lector.HasRows)
            {
                especialidades.Add((string)lector["esp_descrip"].ToString(), int.Parse(lector["esp_id"].ToString()));
                esp_descrip.Add((string)lector["esp_descrip"].ToString());

                while (lector.Read())
                {
                    especialidades.Add((string)lector["esp_descrip"].ToString(), int.Parse(lector["esp_id"].ToString()));
                    esp_descrip.Add((string)lector["esp_descrip"].ToString());
                }
            }

        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1(this.tipo_doc, this.username, this.user_id);
            form.Show();
            this.Close();
        }

        private void buttonMostrar_Click(object sender, EventArgs e)
        {
            switch (this.comboBoxListado.Text)
            {

                case "Top 5 de las especialidades que mas registrarion cancelaciones.":

                    get_top5_esp_cancel();
                    break;

                case "Top 5 de los profesionales mas consultados por plan.":

                    get_top5_prof_consultados(this.comboBoxPlan.Text);
                    break;

                case "Top 5 de los profesionales con menos horas trabajadas por plan y especialidad.":

                    get_top5_prof_horas(this.comboBoxPlan.Text, this.comboBoxEspecialidad.Text);
                    break;

                case "Top 5 de los afiliados con mayor cantidad de bonos comprados.":

                    get_top5_afil_bonos();
                    break;

                case "Top 5 de las especialidades de medicos con mas bonos de consultas utilizados.":

                    get_top5_esp_bonos();
                    break;
            }
        }
    }
}
