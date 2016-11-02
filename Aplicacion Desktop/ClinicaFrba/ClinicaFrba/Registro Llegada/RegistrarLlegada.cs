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

namespace ClinicaFrba.Registro_Llegada
{
    public partial class RegistrarLlegada : Form
    {
        public Sesion sesion;
        DataTable turnos;

        List<string> tipos_esp;
        Dictionary<string, int> tipos_esp_id;

        List<string> especialidades;
        Dictionary<string, int> especialidades_id;

        List<string> vacia;

        List<Profesional> profesionales;
        List<string> profesionales_na;

        public RegistrarLlegada(Sesion sesion)
        {
            InitializeComponent();

            tipos_esp = new List<string>();
            tipos_esp_id = new Dictionary<string, int>();

            especialidades = new List<string>();
            especialidades_id = new Dictionary<string, int>();

            vacia = new List<string>();

            profesionales = new List<Profesional>();
            profesionales_na = new List<string>();

            this.sesion = sesion;
            this.labelFechaActual.Text = DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema")).ToString();

            llenarTipoEsp();
            llenarEspecialidades();
            cargarProfesionales();

        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            validarBono();
            validarNroAfiliado();
            llenarTurnosSegunNroAfiliado();
        }

        private void validarNroAfiliado()
        {
            ////////////a
            ////////////a
            ////////////a
            ////////////a
            ////////////a
        }

        private void validarBono()
        {
            ////////////a
            ////////////a
            ////////////a
            ////////////a
            ////////////a
        }

        private void llenarTurnosSegunNroAfiliado()
        {
            try
            {
                DateTime hoy = DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema"));

                SqlParameter[] dbParams = new SqlParameter[]
            {
                DAL.Classes.DBHelper.MakeParam("@user_id", SqlDbType.Decimal, 250, obtenerUserId()),
                DAL.Classes.DBHelper.MakeParam("@desde", SqlDbType.DateTime, 250, DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema"))),
                DAL.Classes.DBHelper.MakeParam("@hasta", SqlDbType.DateTime, 250, DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema"))),
                DAL.Classes.DBHelper.MakeParam("@prof_id", SqlDbType.VarChar, 250, obtenerProfesionalId()),
                DAL.Classes.DBHelper.MakeParam("@esp_id", SqlDbType.VarChar, 250, this.especialidades_id[this.comboBoxEsp.Text]),
            };

                turnos = DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_get_turnos_pedidos", dbParams).Tables[0];

                this.dataGridView1.DataSource = turnos;
                this.dataGridView1.Columns[1].Visible = false;
                this.dataGridView1.Columns[2].Visible = false;
                this.dataGridView1.Columns[3].Visible = false;
                this.dataGridView1.Columns[4].Visible = false;
                this.dataGridView1.Columns[5].HeaderText = "Fecha y Hora";
                this.dataGridView1.Columns[6].HeaderText = "Hora del turno";
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private object obtenerProfesionalId()
        {
            return this.profesionales[this.comboBoxProfesional.SelectedIndex].id;
        }

        private int obtenerUserId()
        {
            return this.sesion.user_id;
        }


        private void llenarTipoEsp()
        {
            try
            {
                this.tipos_esp_id.Clear();
                this.tipos_esp.Clear();

                get_tipo_esp();
                this.comboBoxTipoEsp.DataSource = tipos_esp;


            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }

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
            try
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
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }

        }

        private void get_especialidades()
        {
            try
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
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }

        }


        private void comboBoxTipoEsp_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                llenarEspecialidades();
                cargarProfesionales();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }

        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1(this.sesion);
            form.Show();
            this.Hide();
        }

        private void cargarProfesionales()
        {
            int id;
            string nombre;
            string apellido;
            Profesional prof;
            this.profesionales.Clear();
            this.profesionales_na.Clear();
            this.comboBoxProfesional.DataSource = this.vacia;

            string expresion = "SELECT DISTINCT * FROM NUL.Profesional P JOIN NUL.Profesional_especialidad PE ON  P.prof_id = PE.prof_id " +
                                "JOIN NUL.Especialidad E ON PE.esp_id = E.esp_id " +
                                "JOIN NUL.Persona PERS ON PERS.pers_id = P.prof_id ";

            string where = "WHERE E.esp_id = " + this.especialidades_id[this.comboBoxEsp.Text] +
                            " AND E.esp_tipo = " + this.tipos_esp_id[this.comboBoxTipoEsp.Text];


            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion + where);

            if (lector != null)
            {
                id = int.Parse(lector["prof_id"].ToString());
                nombre = lector["pers_nombre"].ToString();
                apellido = lector["pers_apellido"].ToString();

                prof = new Profesional(id, nombre, apellido);

                profesionales.Add(prof);
                profesionales_na.Add(prof.nombre_apellido);


                while (lector.Read())
                {
                    id = int.Parse(lector["prof_id"].ToString());
                    nombre = lector["pers_nombre"].ToString();
                    apellido = lector["pers_apellido"].ToString();

                    prof = new Profesional(id, nombre, apellido);

                    profesionales.Add(prof);
                    profesionales_na.Add(prof.nombre_apellido);
                }

                this.comboBoxProfesional.DataSource = this.profesionales_na;
            }
            else
                throw new Exception("No se encuentran profesionales para esa especialidad");
        }

        private void comboBoxEsp_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                cargarProfesionales();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name.Equals("Cancelar"))
            {
                ///
                ///Llamar stored para crear el turno y marcar el bono
                ///
                ///
            }
        }
    }
}
