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

namespace ClinicaFrba
{
    public partial class Form1 : Form
    {
        Sesion sesion;

        Dictionary<string, int> rolesId;

        Dictionary<string, int> rol_ids;
        List<string> rol_descrip;


        public Form1(Sesion sesion)
        {
            try
            {
                InitializeComponent();

                this.sesion = sesion;

                siEsProfesionalComprobarSuMatricula();

                rol_ids = new Dictionary<string, int>();
                rol_descrip = new List<string>();

                rolesDelUsuario();

                comboBoxRol.DataSource = this.rol_descrip;

                rolesId = new Dictionary<string, int>();

                this.labelFechaActual.Text = ConfigurationManager.AppSettings.Get("FechaSistema");
                ID_Usuario.Text = sesion.tipo_doc_id + " - " + sesion.username;

                ObtenerFuncionalidadesPorRolPorUsuario();

                this.label3.Text = this.sesion.rol_actual_id.ToString();/***********/
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void siEsProfesionalComprobarSuMatricula()
        {
            string expresion = "SELECT * FROM NUL.Profesional WHERE prof_id = " + this.sesion.user_id;

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            if (lector != null)
            {
                if (lector != null)
                {
                    Abm_Afiliado.MatriculaFaltante form = new Abm_Afiliado.MatriculaFaltante(this.sesion);
                    if( int.Parse(lector["prof_matric"].ToString()) == 0)
                    form.Show();
                }
            }
            else
            {
                //no es profesional
            }

        }

        private void ObtenerFuncionalidadesPorRolPorUsuario()
        {
            DataSet ds;
            DataRow[] dr;
            string expresion;

            SqlParameter[] dbParams = new SqlParameter[]
                    {
                         DAL.Classes.DBHelper.MakeParam("@id", SqlDbType.Int, 0, get_rol_id()),
                    };

            ds = DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_get_funciones_por_rol", dbParams);

            //ABM Rol
            //
            expresion = "func_descrip = 'ABM Rol'";
            dr = ds.Tables[0].Select(expresion);

            this.buttonAltaRol.Enabled = dr.Count() > 0;
            this.buttonBajaRol.Enabled = dr.Count() > 0;
            this.buttonModificarRol.Enabled = dr.Count() > 0;

            //ABM Afiliado
            //
            expresion = "func_descrip = 'Abm Afiliado'";
            dr = ds.Tables[0].Select(expresion);

            this.buttonAltaAfiliado.Enabled = dr.Count() > 0;
            this.buttonBajaAfiliado.Enabled = dr.Count() > 0;
            this.buttonModificarAfiliado.Enabled = dr.Count() > 0;


            //Compra de Bonos
            //
            expresion = "func_descrip = 'Compra de bonos'";
            dr = ds.Tables[0].Select(expresion);

            this.buttonCompraBono.Enabled = dr.Count() > 0;


            //Pedir Turno
            //
            expresion = "func_descrip = 'Pedir turno'";
            dr = ds.Tables[0].Select(expresion);

            this.buttonPedirTurno.Enabled = dr.Count() > 0;


            //Registrar llegada atencion medica
            //
            expresion = "func_descrip = 'Registro de llegada para atención médica'";
            dr = ds.Tables[0].Select(expresion);

            this.buttonRegistrarLlegada.Enabled = dr.Count() > 0;


            //Registrar resultado atencion medica
            //
            expresion = "func_descrip = 'Registrar resultado para atención médica'";
            dr = ds.Tables[0].Select(expresion);

            this.buttonRegistrarResultado.Enabled = dr.Count() > 0;


            //Cancelar atencion medica
            //
            expresion = "func_descrip = 'Cancelar atención médica'";
            dr = ds.Tables[0].Select(expresion);

            this.buttonCancelarTurno.Enabled = dr.Count() > 0;


            //Listado estadistico
            //
            expresion = "func_descrip = 'Listado estadístico'";
            dr = ds.Tables[0].Select(expresion);

            this.buttonListadoEstadistico.Enabled = dr.Count() > 0;


            //Listado estadistico
            //
            expresion = "func_descrip = 'Crear agenda'";
            dr = ds.Tables[0].Select(expresion);

            this.buttonCrearAgenda.Enabled = dr.Count() > 0;


        }

        private int get_rol_id()
        {
            return this.rol_ids[this.comboBoxRol.Text];
        }

        private void rolesDelUsuario()
        {
            string expresion = "SELECT R.rol_id, R.rol_descrip FROM NUL.Rol R JOIN NUL.User_rol UR ON R.rol_id = UR.rol_id WHERE rol_habilitado = 1 AND UR.user_id = "
                                + this.sesion.user_id;

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            if (lector != null)
            {
                rol_ids.Add((string)lector["rol_descrip"].ToString(), int.Parse(lector["rol_id"].ToString()));
                rol_descrip.Add((string)lector["rol_descrip"].ToString());

                while (lector.Read())
                {
                    rol_ids.Add((string)lector["rol_descrip"].ToString(), int.Parse(lector["rol_id"].ToString()));
                    rol_descrip.Add((string)lector["rol_descrip"].ToString());
                }
            }

           else
           {
               throw new Exception("No hay roles disponibles");
           }

        }
        

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void buttonCerrarSesion_Click(object sender, EventArgs e)
        {
            Login.Login login = new Login.Login();
            login.Show();

            this.Close();


        }

        private void comboBoxRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObtenerFuncionalidadesPorRolPorUsuario();
        }


        private void buttonAltaRol_Click(object sender, EventArgs e)
        {
            this.sesion.rol_actual_id = get_rol_id();

            AbmRol.AltaRol form = new AbmRol.AltaRol(this.sesion);

            form.Show();

            this.Hide();
        }

        private void buttonBajaRol_Click(object sender, EventArgs e)
        {
            this.sesion.rol_actual_id = get_rol_id();

            AbmRol.ModificarRol form = new AbmRol.ModificarRol(this.sesion);

            form.Show();

            this.Hide();
        }

        private void buttonModificarRol_Click(object sender, EventArgs e)
        {
            this.sesion.rol_actual_id = get_rol_id();

            AbmRol.ModificarRol form = new AbmRol.ModificarRol(this.sesion);

            form.Show();

            this.Hide();
        }

        private void buttonAltaAfiliado_Click(object sender, EventArgs e)
        {
            this.sesion.rol_actual_id = get_rol_id();

            Abm_Afiliado.AltaAfiliado form = new Abm_Afiliado.AltaAfiliado(this.sesion);

            form.Show();

            this.Hide();
        }

        private void buttonBajaAfiliado_Click(object sender, EventArgs e)
        {
            this.sesion.rol_actual_id = get_rol_id();

            Abm_Afiliado.BusquedaAfiliado form = new Abm_Afiliado.BusquedaAfiliado(this.sesion);

            form.Show();

            this.Hide();
        }

        private void buttonModificarAfiliado_Click(object sender, EventArgs e)
        {
            this.sesion.rol_actual_id = get_rol_id();

            Abm_Afiliado.BusquedaAfiliado form = new Abm_Afiliado.BusquedaAfiliado(this.sesion);

            form.Show();

            this.Hide();
        }

        private void buttonCompraBono_Click(object sender, EventArgs e)
        {
            this.sesion.rol_actual_id = get_rol_id();

            Compra_Bono.CompraBono form = new Compra_Bono.CompraBono(sesion);

            form.Show();

            this.Hide();
        }

        private void buttonPedirTurno_Click(object sender, EventArgs e)
        {
            this.sesion.rol_actual_id = get_rol_id();

            Pedir_Turno.PedirTurno form = new Pedir_Turno.PedirTurno(this.sesion);

            form.Show();

            this.Hide();
        }

        private void buttonCancelarTurno_Click(object sender, EventArgs e)
        {
            this.sesion.rol_actual_id = get_rol_id();

            Cancelar_Atencion.CancelarAtencion form = new Cancelar_Atencion.CancelarAtencion(this.sesion);

            form.Show();

            this.Hide();
        }

        private void buttonRegistrarLlegada_Click(object sender, EventArgs e)
        {
            this.sesion.rol_actual_id = get_rol_id();

            Registro_Llegada.RegistrarLlegada form = new Registro_Llegada.RegistrarLlegada(this.sesion);

            form.Show();

            this.Hide();
        }

        private void buttonRegistrarResultado_Click(object sender, EventArgs e)
        {
            this.sesion.rol_actual_id = get_rol_id();

            Registro_Resultado.RegistrarResultado form = new Registro_Resultado.RegistrarResultado(this.sesion);

            form.Show();

            this.Hide();
        }

        private void buttonListadoEstadistico_Click(object sender, EventArgs e)
        {
            this.sesion.rol_actual_id = get_rol_id();

            Listados.Listados form = new Listados.Listados(this.sesion);

            form.Show();

            this.Hide();
        }

        private void buttonCrearAgenda_Click(object sender, EventArgs e)
        {
            this.sesion.rol_actual_id = get_rol_id();

            Crear_Agenda.CrearAgenda form = new Crear_Agenda.CrearAgenda(this.sesion);

            form.Show();

            this.Hide();
        }


    }
}
