using ClinicaFrba.extras;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace ClinicaFrba.Login
{
    public partial class Login : Form
    {
        string username;
        string password;
        int    tipo_doc_id;
        int user_id;

        DataSet ds;

        public Login()
        {
            InitializeComponent();

            ds = getTipoDoc();
            this.comboBoxTipo.ValueMember = "doc_descrip";
            this.comboBoxTipo.DataSource = ds.Tables[0];

            this.textBoxUser.Text = "admin";
            this.textBoxPass.Text = "w23e";

            ////comboBoxTipo.DataSource = DAL.Classes.DBHelper.ExecuteQuery_DS("SELECT * FROM NUL.Tipo_doc").Tables[0];
            //// DAL.Classes.DBHelper.ExecuteQuery_DS("SELECT * FROM NUL.Tipo_doc")["Doc_descrip"].ToString();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {

                obtenerDatos();

                if (validarUsuario(textBoxUser.Text, this.tipo_doc_id, textBoxPass.Text))
                {

                    Form1 form = new Form1(this.comboBoxTipo.Text, this.textBoxUser.Text, this.user_id);

                    form.Show();

                    this.Hide();
                }

                else
                {
                    throw new Exception("Login fallido, intente nuevamente");
                }

            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }

        }

        private void obtenerDatos()
        {
            if (string.IsNullOrEmpty(textBoxUser.Text))
                throw new Exception("El campo usuario no puede estar vacio");
            else this.username = textBoxUser.Text;

            if (string.IsNullOrEmpty(textBoxPass.Text))
                throw new Exception("El campo contraseña no puede estar vacio");
            else this.password = textBoxPass.Text;

            if (string.IsNullOrEmpty(comboBoxTipo.Text))
                throw new Exception("El campo tipo de documento no puede estar vacio");
            else
            {
                this.tipo_doc_id = get_tipo_doc_id(comboBoxTipo.Text);
            }
        }

        private int get_tipo_doc_id(string tipo_doc)
        {
            string expresion = "doc_descrip = '" + tipo_doc + "'";
            int tipo = 1;

            tipo = int.Parse(ds.Tables[0].Rows[0][0].ToString());

            return tipo;
        }

        
        private bool validarUsuario (string user, int tipoDoc, string password)
        {

                SqlParameter result_p  = DAL.Classes.DBHelper.MakeParamOutput("@result"  , SqlDbType.Int    , 100);
                SqlParameter error_p   = DAL.Classes.DBHelper.MakeParamOutput("@error"   , SqlDbType.VarChar, 100);
                SqlParameter user_id_p = DAL.Classes.DBHelper.MakeParamOutput("@id"      , SqlDbType.Int    , 100);

            SqlParameter[] dbParams = new SqlParameter[]
            {
                DAL.Classes.DBHelper.MakeParam("@username", SqlDbType.VarChar, 0, user),
                DAL.Classes.DBHelper.MakeParam("@tipo_doc", SqlDbType.Int    , 0, tipoDoc), 
                DAL.Classes.DBHelper.MakeParam("@pass"    , SqlDbType.VarChar, 0, password),
                result_p, error_p, user_id_p,

            };


            DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_login", dbParams);

            if ((int)result_p.Value == 0)
            {
                this.user_id = (int)user_id_p.Value;
                return true;
            }

            else
            {
                throw new Exception(error_p.Value.ToString());
            }

        }

        public static DataSet getTipoDoc()
        {
                SqlParameter[] dbParams = new SqlParameter[]
                    {
                       
                    };


            return DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_get_tipo_doc", dbParams);

        }

    }
}
