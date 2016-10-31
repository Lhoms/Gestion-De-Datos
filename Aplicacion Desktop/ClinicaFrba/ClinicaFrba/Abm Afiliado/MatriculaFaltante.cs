using ClinicaFrba.extras;
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

namespace ClinicaFrba.Abm_Afiliado
{
    public partial class MatriculaFaltante : Form
    {
        Sesion sesion;
        long matricula;

        public MatriculaFaltante(Sesion sesion)
        {
            InitializeComponent();

            this.sesion = sesion;
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.textBoxMatricula.Text) || this.textBoxMatricula.Text == "0")
                    throw new Exception("El campo matricula no puede estar vacio");

                this.matricula = long.Parse(this.textBoxMatricula.Text);


                registrarMatricula();
          
            this.Close();
                
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }

        }

        private void registrarMatricula()
        {   
            try
            {
                SqlParameter result = DAL.Classes.DBHelper.MakeParamOutput("@result", SqlDbType.Int, 100);

                SqlParameter[] dbParams = new SqlParameter[]
            {
                DAL.Classes.DBHelper.MakeParam("@prof_id", SqlDbType.Decimal, 0, this.sesion.user_id),
                DAL.Classes.DBHelper.MakeParam("@matric", SqlDbType.Decimal, 50, Decimal.Parse(this.textBoxMatricula.Text)),
                result,
            };

                DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_set_matricula_profesional", dbParams);

                if ((int)result.Value != 0)
                    throw new Exception("Error agregando matricula, intente nuevamente");
                else
                {
                    MessageBox.Show("Se agrego correctamente la matricula numero " + this.textBoxMatricula.Text);
                    this.Hide();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
