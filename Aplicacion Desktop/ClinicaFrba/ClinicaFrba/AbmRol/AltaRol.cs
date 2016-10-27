using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.AbmRol
{
    public partial class AltaRol : Form
    {
        private string tipo_doc_usuario;
        private string username;
        private int user_id;

        Dictionary<string, bool> funcionalidadesElegidas;

        DataSet func;


        public AltaRol(string tipo_doc_usuario, string username, int user_id)
        {
            InitializeComponent();

            this.tipo_doc_usuario = tipo_doc_usuario;
            this.username = username;
            this.user_id = user_id;

            get_funcionalidades();


            this.dataGridViewFuncionalidades.DataSource = func.Tables[0];

            this.dataGridViewFuncionalidades.Columns["func_id"].Visible = false;
            this.dataGridViewFuncionalidades.Columns["func_descrip"].HeaderText = "Funcionalidades";
            this.dataGridViewFuncionalidades.Columns["func_descrip"].ReadOnly = true;


            dataGridViewFuncionalidades.AutoResizeColumns();
            dataGridViewFuncionalidades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
           



            funcionalidadesElegidas = new Dictionary<string, bool>();

        }

        private DataSet get_funcionalidades()
        {
            string expresion = "SELECT * FROM NUL.Funcionalidad WHERE func_id IS NOT NULL";

            return (func = DAL.Classes.DBHelper.ExecuteQuery_DS(expresion));
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void limpiar()
        {
            this.textBoxNombre.Text = "";
            DataGridViewRowCollection rc = this.dataGridViewFuncionalidades.Rows;

            foreach (DataGridViewRow row in rc)
            {
                row.Cells["CheckColumn"].Value = false;
            }

            this.funcionalidadesElegidas.Clear();
        }



        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            //try
            //{
                if(string.IsNullOrWhiteSpace(this.textBoxNombre.Text))
                    throw new Exception("El campo nombre no puede estar vacio");

                getChecks();

                if (this.funcionalidadesElegidas.ContainsKey("ABM Rol"))
                    if (this.funcionalidadesElegidas["ABM Rol"])
                        MessageBox.Show("ABM Rol");

                if (this.funcionalidadesElegidas.ContainsKey("Abm Afiliado"))
                    if (this.funcionalidadesElegidas["Abm Afiliado"])
                        MessageBox.Show("ABM Afiliado");

                if (this.funcionalidadesElegidas.ContainsKey("Compra de bonos"))
                    if (this.funcionalidadesElegidas["Compra de bonos"])
                        MessageBox.Show("Compra de bonos");

                if (this.funcionalidadesElegidas.ContainsKey("Pedir turno"))
                    if (this.funcionalidadesElegidas["Pedir turno"])
                        MessageBox.Show("Pedir turno");

                if (this.funcionalidadesElegidas.ContainsKey("Registro de llegada para atención médica"))
                    if (this.funcionalidadesElegidas["Registro de llegada para atención médica"])
                        MessageBox.Show("Registro de llegada para atención médica");

                if (this.funcionalidadesElegidas.ContainsKey("Registrar resultado para atención médica"))
                    if (this.funcionalidadesElegidas["Registrar resultado para atención médica"])
                        MessageBox.Show("Registrar resultado para atención médica");

                if (this.funcionalidadesElegidas.ContainsKey("Cancelar atención médica"))
                    if (this.funcionalidadesElegidas["Cancelar atención médica"])
                        MessageBox.Show("Cancelar atención médica");

                if (this.funcionalidadesElegidas.ContainsKey("Listado estadístico"))
                    if (this.funcionalidadesElegidas["Listado estadístico"])
                        MessageBox.Show("Listado estadístico");

                if (this.funcionalidadesElegidas.ContainsKey("Crear agenda"))
                    if (this.funcionalidadesElegidas["Crear agenda"])
                        MessageBox.Show("Crear agenda");



           // }

            //catch(Exception exc)
            //{
            //    MessageBox.Show(exc.Message);
            //}

        }

        private void getChecks()
        {
            DataGridViewRowCollection rc = this.dataGridViewFuncionalidades.Rows;

            foreach (DataGridViewRow row in rc)
            {
                
                if ((bool)row.Cells["CheckColumn"].Value)
                    funcionalidadesElegidas.Add((string)row.Cells["func_descrip"].Value, true);

            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {

        }

    
    
    }
}
