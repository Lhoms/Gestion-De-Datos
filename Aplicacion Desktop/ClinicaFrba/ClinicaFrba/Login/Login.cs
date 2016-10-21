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

            DataSet ds = GetTipoDoc();

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

                //Validar aca el usuario+tipo y que la contraseña sea esa con un SP

                Form1 form = new Form1(comboBoxTipo.Text, textBoxUser.Text );

                form.Show();

                this.Hide();
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        public static DataSet GetTipoDoc()
        {
                SqlParameter[] dbParams = new SqlParameter[]
                    {
                       //aca van los parametros
                    };


            return DAL.Classes.DBHelper.ExecuteDataSet("get_tipo_doc", dbParams);

        }

    }
}
