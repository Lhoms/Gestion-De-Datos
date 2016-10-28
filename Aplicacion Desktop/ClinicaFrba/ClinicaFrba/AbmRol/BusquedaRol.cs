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

namespace ClinicaFrba.AbmRol
{
    public partial class BusquedaRol : Form
    {
        private string tipo_doc_usuario;        //datos del usuario actual
        private string username;
        private int user_id;

        List<string> func_descrip;              //arrays para trabajar combo box + check list
        Dictionary<string, int> funcionalidades;
        List<Boolean> funcionalidadesDelRol;
        List<int> funcionalidadesElegidas;

        Dictionary<int, Rol> roles;
        Dictionary<string, int> rol_descrip_id;
        List<string> rol_descrip;
        List<int> rol_id;

        int rol_actual;


        public BusquedaRol(string tipo_doc_usuario, string username, int user_id)
        {
            InitializeComponent();

            this.tipo_doc_usuario = tipo_doc_usuario;      //guardo los datos del usuario actual
            this.username = username;
            this.user_id = user_id;

            func_descrip = new List<string>();              //inicializo
            funcionalidadesElegidas = new List<int>();
            funcionalidades = new Dictionary<string, int>();
            funcionalidadesDelRol = new List<Boolean>();

            roles = new Dictionary<int, Rol>();
            rol_descrip = new List<string>();
            rol_descrip_id = new Dictionary<string, int>();
            rol_id = new List<int>();

            get_funcionalidades();                          //obtengo funcionalidades en func_descrip y funcionalidades
            get_roles();                                    //obtengo roles para rol_descrip, rol_id y roles

            this.checkedListFunciones.DataSource = func_descrip;  
            this.comboBoxRoles.DataSource = rol_descrip;

            llenarCheckListSegunRol();


        }

        private void llenarCheckListSegunRol()
        {
            this.rol_actual = getRolId(this.comboBoxRoles.Text);
           
            funcionalidadesDelRol.Clear();
            ObtenerFuncionalidadesPorRol();     //va a obtener una lista de booleanos en orden para asignar al checklist

            int i;


            for (i = 0; i < funcionalidades.Count; i++)
            {
                this.checkedListFunciones.SetItemChecked(i, funcionalidadesDelRol[i]);
            }
        }

        private int getRolId(string p)
        {
            return this.rol_descrip_id[p];
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

        private void ObtenerFuncionalidadesPorRol()
        {
            DataSet ds;
            DataRow[] dr;
            string expresion;
            funcionalidadesDelRol.Clear();

            SqlParameter[] dbParams = new SqlParameter[]
                    {
                         DAL.Classes.DBHelper.MakeParam("@id", SqlDbType.Int, 0, this.rol_actual),
                    };

            ds = DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_get_funciones_por_rol", dbParams);

            //ABM Rol
            //
            expresion = "func_descrip = 'ABM Rol'";
            dr = ds.Tables[0].Select(expresion);

            funcionalidadesDelRol.Add(dr.Count() > 0);


            //ABM Afiliado
            //
            expresion = "func_descrip = 'Abm Afiliado'";
            dr = ds.Tables[0].Select(expresion);

            funcionalidadesDelRol.Add(dr.Count() > 0);


            //Compra de Bonos
            //
            expresion = "func_descrip = 'Compra de bonos'";
            dr = ds.Tables[0].Select(expresion);

            funcionalidadesDelRol.Add(dr.Count() > 0);


            //Pedir Turno
            //
            expresion = "func_descrip = 'Pedir turno'";
            dr = ds.Tables[0].Select(expresion);

            funcionalidadesDelRol.Add(dr.Count() > 0);


            //Registrar llegada atencion medica
            //
            expresion = "func_descrip = 'Registro de llegada para atención médica'";
            dr = ds.Tables[0].Select(expresion);

            funcionalidadesDelRol.Add(dr.Count() > 0);


            //Registrar resultado atencion medica
            //
            expresion = "func_descrip = 'Registrar resultado para atención médica'";
            dr = ds.Tables[0].Select(expresion);

            funcionalidadesDelRol.Add(dr.Count() > 0);


            //Cancelar atencion medica
            //
            expresion = "func_descrip = 'Cancelar atención médica'";
            dr = ds.Tables[0].Select(expresion);

            funcionalidadesDelRol.Add(dr.Count() > 0);


            //Listado estadistico
            //
            expresion = "func_descrip = 'Listado estadístico'";
            dr = ds.Tables[0].Select(expresion);

            funcionalidadesDelRol.Add(dr.Count() > 0);


            //Listado estadistico
            //
            expresion = "func_descrip = 'Crear agenda'";
            dr = ds.Tables[0].Select(expresion);

            funcionalidadesDelRol.Add(dr.Count() > 0);


        }

        private void get_roles()
        {
            Rol rol;
            string expresion = "SELECT rol_id, rol_descrip, rol_habilitado FROM NUL.Rol WHERE rol_id IS NOT NULL";

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            if (lector.HasRows)
            {
                rol = new Rol();    //instancio la clase y seteo sus datos por fila

                rol.rol_descrip    = (string)lector["rol_descrip"].ToString();
                rol.rol_id         = int.Parse(lector["rol_id"].ToString());
                rol.rol_habilitado = (Boolean)lector["rol_habilitado"];

                roles.Add(rol.rol_id, rol);

                this.rol_descrip.Add((string)lector["rol_descrip"].ToString());
                this.rol_id.Add(int.Parse(lector["rol_id"].ToString()));
                this.rol_descrip_id.Add((string)lector["rol_descrip"].ToString(), (int.Parse(lector["rol_id"].ToString())));

                while (lector.Read())
                {
                    rol = new Rol();    //instancio la clase y seteo sus datos por fila

                    rol.rol_descrip = (string)lector["rol_descrip"].ToString();
                    rol.rol_id = int.Parse(lector["rol_id"].ToString());
                    rol.rol_habilitado = (Boolean)lector["rol_habilitado"];

                    roles.Add(rol.rol_id, rol);

                    this.rol_descrip.Add((string)lector["rol_descrip"].ToString());
                    this.rol_id.Add(int.Parse(lector["rol_id"].ToString()));
                    this.rol_descrip_id.Add((string)lector["rol_descrip"].ToString(), (int.Parse(lector["rol_id"].ToString())));
                }
            }

        }

        private void comboBoxRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(this.comboBoxRoles.Text);
            llenarCheckListSegunRol();
        }



    }
}
