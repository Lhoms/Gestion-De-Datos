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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Abm_Afiliado
{
    public partial class ModificarAfiliado : Form
    {
        public Sesion sesion;
        public DataGridViewRow afiliado;
        public Afiliado afiliadoDatos;              //mantengo los viejos
        public Afiliado afiliadoDatosNuevos;        //agarro los nuevos

        public Dictionary<string, int> estado_civil;   //para manejar los ids y datasource
        public Dictionary<int, string> id_estado_descrip;
        public List<string> estado_descrip;


        public Dictionary<string, int> planes;
        public Dictionary<int, string> id_planes_descrip;
        public List<string> plan_descrip;

        public ModificarAfiliado(extras.Sesion sesion, DataGridViewRow dataGridViewRow)
        {
            InitializeComponent();

            this.sesion = sesion;
            this.afiliado = dataGridViewRow;

            estado_civil = new Dictionary<string, int>();
            id_estado_descrip = new Dictionary<int, string>();
            estado_descrip = new List<string>();


            planes = new Dictionary<string, int>();
            id_planes_descrip = new Dictionary<int, string>();
            plan_descrip = new List<string>();

            getEstados();
            getPlanes();
            this.comboBoxEstadoCivil.DataSource = this.estado_descrip;
            this.comboBoxPlan.DataSource = this.plan_descrip;

            afiliadoDatos = new Afiliado();
            afiliadoDatosNuevos = new Afiliado();
            rellenarAfiliado();

            this.richTextBox1.Enabled = false;

            this.labelUsuario.Text = this.afiliado.Cells[2].Value.ToString() + " - " +  //username
                                     this.afiliado.Cells[4].Value.ToString() + " " +    //nombre
                                     this.afiliado.Cells[5].Value.ToString(); ;         //apellido
            this.labelNroAfil.Text = this.afiliado.Cells[13].Value.ToString();          //nro afiliado

            modificacionHabilitada();
            modificarPlanHabilitado();
            rellenarBoxes();

        }

        private void modificarPlanHabilitado()
        {
            throw new NotImplementedException();
        }

        private void rellenarBoxes()
        {
            this.textBoxDireccion.Text = this.afiliadoDatos.direccion;
            this.textBoxTelefono.Text = this.afiliadoDatos.telefono_s;
            this.textBoxMail.Text = this.afiliadoDatos.mail;

            this.comboBoxSexo.Text = this.afiliadoDatos.sexo;
            this.comboBoxPlan.Text = this.id_planes_descrip[this.afiliadoDatos.planMedico_id];
            this.comboBoxEstadoCivil.Text = id_estado_descrip[this.afiliadoDatos.estadoCivil_id];
            this.richTextBox1.Text = "Sin descripcion";
        }

        private void rellenarAfiliado()
        {
            this.afiliadoDatos.id               = int.Parse(this.afiliado.Cells[1].Value.ToString());
            this.afiliadoDatos.username         = this.afiliado.Cells[2].Value.ToString();
            this.afiliadoDatos.tipo_doc_id      = int.Parse(this.afiliado.Cells[3].Value.ToString());
            this.afiliadoDatos.nombre           = this.afiliado.Cells[4].Value.ToString();
            this.afiliadoDatos.apellido         = this.afiliado.Cells[5].Value.ToString();
            this.afiliadoDatos.documento_s      = this.afiliado.Cells[6].Value.ToString();
            this.afiliadoDatos.direccion        = this.afiliado.Cells[7].Value.ToString();
            this.afiliadoDatos.telefono_s       = this.afiliado.Cells[8].Value.ToString();
            this.afiliadoDatos.mail             = this.afiliado.Cells[9].Value.ToString();   
            this.afiliadoDatos.numeroAfiliado   = long.Parse(this.afiliado.Cells[13].Value.ToString());
            this.afiliadoDatos.habilitado       = (bool)this.afiliado.Cells[14].Value;

            this.afiliadoDatos.sexo = this.afiliado.Cells[10].Value.ToString();
            this.afiliadoDatos.planMedico_id = int.Parse(this.afiliado.Cells[12].Value.ToString());
            this.afiliadoDatos.estadoCivil_id   = int.Parse(this.afiliado.Cells[15].Value.ToString());
        }

        private void modificacionHabilitada()
        {
            if (this.afiliadoDatos.habilitado)
            {
                this.buttonBaja.Text = "Dar de baja";
                this.groupBoxDatosPersonales.Enabled = true;
                this.groupBoxDatosAfiliado.Enabled = true;
            }

            else
            {
                this.buttonBaja.Text = "Dar de alta";
                this.groupBoxDatosPersonales.Enabled = false;
                this.groupBoxDatosAfiliado.Enabled = false;
            }
        }

        private void buttonBaja_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.buttonBaja.Text == "Dar de alta")
                {
                    habilitarUsuario(this.afiliadoDatos);

                    this.buttonBaja.Text = "Dar de baja";
                }

                else
                {
                    desabilitarUsuario(this.afiliadoDatos);

                    this.buttonBaja.Text = "Dar de alta";
                }
                modificacionHabilitada();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void desabilitarUsuario(Afiliado afiliado)
        {

            SqlParameter result = DAL.Classes.DBHelper.MakeParamOutput("@result", SqlDbType.Decimal, 10);
            SqlParameter[] dbParams = new SqlParameter[]
                    {
                        DAL.Classes.DBHelper.MakeParam("@id", SqlDbType.Decimal, 100, this.afiliadoDatos.id),
                        result,
                    };


            DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_del_usuario", dbParams);

            if (int.Parse(result.Value.ToString()) == 0)
            {
                this.afiliadoDatos.habilitado = false;
                MessageBox.Show("Se deshabilito al afiliado correctamente");
            }
            else
                MessageBox.Show("No se pudo dar de baja al afiliado");
        }

        private void habilitarUsuario(Afiliado afiliado)
        {

            SqlParameter result = DAL.Classes.DBHelper.MakeParamOutput("@result", SqlDbType.Decimal, 10);
            SqlParameter[] dbParams = new SqlParameter[]
                    {
                        DAL.Classes.DBHelper.MakeParam("@id", SqlDbType.Decimal, 100, this.afiliadoDatos.id),
                        result,
                    };


            DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_habil_usuario", dbParams);

            if (int.Parse(result.Value.ToString()) == 0)
            {
                this.afiliadoDatos.habilitado = true;
                MessageBox.Show("Se habilito al afiliado correctamente");
            }
            else
                MessageBox.Show("No se pudo habilitar al afiliado");
        }

        public void getEstados()
        {
            string expresion = "SELECT estado_id, estado_descrip FROM NUL.Estado";

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            if (lector != null)
            {
                estado_civil.Add((string)lector["estado_descrip"].ToString(), int.Parse(lector["estado_id"].ToString()));
                id_estado_descrip.Add(int.Parse(lector["estado_id"].ToString()), (string)lector["estado_descrip"].ToString());
                estado_descrip.Add((string)lector["estado_descrip"].ToString());

                while (lector.Read())
                {
                    estado_civil.Add((string)lector["estado_descrip"].ToString(), int.Parse(lector["estado_id"].ToString()));
                    id_estado_descrip.Add(int.Parse(lector["estado_id"].ToString()), (string)lector["estado_descrip"].ToString());
                    estado_descrip.Add((string)lector["estado_descrip"].ToString());
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
                id_planes_descrip.Add(int.Parse(lector["plan_id"].ToString()), (string)lector["plan_descrip"].ToString());
                plan_descrip.Add((string)lector["plan_descrip"].ToString());

                while (lector.Read())
                {
                    planes.Add((string)lector["plan_descrip"].ToString(), int.Parse(lector["plan_id"].ToString()));
                    id_planes_descrip.Add(int.Parse(lector["plan_id"].ToString()), (string)lector["plan_descrip"].ToString());
                    plan_descrip.Add((string)lector["plan_descrip"].ToString());
                }
            }

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            rellenarBoxes();
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1(this.sesion);
            form.Show();
            this.Close();
        }

        private void buttonModificar_Click(object sender, EventArgs e)
        {
            try
            {
                //llamar al stored que modifica afiliados
                MessageBox.Show("modificar afiliado");


                if (this.afiliadoDatos.planMedico_id != this.planes[this.comboBoxPlan.Text])
                {
                    //llamar al stored de modificar plan medico
                    MessageBox.Show("Se cambio de plan medico");
                    cambiarPlan();
                }


                MessageBox.Show("Se modifico correctamente al afiliado", "Aviso", MessageBoxButtons.OK);

                Form1 form = new Form1(this.sesion);
                form.Show();
                this.Close();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
   
        }

        private void cambiarPlan()
        {
           // NUL.sp_actualizar_plan(@afil numeric(18,0),@plan numeric(18,0),@motivo varchar(255),@error int output)

            int nuevoPlan = this.planes[this.comboBoxPlan.Text];
            DateTime fechaHoy = DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema"));

            SqlParameter result = DAL.Classes.DBHelper.MakeParamOutput("@error", SqlDbType.Int, 100);

            SqlParameter[] dbParams = new SqlParameter[]
            {
                DAL.Classes.DBHelper.MakeParam("@afil", SqlDbType.Decimal, 0, afiliadoDatos.id),
                DAL.Classes.DBHelper.MakeParam("@plan", SqlDbType.Decimal, 0, nuevoPlan),
                DAL.Classes.DBHelper.MakeParam("@fecha", SqlDbType.DateTime, 0, fechaHoy),
                DAL.Classes.DBHelper.MakeParam("@motivo", SqlDbType.VarChar, 0, this.richTextBox1.Text),
                result,
            };

            DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_actualizar_plan", dbParams);

            if ((int)result.Value != 0)
                throw new Exception("Error modificando el plan");
        }

        private void obtenerUsuario()
        {
 
            if (string.IsNullOrWhiteSpace(comboBoxSexo.Text))
                throw new Exception("El campo sexo no puede estar vacio");
            else this.afiliadoDatosNuevos.sexo = comboBoxSexo.Text;

            if (string.IsNullOrWhiteSpace(textBoxDireccion.Text))
                throw new Exception("El campo direccion no puede estar vacio");
            else this.afiliadoDatosNuevos.direccion = textBoxDireccion.Text;

            if (string.IsNullOrWhiteSpace(textBoxTelefono.Text))
                throw new Exception("El campo telefono no puede estar vacio");
            else this.afiliadoDatosNuevos.telefono = long.Parse(textBoxTelefono.Text);

            if (string.IsNullOrWhiteSpace(textBoxMail.Text))
            {
                throw new Exception("El campo mail no puede estar vacio");
            }
            else if (Regex.IsMatch(textBoxMail.Text,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                this.afiliadoDatosNuevos.mail = textBoxMail.Text;
            }
            else
            {
                throw new Exception("El formato del mail no es valido");
            }

            if (string.IsNullOrWhiteSpace(comboBoxEstadoCivil.Text))
                throw new Exception("El campo estado civil no puede estar vacio");
            else
            {
                this.afiliadoDatosNuevos.estadoCivil = comboBoxEstadoCivil.Text;
                this.afiliadoDatosNuevos.estadoCivil_id = this.estado_civil[comboBoxEstadoCivil.Text];
            }

            if (string.IsNullOrWhiteSpace(comboBoxPlan.Text))
                throw new Exception("El campo plan medico no puede estar vacio");
            else
            {
                this.afiliadoDatosNuevos.planMedico = comboBoxPlan.Text;
                this.afiliadoDatosNuevos.planMedico_id = this.planes[comboBoxPlan.Text];
            }

        }

        private void comboBoxPlan_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.afiliadoDatos.planMedico_id != this.planes[this.comboBoxPlan.Text])
            {
                this.richTextBox1.Enabled = true;
            }
            else
                this.richTextBox1.Enabled = false;
        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
        }

        private void buttonGrupo_Click(object sender, EventArgs e)
        {
            //le paso el row asi saca los datos utiles
            GrupoFamiliar form = new GrupoFamiliar(this.sesion, this.afiliado);
            form.Show();
            this.Hide();
        }

    }
}
