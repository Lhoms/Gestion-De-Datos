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
        Sesion sesion;

        string username;
        string password;
        int    tipo_doc_id;
        int user_id;

        DataSet ds;

        List<string> doc_descrip;
        Dictionary<string, int> doc_id;

        public Login()
        {
            InitializeComponent();

            this.textBoxUser.Text = "admin";
            this.textBoxPass.Text = "w23e";

            doc_descrip = new List<string>();
            doc_id = new Dictionary<string, int>();

            getTipoDoc();

            this.comboBoxTipo.DataSource = this.doc_descrip;

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
                    sesion = new Sesion(this.textBoxUser.Text, this.tipo_doc_id, this.user_id);

                    Form1 form = new Form1(this.sesion);

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
            if (string.IsNullOrWhiteSpace(textBoxUser.Text))
                throw new Exception("El campo usuario no puede estar vacio");
            else this.username = textBoxUser.Text;

            if (string.IsNullOrWhiteSpace(textBoxPass.Text))
                throw new Exception("El campo contraseña no puede estar vacio");
            else this.password = textBoxPass.Text;

            if (string.IsNullOrWhiteSpace(comboBoxTipo.Text))
                throw new Exception("El campo tipo de documento no puede estar vacio");
            else
            {
                this.tipo_doc_id = get_tipo_doc_id(comboBoxTipo.Text);
            }
        }

        private int get_tipo_doc_id(string tipo_doc)
        {
            return this.doc_id[this.comboBoxTipo.Text];
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

        public void getTipoDoc()
        {
            //    SqlParameter[] dbParams = new SqlParameter[]
            //        {

            //        };


            //return DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_get_tipo_doc", dbParams);

            string expresion = "SELECT doc_id, doc_descrip FROM NUL.Tipo_doc";

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            if (lector != null)
            {
                this.doc_descrip.Add(lector["doc_descrip"].ToString());
                this.doc_id.Add(lector["doc_descrip"].ToString(), int.Parse(lector["doc_id"].ToString()));

                while (lector.Read())
                {
                    this.doc_descrip.Add(lector["doc_descrip"].ToString());
                    this.doc_id.Add(lector["doc_descrip"].ToString(), int.Parse(lector["doc_id"].ToString()));
                }

            }

        }
    }
}
