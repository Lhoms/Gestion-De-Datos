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

namespace ClinicaFrba.Crear_Agenda
{
    public partial class CrearAgenda : Form
    {
        Sesion sesion;

        List<string> tipos_esp;
        Dictionary<string, int> tipos_esp_id;

        List<string> especialidades;
        Dictionary<string, int> especialidades_id;

        List<string> vacia;

        List<Profesional> profesionales;
        List<string> profesionales_na;

        List<string> horasSemana;
        List<string> horasSabado;

        int agenda_id;

        public CrearAgenda(Sesion sesion)
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

            rellenarComboHora();
            
            restringirSegunUltimaAgenda();
            destildar();
        }

        private void destildar()
        {
            this.comboBox1.Enabled = checkLunes.Checked;
            this.comboBox2.Enabled = checkLunes.Checked;
            this.comboBox3.Enabled = checkMartes.Checked;
            this.comboBox4.Enabled = checkMartes.Checked;
            this.comboBox5.Enabled = checkMiercoles.Checked;
            this.comboBox6.Enabled = checkMiercoles.Checked;
            this.comboBox7.Enabled = checkJueves.Checked;
            this.comboBox8.Enabled = checkJueves.Checked;
            this.comboBox9.Enabled = checkViernes.Checked;
            this.comboBox10.Enabled = checkViernes.Checked;
            this.comboBox11.Enabled = checkSabado.Checked;
            this.comboBox12.Enabled = checkSabado.Checked;
        }

        private void rellenarComboHora()
        {
            List<string> horas1 = new List<string>{ "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                                        "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00"};
            List<string> horas2 = new List<string>{ "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00" };


            this.horasSemana = horas1;
            this.horasSabado = horas2;

            comboBox1.DataSource = new List<string>{ "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                                        "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00"};
            comboBox2.DataSource = new List<string>{ "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                                        "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00"};
            comboBox3.DataSource = new List<string>{ "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                                        "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00"};
            comboBox4.DataSource = new List<string>{ "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                                        "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00"};
            comboBox5.DataSource = new List<string>{ "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                                        "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00"};
            comboBox6.DataSource = new List<string>{ "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                                        "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00"};
            comboBox7.DataSource = new List<string>{ "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                                        "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00"};
            comboBox8.DataSource = new List<string>{ "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                                        "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00"};
            comboBox9.DataSource = new List<string>{ "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                                        "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00"};
            comboBox10.DataSource = new List<string>{ "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
                                        "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00"};
            comboBox11.DataSource = new List<string> { "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00" };

            comboBox12.DataSource = new List<string> { "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00" };

        }

        private void restringirSegunUltimaAgenda()
        {
            this.labelFechaActual.Text = DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema")).ToString();

            string select = "SELECT MAX(agenda_disp_hasta) fecha FROM NUL.Agenda WHERE agenda_prof_id = "+ this.sesion.user_id +
                " AND agenda_prof_esp_id = " + this.especialidades_id[this.comboBoxEsp.Text] ;


            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(select);

            if (lector != null)
            {
                this.dateTimeDesde.MinDate = DateTime.Parse(lector["fecha"].ToString()).AddDays(1);
                this.dateTimeHasta.MinDate = DateTime.Parse(lector["fecha"].ToString()).AddDays(1);

                this.dateTimeDesde.Value = DateTime.Parse(lector["fecha"].ToString()).AddDays(1);
                this.dateTimeHasta.Value = DateTime.Parse(lector["fecha"].ToString()).AddDays(1);


            }
            else
            {
                this.dateTimeDesde.MinDate = DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema")).AddDays(1);
                this.dateTimeHasta.MinDate = DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema")).AddDays(1);

                this.dateTimeDesde.Value = DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema")).AddDays(1);
                this.dateTimeHasta.Value = DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema")).AddDays(1);
            }
        }

        private void comprobarSiEsProfesional()
        {
            if (this.sesion.rol_actual_id != 3)
            {
                this.groupBox1.Enabled = false;
                this.groupBox2.Enabled = false;
                this.groupBox3.Enabled = false;
            }
        }

        private void checkLunes_Click(object sender, EventArgs e)
        {
            this.comboBox1.Enabled = checkLunes.Checked;
            this.comboBox2.Enabled = checkLunes.Checked;
        }

        private void checkMartes_Click(object sender, EventArgs e)
        {
            this.comboBox3.Enabled = checkMartes.Checked;
            this.comboBox4.Enabled = checkMartes.Checked;
        }

        private void checkMiercoles_Click(object sender, EventArgs e)
        {
            this.comboBox5.Enabled = checkMiercoles.Checked;
            this.comboBox6.Enabled = checkMiercoles.Checked;   
        }

        private void checkJueves_Click(object sender, EventArgs e)
        {
            this.comboBox7.Enabled = checkJueves.Checked;
            this.comboBox8.Enabled = checkJueves.Checked; 
        }

        private void checkViernes_Click(object sender, EventArgs e)
        {
            this.comboBox9.Enabled = checkViernes.Checked;
            this.comboBox10.Enabled = checkViernes.Checked; 
        }

        private void checkSabado_Click(object sender, EventArgs e)
        {
            this.comboBox11.Enabled = checkSabado.Checked;
            this.comboBox12.Enabled = checkSabado.Checked;         
        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1(this.sesion);

            form.Show();
            this.Close();
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();

        }

        private void limpiar()
        {
            this.checkLunes.Checked = false;
            this.checkMartes.Checked = false;
            this.checkMiercoles.Checked = false;
            this.checkJueves.Checked = false;
            this.checkViernes.Checked = false;
            this.checkSabado.Checked = false;

            restringirSegunUltimaAgenda();
            destildar();
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

                string expresion = "SELECT DISTINCT tipo_esp_id, tipo_esp_descrip FROM NUL.Tipo_esp TE JOIN NUL.Especialidad E ON TE.tipo_esp_id = E.esp_tipo JOIN NUL.Profesional_especialidad P ON E.esp_id = P.esp_id";
                string where = " WHERE P.prof_id = " + this.sesion.user_id;

                SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion + where);

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
                string select = "SELECT DISTINCT E.esp_id, E.esp_descrip FROM NUL.Especialidad E JOIN NUL.Profesional_especialidad PE ON E.esp_id = PE.esp_id ";
                string where = "WHERE esp_tipo = " + this.tipos_esp_id[this.comboBoxTipoEsp.Text]
                                +" AND PE.prof_id = " + this.sesion.user_id;



                SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(select + where);

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
                exc.ToString();
                //MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }

        }


        private void comboBoxTipoEsp_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                llenarEspecialidades();
                limpiar();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }

        }

        private void comboBoxEsp_SelectionChangeCommitted(object sender, EventArgs e)
        {
            limpiar();
        }

        private void buttonCrear_Click(object sender, EventArgs e)
        {
            try
            {
                comprobarHastaValido();
                comprobarQueTrabajeMenosDe48Hs();

                crearAgenda();

                llenarAgendaLunes();
                llenarAgendaMartes();
                llenarAgendaMiercoles();
                llenarAgendaJueves();
                llenarAgendaViernes();
                llenarAgendaSabado();

                restringirSegunUltimaAgenda();
                limpiar();

                MessageBox.Show("Agenda creada correctamente");
                

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void crearAgenda()
        {   
            try
            {

                SqlParameter id_new = DAL.Classes.DBHelper.MakeParamOutput("@id_new", SqlDbType.Decimal, 100);
                SqlParameter[] dbParams = new SqlParameter[]
            {
                DAL.Classes.DBHelper.MakeParam("@prof_id", SqlDbType.Decimal, 250, this.sesion.user_id),
                DAL.Classes.DBHelper.MakeParam("@esp_id", SqlDbType.Decimal, 250, this.especialidades_id[this.comboBoxEsp.Text]),
                DAL.Classes.DBHelper.MakeParam("@desde", SqlDbType.DateTime, 250, this.dateTimeDesde.Value),
                DAL.Classes.DBHelper.MakeParam("@hasta", SqlDbType.DateTime, 250, this.dateTimeHasta.Value),
                id_new,
            };

                DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_new_agenda_profesional", dbParams);

                if (int.Parse(id_new.Value.ToString()) == 0)
                    throw new Exception("Fallo creando la agenda");
                else
                    this.agenda_id = int.Parse(id_new.Value.ToString());

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void llenarAgendaLunes()
        {
            try
            {
                if (checkLunes.Checked)
                {
                    SqlParameter result = DAL.Classes.DBHelper.MakeParamOutput("@result", SqlDbType.Decimal, 100);

                    SqlParameter[] dbParams = new SqlParameter[]
                    {
                        DAL.Classes.DBHelper.MakeParam("@dia_id", SqlDbType.Decimal, 250, 1),
                        DAL.Classes.DBHelper.MakeParam("@agenda_id", SqlDbType.Decimal, 250, this.agenda_id),
                        DAL.Classes.DBHelper.MakeParam("@hora_desde", SqlDbType.Time, 250, TimeSpan.Parse(this.comboBox1.Text)),
                        DAL.Classes.DBHelper.MakeParam("@hora_hasta", SqlDbType.Time, 250, TimeSpan.Parse(this.comboBox2.Text)),
                        result,
                     };

                    DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_new_dia_agenda_profesional", dbParams);

                    if (int.Parse(result.Value.ToString()) != 0)
                        throw new Exception("Fallo llenando la agenda");
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void llenarAgendaMartes()
        {
            try
            {
                if (checkMartes.Checked)
                {
                    SqlParameter result = DAL.Classes.DBHelper.MakeParamOutput("@result", SqlDbType.Decimal, 100);

                    SqlParameter[] dbParams = new SqlParameter[]
                    {
                        DAL.Classes.DBHelper.MakeParam("@dia_id", SqlDbType.Decimal, 250, 2),
                        DAL.Classes.DBHelper.MakeParam("@agenda_id", SqlDbType.Decimal, 250, this.agenda_id),
                        DAL.Classes.DBHelper.MakeParam("@hora_desde", SqlDbType.Time, 250, TimeSpan.Parse(this.comboBox3.Text)),
                        DAL.Classes.DBHelper.MakeParam("@hora_hasta", SqlDbType.Time, 250, TimeSpan.Parse(this.comboBox4.Text)),
                        result,
                     };

                    DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_new_dia_agenda_profesional", dbParams);

                    if (int.Parse(result.Value.ToString()) != 0)
                        throw new Exception("Fallo llenando la agenda");
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void llenarAgendaMiercoles()
        {
            try
            {
                if (checkMiercoles.Checked)
                {
                    SqlParameter result = DAL.Classes.DBHelper.MakeParamOutput("@result", SqlDbType.Decimal, 100);

                    SqlParameter[] dbParams = new SqlParameter[]
                    {
                        DAL.Classes.DBHelper.MakeParam("@dia_id", SqlDbType.Decimal, 250, 3),
                        DAL.Classes.DBHelper.MakeParam("@agenda_id", SqlDbType.Decimal, 250, this.agenda_id),
                        DAL.Classes.DBHelper.MakeParam("@hora_desde", SqlDbType.Time, 250, TimeSpan.Parse(this.comboBox5.Text)),
                        DAL.Classes.DBHelper.MakeParam("@hora_hasta", SqlDbType.Time, 250, TimeSpan.Parse(this.comboBox6.Text)),
                        result,
                     };

                    DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_new_dia_agenda_profesional", dbParams);

                    if (int.Parse(result.Value.ToString()) != 0)
                        throw new Exception("Fallo llenando la agenda");
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void llenarAgendaJueves()
        {
            try
            {
                if (checkJueves.Checked)
                {
                    SqlParameter result = DAL.Classes.DBHelper.MakeParamOutput("@result", SqlDbType.Decimal, 100);

                    SqlParameter[] dbParams = new SqlParameter[]
                    {
                        DAL.Classes.DBHelper.MakeParam("@dia_id", SqlDbType.Decimal, 250, 4),
                        DAL.Classes.DBHelper.MakeParam("@agenda_id", SqlDbType.Decimal, 250, this.agenda_id),
                        DAL.Classes.DBHelper.MakeParam("@hora_desde", SqlDbType.Time, 250, TimeSpan.Parse(this.comboBox7.Text)),
                        DAL.Classes.DBHelper.MakeParam("@hora_hasta", SqlDbType.Time, 250, TimeSpan.Parse(this.comboBox8.Text)),
                        result,
                     };

                    DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_new_dia_agenda_profesional", dbParams);

                    if (int.Parse(result.Value.ToString()) != 0)
                        throw new Exception("Fallo llenando la agenda");
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void llenarAgendaViernes()
        {
            try
            {
                if (checkViernes.Checked)
                {
                    SqlParameter result = DAL.Classes.DBHelper.MakeParamOutput("@result", SqlDbType.Decimal, 100);

                    SqlParameter[] dbParams = new SqlParameter[]
                    {
                        DAL.Classes.DBHelper.MakeParam("@dia_id", SqlDbType.Decimal, 250, 5),
                        DAL.Classes.DBHelper.MakeParam("@agenda_id", SqlDbType.Decimal, 250, this.agenda_id),
                        DAL.Classes.DBHelper.MakeParam("@hora_desde", SqlDbType.Time, 250, TimeSpan.Parse(this.comboBox9.Text)),
                        DAL.Classes.DBHelper.MakeParam("@hora_hasta", SqlDbType.Time, 250, TimeSpan.Parse(this.comboBox10.Text)),
                        result,
                     };

                    DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_new_dia_agenda_profesional", dbParams);

                    if (int.Parse(result.Value.ToString()) != 0)
                        throw new Exception("Fallo llenando la agenda");
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void llenarAgendaSabado()
        {
            try
            {
                if (checkSabado.Checked)
                {
                    SqlParameter result = DAL.Classes.DBHelper.MakeParamOutput("@result", SqlDbType.Decimal, 100);

                    SqlParameter[] dbParams = new SqlParameter[]
                    {
                        DAL.Classes.DBHelper.MakeParam("@dia_id", SqlDbType.Decimal, 250, 6),
                        DAL.Classes.DBHelper.MakeParam("@agenda_id", SqlDbType.Decimal, 250, this.agenda_id),
                        DAL.Classes.DBHelper.MakeParam("@hora_desde", SqlDbType.Time, 250, TimeSpan.Parse(this.comboBox11.Text)),
                        DAL.Classes.DBHelper.MakeParam("@hora_hasta", SqlDbType.Time, 250, TimeSpan.Parse(this.comboBox12.Text)),
                        result,
                     };

                    DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_new_dia_agenda_profesional", dbParams);

                    if (int.Parse(result.Value.ToString()) != 0)
                        throw new Exception("Fallo llenando la agenda");
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void comprobarHastaValido()
        {
            if (checkLunes.Checked)
                if (!(this.comboBox1.SelectedIndex < this.comboBox2.SelectedIndex))
                    throw new Exception("No es valido el horario Lunes desde: "+ this.comboBox1.Text +" hasta: " + this.comboBox2.Text);

            if (checkMartes.Checked)
                if (!(this.comboBox3.SelectedIndex < this.comboBox4.SelectedIndex))
                    throw new Exception("No es valido el horario Martes desde: " + this.comboBox3.Text + " hasta: " + this.comboBox4.Text);

            if (checkMiercoles.Checked)
                if (!(this.comboBox5.SelectedIndex < this.comboBox6.SelectedIndex))
                    throw new Exception("No es valido el horario Miercoles desde: " + this.comboBox5.Text + " hasta: " + this.comboBox6.Text);

            if (checkJueves.Checked)
                if (!(this.comboBox7.SelectedIndex < this.comboBox8.SelectedIndex))
                    throw new Exception("No es valido el horario Jueves desde: " + this.comboBox7.Text + " hasta: " + this.comboBox8.Text);

            if (checkViernes.Checked)
                if (!(this.comboBox9.SelectedIndex < this.comboBox10.SelectedIndex))
                    throw new Exception("No es valido el horario Viernes desde: " + this.comboBox9.Text + " hasta: " + this.comboBox10.Text);

            if (checkSabado.Checked)
                if (!(this.comboBox11.SelectedIndex < this.comboBox12.SelectedIndex))
                    throw new Exception("No es valido el horario Sabado desde: " + this.comboBox11.Text + " hasta: " + this.comboBox12.Text);
        }

        private void comprobarQueTrabajeMenosDe48Hs()
        {
            Decimal sumaHoras = 0;

            if (checkLunes.Checked)
            {
                sumaHoras = sumaHoras + (Decimal.Parse(this.comboBox2.Text.Split(':')[0]) - Decimal.Parse(this.comboBox1.Text.Split(':')[0]));
                sumaHoras = sumaHoras + (Decimal.Parse(this.comboBox2.Text.Split(':')[1]) / 60 - Decimal.Parse(this.comboBox1.Text.Split(':')[1]) / 60);
            }

            if (checkMartes.Checked)
            {
                sumaHoras = sumaHoras + (Decimal.Parse(this.comboBox4.Text.Split(':')[0]) - Decimal.Parse(this.comboBox3.Text.Split(':')[0]));
                sumaHoras = sumaHoras + (Decimal.Parse(this.comboBox4.Text.Split(':')[1]) / 60 - Decimal.Parse(this.comboBox3.Text.Split(':')[1]) / 60);
            }

            if (checkMiercoles.Checked)
            {
                sumaHoras = sumaHoras + (Decimal.Parse(this.comboBox6.Text.Split(':')[0]) - Decimal.Parse(this.comboBox5.Text.Split(':')[0]));
                sumaHoras = sumaHoras + (Decimal.Parse(this.comboBox6.Text.Split(':')[1]) / 60 - Decimal.Parse(this.comboBox5.Text.Split(':')[1]) / 60);
            }

            if (checkJueves.Checked)
            {
                sumaHoras = sumaHoras + (Decimal.Parse(this.comboBox8.Text.Split(':')[0]) - Decimal.Parse(this.comboBox7.Text.Split(':')[0]));
                sumaHoras = sumaHoras + (Decimal.Parse(this.comboBox8.Text.Split(':')[1]) / 60 - Decimal.Parse(this.comboBox7.Text.Split(':')[1]) / 60);
            }

            if (checkViernes.Checked)
            {
                sumaHoras = sumaHoras + (Decimal.Parse(this.comboBox10.Text.Split(':')[0]) - Decimal.Parse(this.comboBox9.Text.Split(':')[0]));
                sumaHoras = sumaHoras + (Decimal.Parse(this.comboBox10.Text.Split(':')[1]) / 60 - Decimal.Parse(this.comboBox9.Text.Split(':')[1]) / 60);
            }

            if (checkSabado.Checked)
            {
                sumaHoras = sumaHoras + (Decimal.Parse(this.comboBox12.Text.Split(':')[0]) - Decimal.Parse(this.comboBox11.Text.Split(':')[0]));
                sumaHoras = sumaHoras + (Decimal.Parse(this.comboBox12.Text.Split(':')[1]) / 60 - Decimal.Parse(this.comboBox11.Text.Split(':')[1]) / 60);
            }

            if(sumaHoras > 48)
            {
                this.limpiar();
                throw new Exception("No se pueden superar las 48 horas de trabajo por semana");                
            }
        }



    }
}
