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

        private void buttonModificarAfiliado_Click(object sender, EventArgs e)
        {

        }

        private void buttonModificarRol_Click(object sender, EventArgs e)
        {

        }

        private void buttonAltaAfiliado_Click(object sender, EventArgs e)
        {
            Abm_Afiliado.AltaAfiliado form = new Abm_Afiliado.AltaAfiliado(tipo_doc_usuario, username);

            form.Show();

            this.Hide();
        }
    }
}
