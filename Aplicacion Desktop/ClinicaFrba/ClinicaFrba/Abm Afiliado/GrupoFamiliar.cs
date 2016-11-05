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
    public partial class GrupoFamiliar : Form
    {
        public extras.Sesion sesion;
        public DataGridViewRow afiliado;
        public Afiliado afiliadoDatos;
 

        public GrupoFamiliar(extras.Sesion sesion, DataGridViewRow dataGridViewRow)
        {
            InitializeComponent();

            this.sesion = sesion;
            this.afiliado = dataGridViewRow;
            this.afiliadoDatos = new Afiliado();
        }


        private void buttonAgregar_Click(object sender, EventArgs e)
        {

            agregarAlGrupo();

            salir();
        }

        private void agregarAlGrupo()
        {
            //try
            //{
                comprobarSiExisteElGrupo();

                int nroFamiliar = comprobarTipoFamiliar();
                
                this.afiliadoDatos.id = int.Parse(this.afiliado.Cells[1].Value.ToString());


                SqlParameter[] dbParams = new SqlParameter[]
                    {
                        DAL.Classes.DBHelper.MakeParam("@user_id", SqlDbType.Decimal, 100, this.afiliadoDatos.id),
                        DAL.Classes.DBHelper.MakeParam("@titular", SqlDbType.Decimal, 100, Decimal.Parse(this.textBox1.Text)),
                        DAL.Classes.DBHelper.MakeParam("@nro_familiar", SqlDbType.Decimal, 100, nroFamiliar),

                    };


                DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_agregar_a_grupo_familiar", dbParams);

            //}
            //catch (Exception exc)
            //{
            //    MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            //}
        }

        private int comprobarTipoFamiliar()
        {
            if (this.comboBox1.SelectedIndex == 1)
            {
                comprobarSiHayConyuge();
                return 0;
            }
            else
            {
                return 1;
            }
        }

        private void comprobarSiHayConyuge()
        {
            string conyuge = (int.Parse(this.textBox1.Text) + 1).ToString();

            MessageBox.Show(conyuge);

            string expresion = "SELECT * FROM NUL.Afiliado WHERE afil_nro_afiliado = " + conyuge;

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            if (lector != null)
            {
                throw new Exception("No esta disponible 'conyuge' en ese grupo familiar");
            }
        }

        private void comprobarSiExisteElGrupo()
        {
            string expresion = "SELECT * FROM NUL.Afiliado WHERE afil_nro_afiliado = " + textBox1.Text;

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            if (lector == null)
            {
                throw new Exception("No existe el grupo familiar indicado");
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            salir();
        }

        private void salir()
        {
            ModificarAfiliado form = new ModificarAfiliado(this.sesion, this.afiliado);
            form.Show();
            this.Hide();
        }


    }
}
