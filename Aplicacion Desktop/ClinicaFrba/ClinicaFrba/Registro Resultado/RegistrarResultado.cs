using ClinicaFrba.extras;
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

namespace ClinicaFrba.Registro_Resultado
{
    public partial class RegistrarResultado : Form
    {
        Sesion sesion;

        List<string> tipos_esp;
        Dictionary<string, int> tipos_esp_id;

        List<string> especialidades;
        Dictionary<string, int> especialidades_id;

        List<string> vacia;
        List<string> horas_descrip;
        Dictionary<string, int> horas;

        DateTime fecha_hora_minutos;
        int cons_id;


        public RegistrarResultado(Sesion sesion)
        {
            try
            {
                InitializeComponent();

                this.sesion = sesion;

                tipos_esp = new List<string>();
                tipos_esp_id = new Dictionary<string, int>();

                especialidades = new List<string>();
                especialidades_id = new Dictionary<string, int>();

                vacia = new List<string>();
                horas = new Dictionary<string, int>();
                horas_descrip = new List<string>();

                this.groupBox2.Enabled = false;

                llenarTipoEsp();
                llenarEspecialidades();

                this.dateFecha.Value = DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema"));
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void llenarTipoEsp()
        {
            this.tipos_esp_id.Clear();
            this.tipos_esp.Clear();

            get_tipo_esp();
            this.comboBoxTipoEsp.DataSource = tipos_esp;

        }

        private void llenarEspecialidades()
        {
            this.especialidades.Clear();
            this.especialidades_id.Clear();

            this.comboBoxEsp.DataSource = this.vacia;
            get_especialidades();
            this.comboBoxEsp.DataSource = this.especialidades;

        }

        private void get_tipo_esp()
        {
            string expresion = "SELECT tipo_esp_id, tipo_esp_descrip FROM NUL.Tipo_esp";

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            if (lector != null)
            {
                tipos_esp_id.Add((string)lector["tipo_esp_descrip"].ToString(), int.Parse(lector["tipo_esp_id"].ToString()));
                tipos_esp.Add((string)lector["tipo_esp_descrip"].ToString());

                while (lector.Read())
                {
                    tipos_esp_id.Add((string)lector["tipo_esp_descrip"].ToString(), int.Parse(lector["tipo_esp_id"].ToString()));
                    tipos_esp.Add((string)lector["tipo_esp_descrip"].ToString());
                }
            }

        }

        private void get_especialidades()
        {
                string expresion = "SELECT esp_id, esp_descrip FROM NUL.Especialidad WHERE esp_tipo = " + this.tipos_esp_id[this.comboBoxTipoEsp.Text].ToString();

                SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

                if (lector != null)
                {
                    especialidades_id.Add((string)lector["esp_descrip"].ToString(), int.Parse(lector["esp_id"].ToString()));
                    especialidades.Add((string)lector["esp_descrip"].ToString());

                    while (lector.Read())
                    {
                        especialidades_id.Add((string)lector["esp_descrip"].ToString(), int.Parse(lector["esp_id"].ToString()));
                        especialidades.Add((string)lector["esp_descrip"].ToString());
                    }
                }

        }


        private void comboBoxTipoEsp_SelectionChangeCommitted(object sender, EventArgs e)
        {
            llenarEspecialidades();

        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fecha = this.dateFecha.Value;

                int hora = int.Parse(this.numericHora.Text);
                int minutos = int.Parse(this.numericMinutos.Text);


                fecha = fecha.AddHours(hora);
                fecha = fecha.AddMinutes(minutos);
                this.fecha_hora_minutos = fecha;

                if (this.fecha_hora_minutos > DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema")))
                    throw new Exception("La fecha y hora no puede ser siguiente a la actual");

                buscarConsulta();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void buscarConsulta()
        {
            string expresion = "SELECT * FROM NUL.Consulta C JOIN NUL.Turno T ON C.cons_turno_id = T.turno_id ";
            string where = "WHERE T.turno_profesional =  "+ sesion.user_id +
                            " AND T.turno_especialidad = "+ this.especialidades_id[this.comboBoxEsp.Text] +
                            " AND T.turno_fecha_hora   = '" + this.fecha_hora_minutos.ToString("yyyy-MM-dd HH:mm:ss.f") + "'";

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion + where);

            if (lector != null)
            {
                this.cons_id = int.Parse(lector["cons_id"].ToString());
                this.groupBox2.Enabled = true;
            }
            else
            {
                MessageBox.Show("No atendio un turno en esa especialidad en esa fecha y hora");
                this.groupBox2.Enabled = false;
            }

            rellenarTextos();

        }

        private void rellenarTextos()
        {
            string expresion = "SELECT * FROM NUL.Consulta WHERE cons_id = " + this.cons_id;

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            if (lector != null)
            {
                this.richTextBoxDiagnostico.Text = lector["cons_enfermedades"].ToString();
                this.richTextBoxSintoma.Text = lector["cons_sintomas"].ToString();
            }
        }

        private void buttonRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParameter result = DAL.Classes.DBHelper.MakeParamOutput("@result", SqlDbType.Int, 100);

                SqlParameter[] dbParams = new SqlParameter[]
            {
                DAL.Classes.DBHelper.MakeParam("@cons_id", SqlDbType.Decimal, 0, this.cons_id),
                DAL.Classes.DBHelper.MakeParam("@sintomas", SqlDbType.VarChar, 250, this.richTextBoxSintoma.Text),
                DAL.Classes.DBHelper.MakeParam("@enfermedades", SqlDbType.VarChar, 250, this.richTextBoxDiagnostico.Text),
                result,
            };

                DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_set_resultado_consulta", dbParams);

                if ((int)result.Value != 0)
                    throw new Exception("Error modificando la consulta");
                else
                {
                    MessageBox.Show("Se modifico la consulta correctamente");
                    Form1 form = new Form1(this.sesion);
                    form.Show();
                    this.Hide();
                }
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
            this.Hide();
        }


    }
}
