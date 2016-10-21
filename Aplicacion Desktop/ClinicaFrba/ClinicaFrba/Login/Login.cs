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

namespace ClinicaFrba.Login
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            DataSet ds = getTipoDoc();

            comboBoxTipo.ValueMember = "doc_descrip";
            comboBoxTipo.DataSource = ds.Tables[0];

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {

                if (string.IsNullOrEmpty(textBoxUser.Text))
                    throw new Exception("El campo usuario no puede estar vacio");
                
                if (string.IsNullOrEmpty(textBoxPass.Text))
                    throw new Exception("El campo contraseña no puede estar vacio");

                if (string.IsNullOrEmpty(comboBoxTipo.Text ))
                    throw new Exception("El campo tipo de documento no puede estar vacio");

                if (validarUsuario(textBoxUser.Text, comboBoxTipo.Text, textBoxPass.Text))
                {
                    if (intentosDisponibles(textBoxUser.Text, comboBoxTipo.Text, textBoxPass.Text) > 0)
                    {

                    Form1 form = new Form1(comboBoxTipo.Text, textBoxUser.Text);

                    form.Show();

                    this.Hide();
                    }

                    else throw new Exception("No dispone de intentos, contacte un administrador");
                }

                else throw new Exception("Login fallido, intente nuevamente\nQuedan "
                                     + intentosDisponibles(textBoxUser.Text, comboBoxTipo.Text, textBoxPass.Text).ToString()
                                     + " intentos");
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }

        }

        private int intentosDisponibles(string user, string tipoDoc, string password)
        {

            //llamar a sp y preguntarle

            return 3;
        }

        private bool validarUsuario (string user, string tipoDoc, string password)
        {

            //llamar a store y pedir esos datos y preguntar has rows

            return true;
        }

        public static DataSet getTipoDoc()
        {
                SqlParameter[] dbParams = new SqlParameter[]
                    {
                       
                    };


            return DAL.Classes.DBHelper.ExecuteDataSet("NUL.get_tipo_doc", dbParams);

        }

    }
}
