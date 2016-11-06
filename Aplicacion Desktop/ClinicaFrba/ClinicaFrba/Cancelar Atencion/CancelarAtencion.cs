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

namespace ClinicaFrba.Cancelar_Atencion
{
    public partial class CancelarAtencion : Form
    {
        public Sesion sesion;

        List<string> tipos_esp;
        Dictionary<string, int> tipos_esp_id;

        List<string> especialidades;
        Dictionary<string, int> especialidades_id;

        List<string> vacia;

        List<Profesional> profesionales;
        List<string> profesionales_na;

        DataTable turnos;
        DateTime fechaActual;

        public CancelarAtencion(Sesion sesion)
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

                profesionales = new List<Profesional>();
                profesionales_na = new List<string>();

                comprobarSiEsProfesional();

                llenarTipoEsp();
                llenarEspecialidades();

                llenarCalendarios();

                cargarProfesionales();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void llenarCalendarios()
        {
            //setea todo lo respecto a fechas;
            //pone los calendarios desde en el dia de mañana a las 12:00am y el calendario hasta
            //en el dia de mañana a las 11:59 pm

            this.labelFechaActual.Text = ConfigurationManager.AppSettings.Get("FechaSistema").ToString();

            this.dateTimePicker2.Value = (DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema"))
                .Add(-DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema")).TimeOfDay).AddDays(1));
            this.dateTimePicker1.Value = (DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema"))
                .Add(-DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema")).TimeOfDay)
                .Add(TimeSpan.Parse("23:59")).AddDays(1));

            fechaActual = DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema"));

            this.dateTimePicker2.MinDate = (DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema"))
                .Add(-DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema")).TimeOfDay).AddDays(1));

            this.dateTimePicker1.MinDate = (DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema"))
                .Add(-DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema")).TimeOfDay)
                .Add(TimeSpan.Parse("23:59")).AddDays(1));
        }

        private void comprobarSiEsProfesional()
        {
            if (this.sesion.rol_actual_id == 3 && existeProfesional())
            {
                //es profesional, cancela por dias y no por turno
                this.comboBoxProfesional.Visible = false;
                this.label4.Visible = false;
                this.label5.Visible = false;
                this.label6.Visible = false;
                this.dataGridView1.Enabled = false;
                this.comboBoxTipoEsp.Visible = false;
                this.comboBoxEsp.Visible = false;
            }
            else if (this.sesion.rol_actual_id == 2 && existeAfiliado())
            {
                //es afiliado, cancela por turno
            }
            
            else
            {
                //no es afiliado ni profesional o le faltan tablas con sus atributos, no tiene acciones
                this.groupBox1.Enabled = false;
                this.buttonVolver.Enabled = true;
                throw new Exception("No tiene acciones disponibles en esta ventana");
            }

        }

        private bool existeAfiliado()
        {
            string select = "SELECT * FROM NUL.Afiliado ";
            string where = "WHERE afil_id = " + this.sesion.user_id;

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(select + where);

            return (lector != null);
        }

        private bool existeProfesional()
        {
            string select = "SELECT * FROM NUL.Profesional ";
            string where = "WHERE prof_id = " + this.sesion.user_id;

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(select + where);

            return (lector != null);
        }

        private void llenarTurnosSegunNroAfiliado()
        {
            try
            {


                SqlParameter[] dbParams = new SqlParameter[]
            {
                DAL.Classes.DBHelper.MakeParam("@user_id", SqlDbType.Decimal, 250, obtenerUserId()),
                DAL.Classes.DBHelper.MakeParam("@desde", SqlDbType.DateTime, 250, dateTimePicker2.Value),
                DAL.Classes.DBHelper.MakeParam("@hasta", SqlDbType.DateTime, 250, dateTimePicker1.Value),
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

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dateTimePicker2.Value > dateTimePicker1.Value)
                    throw new Exception("La fecha 'Desde' no puede ser mayor a la fecha 'Hasta'");

                if (this.sesion.rol_actual_id == 3)
                {
                    obtenerTurnosEnRango();
                    cancelarTurnosProfesional();
                }

                else
                {
                    llenarTurnosSegunNroAfiliado();
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void cancelarTurnosProfesional()
        {
            foreach (DataRow row in turnos.Rows)
            {
                cancelarTurno(int.Parse(row["turno_id"].ToString()), 2, this.richTextMotivo.Text);
            }
             MessageBox.Show("Se cancelaron los turnos desde: " + this.dateTimePicker2.Value.ToString() + " hasta: " + this.dateTimePicker1.Value.ToString(),
                    "Aviso", MessageBoxButtons.OK)
            
            salir();
        }

        private void salir()
        {
            Form1 form = new Form1(this.sesion);

            form.Show();

            this.Hide();
        }

        private void obtenerTurnosEnRango()
        {

                SqlParameter[] dbParams = new SqlParameter[]
            {
                DAL.Classes.DBHelper.MakeParam("@prof", SqlDbType.Decimal, 250, obtenerUserId()),
                DAL.Classes.DBHelper.MakeParam("@fecha_desde", SqlDbType.DateTime, 250, dateTimePicker2.Value),
                DAL.Classes.DBHelper.MakeParam("@fecha_hasta", SqlDbType.DateTime, 250, dateTimePicker1.Value),
            };

                turnos = DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_turnos_profesional", dbParams).Tables[0];
          
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

        private void comboBoxEsp_TextUpdate(object sender, EventArgs e)
        {
            cargarProfesionales();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) //si tocan una celda y es el boton, borra el turno de la fila
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name.Equals("Cancelar"))
            {
                int t = int.Parse(this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString());

                cancelarTurno( t, 1, this.richTextMotivo.Text);

                MessageBox.Show("Se cancelo el turno correctamente");

                salir();

            } 
        }

        private void cancelarTurno(int turno_id, int tipo_cancelacion, string detalle)
        {
            try
            {

                SqlParameter result = DAL.Classes.DBHelper.MakeParamOutput("@result", SqlDbType.Decimal, 10);
                SqlParameter[] dbParams = new SqlParameter[]
            {
                DAL.Classes.DBHelper.MakeParam("@id_turno", SqlDbType.Decimal, 250, turno_id),
                DAL.Classes.DBHelper.MakeParam("@tipo_cancel", SqlDbType.Decimal, 250, tipo_cancelacion),
                DAL.Classes.DBHelper.MakeParam("@detalle", SqlDbType.VarChar, 250, detalle),
                DAL.Classes.DBHelper.MakeParam("@fecha", SqlDbType.DateTime, 250, DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema"))),
                result,
            };

                DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_cancelar_turno", dbParams);

                if ((int.Parse(result.Value.ToString())) != 0)
                {
                    throw new Exception("No se pudo cancelar el turno: " + turno_id);
                }
                else
                {
                    //se realizo correctamente, se notificara luego de llamar a cancelarTurnosProfesional() o en cancelarTurno() dependiendo del rol
                }

                llenarTurnosSegunNroAfiliado();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

    }
}
