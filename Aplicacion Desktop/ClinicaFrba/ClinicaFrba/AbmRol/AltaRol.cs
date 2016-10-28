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

namespace ClinicaFrba.AbmRol
{
    public partial class AltaRol : Form
    {
        private string tipo_doc_usuario;
        private string username;
        private int user_id;

        List<string> func_descrip;
        Dictionary<string, int> funcionalidades;
        List<int> funcionalidadesElegidas;


        public AltaRol(string tipo_doc_usuario, string username, int user_id)
        {
            InitializeComponent();

            this.tipo_doc_usuario = tipo_doc_usuario;
            this.username = username;
            this.user_id = user_id;

            func_descrip = new List<string>();
            funcionalidadesElegidas = new List<int>();
            funcionalidades = new Dictionary<string, int>();

            get_funcionalidades();


            this.checkListFuncionalidades.DataSource = func_descrip;

        }

        private void get_funcionalidades()
        {
            string expresion = "SELECT func_id, func_descrip FROM NUL.Funcionalidad WHERE func_id IS NOT NULL";

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            if (lector.HasRows)
            {
                funcionalidades.Add((string)lector["func_descrip"].ToString(), int.Parse(lector["func_id"].ToString()));
                func_descrip.Add((string)lector["func_descrip"].ToString());

                while (lector.Read())
                {
                    funcionalidades.Add((string)lector["func_descrip"].ToString(), int.Parse(lector["func_id"].ToString()));
                    func_descrip.Add((string)lector["func_descrip"].ToString());
                }
            }

        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                 if (string.IsNullOrWhiteSpace(this.textBoxNombre.Text))
                    throw new Exception("El campo nombre no puede estar vacio");

                int id_nuevoRol = crearNuevoRol(this.textBoxNombre.Text);
            

                getCheckedList();


                foreach (int numero in this.funcionalidadesElegidas)
                {
                    AgregarFuncionalidadARol(id_nuevoRol, numero);   
                }


                Form1 form = new Form1(this.tipo_doc_usuario, this.username, this.user_id);

                form.Show();

                this.Close();
                               
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        private int crearNuevoRol(string nuevoRol)
        {
            int nuevoId = 1;

            if (yaExisteElNombre(nuevoRol))
                throw new Exception("Ese nombre de rol ya existe");

            SqlParameter result_id = DAL.Classes.DBHelper.MakeParamOutput("@id_new", SqlDbType.Decimal, 0);

            SqlParameter[] dbParams = new SqlParameter[]
            {
                DAL.Classes.DBHelper.MakeParam("@descrip", SqlDbType.VarChar, 0, nuevoRol),
                result_id,
            };


            DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_new_rol", dbParams);


            if (((decimal)result_id.Value) > 0)
            {
                nuevoId = (int)(decimal)result_id.Value;

                MessageBox.Show("Se creo el nuevo rol: " + nuevoRol);

                return nuevoId;
            }

            else
            {
                throw new Exception("Error");
            }
        }


        private void AgregarFuncionalidadARol(int rol_id, int func_id)
        {
            SqlParameter result = DAL.Classes.DBHelper.MakeParamOutput("@result", SqlDbType.Int, 0);

            SqlParameter[] dbParams = new SqlParameter[]
            {
                DAL.Classes.DBHelper.MakeParam("@id", SqlDbType.Decimal, 0, rol_id),
                DAL.Classes.DBHelper.MakeParam("@id_func", SqlDbType.Decimal, 0, func_id),
                result,
            };

            DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_set_funcion_rol", dbParams);

            if (((int)result.Value) != 0)
                throw new Exception("Error agregando funcionalidades.\nIntente nuevamente desde modificar rol");
        }


        private void getCheckedList()
        {
            foreach (string unaFunc in checkListFuncionalidades.CheckedItems)
            {
                this.funcionalidadesElegidas.Add(this.funcionalidades[unaFunc]);
            }
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void limpiar()
        {

            for (int i = 0; i < this.checkListFuncionalidades.Items.Count; i++)
                checkListFuncionalidades.SetItemCheckState(i, CheckState.Unchecked);

            this.funcionalidadesElegidas.Clear();
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1(this.tipo_doc_usuario, this.username, this.user_id);

            form.Show();

            this.Close();
        }

        private void buttonTodo_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkListFuncionalidades.Items.Count; i++)
                checkListFuncionalidades.SetItemCheckState(i, CheckState.Checked);

            this.funcionalidadesElegidas.Clear();
        }

        private Boolean yaExisteElNombre(string rol)
        {

            string expresion = "SELECT * FROM NUL.Rol WHERE rol_descrip = '" + rol + "'";

            DataSet ds = DAL.Classes.DBHelper.ExecuteQuery_DS(expresion);

            return (ds.Tables[0].Rows.Count > 0);
        }
    
    
    }
}
