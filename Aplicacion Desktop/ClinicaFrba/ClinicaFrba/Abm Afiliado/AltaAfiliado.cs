using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Abm_Afiliado
{


    public partial class AltaAfiliado : Form
    {

        string username;             //setea con el usuario actual para despues volver a ese contexto
        string tipo_doc_usuario;

        public AltaAfiliado()
        {
            InitializeComponent();


        }

        public AltaAfiliado(String tipo_doc_usuario, String usuario)
        {
            InitializeComponent();

            this.tipo_doc_usuario = tipo_doc_usuario;  
            this.username = usuario;



        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxNombre.Text))
                    throw new Exception("El campo nombre no puede estar vacio");

                if (string.IsNullOrEmpty(textBoxApellido.Text))
                    throw new Exception("El campo apellido no puede estar vacio");

                if (string.IsNullOrEmpty(textBoxDocumento.Text))
                    throw new Exception("El campo documento no puede estar vacio");

                if (string.IsNullOrEmpty(comboBoxTipoDoc.Text))
                    throw new Exception("El campo tipo de documento no puede estar vacio");

                if (string.IsNullOrEmpty(comboBoxSexo.Text))
                    throw new Exception("El campo sexo no puede estar vacio");

                if (string.IsNullOrEmpty(textBoxDireccion.Text))
                    throw new Exception("El campo direccion no puede estar vacio");

                if (string.IsNullOrEmpty(textBoxTelefono.Text))
                    throw new Exception("El campo telefono no puede estar vacio");

                if (string.IsNullOrEmpty(textBoxMail.Text))
                    throw new Exception("El campo mail no puede estar vacio");

                if (string.IsNullOrEmpty(comboBoxEstadoCivil.Text))
                    throw new Exception("El campo estado civil no puede estar vacio");

                if (string.IsNullOrEmpty(numericUpDownCantHijos.Text))
                    throw new Exception("El campo cantidad de hijos no puede estar vacio");

                if (string.IsNullOrEmpty(comboBoxPlanMedico.Text))
                    throw new Exception("El campo plan medico no puede estar vacio");

                //Validar que el usuario no existe, el dni, etc..

                //cargar en la bd

                //volver

                Form1 form = new Form1(this.tipo_doc_usuario, this.username);

                form.Show();

                this.Hide();



            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        private void comboBoxTipoDoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void labelTipoDoc_Click(object sender, EventArgs e)
        {

        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {

            Form1 form = new Form1(this.tipo_doc_usuario, this.username);

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




    }
}
