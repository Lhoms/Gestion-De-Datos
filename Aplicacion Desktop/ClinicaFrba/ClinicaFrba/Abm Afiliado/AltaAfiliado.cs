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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Abm_Afiliado
{


    public partial class AltaAfiliado : Form
    {

        Afiliado afiliado;
        ArrayList afiliados;
        string adm_tipo_doc;
        string adm_username;

        public AltaAfiliado()
        {
            InitializeComponent();
        }

        public AltaAfiliado(String tipo_doc_usuario, String usuario)
        {        
            InitializeComponent();
           
            this.afiliado = new Afiliado();

            this.adm_tipo_doc = tipo_doc_usuario;
            this.adm_username = usuario;
            this.afiliados = new ArrayList();

            this.comboBoxTipoDoc.ValueMember = "doc_descrip";
            this.comboBoxTipoDoc.DataSource = getTipoDoc().Tables[0];

            this.comboBoxEstadoCivil.ValueMember = "estado_descrip";
            this.comboBoxEstadoCivil.DataSource = getEstado().Tables[0];

            this.comboBoxPlanMedico.ValueMember = "plan_descrip";
            this.comboBoxPlanMedico.DataSource = getPlanes().Tables[0];

            buttonAgregarConyuge.Enabled = false;
            buttonAgregarHijo.Enabled = false;

            
        }

        public AltaAfiliado(int documento, string tipo_doc, string tipo_alta, ArrayList afiliados)
        {

            InitializeComponent();

            this.afiliado = new Afiliado();

            buttonAgregarConyuge.Enabled = false;
            buttonAgregarHijo.Enabled = false;

            if (tipo_alta == "conyuge")
            {
                afiliado.numeroAfiliado = documento * 100 + 1;
            }
            else if (tipo_alta == "hijo")
            {
                afiliado.numeroAfiliado = (documento * 100 + 2 + afiliados.Count);
            }
           
            



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
                    foreach (Afiliado element in afiliados)
                    {
                        nuevo_afiliado(element);
                    }

                    

                    Form1 form = new Form1(this.adm_tipo_doc, this.adm_username);

                    form.Show();

                    this.Hide();
                }
                

            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        private void obtenerUsuario()
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
                this.afiliado.documento = int.Parse(textBoxDocumento.Text);
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
            else this.afiliado.telefono = int.Parse(textBoxTelefono.Text);

            if (string.IsNullOrEmpty(textBoxMail.Text))
                throw new Exception("El campo mail no puede estar vacio");
            else this.afiliado.mail = textBoxMail.Text;

            if (string.IsNullOrEmpty(comboBoxEstadoCivil.Text))
                throw new Exception("El campo estado civil no puede estar vacio");
            else this.afiliado.estadoCivil = comboBoxEstadoCivil.Text;

            if (string.IsNullOrEmpty(numericUpDownCantHijos.Text))
                throw new Exception("El campo cantidad de hijos no puede estar vacio");
            else this.afiliado.cantFamiliares = int.Parse(numericUpDownCantHijos.Text);

            if (string.IsNullOrEmpty(comboBoxPlanMedico.Text))
                throw new Exception("El campo plan medico no puede estar vacio");
            else this.afiliado.planMedico = comboBoxPlanMedico.Text;
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

        }


        private void labelTipoDoc_Click(object sender, EventArgs e)
        {

        }


        private void buttonCancelar_Click(object sender, EventArgs e)
        {

            Form1 form = new Form1(this.adm_tipo_doc, this.adm_username);

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

        }


        private void buttonAgregarConyuge_Click(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(textBoxDocumento.Text) || string.IsNullOrEmpty(comboBoxTipoDoc.Text))
                {
                    throw new Exception("El campo documento y tipo no pueden estar vacios");
                }

                else
                {
                    Abm_Afiliado.AltaAfiliado form;

                    form = new Abm_Afiliado.AltaAfiliado(int.Parse(textBoxDocumento.Text), comboBoxTipoDoc.Text, "conyuge", afiliados);

                    form.Show();
                }
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
                if (string.IsNullOrEmpty(textBoxDocumento.Text) || string.IsNullOrEmpty(comboBoxTipoDoc.Text))
                {
                    throw new Exception("El campo documento y tipo no pueden estar vacios");
                }

                else
                {
                    Abm_Afiliado.AltaAfiliado form;

                    form = new Abm_Afiliado.AltaAfiliado(int.Parse(textBoxDocumento.Text), comboBoxTipoDoc.Text, "hijo", afiliados);

                    form.Show();
                }
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
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
                    if ((numericUpDownCantHijos.Value - afiliados.Count) == 0)
                    {
                        buttonAgregarConyuge.Enabled = false;
                        buttonAgregarHijo.Enabled = false;
                    }

                    else
                    if ((numericUpDownCantHijos.Value - afiliados.Count) == 1)
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
                    if ((numericUpDownCantHijos.Value - afiliados.Count) == 0)
                    {
                        buttonAgregarConyuge.Enabled = false;
                        buttonAgregarHijo.Enabled = false;
                    }

                    else
                        if ((numericUpDownCantHijos.Value - afiliados.Count) == 1)
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
                    if ((numericUpDownCantHijos.Value - afiliados.Count) == 0)
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
