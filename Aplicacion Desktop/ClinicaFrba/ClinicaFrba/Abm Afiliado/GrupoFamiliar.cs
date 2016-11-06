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

namespace ClinicaFrba.Abm_Afiliado
{
    public partial class GrupoFamiliar : Form
    {
        public extras.Sesion sesion;
        public DataGridViewRow afiliado;
        public Afiliado afiliadoDatos;
 

        public GrupoFamiliar(extras.Sesion sesion, DataGridViewRow dataGridViewRow)
        {
            try
            {
                InitializeComponent();

                this.sesion = sesion;
                this.afiliado = dataGridViewRow;
                this.afiliadoDatos = new Afiliado();

                this.groupBox1.Enabled = false;

                comprobarSiPuedeCambiarGrupo();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }

        }

        private void comprobarSiPuedeCambiarGrupo()
        {
            if (tieneFamiliaresEnSuGrupo() && esRaiz()) //si es el afiliado creador del grupo no se puede salir
            {
                this.groupBox1.Enabled = false;

                MessageBox.Show("No puede cambiarse de grupo ya que esta asociado a uno con integrantes");
            }

            else if (!esRaiz())      //si no es raiz no puede cambiarse, ya esta asociado
            {
                this.groupBox1.Enabled = false;

                MessageBox.Show("No puede cambiarse de grupo ya que esta asociado a uno");
            }

            else if ((!tieneFamiliaresEnSuGrupo()) && esRaiz())      //es un afiliado unico en su grupo y puede agregarse a otros
            {
                this.groupBox1.Enabled = true;
            }


        }

        private bool esRaiz()
        {
            string nroAfiliado = this.afiliado.Cells[13].Value.ToString();
            nroAfiliado = nroAfiliado.Substring((nroAfiliado.Length - 2), 2);
            int numero = int.Parse(nroAfiliado);

            return (numero == 01);

        }

        private void esRaiz(string nro)
        {
            string nroAfiliado = nro;
            nroAfiliado = nroAfiliado.Substring((nroAfiliado.Length - 2), 2);
            int numero = int.Parse(nroAfiliado);

            if (numero != 01)
                throw new Exception("Ese numero no pertenece al numero de afiliado del titular del grupo");


        }

        private bool tieneFamiliaresEnSuGrupo()
        {
            string nroAfiliado = this.afiliado.Cells[13].Value.ToString();
            nroAfiliado = nroAfiliado.Substring(0, nroAfiliado.Length - 2) + "__";

            

            string expresion = "SELECT * FROM NUL.Afiliado WHERE afil_nro_afiliado LIKE '" + nroAfiliado + "'";

            DataTable ds = DAL.Classes.DBHelper.ExecuteQuery_DS(expresion).Tables[0];


            if (ds.Rows.Count > 1)
                return true;
            else
                return false;
        
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                agregarAlGrupo();
                notificarNuevoNumeroAfiliado();
                salir();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void notificarNuevoNumeroAfiliado()
        {
            string user_id = this.afiliado.Cells[1].Value.ToString();


            string expresion = "SELECT afil_nro_afiliado FROM NUL.Afiliado WHERE afil_id = " + user_id;

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            string mensaje = "Su nuevo numero de afiliado es: " + lector["afil_nro_afiliado"].ToString();

            MessageBox.Show( mensaje, "Aviso", MessageBoxButtons.OK);

        }

        private void agregarAlGrupo()
        {

            comprobarSiExisteElGrupo();

            comprobarQueNoSeaElMismo();

            esRaiz(this.textBox1.Text);

            int nroFamiliar = comprobarTipoFamiliar();
                
            this.afiliadoDatos.id = int.Parse(this.afiliado.Cells[1].Value.ToString());


            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DAL.Classes.DBHelper.MakeParam("@user_id", SqlDbType.Decimal, 100, this.afiliadoDatos.id),
                    DAL.Classes.DBHelper.MakeParam("@titular", SqlDbType.Decimal, 100, Decimal.Parse(this.textBox1.Text)),
                    DAL.Classes.DBHelper.MakeParam("@nro_familiar", SqlDbType.Decimal, 100, nroFamiliar),
                    DAL.Classes.DBHelper.MakeParam("@fecha", SqlDbType.DateTime, 100, DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema"))),

                };


            DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_agregar_a_grupo_familiar", dbParams);

            MessageBox.Show("Se agrego correctamente al grupo", "Aviso", MessageBoxButtons.OK);

        }

        private void comprobarQueNoSeaElMismo()
        {
            string afiliadoNro = this.afiliado.Cells[13].Value.ToString();

            if (textBox1.Text.Substring(0, textBox1.Text.Length - 2) == afiliadoNro.Substring(0, afiliadoNro.Length-2))
                throw new Exception("No puede agregarse a su mismo grupo familiar");
        }

        private int comprobarTipoFamiliar()
        {
            if (this.comboBox1.SelectedIndex == 0)
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
            string conyuge = (long.Parse(this.textBox1.Text) + 1).ToString();

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
            Form1 form = new Form1(this.sesion);   
            form.Show();
            this.Close();
        }


    }
}
