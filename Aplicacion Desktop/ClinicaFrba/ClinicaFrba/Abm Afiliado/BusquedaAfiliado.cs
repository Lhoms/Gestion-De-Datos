﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Abm_Afiliado
{
    public partial class BusquedaAfiliado : Form
    {
        private string accion;

        public BusquedaAfiliado()
        {
            InitializeComponent();

            

        }

        public BusquedaAfiliado(string accion)
        {

            InitializeComponent();
            // aca va a desplegar todo en funcion de si accion es baja o modificacion
            this.accion = accion;
        }


        public static DataSet getAfiliadosSegun()
        {
            return new DataSet();

        }


    }
}
