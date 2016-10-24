﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaFrba.extras
{
    class Afiliado
    {

        //Atributos como usuario
        public string username; //documento en string
        public string tipo_doc;

        //Atributos como persona
        public string nombre;
        public string apellido;
        public string direccion;
        public string mail;
        public int documento;
        public int telefono;
        public string sexo;
        public DateTime nacimiento;

        //Atributos como afiliado
        public int numeroAfiliado;
        public string estadoCivil;
        public string planMedico;
        public int cantFamiliares;
        public int nroConsulta;

    }
}