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
    public partial class BusquedaAfiliado : Form
    {
        string admin_tipo_doc;
        string admin_username;
        int admin_id;

        DataTable result;
        extras.Afiliado afiliado;

        Dictionary<string, int> tipo_doc;   //para manejar los ids y datasource
        List<string> doc_descrip;

        Dictionary<string, int> planes;
        List<string> plan_descrip;


        public BusquedaAfiliado(string admin_tipo_doc, string admin_username,int admin_id)
        {
            //try
            //{
                InitializeComponent();

                this.admin_tipo_doc = admin_tipo_doc;
                this.admin_username = admin_username;
                this.admin_id = admin_id;



                afiliado = new extras.Afiliado();

                tipo_doc = new Dictionary<string, int>();
                doc_descrip = new List<string>();

                planes = new Dictionary<string, int>();
                plan_descrip = new List<string>();

                llenarComboBoxes();


            //}
            //catch (Exception exc)
            //{
            //    MessageBox.Show(exc.Message);
            //}

        }

        private void obtenerDatos(extras.Afiliado afiliado)
        {
            afiliado.username = this.textBoxUsername.Text;
            afiliado.tipo_doc = this.comboBoxTipo.Text;
            afiliado.nombre = this.textBoxNombre.Text;
            afiliado.apellido = this.textBoxApellido.Text;
            afiliado.documento_s = this.textBoxDni.Text;
            afiliado.direccion = this.textBoxDireccion.Text;
            afiliado.telefono_s = this.textBoxTelefono.Text;
            afiliado.mail = this.textBoxMail.Text;
            afiliado.sexo = this.comboBoxSexo.Text;
            afiliado.nacimiento = this.dateTimePicker1.Value;
            afiliado.planMedico = this.comboBoxPlan.Text;
            afiliado.numeroAfiliado_s = this.textBoxNroAfiliado.Text;
        }


        public DataTable getAfiliadosSegunDatos(extras.Afiliado afiliado)
        {
            string izq = "'%";
            string der = "%'";;

            
            SqlParameter[] dbParams = new SqlParameter[]
                    {
                        DAL.Classes.DBHelper.MakeParam("@username", SqlDbType.VarChar, 0, izq+ afiliado.username +der),
                        DAL.Classes.DBHelper.MakeParam("@tipo_doc", SqlDbType.VarChar, 0, izq+ tipo_doc[afiliado.tipo_doc].ToString() +der),
                        DAL.Classes.DBHelper.MakeParam("@nombre", SqlDbType.VarChar, 0, izq+ afiliado.nombre +der),
                        DAL.Classes.DBHelper.MakeParam("@apellido", SqlDbType.VarChar, 0, izq+ afiliado.apellido +der),
                        DAL.Classes.DBHelper.MakeParam("@documento", SqlDbType.VarChar, 0, izq+ afiliado.documento_s+der),
                        DAL.Classes.DBHelper.MakeParam("@direccion", SqlDbType.VarChar, 0, izq+ afiliado.direccion +der),
                        DAL.Classes.DBHelper.MakeParam("@telefono", SqlDbType.VarChar, 0, izq+ afiliado.telefono_s +der),
                        DAL.Classes.DBHelper.MakeParam("@mail", SqlDbType.VarChar, 0, izq+ afiliado.mail +der),
                        DAL.Classes.DBHelper.MakeParam("@sexo", SqlDbType.VarChar, 0, izq+ afiliado.sexo +der),
                        DAL.Classes.DBHelper.MakeParam("@fechaNac", SqlDbType.VarChar, 0, izq+ afiliado.nacimiento.ToString() +der),
                        DAL.Classes.DBHelper.MakeParam("@plan", SqlDbType.VarChar, 0, izq+ planes[afiliado.planMedico].ToString() +der),
                        DAL.Classes.DBHelper.MakeParam("@nroAfiliado", SqlDbType.VarChar, 0, izq+ afiliado.numeroAfiliado_s +der),
                    };


            return DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_buscar_usuarios", dbParams).Tables[0];

        }


        private void dataGridViewAfiliados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmdBuscar_Click(object sender, EventArgs e)
        {
            obtenerDatos(this.afiliado);
            result = getAfiliadosSegunDatos(this.afiliado);
            this.dataGridViewAfiliados.DataSource = result;
        }

        private void cmdLimpiar_Click(object sender, EventArgs e)
        {
            this.textBoxUsername.Text = "";
            this.textBoxMail.Text = "";
            this.textBoxNombre.Text = "";
            this.textBoxApellido.Text = "";
            this.textBoxDni.Text = "";
            this.textBoxDireccion.Text = "";
            this.textBoxTelefono.Text = "";
            this.textBoxNroAfiliado.Text = "";
        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1(this.admin_tipo_doc, this.admin_username, this.admin_id);
            form.Show();
            this.Close();
        }

        private void llenarComboBoxes()
        {
            this.comboBoxTipo.ValueMember = "doc_descrip";
            getTipoDoc();
            this.comboBoxTipo.DataSource = this.doc_descrip;

            this.comboBoxPlan.ValueMember = "plan_descrip";
            getPlanes();
            this.comboBoxPlan.DataSource = this.plan_descrip;
        }

        public void getTipoDoc()
        {
            string expresion = "SELECT doc_id, doc_descrip FROM NUL.Tipo_doc";

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            if (lector.HasRows)
            {
                tipo_doc.Add((string)lector["doc_descrip"].ToString(), int.Parse(lector["doc_id"].ToString()));
                doc_descrip.Add((string)lector["doc_descrip"].ToString());

                while (lector.Read())
                {
                    tipo_doc.Add((string)lector["doc_descrip"].ToString(), int.Parse(lector["doc_id"].ToString()));
                    doc_descrip.Add((string)lector["doc_descrip"].ToString());
                }
            }

        }

        public string get_tipo_doc_id(string tipo)
        {
            return (this.tipo_doc[tipo]).ToString();
        }


        public void getPlanes()
        {
            string expresion = "SELECT plan_id, plan_descrip FROM NUL.Plan_medico";

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            if (lector.HasRows)
            {
                planes.Add((string)lector["plan_descrip"].ToString(), int.Parse(lector["plan_id"].ToString()));
                plan_descrip.Add((string)lector["plan_descrip"].ToString());

                while (lector.Read())
                {
                    planes.Add((string)lector["plan_descrip"].ToString(), int.Parse(lector["plan_id"].ToString()));
                    plan_descrip.Add((string)lector["plan_descrip"].ToString());
                }
            }

        }

        public string get_plan_id(string plan)
        {
            return planes[plan].ToString();
        }


    }
}
