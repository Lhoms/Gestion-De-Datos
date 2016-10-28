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
        string username;
        string tipo_doc_usuario;
        int user_id;
        DataTable dtRoles;
        Dictionary<string, int> rolesId;


        public Form1(String tipo_doc_usuario, String username, int user_id)
        {

            InitializeComponent();

            this.tipo_doc_usuario = tipo_doc_usuario;   //Me guardo el usuario actual del sistema
            this.username = username;
            this.user_id = user_id;

            comboBoxRol.ValueMember = "rol_descrip";
            this.dtRoles = rolesDelUsuario();
            comboBoxRol.DataSource = this.dtRoles;

            rolesId = new Dictionary<string,int>();

            this.labelFechaActual.Text = ConfigurationManager.AppSettings.Get("FechaSistema");
            ID_Usuario.Text = tipo_doc_usuario + " - " + username;

            ObtenerFuncionalidadesPorRolPorUsuario();
        }

        private void ObtenerFuncionalidadesPorRolPorUsuario()
        {
            DataSet ds;
            DataRow[] dr;
            string expresion;

            SqlParameter[] dbParams = new SqlParameter[]
                    {
                         DAL.Classes.DBHelper.MakeParam("@id", SqlDbType.Int, 0, get_rol_id(this.comboBoxRol.Text)),
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

        private int get_rol_id(string rol)
        {
            string expresion = "rol_descrip = '" + rol + "'";
            int rol_id = 0;

            if (this.dtRoles.Rows.Count == 0)
            {
                MessageBox.Show("No posee roles disponibles\nContacte un administrador para asignarle uno.", "aviso", MessageBoxButtons.OK);
            }
            else
            {
                rol_id = int.Parse(this.dtRoles.Rows[0][0].ToString());
            }

            return rol_id;
        }

        private DataTable rolesDelUsuario()
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {
                         DAL.Classes.DBHelper.MakeParam("@id", SqlDbType.Int, 0, this.user_id),
                    };

            DataTable d = DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_get_roles_disponibles_por_usuario", dbParams).Tables[0];

            return d;
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
            AbmRol.AltaRol form = new AbmRol.AltaRol(this.tipo_doc_usuario, this.username, this.user_id);

            form.Show();

            this.Hide();
        }

        private void buttonBajaRol_Click(object sender, EventArgs e)
        {
            AbmRol.ModificarRol form = new AbmRol.ModificarRol(this.tipo_doc_usuario, this.username, this.user_id);

            form.Show();

            this.Hide();
        }

        private void buttonModificarRol_Click(object sender, EventArgs e)
        {
            AbmRol.ModificarRol form = new AbmRol.ModificarRol(this.tipo_doc_usuario, this.username, this.user_id);

            form.Show();

            this.Hide();
        }

        private void buttonAltaAfiliado_Click(object sender, EventArgs e)
        {
            Abm_Afiliado.AltaAfiliado form = new Abm_Afiliado.AltaAfiliado(this.tipo_doc_usuario, this.username, this.user_id);

            form.Show();

            this.Hide();
        }

        private void buttonBajaAfiliado_Click(object sender, EventArgs e)
        {
            Abm_Afiliado.BusquedaAfiliado form = new Abm_Afiliado.BusquedaAfiliado("Baja");

            form.Show();

            this.Hide();
        }

        private void buttonModificarAfiliado_Click(object sender, EventArgs e)
        {
            Abm_Afiliado.BusquedaAfiliado form = new Abm_Afiliado.BusquedaAfiliado("Modificacion");

            form.Show();

            this.Hide();
        }

        private void buttonCompraBono_Click(object sender, EventArgs e)
        {
            Compra_Bono.CompraBono form = new Compra_Bono.CompraBono();

            form.Show();

            this.Hide();
        }

        private void buttonPedirTurno_Click(object sender, EventArgs e)
        {
            Pedir_Turno.PedirTurno form = new Pedir_Turno.PedirTurno();

            form.Show();

            this.Hide();
        }

        private void buttonCancelarTurno_Click(object sender, EventArgs e)
        {
            Cancelar_Atencion.CancelarAtencion form = new Cancelar_Atencion.CancelarAtencion();

            form.Show();

            this.Hide();
        }

        private void buttonRegistrarLlegada_Click(object sender, EventArgs e)
        {
            Registro_Llegada.RegistrarLlegada form = new Registro_Llegada.RegistrarLlegada();

            form.Show();

            this.Hide();
        }

        private void buttonRegistrarResultado_Click(object sender, EventArgs e)
        {
            Registro_Resultado.RegistrarResultado form = new Registro_Resultado.RegistrarResultado();

            form.Show();

            this.Hide();
        }

        private void buttonListadoEstadistico_Click(object sender, EventArgs e)
        {
            Listados.Listados form = new Listados.Listados();

            form.Show();

            this.Hide();
        }

        private void buttonCrearAgenda_Click(object sender, EventArgs e)
        {
            Crear_Agenda.CrearAgenda form = new Crear_Agenda.CrearAgenda();

            form.Show();

            this.Hide();
        }


    }
}
