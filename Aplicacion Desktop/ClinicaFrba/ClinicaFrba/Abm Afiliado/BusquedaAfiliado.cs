﻿using ClinicaFrba.extras;
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

        Sesion sesion;

        DataTable result;
        extras.Afiliado afiliado;

        Dictionary<string, int> tipo_doc;   //para manejar los ids y datasource
        List<string> doc_descrip;

        Dictionary<string, int> planes;
        List<string> plan_descrip;


        public BusquedaAfiliado(Sesion sesion)
        {
            try
            {
                InitializeComponent();

                this.sesion = sesion;

                afiliado = new extras.Afiliado();

                tipo_doc = new Dictionary<string, int>();
                doc_descrip = new List<string>();

                planes = new Dictionary<string, int>();
                plan_descrip = new List<string>();

                llenarComboBoxes();

                this.comboBoxPlan.Enabled = false;
                this.comboBoxTipo.Enabled = false;
                this.comboBoxSexo.Enabled = false;
                

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

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
            afiliado.planMedico = this.comboBoxPlan.Text;
            afiliado.numeroAfiliado_s = this.textBoxNroAfiliado.Text;
        }


        public DataTable getAfiliadosSegunDatos(extras.Afiliado afiliado)
        {
            string izq = "%";
            string der = "%";

            string plan = "";
            string tipoDoc = "";
            string sexo = "";

            if (checkBoxPlan.Checked)
                plan = planes[afiliado.planMedico].ToString();

            if (checkBoxTipo.Checked)
                tipoDoc = tipo_doc[afiliado.tipo_doc].ToString();

            if (checkBoxSexo.Checked)
                sexo = afiliado.sexo;

            
            SqlParameter[] dbParams = new SqlParameter[]
                    {
                        DAL.Classes.DBHelper.MakeParam("@username", SqlDbType.VarChar, 100, izq+ afiliado.username +der),
                        DAL.Classes.DBHelper.MakeParam("@tipo_doc", SqlDbType.VarChar, 100, izq+ tipoDoc +der),
                        DAL.Classes.DBHelper.MakeParam("@nombre", SqlDbType.VarChar, 100, izq+ afiliado.nombre +der),
                        DAL.Classes.DBHelper.MakeParam("@apellido", SqlDbType.VarChar, 100, izq+ afiliado.apellido +der),
                        DAL.Classes.DBHelper.MakeParam("@documento", SqlDbType.VarChar, 100, izq+ afiliado.documento_s+der),
                        DAL.Classes.DBHelper.MakeParam("@direccion", SqlDbType.VarChar, 100, izq+ afiliado.direccion +der),
                        DAL.Classes.DBHelper.MakeParam("@telefono", SqlDbType.VarChar, 100, izq+ afiliado.telefono_s +der),
                        DAL.Classes.DBHelper.MakeParam("@mail", SqlDbType.VarChar, 100, izq+ afiliado.mail +der),
                        DAL.Classes.DBHelper.MakeParam("@sexo", SqlDbType.VarChar, 100, izq+ sexo +der),
                        DAL.Classes.DBHelper.MakeParam("@fechaNac", SqlDbType.VarChar, 100, izq+der),
                        DAL.Classes.DBHelper.MakeParam("@plan", SqlDbType.VarChar, 100, izq+ plan +der),
                        DAL.Classes.DBHelper.MakeParam("@nroAfiliado", SqlDbType.VarChar, 100, izq+ afiliado.numeroAfiliado_s +der),
                    };


            return DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_buscar_usuarios", dbParams).Tables[0];

        }


        private void dataGridViewAfiliados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewAfiliados.Columns[e.ColumnIndex].Name.Equals("Modificar"))
            {
                //le paso la fila entera, que despues tome los datos

                ModificarAfiliado form = new ModificarAfiliado(this.sesion, this.dataGridViewAfiliados.Rows[e.RowIndex]);
                form.Show();
                this.Hide();
            }   

        }

        private void cmdBuscar_Click(object sender, EventArgs e)
        {
            obtenerDatos(this.afiliado);

            result = getAfiliadosSegunDatos(this.afiliado);
            this.dataGridViewAfiliados.DataSource = result;

            this.dataGridViewAfiliados.Columns[0].HeaderText = "Modificar"; 
            this.dataGridViewAfiliados.Columns[1].Visible = false;
            this.dataGridViewAfiliados.Columns[2].HeaderText = "Cancelaciones"; 
            this.dataGridViewAfiliados.Columns[3].Visible = false;
            this.dataGridViewAfiliados.Columns[4].HeaderText = "Nombre";
            this.dataGridViewAfiliados.Columns[5].HeaderText = "Apellido";
            this.dataGridViewAfiliados.Columns[6].HeaderText = "Documento";
            this.dataGridViewAfiliados.Columns[7].HeaderText = "Direccion";
            this.dataGridViewAfiliados.Columns[8].HeaderText = "Telefono";
            this.dataGridViewAfiliados.Columns[9].HeaderText = "Mail";
            this.dataGridViewAfiliados.Columns[10].HeaderText = "Sexo"; this.dataGridViewAfiliados.Columns[10].Width = 40;
            this.dataGridViewAfiliados.Columns[11].HeaderText = "Nacimiento"; 
            this.dataGridViewAfiliados.Columns[12].Visible = false;
            this.dataGridViewAfiliados.Columns[13].HeaderText = "Afiliado"; 
            this.dataGridViewAfiliados.Columns[14].Visible = false;
            this.dataGridViewAfiliados.Columns[15].Visible = false;
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
            Form1 form = new Form1(this.sesion);
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

            if (lector != null)
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

        public void getPlanes()
        {
            string expresion = "SELECT plan_id, plan_descrip FROM NUL.Plan_medico";

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            if (lector != null)
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

        private void checkBoxPlan_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBoxPlan.Enabled = checkBoxPlan.Checked;
        }

        private void checkBoxTipo_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBoxTipo.Enabled = checkBoxTipo.Checked;
        }

        private void checkBoxSexo_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBoxSexo.Enabled = checkBoxSexo.Checked;
        }


    }
}
