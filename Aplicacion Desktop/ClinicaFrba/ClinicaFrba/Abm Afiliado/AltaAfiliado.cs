using ClinicaFrba.extras;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Abm_Afiliado
{


    public partial class AltaAfiliado : Form
    {
        Sesion sesion;
        Afiliado afiliado;
        ArrayList familiares;
        string password;
        long creado_id; //en este voy a mantener el id del usuario creado
        long titular_nroAfil;

        List<string> doc_descrip;
        Dictionary<string, int> doc_id;

        List<string> plan_descrip;
        Dictionary<string, int> plan_id;

        List<string> estado_descrip;
        Dictionary<string, int> estado_id;

        public AltaAfiliado(Sesion sesion)
        {        
            InitializeComponent();
           
            this.afiliado = new Afiliado();

            doc_descrip = new List<string>();
            doc_id = new Dictionary<string, int>();

            plan_descrip = new List<string>();
            plan_id = new Dictionary<string, int>();

            estado_descrip = new List<string>();
            estado_id = new Dictionary<string, int>();

            password = "w23e"; //todos van a tener esta pass


            this.sesion = sesion;

            this.familiares = new ArrayList();

            this.dateTimePickerNacimiento.MaxDate = DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema"));
            
            getTipoDoc();
            getPlanes();
            getEstados();

            llenarComboBoxes();

            
        }

        private void llenarComboBoxes()
        {
            this.comboBoxTipoDoc.DataSource = this.doc_descrip;

            this.comboBoxEstadoCivil.DataSource = this.estado_descrip;
          
            this.comboBoxPlanMedico.DataSource = this.plan_descrip;

            this.buttonAgregarHijo.Enabled = false;
        }


        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                obtenerUsuario();

                DataSet ds = get_usuario(afiliado.username, afiliado.tipo_doc); //busca al usuario por si existe

                if (ds.Tables[0].Rows.Count > 0)
                {
                    throw new Exception("El usuario ya existe");
                }
               
                else
                {
                    nuevo_afiliado(this.afiliado);

                    notificarUsuarioNuevo(this.afiliado);

                    foreach (Afiliado element in familiares)
                    {
                        nuevo_afiliado(element);
                        notificarUsuarioNuevo(element);

                        agregarAlGrupo(element);
                    }

                    

                    Form1 form = new Form1(this.sesion);

                    form.Show();

                    this.Hide();
                }


            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }

        }

        private void agregarAlGrupo(Afiliado afiliad)
        {
            string ultimos2Digitos = (this.afiliado.numeroAfiliado.ToString());
            ultimos2Digitos = ultimos2Digitos.Substring(ultimos2Digitos.Length-2, 2);
            int tipo_familiar;

            if (ultimos2Digitos == "02")
                tipo_familiar = 0;
            else
                tipo_familiar = 1;


            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DAL.Classes.DBHelper.MakeParam("@user_id", SqlDbType.Decimal, 100, this.creado_id),
                    DAL.Classes.DBHelper.MakeParam("@titular", SqlDbType.Decimal, 100, this.titular_nroAfil),
                    DAL.Classes.DBHelper.MakeParam("@nro_familiar", SqlDbType.Decimal, 100, tipo_familiar),
                    DAL.Classes.DBHelper.MakeParam("@fecha", SqlDbType.DateTime, 100, DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema"))),

                };


            DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_agregar_a_grupo_familiar", dbParams);
        }

        private void notificarUsuarioNuevo(Afiliado usuario)
        {
            MessageBox.Show("Se agrego al afiliado:      " + usuario.nombre + "  " + usuario.apellido +
                          "\nNumero de afiliado:            " + usuario.numeroAfiliado +
                             "\n\n" +
                             "Usuario:                    " + usuario.documento +
                           "\nContraseña:                 " + "w23e");
        }

        private void obtenerUsuario()
        {
            if (string.IsNullOrWhiteSpace(textBoxNombre.Text))
                throw new Exception("El campo nombre no puede estar vacio");
            else this.afiliado.nombre = textBoxNombre.Text;

            if (string.IsNullOrWhiteSpace(textBoxApellido.Text))
                throw new Exception("El campo apellido no puede estar vacio");
            else this.afiliado.apellido = textBoxApellido.Text;

            if (string.IsNullOrWhiteSpace(comboBoxTipoDoc.Text))
                throw new Exception("El campo tipo de documento no puede estar vacio");
            else this.afiliado.tipo_doc_id = get_tipo_doc_id();

            if (string.IsNullOrWhiteSpace(textBoxDocumento.Text))
                throw new Exception("El campo documento no puede estar vacio");
            else
            {
                this.afiliado.documento = long.Parse(textBoxDocumento.Text);
                this.afiliado.username = textBoxDocumento.Text;
                this.afiliado.numeroAfiliado = 
                    (((long.Parse(textBoxDocumento.Text))*10) + get_tipo_doc_id()) * 100 + 1;

                this.titular_nroAfil = this.afiliado.numeroAfiliado;
            }

            if (string.IsNullOrWhiteSpace(comboBoxSexo.Text))
                throw new Exception("El campo sexo no puede estar vacio");
            else this.afiliado.sexo = comboBoxSexo.Text;

            if (string.IsNullOrWhiteSpace(textBoxDireccion.Text))
                throw new Exception("El campo direccion no puede estar vacio");
            else this.afiliado.direccion = textBoxDireccion.Text;

            if (string.IsNullOrWhiteSpace(textBoxTelefono.Text))
                throw new Exception("El campo telefono no puede estar vacio");
            else this.afiliado.telefono = long.Parse(textBoxTelefono.Text);

            if (string.IsNullOrWhiteSpace(textBoxMail.Text))
            {
                throw new Exception("El campo mail no puede estar vacio");
            }
            else if (Regex.IsMatch(textBoxMail.Text,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                this.afiliado.mail = textBoxMail.Text;
            }
            else
            {
                throw new Exception("El formato del mail no es valido");
            }

            if (string.IsNullOrWhiteSpace(comboBoxEstadoCivil.Text))
                throw new Exception("El campo estado civil no puede estar vacio");
            else this.afiliado.estadoCivil_id = get_estado_id();

            this.afiliado.cantFamiliares = 0;

            if (string.IsNullOrWhiteSpace(comboBoxPlanMedico.Text))
                throw new Exception("El campo plan medico no puede estar vacio");
            else this.afiliado.planMedico_id = get_plan_id();

            if (this.dateTimePickerNacimiento.Value > Convert.ToDateTime(ConfigurationManager.AppSettings.Get("FechaSistema")))
                throw new Exception("La fecha de nacimiento no puede ser mayor a hoy");
            else this.afiliado.nacimiento = this.dateTimePickerNacimiento.Value;

            this.afiliado.nroConsulta = 0;
        }

        private void nuevo_afiliado(Afiliado afiliado)
        {
            try
            {
                crearUsuario(afiliado);
                crearPersona(afiliado);
                crearAfiliado(afiliado);
                darRolAfiliado(afiliado);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void crearUsuario(Afiliado afiliado)
        {
            string username = afiliado.username;
            int tipo_doc = afiliado.tipo_doc_id;


            SqlParameter id = DAL.Classes.DBHelper.MakeParamOutput("@id", SqlDbType.Decimal, 100);

            SqlParameter[] dbParams = new SqlParameter[]
            {
                DAL.Classes.DBHelper.MakeParam("@user_username", SqlDbType.VarChar, 0, username),
                DAL.Classes.DBHelper.MakeParam("@user_tipodoc", SqlDbType.Decimal    , 0, tipo_doc), 
                DAL.Classes.DBHelper.MakeParam("@user_pass"    , SqlDbType.VarChar, 0, password),
                id,

            };


            DAL.Classes.DBHelper.ExecuteDataSet("NUL.agregar_usuario", dbParams);

            creado_id = long.Parse(id.Value.ToString());

        }

        private void crearPersona(Afiliado afiliado)
        {

            SqlParameter[] dbParams = new SqlParameter[]
            {
                DAL.Classes.DBHelper.MakeParam("@pers_id",        SqlDbType.Decimal,     0, creado_id),
                DAL.Classes.DBHelper.MakeParam("@pers_nombre",    SqlDbType.VarChar,     0, afiliado.nombre), 
                DAL.Classes.DBHelper.MakeParam("@pers_apellido",  SqlDbType.VarChar,     0, afiliado.apellido),
                DAL.Classes.DBHelper.MakeParam("@pers_doc",       SqlDbType.Decimal,     0, afiliado.documento),
                DAL.Classes.DBHelper.MakeParam("@pers_dire",      SqlDbType.Decimal,     0, afiliado.tipo_doc_id), 
                DAL.Classes.DBHelper.MakeParam("@pers_tel",       SqlDbType.Decimal,     0, afiliado.telefono),
                DAL.Classes.DBHelper.MakeParam("@pers_mail",      SqlDbType.VarChar,     0, afiliado.mail),
                DAL.Classes.DBHelper.MakeParam("@pers_fecha_nac", SqlDbType.DateTime,    0, afiliado.nacimiento), 
                DAL.Classes.DBHelper.MakeParam("@pers_sexo",      SqlDbType.Char,        0, afiliado.sexo),
                

            };

            DAL.Classes.DBHelper.ExecuteDataSet("NUL.agregar_persona", dbParams);
        }

        private void crearAfiliado(Afiliado afiliado)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DAL.Classes.DBHelper.MakeParam("@afil_id",           SqlDbType.Decimal,  0, this.creado_id),
                DAL.Classes.DBHelper.MakeParam("@afil_estado",       SqlDbType.Decimal,  0, this.afiliado.estadoCivil_id), 
                DAL.Classes.DBHelper.MakeParam("@afil_plan_med",     SqlDbType.Decimal,  0, get_plan_id()),
                DAL.Classes.DBHelper.MakeParam("@afil_nro_afiliado", SqlDbType.Decimal,  0, afiliado.numeroAfiliado),
            };

            DAL.Classes.DBHelper.ExecuteDataSet("NUL.agregar_afiliado", dbParams);
        }

        private void darRolAfiliado(Afiliado afiliado)
        {//NUL.sp_asignar_rol_afiliado(@user_id numeric(18,0))
            
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DAL.Classes.DBHelper.MakeParam("@user_id", SqlDbType.Decimal, 0, this.creado_id),
            };


            DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_asignar_rol_afiliado", dbParams);
        }

        private DataSet get_usuario(string username, string tipo_doc)
        {
            string expresion = "SELECT * FROM NUL.Usuario U WHERE U.user_username = '" + username + "' AND U.user_tipodoc = " + get_tipo_doc_id().ToString();
            return DAL.Classes.DBHelper.ExecuteQuery_DS(expresion);
        }


        private void buttonCancelar_Click(object sender, EventArgs e)
        {

            Form1 form = new Form1(this.sesion);

            form.Show();

            this.Hide();

        }


        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            textBoxNombre.Text = "";
            textBoxApellido.Text = "";
            textBoxDocumento.Text = "";
            comboBoxTipoDoc.Text = "";
            comboBoxSexo.Text = "";
            textBoxDireccion.Text = "";
            textBoxTelefono.Text = "";
            textBoxMail.Text = "";
            comboBoxEstadoCivil.Text = "";
            comboBoxPlanMedico.Text = "";

            familiares.Clear();
        }


        private void buttonAgregarConyuge_Click(object sender, EventArgs e)
        {
            try
            {
                agregarFamiliar("conyuge");
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }

        }


        private void buttonAgregarHijo_Click(object sender, EventArgs e)
        {

            try
            {
                agregarFamiliar("hijo");
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void agregarFamiliar(string tipo_alta)
        {
            if (string.IsNullOrWhiteSpace(textBoxDocumento.Text) || string.IsNullOrWhiteSpace(comboBoxPlanMedico.Text))
            {
                throw new Exception("El campo documento y plan no pueden estar vacios");
            }

            else
            {
                this.textBoxDocumento.Enabled = false;
                this.comboBoxPlanMedico.Enabled = false;

                long raizGrupoFamiliar = (((long.Parse(textBoxDocumento.Text)) * 10) + get_tipo_doc_id()) * 100;

                Abm_Afiliado.AltaFamiliar form;

                form = new Abm_Afiliado.AltaFamiliar(raizGrupoFamiliar, comboBoxPlanMedico.Text, tipo_alta, familiares);

                form.Show();

                if (tipo_alta == "conyuge")
                    this.buttonAgregarConyuge.Enabled = false;
                

            }
        }


        public void getTipoDoc()
        {
            string expresion = "SELECT doc_id, doc_descrip FROM NUL.Tipo_doc";

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            if (lector != null)
            {
                this.doc_descrip.Add(lector["doc_descrip"].ToString());
                this.doc_id.Add(lector["doc_descrip"].ToString(), int.Parse(lector["doc_id"].ToString()));

                while (lector.Read())
                {
                    this.doc_descrip.Add(lector["doc_descrip"].ToString());
                    this.doc_id.Add(lector["doc_descrip"].ToString(), int.Parse(lector["doc_id"].ToString()));
                }

            }

        }

        private int get_tipo_doc_id()
        {
            return this.doc_id[this.comboBoxTipoDoc.Text];
        }

        public void getEstados()
        {
            string expresion = "SELECT estado_id, estado_descrip FROM NUL.Estado";

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            if (lector != null)
            {
                estado_id.Add((string)lector["estado_descrip"].ToString(), int.Parse(lector["estado_id"].ToString()));
                estado_descrip.Add((string)lector["estado_descrip"].ToString());

                while (lector.Read())
                {
                    estado_id.Add((string)lector["estado_descrip"].ToString(), int.Parse(lector["estado_id"].ToString()));
                    estado_descrip.Add((string)lector["estado_descrip"].ToString());
                }
            }

        }

        private int get_estado_id()
        {
            return this.estado_id[this.comboBoxEstadoCivil.Text];
        }

        public void getPlanes()
        {
            string expresion = "SELECT plan_id, plan_descrip FROM NUL.Plan_medico";

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            if (lector != null)
            {
                plan_id.Add((string)lector["plan_descrip"].ToString(), int.Parse(lector["plan_id"].ToString()));
                plan_descrip.Add((string)lector["plan_descrip"].ToString());

                while (lector.Read())
                {
                    plan_id.Add((string)lector["plan_descrip"].ToString(), int.Parse(lector["plan_id"].ToString()));
                    plan_descrip.Add((string)lector["plan_descrip"].ToString());
                }
            }

        }

        private int get_plan_id()
        {
            return this.plan_id[this.comboBoxPlanMedico.Text];
        }

        private void comboBoxEstadoCivil_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.comboBoxEstadoCivil.Text == "Casado" || this.comboBoxEstadoCivil.Text == "Concubinato")
                this.comboBoxEstadoCivil.Enabled = true;
            else
                this.comboBoxEstadoCivil.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.buttonAgregarHijo.Enabled = this.checkBox1.Checked;
        }



    }
    
}
