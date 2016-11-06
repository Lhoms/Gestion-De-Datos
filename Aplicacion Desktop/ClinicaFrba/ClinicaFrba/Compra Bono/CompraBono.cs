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

            this.buttonComprar.Enabled = false;

            
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
            try
            {

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
                    throw new Exception("Fallo la compra");

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void obtenerDatosDesdeNroAfiliado(string nroAfiliado)
        {
            string expresion = "SELECT * FROM NUL.Afiliado A JOIN NUL.Usuario U ON A.afil_id = U.user_id WHERE U.user_habilitado = 1 AND afil_nro_afiliado = '" + nroAfiliado + "'";

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
                throw new Exception("Numero de afiliado no valido");
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
                throw new Exception("Numero de afiliado no valido");
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
            this.labelFecha.Text = ConfigurationManager.AppSettings.Get("FechaSistema");; 
            this.labelGrupo.Text = "";
            this.labelPlan.Text = "";
            this.labelPrecioUnit.Text = "$0";
            this.labelMontoTot.Text = "$0";

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

                this.buttonComprar.Enabled = true;
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.textBoxNroAfiliado.Text))
                    throw new Exception("el campo numero de afiliado no puede estar vacio");

                string nroAfiliado = this.textBoxNroAfiliado.Text;
                string grupoFamiliar = (nroAfiliado).Substring(0, nroAfiliado.Length - 3) + "___";

                string select = "SELECT * FROM NUL.Bono B JOIN NUL.Bono_compra BC ON B.bono_compra = BC.bonoc_id JOIN NUL.Afiliado A ON BC.bonoc_id_usuario = A.afil_id ";
                string where = "WHERE B.bono_usado = 0 AND A.afil_nro_afiliado LIKE '" + grupoFamiliar + "' AND B.bono_plan = A.afil_plan_med";

                SqlDataReader lector = DAL.Classes.DBHelper.ExecuteQuery_DR(select + where);


                string mensaje = "No tiene bonos disponibles";

                if (lector != null)
                {
                    int i = 0;

                    mensaje = "Sus bonos disponibles son: \n";
                    mensaje = mensaje + lector["bono_id"] + "\n";

                    while (lector.Read() && i < 30)
                    {
                        mensaje = mensaje + lector["bono_id"] + "\n";
                        i++; //esto es para que no haya una ventana con mas de 30 lineas
                    }



                }

                MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK);
            }
        }

    }
}
