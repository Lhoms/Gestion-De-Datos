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

        int afil_id;

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
            try
            {
                if (validarBono())
                    llenarTurnosSegunNroAfiliado();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private bool validarBono()
        {

            if (string.IsNullOrEmpty(this.textBoxBono.Text) || string.IsNullOrEmpty(this.textBoxNroAfiliado.Text))
                throw new Exception("El numero de afiliado y el numero de bono no pueden estar vacios");

                string nroAfiliado = this.textBoxNroAfiliado.Text;
                string grupoFamiliar = (nroAfiliado).Substring(0, nroAfiliado.Length - 3) + "___";


                SqlParameter result = DAL.Classes.DBHelper.MakeParamOutput("@result", SqlDbType.Decimal, 10);
                SqlParameter[] dbParams = new SqlParameter[]
            {
                DAL.Classes.DBHelper.MakeParam("@bono_id", SqlDbType.Decimal, 250, Decimal.Parse(this.textBoxBono.Text)),
                DAL.Classes.DBHelper.MakeParam("@nroAfiliado", SqlDbType.VarChar, 250, grupoFamiliar),
                result,
            };

                DataTable bono = DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_validar_bono", dbParams).Tables[0];

                if (bono.Rows.Count == 0)
                {
                    throw new Exception("Bono no valido");
                }
                else
                    return true;

        }

        private void llenarTurnosSegunNroAfiliado()
        {
                DateTime hoy = DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema"));
                DateTime fin_hoy = DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema")).
                                    AddDays(1).AddHours(-hoy.Hour).AddMinutes(-hoy.Minute).AddMilliseconds(-hoy.Millisecond);

                obtenerUserId();

                SqlParameter[] dbParams = new SqlParameter[]
            {
                DAL.Classes.DBHelper.MakeParam("@user_id", SqlDbType.Decimal, 250, this.afil_id),
                DAL.Classes.DBHelper.MakeParam("@desde", SqlDbType.DateTime, 250, hoy),
                DAL.Classes.DBHelper.MakeParam("@hasta", SqlDbType.DateTime, 250, fin_hoy),
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

        private object obtenerProfesionalId()
        {
            return this.profesionales[this.comboBoxProfesional.SelectedIndex].id;
        }

        public void obtenerUserId()
        {
            try
            {
                this.afil_id = 0;

                string expresion = "SELECT afil_id FROM NUL.Afiliado A JOIN NUL.Usuario U ON A.afil_id = U.user_id ";
                string where     = "WHERE U.user_habilitado = 1 AND afil_nro_afiliado = " + this.textBoxNroAfiliado.Text;


                SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion + where);

                if (lector == null)
                {
                    throw new Exception("fallo obteniendo al afiliado segun numero");
                }

                this.afil_id = int.Parse(lector["afil_id"].ToString());

            }

            catch (Exception exc)
            {
                exc.ToString();
                MessageBox.Show("Numero de afiliado no valido", "Aviso", MessageBoxButtons.OK);
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
            try
            {
                if (this.dataGridView1.Columns[e.ColumnIndex].Name.Equals("Registrar"))
                {
                    obtenerUserId();

                    int turno_id = int.Parse(this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString());
                    DateTime hoy = DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema"));

                    SqlParameter result = DAL.Classes.DBHelper.MakeParamOutput("@result", SqlDbType.Decimal, 10);
                    SqlParameter[] dbParams = new SqlParameter[]
                {
                    DAL.Classes.DBHelper.MakeParam("@user_id", SqlDbType.Decimal, 250, this.afil_id),
                    DAL.Classes.DBHelper.MakeParam("@id_turno", SqlDbType.Decimal, 250, turno_id),
                    DAL.Classes.DBHelper.MakeParam("@fecha", SqlDbType.DateTime, 250, hoy),
                    DAL.Classes.DBHelper.MakeParam("@hora_llegada", SqlDbType.Time, 250, hoy.TimeOfDay),
                    DAL.Classes.DBHelper.MakeParam("@bono_id", SqlDbType.VarChar, 250, Decimal.Parse(this.textBoxBono.Text)),
                    result,
                };

                    DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_set_llegada", dbParams);

                    if (int.Parse(result.Value.ToString()) == 0)
                    {
                        MessageBox.Show("Se registro correctamente la llegada", "Aviso", MessageBoxButtons.OK);
                        
                        Form1 form = new Form1(this.sesion);
                        form.Show();
                        this.Hide();
                    }

                    else
                        throw new Exception("Fallo registrando la llegada");


                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void buttonBonosDisp_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.textBoxNroAfiliado.Text))
                    throw new Exception("el campo numero de afiliado no puede estar vacio");

                string nroAfiliado = this.textBoxNroAfiliado.Text;
                string grupoFamiliar = (nroAfiliado).Substring(0, nroAfiliado.Length - 3) + "___";

                string select = "SELECT * FROM NUL.Bono B JOIN NUL.Bono_compra BC ON B.bono_compra = BC.bonoc_id JOIN NUL.Afiliado A ON BC.bonoc_id_usuario = A.afil_id ";
                string where = "WHERE B.bono_usado = 0 AND A.afil_nro_afiliado LIKE '" + grupoFamiliar + "'";

                SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(select + where);


                string mensaje = "No tiene bonos disponibles";

                if (lector != null)
                {
                    int i = 0;

                    mensaje = "Sus bonos disponibles son: \n";

                    while (lector.Read() && i < 30)
                    {
                        mensaje = mensaje + lector["bono_id"] + "\n";
                        i++; //esto es para que no haya una ventana con mas de 30 lineas
                    }



                }

                MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }
    }
}
