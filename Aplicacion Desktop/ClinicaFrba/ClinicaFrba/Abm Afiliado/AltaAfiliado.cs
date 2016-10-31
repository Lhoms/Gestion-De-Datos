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

        DataSet dsDoc, dsEstado, dsPlanes;

        public AltaAfiliado(Sesion sesion)
        {        
            InitializeComponent();
           
            this.afiliado = new Afiliado();

            this.sesion = sesion;    
            this.familiares = new ArrayList();
            
            llenarComboBoxes();

            buttonAgregarConyuge.Enabled = false;
            buttonAgregarHijo.Enabled = false;

            
        }

        private void llenarComboBoxes()
        {
            this.comboBoxTipoDoc.ValueMember = "doc_descrip";
            this.dsDoc = getTipoDoc();
            this.comboBoxTipoDoc.DataSource = this.dsDoc.Tables[0];

            this.comboBoxEstadoCivil.ValueMember = "estado_descrip";
            this.dsEstado = getEstado();
            this.comboBoxEstadoCivil.DataSource = this.dsEstado.Tables[0];

            this.comboBoxPlanMedico.ValueMember = "plan_descrip";
            this.dsPlanes = getPlanes();
            this.comboBoxPlanMedico.DataSource = this.dsPlanes.Tables[0];
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

            if (string.IsNullOrWhiteSpace(textBoxDocumento.Text))
                throw new Exception("El campo documento no puede estar vacio");
            else
            {
                this.afiliado.documento = long.Parse(textBoxDocumento.Text);
                this.afiliado.username = textBoxDocumento.Text;
                this.afiliado.numeroAfiliado = (long.Parse(textBoxDocumento.Text)) * 100 + 1;
            }

            if (string.IsNullOrWhiteSpace(comboBoxTipoDoc.Text))
                throw new Exception("El campo tipo de documento no puede estar vacio");
            else this.afiliado.tipo_doc = comboBoxTipoDoc.Text;

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
            else this.afiliado.estadoCivil = comboBoxEstadoCivil.Text;

            if (string.IsNullOrWhiteSpace(numericUpDownCantHijos.Text))
                throw new Exception("El campo cantidad de hijos no puede estar vacio");
            else this.afiliado.cantFamiliares = int.Parse(numericUpDownCantHijos.Text);

            if (string.IsNullOrWhiteSpace(comboBoxPlanMedico.Text))
                throw new Exception("El campo plan medico no puede estar vacio");
            else this.afiliado.planMedico = comboBoxPlanMedico.Text;

            if (this.dateTimePickerNacimiento.Value > Convert.ToDateTime(ConfigurationManager.AppSettings.Get("FechaSistema")))
                throw new Exception("La fecha de nacimiento no puede ser mayor a hoy");
            else this.afiliado.nacimiento = this.dateTimePickerNacimiento.Value;

            this.afiliado.nroConsulta = 0;
        }

        private void nuevo_afiliado(Afiliado afiliado)
        {
            //llamar store para crear nuevo usuario+persona+afiliado
        }

        private DataSet get_usuario(string username, string tipo_doc)
        {
            string expresion = "SELECT * FROM NUL.Usuario U WHERE U.user_username = '" + username + "' AND U.user_tipodoc = " + get_tipo_doc_id(tipo_doc).ToString();
            return DAL.Classes.DBHelper.ExecuteQuery_DS(expresion);
        }


        private void comboBoxTipoDoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void labelTipoDoc_Click(object sender, EventArgs e)
        {

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
            //dateTimePickerNacimiento;
            comboBoxSexo.Text = "";
            textBoxDireccion.Text = "";
            textBoxTelefono.Text = "";
            textBoxMail.Text = "";
            comboBoxEstadoCivil.Text = "";
            numericUpDownCantHijos.Text = "";
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
                Abm_Afiliado.AltaFamiliar form;

                form = new Abm_Afiliado.AltaFamiliar(long.Parse(textBoxDocumento.Text), comboBoxPlanMedico.Text, tipo_alta, familiares);

                form.Show();
                

            }
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
            cambioCantidadDeFamiliares();
        }


        private void numericUpDownCantHijos_ValueChanged(object sender, EventArgs e)
        {
            cambioCantidadDeFamiliares();
        }


        private void cambioCantidadDeFamiliares()
        {
            switch (comboBoxEstadoCivil.Text)
            {
                case "Casado":
                    if ((numericUpDownCantHijos.Value - familiares.Count) == 0)
                    {
                        buttonAgregarConyuge.Enabled = false;
                        buttonAgregarHijo.Enabled = false;
                    }

                    else
                    if ((numericUpDownCantHijos.Value - familiares.Count) == 1)
                    {
                        buttonAgregarConyuge.Enabled = true;
                        buttonAgregarHijo.Enabled = false;
                    }

                    else
                    {
                        buttonAgregarConyuge.Enabled = true;
                        buttonAgregarHijo.Enabled = true;
                    }
                    break;

                case "Concubinato":
                    if ((numericUpDownCantHijos.Value - familiares.Count) == 0)
                    {
                        buttonAgregarConyuge.Enabled = false;
                        buttonAgregarHijo.Enabled = false;
                    }

                    else
                        if ((numericUpDownCantHijos.Value - familiares.Count) == 1)
                        {
                            buttonAgregarConyuge.Enabled = true;
                            buttonAgregarHijo.Enabled = false;
                        }

                        else
                        {
                            buttonAgregarConyuge.Enabled = true;
                            buttonAgregarHijo.Enabled = true;
                        }
                    break;

                default:
                    if ((numericUpDownCantHijos.Value - familiares.Count) == 0)
                    {
                        buttonAgregarConyuge.Enabled = false;
                        buttonAgregarHijo.Enabled = false;
                    }

                    else
                    {
                        buttonAgregarConyuge.Enabled = false;
                        buttonAgregarHijo.Enabled = true;
                    }

                    break;


            }
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


            return DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_get_tipo_doc", dbParams);

        }

        private int get_tipo_doc_id(string tipo_doc)
        {
            string expresion = "doc_descrip = '" + tipo_doc + "'";
            int tipo = 1;

            tipo = int.Parse(this.dsDoc.Tables[0].Rows[0][0].ToString());

            return tipo;
        }

        public DataSet getEstado()
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {
                       
                    };


            return DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_get_estados_civiles", dbParams);

        }

        private int get_estado_id(string estado)
        {
            string expresion = "estado_descrip = '" + estado + "'";
            int tipo = 1;

            tipo = int.Parse(this.dsEstado.Tables[0].Rows[0][0].ToString());

            return tipo;
        }

        public DataSet getPlanes()
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {
                       
                    };


            return DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_get_planes", dbParams);

        }

        private int get_plan_id(string plan)
        {
            string expresion = "plan_descrip = '" + plan + "'";
            int tipo = 1;

            tipo = int.Parse(this.dsPlanes.Tables[0].Rows[0][0].ToString());

            return tipo;
        }

        private void comboBoxTipoDoc_Validating(object sender, CancelEventArgs e)
        {
            //  
        }

    }
    
}
