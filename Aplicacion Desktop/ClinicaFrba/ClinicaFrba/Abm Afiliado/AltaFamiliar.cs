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


    public partial class AltaFamiliar : Form
    {

        Afiliado afiliado;
        ArrayList afiliados;

        public AltaFamiliar()
        {
            InitializeComponent();
        }



        public AltaFamiliar(long documento, string plan, string tipo_alta, ArrayList afiliados)
        {

            InitializeComponent();

            this.afiliado = new Afiliado();
            this.afiliados = afiliados;

            this.buttonAgregarConyuge.Enabled = false;
            this.buttonAgregarHijo.Enabled = false;
            this.comboBoxPlanMedico.Enabled = false;
            this.numericUpDownCantHijos.Enabled = false;


            this.comboBoxTipoDoc.ValueMember = "doc_descrip";
            this.comboBoxTipoDoc.DataSource = getTipoDoc().Tables[0];

            this.comboBoxEstadoCivil.ValueMember = "estado_descrip";
            this.comboBoxEstadoCivil.DataSource = getEstado().Tables[0];

            this.comboBoxPlanMedico.ValueMember = "plan_descrip";
            this.comboBoxPlanMedico.DataSource = getPlanes().Tables[0];

            this.afiliado.planMedico = plan;

            if (tipo_alta == "conyuge")
            {
                afiliado.numeroAfiliado = documento * 100 + 2;
            }
            else if (tipo_alta == "hijo")
            {
                afiliado.numeroAfiliado = (documento * 100 + 3 + afiliados.Count);
            }


        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                obtenerFamiliar();

                DataSet ds = get_usuario(afiliado.username, afiliado.tipo_doc); //busca al usuario por si existe

                if (ds.Tables[0].Rows.Count > 0)
                {

                    throw new Exception("El usuario ya existe");
                }
               
                else
                {
                    afiliados.Add(this.afiliado);

                    this.Close();
                }
                

            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        private void obtenerFamiliar()
        {
            if (string.IsNullOrEmpty(textBoxNombre.Text))
                throw new Exception("El campo nombre no puede estar vacio");
            else this.afiliado.nombre = textBoxNombre.Text;

            if (string.IsNullOrEmpty(textBoxApellido.Text))
                throw new Exception("El campo apellido no puede estar vacio");
            else this.afiliado.apellido = textBoxApellido.Text;

            if (string.IsNullOrEmpty(textBoxDocumento.Text))
                throw new Exception("El campo documento no puede estar vacio");
            else
            {
                this.afiliado.documento = long.Parse(textBoxDocumento.Text);
                this.afiliado.username = textBoxDocumento.Text;
            }

            if (string.IsNullOrEmpty(comboBoxTipoDoc.Text))
                throw new Exception("El campo tipo de documento no puede estar vacio");
            else this.afiliado.tipo_doc = comboBoxTipoDoc.Text;

            if (string.IsNullOrEmpty(comboBoxSexo.Text))
                throw new Exception("El campo sexo no puede estar vacio");
            else this.afiliado.sexo = comboBoxSexo.Text;

            if (string.IsNullOrEmpty(textBoxDireccion.Text))
                throw new Exception("El campo direccion no puede estar vacio");
            else this.afiliado.direccion = textBoxDireccion.Text;

            if (string.IsNullOrEmpty(textBoxTelefono.Text))
                throw new Exception("El campo telefono no puede estar vacio");
            else this.afiliado.telefono = long.Parse(textBoxTelefono.Text);

            if (string.IsNullOrEmpty(textBoxMail.Text))
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

            if (string.IsNullOrEmpty(comboBoxEstadoCivil.Text))
                throw new Exception("El campo estado civil no puede estar vacio");
            else this.afiliado.estadoCivil = comboBoxEstadoCivil.Text;

            if (this.dateTimePickerNacimiento.Value > Convert.ToDateTime(ConfigurationManager.AppSettings.Get("FechaSistema")))
                throw new Exception("La fecha de nacimiento no puede ser mayor a hoy");
            else this.afiliado.nacimiento = this.dateTimePickerNacimiento.Value;

            this.afiliado.nroConsulta = 0;
            this.afiliado.cantFamiliares = 0;
            
        }

        private void nuevo_afiliado(Afiliado afiliado)
        {
            //llamar store para crear nuevo usuario+persona+afiliado
        }

        private DataSet get_usuario(string username, string tipo_doc)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {
                     DAL.Classes.DBHelper.MakeParam("@username", SqlDbType.VarChar, 0, afiliado.username),
                     DAL.Classes.DBHelper.MakeParam("@tipo_doc", SqlDbType.VarChar, 0, afiliado.tipo_doc)
                    };

            return DAL.Classes.DBHelper.ExecuteDataSet("NUL.get_usuario", dbParams);
        }


        private void comboBoxTipoDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
        }


        private void labelTipoDoc_Click(object sender, EventArgs e)
        {
            //
        }


        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();

        }


        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
           

            textBoxNombre.Text = "";
            textBoxApellido.Text = "";
            textBoxDocumento.Text = "";
            comboBoxTipoDoc.Text = "";
            //dateTimePickerNacimiento;
            comboBoxSexo.Text = "";
            textBoxDireccion.Text = "";
            textBoxTelefono.Text = "";
            textBoxMail.Text = "";
            comboBoxEstadoCivil.Text = "";
            numericUpDownCantHijos.Text = "";
            comboBoxPlanMedico.Text = "";

        }


        private void numericUpDownCantHijos_KeyDown(object sender, KeyEventArgs e)
        {
            //
        }


        private void numericUpDownCantHijos_KeyUp(object sender, KeyEventArgs e)
        {
            //
        }


        private void comboBoxEstadoCivil_TextChanged(object sender, EventArgs e)
        {
            //
        }


        private void numericUpDownCantHijos_ValueChanged(object sender, EventArgs e)
        {
            //
        }



        private void textBoxDocumento_TextChanged(object sender, EventArgs e)
        {
            //
        }

        private void comboBoxTipoDoc_TextChanged(object sender, EventArgs e)
        {
            //
        }

        public static DataSet getTipoDoc()
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {
                       
                    };


            return DAL.Classes.DBHelper.ExecuteDataSet("NUL.get_tipo_doc", dbParams);

        }

        public DataSet getEstado()
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {
                       
                    };


            return DAL.Classes.DBHelper.ExecuteDataSet("NUL.get_estados_civiles", dbParams);

        }

        public DataSet getPlanes()
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {
                       
                    };


            return DAL.Classes.DBHelper.ExecuteDataSet("NUL.get_planes", dbParams);

        }

    }
    
}
