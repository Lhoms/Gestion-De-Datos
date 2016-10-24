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


        public Form1()
        {

            InitializeComponent();

        }

        public Form1(String tipo_doc_usuario, String username)
        {

            InitializeComponent();

            comboBoxRol.ValueMember = "rol_descrip";
            comboBoxRol.DataSource = rolesDelUsuario(username, tipo_doc_usuario).Tables[0];

            this.tipo_doc_usuario = tipo_doc_usuario;
            this.username = username;

            this.labelFechaActual.Text = ConfigurationManager.AppSettings.Get("FechaSistema");


            ID_Usuario.Text = tipo_doc_usuario + " - " + username;



        }

        private DataSet rolesDelUsuario(string username, string tipo_doc_usuario)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {
                    };

            return DAL.Classes.DBHelper.ExecuteDataSet("NUL.get_roles_disponibles", dbParams);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void buttonCerrarSesion_Click(object sender, EventArgs e)
        {
            Login.Login login = new Login.Login();
            login.Show();

            this.Hide();


        }

        private void comboBoxRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            //aca calcularia que puede hacer en cada rol
            
            //buttonAltaAfiliado.Visible = "Aca va a validar el boton en funcion del rol";



        }


        private void buttonAltaRol_Click(object sender, EventArgs e)
        {
            AbmRol.AltaRol form = new AbmRol.AltaRol();

            form.Show();

            this.Hide();
        }

        private void buttonBajaRol_Click(object sender, EventArgs e)
        {
            AbmRol.BusquedaRol form = new AbmRol.BusquedaRol("Baja");

            form.Show();

            this.Hide();
        }

        private void buttonModificarRol_Click(object sender, EventArgs e)
        {
            AbmRol.BusquedaRol form = new AbmRol.BusquedaRol("Modificacion");

            form.Show();

            this.Hide();
        }

        private void buttonAltaAfiliado_Click(object sender, EventArgs e)
        {
            Abm_Afiliado.AltaAfiliado form = new Abm_Afiliado.AltaAfiliado(tipo_doc_usuario, username);

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
