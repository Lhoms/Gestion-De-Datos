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

namespace ClinicaFrba.Pedir_Turno
{
    public partial class PedirTurno : Form
    {
        Sesion sesion;
        int afiliado_id;

        List<string> tipos_esp;
        Dictionary<string, int> tipos_esp_id;

        List<string> especialidades;
        Dictionary<string, int> especialidades_id;

        List<string> vacia;

        List<Profesional> profesionales;
        List<string> profesionales_na;

        public PedirTurno(Sesion sesion)
        {
            InitializeComponent();

            this.sesion = sesion;

            esAfiliado();

            tipos_esp = new List<string>();
            tipos_esp_id = new Dictionary<string, int>();

            especialidades = new List<string>();
            especialidades_id = new Dictionary<string, int>();

            vacia = new List<string>();

            profesionales = new List<Profesional>();
            profesionales_na = new List<string>();

            llenarTipoEsp();
            llenarEspecialidades();
            cargarProfesionales();

            rellenarComboBoxes();

            setearFechas();
 

        }

        private void setearFechas()
        {
            TimeSpan hora = DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema")).TimeOfDay; //asi el calendario arranca en 0:00

            this.dateTimePicker1.MinDate = DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema")).Add(-hora);
            this.dateTimePicker2.MinDate = DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema")).Add(-hora);

            this.dateTimePicker1.Value = DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema")).Add(-hora);
            this.dateTimePicker2.Value = DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema")).Add(-hora);

            this.labelFechaActual.Text = DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema")).ToString();
        }


        private void esAfiliado()
        {
            if (this.sesion.rol_actual_id == 2) //si es afiliado no necesita poner su nro afiliado
            {
                this.textBoxNroAfiliado.Visible = false;
                this.label1.Visible = false;
            }
            else
                if (this.sesion.rol_actual_id == 1) //si es administrativo puede pedirle a un afiliado
                {
                    this.textBoxNroAfiliado.Visible = true;
                    this.label1.Visible = true;
                }
            else
                if (this.sesion.rol_actual_id == 3) //si es medico no hace nada
                {
                    this.groupBox1.Enabled = false;
                }
        }

        
        private int obtenerProfesionalId()
        {
            return this.profesionales[this.comboBoxProfesional.SelectedIndex].id;
        }

        private void rellenarComboBoxes()
        {
            this.comboBox1.DataSource = new List<string>{ "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                                        "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00"};
            this.comboBox2.DataSource = new List<string>{ "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                                        "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00"};
        }

        public void obtenerUserId()
        {
                if (this.sesion.rol_actual_id == 2)
                {
                    this.afiliado_id = this.sesion.user_id;
                }
                else
                {
                    this.afiliado_id = 0;

                    if (string.IsNullOrWhiteSpace(this.textBoxNroAfiliado.Text))
                        throw new Exception("El numero de afiliado no puede estar vacio");

                    string expresion = "SELECT afil_id FROM NUL.Afiliado A JOIN NUL.Usuario U ON A.afil_id = U.user_id ";
                    string where = "WHERE U.user_habilitado = 1 AND afil_nro_afiliado = " + this.textBoxNroAfiliado.Text;


                    SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion + where);

                    if (lector == null)
                    {
                        throw new Exception("No se encontro al afiliado");
                    }
                    else
                    {
                        this.afiliado_id = int.Parse(lector["afil_id"].ToString());
                    }
                }

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
            //try
            //{
                if (this.dataGridViewT.Columns[e.ColumnIndex].Name.Equals("Pedir"))
                {

                    DateTime fechaTurno = DateTime.Parse(this.dataGridViewT.Rows[e.RowIndex].Cells[e.ColumnIndex+1].Value.ToString());
                    obtenerUserId();

                    SqlParameter result = DAL.Classes.DBHelper.MakeParamOutput("@result", SqlDbType.Decimal, 10);
                    SqlParameter[] dbParams = new SqlParameter[]
                    {
                        DAL.Classes.DBHelper.MakeParam("@afil_id", SqlDbType.Decimal, 250, this.afiliado_id),
                        DAL.Classes.DBHelper.MakeParam("@prof_id", SqlDbType.Decimal, 250, obtenerProfesionalId()),
                        DAL.Classes.DBHelper.MakeParam("@esp_id", SqlDbType.Decimal, 250, this.especialidades_id[this.comboBoxEsp.Text]),
                        DAL.Classes.DBHelper.MakeParam("@fecha", SqlDbType.DateTime, 250, fechaTurno),
                        result,
                    };

                    DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_set_pedir_turno", dbParams);

                    if (int.Parse(result.Value.ToString()) == 0)
                    {
                        MessageBox.Show("Se registro correctamente el turno", "Aviso", MessageBoxButtons.OK);

                        Form1 form = new Form1(this.sesion);
                        form.Show();
                        this.Hide();
                    }

                    else
                       throw new Exception("Fallo registrando el turno");


                }

            //}
            //catch (Exception exc)
            //{
            //    MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            //}
        }

        private void buttonVolver_Click_1(object sender, EventArgs e)
        {
            Form1 form = new Form1(this.sesion);
            form.Show();
            this.Hide();
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {   //sp_get_disp_profesional(@id_prof numeric(18,0), @esp_id numeric(18,0), @fec_inicio datetime, @fec_fin datetime)
            try
            {
                
                obtenerUserId();
                obtenerProfesionalId();
                validarHoras();

                DateTime desde = this.dateTimePicker1.Value.Add(TimeSpan.Parse(this.comboBox1.Text));
                DateTime hasta = this.dateTimePicker2.Value.Add(TimeSpan.Parse(this.comboBox2.Text));

                SqlParameter[] dbParams = new SqlParameter[]
            {
                DAL.Classes.DBHelper.MakeParam("@id_prof", SqlDbType.Decimal, 250, obtenerProfesionalId()),
                DAL.Classes.DBHelper.MakeParam("@esp_id", SqlDbType.Decimal, 250, this.especialidades_id[this.comboBoxEsp.Text]),
                DAL.Classes.DBHelper.MakeParam("@fec_inicio", SqlDbType.DateTime, 250, desde),
                DAL.Classes.DBHelper.MakeParam("@fec_fin", SqlDbType.DateTime, 250, hasta),
            };

                DataTable dt = DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_get_disp_profesional", dbParams).Tables[0];

                if (dt.Rows.Count == 0)
                    throw new Exception("No hay turnos disponibles para esas fechas para ese profesional.");

                this.dataGridViewT.DataSource = dt;



            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void validarHoras()
        {
            if (this.dateTimePicker1.Value == this.dateTimePicker2.Value)
                if (!(this.comboBox1.SelectedIndex < this.comboBox2.SelectedIndex))
                    throw new Exception("Horario invalido");

            if(this.dateTimePicker1.Value > this.dateTimePicker2.Value)
                throw new Exception("Rango de fechas invalido");

            if (DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema")).Date == this.dateTimePicker1.Value.Date)
                if (TimeSpan.Parse(this.comboBox1.Text) < DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema")).TimeOfDay)
                    throw new Exception("Hora no valida, no se puede ingresar un tiempo anterior");

        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            rellenarComboBoxes();
            setearFechas();
            this.textBoxNroAfiliado.Text = "";
        }
    }
}
