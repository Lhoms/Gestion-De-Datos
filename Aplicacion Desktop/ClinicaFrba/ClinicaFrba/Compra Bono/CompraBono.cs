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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Compra_Bono
{
    public partial class CompraBono : Form
    {
        Sesion sesion;                      //mantengo la sesion
        Afiliado afiliado;

        Decimal precio_unitario;
        Decimal monto_total;

        public CompraBono(Sesion sesion)
        {
            InitializeComponent();

            this.sesion = sesion;
            afiliado = new Afiliado();

            this.labelFecha.Text = ConfigurationManager.AppSettings.Get("FechaSistema");
            this.labelGrupo.Text = "";
            this.labelPlan.Text = "";
            this.labelPrecioUnit.Text = "$0";
            this.labelMontoTot.Text = "$0";

            
        }

        private void buttonComprar_Click(object sender, EventArgs e)
        {
            try
            {
                comprarBono();



                Form1 form = new Form1(this.sesion);
                form.Show();
                this.Close();
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void comprarBono()
        {
            //NUL.sp_new_bono(@id_user numeric(18,0), @fecha datetime, @cantidad numeric(18,0), @monto numeric(16,2), @plan numeric(18,0), @result int output)
            //try
            //{

                SqlParameter result = DAL.Classes.DBHelper.MakeParamOutput("@result", SqlDbType.Decimal, 0);
                SqlParameter[] dbParams = new SqlParameter[]
            {
                DAL.Classes.DBHelper.MakeParam("@id_user", SqlDbType.Decimal, 250, afiliado.id),
                DAL.Classes.DBHelper.MakeParam("@fecha", SqlDbType.DateTime, 250, DateTime.Parse(ConfigurationManager.AppSettings.Get("FechaSistema").ToString())),
                DAL.Classes.DBHelper.MakeParam("@cantidad", SqlDbType.Decimal, 250, this.numericCantidad.Value),
                DAL.Classes.DBHelper.MakeParam("@monto", SqlDbType.Decimal, 250, this.monto_total),
                DAL.Classes.DBHelper.MakeParam("@plan", SqlDbType.Decimal, 250, afiliado.planMedico_id),
                result,
            };

                DAL.Classes.DBHelper.ExecuteDataSet("NUL.sp_new_bono", dbParams);

                if (int.Parse(result.Value.ToString()) == 0)
                    MessageBox.Show("Se realizo la compra correctamente", "Aviso", MessageBoxButtons.OK);
                else
                    throw new Exception("");

            //}
            //catch (Exception exc)
            //{
            //    MessageBox.Show("Fallo la compra", "Aviso", MessageBoxButtons.OK);
            //}
        }

        private void obtenerDatosDesdeNroAfiliado(string nroAfiliado)
        {
            string expresion = "SELECT * FROM NUL.Afiliado WHERE afil_nro_afiliado = '" + nroAfiliado + "'";

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            if (lector != null)
            {
                afiliado.id = int.Parse(lector["afil_id"].ToString());
                afiliado.planMedico_id = int.Parse(lector["afil_plan_med"].ToString());
                afiliado.numeroAfiliado = long.Parse(lector["afil_nro_afiliado"].ToString());
                afiliado.nroConsulta = int.Parse(lector["afil_nro_consulta"].ToString());
            }
            else
            {
                MessageBox.Show("Numero de afiliado no valido", "Aviso", MessageBoxButtons.OK);
            }

        }

        private void llenarInformacionSegunAfiliado()
        {
            string expresion = "SELECT plan_descrip, plan_precio_bono_cons FROM NUL.Plan_medico WHERE plan_id = '" + afiliado.planMedico_id + "'";

            SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(expresion);

            if (lector != null)
            {
                this.afiliado.planMedico = lector["plan_descrip"].ToString();
                this.precio_unitario = decimal.Parse(lector["plan_precio_bono_cons"].ToString());

            }
            else
            {
                //MessageBox.Show("Numero de afiliado no valido", "Aviso", MessageBoxButtons.OK);
            }
            
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1(this.sesion);

            form.Show();

            this.Close();
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            this.textBoxNroAfiliado.Text = "";
            this.numericCantidad.Value = 1;
            this.labelFecha.Text = ""; 
            this.labelGrupo.Text = "";
            this.labelPlan.Text = "";
            this.labelPrecioUnit.Text = "";
            this.labelMontoTot.Text = "";
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.textBoxNroAfiliado.Text))
                    throw new Exception("el campo numero de afiliado no puede estar vacio");

                obtenerDatosDesdeNroAfiliado(this.textBoxNroAfiliado.Text);
                llenarInformacionSegunAfiliado();
                
                this.labelPlan.Text = this.afiliado.planMedico;
                this.labelPrecioUnit.Text = this.precio_unitario.ToString();
                this.monto_total = this.precio_unitario * this.numericCantidad.Value;
                this.labelMontoTot.Text = this.monto_total.ToString();
                this.labelGrupo.Text = (this.afiliado.numeroAfiliado / 100).ToString();
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }

        }

        private void numericCantidad_ValueChanged(object sender, EventArgs e)
        {
            this.labelMontoTot.Text = (this.precio_unitario * this.numericCantidad.Value).ToString();
        }

    }
}
